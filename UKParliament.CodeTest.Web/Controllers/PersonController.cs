using Microsoft.AspNetCore.Mvc;
using UKParliament.CodeTest.Services;
using UKParliament.CodeTest.Web.ViewModels;

namespace UKParliament.CodeTest.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        private readonly IPersonService _service;

        public PersonController(ILogger<PersonController> logger, IPersonService service)
        {
            _logger = logger;
            _service = service;
        }

        [Route("{id:int}")]
        [HttpGet]
        public ActionResult<PersonViewModel> GetById(int id)
        {
            return Ok(new PersonViewModel());
        }

        [Route("GetAll")]
        [HttpGet]
        public ActionResult<IList<PersonViewModel>> GetAll()
        {
            var people = new List<PersonViewModel>();
            for (var i = 0; i < 20; i++)
            {
                people.Add(new PersonViewModel { Id = i * 2 + 1, Name = $"Eirini_{i * 2 + 1}", DateOfBirth = new DateTime(2001, 7, 17), Address = "Athina", Details = "Kavlara" });
                people.Add(new PersonViewModel { Id = i * 2 + 2, Name = $"George_{i * 2 + 2}", DateOfBirth = new DateTime(1988, 8, 12), Address = "Lamia", Details = "Ntaks"});
            }
            return Ok(people);
        }
    }
}