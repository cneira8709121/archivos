using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;

using Ruv.Data;
using Ruv.Data.Reconocimiento;
using System.Data.Common;
using System.Configuration;

namespace Ruv.Business.Captura.Declaracion
{
    public class InfoDeclaracion
    {
        #region Guardar
        public static void Guardar(ref clsDeclaracion declaracionView, DbTransaction tran)
        {
            //Guardar info de la declaracion
            TBDECLARACIONES declaracionData = new TBDECLARACIONES();

            InfoDeclaracion.ParseViewToData(declaracionView, ref declaracionData);


            if (declaracionData.PARAM_ESTADO == (int?)eEstadoDeclaracion.FinalizaCapturaSinRadicar) // FAILSAFE: El estado 696 está deprecado
                declaracionData.PARAM_ESTADO = (int?)eEstadoDeclaracion.ValoracionPendientePorAsignar;



            Ruv.Data.Reconocimiento.entDeclaraciones entDecl = new entDeclaraciones();
            switch (declaracionView.EstadoRegistro)
            {
                case eEstadoRegistro.Insertar:
                    //Insertar nueva declaracion
                    entDecl.setDeclaraciones(declaracionData, (int)declaracionView.RadicacionId, tran);
                    declaracionView.ID = declaracionData.ID;
                    declaracionView.DeclaracionNumero = declaracionData.NUMEROFORMULARIO;
                    break;
                case eEstadoRegistro.Modificado:
                    entDecl.updDeclaraciones(declaracionData, (int)declaracionView.RadicacionId, tran);
                    break;
                case eEstadoRegistro.Eliminado:
                    break;
                case eEstadoRegistro.SinModificaciones:
                    break;
            }

            #region Narracion - HOJA 3
            try
            {
                Narracion.Guardar(declaracionView, tran);
            }
            catch (Exception ex)
            {
                throw new ExceptionRuv("Error Guardando Narración de la declaración: " + Environment.NewLine + ex.Message);
            }
            finally
            {
                //Reiniciar EstadoRegistro
                if (declaracionView.EstadoRegistro != eEstadoRegistro.Eliminado)
                    declaracionView.EstadoRegistro = eEstadoRegistro.SinModificaciones;
            }
            #endregion

            #region Encargado Declaración
            try
            {
                int? old_id_encargado = declaracionView.TomaDeclaracion.Encargado.ID;
                Encargado.Guardar(declaracionData, ref declaracionView, tran);

                //Si cambio ID del encargado hay que actualizar la declaracion
                if (declaracionView.TomaDeclaracion.Encargado.ID != old_id_encargado)
                {
                    //Actualizar ID del encargado en la declaracion
                    declaracionData.ID_ENCARGADO = declaracionView.TomaDeclaracion.Encargado.ID;
                    // TODO: jairovg - Al actualizar la declaración siendo una nueva, la deja en estado 696 y no 702.
                    entDecl.updDeclaraciones(declaracionData, (int)declaracionView.RadicacionId, tran);
                }
            }
            catch (Exception ex)
            {
                throw new ExceptionRuv("Error Guardando Información del encargado: " + Environment.NewLine + ex.Message);
            }
            #endregion
        }

        public static void ParseViewToData(clsDeclaracion declaracionView, ref TBDECLARACIONES declaracionData)
        {
            declaracionData.ID = declaracionView.ID ?? -1;

            //H1:
            /*
            //H1-01
            declaracionData.ID_DEPARTAMENTODECLARACION = (short)(declaracionView.TomaDeclaracion.LugarDeclaracionDepartamento ?? (int)Common.ThrowException("Debe declarar Lugar Declaracion Departamento"));
            declaracionData.ID_MUNICIPIODECLARACION = (short)(declaracionView.TomaDeclaracion.LugarDeclaracionMunicipio ?? (int)Common.ThrowException("Debe declarar Lugar Declaracion Municipio"));

            //H1-02
            declaracionData.PARAM_ENTIDADATIENDE = declaracionView.TomaDeclaracion.EntidadQueAtiende ?? (int)Common.ThrowException("Debe declarar EntidadQueAtiende");
            //H1-03
            declaracionData.FECHADECLARACION = declaracionView.TomaDeclaracion.FechaDeclaracion ?? (DateTime)Common.ThrowException("Debe declarar FECHADECLARACION");
            */
            //General
            declaracionData.VERSION_FUD = Common.ParseIntToShortNullable(declaracionView.VersionFUD);

            //H1-01
            declaracionData.ID_PAISDECLARACION = Common.ParseIntToLongNullable(declaracionView.TomaDeclaracion.LugarDeclaracionPais);
            declaracionData.ID_DEPARTAMENTODECLARACION = Common.ParseIntToLongNullable(declaracionView.TomaDeclaracion.LugarDeclaracionDepartamento);
            declaracionData.ID_MUNICIPIODECLARACION = Common.ParseIntToLongNullable(declaracionView.TomaDeclaracion.LugarDeclaracionMunicipio);
            //H1-02
            declaracionData.ID_ENTIDADMUNICIPIODECLARACION = Common.ParseIntToShortNullable(declaracionView.TomaDeclaracion.LugarDeclaracionEntidadMunicipio);
            declaracionData.PARAM_ENTIDADATIENDE = declaracionView.TomaDeclaracion.EntidadQueAtiende;
            //H1-03
            declaracionData.FECHADECLARACION = declaracionView.TomaDeclaracion.FechaDeclaracion;


            //H1 entre 09 y 10
            declaracionData.MENSAJE_CELULAR = Common.ParseIntToShortNullable(declaracionView.TomaDeclaracion.MedioDeContactoMensajeVoz);
            declaracionData.MENSAJE_CORREOE = Common.ParseIntToShortNullable(declaracionView.TomaDeclaracion.MedioDeContactoCorreoElectronico);
            declaracionData.MENSAJE_FIJO = Common.ParseIntToShortNullable(declaracionView.TomaDeclaracion.MedioDeContactoMensajeVoz);
            declaracionData.OTRO = declaracionView.TomaDeclaracion.MedioDeContactoOtro;

            //H1-10
            declaracionData.OTROHECHO = declaracionView.TomaDeclaracion.HechosOtrosCual;

            //H4:
            //H4-25
            declaracionData.CORREGIRDECLARACION = Common.ParseIntToShortNullable(declaracionView.VerificacionProcedimiento.EnmendarDeclaracion);
            declaracionData.QUECORRECCIONES = declaracionView.VerificacionProcedimiento.EnmendarDeclaracionTexto;
            //H4-26
            declaracionData.DOCUMENTOSADICIONALES = Common.ParseIntToShortNullable(declaracionView.VerificacionProcedimiento.NumeroTotalSoportes);
            declaracionData.CUANTOSFOLIOS = Common.ParseIntToShortNullable(declaracionView.VerificacionProcedimiento.NumeroTotalFolios);
            declaracionData.CUANTOS_ANEXOS = declaracionView.VerificacionProcedimiento.NumeroTotalAnexos;
            //H4-27 a 31
            declaracionData.ENTREVISTAPREVIA = Common.ParseIntToShortNullable(declaracionView.VerificacionProcedimiento.RealizoEntrevistaPrevia);
            declaracionData.REALIZOJURAMENTO = Common.ParseIntToShortNullable(declaracionView.VerificacionProcedimiento.RealizoTomaJuramento);
            
            declaracionData.LEYODECLARACION = Common.ParseIntToShortNullable(declaracionView.VerificacionProcedimiento.LeyoAlDeclaranteLaDeclaracion);
            declaracionData.ORIENTACIONENMENDADURAS = Common.ParseIntToShortNullable(declaracionView.VerificacionProcedimiento.HuboOrientacionParaCorregir);
            declaracionData.TIENEENMENDADURAS = Common.ParseIntToShortNullable(declaracionView.VerificacionProcedimiento.SeIncluyeronCorrecciones);
            //H4-32
            declaracionData.OBSERVACIONES = declaracionView.VerificacionProcedimiento.ObservacionesSobreDiligenciamiento;
            //H4-33
            declaracionData.USODATOSPERSONALES = Common.ParseIntToShortNullable(declaracionView.VerificacionProcedimiento.UsoDatosPersonales);
            //H4-34 y 36
            declaracionData.SABE_FIRMAR = Common.ParseIntToShortNullable(declaracionView.VerificacionProcedimiento.DeclaranteSabeFirmar);
            declaracionData.CARGO = declaracionView.VerificacionProcedimiento.FuncionarioCargo;
            declaracionData.FUNCIONARIO = declaracionView.VerificacionProcedimiento.FuncionarioNombre;
            declaracionData.IDENTIFICACIONFUNCIONARIO = declaracionView.VerificacionProcedimiento.FuncionarioDocumentoIdentidad;

            //Estado Declaración.
            declaracionData.PARAM_ESTADO = (int)declaracionView.EstadoDeclaracion;

            //Numer Formulario Radicacion.
            declaracionData.NUMEROFORMULARIO = declaracionView.DeclaracionNumero;

            declaracionData.ID_USUARIO = declaracionView.UsuarioId;
            declaracionData.ID_UTERRITORIAL = declaracionView.UnidadTerritorialId;

            declaracionData.NUMEROSOPORTESOTROS = declaracionView.VerificacionProcedimiento.NumeroTotalSoportesOtros;
            declaracionData.NUMEROSOPORTESOTROSDESC = declaracionView.VerificacionProcedimiento.NumeroTotalSoportesOtrosDesc;

        }
        #endregion

        #region Obtener Datos
        public static void Obtener(ref clsDeclaracion declaracionView)
        {
            //Obtener iformacion de la declaracion
            entDeclaraciones entDecl = new entDeclaraciones();

            TBDECLARACIONES declaracionData = entDecl.getDeclaraciones((int)declaracionView.ID);

            //Reiniciar EstadoRegistro
            declaracionView.EstadoRegistro = eEstadoRegistro.SinModificaciones;

            declaracionView.UsuarioId = declaracionData.ID_USUARIO;

            InfoDeclaracion.ParseDataToView(declaracionData, ref declaracionView);

            //Obtener Encargado
            if (declaracionData.ID_ENCARGADO != null)
                Encargado.Obtener(declaracionData, (int)declaracionData.ID_ENCARGADO, ref declaracionView);
        }

        public static void ParseDataToView(TBDECLARACIONES declaracionData, ref clsDeclaracion declaracionView)
        {

            declaracionView.ID = declaracionData.ID;
            declaracionView.VersionFUD = declaracionData.VERSION_FUD.HasValue ? declaracionData.VERSION_FUD.Value : 1;

            //H1-1
            if (declaracionView.TomaDeclaracion == null)
                declaracionView.TomaDeclaracion = new clsTomaDeclaracion();
            declaracionView.TomaDeclaracion.LugarDeclaracionPais = declaracionData.ID_PAISDECLARACION;
            declaracionView.TomaDeclaracion.LugarDeclaracionDepartamento = declaracionData.ID_DEPARTAMENTODECLARACION;
            declaracionView.TomaDeclaracion.LugarDeclaracionMunicipio = declaracionData.ID_MUNICIPIODECLARACION;

            //H1-2
            declaracionView.TomaDeclaracion.LugarDeclaracionEntidadMunicipio = (declaracionData.ID_ENTIDADMUNICIPIODECLARACION.HasValue) ? (short?)declaracionData.ID_ENTIDADMUNICIPIODECLARACION.Value : null;
            declaracionView.TomaDeclaracion.EntidadQueAtiende = declaracionData.PARAM_ENTIDADATIENDE;
            //H1-3
            declaracionView.TomaDeclaracion.FechaDeclaracion = declaracionData.FECHADECLARACION;


            //H4:
            if (declaracionView.VerificacionProcedimiento == null)
                declaracionView.VerificacionProcedimiento = new clsVerificacionProcedimiento();
            //H4-25
            declaracionView.VerificacionProcedimiento.EnmendarDeclaracion = declaracionData.CORREGIRDECLARACION;
            declaracionView.VerificacionProcedimiento.EnmendarDeclaracionTexto = declaracionData.QUECORRECCIONES;
            //H4-26
            declaracionView.VerificacionProcedimiento.NumeroTotalSoportes = declaracionData.DOCUMENTOSADICIONALES ?? 0;
            declaracionView.VerificacionProcedimiento.NumeroTotalFolios = declaracionData.CUANTOSFOLIOS ?? 0;
            declaracionView.VerificacionProcedimiento.NumeroTotalAnexos = declaracionData.CUANTOS_ANEXOS ?? 0;
            //H4-27 a 31
            declaracionView.VerificacionProcedimiento.RealizoEntrevistaPrevia = declaracionData.ENTREVISTAPREVIA;
            declaracionView.VerificacionProcedimiento.RealizoTomaJuramento = declaracionData.REALIZOJURAMENTO;
            declaracionView.VerificacionProcedimiento.LeyoAlDeclaranteLaDeclaracion = declaracionData.LEYODECLARACION;
            declaracionView.VerificacionProcedimiento.HuboOrientacionParaCorregir = declaracionData.ORIENTACIONENMENDADURAS;
            declaracionView.VerificacionProcedimiento.SeIncluyeronCorrecciones = declaracionData.TIENEENMENDADURAS;
            //H4-32
            declaracionView.VerificacionProcedimiento.ObservacionesSobreDiligenciamiento = declaracionData.OBSERVACIONES;
            //H4-33
            declaracionView.VerificacionProcedimiento.UsoDatosPersonales = declaracionData.USODATOSPERSONALES;
            //H4-34 y 36
            declaracionView.VerificacionProcedimiento.DeclaranteSabeFirmar = declaracionData.SABE_FIRMAR;
            declaracionView.VerificacionProcedimiento.FuncionarioCargo = declaracionData.CARGO;


            //H1:
            declaracionView.TomaDeclaracion.MedioDeContactoMensajeTexto = declaracionData.MENSAJE_CELULAR;
            declaracionView.TomaDeclaracion.MedioDeContactoCorreoElectronico = declaracionData.MENSAJE_CORREOE;
            declaracionView.TomaDeclaracion.MedioDeContactoMensajeVoz = declaracionData.MENSAJE_FIJO;
            declaracionView.TomaDeclaracion.MedioDeContactoOtro = declaracionData.OTRO;

            declaracionView.TomaDeclaracion.HechosOtrosCual = declaracionData.OTROHECHO;

            //H4:
            declaracionView.VerificacionProcedimiento.NumeroTotalAnexos = declaracionData.CUANTOS_ANEXOS ?? 0;
            declaracionView.VerificacionProcedimiento.DeclaranteSabeFirmar = declaracionData.SABE_FIRMAR;

            //Estado Declaración.
            declaracionView.EstadoDeclaracion = (declaracionData.PARAM_ESTADO != null) ? (eEstadoDeclaracion)declaracionData.PARAM_ESTADO : eEstadoDeclaracion.FinalizaCapturaSinRadicar;

            //Numer Formulario Radicacion.
            declaracionView.DeclaracionNumero = declaracionData.NUMEROFORMULARIO;
            declaracionView.RadicacionId = declaracionData.ID_DETALLERADICACION;

            declaracionView.UsuarioId = declaracionData.ID_USUARIO;
            declaracionView.UnidadTerritorialId = declaracionData.ID_UTERRITORIAL;

            declaracionView.VerificacionProcedimiento.NumeroTotalSoportesOtros = declaracionData.NUMEROSOPORTESOTROS.HasValue ? declaracionData.NUMEROSOPORTESOTROS.Value : 0;
            declaracionView.VerificacionProcedimiento.NumeroTotalSoportesOtrosDesc = declaracionData.NUMEROSOPORTESOTROSDESC;
        }
        #endregion
    }
}