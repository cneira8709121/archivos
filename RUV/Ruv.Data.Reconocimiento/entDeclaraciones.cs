using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Ruv.Data;
using System.Data.Common;
using System.Data;


namespace Ruv.Data.Reconocimiento
{
    public class entDeclaraciones : entidadRUV
    {
        #region Guardar Datos
        public void setDeclaraciones(TBDECLARACIONES objDeclaracion, int idRadicacion, DbTransaction tran)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_RECONOCIMIENTO.sp_setDeclaracion", getParametros(objDeclaracion, idRadicacion));
            dbRUV.ExecuteNonQuery(cmd, tran);
            objDeclaracion.NUMEROFORMULARIO = dbRUV.GetParameterValue(cmd, "P_NRO_FORMULARIO").ToString();
            objDeclaracion.ID = Convert.ToInt32(dbRUV.GetParameterValue(cmd, "P_idCreado"));
        }

        public void updDeclaraciones(TBDECLARACIONES objDeclaracion, int idRadicacion, DbTransaction tran)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_RECONOCIMIENTO.sp_updDeclaracion", getParametros(objDeclaracion, idRadicacion));
            dbRUV.ExecuteNonQuery(cmd, tran);
        }

        private object[] getParametros(TBDECLARACIONES objDeclaracion, int idRadicacion)
        {
            return new object[]{
                                  objDeclaracion.ID
                                , objDeclaracion.ENTREVISTAPREVIA
                                , objDeclaracion.EXPLICACIONALCANCE
                                , objDeclaracion.PARAM_TIPODESPLAZAMIENTO                                
                                , objDeclaracion.ID_MUNICIPIODECLARACION
                                , objDeclaracion.ID_DEPARTAMENTODECLARACION
                                , objDeclaracion.ID_PAISDECLARACION
                                , objDeclaracion.PARAM_ENTIDADATIENDE
                                , objDeclaracion.FECHADECLARACION
                                , objDeclaracion.ID_MUNICIPIOACTUAL
                                , objDeclaracion.ID_DEPARTAMENTOACTUAL
                                , objDeclaracion.PARAM_ENTORNOACTUAL
                                , objDeclaracion.PARAM_TIPOENTORNOACTUAL
                                , objDeclaracion.ID_POBLADOACTUAL
                                , objDeclaracion.CUALPOBLADOACTUAL
                                , objDeclaracion.FECHAARRIBO
                                , objDeclaracion.DIRECCIONCORRESPONDENCIA
                                , objDeclaracion.TELEFONOACTUAL
                                , objDeclaracion.PARAM_TIPOENTORNODESPLAZ
                                , objDeclaracion.PARAM_ENTORNODESPLAZ
                                , objDeclaracion.ID_DEPARTAMENTODESPLAZ
                                , objDeclaracion.ID_MUNICIPIODESPLAZ
                                , objDeclaracion.ID_POBLADODESPLAZ
                                , objDeclaracion.CUALPOBLADODESPLAZ
                                , objDeclaracion.ANHOSRESIDENCIA
                                , objDeclaracion.MESESRESIDENCIA
                                , objDeclaracion.FECHADESPLAZ
                                , objDeclaracion.PARAM_DECLAROANTERIORMENTE
                                , objDeclaracion.ID_MUNICIPIOANTERIOR
                                , objDeclaracion.ID_DEPARTAMENTOANTERIOR
                                , objDeclaracion.PARAM_ENTIDADATENDIO
                                , objDeclaracion.FECHADECLARACIONANTERIOR
                                , objDeclaracion.RAZONSITIO
                                , objDeclaracion.PARAM_DESEOHOGAR
                                , objDeclaracion.ID_MUNICIPIODESEADO
                                , objDeclaracion.ID_DEPARTAMENTODESEADO
                                , objDeclaracion.PARAM_ENTORNODESEADO
                                , objDeclaracion.FECHATERMINACION
                                , objDeclaracion.REALIZOJURAMENTO
                                , objDeclaracion.LEYODECLARACION
                                , objDeclaracion.DOCUMENTOSADICIONALES
                                , objDeclaracion.CUANTOSFOLIOS
                                , objDeclaracion.ORIENTACIONENMENDADURAS
                                , objDeclaracion.TIENEENMENDADURAS
                                , objDeclaracion.ID_USUARIO
                                , objDeclaracion.CODIGOANTIGUO
                                , objDeclaracion.PARAM_ESTADO
                                , objDeclaracion.FUNCIONARIO
                                , objDeclaracion.CARGO
                                , objDeclaracion.CAMPOPRUEBA
                                , objDeclaracion.ID_DETALLERADICACION
                                , objDeclaracion.ID_UTERRITORIAL
                                , objDeclaracion.PUNTAJE_HOGAR
                                , objDeclaracion.PARAM_PROCESO
                                , objDeclaracion.FECHAFINALIZACION
                                , objDeclaracion.FECHAREGISTRO
                                , objDeclaracion.PARAM_TIPOREPRESENTANTE
                                , objDeclaracion.CORREGIRDECLARACION
                                , objDeclaracion.QUECORRECCIONES
                                , objDeclaracion.TELEFONOGERESS
                                , objDeclaracion.OBSERVACIONES
                                , objDeclaracion.FECHA_PRIMERA_INCLUSION
                                , objDeclaracion.VECES_HOGAR_NO_INCLUIDO
                                , objDeclaracion.ID_DECLARACION_PADRE
                                , objDeclaracion.MENSAJE_CELULAR
                                , objDeclaracion.MENSAJE_CORREOE
                                , objDeclaracion.MENSAJE_FIJO
                                , objDeclaracion.OTRO
                                , objDeclaracion.CUANTOS_ANEXOS
                                , objDeclaracion.SABE_FIRMAR
                                , objDeclaracion.ID_ENCARGADO
                                , idRadicacion
                                , objDeclaracion.NUMEROFORMULARIO
                                , objDeclaracion.OTROHECHO
                                , objDeclaracion.ID_ENTIDADMUNICIPIODECLARACION
                                , objDeclaracion.NUMEROSOPORTESOTROS
                                , objDeclaracion.NUMEROSOPORTESOTROSDESC
                                , objDeclaracion.USODATOSPERSONALES
                                , objDeclaracion.VERSION_FUD
                                //, objDeclaracion.IDENTIFICACIONFUNCIONARIO 
                                , null
            };
        }

        /// <summary>
        /// Actualizar el ID del jefe de hogar para todos los TBREGISTROS_PERSONAS
        /// asociados al TBDECLARACIONES
        /// </summary>
        /// <param name="id_declaracion">ID de TBDECLARACIONES</param>
        /// <param name="id_JefeHogar">ID del jefe de hogar</param>
        public void actualizarJefeHogar(int id_declaracion, int id_JefeHogar, int FamiliaConsecutivo, DbTransaction tran)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_RECONOCIMIENTO.sp_updJefeHogarRegistroPersona", id_declaracion, id_JefeHogar, FamiliaConsecutivo);
            dbRUV.ExecuteNonQuery(cmd, tran);
        }
        #endregion


        #region Obtener Datos
        public TBDECLARACIONES getDeclaraciones(int ID)
        {
            using (IDataReader dataReader = dbRUV.ExecuteReader("PKG_RECONOCIMIENTO.sp_getDeclaracion", new object[] { ID, null }))
            {
                while (dataReader.Read())
                {
                    TBDECLARACIONES RegistroDec = EnterpriseLibraryContainer.Current.GetInstance<TBDECLARACIONES>();
                    int index = 0;

                    RegistroDec.ID = (int)dbDefaults.getInt32(dataReader, index++);
                    RegistroDec.ENTREVISTAPREVIA = dbDefaults.getInt16(dataReader, index++);
                    RegistroDec.EXPLICACIONALCANCE = dbDefaults.getInt16(dataReader, index++);
                    RegistroDec.PARAM_TIPODESPLAZAMIENTO = dbDefaults.getInt16(dataReader, index++);
                    RegistroDec.ID_MUNICIPIODECLARACION = dbDefaults.getInt64(dataReader, index++);
                    RegistroDec.ID_DEPARTAMENTODECLARACION = dbDefaults.getInt64(dataReader, index++);
                    RegistroDec.ID_PAISDECLARACION = dbDefaults.getInt64(dataReader, index++);
                    RegistroDec.PARAM_ENTIDADATIENDE = dbDefaults.getInt16(dataReader, index++);
                    RegistroDec.FECHADECLARACION = dbDefaults.getDateTime(dataReader, index++);
                    RegistroDec.ID_MUNICIPIOACTUAL = dbDefaults.getInt16(dataReader, index++);
                    RegistroDec.ID_DEPARTAMENTOACTUAL = dbDefaults.getInt16(dataReader, index++);
                    RegistroDec.PARAM_ENTORNOACTUAL = dbDefaults.getInt16(dataReader, index++);
                    RegistroDec.PARAM_TIPOENTORNOACTUAL = dbDefaults.getInt16(dataReader, index++);
                    RegistroDec.ID_POBLADOACTUAL = dbDefaults.getInt16(dataReader, index++);
                    RegistroDec.CUALPOBLADOACTUAL = dbDefaults.getString(dataReader, index++);
                    RegistroDec.FECHAARRIBO = dbDefaults.getDateTime(dataReader, index++);
                    RegistroDec.DIRECCIONCORRESPONDENCIA = dbDefaults.getString(dataReader, index++);
                    RegistroDec.TELEFONOACTUAL = dbDefaults.getString(dataReader, index++);
                    RegistroDec.PARAM_TIPOENTORNODESPLAZ = dbDefaults.getInt16(dataReader, index++);
                    RegistroDec.PARAM_ENTORNODESPLAZ = dbDefaults.getInt16(dataReader, index++);
                    RegistroDec.ID_DEPARTAMENTODESPLAZ = dbDefaults.getInt16(dataReader, index++);
                    RegistroDec.ID_MUNICIPIODESPLAZ = dbDefaults.getInt16(dataReader, index++);
                    RegistroDec.ID_POBLADODESPLAZ = dbDefaults.getInt16(dataReader, index++);
                    RegistroDec.CUALPOBLADODESPLAZ = dbDefaults.getString(dataReader, index++);
                    RegistroDec.ANHOSRESIDENCIA = dbDefaults.getInt16(dataReader, index++);
                    RegistroDec.MESESRESIDENCIA = dbDefaults.getInt16(dataReader, index++);
                    RegistroDec.FECHADESPLAZ = dbDefaults.getDateTime(dataReader, index++);
                    RegistroDec.PARAM_DECLAROANTERIORMENTE = dbDefaults.getInt16(dataReader, index++);
                    RegistroDec.ID_MUNICIPIOANTERIOR = dbDefaults.getInt16(dataReader, index++);
                    RegistroDec.ID_DEPARTAMENTOANTERIOR = dbDefaults.getInt16(dataReader, index++);
                    RegistroDec.PARAM_ENTIDADATENDIO = dbDefaults.getInt16(dataReader, index++);
                    RegistroDec.FECHADECLARACIONANTERIOR = dbDefaults.getDateTime(dataReader, index++);
                    RegistroDec.RAZONSITIO = dbDefaults.getString(dataReader, index++);
                    RegistroDec.PARAM_DESEOHOGAR = dbDefaults.getInt16(dataReader, index++);
                    RegistroDec.ID_MUNICIPIODESEADO = dbDefaults.getInt16(dataReader, index++);
                    RegistroDec.ID_DEPARTAMENTODESEADO = dbDefaults.getInt16(dataReader, index++);
                    RegistroDec.PARAM_ENTORNODESEADO = dbDefaults.getInt16(dataReader, index++);
                    RegistroDec.FECHATERMINACION = dbDefaults.getDateTime(dataReader, index++);
                    RegistroDec.REALIZOJURAMENTO = dbDefaults.getInt16(dataReader, index++);
                    RegistroDec.LEYODECLARACION = dbDefaults.getInt16(dataReader, index++);
                    RegistroDec.DOCUMENTOSADICIONALES = dbDefaults.getInt16(dataReader, index++);
                    RegistroDec.CUANTOSFOLIOS = dbDefaults.getInt16(dataReader, index++);
                    RegistroDec.ORIENTACIONENMENDADURAS = dbDefaults.getInt16(dataReader, index++);
                    RegistroDec.TIENEENMENDADURAS = dbDefaults.getInt16(dataReader, index++);
                    RegistroDec.ID_USUARIO = dbDefaults.getInt32(dataReader, index++);
                    RegistroDec.CODIGOANTIGUO = dbDefaults.getString(dataReader, index++);
                    RegistroDec.PARAM_ESTADO = dbDefaults.getInt16(dataReader, index++);
                    RegistroDec.FUNCIONARIO = dbDefaults.getString(dataReader, index++);
                    RegistroDec.CARGO = dbDefaults.getString(dataReader, index++);
                    RegistroDec.CAMPOPRUEBA = dbDefaults.getString(dataReader, index++);
                    RegistroDec.ID_DETALLERADICACION = dbDefaults.getInt32(dataReader, index++);
                    RegistroDec.ID_UTERRITORIAL = dbDefaults.getInt16(dataReader, index++);
                    RegistroDec.PUNTAJE_HOGAR = dbDefaults.getInt16(dataReader, index++);
                    RegistroDec.PARAM_PROCESO = dbDefaults.getInt16(dataReader, index++);
                    RegistroDec.FECHAFINALIZACION = dbDefaults.getDateTime(dataReader, index++);
                    RegistroDec.FECHAREGISTRO = dbDefaults.getDateTime(dataReader, index++);
                    RegistroDec.PARAM_TIPOREPRESENTANTE = dbDefaults.getInt16(dataReader, index++);
                    RegistroDec.CORREGIRDECLARACION = dbDefaults.getInt16(dataReader, index++);
                    RegistroDec.QUECORRECCIONES = dbDefaults.getString(dataReader, index++);
                    RegistroDec.TELEFONOGERESS = dbDefaults.getString(dataReader, index++);
                    RegistroDec.OBSERVACIONES = dbDefaults.getString(dataReader, index++);
                    RegistroDec.FECHA_PRIMERA_INCLUSION = dbDefaults.getDateTime(dataReader, index++);
                    RegistroDec.VECES_HOGAR_NO_INCLUIDO = dbDefaults.getInt16(dataReader, index++);
                    RegistroDec.ID_DECLARACION_PADRE = dbDefaults.getInt16(dataReader, index++);
                    RegistroDec.MENSAJE_CELULAR = dbDefaults.getInt16(dataReader, index++);
                    RegistroDec.MENSAJE_CORREOE = dbDefaults.getInt16(dataReader, index++);
                    RegistroDec.MENSAJE_FIJO = dbDefaults.getInt16(dataReader, index++);
                    RegistroDec.OTRO = dbDefaults.getString(dataReader, index++);
                    RegistroDec.CUANTOS_ANEXOS = dbDefaults.getInt16(dataReader, index++);
                    RegistroDec.SABE_FIRMAR = dbDefaults.getInt16(dataReader, index++);
                    RegistroDec.ID_ENCARGADO = dbDefaults.getInt32(dataReader, index++);
                    RegistroDec.NUMEROFORMULARIO = dbDefaults.getString(dataReader, index++);
                    RegistroDec.OTROHECHO = dbDefaults.getString(dataReader, index++);
                    RegistroDec.IDENTIFICACIONFUNCIONARIO = dbDefaults.getString(dataReader, index++);
                    RegistroDec.ID_ENTIDADMUNICIPIODECLARACION = dbDefaults.getInt16(dataReader, index++);
                    RegistroDec.NUMEROSOPORTESOTROS = dbDefaults.getInt32(dataReader, index++);
                    RegistroDec.NUMEROSOPORTESOTROSDESC = dbDefaults.getString(dataReader, index++);
                    RegistroDec.VERSION_FUD = dbDefaults.getInt16(dataReader, index++);
                    RegistroDec.USODATOSPERSONALES = dbDefaults.getInt16(dataReader, index++);
                    return RegistroDec;
                }
            }
            return null;
        }

        /// <summary>
        /// Obtener la lista de declaraciones segun los parametros recibidos
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public DataTable BuscarDeclaracion(object[] parametros)
        {

            DataTable registros = new DataTable();
            using (IDataReader dataReader = dbRUV.ExecuteReader("PKG_RECONOCIMIENTO.sp_getListaDeclaracion", parametros))
            {
                registros.Columns.Add("Id", typeof(int));
                registros.Columns.Add("CodigoDeclaracion", typeof(string));
                registros.Columns.Add("FechaDeclaracion", typeof(DateTime));
                registros.Columns.Add("EstadoDeclaracion", typeof(int));
                registros.Columns.Add("DeclaranteNumeroIdentificacion", typeof(string));
                registros.Columns.Add("DeclarantePrimerNombre", typeof(string));
                registros.Columns.Add("DeclaranteDemasNombres", typeof(string));
                registros.Columns.Add("DeclarantePrimerApellido", typeof(string));
                registros.Columns.Add("DeclaranteSegundoApellido", typeof(string));


                while (dataReader.Read())
                {
                    int index = 0;

                    registros.Rows.Add(
                      dbDefaults.getInt32(dataReader, index++),
                      dbDefaults.getString(dataReader, index++),
                      dbDefaults.getDateTime(dataReader, index++),
                      dbDefaults.getInt32(dataReader, index++),
                      dbDefaults.getString(dataReader, index++),
                      dbDefaults.getString(dataReader, index++),
                      dbDefaults.getString(dataReader, index++),
                      dbDefaults.getString(dataReader, index++),
                      dbDefaults.getString(dataReader, index++)
                      );
                }
            }
            return registros;
        }
        #endregion
    
        public void EnviarValoracion(int id_declaracion, DbTransaction tran)
        {
            DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_VALORACION.SP_ENVIAR_VALORACION", id_declaracion);
            dbRUV.ExecuteNonQuery(cmd, tran);
        }
    
    }

    // Soporte de operaciones con Transacciones
    class declaracionCommand : Ruv.Data.comandoRuv
    {
        private TBDECLARACIONES objDeclaracion;

        public declaracionCommand(TBDECLARACIONES myDeclaracion)
        {
            objDeclaracion = myDeclaracion;
        }
        public override void ejecutar(Database objBaseDatos, IDbTransaction objTransaccion)
        {
            throw new NotImplementedException();
        }

        public override void retroceder(Database objBaseDatos, IDbTransaction objTransaccion)
        {
            throw new NotImplementedException();
        }

        public override void ejecutar()
        {
            throw new NotImplementedException();
        }

        public override void retroceder()
        {
            throw new NotImplementedException();
        }
    }
}
