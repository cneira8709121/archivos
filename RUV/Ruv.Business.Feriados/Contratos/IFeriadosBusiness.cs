using System;
using System.Collections.Generic;
using Ruv.Business.DTO.Feriado;

namespace Ruv.Business.Feriados.Contratos
{
    public interface IFeriadosBusiness
    {
        int? CreacionFestivo(DateTime fecha, string nombre, string descripcion, bool recurrente, ref string cError);

        void BorrarFestivo(int idFestivo, ref string cError);

        DateTime? CalcularDiasHabiles(DateTime fecha, int numeroDias, bool contarCero, ref string cError);

        List<Feriado> ConsultarFestivos(int ano, ref string cError);
    }
}
