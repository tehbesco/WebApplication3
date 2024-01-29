using AspNetCore.ReCaptcha;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication3.ViewModels;
using IdentityUser = Microsoft.AspNetCore.Identity.IdentityUser;

namespace WebApplication3.Pages
{

	public class LoginModel : PageModel
    {
		[BindProperty]
		public Login LModel { get; set; }

		private readonly SignInManager<IdentityUser> signInManager;
        private readonly IHttpContextAccessor contxt;

        public LoginModel(SignInManager<IdentityUser> signInManager, IHttpContextAccessor httpContextAccessor)
		{
			this.signInManager = signInManager;
			contxt = httpContextAccessor;
		}
		public void OnGet()
        {
        }
		[ValidateReCaptcha]
		public async Task<IActionResult> OnPostAsync()
		{
			if (ModelState.IsValid)
			{
				var identityResult = await signInManager.PasswordSignInAsync(LModel.Email, LModel.Password, LModel.RememberMe, false);
				if (identityResult.Succeeded)
				{
					contxt.HttpContext.Session.SetString("LastAccessTime", DateTime.Now.ToString());
                    contxt.HttpContext.Session.SetString("ID",Guid.NewGuid().ToString());
					return RedirectToPage("Index");
				}
				ModelState.AddModelError("", "Username or Password incorrect");
			}
			return Page();
		}
	}
}
