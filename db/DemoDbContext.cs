using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SafeVault.db
{
    public class DemoDbContext : IdentityDbContext // Cambia la herencia aquí
    {
        public DemoDbContext(DbContextOptions<DemoDbContext> options)
            : base(options)
        {
        }

        // Use the 'new' keyword to hide the inherited 'Users' property
       // public new DbSet<User> Users { get; set; }
    }
}