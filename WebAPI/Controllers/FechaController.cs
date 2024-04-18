using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Aplicacion;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApi.Controllers
{
    [Route("FechaController")]
    public class FechaController : Controller
    {
        private readonly IMediator _mediator;

        // Constructor que recibe una instancia de IMediator mediante inyección de dependencias
        public FechaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Método para manejar la solicitud GET
        public async Task<JsonResult> Get()
        {
            try
            {
                // Utiliza el mediador para enviar una consulta y obtener datos de facturas
                var invoices = await _mediator.Send(new Datos.Consulta());

                // Transforma los datos de facturas en un formato específico
                var data = invoices.Select(invoice => new object[]
                {
                    invoice.Idfactura,
                    invoice.NombreTrabajador,
                    invoice.NombreProducto,
                    invoice.Fecha,
                    invoice.TotalFactura
                }).ToList();

                // Devuelve los datos transformados como una respuesta JSON
                return Json(new
                {
                    draw = 1, // Puedes ajustar este valor según tus necesidades
                    recordsTotal = data.Count,
                    recordsFiltered = data.Count,
                    data = invoices
                });
            }
            catch (Exception ex)
            {
                // Maneja las excepciones y registra información de depuración si es necesario
                Debug.WriteLine($"Error en FechaController: {ex.Message}");
                return Json(new { error = "Ocurrió un error al procesar la solicitud." });
            }
        }
    }
}
