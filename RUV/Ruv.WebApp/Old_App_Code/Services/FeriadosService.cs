using System;
using System.ServiceModel.Activation;
using Ruv.Business.Feriados.Contratos;
using Ruv.Business.DTO.Feriado;
using System.Collections.Generic;
using Ruv.Business.Feriados;

[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
public class FeriadosService : IFeriadosService
{
    public int? CreacionFestivo(DateTime fecha, string nombre, string descripcion, bool recurrente, ref string cError)
    {
        IFeriadosBusiness iFeriados = new FeriadosBusiness();
        return iFeriados.CreacionFestivo(fecha, nombre, descripcion, recurrente, ref cError);
    }

    public void BorrarFestivo(int idFestivo, ref string cError)
    {
        IFeriadosBusiness iFeriados = new FeriadosBusiness();
        iFeriados.BorrarFestivo(idFestivo, ref cError);
    }

    public DateTime? CalcularDiasHabiles(DateTime fecha, int numeroDias, bool contarCero, ref string cError)
    {
        IFeriadosBusiness iFeriados = new FeriadosBusiness();
        return iFeriados.CalcularDiasHabiles(fecha, numeroDias, contarCero, ref cError);
    }


    public List<Feriado> ConsultarFestivos(int ano, ref string cError)
    {
        IFeriadosBusiness iFeriados = new FeriadosBusiness();
        return iFeriados.ConsultarFestivos(ano, ref cError);
    }
}
