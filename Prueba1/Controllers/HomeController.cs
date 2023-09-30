using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Prueba1.Models;
using System.Diagnostics;
using System.Net.Mail;
using System.Net;

namespace Prueba1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration )
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Formulario()
        {
            return View();
        }

        public IActionResult about()
        {
            return View();
        }

        public IActionResult courses()
        {
            return View();
        }

        public IActionResult Exito()
        {
            return View();
        }

        /* Areas */

        public IActionResult AreaAgil()
        {
            return View();
        }

        public IActionResult AreaData()
        {
            return View();
        }
        public IActionResult AreaCiber()
        {
            return View();
        }
        public IActionResult AreaGestion()
        {
            return View();
        }

        /* Fin Areas */

        /*Cursos*/

        public IActionResult CUBPM()
        {
            return View();
        }

        public IActionResult CUBusinessSQL()
        {
            return View();
        }

        public IActionResult CUCiberSecurity()
        {
            return View();
        }
        public IActionResult CUDashboardPBI()
        {
            return View();
        }
        public IActionResult CUDataScience()
        {
            return View();
        }
        public IActionResult CUDatosR()
        {
            return View();
        }
        public IActionResult CUISO56002()
        {
            return View();
        }
        public IActionResult CUOKR()
        {
            return View();
        }
        public IActionResult CUPython()
        {
            return View();
        }
        public IActionResult CUScrum()
        {
            return View();
        }
        public IActionResult CUStartup()
        {
            return View();
        }

        /* Fin Cursos */

        [HttpPost]
        public ActionResult EnviarCorreo(IFormCollection form)
        {
            try
            {
                if (string.IsNullOrEmpty(form["apellidoPaterno"]) || string.IsNullOrEmpty(form["apellidoMaterno"]) || string.IsNullOrEmpty(form["nombres"]) || string.IsNullOrEmpty(form["tipoDocumento"]) || string.IsNullOrEmpty(form["numeroDocumento"]) || string.IsNullOrEmpty(form["celular"]) || string.IsNullOrEmpty(form["correoElectronico"]) || string.IsNullOrEmpty(form["correoElectronico"]) || string.IsNullOrEmpty(form["tipodeCurso"]) && !bool.Parse(form["consentimiento"]))
                {
                    // Si la validación falla, puedes redirigir al usuario a una página de error o volver a mostrar el formulario con mensajes de error.
                    ModelState.AddModelError(string.Empty, "Por favor, complete todos los campos y acepte el consentimiento.");
                    return View("Index"); // Reemplaza "NombreDeTuVista" con el nombre de tu vista
                }

                // Recuperar los datos del formulario
                string apellidoPaterno = form["apellidoPaterno"];
                string apellidoMaterno = form["apellidoMaterno"];
                string nombres = form["nombres"];
                string tipoDocumento = form["tipoDocumento"];
                string numeroDocumento = form["numeroDocumento"];
                string celular = form["celular"];
                string correoElectronico = form["correoElectronico"];
                string Curso = form["tipodeCurso"];

                // Configurar el mensaje de correo
                var mensaje = new MailMessage
                {
                    From = new MailAddress(_configuration["EmailSettings:Sender"]),
                    Subject = "Solicitud de información",
                    Body = $"Apellido Paterno: {apellidoPaterno}\n" +
                           $"Apellido Materno: {apellidoMaterno}\n" +
                           $"Nombres: {nombres}\n" +
                           $"Tipo de Documento: {tipoDocumento}\n" +
                           $"Número de Documento: {numeroDocumento}\n" +
                           $"Celular: {celular}\n" +
                           $"Correo Electrónico: {correoElectronico}\n" +
                           $"Curso: {Curso}"

                };

                mensaje.To.Add("ronaldo@genesiscenterit.com");

                // Configurar el cliente SMTP
                var smtpCliente = new SmtpClient
                {
                    Host = _configuration["EmailSettings:MailServer"], // o el host de tu servidor SMTP
                    Port = _configuration.GetValue<int>("EmailSettings:MailPort"),
                    Credentials = new NetworkCredential(_configuration["EmailSettings:Sender"], _configuration["EmailSettings:Password"]),
                    EnableSsl = true
                };

                // Enviar el correo electrónico
                smtpCliente.Send(mensaje);

                // Redireccionar a una página de éxito
                return RedirectToAction("Exito");
            }
            catch (Exception ex)
            {
                // Manejar cualquier error y redireccionar a una página de error
                ViewBag.ErrorMessage = ex.Message;
                return View("Error");
            }
        }





        public IActionResult Privacy()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}