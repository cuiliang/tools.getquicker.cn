using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace QuickerWebTools.Pages.Browser
{
    public class UserAgentModel : PageModel
    {
        public string UserAgent { get; set; }
        public void OnGet()
        {
            UserAgent = Request.Headers["User-Agent"];
        }
    }
}
