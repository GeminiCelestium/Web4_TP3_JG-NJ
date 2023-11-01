using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Web2.API.Data;
using Web2.API.Data.Models;
using Web2.API.Data.Repositories;
using Web2.API.DTO;

namespace Web2.API.BusinessLogic
{
    public class CategoryBL : ICategoryBL
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryBL(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public CategoryDTO Add(CategoryDTO value)
        {
            if (value is null || String.IsNullOrEmpty(value.Name?.Trim()) )
            {
                throw new HttpException 
                {
                    Errors = new { Error = "Une valeur pour le champs 'Nom' est requis"},
                    StatusCode = StatusCodes.Status400BadRequest 
                };
            }

            var category = new Category { Name = value.Name.Trim().ToUpper() };
            _categoryRepository.Create(category);

            value.ID = category.ID;
            return value;
        }

        public void Delete(int id)
        {
            var category = _categoryRepository.GetByIdIncludingEvents(id);
            if (category != null)
            {
                if (category.Evenements.Count > 0)
                {
                    throw new HttpException
                    {
                        Errors = new { Error = "Il n'est pas possible de supprimer une categorie lié a au moins un evenement" },
                        StatusCode = StatusCodes.Status409Conflict
                    };

                }
                _categoryRepository.Delete(category);
            }
        }

        public CategoryDTO Get(int id)
        {
            var category = _categoryRepository.GetById(id);
            return category != null ? new CategoryDTO { ID = category.ID, Name = category.Name } : null;
        }

        public IEnumerable<CategoryDTO> GetList()
        {
            return _mapper.Map<IEnumerable<CategoryDTO>>(_categoryRepository.GetAll());
        }

        public CategoryDTO Updade(int id, CategoryDTO value)
        {
            if (value == null || String.IsNullOrEmpty(value.Name?.Trim()))
            {
                throw new HttpException
                {
                    Errors = new { Error = "Une valeur pour le champs 'Nom' est requis" },
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }

            var category = _categoryRepository.GetById(id);


            if (category == null)
            {
                throw new HttpException
                {
                    Errors = new { Errors = $"Element introuvable (id = {id})" },
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }

            category.Name = value.Name.Trim().ToUpper();
            _categoryRepository.Update(category);

            return value;
        }
    }
}
