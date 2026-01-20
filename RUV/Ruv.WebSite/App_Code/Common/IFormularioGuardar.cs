using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.Infrastructure.Crosscutting.Common.Valoracion;

/// <summary>
/// Summary description for IFormularioGuardar
/// </summary>
public interface IFormularioGuardar
{
    bool Guardar(eEstadosValoracion estado, bool finalizar = false);
    void ShowMessage(string sMessage);
    //clsValoracion Valoracion { get; set; }

}