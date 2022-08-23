using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Numito_Blog.CoreLayer.DTOs.Users;
using Numito_Blog.CoreLayer.Services.Users;
using Numito_Blog.CoreLayer.Utilities;

namespace Numito_Blog.Web.Pages.Auth
{
    [BindProperties]
    [ValidateAntiForgeryToken]
    public class LoginModel : PageModel
    {
        private readonly IUserService _userService;

        public LoginModel(IUserService userService)
        {
            _userService = userService;
        }

        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "{0} را وارد نمایید.")]
        public string UserName { get; set; }
        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "{0} را وارد نمایید.")]
        [MinLength(6, ErrorMessage = "{0} باید بیشتر از 5 کاراکتر باشد.")]
        public string Password { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = _userService.LoginUser(new UserLoginDto()
            {
                Password = Password,
                UserName = UserName
            });
            if (user == null)
            {
                ModelState.AddModelError("UserName","کاربری با مشخصات وارد شده یافت نشد.");
                return Page();
            }

            List<Claim> claims = new List<Claim>()
            {
                new Claim("Test","test"),
                new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                new Claim(ClaimTypes.Name,user.FullName)
            };
            var identity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
            var claimPrincipal = new ClaimsPrincipal(identity);
            var properties = new AuthenticationProperties()
            {
                IsPersistent = true,
            };
            HttpContext.SignInAsync(claimPrincipal, properties);
            return RedirectToPage("../Index");
        }
    }
}
