using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Filtro
{
    public class FiltrarIDCompras
    {
        public class Cajas : IRequest<Stream>
        {
            public int CursoId;
        }

        public class Manejador : IRequestHandler<Cajas, Stream>
        {
            private readonly CellMasterDbContext context;

            public Manejador(CellMasterDbContext _context)
            {
                this.context = _context;
            }

            public async Task<Stream> Handle(Cajas request, CancellationToken cancellationToken)
            {
                // Obtener el ID de la solicitud
                int ID = request.CursoId;

                // Obtener datos de compras filtrando por ID
                var invoices = await context.VistaCompras.Where(invoice => invoice.Idcompra == ID).ToListAsync();
                var datosFactura = context.VistaCompras.FirstOrDefault(invoice => invoice.Idcompra == ID);

                // Definir fuentes para el PDF
                Font fuenteTitulo = new Font(Font.HELVETICA, 14f, Font.BOLD, BaseColor.Blue);
                Font fuenteEncabezado = new Font(Font.HELVETICA, 11f, Font.BOLD, BaseColor.Black);
                Font fuenteDatos = new Font(Font.HELVETICA, 10f, Font.NORMAL, BaseColor.DarkGray);

                // Crear un flujo de memoria para el PDF
                MemoryStream workstream = new MemoryStream();
                Rectangle rect = new Rectangle(PageSize.A4);
                Document document = new Document(rect, 0, 0, 50, 100);
                PdfWriter writer = PdfWriter.GetInstance(document, workstream);
                writer.CloseStream = false;

                // Abrir el documento
                document.Open();
                document.AddTitle("Impresión de compras");

                #region Encabezado
                // Crear la tabla de encabezado
                PdfPTable headertable = new PdfPTable(3);
                headertable.WidthPercentage = 90;
                headertable.SetWidths(new float[] { 4, 2, 4 });
                headertable.DefaultCell.Border = Rectangle.NO_BORDER;
                headertable.SpacingAfter = 30;

                // Tabla anidada para la dirección
                PdfPTable nested = new PdfPTable(1);
                nested.DefaultCell.Border = Rectangle.BOX;

                // Celdas de la dirección
                PdfPCell CeldaDir1 = new PdfPCell(new Phrase("CellMaster Online ", fuenteDatos));
                CeldaDir1.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                nested.AddCell(CeldaDir1);

                PdfPCell CeldaDir2 = new PdfPCell(new Phrase("Gasolinera uno, Frente a la tiptop", fuenteDatos));
                CeldaDir2.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                nested.AddCell(CeldaDir2);

                PdfPCell CeldaDir3 = new PdfPCell(new Phrase("Matagalpa, Nicaragua", fuenteDatos));
                CeldaDir3.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                nested.AddCell(CeldaDir3);

                PdfPCell nesthousing = new PdfPCell(nested);
                nesthousing.Rowspan = 4;
                nesthousing.Padding = 0f;
                headertable.AddCell(nesthousing);

                headertable.AddCell("");

                // Celda del título
                PdfPCell CeldaFactura = new PdfPCell(new Phrase("Compra", fuenteTitulo));
                CeldaFactura.HorizontalAlignment = 2;
                CeldaFactura.Border = Rectangle.NO_BORDER;
                headertable.AddCell(CeldaFactura);

                // Celda del número de compra
                PdfPCell noCell = new PdfPCell(new Phrase("No :", fuenteEncabezado));
                noCell.HorizontalAlignment = 2;
                noCell.Border = Rectangle.NO_BORDER;
                headertable.AddCell(noCell);

                headertable.AddCell(new Phrase(datosFactura.Idcompra.ToString(), fuenteEncabezado));

                // Celda de la fecha
                PdfPCell CeldaFecha = new PdfPCell(new Phrase("Fecha :", fuenteEncabezado));
                CeldaFecha.HorizontalAlignment = 2;
                CeldaFecha.Border = Rectangle.NO_BORDER;
                headertable.AddCell(CeldaFecha);
                headertable.AddCell(new Phrase(datosFactura.Fecha.ToString(), fuenteDatos));

                // Celda del proveedor
                PdfPCell CeldaCliente = new PdfPCell(new Phrase("Proveedor:", fuenteEncabezado));
                CeldaCliente.HorizontalAlignment = 2;
                CeldaCliente.Border = Rectangle.NO_BORDER;
                headertable.AddCell(CeldaCliente);
                headertable.AddCell(new Phrase(datosFactura.Proveedor, fuenteDatos));

                // Agregar la tabla de encabezado al documento
                document.Add(headertable);
                #endregion

                #region TablaDetalle
                // Crear la tabla de detalles
                PdfPTable tablaDatos = new PdfPTable(5);
                float[] width = new float[] { 20, 20, 20, 20, 20 };
                tablaDatos.SetWidthPercentage(width, rect);
                tablaDatos.SpacingAfter = 40;
                tablaDatos.DefaultCell.Border = Rectangle.BOX;

                // Encabezados de la tabla de detalles
                PdfPCell celdaNomClien = new PdfPCell(new Phrase("Nombre cliente", fuenteEncabezado));
                tablaDatos.AddCell(celdaNomClien);

                PdfPCell celdaCantidad = new PdfPCell(new Phrase("Cantidad", fuenteEncabezado));
                tablaDatos.AddCell(celdaCantidad);

                PdfPCell celdaProducto = new PdfPCell(new Phrase("Producto", fuenteEncabezado));
                tablaDatos.AddCell(celdaProducto);

                PdfPCell celdaCantProducto = new PdfPCell(new Phrase("Fecha", fuenteEncabezado));
                tablaDatos.AddCell(celdaCantProducto);

                PdfPCell celdatotal = new PdfPCell(new Phrase("Precio", fuenteEncabezado));
                tablaDatos.AddCell(celdatotal);

                tablaDatos.WidthPercentage = 90;

                int item = 0;
                decimal? totalFactura = 0;

                // Llenar la tabla de detalles con los datos de las compras
                foreach (var invoice in invoices)
                {
                    item++;

                    PdfPCell celdaDatoNombre = new PdfPCell(new Phrase(invoice.NombreU, fuenteDatos));
                    tablaDatos.AddCell(celdaDatoNombre);

                    PdfPCell celdaDatoCantidad = new PdfPCell(new Phrase(invoice.Cantidad.ToString(), fuenteDatos));
                    tablaDatos.AddCell(celdaDatoCantidad);

                    PdfPCell celdaDatoProducto = new PdfPCell(new Phrase(invoice.Producto.ToString(), fuenteDatos));
                    tablaDatos.AddCell(celdaDatoProducto);

                    PdfPCell celdaDatoFecha = new PdfPCell(new Phrase(invoice.Fecha.ToString(), fuenteDatos));
                    tablaDatos.AddCell(celdaDatoFecha);

                    PdfPCell celdaDatoSubtotal = new PdfPCell(new Phrase(invoice.Precio.ToString(), fuenteDatos));
                    tablaDatos.AddCell(celdaDatoSubtotal);

                    totalFactura += invoice.Total;
                }

                // Celdas para el total en la tabla de detalles
                PdfPCell CeldaTotal1 = new PdfPCell(new Phrase(""));
                CeldaTotal1.Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER;
                tablaDatos.AddCell(CeldaTotal1);

                PdfPCell CeldaTotal2 = new PdfPCell(new Phrase(""));
                CeldaTotal2.Border = Rectangle.TOP_BORDER;
                tablaDatos.AddCell(CeldaTotal2);

                PdfPCell CeldaTotal3 = new PdfPCell(new Phrase(""));
                CeldaTotal3.Border = Rectangle.TOP_BORDER;
                tablaDatos.AddCell(CeldaTotal3);

                PdfPCell CeldaTotal4 = new PdfPCell(new Phrase(""));
                CeldaTotal4.Border = Rectangle.TOP_BORDER;
                tablaDatos.AddCell(CeldaTotal4);

                // Celdas para mostrar 'Total'
                PdfPCell CeldaTotalLetras = new PdfPCell(new Phrase("Total", fuenteEncabezado));
                CeldaTotalLetras.Border = Rectangle.TOP_BORDER;
                CeldaTotalLetras.HorizontalAlignment = 1;
                tablaDatos.AddCell(CeldaTotalLetras);

                // Celdas para mostrar el valor total de la factura
                PdfPCell CeldaTotalValor = new PdfPCell(new Phrase(totalFactura.ToString(), fuenteEncabezado));
                CeldaTotalValor.HorizontalAlignment = 1;
                tablaDatos.AddCell(CeldaTotalValor);

                // Celda para el mensaje del pie de página
                PdfPCell msjFooter = new PdfPCell(new Phrase("* Gire su cheque a Nombre de Luis Sanchez *", fuenteDatos));
                msjFooter.Colspan = 6;
                msjFooter.HorizontalAlignment = 1;
                tablaDatos.AddCell(msjFooter);
                #endregion

                // Agregar la tabla de detalles al documento
                document.Add(tablaDatos);

                // Cerrar el documento
                document.Close();

                // Convertir el flujo de memoria en un arreglo de bytes y escribirlo en el flujo de trabajo
                byte[] byteData = workstream.ToArray();
                workstream.Write(byteData, 0, byteData.Length);
                workstream.Position = 0;

                // Devolver el flujo de trabajo
                return workstream;
            }
        }
    }
}
