using BCrypt.Net;

public static class PasswordHelper
{
    // Genera un hash seguro usando bcrypt
    public static string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    // Verifica una contrase�a contra un hash almacenado
    public static bool VerifyPassword(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}