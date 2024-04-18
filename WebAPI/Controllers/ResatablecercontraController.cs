using Dominio;
using iTextSharp.text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("Email")]
    public class EmailController : Controller
    {
        private readonly CellMasterDbContext _DbContext;

        public EmailController(CellMasterDbContext dbContext)
        {
            _DbContext = dbContext;
        }

        [HttpPost("EnviarContraseña")]
        public async Task<IActionResult> EnviarContraseña([FromBody] CorreoModel correoModel)
        {
            try
            {
                // Obtener el usuario por el correo electrónico
                var usuario = _DbContext.Usuarios.FirstOrDefault(u => u.Email == correoModel.Destinatario);

                if (usuario == null)
                {
                    return BadRequest("Usuario no encontrado");
                }

                string contraseña = "ownfwczbtpkvfxvp";

                string destinatario = correoModel.Destinatario;
                string remitente = "jomarmtz639@gmail.com";
                string asunto = "Nueva Contraseña CellMaster";
                string cuerpoMensaje = "Su nueva contraseña es " + correoModel.NuevaContraseña;

                using (MailMessage ms = new MailMessage(remitente, destinatario, asunto, cuerpoMensaje))
                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.EnableSsl = true;
                    smtp.Credentials = new NetworkCredential(remitente, contraseña);

                    await Task.Run(() =>
                    {
                        System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        smtp.Send(ms);
                        ms.Dispose();
                    });

                    usuario.Contraseña = correoModel.NuevaContraseña;
                    _DbContext.SaveChanges();

                    return Ok("Correo enviado, revise su bandeja de entrada");
                }
            }
            catch (Exception error)
            {
                return BadRequest("Error al enviar el correo electrónico " + error.Message);
            }
        }
    }
    }
