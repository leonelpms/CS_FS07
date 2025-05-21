using Microsoft.AspNetCore.Identity; // Ensure this is included
using Microsoft.EntityFrameworkCore; // Ensure this is included
using SafeVault.db;

var builder = WebApplication.CreateBuilder(args);

// Configura la cadena de conexi�n (ajusta seg�n tu entorno)
var connectionString = builder.Configuration.GetConnectionString("DemoDb")
    ?? "Server=(localdb)\\mssqllocaldb;Database=demo;Trusted_Connection=True;MultipleActiveResultSets=true";

// Agrega DemoDbContext al pipeline de servicios
builder.Services.AddDbContext<DemoDbContext>(options =>
    options.UseSqlServer(connectionString)); // Ensure the SQL Server provider is installed

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<DemoDbContext>();

builder.Services.AddMvc();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = "/AccessDenied";
});

/*
// Agrega Identity con roles
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
})
.AddEntityFrameworkStores<DemoDbContext>();
*/
builder.Services.AddRazorPages();

var app = builder.Build();

/*
// SEED: Crear roles y usuario administrador al iniciar
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    string[] roles = { "Admin", "User" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));
    }

    // Crear usuario admin si no existe
    var adminEmail = "admin@demo.com";
    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser == null)
    {
        adminUser = new IdentityUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
        await userManager.CreateAsync(adminUser, "Admin123!");
        await userManager.AddToRoleAsync(adminUser, "Admin");
    }
} */

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication(); // Importante: agrega autenticaci�n
app.UseAuthorization();

app.MapRazorPages();

app.Run();
