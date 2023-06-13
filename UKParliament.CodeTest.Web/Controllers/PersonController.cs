using Microsoft.AspNetCore.Mvc;
using UKParliament.CodeTest.Web.ViewModels;

namespace UKParliament.CodeTest.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;

        public PersonController(ILogger<PersonController> logger)
        {
            _logger = logger;
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
            var people = new List<PersonViewModel>() 
            {
                new PersonViewModel { Name = "Eirini", DateOfBirth = new DateTime(2001, 7, 17), Address = "Athina", Details = "Kavlara"},
                new PersonViewModel { Name = "George", DateOfBirth = new DateTime(1988, 8, 12), Address = "Lamia", Details = "Ntaks"}
            };
            return Ok(people);
        }
    }
}