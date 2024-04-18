using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rectangle = iTextSharp.text.Rectangle;
using Dominio;
using Persistencia;


namespace Aplicacion
{
    public class ExportarPDFFactura
    {
        public class Prueba: IRequest<List<VistaFactura>>{}

        public class Manejador: IRequestHandler<Prueba,List<VistaFactura>>{

            //dbContext
            public readonly CellMasterDbContext context;

            public Manejador(CellMasterDbContext _context)
            {
                this.context = _context;
            }

            public async Task<List<VistaFactura>> Handle(Prueba request , CancellationToken cancellationToken){
                var invoice = await context.VistaFacturas.ToListAsync();
                return invoice;
            }
    }
    }
}