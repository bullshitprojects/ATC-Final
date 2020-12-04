using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using RegistroDeTransacciones.Clases;
using System;
using System.Collections.Generic;

namespace RegistroDeTransacciones.Reportes
{
    class Reporte
    {
        public string path;
        public void PrintReport(List<Inventario> lista, string unidades, string costos, string total)
        {
            //Fuentes y Colores
            Color navy = new DeviceRgb(44, 59, 84);
            Color gray = new DeviceRgb(217, 217, 217);
            Color encabezado = new DeviceRgb(40, 80, 84);
            PdfFont normal = PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.HELVETICA);

            //Estilos
            Dictionary<int, Style> mainStyles = new Dictionary<int, Style>
             {
                 //TITULO CENTRADO
                 {100, new Style().SetBackgroundColor(ColorConstants.WHITE).SetBorder(Border.NO_BORDER).SetFontColor(ColorConstants.BLACK).SetFontSize(14).SetFont(normal).SetTextAlignment(TextAlignment.CENTER) },
                  //TEXTO NORMAL ALINEADO IZQUIERA FONDO GRIS
                {101, new Style().SetBackgroundColor(gray).SetBorder(Border.NO_BORDER).SetFontColor(ColorConstants.BLACK).SetFontSize(12).SetFont(normal).SetTextAlignment(TextAlignment.LEFT) },
                 //TEXTO NORMAL ALINEAMIENTO IZQUIERDA
                 {102, new Style().SetBackgroundColor(ColorConstants.WHITE).SetBorder(Border.NO_BORDER).SetFontColor(ColorConstants.BLACK).SetFontSize(12).SetFont(normal).SetTextAlignment(TextAlignment.LEFT) },
                //Fecha de impresion
                {103, new Style().SetBackgroundColor(ColorConstants.WHITE).SetBorder(Border.NO_BORDER).SetFontColor(ColorConstants.BLACK).SetFontSize(9).SetFont(normal).SetTextAlignment(TextAlignment.LEFT) },
                 //TEXTO DE ENCABEZADO DE COLUMNAS
                {105, new Style().SetBackgroundColor(navy).SetBorder(Border.NO_BORDER).SetFontColor(ColorConstants.WHITE).SetFontSize(12).SetFont(normal).SetTextAlignment(TextAlignment.LEFT) },
                //Encabezado Principal
                {106, new Style().SetBackgroundColor(encabezado).SetBorder(Border.NO_BORDER).SetFontColor(ColorConstants.WHITE).SetFontSize(12).SetFont(normal).SetTextAlignment(TextAlignment.CENTER) }
             };

            string fecha = DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year;
            var exportFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            path = System.IO.Path.Combine(exportFolder, "Inventario(" + fecha + ").pdf");
            using (var writer = new PdfWriter(path))
            {
                using (var pdf = new PdfDocument(writer))
                {
                    var doc = new Document(pdf, PageSize.A4.Rotate());
                    doc.SetMargins(35, 35, 35, 35);

                    Table tabla = new Table(new float[11]).UseAllAvailableWidth();
                    Cell contenido;

                    //Titulo
                    contenido = new Cell(1, 11).Add(new Paragraph("Valuación de Inventario - Método Promedio")).AddStyle(mainStyles[100]);
                    tabla.AddCell(contenido);

                    //Espacio en blanco
                    contenido = new Cell(1, 11).Add(new Paragraph("\n ")).AddStyle(mainStyles[102]);
                    tabla.AddCell(contenido);
                    

                    //Contenido
                    contenido = new Cell(1, 3).Add(new Paragraph("Promedio Ponderado")).AddStyle(mainStyles[106]);
                    tabla.AddCell(contenido);
                    contenido = new Cell(1, 3).Add(new Paragraph("Unidades")).AddStyle(mainStyles[106]);
                    tabla.AddCell(contenido);
                    contenido = new Cell(1, 2).Add(new Paragraph("Costos")).AddStyle(mainStyles[106]);
                    tabla.AddCell(contenido);
                    contenido = new Cell(1, 3).Add(new Paragraph("Valores")).AddStyle(mainStyles[106]);
                    tabla.AddCell(contenido);

                    contenido = new Cell(1, 1).Add(new Paragraph("Fecha")).AddStyle(mainStyles[105]);
                    tabla.AddCell(contenido);
                    contenido = new Cell(1, 1).Add(new Paragraph("Nombre")).AddStyle(mainStyles[105]);
                    tabla.AddCell(contenido);
                    contenido = new Cell(1, 1).Add(new Paragraph("Concepto")).AddStyle(mainStyles[105]);
                    tabla.AddCell(contenido);
                    contenido = new Cell(1, 1).Add(new Paragraph("Entradas")).AddStyle(mainStyles[105]);
                    tabla.AddCell(contenido);
                    contenido = new Cell(1, 1).Add(new Paragraph("Salidas")).AddStyle(mainStyles[105]);
                    tabla.AddCell(contenido);
                    contenido = new Cell(1, 1).Add(new Paragraph("Existencias")).AddStyle(mainStyles[105]);
                    tabla.AddCell(contenido);
                    contenido = new Cell(1, 1).Add(new Paragraph("Unitario")).AddStyle(mainStyles[105]);
                    tabla.AddCell(contenido);
                    contenido = new Cell(1, 1).Add(new Paragraph("Promedio")).AddStyle(mainStyles[105]);
                    tabla.AddCell(contenido);
                    contenido = new Cell(1, 1).Add(new Paragraph("Debe")).AddStyle(mainStyles[105]);
                    tabla.AddCell(contenido);
                    contenido = new Cell(1, 1).Add(new Paragraph("Haber")).AddStyle(mainStyles[105]);
                    tabla.AddCell(contenido);
                    contenido = new Cell(1, 1).Add(new Paragraph("Saldo")).AddStyle(mainStyles[105]);
                    tabla.AddCell(contenido);

                    for (int i = 0; i < lista.Count; i++)
                    {
                        if (i % 2 == 0)
                        {
                            contenido = new Cell(1, 1).Add(new Paragraph(lista[i].Fecha)).AddStyle(mainStyles[102]);
                            tabla.AddCell(contenido);
                            contenido = new Cell(1, 1).Add(new Paragraph(lista[i].Nombre)).AddStyle(mainStyles[102]);
                            tabla.AddCell(contenido);
                            contenido = new Cell(1, 1).Add(new Paragraph(lista[i].Concepto)).AddStyle(mainStyles[102]);
                            tabla.AddCell(contenido);
                            contenido = new Cell(1, 1).Add(new Paragraph(lista[i].Entradas)).AddStyle(mainStyles[102]);
                            tabla.AddCell(contenido);
                            contenido = new Cell(1, 1).Add(new Paragraph(lista[i].Salidas)).AddStyle(mainStyles[102]);
                            tabla.AddCell(contenido);
                            contenido = new Cell(1, 1).Add(new Paragraph(lista[i].Existencias)).AddStyle(mainStyles[102]);
                            tabla.AddCell(contenido);
                            contenido = new Cell(1, 1).Add(new Paragraph(lista[i].Unitario)).AddStyle(mainStyles[102]);
                            tabla.AddCell(contenido);
                            contenido = new Cell(1, 1).Add(new Paragraph(lista[i].Promedio)).AddStyle(mainStyles[102]);
                            tabla.AddCell(contenido);
                            contenido = new Cell(1, 1).Add(new Paragraph(lista[i].Debe)).AddStyle(mainStyles[102]);
                            tabla.AddCell(contenido);
                            contenido = new Cell(1, 1).Add(new Paragraph(lista[i].Haber)).AddStyle(mainStyles[102]);
                            tabla.AddCell(contenido);
                            contenido = new Cell(1, 1).Add(new Paragraph(lista[i].Saldo)).AddStyle(mainStyles[102]);
                            tabla.AddCell(contenido);
                        }
                        else
                        {
                            contenido = new Cell(1, 1).Add(new Paragraph(lista[i].Fecha)).AddStyle(mainStyles[101]);
                            tabla.AddCell(contenido);
                            contenido = new Cell(1, 1).Add(new Paragraph(lista[i].Nombre)).AddStyle(mainStyles[101]);
                            tabla.AddCell(contenido);
                            contenido = new Cell(1, 1).Add(new Paragraph(lista[i].Concepto)).AddStyle(mainStyles[101]);
                            tabla.AddCell(contenido);
                            contenido = new Cell(1, 1).Add(new Paragraph(lista[i].Entradas)).AddStyle(mainStyles[101]);
                            tabla.AddCell(contenido);
                            contenido = new Cell(1, 1).Add(new Paragraph(lista[i].Salidas)).AddStyle(mainStyles[101]);
                            tabla.AddCell(contenido);
                            contenido = new Cell(1, 1).Add(new Paragraph(lista[i].Existencias)).AddStyle(mainStyles[101]);
                            tabla.AddCell(contenido);
                            contenido = new Cell(1, 1).Add(new Paragraph(lista[i].Unitario)).AddStyle(mainStyles[101]);
                            tabla.AddCell(contenido);
                            contenido = new Cell(1, 1).Add(new Paragraph(lista[i].Promedio)).AddStyle(mainStyles[101]);
                            tabla.AddCell(contenido);
                            contenido = new Cell(1, 1).Add(new Paragraph(lista[i].Debe)).AddStyle(mainStyles[101]);
                            tabla.AddCell(contenido);
                            contenido = new Cell(1, 1).Add(new Paragraph(lista[i].Haber)).AddStyle(mainStyles[101]);
                            tabla.AddCell(contenido);
                            contenido = new Cell(1, 1).Add(new Paragraph(lista[i].Saldo)).AddStyle(mainStyles[101]);
                            tabla.AddCell(contenido);
                        }
                    }
                    //Espacio en blanco
                    contenido = new Cell(1, 11).Add(new Paragraph("\n ")).AddStyle(mainStyles[102]);
                    tabla.AddCell(contenido);

                    //Datos Finales
                    contenido = new Cell(1, 1).Add(new Paragraph(" ")).AddStyle(mainStyles[102]);
                    tabla.AddCell(contenido);
                    contenido = new Cell(1, 1).Add(new Paragraph(" ")).AddStyle(mainStyles[102]);
                    tabla.AddCell(contenido);
                    contenido = new Cell(1, 1).Add(new Paragraph(" ")).AddStyle(mainStyles[102]);
                    tabla.AddCell(contenido);
                    contenido = new Cell(1, 1).Add(new Paragraph(" ")).AddStyle(mainStyles[102]);
                    tabla.AddCell(contenido);
                    contenido = new Cell(1, 1).Add(new Paragraph(" ")).AddStyle(mainStyles[102]);
                    tabla.AddCell(contenido);
                    contenido = new Cell(1, 1).Add(new Paragraph(" ")).AddStyle(mainStyles[102]);
                    tabla.AddCell(contenido);
                    contenido = new Cell(1, 1).Add(new Paragraph(" ")).AddStyle(mainStyles[102]);
                    tabla.AddCell(contenido);
                    contenido = new Cell(1, 1).Add(new Paragraph(" ")).AddStyle(mainStyles[102]);
                    tabla.AddCell(contenido);
                    contenido = new Cell(1, 1).Add(new Paragraph("Unidades")).AddStyle(mainStyles[106]);
                    tabla.AddCell(contenido);
                    contenido = new Cell(1, 1).Add(new Paragraph("Costos")).AddStyle(mainStyles[106]);
                    tabla.AddCell(contenido);
                    contenido = new Cell(1, 1).Add(new Paragraph("Total")).AddStyle(mainStyles[106]);
                    tabla.AddCell(contenido);

                    contenido = new Cell(1, 1).Add(new Paragraph(" ")).AddStyle(mainStyles[102]);
                    tabla.AddCell(contenido);
                    contenido = new Cell(1, 1).Add(new Paragraph(" ")).AddStyle(mainStyles[102]);
                    tabla.AddCell(contenido);
                    contenido = new Cell(1, 1).Add(new Paragraph(" ")).AddStyle(mainStyles[102]);
                    tabla.AddCell(contenido);
                    contenido = new Cell(1, 1).Add(new Paragraph(" ")).AddStyle(mainStyles[102]);
                    tabla.AddCell(contenido);
                    contenido = new Cell(1, 1).Add(new Paragraph(" ")).AddStyle(mainStyles[102]);
                    tabla.AddCell(contenido);
                    contenido = new Cell(1, 1).Add(new Paragraph(" ")).AddStyle(mainStyles[102]);
                    tabla.AddCell(contenido);
                    contenido = new Cell(1, 1).Add(new Paragraph(" ")).AddStyle(mainStyles[102]);
                    tabla.AddCell(contenido);
                    contenido = new Cell(1, 1).Add(new Paragraph(" ")).AddStyle(mainStyles[102]);
                    tabla.AddCell(contenido);
                    contenido = new Cell(1, 1).Add(new Paragraph(unidades)).AddStyle(mainStyles[101]).AddStyle(new Style().SetTextAlignment(TextAlignment.CENTER));
                    tabla.AddCell(contenido);
                    contenido = new Cell(1, 1).Add(new Paragraph(costos)).AddStyle(mainStyles[101]);
                    tabla.AddCell(contenido);
                    contenido = new Cell(1, 1).Add(new Paragraph(total)).AddStyle(mainStyles[101]);
                    tabla.AddCell(contenido);

                    //espacios
                    contenido = new Cell(1, 11).Add(new Paragraph("\n ")).AddStyle(mainStyles[100]);
                    tabla.AddCell(contenido);
                 
                    //pie de página
                    contenido = new Cell(1, 11).Add(new Paragraph("Fecha de impresión: " + DateTime.Now.ToString())).AddStyle(mainStyles[103]);
                    tabla.AddCell(contenido);
                    doc.Add(tabla);
                    doc.Close();
                }
            }
        }
    }
}
