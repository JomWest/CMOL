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

namespace WebAPI.Controllers
{
    [Route("ReporteFacturaController")]
    public class ReporteFacturaController : Controller
    {
        private readonly IMediator _mediator;
         public ReporteFacturaController(IMediator mediator)
        {
            _mediator = mediator;
        }


      [HttpGet]
        public async Task<JsonResult> Get()
        {
            var invoices = await _mediator.Send(new ExportarPDFFactura.Prueba());
            //return Json(invoices);
            var data = invoices.Select(invoice => new object[]
            {
                invoice.Idfactura,
                invoice.IdUsuario,
                invoice.CedulaC,
                invoice.NombreCliente,
                invoice.NombreProducto,
                invoice.Precio,
                invoice.CantidadProductos,
                invoice.TotalFactura,
                // Agrega los demás campos necesarios aquí
               // invoice.OrderTotal
            }).ToList();

            return Json(new
            {
                draw = 1, // Puedes ajustar este valor según tus necesidades
                //recordsTotal = data.Count,
                //recordsFiltered = data.Count,
                data = invoices
            });
        }
    }
}