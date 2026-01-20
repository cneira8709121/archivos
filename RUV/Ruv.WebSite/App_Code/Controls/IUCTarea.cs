using System;

namespace Ruv.WebSite.Utilidades.Controles {
    
    public interface IUCTarea {

        string ID { get; set; }
        string Formulario { set; }
        string Estado { set; }
        DateTime Fecha { set; }
        int IdDeclaracion { get; set; }
        int? IdCorreccion { get; set; }

    }

}