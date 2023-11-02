using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Web2.API.Data;
using Web2.API.Data.Models;
using Web2.API.Data.Repositories;
using Web2.API.DTO;

namespace Web2.API.BusinessLogic
{
    public class VillesBL : IVilleBL
    {
        private readonly IVilleRepository _villeRepository;
        private readonly IEvenementRepository _evenementRepository;
        private readonly IMapper _mapper;

        public VillesBL(IVilleRepository villeRepository, IEvenementRepository evenementRepository, IMapper mapper)
        {
            _villeRepository = villeRepository;
            _evenementRepository = evenementRepository;
            _mapper = mapper;
        }

        public VilleDTO Add(VilleDTO value)
        {

            if (value == null )
            {
                throw new HttpException 
                { 
                    Errors = new { Errors = "Parametres d'entrés non valides" },
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }

            var ville = new Ville {Name = value.Name, Region = value.Region };
            _villeRepository.Create(ville);

            return _mapper.Map<VilleDTO>(ville);
        }

        public IEnumerable<VilleDTO> GetList(bool orderByPopularity)
        {
            return orderByPopularity ? _villeRepository.GetVillesOrderByPopularity() : _mapper.Map<IEnumerable<VilleDTO>>(_villeRepository.GetList());
        }

        public VilleDTO Get(int id)
        {
            return _mapper.Map<VilleDTO>(_villeRepository.GetVille(id));
        }

        public VilleDTO Updade(int id, VilleDTO value)
        {
            if (value == null)
            {
                throw new HttpException
                {
                    Errors = new { Errors = "Parametres d'entrés non valides" },
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }

            var ville = _villeRepository.GetById(id);

            if (ville == null)
            {
                throw new HttpException
                {
                    Errors = new { Errors = $"Element introuvable (id = {id})" },
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }

            ville.Name = value.Name;
            ville.Region = value.Region;
            _villeRepository.Update(ville);

            return _mapper.Map<VilleDTO>(ville);
        }

        public void Delete(int id)
        {
            var ville = _villeRepository.GetById(id);
            if (ville != null)
            {
                if (_evenementRepository.ExistForVille(ville.ID))
                {
                    throw new HttpException
                    {
                        Errors = new { Error = "Il n'est pas possible de supprimer une ville lié a au moins un evenement" },
                        StatusCode = StatusCodes.Status409Conflict
                    };

                }
                _villeRepository.Delete(ville);
            }
        }
    }
}
