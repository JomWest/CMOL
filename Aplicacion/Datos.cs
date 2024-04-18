using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using Api.Models;
using Dominio;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion
{
    public class Datos
    {
        public class Consulta: IRequest<List<VFactura>>{

        }

        public class Manejador : IRequestHandler<Consulta,List<VFactura>>{

            //dbContext
            private readonly CellMasterDbContext context;

            public Manejador(CellMasterDbContext _context)
            {
                this.context = _context;
            }

            public async Task<List<VFactura>> Handle(Consulta request, CancellationToken cancellationToken){

            var invoices = await context.Vfacturas.ToListAsync();
            return invoices;

            }
        }
    }
    
    }