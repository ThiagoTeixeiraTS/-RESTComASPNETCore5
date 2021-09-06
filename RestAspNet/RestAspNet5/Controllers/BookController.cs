using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestAspNet5.Model;
using RestAspNet5.Business.Implementations;
using RestAspNet5.Data.VO;
using RestAspNet5.Hypermedia.Filters;
using System.Collections.Generic;

namespace RestAspNet5.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BookController : ControllerBase
    {


        private readonly ILogger<BookController> _logger;
        private IBookBusiness _bookBusiness;

        public BookController(ILogger<BookController> logger, IBookBusiness bookBusiness)
        {
            _logger = logger;
            _bookBusiness = bookBusiness;
        }



        [HttpGet]
        [TypeFilter(typeof(HyperMediaFilter))]
        [ProducesResponseType((200), Type = typeof(List<BookVO>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Get()
        {

            return Ok(_bookBusiness.FindAll());
        }



        [HttpGet("{id}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        [ProducesResponseType((200), Type = typeof(List<BookVO>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Get(long id)
        {
            var book = _bookBusiness.FindById(id);

            if (book == null) return NotFound();

            return Ok(_bookBusiness.FindById(id));
        }

        [HttpPost]
        [TypeFilter(typeof(HyperMediaFilter))]
        [ProducesResponseType((200), Type = typeof(List<BookVO>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Post([FromBody] BookVO book)
        {
            if (book == null) return BadRequest();
            return Ok(_bookBusiness.Create(book));
        }

        [HttpPut]
        [TypeFilter(typeof(HyperMediaFilter))]
        [ProducesResponseType((200), Type = typeof(List<BookVO>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Put([FromBody] BookVO book)
        {
            if (book == null) return BadRequest();
            return Ok(_bookBusiness.Update(book));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Delete(long id)
        {
            _bookBusiness.Delete(id);
            return NoContent();
        }
    }
}
