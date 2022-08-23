using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Numito_Blog.CoreLayer.DTOs.Users;
using Numito_Blog.CoreLayer.Services.Users;
using Numito_Blog.CoreLayer.Utilities;

namespace Numito_Blog.Web.Pages.Auth
{
    [BindProperties]
    [ValidateAntiForgeryToken]
    public class RegisterModel : PageModel
    {
        private readonly IUserService _userService;

        #region Properties
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "{0} را وارد نمایید.")]
        public string UserName { get; set; }
        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "{0} را وارد نمایید.")]
        [MinLength(6,ErrorMessage = "{0} باید بیشتر از 5 کاراکتر باشد.")]
        public string Password { get; set; }
        [Display(Name = "نام و نام خانوادگی")]
        [Required(ErrorMessage = "{0} را وارد نمایید.")]
        public string FullName { get; set; }

        #endregion

        public RegisterModel(IUserService userService)
        {
            _userService = userService;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = _userService.RegisterUser(new UserRegisterDto()
            {
                UserName = UserName,
                FullName = FullName,
                Password = Password
            });
            if (result.Status == OperationResultStatus.Error)
            {
                ModelState.AddModelError("UserName",result.Message);
                return Page();
            }
            return RedirectToPage("Login");
        }
    }
}
