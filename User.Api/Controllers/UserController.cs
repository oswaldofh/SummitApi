using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using User.Api.Core;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace User.Api.Controllers
{
    [Route("user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly GetUserBusiness _getUser;
        private readonly SaveUserBusiness _saveUser;

        public UserController(GetUserBusiness getUser, SaveUserBusiness saveUser)
        {
            _getUser = getUser;
            _saveUser = saveUser;
        }
        [HttpGet]
        public async Task<IActionResult> GetUser(string? userName)
        {
            try
            {
                var user = _getUser.Process(userName);
                return Ok(user);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPost("SaveUser")]
        public async Task<IActionResult> SaveUser()
        {
            try
            {
                var user = await _saveUser.Process();
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("ValidateUser")]
        public async Task<IActionResult> ValidateUser(string userName, string password)
        {
            try
            {
                var data = _getUser.Process(userName);
                var user = JsonSerializer.Deserialize<UserDto>(data);
                if (user is null)
                    return NotFound("No existe un usuario con esas credenciales...");

                var validate = PasswordHash.Validate(user.UserName, password, user.Password);

                if (validate.ToString() == "Rejected")
                {
                    return StatusCode(403, new { Message = "Las credenciales de ingreso no son correctos..." });
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

       
    }
}
