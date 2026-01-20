using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ruv.Infrastructure.Crosscutting.Common.Entidades.GestionFormulario;

namespace Ruv.Business.GestionFormulario.Contratos
{
    public interface IGetFormulario
    {
        clsFormulario ObtenerFormulario(string cNumeroFormulario, ref string cError);
    }
}
