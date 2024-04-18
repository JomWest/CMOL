using System.IO;
using System.Threading;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using Rectangle = iTextSharp.text.Rectangle;

namespace Aplicacion
{
    public class ExportarPDFProducto
    {
        public class Consulta: IRequest<Stream>{}

        public class Manejador: IRequestHandler<Consulta, Stream>{

            //dbContext
            public readonly CellMasterDbContext context;

            public Manejador(CellMasterDbContext _context)
            {
                this.context = _context;
            }

            public async Task<Stream> Handle(Consulta request, CancellationToken cancellationToken)
            {
                var customers =  await context.Productos.ToListAsync();

                Font fuenteTitulo = new Font(Font.HELVETICA,14f, Font.BOLD, BaseColor.Blue);
                Font fuenteEncabezado = new Font(Font.HELVETICA,12f, Font.BOLD, BaseColor.Black);
                Font fuenteDatos = new Font(Font.HELVETICA,10f, Font.NORMAL, BaseColor.DarkGray);

                MemoryStream workstream = new MemoryStream();
                Rectangle rect = new Rectangle(PageSize.A4);

                Document document = new Document(rect, 0,0,50,100);
                PdfWriter writer = PdfWriter.GetInstance(document, workstream);
                writer.CloseStream = false;

                document.Open();
                document.AddTitle("Reporte de Productos");

                PdfPTable tabla = new PdfPTable(1);
                tabla.WidthPercentage=90;
                PdfPCell celda = new PdfPCell(new Phrase("Lista de Productos", fuenteTitulo));
                celda.Border = Rectangle.NO_BORDER;
                tabla.AddCell(celda);
                document.Add(tabla);

                document.Add(new Phrase(" "));

                Chunk linea = new Chunk(new LineSeparator(2f,100f, BaseColor.Red, Element.ALIGN_CENTER,0));
                document.Add(linea);

                document.Add(new Phrase(" "));

                PdfPTable tablaDatos = new PdfPTable(5);
                float[] width = new float[]{10,20,20,30,20};
                tablaDatos.SetWidthPercentage(width, rect);

                PdfPCell celdaID = new PdfPCell(new Phrase("ID Productos", fuenteEncabezado));
                tablaDatos.AddCell(celdaID);
                PdfPCell celdaCompania = new PdfPCell(new Phrase("Nombre", fuenteEncabezado));
                tablaDatos.AddCell(celdaCompania);
                PdfPCell celdaTitulo = new PdfPCell(new Phrase("Precio", fuenteEncabezado));
                tablaDatos.AddCell(celdaTitulo);
                PdfPCell celdaContacto = new PdfPCell(new Phrase("Cantidad", fuenteEncabezado));
                tablaDatos.AddCell(celdaContacto);
                PdfPCell celdaTelefono = new PdfPCell(new Phrase("ID Categoria", fuenteEncabezado));
                tablaDatos.AddCell(celdaTelefono);

                tablaDatos.WidthPercentage=90;

                foreach(var customer in customers){
                    PdfPCell celdaDatoID = new PdfPCell(new Phrase(customer.CodigoProd.ToString(), fuenteDatos));
                    tablaDatos.AddCell(celdaDatoID);

                    PdfPCell celdaDatoCompania = new PdfPCell(new Phrase(customer.NombreProducto, fuenteDatos));
                    tablaDatos.AddCell(celdaDatoCompania);

                    PdfPCell celdaDatoTitulo = new PdfPCell(new Phrase(customer.PrecioVenta.ToString(), fuenteDatos));
                    tablaDatos.AddCell(celdaDatoTitulo);
                    
                    PdfPCell celdaDatoContacto = new PdfPCell(new Phrase(customer.Stock.ToString(), fuenteDatos));
                    tablaDatos.AddCell(celdaDatoContacto);

                    PdfPCell celdaDatoPhone = new PdfPCell(new Phrase(customer.IdCategorias.ToString(), fuenteDatos));
                    tablaDatos.AddCell(celdaDatoPhone);

                }

                document.Add(tablaDatos);


                document.Close();


                byte[] byteData = workstream.ToArray();
                workstream.Write(byteData,0,byteData.Length);
                workstream.Position =0;
                return workstream;

            }
        }
    }
}