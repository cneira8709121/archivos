using System;
using System.Collections.Generic;
using System.Data;
using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.Infrastructure.Crosscutting.Common.General;

namespace Ruv.WPF.Data
{
    /// <summary>
    /// Provee información de caracter general.
    /// </summary>
    public class clsGeneral
    {
        /// <summary>
        /// Obtiene la lista de los parámetros generales.
        /// </summary>
        /// <returns></returns>
        public clsDatosGenerales ObtenerParametrosGenerales()
        {
            clsDatosGenerales Output = new clsDatosGenerales
            {
                GrupoParamDetalle = new List<clsGrupoParamDetalle>(),
                Departamentos = new List<clsParametroDepartamento>(),
                Nacionalidades = new List<clsParametroNacionalidad>(),
                Municipios = new List<clsParametroMunicipio>(),
                Parametros = new List<clsParametroGeneral>(),
                ComunidadesEtnicas = new List<clsComunidadEtnica>(),
                GruposEtnicos = new List<clsGrupoEtnica>(),
                Poblaciones = new List<clsPoblacion>(),
                UnidadesTerritoriales = new List<clsParametroUT>(),
                Paises = new List<clsParametroPais>(),
                Validaciones = new List<clsValidaciones>(),
                EntidadesMunicipios = new List<clsEntidadMunicipio>(),
                PreguntasCriticaN = new List<clsPreguntaCriticaN>(),
                Causales = new List<clsCausal>()
            };

            DataSet DS = null;


            Ruv.WPF.Data.clsDB db = new clsDB();
            DS = db.ExecuteDataSet(clsComando.Paquete + "OBTENERINFOGENERAL",
              null, null, null, null, null, null, null, null, null, null, null, null, null, null);

            // Armar la lista de resultados.
            // Convertir los parámetros en una colección de objetos.

            // 1) Los departamentos.
            foreach (DataRow item in DS.Tables[0].Rows)
            {
                bool? representacion = null;
                if (item["REPRESENTACION"] != DBNull.Value) representacion = Convert.ToBoolean(item["REPRESENTACION"]);

                Output.Departamentos.Add(new clsParametroDepartamento
                  {
                      Id = Convert.ToInt64(item["ID"]),
                      Nombre = Convert.ToString(item["NOMBRE"]),
                      PaisId = Convert.ToInt64(item["PADREID"]),
                      TieneRepresentacion = representacion

                  });
            }

            // 2) Los municipios.
            foreach (DataRow item in DS.Tables[1].Rows)
            {
                int? codigo = null;
                if (item["codigoTelefono"] != DBNull.Value) codigo = Convert.ToInt32(item["codigoTelefono"]);

                bool? representacion = null;
                if (item["REPRESENTACION"] != DBNull.Value) representacion = Convert.ToBoolean(item["REPRESENTACION"]);

                Output.Municipios.Add(new clsParametroMunicipio
                    {
                        Id = Convert.ToInt32(item["ID"]),
                        Nombre = Convert.ToString(item["NOMBRE"]),
                        DepartamentoId = Convert.ToInt32(item["PADREID"]),
                        //Agregados para codigo area
                        CodigoTelefono = codigo,
                        TieneRepresentacion = representacion
                    });
            }

            // 3) Demás parámetros.
            foreach (DataRow item in DS.Tables[2].Rows)
            {
                Output.Parametros.Add(new clsParametroGeneral
                    {
                        Id = Convert.ToInt32(item["ID"]),
                        Nombre = Convert.ToString(item["NOMBRE"]),
                        Tipo =
                         (eTipoParametros)
                         Enum.Parse(typeof(eTipoParametros), Convert.ToString(item["TIPO"])),
                        EsOtro = Convert.ToInt32(item["OTRO"]) == 1,
                        Numero = Convert.ToInt32(item["NUMERO"]),
                        Valor = item["VALOR"].ToString(),
                        Activo = Convert.ToBoolean(item["ACTIVO"])
                });
            }

            // 4) Detalle de los grupos de parámetros.
            foreach (DataRow item in DS.Tables[3].Rows)
            {
                Output.GrupoParamDetalle.Add(new clsGrupoParamDetalle
                {
                    Conjunto = (eGruposParametros)Convert.ToInt32(item["grupoparametroid"]),
                    ParametroId = Convert.ToInt32(item["parametroid"]),
                    Orden = Convert.ToInt32(item["orden"])
                });
            }

            // 5) Los grupos étnicos.
            foreach (DataRow item in DS.Tables[4].Rows)
            {
                Output.GruposEtnicos.Add(new clsGrupoEtnica
                {
                    Id = Convert.ToInt32(item["Id"]),
                    Nombre = item["Nombre"].ToString(),
                    EtniaId = Convert.ToInt32(item["EtniaId"])
                });
            }

            // 6) Las comunidades étnicas
            foreach (DataRow item in DS.Tables[5].Rows)
            {
                Output.ComunidadesEtnicas.Add(new clsComunidadEtnica
                {
                    Id = Convert.ToInt32(item["Id"]),
                    Nombre = item["Nombre"].ToString(),
                    GrupoEtnicoId = Convert.ToInt32(item["GrupoEtnicoId"])
                });
            }

            // 7) Las poblaciones.
            bool TipoValido = false;
            foreach (DataRow item in DS.Tables[6].Rows)
            {
                var NP = new clsPoblacion
                {
                    Id = Convert.ToInt32(item["Id"]),
                    Nombre = item["Nombre"].ToString(),
                    MunicipioId = Convert.ToInt32(item["id_municipio"])
                };

                TipoValido = true;
                switch (Convert.ToInt32(item["id_entorno"]))
                {
                    case 653: NP.TipoPoblacion = eTipoPoblacion.Urbano_Barrio; break;
                    case 655:
                    case 656: NP.TipoPoblacion = eTipoPoblacion.Urbano_Localidad; break;
                    case 651: NP.TipoPoblacion = eTipoPoblacion.Rural_Corregimiento; break;
                    case 652: NP.TipoPoblacion = eTipoPoblacion.Rural_Vereda; break;
                    default:
                        TipoValido = false;
                        break;
                }

                if (TipoValido)
                    Output.Poblaciones.Add(NP);
            }

            // 8) Las unidades territoriales.
            foreach (DataRow item in DS.Tables[7].Rows)
            {
                Output.UnidadesTerritoriales.Add(new clsParametroUT
                {
                    Id = Convert.ToInt32(item["ID"]),
                    Nombre = Convert.ToString(item["NOMBRE"])
                });
            }

            // 9) Los Paises.
            foreach (DataRow item in DS.Tables[8].Rows)
            {
                int? codigo = null;
                if (item["codigoTelefono"] != DBNull.Value) codigo = Convert.ToInt32(item["codigoTelefono"]);

                bool? representacion = null;
                if (item["REPRESENTACION"] != DBNull.Value) representacion = Convert.ToBoolean(item["REPRESENTACION"]);

                Output.Paises.Add(new clsParametroPais
                {
                    Id = Convert.ToInt64(item["ID"]),
                    Nombre = Convert.ToString(item["NOMBRE"]),
                    //Agregados para codigo area y consulado
                    CodigoTelefono = codigo,
                    TieneRepresentacion = representacion
                });
            }
            
            // 10) Las Validaciones.

            foreach (DataRow item in DS.Tables[9].Rows)
            {
                Output.Validaciones.Add(new clsValidaciones
                {
                    NombreHoja = Convert.ToString(item["NombreHoja"]),
                    Propiedad = Convert.ToString(item["Propiedad"]),
                    Valor = (eEstadoValidacion)Convert.ToInt32(item["Valor"])
                });
            }

            foreach (DataRow em in DS.Tables[10].Rows)
            {
                Output.EntidadesMunicipios.Add(new clsEntidadMunicipio
                    {
                        NId = Convert.ToInt64(em["id"]),
                        NIdEntidad = Convert.ToInt16(em["id_entidad"]),
                        NIdMunicipio = Convert.ToInt64(em["id_municipio"]),
                        CNombreEncargado = Convert.ToString(em["nombreencargado"]),
                        CNombreEntidad = Convert.ToString(em["nombre"]),
                        CNombreOtraEntidad = Convert.ToString(em["nombre_otros"])
                    });
            }

            // Critica N
            foreach (DataRow item in DS.Tables[11].Rows)
            {
                Output.PreguntasCriticaN.Add(new clsPreguntaCriticaN
                {
                    NId = Convert.ToInt32(item["ID"]),
                    CNombre = Convert.ToString(item["NOMBREPREGUNTA"]),
                    NIdCausal = Convert.ToInt32(item["ID_CAUSAL"])
                });
            }

            // Causales
            foreach (DataRow item in DS.Tables[12].Rows)
            {
                Output.Causales.Add(new clsCausal
                {
                    NId = Convert.ToInt32(item["ID"]),
                    CNombre = Convert.ToString(item["NOMBRECAUSAL"]),
                    CParteEmotiva = Convert.ToString(item["PARTEEMOTIVA"]),
                    EParametroTipoCausal = (eTipoParametros)Convert.ToInt32(item["TIPO"])
                });
            }

            foreach (DataRow item in DS.Tables[13].Rows)
            {
                Output.Nacionalidades.Add(new clsParametroNacionalidad
                {
                    Id = Convert.ToInt32(item["ID"]),
                    Nacionalidad = Convert.ToString(item["NACIONALIDAD"]),
                    CodNacionalidad = Convert.ToString(item["CLAVE_NACIONALIDAD"])
                });
            }

            return Output;
        }
    }
}
