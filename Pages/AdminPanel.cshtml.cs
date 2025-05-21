using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SafeVault.Pages
{
    [Authorize]
    public class AdminPanelModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminPanelModel(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IList<IdentityUser> Users { get; set; }
        public IList<IdentityRole> Roles { get; set; }
        public Dictionary<string, IList<string>> UserRoles { get; set; } = new();

        [BindProperty]
        public Dictionary<string, List<string>> SelectedRoles { get; set; } = new();

        [TempData]
        public string StatusMessage { get; set; }

        public async Task OnGetAsync()
        {
            Users = _userManager.Users.ToList();
            Roles = _roleManager.Roles.ToList();

            foreach (var user in Users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                UserRoles[user.Id] = roles;
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Users = _userManager.Users.ToList();
            Roles = _roleManager.Roles.ToList();
            bool anyChanged = false;

            foreach (var user in Users)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var selected = SelectedRoles.ContainsKey(user.Id) ? SelectedRoles[user.Id] : new List<string>();

                // Roles to add
                var toAdd = Roles.Select(r => r.Name).Where(r => selected.Contains(r) && !userRoles.Contains(r));
                // Roles to remove
                var toRemove = Roles.Select(r => r.Name).Where(r => !selected.Contains(r) && userRoles.Contains(r));

                if (toAdd.Any())
                {
                    var result = await _userManager.AddToRolesAsync(user, toAdd);
                    if (!result.Succeeded)
                    {
                        StatusMessage = $"Error adding roles to {user.Email}: {string.Join(", ", result.Errors.Select(e => e.Description))}";
                        return RedirectToPage();
                    }
                    anyChanged = true;
                }

                if (toRemove.Any())
                {
                    var result = await _userManager.RemoveFromRolesAsync(user, toRemove);
                    if (!result.Succeeded)
                    {
                        StatusMessage = $"Error removing roles from {user.Email}: {string.Join(", ", result.Errors.Select(e => e.Description))}";
                        return RedirectToPage();
                    }
                    anyChanged = true;
                }
            }

            StatusMessage = anyChanged ? "Roles updated successfully." : "No changes made.";
            return RedirectToPage();
        }
    }
}