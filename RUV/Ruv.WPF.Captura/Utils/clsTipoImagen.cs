using Ruv.WPF.Captura.Recursos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;

namespace Ruv.WPF.Captura.Utils
{
    public class clsTipoImagen
    {       
        /// <summary>
        /// Valida si el tipo de archivo seleccionado es valido para guardarlo 
        /// Solo acepta imagenes y PDF
        /// </summary>
        /// <param name="archivo">Archivo validado</param>
        /// <returns>Retorna True si es valido o false en caso contrario </returns>
        public static Boolean validaExtensionImagen(string archivo)
        {
            Boolean resultado = false;
            try
            {
                string extension = System.IO.Path.GetExtension(archivo);
                string[] listaTiposArchivosValidos = Convert.ToString(ExtensionesImagenes.extensionImagenes).Split(',');
                if (listaTiposArchivosValidos.ToList().Contains(extension.ToLower()))
                {
                    resultado = true;
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al leer el archivo de recursos ExtensionesImagenes.resx", ex);
            }
            
        }
    }
}
