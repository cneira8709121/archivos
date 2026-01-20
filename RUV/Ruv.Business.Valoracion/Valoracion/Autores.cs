using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ruv.Data.Valoracion.Valoracion;
using Ruv.Data;
using Ruv.Infrastructure.Crosscutting.Common.Valoracion;
using System.Data.Common;

namespace Ruv.Business.Valoracion.Valoracion
{
    public class Autores
    {
        public static void Insertar(int AutorId, int ValAnexoId, DbTransaction tra)
        {
            entAutor objAutor = new entAutor();
            //TBAUTORHV_VAL_ANEXO autorAnexo = new TBAUTORHV_VAL_ANEXO();
            //ParseViewToData(AutorId, ValAnexoId, ref autorAnexo);
            objAutor.Insertar(AutorId, ValAnexoId, tra);
        }
        /*
        private static void ParseViewToData(int AutorId, int ValAnexoId, ref TBAUTORHV_VAL_ANEXO data)
        {
            data.ID_AUTORHV = AutorId;
            data.ID_VAL_ANEXO_PERSONA = ValAnexoId;
        }*/

        private static void ParseDataToView(TBAUTORHV data, ref clsAutores view)
        {
            view.Id = data.ID;
            view.Nombre = data.NOMBRE;
            view.FechaCreacion = data.FECHA_CREACION;
            view.FechaDesmovilizacion = data.FECHA_DESMOVILIZACION;
        }

        public static void Eliminar(int ValAnexoId, DbTransaction tra) 
        {
            entAutor objAutor = new entAutor();
            objAutor.Eliminar(ValAnexoId, tra);
        }

        public static List<clsAutores> GetAutores()
        {
            List<clsAutores> autores = new List<clsAutores>();
            entAutor objAutor = new entAutor();
            List<TBAUTORHV> autoresData = objAutor.GetAutores();
            foreach (TBAUTORHV data in autoresData)
            {
                clsAutores view = new clsAutores();
                ParseDataToView(data, ref view);
                autores.Add(view);
            }
            return autores;
        }

        public static List<clsAutores> GetAutores(int valAnexoPerId)
        {
            List<clsAutores> autores = new List<clsAutores>();
            entAutor objAutor = new entAutor();
            List<TBAUTORHV> autoresData = objAutor.GetAutoresPorValAnexoPersona(valAnexoPerId);
            foreach (TBAUTORHV data in autoresData)
            {
                clsAutores view = new clsAutores();
                ParseDataToView(data, ref view);
                autores.Add(view);
            }
            return autores;
        }
    }
}
