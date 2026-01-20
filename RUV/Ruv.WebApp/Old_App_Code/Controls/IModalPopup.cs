using Ruv.Infrastructure.Crosscutting.Common;

namespace Ruv.WebApp.Utilidades.Controles {
    
    public interface IModalPopUp {

        void Ocultar();

        void MostrarMensaje(string titulo, string mensaje);

        void MostrarMensajeYRedirigir(string titulo, string mensaje, string urlRedireccion);

        void MostrarMensajeConfirmacion(string titulo, string mensaje, OnBtnClick eventoAceptar);

    }

}