using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestAspNet5.Business.Implementations;
using RestAspNet5.Data.VO;
using RestAspNet5.Hypermedia.Filters;
using System.Collections.Generic;

namespace RestAspNet5.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PersonController : ControllerBase
    {


        private readonly ILogger<PersonController> _logger;
        private IPersonBusiness _personBusiness;

        public PersonController(ILogger<PersonController> logger, IPersonBusiness personBusiness)
        {
            _logger = logger;
            _personBusiness = personBusiness;
        }



        [HttpGet]
        [ProducesResponseType((200), Type = typeof(List<PersonVO>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]

        public IActionResult Get()
        {

            return Ok(_personBusiness.FindAll());
        }



        [HttpGet("{id}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        [ProducesResponseType((200), Type = typeof(List<PersonVO>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Get(long id)
        {
            var person = _personBusiness.FindById(id);

            if (person == null) return NotFound();

            return Ok(_personBusiness.FindById(id));
        }


        [HttpPost]
        [TypeFilter(typeof(HyperMediaFilter))]
        [ProducesResponseType((200), Type = typeof(List<PersonVO>))]

        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Post([FromBody] PersonVO person)
        {
            if (person == null) return BadRequest();
            return Ok(_personBusiness.Create(person));
        }

        [HttpPut]
        [TypeFilter(typeof(HyperMediaFilter))]
        [ProducesResponseType((200), Type = typeof(List<PersonVO>))]

        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Put([FromBody] PersonVO person)
        {
            if (person == null) return BadRequest();
            return Ok(_personBusiness.Update(person));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Delete(long id)
        {
            _personBusiness.Delete(id);
            return NoContent();
        }
    }
}
