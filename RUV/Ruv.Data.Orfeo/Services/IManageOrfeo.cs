using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using Ruv.Business.DTO.Orfeo;

namespace Ruv.Data.Orfeo.Services
{
    public interface IManageOrfeo
    {
        /// <summary>
        /// Inserta el dignatario dado y devuelve una secuencia
        /// </summary>
        /// <param name="dig">Dignatario a insertar</param>
        /// <param name="cError"></param>
        /// <returns>Secuencia generada por el servicio</returns>
        Secuencia InsertaDignatario(Dignatario dig, ref string cError);
        /// <summary>
        /// Inserta el radicado dado y devuelve una secuencia
        /// </summary>
        /// <param name="dig">Radicado a insertar</param>
        /// <param name="cError"></param>
        /// <returns>Secuencia generada por el servicio</returns>
        Secuencia InsertaRadicado(Radicado rad, ref string cError);
        /// <summary>
        /// Inserta la dirección dada y devuelve una secuencia
        /// </summary>
        /// <param name="dig">Dirección a insertar</param>
        /// <param name="cError"></param>
        /// <returns>Secuencia generada por el servicio</returns>
        Secuencia InsertaDireccion(Direccion dir, ref string cError);
        /// <summary>
        /// Inserta el evento dado y devuelve una secuencia
        /// </summary>
        /// <param name="dig">Evento a insertar</param>
        /// <param name="cError"></param>
        /// <returns>Secuencia generada por el servicio</returns>
        Secuencia InsertaEvento(Evento evt, ref string cError);

        /// <summary>
        /// Vincula el archivo cargado con su correspondiente codigo ORFEO
        /// </summary>
        /// <param name="rutaArchivo">Ruta remota del archivo cargado</param>
        /// <param name="codigoRadicacion">Codigo de Radicación ORFEO</param>
        /// <returns></returns>
        Secuencia VincularArchivoCargado(string rutaArchivo, string numeroRadicado);

        /// <summary>
        /// Obtiene el nombre temporal que tendrá el adjunto en el servidor remoto
        /// </summary>
        /// <param name="numeroRadicado">Numero de radicado ORFEO</param>
        /// <param name="nombreArchivo">Nombre actual del archivo</param>
        /// <returns></returns>
        Resultado ObtenerNombreAnexo(string numeroRadicado, string nombreArchivo);

        /// <summary>
        /// Carga el archivo al servidor remoto
        /// </summary>
        /// <param name="numeroRadicado">Numero de radicado ORFEO</param>
        /// <param name="base64EncodedBytes">Archivo a cargar, en bytes codificados a string en base 64</param>
        /// <param name="nombreArchivo">Nombre temporal que tendrá el adjunto en el servidor remoti</param>
        /// <returns>Ruta final del archivo cargado en el servidor remoto</returns>
        string CargarArchivoRemoto(string numeroRadicado, string base64EncodedBytes, string nombreArchivo);

        /// <summary>
        /// Registra el evento de carga de archivo
        /// </summary>
        /// <param name="rutaArchivo">Ruta en el servidor remoto donde reside el archivo cargado</param>
        /// <param name="numeroPaginas">Numero de páginas incluidas</param>
        /// <param name="numeroRadicado">Numero de radicado ORFEO</param>
        /// <param name="usuarioDigitalizador">Nombre de usuario digitalizador</param>
        /// <returns></returns>
        Resultado RegistrarEventoCargaArchivo(string rutaArchivo, int numeroPaginas, string numeroRadicado, string usuarioDigitalizador);

        bool RelacionarOrfeoValoracion(string cOrfeo, int nValoracion, DbTransaction tra, ref string cError);

        /// <summary>
        /// Obtiene el codigo orfeo que se relaciona con una valoración
        /// </summary>
        /// <param name="idValoracion">Id de valoración</param>
        /// <param name="cError"></param>
        /// <returns>Codigo orfeo relacionado con la valoración</returns>
        string ObtenerCodigoOrfeoPorIdVal(int idValoracion, ref string cError);
    }
}
