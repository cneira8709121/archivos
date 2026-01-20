using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Ruv.Infrastructure.Crosscutting.Common;

namespace Ruv.WPF.Data
{
    public class clsAutenticador
    {

        public clsUsuario ValidarCredenciales(string userName, string password, clsInterfaseRed ir)
        {
            clsUsuario Output = null;

            // Validarlo contra la base de datos.
            DataSet DS = null;
            Cryptography.Cryptography.Encrypt oEncrypt = new Cryptography.Cryptography.Encrypt();
            string EncriptedPassword = oEncrypt.EncryptData(password);

            Ruv.WPF.Data.clsDB db = new clsDB();

            // Registrar la información de red del usuario.
            string InterfaseRed = null;
            string Ip = null;
            if (ir != null)
            {
                Ip = ir.Ips;
                InterfaseRed = string.Format("{0} {1} {2}",
                  ir.Mac,
                  ir.PcName,
                  ir.Dns);
                if (InterfaseRed.Any())
                    InterfaseRed = InterfaseRed.Substring(0, Math.Min(InterfaseRed.Length, 50));
            }

            DS = db.ExecuteDataSet(clsComando.Paquete + "AUTENTICARUSUARIO", userName, EncriptedPassword, InterfaseRed, Ip, null);

            if (DS.Tables[0].AsEnumerable().Any())
            {
                DataRow DR = DS.Tables[0].Rows[0];
                ResultadoAutenticacion =
                  (eCodigoAutenticacion)Convert.ToInt32(DR["RESULTADO"]);
                MensajeAutenticacion = Convert.ToString(DR["MENSAJE"]);

                Output = new clsUsuario { MensajeAutenticacionFallida = Convert.ToString(DR["MENSAJE"]) };
            }

            if (ResultadoAutenticacion == eCodigoAutenticacion.AutenticacionExitosa)
            {
                DataRow DR = DS.Tables[0].Rows[0];

                // Crear el objeto del usuario con los permisos.
                Output = new clsUsuario()
                {
                    Id = Convert.ToInt32(DR["ID"]),
                    Nombre = Convert.ToString(DR["NOMBRE"]),
                    Contraseña = password,
                    Cuenta = Convert.ToString(DR["CUENTA"]),
                    NumeroDocumento = Convert.ToString(DR["IDENTIFICACION"]),
                    Cargo = Convert.ToString(DR["CARGO"]),
                    UtilizaCertificadoDigital = Convert.ToInt32(DR["FIRMADIGITAL"]) == 1,
                    UnidadTerritorialId = Convert.ToInt32(DR["UNIDADTERRITORIAL"]),
                    ID_DEPARTAMENTO = Convert.ToInt32(DR["ID_DEPARTAMENTO"]),
                    ID_MUNICIPIO = Convert.ToInt32(DR["ID_MUNICIPIO"]),
                    ID_PAIS = (DR["ID_PAIS"] != DBNull.Value) ? Convert.ToInt32(DR["ID_PAIS"]) : (int)ePaises.Colombia,
                    ID_ENTIDADMUNICIPIO = (DR["ID_ENTIDADMUNICIPIO"] != DBNull.Value) ? Convert.ToInt32(DR["ID_ENTIDADMUNICIPIO"]) : 0,
                    ImagenFirmaDigital = DR["IMAGENFIRMADIGITAL"] != DBNull.Value ? DR["IMAGENFIRMADIGITAL"] as byte[] : null
                };

                //DS.Tables[0].AsEnumerable().ToList().ForEach(
                //  x => Output.Permisos.Add((ePermisosUsuario)x.Field<decimal>("PERMISO")));

                DS.Tables[0].AsEnumerable().ToList().ForEach(
                  x => AddPermiso(x.Field<decimal?>("PERMISO"), ref Output));

                List<decimal?> lstRoles = new List<decimal?>();
                DS.Tables[0].AsEnumerable().ToList().ForEach(x => lstRoles.Add(x.Field<decimal?>("ID_ROL")));
                lstRoles.Distinct().ToList().ForEach(x => AddRoles(x, ref Output));


            }

            return Output;
        }

        private void AddPermiso(decimal? permiso, ref clsUsuario Output)
        {
            if (Enum.IsDefined(typeof(ePermisosUsuario), (permiso != null) ? (int)permiso : 0)) //Enum.IsDefined(typeof(ePermisosUsuario), permiso))
            {
                ePermisosUsuario ePermiso = (ePermisosUsuario)permiso;
                Output.Permisos.Add(ePermiso);
            }
        }

        private void AddRoles(decimal? Rol, ref clsUsuario Output)
        {

            if (Enum.IsDefined(typeof(eRolesUsuario), (Rol != null) ? (int)Rol : 0))
            {
                eRolesUsuario eRoles = (eRolesUsuario)Rol;
                Output.RolesUsuario.Add(eRoles);
            }

        }

        /// <summary>
        /// El código del proceso de autenticación.
        /// </summary>
        public eCodigoAutenticacion ResultadoAutenticacion { get; set; }

        /// <summary>
        /// Mensaje del proceso de autenticación
        /// </summary>
        public string MensajeAutenticacion { get; set; }

        /// <summary>
        /// Cierra la sesión del usuario  
        /// </summary>
        /// <param name="userName"></param>
        public void CerrarSesion(string userName)
        {
            Ruv.WPF.Data.clsDB db = new clsDB();
            db.ExecutenonQuery(clsComando.Paquete + "CERRARSESION", userName);
        }
    }
}
