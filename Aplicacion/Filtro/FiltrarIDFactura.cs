using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Filtro
{
    public class FiltrarIDFactura
    {
        public class Cajas : IRequest<Stream>
        {
            public int CursoId;
        }

        public class Manejador : IRequestHandler<Cajas, Stream>
        {
            // DbContext
            private readonly CellMasterDbContext context;

            public Manejador(CellMasterDbContext _context)
            {
                this.context = _context;
            }

            public async Task<Stream> Handle(Cajas request, CancellationToken cancellationToken)
            {
                // Obtener el ID de la factura desde la solicitud
                int ID = request.CursoId;

                // Obtener los datos de la factura desde la base de datos
                var invoices = await context.VistaFacturas.Where(invoice => invoice.Idfactura == ID).ToListAsync();
                var datosFactura = context.VistaFacturas.FirstOrDefault(invoice => invoice.Idfactura == ID);

                // Definir fuentes para el PDF
                Font fuenteTitulo = new Font(Font.HELVETICA, 14f, Font.BOLD, BaseColor.Blue);
                Font fuenteEncabezado = new Font(Font.HELVETICA, 11f, Font.BOLD, BaseColor.Black);
                Font fuenteDatos = new Font(Font.HELVETICA, 10f, Font.NORMAL, BaseColor.DarkGray);

                // Inicializar el documento PDF
                MemoryStream workstream = new MemoryStream();
                Rectangle rect = new Rectangle(PageSize.A4);
                Document document = new Document(rect, 0, 0, 50, 100);
                PdfWriter writer = PdfWriter.GetInstance(document, workstream);
                writer.CloseStream = false;

                document.Open();
                document.AddTitle("Impresión de Ventas");

                #region Encabezado
                // Crear el encabezado

                PdfPTable headertable = new PdfPTable(3);
                headertable.WidthPercentage = 90;
                headertable.SetWidths(new float[] { 4, 2, 4 });
                headertable.DefaultCell.Border = Rectangle.NO_BORDER;
                headertable.SpacingAfter = 30;

                PdfPTable nested = new PdfPTable(1);
                nested.DefaultCell.Border = Rectangle.BOX;

                // Dirección de la empresa
                PdfPCell CeldaDir1 = new PdfPCell(new Phrase("CellMaster Online ", fuenteDatos));
                CeldaDir1.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                nested.AddCell(CeldaDir1);

                PdfPCell CeldaDir2 = new PdfPCell(new Phrase("Gasolinera uno, Frente a la tiptop", fuenteDatos));
                CeldaDir2.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                nested.AddCell(CeldaDir2);

                PdfPCell CeldaDir3 = new PdfPCell(new Phrase("Matagalpa, Nicaragua", fuenteDatos));
                CeldaDir3.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                nested.AddCell(CeldaDir3);

                // Celda que contiene la tabla anidada
                PdfPCell nesthousing = new PdfPCell(nested);
                nesthousing.Rowspan = 4;
                nesthousing.Padding = 0f;
                headertable.AddCell(nesthousing);

                headertable.AddCell("");  // Espacio en medio

                // Celda para el título "Compra"
                PdfPCell CeldaFactura = new PdfPCell(new Phrase("Factura", fuenteTitulo));
                CeldaFactura.HorizontalAlignment = 2;
                CeldaFactura.Border = Rectangle.NO_BORDER;
                headertable.AddCell(CeldaFactura);

                // Celda para "No"
                PdfPCell noCell = new PdfPCell(new Phrase("No :", fuenteEncabezado));
                noCell.HorizontalAlignment = 2;
                noCell.Border = Rectangle.NO_BORDER;
                headertable.AddCell(noCell);

                // Celda para el número de compra
                headertable.AddCell(new Phrase(datosFactura.Idfactura.ToString(), fuenteEncabezado));

                // Celda para "Fecha"
                PdfPCell CeldaFecha = new PdfPCell(new Phrase("Fecha :", fuenteEncabezado));
                CeldaFecha.HorizontalAlignment = 2;
                CeldaFecha.Border = Rectangle.NO_BORDER;
                headertable.AddCell(CeldaFecha);
                headertable.AddCell(new Phrase(datosFactura.Fecha.ToString(), fuenteDatos));

                // Celda para "Chambeador"
                PdfPCell CeldaCliente = new PdfPCell(new Phrase("Chambeador:", fuenteEncabezado));
                CeldaCliente.HorizontalAlignment = 2;
                CeldaCliente.Border = Rectangle.NO_BORDER;
                headertable.AddCell(CeldaCliente);
                headertable.AddCell(new Phrase(datosFactura.Chambeador, fuenteDatos));

                // Agregar la tabla de encabezado al documento
                document.Add(headertable);
                #endregion

                #region TablaDetalle
                // Crear la tabla para los detalles de la compra
                PdfPTable tablaDatos = new PdfPTable(5);
                float[] width = new float[] { 20, 20, 20, 20, 20 };
                tablaDatos.SetWidthPercentage(width, rect);
                tablaDatos.SpacingAfter = 40;
                tablaDatos.DefaultCell.Border = Rectangle.BOX;

                // Celdas de encabezado
                PdfPCell celdaNomClien = new PdfPCell(new Phrase("Nombre cliente", fuenteEncabezado));
                tablaDatos.AddCell(celdaNomClien);
                
                PdfPCell celdaPrecUnit = new PdfPCell(new Phrase("Cedula", fuenteEncabezado));
                tablaDatos.AddCell(celdaPrecUnit);

                PdfPCell celdaSubtotal = new PdfPCell(new Phrase("Producto", fuenteEncabezado));
                tablaDatos.AddCell(celdaSubtotal);

                PdfPCell celdaCant = new PdfPCell(new Phrase("Cantidad de Productos", fuenteEncabezado));
                tablaDatos.AddCell(celdaCant);

                PdfPCell celdatotal = new PdfPCell(new Phrase("Precio", fuenteEncabezado));
                tablaDatos.AddCell(celdatotal);

                tablaDatos.WidthPercentage = 90;

                // Detalles en el iterador
                int item = 0;
                decimal? totalFactura = 0;
                foreach (var invoice in invoices)
                {
                    PdfPCell celdaDatoNombre = new PdfPCell(new Phrase(invoice.NombreCliente, fuenteDatos));
                    tablaDatos.AddCell(celdaDatoNombre);

                    PdfPCell celdaDatoProducto = new PdfPCell(new Phrase(invoice.CedulaC.ToString(), fuenteDatos));
                    tablaDatos.AddCell(celdaDatoProducto);

                    PdfPCell celdaDatoPrecio = new PdfPCell(new Phrase(invoice.NombreProducto.ToString(), fuenteDatos));
                    tablaDatos.AddCell(celdaDatoPrecio);

                    item++;
                    PdfPCell celdaDatoCantidad = new PdfPCell(new Phrase(invoice.CantidadProductos.ToString(), fuenteDatos));
                    tablaDatos.AddCell(celdaDatoCantidad);

                    PdfPCell celdaDatoSubtotal = new PdfPCell(new Phrase(invoice.Precio.ToString(), fuenteDatos));
                    tablaDatos.AddCell(celdaDatoSubtotal);

                    // Calcular el total de la factura mientras se imprime
                    totalFactura += invoice.TotalFactura;
                }

                // Celdas para el total
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

                PdfPCell CeldaTotalLetras = new PdfPCell(new Phrase("Total", fuenteEncabezado));
                CeldaTotalLetras.Border = Rectangle.TOP_BORDER;
                CeldaTotalLetras.HorizontalAlignment = 1;
                tablaDatos.AddCell(CeldaTotalLetras);

                PdfPCell CeldaTotalValor = new PdfPCell(new Phrase(totalFactura.ToString(), fuenteEncabezado));
                CeldaTotalValor.HorizontalAlignment = 1;
                tablaDatos.AddCell(CeldaTotalValor);

                // Celda para un mensaje en el pie de página
                PdfPCell msjFooter = new PdfPCell(new Phrase("*** Gire su cheque a Nombre de Luis Sanchez ***", fuenteDatos));
                msjFooter.Colspan = 6;
                msjFooter.HorizontalAlignment = 1;
                tablaDatos.AddCell(msjFooter);
                #endregion

                // Agregar la tabla de detalles al documento
                document.Add(tablaDatos);
                document.Close();

                // Convertir el stream de memoria en un array de bytes y retornarlo
                byte[] byteData = workstream.ToArray();
                workstream.Write(byteData, 0, byteData.Length);
                workstream.Position = 0;

                return workstream;
            }
        }
    }
}
