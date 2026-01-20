using System;
using System.ServiceModel;
using Ruv.Business.DTO.Feriado;
using System.Collections.Generic;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IFeriadosService" in both code and config file together.
[ServiceContract]
public interface IFeriadosService
{

    [OperationContract]
    int? CreacionFestivo(DateTime fecha, string nombre, string descripcion, bool recurrente, ref string cError);

    [OperationContract]
    void BorrarFestivo(int idFestivo, ref string cError);

    [OperationContract]
    DateTime? CalcularDiasHabiles(DateTime fecha, int numeroDias, bool contarCero, ref string cError);

    [OperationContract]
    List<Feriado> ConsultarFestivos(int ano, ref string cError);
}
