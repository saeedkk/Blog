using System;
using System.Linq;
using Numito_Blog.CoreLayer.DTOs.Users;
using Numito_Blog.CoreLayer.Utilities;
using Numito_Blog.DataLayer.Context;
using Numito_Blog.DataLayer.Entities;

namespace Numito_Blog.CoreLayer.Services.Users
{
    public class UserService : IUserService
    {
        private readonly BlogContext _context;

        public UserService(BlogContext context)
        {
            _context = context;
        }
        public OperationResult RegisterUser(UserRegisterDto registerDto)
        {
            var isUserNameExist = _context.Users.Any(u => u.UserName == registerDto.UserName);
            if (isUserNameExist)
                return OperationResult.Error("نام کاربری تکراری است.");

                var HashPassword = registerDto.Password.EncodeToMd5();
                _context.Users.Add(new User()
                {
                    FullName = registerDto.FullName,
                    UserName = registerDto.UserName,
                    Password = HashPassword,
                    IsDelete = false,
                    Role = UserRole.User,
                    CreatedDate = DateTime.Now
                });

                _context.SaveChanges();
                return OperationResult.Success();
            
        }

        public UserDto LoginUser(UserLoginDto loginDto)
        {
            var HashPassword = loginDto.Password.EncodeToMd5();
            var user = _context.Users.FirstOrDefault(u => u.UserName == loginDto.UserName && u.Password == HashPassword);

            if (user == null) 
                return null;

            var userDto = new UserDto()
            {
                FullName = user.FullName,
                Password = user.Password,
                Role = user.Role,
                UserName = user.UserName,
                RegisterDate = user.CreatedDate,
                UserId = user.Id
            };
            return userDto;
        }
    }
}