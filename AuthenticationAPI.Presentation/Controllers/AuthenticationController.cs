using AuthenticationAPI.Application.DTOs;
using AuthenticationAPI.Application.Interfaces;
using eCommerce.SharedLib.ResponseT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationAPI.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(IUser userInterface) : ControllerBase
    {
        [HttpPost("Register")]
        public async Task<ActionResult<Response>> Register([FromBody] AppUserDTO appUser)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await userInterface.Register(appUser);

            return result.Flag ? Ok(result) : BadRequest(result);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO appUser)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await userInterface.Login(appUser);
            return result.Flag ? Ok(result) : BadRequest(result);
        }

        [HttpGet("GetUser/{userId}")]
        [Authorize]
        public async Task<ActionResult<GetUserDTO>> GetUser(int userId)
        {
            if (userId <= 0)
                return BadRequest("Invalid user ID");
            var user = await userInterface.GetUser(userId);
            if (user is null)
                return NotFound("User not found");
            return Ok(user);

        }
        [HttpGet("GetUserEmail/{email}")]
        [Authorize]
        public async Task<ActionResult<GetUserDTO>> GetUserByEmail(string email)
        {
            
            var user = await userInterface.GetUserByEmail(email);
            if (user is null)
                return NotFound("User not found");
            return Ok(user);

        }

        [Authorize]
        [HttpGet("whoami")]
        public IActionResult WhoAmI()
        {
            return Ok(new
            {
                Name = User.Identity?.Name,
                Roles = User.Claims
                    .Where(c => c.Type.Contains("role"))
                    .Select(c => new { c.Type, c.Value }),
                IsAdmin = User.IsInRole("Admin")
            });
        }
    }
}
