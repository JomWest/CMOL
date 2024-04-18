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
    public class ExportarPDFCompras
    {
        public class Prueba: IRequest<List<VistaCompra>>{}

        public class Manejador: IRequestHandler<Prueba,List<VistaCompra>>{

            //dbContext
            public readonly CellMasterDbContext context;

            public Manejador(CellMasterDbContext _context)
            {
                this.context = _context;
            }

            public async Task<List<VistaCompra>> Handle(Prueba request , CancellationToken cancellationToken){
                var invoice = await context.VistaCompras.ToListAsync();
                return invoice;
            }
    }
    }
}