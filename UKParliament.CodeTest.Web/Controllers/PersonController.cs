using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UKParliament.CodeTest.Data;
using UKParliament.CodeTest.Services;
using UKParliament.CodeTest.Web.ViewModels;

namespace UKParliament.CodeTest.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        private readonly IMapper _mapper;
        private readonly IPersonService _service;

        public PersonController(ILogger<PersonController> logger, IMapper mapper, IPersonService service)
        {
            _logger = logger;
            _service = service;
            _mapper = mapper;
        }

        [Route("{id:int}")]
        [HttpGet]
        public ActionResult<PersonViewModel> GetById(int id, CancellationToken ct = default)
        {
            return Ok(_mapper.Map<PersonViewModel>(_service.GetPersonByIdAsync(id, ct).Result));
        }

        [Route("{id:int}")]
        [HttpDelete]
        public ActionResult<PersonViewModel> DeleteById(int id, CancellationToken ct = default)
        {
            _logger.LogInformation("Attempting Person deletion");
            return _service.DeletePersonAsync(id, ct).Result ? Ok(true) : NotFound(id);
        }

        [HttpGet]
        public ActionResult<IList<PersonViewModel>> GetAll(CancellationToken ct = default)
        {
            return Ok(_mapper.Map<IEnumerable<PersonViewModel>>(_service.GetAllPersonsAsync(ct).Result));
        }

        [HttpPost("person")]
        [ProducesResponseType(201, Type = typeof(PersonViewModel))]
        [ProducesResponseType(400)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> AddPerson([FromBody] PersonViewModel person, CancellationToken ct = default)
        {
            if (person == null)
                return BadRequest($"{nameof(person)} cannot be null");

            var newPerson = _mapper.Map<Person>(person);

            var result = await _service.AddPersonAsync(newPerson, ct);
            if (result is not null)
            {
                return CreatedAtAction("GetById", new { id = result.Id }, result);
            }

            return BadRequest(ModelState);
        }

        [HttpPut("person")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdatePerson([FromBody] PersonViewModel person, CancellationToken ct = default)
        {
            if (person == null)
                return BadRequest($"{nameof(person)} cannot be null");

            var existingPerson = await _service.GetPersonByIdAsync(person.Id);

            if (existingPerson == null)
                return NotFound(person.Id);

            _mapper.Map(person, existingPerson);

            return await _service.UpdatePersonAsync(existingPerson, ct) ? NoContent() : BadRequest(ModelState);
        }
    }
}