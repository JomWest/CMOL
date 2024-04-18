using System;
using System.Collections.Generic;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aplicacion;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

/*using Persistencia;
using Dominio;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    [Route("ControladorReportProveedores")]
    public class ControladorReportProveedores : Controller
    {
    public readonly IMediator _mediator;

    public ControladorReportProveedores(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet]
    public async Task<ActionResult<Stream>> GetTask(){
        return await _mediator.Send(new ExportarPDFProveedores.Consulta());
    }
    }
}*/