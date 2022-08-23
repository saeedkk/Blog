using Numito_Blog.CoreLayer.DTOs.Users;
using Numito_Blog.CoreLayer.Utilities;

namespace Numito_Blog.CoreLayer.Services.Users
{
    public interface IUserService
    {
        OperationResult RegisterUser(UserRegisterDto registerDto);
        UserDto LoginUser(UserLoginDto loginDto);
    }
}