using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using P07_UnitTestingUzduotis.Dtos;
using P07_UnitTestingUzduotis.Models;
using P07_UnitTestingUzduotis.Repositories;
using P07_UnitTestingUzduotis.Services;
using System.Net.Mime;

namespace P07_UnitTestingUzduotis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostController : ControllerBase
    {
        private readonly BlogPostRepository _repository;
        private readonly IBlogPostMapper _mapper;

        public BlogPostController(BlogPostRepository repository, IBlogPostMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        private BlogPost blogPost = new();


        // GET api/blog
        /// <summary>
        /// grazina visus blog postus
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        [ProducesResponseType(typeof(IEnumerable<BlogPost>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public IActionResult GetAllPosts()
        {
            return BadRequest(_repository.GetAllPosts().ToList());
        }

        // GET api/blog/{id}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns> vieną todo išfiltruotą pagal id </returns>
        /// <response code="401">Naudotojas neprisijungęs, naudokie Authorization metodą</response>
        /// <response code="403">Naudotojas neturi teisės pasiekti įrašo</response>
        /// <response code="404">Įrašas su pateiktu id nerastas</response>
        /// <response code="500">Sistemos klaida, duomenų bazė nepasiekiama</response>
        [HttpGet("one/{id}")]
        [ProducesResponseType(typeof(BlogPost), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public IActionResult GetPostById(int id)
        {
            var blgoPost = _repository.GetPostById(id);
            if (blgoPost == null)
            {
                return NotFound("Irasas db nerastas");
            }
            return Ok(blgoPost); 
        }


        // GET api/blog/add?title={title}&content={content} 
        /// <summary>
        /// prideda nauja blog posta
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <response code="400">Modelio validacijos klaida</response>
        /// <response code="401">Naudotojas neprisijungęs, naudokie Authorization metodą</response>
        /// <response code="500">Sistemos klaida, duomenų bazė nepasiekiama</response>
        [HttpGet("add")]
        [ProducesResponseType(StatusCodes.Status201Created)] // Same as StatusCodes.Status200OK
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        public IActionResult AddPost(string title, string content)
        {
            BlogPost blgoPost = new BlogPost { Title = title, Content = content }; 
            _repository.AddPost(blogPost);
            return Ok(blgoPost);
        }

        // POST api/blog/update/{id}
        /// <summary>
        ///  atnaujina blog posta
        /// </summary>
        /// <param name="id"></param>
        /// <param name="blogPost"></param>
        /// <returns></returns>
        [HttpPost("update/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        public IActionResult UpdatePost(int id, UpdateBlogPost blogPost) 
        {
            _repository.UpdatePost(_mapper.Map(id, blogPost));
            return NoContent(); 
        }

        // DELETE api/blog/delete/{id}
        /// <summary>
        ///  
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete/{id}")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public IActionResult DeletePost(int id)
        {
            var post =  _repository.GetPostById(id);
            if (post == null)
            {
                return NotFound();
            }
            _repository.UpdatePost(post);
           
            return NoContent();
        }
    }
}
