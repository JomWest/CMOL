using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Aplicacion;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Persistencia;
using Dominio;
using Microsoft.EntityFrameworkCore;
namespace WebAPI.Controllers
{
    [Route("ControladorReportProductos2")]
    public class ControladorReportProductos2 : Controller
    {
    public readonly IMediator _mediator;

    public ControladorReportProductos2(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet]
    public async Task<ActionResult<Stream>> GetTask(){
        return await _mediator.Send(new ExportarPDFProducto.Consulta());
    }
     
    }
    }
