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
    [Route("ReporteComprasController")]
    public class ReporteComprasController : Controller
    {
        private readonly IMediator _mediator;
         public ReporteComprasController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
      [HttpGet]
        public async Task<JsonResult> Get()
        {
            var invoices = await _mediator.Send(new ExportarPDFCompras.Prueba());
            //return Json(invoices);
            var data = invoices.Select(invoice => new object[]
            {
                invoice.Idcompra,
                invoice.Fecha,
                invoice.IdUsuario,
                invoice.NombreU,
                invoice.Proveedor,
                invoice.Producto,
                invoice.Cantidad,
                invoice.Precio,
                invoice.Total,
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