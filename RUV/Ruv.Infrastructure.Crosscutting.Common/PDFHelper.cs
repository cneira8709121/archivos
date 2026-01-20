using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using iTextSharp.text.pdf;
using Ionic.Zip;
using iTextSharp.text;
using resx = Ruv.Infrastructure.Crosscutting.Resources.Globalization;

namespace Ruv.Infrastructure.Crosscutting.Common
{
    public class PDFHelper
    {
        /// <summary>
        /// Genera un archivo PDF a partir del template definido.
        /// </summary>
        /// <param name="codigo">Codigo que se imprimirá en el archivo PDF</param>
        /// <param name="templatePath">Ruta donde se encuentra el template</param>
        /// <returns>Arreglo de bytes que conforman el PDF</returns>
        public static byte[] GenerateOnePdfFile(string codigo, string templatePath)
        {
            byte[] buffer;
            using (var existingFileStream = new FileStream(templatePath, FileMode.Open))
            using (var newFileStream = new MemoryStream())
            {
                // Open existing PDF
                var pdfReader = new PdfReader(existingFileStream);

                // PdfStamper, which will create
                var stamper = new PdfStamper(pdfReader, newFileStream);

                var form = stamper.AcroFields;
                var fieldKeys = form.Fields.Keys;

                form.SetField("txtFormulario1", codigo);
                form.SetField("txtFormulario2", codigo);
                form.SetField("txtFormulario3", codigo);
                form.SetField("txtFormulario4", codigo);
                form.SetField("txtFormulario5", codigo);

                // "Flatten" the form so it wont be editable/usable anymore
                stamper.FormFlattening = true;

                stamper.Close();
                pdfReader.Close();

                buffer = newFileStream.ToArray();
            }

            return buffer;
        }

        /// <summary>
        /// Genera varios archivos PDFs, uno para cada codigo suministrado en el listado de codigos
        /// </summary>
        /// <param name="codigos">Listado de codigos de formulario</param>
        /// <param name="templatePath">Ruta donde se encuentra el template</param>
        /// <returns>Diccionario con los codigos y arreglos de bytes que conforman los PDFs</returns>
        public static IDictionary<string, byte[]> GenerateManyPdfFiles(IEnumerable<string> codigos, string templatePath)
        {
            IDictionary<string, byte[]> dic = new Dictionary<string, byte[]>();

            foreach (string codigo in codigos)
            {
                dic.Add(codigo, GenerateOnePdfFile(codigo, templatePath));
            }

            return dic;
        }

        /// <summary>
        /// Genera un archivo ZIP con múltiples archivos PDF, cada uno de ellos con el código ingresado.
        /// Cada archivo PDF es generado a partir de un template definido.
        /// </summary>
        /// <param name="codigos">Lista de códigos que llevará cada archivo PDF</param>
        /// <param name="templatePath">Ruta donde se encuentra el template</param>
        /// <returns></returns>
        public static byte[] GenerateManyPdfFilesAsZip(IList<string> codigos, string templatePath)
        {
            byte[] bZipedPdfs = null;
            IDictionary<string, byte[]> dictPdfs = PDFHelper.GenerateManyPdfFiles(codigos, templatePath);
            using (ZipFile zip = new ZipFile())
            {
                using (MemoryStream st = new MemoryStream())
                {
                    foreach (KeyValuePair<string, byte[]> pair in dictPdfs)
                    {
                        zip.AddEntry(pair.Key + ".pdf", pair.Value);
                    }
                    zip.Save(st);
                    bZipedPdfs = st.ToArray();
                }
            }
            return bZipedPdfs;
        }

        public static byte[] GenerateManyPdfFilesAsZip(Dictionary<string, bool> codigos, string templatePathTrue, string templatePathFalse)
        {
            byte[] bZipedPdfs = null;
            IDictionary<string, byte[]> dictPdfsTrue = PDFHelper.GenerateManyPdfFiles(
                codigos
                    .Where(x => x.Value)
                    .Select(x => x.Key), templatePathTrue);
            IDictionary<string, byte[]> dictPdfsFalse = PDFHelper.GenerateManyPdfFiles(
                codigos
                    .Where(x => !x.Value)
                    .Select(x => x.Key), templatePathFalse);

            using (ZipFile zip = new ZipFile())
            {
                using (MemoryStream st = new MemoryStream())
                {
                    foreach (KeyValuePair<string, byte[]> pair in dictPdfsTrue)
                    {
                        zip.AddEntry(pair.Key + ".pdf", pair.Value);
                    }
                    foreach (KeyValuePair<string, byte[]> pair in dictPdfsFalse)
                    {
                        zip.AddEntry(pair.Key + ".pdf", pair.Value);
                    }
                    zip.Save(st);
                    bZipedPdfs = st.ToArray();
                }
            }
            return bZipedPdfs;
        }

        /// <summary>
        /// Genera un archivo PDF a aprtir del template, con el contenido especificado
        /// </summary>
        /// <param name="contenido">Contenido del PDF</param>
        /// <param name="templatePath">Ruta donde se encuentra el template</param>
        /// <returns></returns>
        public static byte[] GeneratePdf(string contenido, string templatePath)
        {
            byte[] buffer;
            using (var existingFileStream = new FileStream(templatePath, FileMode.Open))
            using (var newFileStream = new MemoryStream())
            {
                var pdfReader = new PdfReader(existingFileStream);

                var stamper = new PdfStamper(pdfReader, newFileStream);

                var form = stamper.AcroFields;
                var fieldKeys = form.Fields.Keys;

                form.SetField("txtContenido", contenido);

                stamper.FormFlattening = true;

                stamper.Close();
                pdfReader.Close();

                buffer = newFileStream.ToArray();
            }

            return buffer;
        }

        /// <summary>
        /// Genera un archivo PDF a partir del template, con el contenido especificado
        /// </summary>
        /// <param name="contenido">Contenido del PDF</param>
        /// <param name="templatePath">Ruta donde se encuentra el template</param>
        /// <returns></returns>
        public static byte[] GeneratePdfDevolucion(int NIdDeclaracion,             
                                                   string cEntidadMunicipio,
                                                   string cMunicipio,
                                                   string CParteEmotiva,
                                                   DateTime? DFechaDevolucion,
                                                   string CNombreDeclarante,
                                                   string cTipoDocumento,
                                                   int nNumeroDocumento,
                                                   DateTime? DFechaDeclaracion, 
                                                   string templatePath)
        {
            //TODO: Johnatan debe modificar esta función para que el pdf a partir del template pagíne correctamente

            byte[] buffer;
            using (var existingFileStream = new FileStream(templatePath, FileMode.Open))
            using (var newFileStream = new MemoryStream())
            {
                var pdfReader = new PdfReader(existingFileStream);

                var stamper = new PdfStamper(pdfReader, newFileStream);

                var form = stamper.AcroFields;
                var fieldKeys = form.Fields.Keys;

                string strContenido = string.Format(Resources.General.TextoDevolucion, 
                                                    ((DateTime)DFechaDevolucion).ToShortDateString(),
                                                    cEntidadMunicipio, 
                                                    cMunicipio,
                                                    NIdDeclaracion,
                                                    DFechaDeclaracion.HasValue ? DFechaDeclaracion.Value.ToShortDateString() : resx::Controles.NA,
                                                    CNombreDeclarante,
                                                    cTipoDocumento, 
                                                    nNumeroDocumento, 
                                                    CParteEmotiva);

                form.SetField("txtContenido", strContenido);

                stamper.FormFlattening = true;

                stamper.Close();
                pdfReader.Close();

                buffer = newFileStream.ToArray();
            }

            return buffer;
        }

        //public static byte[] GeneratePdfDevolucion(string contenido, string templatePath)
        //{
        //    byte[] buffer;
        //    Document doc = new Document();

        //    using (FileStream existingFileStream = new FileStream(templatePath, FileMode.Open))
        //    using (MemoryStream newFileStream = new MemoryStream())
        //    {
        //        PdfWriter pdfWriter = PdfWriter.GetInstance(doc, existingFileStream);
        //        doc.Open();

        //        Paragraph p = new Paragraph();
        //        p.Add(contenido);

        //        doc.Add(p);
        //        //doc.Close();

        //        newFileStream.SetLength(existingFileStream.Length);
        //        existingFileStream.Read(newFileStream.GetBuffer(), 0, (int)existingFileStream.Length);

        //        newFileStream.Flush();
        //        existingFileStream.Close();

        //        doc.Close();
        //        buffer = newFileStream.ToArray();
        //    }

        //    return buffer;
        //}

        //public static byte[] GeneratePdfDevolucion(string contenido, string templatePath)
        //{
            //byte[] buffer;
            //Document doc = new Document();
            //using (Stream inputPdfStream = new FileStream(templatePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            //using (MemoryStream newFileStream = new MemoryStream())
            //{
            //    PdfWriter writer = PdfWriter.GetInstance(doc, newFileStream);
            //    doc.SetPageSize(PageSize.LETTER);
            //    doc.Open();
            //    PdfContentByte cb = writer.DirectContent;
            //    PdfImportedPage page;

            //    PdfReader reader = new PdfReader(inputPdfStream);
            //    doc.Add(

            //    buffer = newFileStream.ToArray();
            //}
            //return buffer;
            ////----------------------------------------
        //}

        public static byte[] MergePDFFiles(List<FileInfo> pdfFiles, ref int numberOfPages) {
            Document document = new Document();
            var generatedFileStream = new MemoryStream();
            numberOfPages = 0;
            try
            {
                //PdfWriter writer = PdfWriter.GetInstance(document, generatedFileStream);
                PdfCopy copy = new PdfCopy(document, generatedFileStream);
                document.Open();

                //PdfContentByte cb = writer.DirectContent;

                foreach (var file in pdfFiles) {
                    PdfReader reader = new PdfReader(file.FullName);
                    var thisNumberOfPages = reader.NumberOfPages;
                    for (int i = 1; i <= thisNumberOfPages; i++) {
                        //document.SetPageSize(reader.GetPageSizeWithRotation(1));
                        //document.NewPage();
                        //if (i == 1) {
                        //    Chunk fileRef = new Chunk(" ");
                        //    fileRef.SetLocalDestination(file.Name);
                        //    document.Add(fileRef);
                        //}
                        //PdfImportedPage page = writer.GetImportedPage(reader, i);
                        //int rotation = reader.GetPageRotation(i);
                        //if (rotation == 90 || rotation == 270)
                        //{
                        //    cb.AddTemplate(page, 0, -1f, 1f, 0, 0, reader.GetPageSizeWithRotation(i).Height);
                        //}
                        //else
                        //{
                        //    cb.AddTemplate(page, 1f, 0, 0, 1f, 0, 0);
                        //}
                        copy.AddPage(copy.GetImportedPage(reader, i));
                    }
                    numberOfPages += thisNumberOfPages;
                    copy.FreeReader(reader);
                    reader.Close();
                }
            }
            catch (Exception e) { throw new PdfException("No fue posible generar el archivo PDF unificado: " + e.Message); }
            finally { document.Close(); }
            return generatedFileStream.ToArray();
        }

    }
}
