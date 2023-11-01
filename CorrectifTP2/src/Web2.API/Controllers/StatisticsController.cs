using Microsoft.AspNetCore.Authorization;
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
    public class StatisticsController : ControllerBase
    {
        private readonly IEvenementBL _evenementBL;
        private readonly IVilleBL _villeBL;

        public StatisticsController(IEvenementBL evenementBL, IVilleBL villeBL)
        {
            _evenementBL = evenementBL;
            _villeBL = villeBL;
        }


       
        [HttpGet("events")]
        [ProducesResponseType(typeof(List<object>),(int)HttpStatusCode.OK)]
        public ActionResult<IEnumerable<object>> GetEventsProfitability()
        {
            return Ok(_evenementBL.GetEventsProfitability());
        }

        [HttpGet("villes")]
        [ProducesResponseType(typeof(List<VillePopularityDTO>),(int)HttpStatusCode.OK)]
        public ActionResult<IEnumerable<VillePopularityDTO>> GetVillesPopularity()
        {
            return Ok(_villeBL.GetList(true));
        }
    }
}
