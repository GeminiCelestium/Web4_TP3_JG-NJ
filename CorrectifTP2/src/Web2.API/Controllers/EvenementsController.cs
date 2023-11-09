using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework.Internal.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using Todolist.API;
using Web2.API.BusinessLogic;
using Web2.API.Data.Models;
using Web2.API.DTO;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web2.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class EvenementsController : ControllerBase
    {
        private readonly IEvenementBL _evenementBL;
        private static int idSequence = 1;
        public EvenementsController(IEvenementBL evenementBL)
        {
            _evenementBL = evenementBL;
        }

       private static readonly List<EvenementDTO> Events = new List<EvenementDTO>()
        {
            new EvenementDTO {ID = idSequence++, Titre = "Jazz Fest", VilleID = 1, Organisateur = "Def Jam",DateDebut = DateTime.Parse("2022-08-08T10:00:00"), DateFin = DateTime.Parse("2022-08-10T10:00:00"), Prix = 100.00, Description = "Festival de Jazz", CategoryIDs = new List<int> { 0 }},
            new EvenementDTO {ID = idSequence++, Titre = "Grand Prix" ,VilleID = 2, Organisateur = "7 ieme Ciel",DateDebut = DateTime.Parse("2022-08-08T10:00:00"), DateFin = DateTime.Parse("2022-08-10T10:00:00"), Prix = 70.00 , Description = "Festival de Rap", CategoryIDs = new List<int> { 0 }},
            new EvenementDTO {ID = idSequence++, Titre = "LAN" ,VilleID = 3, Organisateur = "LOL Esport ",DateDebut = DateTime.Parse("2023-02-01T10:00:00"), DateFin = DateTime.Parse("2023-02-09T10:00:00"), Prix = 00.00 , Description = "Evenement Esport League of Legend",CategoryIDs = new List<int> { 1 }}
        };

        [HttpGet]
        public Pageable<EvenementDTO> GetAll(string filterString, int pageIndex = 1, int pageSize = 5)
        {
            var query = Events
                .FindAll(evenement => string.IsNullOrEmpty(filterString) || evenement.Titre.Contains(filterString, StringComparison.CurrentCultureIgnoreCase) || evenement.Description.Contains(filterString, StringComparison.CurrentCultureIgnoreCase))
                 .Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            var todos = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            var pageCount = (double)query.Count() / pageSize;

            return new Pageable<EvenementDTO>() { data = todos, PageSize = pageSize, PageIndex = pageIndex, PageCount = (int)Math.Ceiling(pageCount) };
        }


        // GET api/evenements/5
        /// <summary>
        /// Obtenir un evenement par son ID
        /// </summary>
        /// <remarks> Sample of request:
        /// 
        ///    GET api/evenements/5
        ///     
        /// </remarks>
        /// <param name="id"> ID de l'evenement</param>
        /// <returns></returns>
        /// <response code="200">Retourne un evenement</response>
        /// <response code="404">Retourne une erreur si l'evenement est introuvable</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(EvenementDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public ActionResult<EvenementDTO> Get(int id)
        {
            var evenement = _evenementBL.Get(id);
            return evenement != null ? Ok(evenement) : NotFound(new { Errors = $"Element introuvable (id = {id})" });
        }

        // POST api/evenements
        /// <summary>
        /// Creer un evenement
        /// </summary>
        /// <param name="value">Événement a créer</param>
        /// <returns></returns>
        /// <response code="201">Retourne une reponse avec un header Loaction contenant l'url vers le GET de l'evenement créé</response>
        /// <response code="400">Retourne une erreur de validation de l'evenement</response>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Authorize(Policy = "requireManager")]
        public ActionResult Post([FromBody] EvenementDTO value)
        {
            value = _evenementBL.Add(value);

            return CreatedAtAction(nameof(Get), new { id = value.ID }, null);

        }

        // PUT api/evenements/5
        /// <summary>
        /// Modification d'un événement
        /// </summary>
        /// <param name="id">Identifiant de l'événement a modifier</param>
        /// <param name="value">Les nouvelles valeurs de l'événement</param>
        /// <returns></returns>
        /// <response code="204">Operation reussi. Reponse vide</response>
        /// <response code="400">Retourne une erreur de validation de l'evenement</response>
        /// <response code="404">Retourne une erreur si l'evenement est introuvable</response>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Authorize(Policy = "requireManager")]
        public ActionResult Put(int id, [FromBody] EvenementDTO value)
        {
            _evenementBL.Updade(id, value);

            return NoContent();
        }

        // DELETE api/evenements/5
        /// <summary>
        /// Supprimer un événement
        /// </summary>
        /// <param name="id">Identifiant de l'événement a supprimer</param>
        /// <returns></returns>
        /// <response code="204">Operation reussi. Reponse vide</response>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [Authorize(Policy = "requireManager")]
        public ActionResult Delete(int id)
        {
            _evenementBL.Delete(id);
            return NoContent();
        }

        // GET api/villes/2/evenements/
        /// <summary>
        /// Liste des événements d'une ville donnée
        /// </summary>
        /// <param name="villeId">Identifiant de la ville</param>
        /// <returns>Liste de participation <see cref="ParticipationDTO"/></returns>
        /// <response code="200">Lister des evenements de la ville</response>
        [HttpGet("/api/villes/{villeId}/[controller]")]
        [ProducesResponseType(typeof(List<EvenementDTO>), (int)HttpStatusCode.OK)]
        public ActionResult<IEnumerable<ParticipationDTO>> GetListByVille(int villeId, int pageIndex = 1, int pageCount = 5)
        {
            return Ok(_evenementBL.GetByVille(villeId, pageIndex, pageCount));
        }

        [HttpGet("{id}/totalventes")]
        [ProducesResponseType(typeof(double), (int)HttpStatusCode.OK)]
        public ActionResult<double> GetTotalVente(int id)
        {
            return Ok(_evenementBL.GetTotalVentes(id));
        }
    }
}

