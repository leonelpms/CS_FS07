using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public class UserInputModel
{
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Username { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    /// <summary>
    /// Sanitizes the Username and Email fields to remove potentially dangerous characters and scripts.
    /// </summary>
    public void Sanitize()
    {
        // Remove script tags and angle brackets from Username
        Username = string.IsNullOrEmpty(Username)
            ? Username
            : Regex.Replace(Username, @"<.*?>", string.Empty); // Remove HTML tags

        Username = Regex.Replace(Username, @"[^\w\s@.-]", string.Empty); // Allow only safe characters

        // For Email, allow only valid email characters
        Email = string.IsNullOrEmpty(Email)
            ? Email
            : Regex.Replace(Email, @"[^\w@\.\-+]", string.Empty);
    }
}   