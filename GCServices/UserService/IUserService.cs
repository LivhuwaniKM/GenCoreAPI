using GCDomain.Models.ServiceResponse;
using GCDomain.Models.User;

namespace GCServices.UserService
{
    public interface IUserService
    {
        Task<ServiceResponseDto<UserDto>> LoginAsync(LoginDto model);
        Task<ServiceResponse<UserDto>> RegisterAsync(User model);
        Task<ServiceResponse<IEnumerable<UserDto>>> GetUsersListAsync();
        Task<ServiceResponse<UserDto>> SearchUserAsync(SearchUserModelDto model);
        Task<ServiceResponse<UserDto>> UpdateUserAsync(int id, UserDto model);
        Task<ServiceResponse<string>> ResetPasswordAsync(int id, ResetPasswordDto model);
        Task<ServiceResponse<string>> DeleteUserAsync(int id);
    }
}
