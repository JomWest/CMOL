using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Aplicacion;
using MediatR;
using Aplicacion.Filtro;

namespace WebAPI.Controllers
{
    [Route("CompraIDController")]
    public class CompraIDController : Controller
    {
        private readonly IMediator _mediator;

        // Constructor que recibe una instancia de IMediator mediante inyección de dependencias
        public CompraIDController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Método para manejar la solicitud GET con un parámetro 'cursoid'
        [HttpGet("{cursoid}")]
        public async Task<ActionResult<Stream>> Compras(int cursoid)
        {
            try
            {
                // Utiliza el mediador para enviar una solicitud de filtrado por ID de compra
                return await _mediator.Send(new FiltrarIDCompras.Cajas { CursoId = cursoid });
            }
            catch (Exception ex)
            {
                // Maneja las excepciones y registra información de depuración si es necesario
                Debug.WriteLine($"Error en CompraIDController: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}
