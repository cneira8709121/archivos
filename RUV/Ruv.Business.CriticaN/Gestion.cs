using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using Ruv.Infrastructure.Crosscutting.Utilities;
using Ruv.Infrastructure.Crosscutting.Resources.Dependencias;
using dal = Ruv.Data.CriticaN;
using Ruv.Business.DTO.CriticaN;
using System.Data.Common;
using static System.Net.Mime.MediaTypeNames;
using System.IO;

namespace Ruv.Business.CriticaN
{
    public class Gestion : Contratos.ICriticaN
    {
        #region Public methods

        #region Services implementation

        public byte[] ObtenerImagenRadicacion(long nId, ref string cNombreImagen, ref string cError)
        {
            string idRadicado = nId.ToString();
            string[] extensiones = { ".tif", ".tiff", ".pdf", ".jpg", ".doc", ".bpm", ".gif", ".htm", ".docx", ".gif", ".jpeg", ".msg", ".png", ".pptx", ".rar", ".txt", ".zip" };
            string archivoRadicacion = string.Empty;
            string directorio = ConfigurationManager.AppSettings["PathArchivosRadicacion"].ToString();
            string tmpDirectory = Path.Combine(Path.GetTempPath(), "Radicacion_" + idRadicado);
            string zipfile = Path.Combine(Path.GetTempPath(), "Radicacion_" + idRadicado + ".zip");
            Dictionary<string, FileInfo> keyValuePairs = new Dictionary<string, FileInfo>();
            Directory.CreateDirectory(tmpDirectory);
            foreach (var item in extensiones)
            {
                archivoRadicacion = Path.Combine(directorio, idRadicado + item);
                if (File.Exists(archivoRadicacion))
                {
                    File.Copy(archivoRadicacion, Path.Combine(tmpDirectory, Path.GetFileName(archivoRadicacion)), true);
                }

                if (File.Exists(Path.Combine(tmpDirectory, Path.GetFileName(archivoRadicacion))) && File.Exists(archivoRadicacion))
                {
                    keyValuePairs.Add(archivoRadicacion, new FileInfo(archivoRadicacion));
                }
            }
            cNombreImagen = zipfile;
            var resultado = FileHelper.CompressFiles(keyValuePairs);
            return resultado;
            
            /*
            cNombreImagen = FileHelper.GetFileName(ConfigurationManager.AppSettings["PathArchivosRadicacion"], nId.ToString(), ref cError);

            if (!string.IsNullOrEmpty(cError)) return null;
            string cNombreImagenCompleta = string.Format(@"{0}{1}", ConfigurationManager.AppSettings["PathArchivosRadicacion"], cNombreImagen);

            return FileHelper.ReadFile(cNombreImagenCompleta, ref cError);*/
        }

        public bool InsertaCriticaN(List<clsRespuestaCritica> lstRespuesta, ref string cError)
        {
            using (DbTransaction tra = Ruv.Data.Dao.InitTransaction())
            {
                dal::Contratos.ICriticaN iGestion = (dal::Contratos.ICriticaN)Spring.GetService(Objetos.CriticaNData);
                if (iGestion.GuardarValidacion(lstRespuesta, tra, ref cError) && string.IsNullOrEmpty(cError))
                {
                    tra.Commit();
                    return true;
                }
                tra.Rollback();
                return false;
            }
        }

        #endregion

        #endregion
    }
}
