using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace SafeVault.db;

public class DB
{
    public DbSet<User> Users { get; set; }    
}

public class User
{
    public int UserID { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; } // Consider using a secure hash instead of plain text
    public string Salt { get; set; } // For password hashing
    public string Hash { get; set; } // For password hashing
}

