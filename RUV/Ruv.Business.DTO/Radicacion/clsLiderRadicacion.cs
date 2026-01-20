using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ruv.Business.DTO.GestionFormulario;

namespace Ruv.Business.DTO.Radicacion
{
    public class clsLiderRadicacion
    {
        public clsRadicacion RadActual { get; set; }
        public clsRadicacion RadExistente { get; set; }
        public clsFormulario FrmRadicacionPrevia { get; set; }
    }
}
