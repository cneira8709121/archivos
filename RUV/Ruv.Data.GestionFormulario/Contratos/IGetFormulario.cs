using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ruv.Business.DTO.GestionFormulario;

namespace Ruv.Data.GestionFormulario.Contratos
{
    public interface IGetFormulario
    {
        clsFormulario ObtenerFormulario(string cNumeroFormulario, ref string cError);
    }
}
