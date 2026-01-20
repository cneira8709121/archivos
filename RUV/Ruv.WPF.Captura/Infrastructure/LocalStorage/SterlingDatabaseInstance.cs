using System;
using System.Collections.Generic;
using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.Infrastructure.Crosscutting.Common.General;
using Ruv.WPF.Captura.Infrastructure.ColaProcesos;
using Ruv.WPF.Captura.Infrastructure.Configuracion;
using Wintellect.Sterling.Database;

namespace Ruv.WPF.Captura.Infrastructure.LocalStorage
{
    public class SterlingDatabaseInstance : BaseDatabaseInstance
    {
        protected override List<ITableDefinition> RegisterTables()
        {
            return new List<ITableDefinition>
          { 
            CreateTableDefinition<clsUsuario, String>(x => x.Cuenta),
            CreateTableDefinition<clsProceso, string>(x => x.Id),
            CreateTableDefinition<clsConfiguracion, int>(x => x.Id),
            CreateTableDefinition<clsPoblacion, int>(x => x.Id)
              .WithIndex<clsPoblacion,int, int, int>("MunicipioTipoPoblacion",
              x => Tuple.Create(x.MunicipioId, (int)x.TipoPoblacion))
          };
        }

        public override string Name
        {
            get { return "Ruv.WPF.DatabaseInstance"; }
        }
    }
}