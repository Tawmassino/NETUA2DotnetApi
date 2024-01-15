using Microsoft.AspNetCore.Mvc;
using NETUA2DotnetApi.DataLayer.Models;
using NETUA2DotnetApi.DataLayer.Repositories;
using NETUA2DotnetApi.Dtos;
using NETUA2DotnetApi.Services;
using System.Net.Mime;

namespace NETUA2DotnetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoRepository _repository;
        private readonly IToDoEmailService _emailService;
        private readonly ITodoMapper _mapper;
        private readonly ITodoValidationService _validationService;
        private readonly ILogger<TodoController> _logger;

        public TodoController(ITodoRepository repository,
            IToDoEmailService emailService,
            ITodoMapper mapper,
            ITodoValidationService validationService,
            ILogger<TodoController> logger)
        {
            _repository = repository;
            _emailService = emailService;
            _mapper = mapper;
            _validationService = validationService;
            _logger = logger;
        }



        /// <summary>
        /// Grazina visus todo irasus
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<GetToDoItemDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAll()
        {
            //var database = new TodoRepository(); //toks budas yra negeras, nes servisams negalima naudoti new
            var data = _repository.GetAll();
            return Ok(_mapper.Map(data));
        }

        /// <summary>
        /// Grazina todo irasa pagal id
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet("{key}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(GetToDoItemDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int key)
        {
            var data = _repository.GetById(key);
            if (data == null)
            {
                return NotFound();
            }

            // return Ok(new GetToDoItemDto(data));
            return Ok(_mapper.Map(data));
        }

        /// <summary>
        /// grazina todo irasus pagal tipa ir contenta
        /// </summary>
        /// <param name="type"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        [HttpGet("filter")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<GetToDoItemDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult FilterBy(string? type, string? content)
        {
            var data = _repository.GetAll();
            if (type != null)
            {
                data = data.Where(t => t.Type == type).ToList();
            }
            if (content != null)
            {
                data = data.Where(t => t.Content.Contains(content)).ToList();
            }


            return Ok(_mapper.Map(data));

        }






        /// <summary>
        /// Iraso nauja todo irasa i duomenu baze ir issiuncia emaila apie tai
        /// </summary>
        /// <param name="dto">todo item oblektas</param>
        /// <returns>grazina naujai sukurto iraso id</returns>
        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post(CreateToDoItemDto dto)
        {
            if (!_validationService.IsValid(dto))
            {
                return BadRequest("Modelis neteisingas");
            }


            TodoItem model = _mapper.Map(dto);
            long newId = _repository.Add(model);
            _emailService.TrySendEmail("kazkam@example.com", model);

            //return CreatedAtAction(nameof(GetById), new { key = newId }, model);
            return Created(nameof(GetById), new { key = newId });
        }













        /// <summary>
        /// atnaujina todo ir isiuncia emaila apie tai
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dto"></param>

        [HttpPut("{key}")]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Put(int key, UpdateToDoItemDto dto)
        {
            TodoItem model = _mapper.Map(dto);

            var data = _repository.GetById(key);
            if (data == null)
            {
                return NotFound();
            }

            _repository.Update(model);
            _emailService.TrySendEmail("kazkam@example.com", model);
            return NoContent();
        }


    }
}



/*
Create a Model:
- Kataloge the Models sukurkite naują  class  Book.
- Book turi turėti properčius: Id (int), CoverType (enum) Title (string), Author (string), PublishYear (int).
- CoverType turi būti enum su reikšmėmis: Hard, Soft, Digital.

Create In-Memory Data:
- Database kataloge sukurkite BooksFakeInMemoryDatabase.
- BooksFakeInMemoryDatabase įdėkite 15 knygų, bent 3 sknygos turi būti to paties autoriaus

Create a DTO:
Kataloge the Dtos:
- sukurkite GetBookDto, kuris bus naudojamas GET metode
GetBookDto turi turėti properčius: Id (int), CoverType (string) Title (string), Author (string), PublishYear (DateTime).
- sukurkite CreateBookDto, kuris bus naudojamas POST metode
CreateBookDto turi turėti properčius: CoverType (string) Title (string), Author (string), PublishYear (DateTime).
- sukurkite UpdateBookDto, kuris bus naudojamas PUT metode
UpdateBookDto turi turėti properčius: Id (int), CoverType (string) Title (string), Author (string), PublishYear (DateTime).


Implement the HTTP Methods:
- Sukurkite GET metodą BooksController kuris grąžins list of books.
- Sukurkite GET metodą BooksController kuris grąžins vieną knygą pagal id.
- Sukurkite POST metodą BooksController kuris įrašys naują knygą.
- Sukurkite PUT metodą BooksController kuris atnaujins knygą.
- Sukurkite DELETE metodą BooksController kuris ištrins knygą pagal id.
- Sukurkite GET metodą BooksController kuris patikrins ar knyga egzistuoja pagal id.
*-Metodams naudokite DTO objektus.

Bonus Challenge (if time allows):
- sukurkite FilterBookRequestDto, kuris bus naudojamas GET metode
FilterBookRequestDto turi turėti properčius: Title (string), Author (string), CoverType (string).
- dinaminė DTO Konfigūracija
Leiskite klientui per query parametrus nurodyti (pvz localhost/books?fields=Id,Title,Author ), kuriuos laukus jis nori gauti atsakyme.
*/
