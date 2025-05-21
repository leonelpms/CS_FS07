using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

public class CustomRegisterModel : PageModel
{
    [BindProperty]
    public RegisterInput Input { get; set; }

    public class RegisterInput
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public IActionResult OnGet() => Page();

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        // Hashea la contraseña antes de guardar
        string hashedPassword = PasswordHelper.HashPassword(Input.Password);

        // Guarda Email y hashedPassword en la base de datos (ejemplo omitido)
        // await _dbContext.Users.AddAsync(new User { Email = Input.Email, PasswordHash = hashedPassword });
        // await _dbContext.SaveChangesAsync();

        return RedirectToPage("Login");
    }
}