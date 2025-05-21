using NUnit.Framework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace SafeVault.Tests
{
    [TestFixture]
    public class TestInputValidation
    {
        [Test]
        public void TestForSQLInjection()
        {
            // Simula un input malicioso t�pico de inyecci�n SQL
            var maliciousInput = new UserInputModel
            {
                Username = "admin'; DROP TABLE Users; --",
                Email = "attacker@example.com"
            };

            // Forzar la validaci�n del modelo
            var validationContext = new ValidationContext(maliciousInput);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(maliciousInput, validationContext, validationResults, true);

            // El modelo debe ser v�lido para esta prueba sint�tica
            Assert.That(isValid, Is.True, "El modelo debe ser v�lido para esta prueba sint�tica.");

            // Sanitizar la entrada
            maliciousInput.Sanitize();

            // Verifica que los caracteres peligrosos hayan sido eliminados
            Assert.That(maliciousInput.Username.Contains("'"), Is.False, "El nombre de usuario no debe contener comillas simples.");
            Assert.That(maliciousInput.Username.ToLower().Contains("drop table"), Is.False, "El nombre de usuario no debe contener comandos SQL.");
        }

        [Test]
        public void TestForXSS()
        {
            // Simula un input malicioso t�pico de XSS
            var maliciousInput = new UserInputModel
            {
                Username = "<script>alert('XSS');</script>",
                Email = "victim@example.com"
            };

            // Forzar la validaci�n del modelo
            var validationContext = new ValidationContext(maliciousInput);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(maliciousInput, validationContext, validationResults, true);

            // El modelo debe ser v�lido para esta prueba sint�tica
            Assert.That(isValid, Is.True, "El modelo debe ser v�lido para esta prueba sint�tica.");

            // Sanitizar la entrada
            maliciousInput.Sanitize();

            // Verifica que las etiquetas de script hayan sido eliminadas
            Assert.That(maliciousInput.Username.Contains("<script>"), Is.False, "El nombre de usuario no debe contener etiquetas <script>.");
            Assert.That(maliciousInput.Username.Contains("</script>"), Is.False, "El nombre de usuario no debe contener etiquetas </script>.");

            // Simula el renderizado en Razor (que codifica autom�ticamente la salida)
            string renderedOutput = System.Net.WebUtility.HtmlEncode(maliciousInput.Username);

            // Verifica que la salida est� codificada
            Assert.That(renderedOutput.Contains("<script>"), Is.False, "La salida no debe contener etiquetas <script> sin codificar.");
            Assert.That(renderedOutput.Contains("&lt;script&gt;"), Is.False, "La salida no debe contener etiquetas codificadas si fueron eliminadas por Sanitize.");
        }
    }
}