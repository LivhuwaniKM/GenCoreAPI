using GCDomain.Helpers;
using GCDomain.Models.ServiceResponse;
using GCDomain.Models.User;
using GCServices.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GenCoreAPI.Controllers
{
    public class UserController(IUserService _userService, IResponseHelper _response) : BaseApiController
    {
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<UserDto>>> RegisterAsync(User model)
        {
            if (!ModelState.IsValid)
                return _response.CreateResponse<UserDto>(false, 400, "Invalid request", null);

            var response = await _userService.RegisterAsync(model);

            return StatusCode(response.StatusCode, response);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponseDto<UserDto>>> LoginAsync(LoginDto model)
        {
            if (!ModelState.IsValid)
                return _response.CreateResponseWithToken<UserDto>(false, 400, "Invalid request", null, null!);

            var response = await _userService.LoginAsync(model);

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("list")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<UserDto>>>> GetUsersListAsync()
        {
            var response = await _userService.GetUsersListAsync();

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("search")]
        public async Task<ActionResult<ServiceResponse<UserDto>>> SearchUserAsync(SearchUserModelDto model)
        {
            var response = await _userService.SearchUserAsync(model);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult<ServiceResponse<UserDto>>> UpdateUserAsync(int id, [FromBody] UserDto model)
        {
            var response = await _userService.UpdateUserAsync(id, model);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("reset-password/{id}")]
        public async Task<ActionResult<ServiceResponse<UserDto>>> ResetPasswordAsync(int id, [FromBody] ResetPasswordDto model)
        {
            if (!ModelState.IsValid)
                return _response.CreateResponse<UserDto>(false, 400, "Invalid request", null);

            var response = await _userService.ResetPasswordAsync(id, model);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("delete/{id}")]
        public async Task<ActionResult<ServiceResponse<string>>> DeleteUserAsync(int id)
        {
            var response = await _userService.DeleteUserAsync(id);

            return StatusCode(response.StatusCode, response);
        }
    }
}