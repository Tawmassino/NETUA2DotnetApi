using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NETUA2DotnetApi.Services;

namespace NETUA2DotnetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        /*
         Padarykite kad irasius nauja reiksme per POST ji butu issaugoma ir grazinama per GET metoda
         - Duomenu bazes nenaudoti
         - Sukurkite servisa ir naudokite Dependency Injection
        */
        //List<string> values = new List<string> { "value1", "value2" };

        private readonly IUzduotisValuesService _service;

        public ValuesController(IUzduotisValuesService service)
        {
            _service = service;
        }


        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return _service.Values;
        }
        [HttpPost]
        public ActionResult Post([FromBody] string value)
        {
            _service.Values.Add(value);
            Console.WriteLine(string.Join(", ", _service.Values));
            return Ok();
        }
    }
}
