using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryBL _categoryBL;
        private static int idSequence = 0;
        public CategoriesController(ICategoryBL categoryBL)
        {
            _categoryBL = categoryBL;
        }

        private static readonly List<CategoryDTO> Categorys  = new List<CategoryDTO>()
        {
            new CategoryDTO {ID = idSequence++, Name = "Festival"},
            new CategoryDTO {ID = idSequence++, Name = "LAN" },
            new CategoryDTO {ID = idSequence++, Name = "Sport" },
            new CategoryDTO {ID = idSequence++, Name = "Politique"}
        };

        // GET: api/<CategoriesController>
        [HttpGet]
        [ProducesResponseType(typeof(List<CategoryDTO>), StatusCodes.Status200OK)]
        public IEnumerable<CategoryDTO> Get()
        {
            return Categorys;
        }

        // GET api/<CategoriesController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CategoryDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Get(int id)
        {
            var category = _categoryBL.Get(id);
            return category is null ? NotFound(new { Errors = $"Element introuvable (id = {id})" }) : Ok(category);
        }

        // POST api/<CategoriesController>
        [HttpPost]
        [ProducesResponseType(typeof(CategoryDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Consumes(MediaTypeNames.Application.Json)]
        public ActionResult Post([FromBody] CategoryDTO value)
        {
            value = _categoryBL.Add(value);
            return CreatedAtAction(nameof(Get), new { id = value.ID }, value);
        }

        // PUT api/<CategoriesController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(CategoryDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Consumes(MediaTypeNames.Application.Json)]
        public ActionResult Put(int id, [FromBody] CategoryDTO value)
        {
            value = _categoryBL.Updade(id, value);
            return Ok(value);
        }

        // PUT api/<CategoriesController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult Delete(int id)
        {
            _categoryBL.Delete(id);
            return NoContent();
        }

    }
}
