using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Web2.API.Data;
using Web2.API.Data.Models;
using Web2.API.Data.Repositories;
using Web2.API.DTO;

namespace Web2.API.BusinessLogic
{
    public class ParticipationBL : IParticipationBL
    {
        private readonly IParticipationRepository _participationRepository;
        private readonly IEvenementRepository _evenementRepository;
        private readonly IMapper _mapper;

        public ParticipationBL(IParticipationRepository participationRepository, IEvenementRepository evenementRepository, IMapper mapper)
        {
            _participationRepository = participationRepository;
            _evenementRepository = evenementRepository;
            _mapper = mapper;
        }

        public ParticipationDTO Add(ParticipationDTO value)
        {


            ProcessParticipationValidation(value);

            var participation = new Participation { Nom = value.Nom, Prenom = value.Prenom, NombrePlace = value.NombrePlace, Email = value.Email.Trim(), Evenement = _evenementRepository.GetById(value.EvenementId.Value) };
            _participationRepository.Create(participation);

            return _mapper.Map<ParticipationDTO>(participation);
        }

        public void Delete(int id)
        {
            var participation = _participationRepository.GetById(id);
            if (participation != null)
            {
                _participationRepository.Delete(participation);
            }
        }

        public ParticipationDTO Get(int id)
        {
            return _mapper.Map<ParticipationDTO>(_participationRepository.GetById(id));
        }

        public IEnumerable<ParticipationDTO> GetList()
        {
            return _mapper.Map<IEnumerable<ParticipationDTO>>(_participationRepository.GetAll());
        }

        public IEnumerable<ParticipationDTO> GetByEvent(int eventId)
        {
            return _mapper.Map<IEnumerable<ParticipationDTO>>(_participationRepository.GetByEvent(eventId));
        }

        public bool GetStatus(int id)
        {
            var participation = _participationRepository.GetByIdIgnoreQueryFilter(id);
            if (participation == null)
            {
                throw new HttpException
                {
                    Errors = new { Errors = $"Element introuvable (id = {id})" },
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }

            VerifyParticipation(participation);

            return participation.IsValid;
        }

        private void VerifyParticipation(Participation participation)
        {
            if (!participation.IsValid)
            {
                var isValid = new Random().Next(1, 10) > 5 ? true : false;//Simuler la validation externe;
                participation.IsValid = isValid;
                _participationRepository.SaveChanges();
            }

        }

        private void ProcessParticipationValidation(ParticipationDTO value)
        {
            string errorMsg = null;

            if (String.IsNullOrEmpty(value?.Nom))
            {
                errorMsg = "Le nom du participant est requis";
            }
            else if (String.IsNullOrEmpty(value?.Prenom))
            {
                errorMsg = "Le prenom du participant  est requis";
            }
            else if (!(value?.NombrePlace >= 0))
            {
                errorMsg = "Le nombre de place d'une participation doit etre valide";
            }
            else if (!IsValidEmail(value?.Email))
            {
                errorMsg = "Une adresse email valide est requise pour une participation";
            }
            else if (value?.EvenementId is null)
            {
                errorMsg = "L'identifiant de l'événement d'une participantion est requis";
            }
            else
            {
                var evenement = _evenementRepository.GetById(value.EvenementId.Value);
                if (evenement is null)
                {
                    errorMsg = $"L'événement  (id = {value.EvenementId}) n'existe pas";
                }
                else if (evenement.DateFin.CompareTo(DateTime.Now) < 0)
                {
                    errorMsg = "Il n'est pas possible de participer a un événement passé";
                }
                else if (_participationRepository.ExistForEvenementAndEmail(value.EvenementId.Value, value.Email))
                {
                    errorMsg = $"Il existe déja une particpation enregistré avec cette adresse email  (email = {value.Email}) pour cette événement.";
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

        private bool IsValidEmail(string email)
        {
            if (String.IsNullOrEmpty(email))
            {
                return false;
            }
            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

    }
}
