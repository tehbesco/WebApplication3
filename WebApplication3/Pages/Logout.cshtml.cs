using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Session;

namespace WebApplication3.Pages
{
    public class LogoutModel : PageModel
    {
		private readonly SignInManager<IdentityUser> signInManager;
        private readonly IHttpContextAccessor contxt;
        public LogoutModel(SignInManager<IdentityUser> signInManager, IHttpContextAccessor httpContextAccessor)
		{
			this.signInManager = signInManager;
			contxt = httpContextAccessor;
		}
		public void OnGet() { }
		public async Task<IActionResult> OnPostLogoutAsync()
		{
			await signInManager.SignOutAsync();
			contxt.HttpContext.Session.Clear();
			return RedirectToPage("Login");
		}
		public async Task<IActionResult> OnPostDontLogoutAsync()
		{
			return RedirectToPage("Index");
		}
	}
}
