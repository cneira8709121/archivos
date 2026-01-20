using System;
using System.Collections.Generic;
using Ruv.Business.DTO.Feriado;
using Ruv.Business.Feriados.Contratos;
using Ruv.Data.Feriados.Contratos;
using Ruv.Infrastructure.Crosscutting.Resources.Dependencias;
using u = Ruv.Infrastructure.Crosscutting.Utilities;

namespace Ruv.Business.Feriados
{
    public class FeriadosBusiness : IFeriadosBusiness
    {
        public int? CreacionFestivo(DateTime fecha, string nombre, string descripcion, bool recurrente, ref string cError)
        {
            IGestionFeriados iFeriados = (IGestionFeriados)u::Spring.GetService(Objetos.FeriadosData);
            return iFeriados.CreacionFestivo(fecha, nombre, descripcion, recurrente, ref cError);
        }

        public void BorrarFestivo(int idFestivo, ref string cError)
        {
            IGestionFeriados iFeriados = (IGestionFeriados)u::Spring.GetService(Objetos.FeriadosData);
            iFeriados.BorrarFestivo(idFestivo, ref cError);
        }

        public DateTime? CalcularDiasHabiles(DateTime fecha, int numeroDias, bool contarCero, ref string cError)
        {
            IGestionFeriados iFeriados = (IGestionFeriados)u::Spring.GetService(Objetos.FeriadosData);
            return iFeriados.CalcularDiasHabiles(fecha, numeroDias, contarCero, ref cError);
        }

        public List<Feriado> ConsultarFestivos(int ano, ref string cError)
        {
            IGestionFeriados iFeriados = (IGestionFeriados)u::Spring.GetService(Objetos.FeriadosData);
            return iFeriados.ConsultarFestivos(ano, ref cError);
        }
    }
}
