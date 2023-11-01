﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using Web2.API.BusinessLogic;
using Web2.API.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web2.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class EvenementsController : ControllerBase
    {
        private readonly IEvenementBL _evenementBL;

        public EvenementsController(IEvenementBL evenementBL)
        {
            _evenementBL = evenementBL;
        }


        // GET: api/<EvenementsController>
        /// <summary>
        /// Lister tous les evenements de la plateforme
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Retourne un evenement</response>
        [HttpGet()]
        [ProducesResponseType(typeof(List<EvenementDTO>),(int)HttpStatusCode.OK)]
        public ActionResult<IEnumerable<EvenementDTO>> Get(int pageIndex = 1, int pageCount = 5, string searchQuery = null)
        {
            return Ok(_evenementBL.GetList(pageIndex, pageCount, searchQuery));
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
