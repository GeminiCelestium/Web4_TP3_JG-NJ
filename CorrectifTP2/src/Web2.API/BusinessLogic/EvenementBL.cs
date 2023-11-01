using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web2.API.Data;
using Web2.API.Data.Models;
using Web2.API.Data.Repositories;
using Web2.API.DTO;

namespace Web2.API.BusinessLogic
{
    public class EvenementBL : IEvenementBL
    {
        private readonly IEvenementRepository _evenementRepository;
        private readonly IVilleRepository _villeRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public EvenementBL(IEvenementRepository evenementRepository, IVilleRepository villeRepository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            _evenementRepository = evenementRepository;
            _villeRepository = villeRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public EvenementDTO Add(EvenementDTO value)
        {

            ProcessEventValidation(value);

            var ville = _villeRepository.GetById(value.VilleID.Value);
            var categories = _categoryRepository.GetList(value.CategoryIDs).ToList();
            var evenement = new Evenement { Adresse = value.Adresse, DateDebut = value.DateDebut.Value, DateFin = value.DateFin.Value, Description = value.Description, Organisateur = value.Organisateur, prix = value.Prix.Value, Titre = value.Titre, Ville = ville, Categories = categories };
            _evenementRepository.Create(evenement);
            value.ID = evenement.ID;
            return value;
        }

        public void Delete(int id)
        {
            var evenement = _evenementRepository.GetById(id);
            if (evenement is not null)
            {
                _evenementRepository.Delete(id);
            }
        }

        public EvenementDTO Get(int id)
        {
            return _mapper.Map<EvenementDTO>(_evenementRepository.GetEvent(id));
        }

        public IEnumerable<EvenementDTO> GetList(int pageIndex, int pageCount, string searchQuery)
        {
            return _mapper.Map<IEnumerable<EvenementDTO>>(_evenementRepository.GetList(pageIndex, pageCount, searchQuery));
        }

        public EvenementDTO Updade(int id, EvenementDTO value)
        {
            var evenement = _evenementRepository.GetByIdIncludingCategories(id);


            if (evenement == null)
            {
                throw new HttpException
                {
                    Errors = new { Errors = $"Element introuvable (id = {id})" },
                    StatusCode = StatusCodes.Status404NotFound
                };
            }

            ProcessEventValidation(value);
            
            evenement.Categories.Clear();
            _evenementRepository.SaveChanges();

            evenement.Titre = value.Titre;
            evenement.prix = value.Prix.Value;
            evenement.Description = value.Description;
            evenement.Organisateur = value.Organisateur;
            evenement.DateDebut = value.DateDebut.Value;
            evenement.DateFin = value.DateFin.Value;
            evenement.Adresse = value.Adresse;
            evenement.Ville = _villeRepository.GetById(value.VilleID.Value);
            evenement.Categories = _categoryRepository.GetList(value.CategoryIDs).ToList();

            _evenementRepository.Update(evenement);

            return value;
        }

        public IEnumerable<EvenementDTO> GetByVille(int villeId, int pageIndex, int pageCount)
        {
            return _mapper.Map<IEnumerable<EvenementDTO>>(_evenementRepository.GetByVilleId(villeId, pageIndex, pageCount));
        }

        public double GetTotalVentes(int eventId)
        {
            return _evenementRepository.GetTotalVentes(eventId);
        }
        public object GetEventsProfitability(){
            return _evenementRepository.GetEventsProfitability();
        }

        private void ProcessEventValidation(EvenementDTO value)
        {
            string errorMsg = null;

            if (String.IsNullOrEmpty(value?.Titre))
            {
                errorMsg = "Le Titre d'un evenement est requis";
            }
            else if (String.IsNullOrEmpty(value?.Description))
            {
                errorMsg = "La description d'un evenement est requis";
            }
            else if (String.IsNullOrEmpty(value?.Organisateur))
            {
                errorMsg = "L'organisateur d'un evenement est requis";
            }
            else if (String.IsNullOrEmpty(value?.Adresse))
            {
                errorMsg = "L'adresse de rue d'un evenement est requis";
            }
            else if (!value.DateDebut.HasValue)
            {
                errorMsg = "La date de debut d'un evenement est requis";
            }
            else if (!value.DateFin.HasValue)
            {
                errorMsg = "La date de fin d'un evenement est requis";
            }
            else if (value.DateFin.Value.CompareTo(value.DateDebut.Value) <= 0)
            {
                errorMsg = "La date de fin d'un evenement ne peut pas etre avant la date de debut";
            }
            else if (value.DateDebut.Value.CompareTo(DateTime.Now) <= 0)
            {
                errorMsg = "La date de debut d'un evenement ne peut pas dans le passé";
            }
            else if (value?.CategoryIDs?.Any() != true)
            {
                errorMsg = "Une Categorie au moins est requis pour un evenement";
            }
            else if (value?.VilleID is null)
            {
                errorMsg = "La  Ville  d'un evenement est requis";
            }
            else
            {
                if (!_villeRepository.Exist(value.VilleID.Value))
                {
                    errorMsg = $"La  Ville  (id = {value.VilleID}) n'existe pas";
                }
                else
                {
                    foreach (var categoryId in value.CategoryIDs)
                    {
                        if (!_categoryRepository.Exist(categoryId))
                        {
                            errorMsg = $"La  category (id = {categoryId}) n'existe pas";
                            break;
                        }
                    }
                }
            }

            if (errorMsg is not null)
            {
                throw new HttpException
                {
                    Errors = new { Errors = errorMsg },
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
        }
    }
}