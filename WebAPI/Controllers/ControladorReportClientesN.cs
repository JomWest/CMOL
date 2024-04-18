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
    [Route("ControladorReportClientesN")]
    public class ControladorReportClientesN : Controller
    {
    public readonly IMediator _mediator;

    public ControladorReportClientesN(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet]
    public async Task<ActionResult<Stream>> GetTask(){
        return await _mediator.Send(new ExportarPDFClientesN.Consulta());
    }
     
    }
    }
