using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.Pkcs;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Org.BouncyCastle.X509;
using SysX509 = System.Security.Cryptography.X509Certificates;


    public class PdfUtilidades
    {
        public static byte[] MergeFiles(List<byte[]> sourceFiles)
        {
            Document document = new Document();
            MemoryStream output = new MemoryStream();
            try
            {
                try
                {
                    // Initialize pdf writer
                    PdfWriter writer = PdfWriter.GetInstance(document, output);
                    //writer.PageEvent = new PdfPageEvents();

                    // Open document to write
                    document.Open();
                    PdfContentByte content = writer.DirectContent;

                    // Iterate through all pdf documents
                    for (int fileCounter = 0; fileCounter < sourceFiles.Count; fileCounter++)
                    {
                        // Create pdf reader
                        PdfReader reader = new PdfReader(sourceFiles[fileCounter]);
                        int numberOfPages = reader.NumberOfPages;

                        // Iterate through all pages
                        for (int currentPageIndex = 1; currentPageIndex <= numberOfPages; currentPageIndex++)
                        {
                            // Determine page size for the current page
                            document.SetPageSize(reader.GetPageSizeWithRotation(currentPageIndex));
                            // Create page
                            document.NewPage();
                            PdfImportedPage importedPage = writer.GetImportedPage(reader, currentPageIndex);
                            // Determine page orientation
                            int pageOrientation = reader.GetPageRotation(currentPageIndex);
                            if ((pageOrientation == 90) || (pageOrientation == 270))
                            {
                                content.AddTemplate(importedPage, 0, -1f, 1f, 0, 0, reader.GetPageSizeWithRotation(currentPageIndex).Height);
                            }
                            else
                            {
                                content.AddTemplate(importedPage, 1f, 0, 0, 1f, 0, 0);
                            }
                        }
                    }
                }
                catch (Exception exception)
                {
                    RegistroTraza.I.Registrar(exception);
                    throw new Exception("There has an unexpected exception occured during the pdf merging process.", exception);
                }
            }
            finally
            {
                document.Close();
            }
            return output.GetBuffer();
        }
        /// <summary>
        /// Firma un documento
        /// </summary>
        /// <param name="Source">Documento origen</param>
        /// <param name="Target">Documento destino</param>
        /// <param name="Certificate">Certificado a utilizar</param>
        /// <param name="Reason">Razón de la firma</param>
        /// <param name="Location">Ubicación</param>
        /// <param name="AddVisibleSign">Establece si hay que agregar la firma visible al documento</param>
        public static void SignHashed(string Source, string Target, SysX509.X509Certificate2 Certificate, string Reason, string Location, bool AddVisibleSign)
        {
            X509CertificateParser objCP = new X509CertificateParser();
            X509Certificate[] objChain = new X509Certificate[] { objCP.ReadCertificate(Certificate.RawData) };

            PdfReader objReader = new PdfReader(Source);
            PdfStamper objStamper = PdfStamper.CreateSignature(objReader, new FileStream(Target, FileMode.Create), '\0');
            PdfSignatureAppearance objSA = objStamper.SignatureAppearance;
            try
            {
                if (AddVisibleSign)
                    objSA.SetVisibleSignature(new Rectangle(0, 0, 0, 0), 1, null);

                objSA.SignDate = DateTime.Now;
                objSA.SetCrypto(null, objChain, null, null);
                objSA.Reason = Reason;
                objSA.Location = Location;
                objSA.Acro6Layers = true;
                objSA.Render = PdfSignatureAppearance.SignatureRender.NameAndDescription;
                PdfSignature objSignature = new PdfSignature(PdfName.ADOBE_PPKMS, PdfName.ADBE_PKCS7_SHA1);
                objSignature.Date = new PdfDate(objSA.SignDate);
                objSignature.Name = PdfPKCS7.GetSubjectFields(objChain[0]).GetField("CN");
                if (objSA.Reason != null)
                    objSignature.Reason = objSA.Reason;
                if (objSA.Location != null)
                    objSignature.Location = objSA.Location;
                objSA.CryptoDictionary = objSignature;
                int intCSize = 4000;
                Hashtable objTable = new Hashtable();
                objTable[PdfName.CONTENTS] = intCSize * 2 + 2;
                if (!objSA.IsPreClosed())
                {
                    objSA.PreClose(objTable);
                }
                HashAlgorithm objSHA1 = new SHA1CryptoServiceProvider();

                Stream objStream = objSA.RangeStream;
                int intRead = 0;
                byte[] bytBuffer = new byte[8192];
                while ((intRead = objStream.Read(bytBuffer, 0, 8192)) > 0)
                    objSHA1.TransformBlock(bytBuffer, 0, intRead, bytBuffer, 0);
                objSHA1.TransformFinalBlock(bytBuffer, 0, 0);


                
                //byte[] bytPK = SignMsg(Certificate.GetCertHash(), Certificate, false);
                byte[] bytPK = SignMsg(objSHA1.Hash, Certificate, false);
                byte[] bytOut = new byte[intCSize];

                PdfDictionary objDict = new PdfDictionary();

                Array.Copy(bytPK, 0, bytOut, 0, bytPK.Length);

                objDict.Put(PdfName.CONTENTS, new PdfString(bytOut).SetHexWriting(true));
                objSA.Close(objDict);
            }
            catch (Exception ex)
            {
                RegistroTraza.I.Registrar(ex);
                throw;
            }
            finally
            {
            }
            
        }

        /// <summary>
        /// Crea la firma CMS/PKCS #7
        /// </summary>
        private static byte[] SignMsg(byte[] Message, SysX509.X509Certificate2 SignerCertificate, bool Detached)
        {
            ContentInfo contentInfo = new ContentInfo(Message);

            SignedCms objSignedCms = new SignedCms(contentInfo, Detached);

            CmsSigner objCmsSigner = new CmsSigner(SignerCertificate);

            objCmsSigner.IncludeOption = SysX509.X509IncludeOption.EndCertOnly;

            objSignedCms.ComputeSignature(objCmsSigner);

            return objSignedCms.Encode();
        }
    }
