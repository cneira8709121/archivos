using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Ruv.Business.DTO.GestionFormulario;
using System.Data.Common;
using System.Data.OracleClient;
using resx = Ruv.Infrastructure.Crosscutting.Resources;

namespace Ruv.Data.GestionFormulario
{
    public class Administrador : Contratos.IGestionFormulario, Contratos.IGetFormulario
    {
        #region Public methods

        #region Services implementation

        public List<clsFormulario> GenerarFormularios(uint nCantidad,
                                                      string cSerie,
                                                      int nIdUsuario,
                                                      int nIdEstado,
                                                      int? nIdPais,
                                                      int? nIdDepartamento,
                                                      int? nIdMunicipio,
                                                      int? nIdEntidadmunicipio,
                                                      ref string cError)
        {
            Dao d = new Dao();
            d.RefreshParameters();
            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.Cantidad,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Value = nCantidad,
                Direction = ParameterDirection.Input
            });
            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.Serie,
                OracleType = System.Data.OracleClient.OracleType.VarChar,
                Value = cSerie,
                Direction = ParameterDirection.Input
            });
            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdUsuario,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Value = nIdUsuario,
                Direction = ParameterDirection.Input
            });
            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdEstado,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Value = nIdEstado,
                Direction = ParameterDirection.Input
            });
            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdPais,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Value = nIdPais == 0 ? null : nIdPais,
                Direction = ParameterDirection.Input,
                IsNullable = true
            });
            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdDepartamento,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Value = nIdDepartamento == 0 ? null : nIdDepartamento,
                Direction = ParameterDirection.Input,
                IsNullable = true
            });
            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdMunicipio,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Value = nIdMunicipio == 0 ? null : nIdMunicipio,
                Direction = ParameterDirection.Input,
                IsNullable = true
            });
            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdEntidadmunicipio,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Value = nIdEntidadmunicipio == 0 ? null : nIdEntidadmunicipio,
                Direction = ParameterDirection.Input,
                IsNullable = true
            });

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.Formularios,
                OracleType = System.Data.OracleClient.OracleType.Cursor,
                Direction = ParameterDirection.Output
            });

            IDataReader dr = null;
            try
            {
                dr = d.ExecuteReader(Infrastructure.Crosscutting.Resources.DB.Procedimientos.GenerarFormularios, ref cError);
                if (!(cError == null || cError == string.Empty)) return null;
            }
            catch (Exception ex)
            {
                cError = ex.Message;
                return null;
            }

            return ComplexDataAccessImplements.MapFromDataReaderI<clsFormulario>(dr, true);
        }

        /// <summary>
        /// Purpose : Generar Formularios WEB
        /// Author  : John Henao
        /// Date    : 7/6/2013
        /// </summary>
        /// <param name="nCantidad"></param>
        /// <param name="cSerie"></param>
        /// <param name="nIdUsuario"></param>
        /// <param name="nIdEstado"></param>
        /// <param name="nIdEntidadmunicipio"></param>
        /// <param name="cError"></param>
        /// <returns></returns>
        public List<clsFormulario> GenerarFormulariosWEB(uint nCantidad,
                                                         string cSerie,
                                                         int nIdUsuario,
                                                         int nIdEstado,
                                                         int? nIdEntidadmunicipio,
                                                         ref string cError)
        {
            Dao d = new Dao();
            d.RefreshParameters();
            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.Cantidad,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Value = nCantidad,
                Direction = ParameterDirection.Input
            });
            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.Serie,
                OracleType = System.Data.OracleClient.OracleType.VarChar,
                Value = cSerie,
                Direction = ParameterDirection.Input
            });
            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdUsuario,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Value = nIdUsuario,
                Direction = ParameterDirection.Input
            });
            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdEstado,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Value = nIdEstado,
                Direction = ParameterDirection.Input
            });
            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdEntidadmunicipio,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Value = nIdEntidadmunicipio == 0 ? null : nIdEntidadmunicipio,
                Direction = ParameterDirection.Input,
                IsNullable = true
            });

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.Formularios,
                OracleType = System.Data.OracleClient.OracleType.Cursor,
                Direction = ParameterDirection.Output
            });

            IDataReader dr = null;
            try
            {
                dr = d.ExecuteReader(Infrastructure.Crosscutting.Resources.DB.Procedimientos.GenerarFormularioWEB, ref cError);
                if (!(cError == null || cError == string.Empty)) return null;
            }
            catch (Exception ex)
            {
                cError = ex.Message;
                return null;
            }

            return ComplexDataAccessImplements.MapFromDataReaderI<clsFormulario>(dr, true);
        }

        /// <summary>
        /// Purpose : Obtiene ID PAIS que Genera Formularios WEB
        /// Author  : John Henao
        /// Date    : 7/6/2013
        /// </summary>
        /// <param name="nIdEntidadmunicipio"></param>
        /// <param name="cError"></param>
        /// <returns></returns>
        public int ObtenerPaisGeneraFormularioWEB(int? nIdEntidadmunicipio, ref string cError)
        {
            Dao d = new Dao();
            d.RefreshParameters();

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdEntidadmunicipio,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Value = nIdEntidadmunicipio == 0 ? null : nIdEntidadmunicipio,
                Direction = ParameterDirection.Input,
                IsNullable = true
            });

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.Po_IdPais,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Direction = ParameterDirection.Output
            });
            
            int IdPais = 0;

                d.ExecuteNonQuery(Infrastructure.Crosscutting.Resources.DB.Procedimientos.ObtienePaisGeneradorFormularioWEB, null, ref cError);
                if (string.IsNullOrEmpty(cError)) {
                 IdPais = (int)(decimal)d.GetOutputParameter(Infrastructure.Crosscutting.Resources.DB.Parametros.Po_IdPais);
                return IdPais;    
            }

            return IdPais;

        }

        public List<clsFormulario> ListarFormularios(ref string cError)
        {
            Dao d = new Dao();
            d.RefreshParameters();
            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.Resultado,
                OracleType = System.Data.OracleClient.OracleType.Cursor,
                Direction = ParameterDirection.Output
            });

            IDataReader dr = null;
            try
            {
                dr = d.ExecuteReader(Infrastructure.Crosscutting.Resources.DB.Procedimientos.ListarFormularios, ref cError);
                if (!(cError == null || cError == string.Empty)) return null;
            }
            catch (Exception ex)
            {
                cError = ex.Message;
                return null;
            }

            return ComplexDataAccessImplements.MapFromDataReaderI<clsFormulario>(dr, true);
        }

        public List<clsFormulario> ListarFormulariosNoRadicados(clsFormulario frm, ref string cError)
        {
            Dao d = new Dao();
            d.RefreshParameters();
            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.NumeroFormulario.ToUpper(),
                OracleType = System.Data.OracleClient.OracleType.VarChar,
                IsNullable = true,
                Value = frm.CNumeroFormulario,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdPais.ToUpper(),
                OracleType = System.Data.OracleClient.OracleType.Number,
                IsNullable = true,
                Value = frm.NIdPais,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdDepartamento.ToUpper(),
                OracleType = System.Data.OracleClient.OracleType.Number,
                IsNullable = true,
                Value = frm.NIdDepartamento,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdMunicipio.ToUpper(),
                OracleType = System.Data.OracleClient.OracleType.Number,
                IsNullable = true,
                Value = frm.NIdMunicipio,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdEntidadmunicipio.ToUpper(),
                OracleType = System.Data.OracleClient.OracleType.Number,
                IsNullable = true,
                Value = frm.NIdEntidad,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.Mensaje.ToUpper(),
                OracleType = System.Data.OracleClient.OracleType.VarChar,
                Size = 1000,
                Direction = ParameterDirection.Output
            });

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.Resultado.ToUpper(),
                OracleType = System.Data.OracleClient.OracleType.Cursor,
                Direction = ParameterDirection.Output
            });

            IDataReader dr = null;
            try
            {
                dr = d.ExecuteReader(Infrastructure.Crosscutting.Resources.DB.Procedimientos.ListarFormulariosNoRadicados, ref cError);
                if (!(cError == null || cError == string.Empty)) return null;
            }
            catch (Exception ex)
            {
                cError = ex.Message;
                return null;
            }

            return ComplexDataAccessImplements.MapFromDataReaderI<clsFormulario>(dr, true);
        }

        public List<clsFormulario> ListarFormulariosPorEstado(ushort nIdEstado, ref string cError)
        {
            Dao d = new Dao();
            d.RefreshParameters();
            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdEstado,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Value = nIdEstado,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.Resultado,
                OracleType = System.Data.OracleClient.OracleType.Cursor,
                Direction = ParameterDirection.Output
            });

            IDataReader dr = null;
            try
            {
                dr = d.ExecuteReader(Infrastructure.Crosscutting.Resources.DB.Procedimientos.ListarFormulariosPorEstado, ref cError);
                if (!(cError == null || cError == string.Empty)) return null;
            }
            catch (Exception ex)
            {
                cError = ex.Message;
                return null;
            }

            return ComplexDataAccessImplements.MapFromDataReaderI<clsFormulario>(dr, true);
        }

        public uint? AsignarFormulario(clsFormulario frm, ref string cError)
        {
            Dao d = new Dao();
            d.RefreshParameters();
            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.NumeroFormulario,
                OracleType = System.Data.OracleClient.OracleType.VarChar,
                Value = frm.CNumeroFormulario,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdPais,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Value = frm.NIdPais,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdDepartamento,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Value = frm.NIdDepartamento,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdMunicipio,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Value = frm.NIdMunicipio,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdEntidadmunicipio,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Value = frm.NIdEntidad,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdUsuario,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Value = frm.NIdUsuario,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdFormulario,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Direction = ParameterDirection.Output
            });

            try
            {
                d.ExecuteNonQuery(Infrastructure.Crosscutting.Resources.DB.Procedimientos.AsignarFormulario, null, ref cError);
                if (!(cError == null || cError == string.Empty)) return null;
            }
            catch (Exception ex)
            {
                cError = ex.Message;
                return null;
            }

            return (uint?)(decimal)d.GetOutputParameter(Infrastructure.Crosscutting.Resources.DB.Parametros.IdFormulario);
        }

        public bool AsignarFormulario(clsSolicitudFormularioEstado frm, DbTransaction tra, ref string cError)
        {
            Dao d = new Dao();
            d.RefreshParameters();
            d.AddParameter(new OracleParameter { ParameterName = resx::DB.Parametros.NumeroFormulario, OracleType = OracleType.VarChar, Value = frm.CNumeroFormulario, Direction = ParameterDirection.Input, IsNullable = true });
            d.AddParameter(new OracleParameter { ParameterName = resx::DB.Parametros.PINumeroDesde, OracleType = OracleType.Number, Value = frm.NDesde, Direction = ParameterDirection.Input, IsNullable = true });
            d.AddParameter(new OracleParameter { ParameterName = resx::DB.Parametros.PINumeroHasta, OracleType = OracleType.Number, Value = frm.NHasta, Direction = ParameterDirection.Input, IsNullable = true });
            d.AddParameter(new OracleParameter { ParameterName = resx::DB.Parametros.PIDGenerado, OracleType = OracleType.DateTime, Value = frm.DGenerado, Direction = ParameterDirection.Input, IsNullable = true });
            d.AddParameter(new OracleParameter { ParameterName = resx::DB.Parametros.IdUsuario, OracleType = OracleType.Number, Value = frm.NIdUsuario, Direction = ParameterDirection.Input });
            d.AddParameter(new OracleParameter { ParameterName = resx::DB.Parametros.IdPais, OracleType = OracleType.Number, Value = frm.NIdPais, Direction = ParameterDirection.Input });
            d.AddParameter(new OracleParameter { ParameterName = resx::DB.Parametros.IdDepartamento, OracleType = OracleType.Number, Value = frm.NIdDepartamento, Direction = ParameterDirection.Input });
            d.AddParameter(new OracleParameter { ParameterName = resx::DB.Parametros.IdMunicipio, OracleType = OracleType.Number, Value = frm.NIdMunicipio, Direction = ParameterDirection.Input });
            d.AddParameter(new OracleParameter { ParameterName = resx::DB.Parametros.IdEntidad, OracleType = OracleType.Number, Value = frm.NIdEntidad, Direction = ParameterDirection.Input });
            d.AddParameter(new OracleParameter { ParameterName = resx::DB.Parametros.ResultadoConsulta, OracleType = System.Data.OracleClient.OracleType.Cursor, Direction = ParameterDirection.Output });

            try
            {
                d.ExecuteNonQuery(resx::DB.Procedimientos.AsignarFormularioFiltro, tra, ref cError);
                if (!string.IsNullOrEmpty(cError)) return false;
            }
            catch (Exception ex)
            {
                cError = ex.Message;
                return false;
            }
            return true;
        }

        public uint? InactivarFormulario(uint nIdFormulario, string observacion, ref string cError)
        {
            Dao d = new Dao();
            d.RefreshParameters();
            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdFormularioIn,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Value = nIdFormulario,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.Observacion,
                OracleType = System.Data.OracleClient.OracleType.VarChar,
                Value = observacion,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdFormulario,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Direction = ParameterDirection.Output
            });

            try
            {
                d.ExecuteNonQuery(Infrastructure.Crosscutting.Resources.DB.Procedimientos.InactivarFormulario, null, ref cError);
                if (!(cError == null || cError == string.Empty)) return null;
            }
            catch (Exception ex)
            {
                cError = ex.Message;
                return null;
            }

            return (uint?)(decimal)d.GetOutputParameter(Infrastructure.Crosscutting.Resources.DB.Parametros.IdFormulario);
        }

        public uint? SepararFormularioImprenta(clsFormulario frm, ref string cError)
        {
            Dao d = new Dao();
            d.RefreshParameters();
            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.NumeroFormulario,
                OracleType = System.Data.OracleClient.OracleType.VarChar,
                Value = frm.CNumeroFormulario,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdUsuario,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Value = frm.NIdUsuario,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdFormulario,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Direction = ParameterDirection.Output
            });

            try
            {
                d.ExecuteNonQuery(Infrastructure.Crosscutting.Resources.DB.Procedimientos.SepararFormularioImprenta, null, ref cError);
                if (!(cError == null || cError == string.Empty)) return null;
            }
            catch (Exception ex)
            {
                cError = ex.Message;
                return null;
            }

            return (uint?)(decimal)d.GetOutputParameter(Infrastructure.Crosscutting.Resources.DB.Parametros.IdFormulario);
        }

        public List<clsFormulario> SepararFormularioImprenta(clsSolicitudFormularioEstado frm, DbTransaction tra, ref string cError)
        {
            Dao d = new Dao();
            d.RefreshParameters();
            d.AddParameter(new OracleParameter { ParameterName = resx::DB.Parametros.NumeroFormulario, OracleType = OracleType.VarChar, Value = frm.CNumeroFormulario, Direction = ParameterDirection.Input, IsNullable = true });
            d.AddParameter(new OracleParameter { ParameterName = resx::DB.Parametros.PINumeroDesde, OracleType = OracleType.Number, Value = frm.NDesde, Direction = ParameterDirection.Input, IsNullable = true });
            d.AddParameter(new OracleParameter { ParameterName = resx::DB.Parametros.PINumeroHasta, OracleType = OracleType.Number, Value = frm.NHasta, Direction = ParameterDirection.Input, IsNullable = true });
            d.AddParameter(new OracleParameter { ParameterName = resx::DB.Parametros.PIDGenerado, OracleType = OracleType.DateTime, Value = frm.DGenerado, Direction = ParameterDirection.Input, IsNullable = true });
            d.AddParameter(new OracleParameter { ParameterName = resx::DB.Parametros.IdUsuario, OracleType = System.Data.OracleClient.OracleType.Number, Value = frm.NIdUsuario, Direction = ParameterDirection.Input });
            d.AddParameter(new OracleParameter { ParameterName = resx::DB.Parametros.ResultadoConsulta, OracleType = System.Data.OracleClient.OracleType.Cursor, Direction = ParameterDirection.Output });

            try
            {
                d.ExecuteNonQuery(resx::DB.Procedimientos.SepararFormularioImprentaFiltro, tra, ref cError);
                if (!(cError == null || cError == string.Empty)) return null;
            }
            catch (Exception ex)
            {
                cError = ex.Message;
                return null;
            }

            IDataReader dr = (IDataReader)d.GetOutputParameter(resx::DB.Parametros.ResultadoConsulta);
            return ComplexDataAccessImplements.MapFromDataReaderI<clsFormulario>(dr, true);
        }

        public List<clsFormulario> ObtenerFormulariosPorUsuario(int nIdUsuario, ref string cError)
        {
            Dao d = new Dao();
            d.RefreshParameters();
            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdUsuario,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Value = nIdUsuario,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.Formularios,
                OracleType = System.Data.OracleClient.OracleType.Cursor,
                Direction = ParameterDirection.Output
            });

            IDataReader dr = null;
            try
            {
                dr = d.ExecuteReader(Infrastructure.Crosscutting.Resources.DB.Procedimientos.ObtenerFrmPorUsuario, ref cError);
                if (!(cError == null || cError == string.Empty)) return null;
            }
            catch (Exception ex)
            {
                cError = ex.Message;
                return null;
            }

            return ComplexDataAccessImplements.MapFromDataReaderI<clsFormulario>(dr, true);
        }

        public uint? MarcarDescargado(uint nIdFormulario, ref string cError)
        {
            Dao d = new Dao();
            d.RefreshParameters();
            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdFormularioIn,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Value = nIdFormulario,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdFormulario,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Direction = ParameterDirection.Output
            });

            try
            {
                d.ExecuteNonQuery(Infrastructure.Crosscutting.Resources.DB.Procedimientos.MarcarDescargado, null, ref cError);
                if (!(cError == null || cError == string.Empty)) return null;
            }
            catch (Exception ex)
            {
                cError = ex.Message;
                return null;
            }

            return (uint?)(decimal)d.GetOutputParameter(Infrastructure.Crosscutting.Resources.DB.Parametros.IdFormulario);
        }

        public void MarcarRadicado(string cNumeroFormulario, DbTransaction transaction, ref string cError)
        {
            Dao d = new Dao();
            d.RefreshParameters();
            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.NumeroFormulario,
                OracleType = System.Data.OracleClient.OracleType.VarChar,
                Value = cNumeroFormulario,
                Direction = ParameterDirection.Input
            });

            try
            {
                d.ExecuteNonQuery(Infrastructure.Crosscutting.Resources.DB.Procedimientos.MarcarFormularioRadicado, transaction, ref cError);
            }
            catch (Exception ex)
            {
                cError = ex.Message;
            }
        }

        public clsFormulario ObtenerFormulario(string cNumeroFormulario, ref string cError)
        {
            Dao d = new Dao();
            d.RefreshParameters();
            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.NumeroFormulario,
                OracleType = System.Data.OracleClient.OracleType.VarChar,
                Value = cNumeroFormulario,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.Resultado,
                OracleType = System.Data.OracleClient.OracleType.Cursor,
                Direction = ParameterDirection.Output
            });

            IDataReader dr = null;
            try
            {
                dr = d.ExecuteReader(Infrastructure.Crosscutting.Resources.DB.Procedimientos.ObtenerFrmPorNumero, ref cError);
                if (!(cError == null || cError == string.Empty)) return null;
            }
            catch (Exception ex)
            {
                cError = ex.Message;
                return null;
            }

            List<clsFormulario> listClsFormulario = ComplexDataAccessImplements.MapFromDataReaderI<clsFormulario>(dr, true);

            if (listClsFormulario != null && listClsFormulario.Count > 0)
                return listClsFormulario.FirstOrDefault();
            else
                return null;
        }

        public List<clsFormulario> ObtenerFormulariosPorUsuarioEstadoPaginado(clsSolicitudFormularioEstado frm, ref string cError)
        {
            Dao d = new Dao();
            d.RefreshParameters();

            d.AddParameter(new OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.NumeroFormulario,
                OracleType = OracleType.VarChar,
                Value = frm.CNumeroFormulario,
                Direction = ParameterDirection.Input,
                IsNullable = true
            });

            d.AddParameter(new OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.PINumeroDesde,
                OracleType = OracleType.Number,
                Value = frm.NDesde,
                Direction = ParameterDirection.Input,
                IsNullable = true
            });

            d.AddParameter(new OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.PINumeroHasta,
                OracleType = OracleType.Number,
                Value = frm.NHasta,
                Direction = ParameterDirection.Input,
                IsNullable = true
            });

            d.AddParameter(new OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.PIDGenerado,
                OracleType = OracleType.DateTime,
                Value = frm.DGenerado,
                Direction = ParameterDirection.Input,
                IsNullable = true
            });

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdEstado,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Value = frm.IdEstado.GetHashCode(),
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdUsuario,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Value = frm.NIdUsuario,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.NumeroPagina,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Value = frm.NPagina,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.RegistrosPorPagina,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Value = frm.NDatosPorPg,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.ResultadoConsulta,
                OracleType = System.Data.OracleClient.OracleType.Cursor,
                Direction = ParameterDirection.Output
            });

            IDataReader dr = null;
            try
            {
                dr = d.ExecuteReader(Infrastructure.Crosscutting.Resources.DB.Procedimientos.ObtenerFrmPorUsuarioEstadoPaginado, ref cError);
                if (!(cError == null || cError == string.Empty)) return null;
            }
            catch (Exception ex)
            {
                cError = ex.Message;
                return null;
            }

            return ComplexDataAccessImplements.MapFromDataReaderI<clsFormulario>(dr, true);
        }

        public int ObtenerCantidadFormulariosPorUsuarioEstado(clsSolicitudFormularioEstado frm, ref string cError)
        {
            Dao d = new Dao();
            d.RefreshParameters();

            d.AddParameter(new OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.NumeroFormulario,
                OracleType = OracleType.VarChar,
                Value = frm.CNumeroFormulario,
                Direction = ParameterDirection.Input,
                IsNullable = true
            });

            d.AddParameter(new OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.PINumeroDesde,
                OracleType = OracleType.Number,
                Value = frm.NDesde,
                Direction = ParameterDirection.Input,
                IsNullable = true
            });

            d.AddParameter(new OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.PINumeroHasta,
                OracleType = OracleType.Number,
                Value = frm.NHasta,
                Direction = ParameterDirection.Input,
                IsNullable = true
            });

            d.AddParameter(new OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.PIDGenerado,
                OracleType = OracleType.DateTime,
                Value = frm.DGenerado,
                Direction = ParameterDirection.Input,
                IsNullable = true
            });

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdEstado,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Value = frm.IdEstado.GetHashCode(),
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new System.Data.OracleClient.OracleParameter
            {
                ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdUsuario,
                OracleType = System.Data.OracleClient.OracleType.Number,
                Value = frm.NIdUsuario,
                Direction = ParameterDirection.Input
            });

            d.AddParameter(new OracleParameter() { ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.ResultadoConteo, OracleType = OracleType.Number, Direction = ParameterDirection.Output });

            try
            {
                d.ExecuteNonQuery(Infrastructure.Crosscutting.Resources.DB.Procedimientos.ObtenerFrmPorUsuarioEstadoCantidad, null, ref cError);
                if (!(cError == null || cError == string.Empty)) return 0;
            }
            catch (Exception ex)
            {
                cError = ex.Message;
                return 0;
            }

            return int.Parse(d.GetOutputParameter(Infrastructure.Crosscutting.Resources.DB.Parametros.ResultadoConteo).ToString());
        }

        public int ObtenerCantidadFormulariosActivar(clsFormulario frm, ref string cError)
        {
            Dao d = new Dao();
            d.RefreshParameters();
            d.AddParameter(new System.Data.OracleClient.OracleParameter { ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.NumeroFormulario, OracleType = System.Data.OracleClient.OracleType.VarChar, Value = frm.CNumeroFormulario, Direction = ParameterDirection.Input });
            d.AddParameter(new System.Data.OracleClient.OracleParameter { ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdPais, OracleType = System.Data.OracleClient.OracleType.Number, Value = frm.NIdPais, Direction = ParameterDirection.Input });
            d.AddParameter(new System.Data.OracleClient.OracleParameter { ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdDepartamento, OracleType = System.Data.OracleClient.OracleType.Number, Value = frm.NIdDepartamento, Direction = ParameterDirection.Input });
            d.AddParameter(new System.Data.OracleClient.OracleParameter { ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdMunicipio, OracleType = System.Data.OracleClient.OracleType.Number, Value = frm.NIdMunicipio, Direction = ParameterDirection.Input });
            d.AddParameter(new System.Data.OracleClient.OracleParameter { ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdEntidadmunicipio, OracleType = System.Data.OracleClient.OracleType.Number, Value = frm.NIdEntidad, Direction = ParameterDirection.Input });
            d.AddParameter(new System.Data.OracleClient.OracleParameter { ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.ActivarOInactivar, OracleType = System.Data.OracleClient.OracleType.Number, Value = frm.Accion, Direction = ParameterDirection.Input });
            d.AddParameter(new System.Data.OracleClient.OracleParameter { ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.ResultadoConteo, OracleType = System.Data.OracleClient.OracleType.Number, Direction = ParameterDirection.Output });

            try
            {
                d.ExecuteNonQuery(Infrastructure.Crosscutting.Resources.DB.Procedimientos.CantidadFormulariosActivarInactivar, null, ref cError);
                if (!(cError == null || cError == string.Empty)) return 0;
            }
            catch (Exception ex)
            {
                cError = ex.Message;
                return 0;
            }

            return int.Parse(d.GetOutputParameter(Infrastructure.Crosscutting.Resources.DB.Parametros.ResultadoConteo).ToString());
        }

        public List<clsFormulario> ObtenerFormulariosActivar(clsFormulario frm, int nPagina, int nTamaño, ref string cError)
        {
            Dao d = new Dao();
            d.RefreshParameters();
            d.AddParameter(new System.Data.OracleClient.OracleParameter { ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.NumeroFormulario, OracleType = System.Data.OracleClient.OracleType.VarChar, Value = frm.CNumeroFormulario, Direction = ParameterDirection.Input});
            d.AddParameter(new System.Data.OracleClient.OracleParameter { ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdPais, OracleType = System.Data.OracleClient.OracleType.Number, Value = frm.NIdPais, Direction = ParameterDirection.Input });
            d.AddParameter(new System.Data.OracleClient.OracleParameter { ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdDepartamento, OracleType = System.Data.OracleClient.OracleType.Number, Value = frm.NIdDepartamento, Direction = ParameterDirection.Input });
            d.AddParameter(new System.Data.OracleClient.OracleParameter { ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdMunicipio, OracleType = System.Data.OracleClient.OracleType.Number, Value = frm.NIdMunicipio, Direction = ParameterDirection.Input });
            d.AddParameter(new System.Data.OracleClient.OracleParameter { ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.IdEntidadmunicipio, OracleType = System.Data.OracleClient.OracleType.Number, Value = frm.NIdEntidad, Direction = ParameterDirection.Input });
            d.AddParameter(new System.Data.OracleClient.OracleParameter { ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.ActivarOInactivar, OracleType = System.Data.OracleClient.OracleType.Number, Value = frm.Accion, Direction = ParameterDirection.Input });
            d.AddParameter(new System.Data.OracleClient.OracleParameter { ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.NumeroPagina, OracleType = System.Data.OracleClient.OracleType.Number, Value = nPagina, Direction = ParameterDirection.Input });
            d.AddParameter(new System.Data.OracleClient.OracleParameter { ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.RegistrosPorPagina, OracleType = System.Data.OracleClient.OracleType.Number, Value = nTamaño, Direction = ParameterDirection.Input });
            d.AddParameter(new System.Data.OracleClient.OracleParameter { ParameterName = Infrastructure.Crosscutting.Resources.DB.Parametros.ResultadoConsulta, OracleType = System.Data.OracleClient.OracleType.Cursor, Direction = ParameterDirection.Output });

            IDataReader dr = null;
            try
            {
                dr = d.ExecuteReader(Infrastructure.Crosscutting.Resources.DB.Procedimientos.ListarFormulariosAcitvarInactivar, ref cError);
                if (!(cError == null || cError == string.Empty)) return null;
            }
            catch (Exception ex)
            {
                cError = ex.Message;
                return null;
            }

            return ComplexDataAccessImplements.MapFromDataReaderI<clsFormulario>(dr, true);
        }
        #endregion

        #endregion
    }
}
