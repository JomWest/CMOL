using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion
{
    public class ImprimirFactura
    {
        public class Consulta: IRequest<Stream>
        {
           public int IdUser;
         }

        public class Manejador: IRequestHandler<Consulta, Stream>{


            //dbContext
            private readonly CellMasterDbContext context;

            public Manejador(CellMasterDbContext _context)
            {
                this.context = _context;
            }

            public async Task<Stream> Handle(Consulta request, CancellationToken cancellationToken)
            {

                int Id = request.IdUser;

                //var invoices = await context.Invoices.ToListAsync();
                //Obteniendo detalle del valor del id de factura enviado desde front.
                var invoices = await context.Vfacturas.Where(invoice => invoice.Idfactura == Id).ToListAsync();
                //Vuelvo a obtener, pero esta vez sólo una fila para los encabezados.
                var datosFactura = context.Vfacturas.FirstOrDefault(invoice => invoice.Idfactura == Id);


                //Definiendo Fuentes
                Font fuenteTitulo = new Font(Font.HELVETICA,14f, Font.BOLD, BaseColor.Blue);
                Font fuenteEncabezado = new Font(Font.HELVETICA,11f, Font.BOLD, BaseColor.Black);
                Font fuenteDatos = new Font(Font.HELVETICA,10f, Font.NORMAL, BaseColor.DarkGray);

                //Inicializando el documento
                MemoryStream workstream = new MemoryStream();
                Rectangle rect = new Rectangle(PageSize.A4);

                Document document = new Document(rect, 0,0,50,100);
                PdfWriter writer = PdfWriter.GetInstance(document, workstream);
                writer.CloseStream = false;

                document.Open();
                document.AddTitle("Impresión de Facturas");

#region Encabezado
    // Crea el encabezado
                PdfPTable headertable = new PdfPTable(3);
                headertable.WidthPercentage = 90;
                headertable.SetWidths(new float[] { 4, 2, 4 });  // columnas relativas
                headertable.DefaultCell.Border = Rectangle.NO_BORDER;
                headertable.SpacingAfter = 30;
                PdfPTable nested = new PdfPTable(1);
                nested.DefaultCell.Border = Rectangle.BOX;
                PdfPCell CeldaDir1 = new PdfPCell(new Phrase("UNAN Managua FAREM Matagalpa", fuenteDatos));
                CeldaDir1.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                nested.AddCell(CeldaDir1);
                PdfPCell CeldaDir2 = new PdfPCell(new Phrase("Parque Darío 2 C Oeste", fuenteDatos));
                CeldaDir2.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                nested.AddCell(CeldaDir2);
                PdfPCell CeldaDir3 = new PdfPCell(new Phrase("Matagalpa, Nicaragua", fuenteDatos));
                CeldaDir3.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                nested.AddCell(CeldaDir3);
                PdfPCell nesthousing = new PdfPCell(nested);
                nesthousing.Rowspan = 4;
                nesthousing.Padding = 0f;
                headertable.AddCell(nesthousing);

                headertable.AddCell("");  //el vacio de en medio

                PdfPCell CeldaFactura = new PdfPCell(new Phrase("FACTURA", fuenteTitulo));
                CeldaFactura.HorizontalAlignment = 2;
                CeldaFactura.Border = Rectangle.NO_BORDER;
                headertable.AddCell(CeldaFactura);
                PdfPCell noCell = new PdfPCell(new Phrase("No :", fuenteEncabezado));
                noCell.HorizontalAlignment = 2;
                noCell.Border = Rectangle.NO_BORDER;
                headertable.AddCell(noCell);
                headertable.AddCell(new Phrase(datosFactura?.Idfactura.ToString(), fuenteEncabezado));
                PdfPCell CeldaFecha = new PdfPCell(new Phrase("Fecha :", fuenteEncabezado));
                CeldaFecha.HorizontalAlignment = 2;
                CeldaFecha.Border = Rectangle.NO_BORDER;
                headertable.AddCell(CeldaFecha);
                headertable.AddCell(new Phrase(datosFactura?.NombreProducto.ToString(), fuenteDatos));
                PdfPCell CeldaCliente = new PdfPCell(new Phrase("Cliente:", fuenteEncabezado));
                CeldaCliente.HorizontalAlignment = 2;
                CeldaCliente.Border = Rectangle.NO_BORDER;
                headertable.AddCell(CeldaCliente);
                headertable.AddCell(new Phrase(datosFactura?.ApellidoCliente, fuenteDatos));
                document.Add(headertable);
    #endregion


#region TablaDetalle

                PdfPTable tablaDatos = new PdfPTable(6);
                float[] width = new float[]{5,10,40,20,15,10};
                tablaDatos.SetWidthPercentage(width, rect);
                tablaDatos.SpacingAfter = 40;
                tablaDatos.DefaultCell.Border = Rectangle.BOX;

                PdfPCell celdaIDP = new PdfPCell(new Phrase("N°", fuenteEncabezado));
                tablaDatos.AddCell(celdaIDP);
                PdfPCell celdaCant = new PdfPCell(new Phrase("Cantidad", fuenteEncabezado));
                tablaDatos.AddCell(celdaCant);
                PdfPCell celdaNomProd = new PdfPCell(new Phrase("Descripción", fuenteEncabezado));
                tablaDatos.AddCell(celdaNomProd);
                PdfPCell celdaPrecUnit = new PdfPCell(new Phrase("Precio Unitario", fuenteEncabezado));
                tablaDatos.AddCell(celdaPrecUnit);
                PdfPCell celdaDesc = new PdfPCell(new Phrase("Descuento", fuenteEncabezado));
                tablaDatos.AddCell(celdaDesc);
                PdfPCell celdaSubtotal = new PdfPCell(new Phrase("SubTotal", fuenteEncabezado));
                tablaDatos.AddCell(celdaSubtotal);

                tablaDatos.WidthPercentage=90;

                //Detalle en iterador
                int item = 0;
                decimal totalFactura= 0;
                foreach(var invoice in invoices){

                    item++;
                    PdfPCell celdaDatoID = new PdfPCell(new Phrase(item.ToString(), fuenteDatos));
                    tablaDatos.AddCell(celdaDatoID);

                    PdfPCell celdaDatoCantidad = new PdfPCell(new Phrase(invoice.Idfactura.ToString(), fuenteDatos));
                    tablaDatos.AddCell(celdaDatoCantidad);

                    PdfPCell celdaDatoProducto = new PdfPCell(new Phrase(invoice.NombreProducto.ToString(), fuenteDatos));
                    tablaDatos.AddCell(celdaDatoProducto);
                    
                    PdfPCell celdaDatoPrecio = new PdfPCell(new Phrase(invoice.TotalFactura.ToString(), fuenteDatos));
                    tablaDatos.AddCell(celdaDatoPrecio);

                    PdfPCell celdaDatoSubtotal = new PdfPCell(new Phrase(invoice.TotalFactura.ToString(), fuenteDatos));
                    tablaDatos.AddCell(celdaDatoSubtotal);

                    //a la vez que imprimo, voy calculando el total de la factura.
                    totalFactura += 1;

                }

                // Table footer
                PdfPCell CeldaTotal1 = new PdfPCell(new Phrase(""));
                CeldaTotal1.Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER;
                tablaDatos.AddCell(CeldaTotal1);
                PdfPCell CeldaTotal2 = new PdfPCell(new Phrase(""));
                CeldaTotal2.Border = Rectangle.TOP_BORDER; //Rectangle.NO_BORDER; //Rectangle.TOP_BORDER;
                tablaDatos.AddCell(CeldaTotal2);
                PdfPCell CeldaTotal3 = new PdfPCell(new Phrase(""));
                CeldaTotal3.Border = Rectangle.TOP_BORDER; //Rectangle.NO_BORDER; //Rectangle.TOP_BORDER;
                tablaDatos.AddCell(CeldaTotal3);
                PdfPCell CeldaTotal4 = new PdfPCell(new Phrase(""));
                CeldaTotal4.Border = Rectangle.TOP_BORDER; //Rectangle.NO_BORDER; //Rectangle.TOP_BORDER;
                tablaDatos.AddCell(CeldaTotal4);
                PdfPCell CeldaTotalLetras = new PdfPCell(new Phrase("Total", fuenteEncabezado));
                CeldaTotalLetras.Border = Rectangle.TOP_BORDER;   //Rectangle.NO_BORDER; //Rectangle.TOP_BORDER;
                CeldaTotalLetras.HorizontalAlignment = 1;
                tablaDatos.AddCell(CeldaTotalLetras);
                PdfPCell CeldaTotalValor = new PdfPCell(new Phrase(totalFactura.ToString("#,###.00"), fuenteEncabezado));
                CeldaTotalValor.HorizontalAlignment = 1;
                tablaDatos.AddCell(CeldaTotalValor);

                PdfPCell msjFooter = new PdfPCell(new Phrase("*** Emita los cheques en Dólares a Nombre de Erick Lanzas ***", fuenteDatos));
                msjFooter.Colspan = 6;
                msjFooter.HorizontalAlignment = 1;
                tablaDatos.AddCell(msjFooter);
                
    #endregion


                document.Add(tablaDatos);


                //Cerrando el documento
                document.Close();

                //Pintando
                byte[] byteData = workstream.ToArray();
                workstream.Write(byteData,0,byteData.Length);
                workstream.Position =0;
                return workstream;

            }


        }
    }
}