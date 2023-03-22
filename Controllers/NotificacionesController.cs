using Microsoft.AspNetCore.Mvc;
using SendGrid;
using SendGrid.Helpers.Mail;
using ms_notificaciones.Models;
namespace ms_notificaciones.Controllers;

[ApiController]
[Route("[controller]")]
public class NotificacionesController : ControllerBase
{
    [Route("correo")]
    [HttpPost]
    public async Task<ActionResult> EnviarCorreo(ModeloCorreo datos)
    {
        //var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
        var apiKey = "";
        var client = new SendGridClient(apiKey);
        var from = new EmailAddress("jeferson.arango@ucaldas.edu.co", "Jeferson Arango L");
        var subject = datos.asuntoCorreo;
        var to = new EmailAddress(datos.correoDestino, datos.nombreDestino);
        var plainTextContent = "plain text content";
        var htmlContent = datos.contenidoCorreo;
        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        var response = await client.SendEmailAsync(msg);
        if(response.StatusCode == System.Net.HttpStatusCode.Accepted){
            return Ok("Correo enviado a la dirección " + datos.correoDestino);
        }else{
            return BadRequest("Error enviando el mensaje a la dirección: " + datos.correoDestino);
        }
    }
}
