using Microsoft.AspNetCore.Mvc;
using SendGrid;
using SendGrid.Helpers.Mail;
using ms_notificaciones.Models;

namespace ms_notificaciones.Controllers;

[ApiController]
[Route("[controller]")]
public class NotificacionController : ControllerBase
{
    [Route("correo")]
    [HttpPost]
    public async Task<IActionResult> EnviarCorreo(CorreoModel correo)
    {
        Console.WriteLine("Hi from post method");
        var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
        var client = new SendGridClient(apiKey);
        var from = new EmailAddress("jeferson.arango@ucaldas.edu.co", "Jeferson Arango López");
        var subject = correo.asuntoCorreo;
        var to = new EmailAddress(correo.correoDestino, correo.nombreDestino);
        var htmlContent = correo.contenidoCorreo;
        var msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlContent);
        var response = await client.SendEmailAsync(msg);
        if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
        {
            return Ok("El correo se ha enviado correctamente a " + correo.nombreDestino + " en la dirección: " + correo.correoDestino);
        }
        else
        {
            return BadRequest("Error enviando el correo a " + correo.nombreDestino + " en la dirección: " + correo.correoDestino);
        }
    }

}
