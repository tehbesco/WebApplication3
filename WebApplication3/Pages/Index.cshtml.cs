using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;

namespace WebApplication3.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IHttpContextAccessor contxt;

        public IndexModel(ILogger<IndexModel> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            contxt = httpContextAccessor;
        }

        public void OnGet()
        {
            // Check if the session has expired
            if (contxt.HttpContext.Session.IsAvailable)
            {
                var lastAccess = contxt.HttpContext.Session.GetString("LastAccessTime");

                if (!string.IsNullOrEmpty(lastAccess))
                {
                    var lastAccessTime = DateTime.Parse(lastAccess);
                    var currentTime = DateTime.Now;

                    var sessionTimeout = TimeSpan.FromSeconds(10);

                    if ((currentTime - lastAccessTime) > sessionTimeout)
                    {
                        // Session has expired
                        // Perform your desired action here, such as redirecting to a login page
                        contxt.HttpContext.Session.Clear();
                        Response.Redirect("/Login");
                    }
                    else
                    {
                        contxt.HttpContext.Session.SetString("LastAccessTime", DateTime.Now.ToString());
                    }
                }
            }
            else
            {
                contxt.HttpContext.Session.Clear();
                Response.Redirect("/Login");
            }
        }
    }
}
