using Microsoft.AspNetCore.Mvc;
using UnitTestPracticeApplication.DTOs;
using UnitTestPracticeApplication.Services;

namespace UnitTestPracticeApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;

        public UserController(IUserService userService, IJwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        [HttpPost("Login")]
        public ActionResult<ResponseDto> Login([FromBody] UserDto request)
        {
            var response = _userService.Login(request.Username, request.Password);
            if (!response.IsSuccess)
            {
                return BadRequest(response.Message);
            }

            var jwtToken = _jwtService.GetJwtToken(request.Username);

            return new LoginResponseDto(true, response.Message, jwtToken);
        }

        [HttpPost("Signup")]
        public ActionResult<ResponseDto> Signup([FromBody] UserDto request)
        {
            var response = _userService.Signup(request.Username, request.Password);
            if (!response.IsSuccess)
            {
                return BadRequest(response.Message);
            }
            return response;
        }
    }
}
