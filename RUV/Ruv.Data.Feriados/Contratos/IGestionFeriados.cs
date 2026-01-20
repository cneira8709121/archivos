using System;
using System.Collections.Generic;
using Ruv.Business.DTO.Feriado;

namespace Ruv.Data.Feriados.Contratos
{
    public interface IGestionFeriados
    {

        int? CreacionFestivo(DateTime fecha, string nombre, string descripcion, bool recurrente, ref string cError);

        void BorrarFestivo(int idFestivo, ref string cError);

        /// <summary>
        /// Calcula los días habiles a partir de una fecha inicial
        /// </summary>
        /// <param name="fecha">Fecha Inicial</param>
        /// <param name="numeroDias">Numero de días a contar</param>
        /// <param name="contarCero">Usar el siguiente día como dia "cero" para iniciar el conteo</param>
        /// <param name="cError"></param>
        /// <returns></returns>
        DateTime? CalcularDiasHabiles(DateTime fecha, int numeroDias, bool contarCero, ref string cError);

        List<Feriado> ConsultarFestivos(int ano, ref string cError);

    }
}
