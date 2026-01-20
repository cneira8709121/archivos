using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ruv.Data;
using System.Data.Common;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;

namespace Ruv.Data.Reconocimiento
{
  public class entListaTareas : entidadRUV
  {
    #region Guardar
      public void updTBESTADOPROCESOS(int idRadicacion, int param_estado, DbTransaction tran)
    {
      DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_RADICACION.sp_updTBESTADOPROCESOS", new object[] { idRadicacion, param_estado });
      dbRUV.ExecuteNonQuery(cmd, tran);
    }

    #endregion

    #region Obtener
    /// <summary>
    /// 
    /// </summary>
    /// <param name="ID">ID Usuario.</param>
    /// <returns></returns>
    public DataTable getData(int ID, DateTime? FecharadicadoInicia, DateTime? FechaRadicadofinal,string NumeroFormulario, int? PageNumber, int? PageSize)
    {

      DataTable registros = new DataTable();

      registros = dbRUV.ExecuteDataSet("PKG_COMMON.SPLISTATAREASWPF", new object[] 
      { ID,FecharadicadoInicia,FechaRadicadofinal,NumeroFormulario,PageNumber,PageSize, null }).Tables[0];
      /*using (IDataReader dataReader = dbRUV.ExecuteReader("PKG_RADICACION.SP_LISTA_TAREAS", new object[] { ID, null }))
      {
        registros.Columns.Add("ID", typeof(int));
        registros.Columns.Add("FECHA", typeof(DateTime));
        registros.Columns.Add("ACCION", typeof(string));
        registros.Columns.Add("FORMULARIO", typeof(string));

        registros.Columns.Add("DECLARACION", typeof(int));

        while (dataReader.Read())
        {
          int index = 0;

          registros.Rows.Add(dbDefaults.getInt32(dataReader, index++), 
            dbDefaults.getDateTime(dataReader, index++), 
            dbDefaults.getString(dataReader, index++), 
            dbDefaults.getString(dataReader, index++),
            dbDefaults.getInt32(dataReader, index++));
        }
      }*/
      return registros;
    }

    public DataTable getDataPaginado(int ID, int startRow, int pageSize, string sortColumns, string filterEx)
    {

        DataTable registros = new DataTable();

        registros = dbRUV.ExecuteDataSet("PKG_COMMON.SP_LISTA_TAREAS_PAGINADO", new object[] { ID, startRow, pageSize, sortColumns, filterEx, null }).Tables[0];
        /*using (IDataReader dataReader = dbRUV.ExecuteReader("PKG_RADICACION.SP_LISTA_TAREAS", new object[] { ID, null }))
        {
          registros.Columns.Add("ID", typeof(int));
          registros.Columns.Add("FECHA", typeof(DateTime));
          registros.Columns.Add("ACCION", typeof(string));
          registros.Columns.Add("FORMULARIO", typeof(string));

          registros.Columns.Add("DECLARACION", typeof(int));

          while (dataReader.Read())
          {
            int index = 0;

            registros.Rows.Add(dbDefaults.getInt32(dataReader, index++), 
              dbDefaults.getDateTime(dataReader, index++), 
              dbDefaults.getString(dataReader, index++), 
              dbDefaults.getString(dataReader, index++),
              dbDefaults.getInt32(dataReader, index++));
          }
        }*/
        return registros;
    }

    public int getDataCount(int idUsuario)
    {
        DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_COMMON.SP_LISTATAREASCANTIDAD", new object[] {idUsuario, null });

        dbRUV.ExecuteNonQuery(cmd);
        return Convert.ToInt32(dbRUV.GetParameterValue(cmd, "PO_RECORDCOUNT"));
    }

    public int getDataCountWPF(int idUsuario, DateTime? FecharadicadoInicia, DateTime? FechaRadicadofinal, string NumeroFormulario)
    {
        DbCommand cmd = dbRUV.GetStoredProcCommand("PKG_COMMON.SPLISTATAREASWPFCANTIDAD", new object[] { idUsuario, FecharadicadoInicia, FechaRadicadofinal, NumeroFormulario, null });

        dbRUV.ExecuteNonQuery(cmd);
        return Convert.ToInt32(dbRUV.GetParameterValue(cmd, "PO_RECORDCOUNT"));
    }
    #endregion
    }
}
