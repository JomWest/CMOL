using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Dominio;
using Persistencia;
using Aplicacion;
using MediatR;
using Aplicacion.Filtro;


namespace WebAPI.Controllers
{
    [Route("InventarioIDController")]
    public class InventarioIDController : Controller
    {
        private readonly IMediator _mediator;

        // Constructor que recibe una instancia de IMediator mediante inyección de dependencias
        public InventarioIDController(IMediator mediator)
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
                return await _mediator.Send(new FiltrarIDInventario.Cajas { CursoId = cursoid });
            }
            catch (Exception ex)
            {
                // Maneja las excepciones y registra información de depuración si es necesario
                Debug.WriteLine($"Error en ClienteIDController: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}