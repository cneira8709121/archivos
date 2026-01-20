
namespace Ruv.WPF.Captura.Impresion
{
    interface ILogicaImpresion
    {
        clsInformacionImpresion ProcesarImpresion(object fuenteDatos = null);
        void ImpresionIniciar();
        void PasarPagina();
        object ObtenerBloque(int indice);
        string NombreEntidad { get; }
    }
}
