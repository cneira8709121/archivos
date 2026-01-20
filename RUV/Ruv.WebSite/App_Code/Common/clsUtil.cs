using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using Ionic.Zip;

/// <summary>
/// Descripción breve de clsUtil
/// </summary>
public class clsUtil
{
	public clsUtil()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}

  /// <summary>
  /// Graba un archivo serializado, comprimido y con clave.
  /// Solo se debe guardar la Primera vez
  /// </summary>
  /// <typeparam name="T1"></typeparam>
  /// <param name="rutaArchivo"></param>
  /// <param name="objeto"></param>
  public void GrabarArchivoSerializado<T1>(string rutaArchivo, T1 objeto, string ClaveZip, Boolean Sobreescribir) where T1 : class
  {
    //Si no existe el archivo se guarda en el servidor
    //Si ya existe se pasa por alto y no se hace nada
    if (!System.IO.File.Exists(rutaArchivo) || Sobreescribir)
    {

      System.Xml.Serialization.XmlSerializer Serializador =
                 new System.Xml.Serialization.XmlSerializer(objeto.GetType());

      string ArchivoTemp = rutaArchivo + ".tmp";

      if (System.IO.File.Exists(rutaArchivo))
        System.IO.File.Delete(rutaArchivo);

      if (System.IO.File.Exists(ArchivoTemp))
        System.IO.File.Delete(ArchivoTemp);


      using (StreamWriter SW = System.IO.File.CreateText(ArchivoTemp))
      {
        Serializador.Serialize(SW, objeto);
      }

      using (ZipFile zip = new ZipFile())
      {
        zip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression;
        zip.Encryption = EncryptionAlgorithm.WinZipAes256;
        zip.Password = ClaveZip;
        zip.AddFile(ArchivoTemp);
        zip.Save(rutaArchivo);
      }

      if (System.IO.File.Exists(ArchivoTemp))
        System.IO.File.Delete(ArchivoTemp);
    }
  }
}