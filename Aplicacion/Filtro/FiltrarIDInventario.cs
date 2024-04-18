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
    public class FiltrarIDInventario
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
                // Obtener el ID del producto desde la solicitud
                int ID = request.CursoId;

                // Obtener los datos del producto desde la base de datos
                var inventarios = await context.VistaInventarios.Where(inventario => inventario.IdProducto == ID).ToListAsync();
                var datosProducto = context.VistaInventarios.FirstOrDefault(inventario => inventario.IdProducto == ID);

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

                // Celda para el título "Factura"
                PdfPCell CeldaFactura = new PdfPCell(new Phrase("Inventario", fuenteTitulo));
                CeldaFactura.HorizontalAlignment = 2;
                CeldaFactura.Border = Rectangle.NO_BORDER;
                headertable.AddCell(CeldaFactura);

                // Celda para "No"
                PdfPCell noCell = new PdfPCell(new Phrase("No :", fuenteEncabezado));
                noCell.HorizontalAlignment = 2;
                noCell.Border = Rectangle.NO_BORDER;
                headertable.AddCell(noCell);

                // Celda para el número de factura
                headertable.AddCell(new Phrase(datosProducto.IdProducto.ToString(), fuenteEncabezado));

                // Celda para "Fecha"
                PdfPCell CeldaFecha = new PdfPCell(new Phrase("Fecha :", fuenteEncabezado));
                CeldaFecha.HorizontalAlignment = 2;
                CeldaFecha.Border = Rectangle.NO_BORDER;
                headertable.AddCell(CeldaFecha);

                // Generar fecha actual
                DateTime fechaActual = DateTime.Now;
                headertable.AddCell(new Phrase(fechaActual.ToString(), fuenteDatos));


                // Agregar la tabla de encabezado al documento
                document.Add(headertable);
                #endregion

                #region TablaDetalle
                // Crear la tabla para los detalles de la venta
                PdfPTable tablaDatos = new PdfPTable(8);
                float[] width = new float[] { 10, 10, 10, 10, 10, 10, 10, 10 };
                tablaDatos.SetWidthPercentage(width, rect);
                tablaDatos.SpacingAfter = 40;
                tablaDatos.DefaultCell.Border = Rectangle.BOX;

                // Celdas de encabezado
                

                PdfPCell celdaIdProveedores = new PdfPCell(new Phrase("ID Proveedore", fuenteEncabezado));
                tablaDatos.AddCell(celdaIdProveedores);

                PdfPCell celdaStock = new PdfPCell(new Phrase("Stock", fuenteEncabezado));
                tablaDatos.AddCell(celdaStock);

                PdfPCell celdaPrecioVenta = new PdfPCell(new Phrase("Precio Venta", fuenteEncabezado));
                tablaDatos.AddCell(celdaPrecioVenta);

                PdfPCell celdaIdCategorias = new PdfPCell(new Phrase("ID Categorías", fuenteEncabezado));
                tablaDatos.AddCell(celdaIdCategorias);

                PdfPCell celdaIdMarca = new PdfPCell(new Phrase("Marca Producto", fuenteEncabezado));
                tablaDatos.AddCell(celdaIdMarca);

                PdfPCell celdaNombreMarca = new PdfPCell(new Phrase("Categoria producto", fuenteEncabezado));
                tablaDatos.AddCell(celdaNombreMarca);

                PdfPCell celdaNombreEmpresa = new PdfPCell(new Phrase("Nombre Empresa", fuenteEncabezado));
                tablaDatos.AddCell(celdaNombreEmpresa);

                tablaDatos.WidthPercentage = 90;

                // Detalles en el iterador
                decimal? totalVenta = 0;
                foreach (var inventario in inventarios)
                {
                    PdfPCell celdaDatoIdProducto = new PdfPCell(new Phrase(inventario.IdProducto.ToString(), fuenteDatos));
                    tablaDatos.AddCell(celdaDatoIdProducto);

                    PdfPCell celdaDatoIdProveedores = new PdfPCell(new Phrase(inventario.IdProveedores.ToString(), fuenteDatos));
                    tablaDatos.AddCell(celdaDatoIdProveedores);

                    PdfPCell celdaDatoStock = new PdfPCell(new Phrase(inventario.Stock.ToString(), fuenteDatos));
                    tablaDatos.AddCell(celdaDatoStock);

                    PdfPCell celdaDatoPrecioVenta = new PdfPCell(new Phrase(inventario.PrecioVenta.ToString(), fuenteDatos));
                    tablaDatos.AddCell(celdaDatoPrecioVenta);

                    PdfPCell celdaDatoIdCategorias = new PdfPCell(new Phrase(inventario.IdCategorias.ToString(), fuenteDatos));
                    tablaDatos.AddCell(celdaDatoIdCategorias);

                    PdfPCell celdaDatoIdMarca = new PdfPCell(new Phrase(inventario.NombreMarca.ToString(), fuenteDatos));
                    tablaDatos.AddCell(celdaDatoIdMarca);

                    PdfPCell celdaDatoNombreMarca = new PdfPCell(new Phrase(inventario.Nombre, fuenteDatos));
                    tablaDatos.AddCell(celdaDatoNombreMarca);

                    PdfPCell celdaDatoNombreEmpresa = new PdfPCell(new Phrase(inventario.NombreEmpresa.ToString(), fuenteDatos));
                    tablaDatos.AddCell(celdaDatoNombreEmpresa);

                    // Calcular el total de la venta mientras se imprime
                    totalVenta += inventario.PrecioVenta;
                }

                // Celdas para el total
                PdfPCell celdaTotal1 = new PdfPCell(new Phrase(""));
                celdaTotal1.Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER;
                tablaDatos.AddCell(celdaTotal1);

                PdfPCell celdaTotal2 = new PdfPCell(new Phrase(""));
                celdaTotal2.Border = Rectangle.TOP_BORDER;
                tablaDatos.AddCell(celdaTotal2);

                PdfPCell celdaTotal3 = new PdfPCell(new Phrase(""));
                celdaTotal3.Border = Rectangle.TOP_BORDER;
                tablaDatos.AddCell(celdaTotal3);

                PdfPCell celdaTotal4 = new PdfPCell(new Phrase(""));
                celdaTotal4.Border = Rectangle.TOP_BORDER;
                tablaDatos.AddCell(celdaTotal4);

                PdfPCell celdaTotalLetras = new PdfPCell(new Phrase("Total", fuenteEncabezado));
                celdaTotalLetras.Border = Rectangle.TOP_BORDER;
                celdaTotalLetras.HorizontalAlignment = 1;
                tablaDatos.AddCell(celdaTotalLetras);

                PdfPCell celdaTotalValor = new PdfPCell(new Phrase(totalVenta.ToString(), fuenteEncabezado));
                celdaTotalValor.HorizontalAlignment = 1;
                tablaDatos.AddCell(celdaTotalValor);

                // Celda para un mensaje en el pie de página
                PdfPCell msjFooter = new PdfPCell(new Phrase("*** Gire su cheque a Nombre de Luis Sanchez ***", fuenteDatos));
                msjFooter.Colspan = 8;
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
