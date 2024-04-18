using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Aplicacion;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Controllers
{
    [Route("UsuarioFiltradoController")]
    public class UsuarioFiltradoController : Controller
    {
        private readonly IMediator _mediator;

        public UsuarioFiltradoController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("{Id}")]
        public async Task<ActionResult<Stream>> factura(int Id){
            return await _mediator.Send(new ImprimirFactura.Consulta{IdUser = Id} );
        }
    }
}