using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using iTextSharp.text;
using Rectangle = iTextSharp.text.Rectangle;
using Dominio;
using Persistencia;

namespace Aplicacion
{
    public class ExportarPDFInventario
    {
        public class Prueba : IRequest<List<VistaInventario>> { }

        public class Manejador : IRequestHandler<Prueba, List<VistaInventario>>
        {
            // DbContext
            private readonly CellMasterDbContext context;

            public Manejador(CellMasterDbContext _context)
            {
                this.context = _context;
            }

            public async Task<List<VistaInventario>> Handle(Prueba request, CancellationToken cancellationToken)
            {
                // Obtener datos del inventario desde la base de datos
                var inventario = await context.VistaInventarios.ToListAsync();

                return inventario;
            }
        }
    }
}
