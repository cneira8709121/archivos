using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Activation;
using Ruv.Infrastructure.Crosscutting.Common;

  [ServiceContract]
  public interface ILoginService
  {
    [OperationContract]
    [FaultContract(typeof(Ruv.WPF.Server.clsDefaultFaultContract))]
    clsUsuario Authenticate(string cuenta, string contraseña, clsInterfaseRed ir, string info);

    [OperationContract]
    void CerrarSesion(string nombreUsuario, string cuentaUsuario);
  }
