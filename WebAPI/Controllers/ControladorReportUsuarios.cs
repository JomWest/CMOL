using System.IO;
using System.Threading.Tasks;
using Aplicacion;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ControladorReportUsuarios : Controller
    {
        private readonly IMediator _mediator;

        public ControladorReportUsuarios(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var pdfStream = await _mediator.Send(new ExportarPDFUsuario.Consulta());
            return File(pdfStream, "application/pdf", "reporte_usuarios.pdf");
        }
    }
}
