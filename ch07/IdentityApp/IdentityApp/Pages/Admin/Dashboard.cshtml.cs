using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace IdentityApp.Pages.Admin
{
    public class DashboardModel : PageModel
    {
        public UserManager<IdentityUser> UserManager { get; set; }

        public DashboardModel (UserManager<IdentityUser> userMgr)
            => UserManager = userMgr ;

        public int UsersCount { get; set; } = 0;
        public int UsersUnconfirmed { get; set; } = 0;
        public int UsersLockedout { get; set; } = 0;
        public int UsersTwoFactor { get; set; } = 0;

        private readonly string[] emails =
        {
            "alice@example.com", "bob@example.com", "charlie@example.com"
        };

        public async Task<IActionResult> OnPostAsync()
        {
            foreach (string email in emails)
            {
                IdentityUser userObject = new IdentityUser
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true
                };

                await UserManager.CreateAsync(userObject);
            }

            return RedirectToPage();
        }
    }
}
