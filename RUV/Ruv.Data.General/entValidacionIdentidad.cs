using RestSharp;
using Ruv.Business.DTO.IdentidadPersona;
using Ruv.Infrastructure.Crosscutting.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;

namespace Ruv.Data.General
{
    public class entValidacionIdentidad
    {
        public entValidacionIdentidad() { }

        /// <summary>
        /// Consumo de SP de Validacion de Identidad con Registraduria
        /// </summary>
        /// <param name="idTipoToma"></param>
        /// <param name="idUsuario"></param>
        /// <param name="idTipoDoc"></param>
        /// <param name="numDocumento"></param>
        /// <param name="primerNombre"></param>
        /// <param name="segundoNombre"></param>
        /// <param name="primerApellido"></param>
        /// <param name="segundoApellido"></param>
        /// <param name="celular"></param>
        /// <param name="email"></param>
        /// <param name="cError"></param>
        /// <returns></returns>
        [Obsolete("Este metodo es obsoleto por que ya no esta disponible el servicio a traves del procedimiento almacenado")]
        public List<clsPersonaIdentidad> ValidarPersonaRNEC(int idTipoToma, int idUsuario, int idTipoDoc, string numDocumento, string primerNombre, string segundoNombre,
            string primerApellido, string segundoApellido, string celular, string email, ref string cError)
        {
            List<clsPersonaIdentidad> resultado = new List<clsPersonaIdentidad>();
            using (Dao d = new Dao())
            {
                d.AddParameter(new OracleParameter { ParameterName = "p_id_usuario", OracleType = OracleType.VarChar, Direction = ParameterDirection.Input, Value = idUsuario });
                d.AddParameter(new OracleParameter { ParameterName = "P_PMT_TOMA_DECLA", OracleType = OracleType.Number, Direction = ParameterDirection.Input, Value = idTipoToma });
                d.AddParameter(new OracleParameter { ParameterName = "P_PROCESO", OracleType = OracleType.VarChar, Direction = ParameterDirection.Input, Value = "TOMA_EN_LINEA" });
                d.AddParameter(new OracleParameter { ParameterName = "p_documento", OracleType = OracleType.VarChar, Direction = ParameterDirection.Input, Value = numDocumento });
                d.AddParameter(new OracleParameter { ParameterName = "p_tip_doc", OracleType = OracleType.Number, Direction = ParameterDirection.Input, Value = idTipoDoc });
                d.AddParameter(new OracleParameter { ParameterName = "p_nom1", OracleType = OracleType.VarChar, Direction = ParameterDirection.Input, Value = primerNombre });
                d.AddParameter(new OracleParameter { ParameterName = "p_nom2", OracleType = OracleType.VarChar, Direction = ParameterDirection.Input, Value = !string.IsNullOrEmpty(segundoNombre) ? segundoNombre : " " });
                d.AddParameter(new OracleParameter { ParameterName = "p_ape1", OracleType = OracleType.VarChar, Direction = ParameterDirection.Input, Value = primerApellido });
                d.AddParameter(new OracleParameter { ParameterName = "p_ape2", OracleType = OracleType.VarChar, Direction = ParameterDirection.Input, Value = !string.IsNullOrEmpty(segundoApellido) ? segundoApellido : " " });
                d.AddParameter(new OracleParameter { ParameterName = "p_cel", OracleType = OracleType.VarChar, Direction = ParameterDirection.Input, Value = !string.IsNullOrEmpty(celular) ? celular : " " });
                d.AddParameter(new OracleParameter { ParameterName = "p_mail", OracleType = OracleType.VarChar, Direction = ParameterDirection.Input, Value = !string.IsNullOrEmpty(email) ? email : " " });
                d.AddParameter(new OracleParameter { ParameterName = "p_Result", OracleType = OracleType.Cursor, Direction = ParameterDirection.Output });
                var result = ComplexDataAccessImplements.MapFromDataReaderI<clsPersonaIdentidad>(d.ExecuteReader("AAA_PROC_TD_VAL_IDENT_RUV", ref cError), true);
                return result;
            }
        }

        public List<clsPersonaIdentidad> ValidarPersonaRNECSServicio(int idTipoToma, int idUsuario, int idTipoDoc, string numDocumento, string primerNombre, string segundoNombre,
            string primerApellido, string segundoApellido, string celular, string email, ref string cError)
        {
            string url = ConfigurationManager.AppSettings.Get("ServicioRNEC");

            List<clsPersonaIdentidad> resultado = new List<clsPersonaIdentidad>();
            string nombreCompleto = primerNombre + segundoNombre + primerApellido + segundoApellido;
            var client = new RestClient(string.Format(url, numDocumento));
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var personasRnec = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PersonaRNEC>>(response.Content);

                foreach (var item in personasRnec)
                {
                    string nombreCompletoRNEC = string.Empty;
                    nombreCompletoRNEC = item.nom1 + item.nom2 + item.ape1 + item.ape2;
                    double porcentaje = 0;
                    LevenshteinDistance(nombreCompleto, nombreCompletoRNEC, out porcentaje);
                    int valida = porcentaje < 0.75 ? 1 : 0;
                    string mensajeValida = valida == 1 ? "VALIDADO" : "NO VALIDADO";
                    int idValidacion = 0;

                    using (Dao d = new Dao())
                    {
                        d.AddParameter(new OracleParameter { ParameterName = "P_PARAM_TOMA_DECLA", OracleType = OracleType.VarChar, Direction = ParameterDirection.Input, Value = idTipoToma });
                        d.AddParameter(new OracleParameter { ParameterName = "P_ID_USUARIO", OracleType = OracleType.VarChar, Direction = ParameterDirection.Input, Value = idUsuario });
                        d.AddParameter(new OracleParameter { ParameterName = "P_TIPO_DOC", OracleType = OracleType.VarChar, Direction = ParameterDirection.Input, Value = idTipoDoc });
                        d.AddParameter(new OracleParameter { ParameterName = "P_NUMERO", OracleType = OracleType.VarChar, Direction = ParameterDirection.Input, Value = numDocumento });
                        d.AddParameter(new OracleParameter { ParameterName = "P_NOM1", OracleType = OracleType.VarChar, Direction = ParameterDirection.Input, Value = primerNombre });
                        d.AddParameter(new OracleParameter { ParameterName = "P_NOM2", OracleType = OracleType.VarChar, Direction = ParameterDirection.Input, Value = segundoNombre });
                        d.AddParameter(new OracleParameter { ParameterName = "P_APE1", OracleType = OracleType.VarChar, Direction = ParameterDirection.Input, Value = primerApellido });
                        d.AddParameter(new OracleParameter { ParameterName = "P_APE2", OracleType = OracleType.VarChar, Direction = ParameterDirection.Input, Value = segundoApellido });
                        d.AddParameter(new OracleParameter { ParameterName = "P_CELULAR", OracleType = OracleType.VarChar, Direction = ParameterDirection.Input, Value = celular });
                        d.AddParameter(new OracleParameter { ParameterName = "P_MAIL", OracleType = OracleType.VarChar, Direction = ParameterDirection.Input, Value = email });
                        d.AddParameter(new OracleParameter { ParameterName = "P_VALIDACION", OracleType = OracleType.Number, Direction = ParameterDirection.Input, Value = valida });
                        d.AddParameter(new OracleParameter { ParameterName = "P_TIPO_DOC_VAL", OracleType = OracleType.VarChar, Direction = ParameterDirection.Input, Value = item.tipo_doc });
                        d.AddParameter(new OracleParameter { ParameterName = "P_NOM1_VAL", OracleType = OracleType.VarChar, Direction = ParameterDirection.Input, Value = item.nom1 });
                        d.AddParameter(new OracleParameter { ParameterName = "P_NOM2_VAL", OracleType = OracleType.VarChar, Direction = ParameterDirection.Input, Value = item.nom2 });
                        d.AddParameter(new OracleParameter { ParameterName = "P_APE1_VAL", OracleType = OracleType.VarChar, Direction = ParameterDirection.Input, Value = item.ape1 });
                        d.AddParameter(new OracleParameter { ParameterName = "P_APE2_VAL", OracleType = OracleType.VarChar, Direction = ParameterDirection.Input, Value = item.ape2 });
                        d.AddParameter(new OracleParameter { ParameterName = "P_VIGENCIA", OracleType = OracleType.VarChar, Direction = ParameterDirection.Input, Value = item.estado_cedula });
                        d.AddParameter(new OracleParameter { ParameterName = "P_RESOL", OracleType = OracleType.VarChar, Direction = ParameterDirection.Input, Value = item.num_resol });
                        d.AddParameter(new OracleParameter { ParameterName = "P_ANO_RESOL", OracleType = OracleType.VarChar, Direction = ParameterDirection.Input, Value = item.ano_resol });
                        d.AddParameter(new OracleParameter { ParameterName = "P_DOCUMENTO_CANCELADO", OracleType = OracleType.VarChar, Direction = ParameterDirection.Input, Value = item.documento_cancelado });
                        d.AddParameter(new OracleParameter { ParameterName = "P_GENERO", OracleType = OracleType.VarChar, Direction = ParameterDirection.Input, Value = item.genero });
                        d.AddParameter(new OracleParameter { ParameterName = "P_F_NACIMIENTO", OracleType = OracleType.DateTime, Direction = ParameterDirection.Input, Value = item.fechaNacimiento });
                        d.AddParameter(new OracleParameter { ParameterName = "P_ID", OracleType = OracleType.Number, Direction = ParameterDirection.Output });
                        d.ExecuteNonQuery("AAA_PROD_TD_VAL_IDENT_INSERT", null);
                        idValidacion = Convert.ToInt32(d.GetOutputParameter("P_ID"));

                        resultado.Add(new clsPersonaIdentidad
                        {
                            Identificador = idValidacion,
                            PrimerNombre = item.nom1,
                            SegundoNombre = item.nom2,
                            PrimerApellido = item.ape1,
                            SegundoApellido = item.ape2,
                            Vigencia = item.estado_cedula,
                            Validacion = valida,
                            Resultado = mensajeValida
                        });
                    }
                }

            }
            return resultado;
        }

        public class PersonaRNEC
        {
            public string fuente { get; set; }
            public string tipo_doc { get; set; }
            public string nuip { get; set; }
            public string nom1 { get; set; }
            public string nom2 { get; set; }
            public string ape1 { get; set; }
            public string ape2 { get; set; }
            public string pais_exp { get; set; }
            public string depto_exp { get; set; }
            public string mun_exp { get; set; }
            public string f_exp { get; set; }
            public string estado_cedula { get; set; }
            public string num_resol { get; set; }
            public string ano_resol { get; set; }
            public decimal documento_cancelado { get; set; }
            public string observacion { get; set; }
            public string genero { get; set; }
            public string fechaNacimiento { get; set; }
            public string f_consulta { get; set; }
        }


        public List<clsPersonaRNEC> BuscarPersonaRNEC(string numDocumento, string tipoDocumento)
        {
            string url = ConfigurationManager.AppSettings.Get("ServicioRNEC");

            List<clsPersonaRNEC> resultado = new List<clsPersonaRNEC>();
            var client = new RestClient(string.Format(url, numDocumento, tipoDocumento));
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                resultado = Newtonsoft.Json.JsonConvert.DeserializeObject<List<clsPersonaRNEC>>(response.Content);
            }
            return resultado;
        }
        public int LevenshteinDistance(string s, string t, out double porcentaje)
        {
            porcentaje = 0;
            s = s.ToUpper();
            t = t.ToUpper();
            // d es una tabla con m+1 renglones y n+1 columnas
            int costo = 0;
            int m = s.Length;
            int n = t.Length;
            int[,] d = new int[m + 1, n + 1];

            // Verifica que exista algo que comparar
            if (n == 0) return m;
            if (m == 0) return n;

            // Llena la primera columna y la primera fila.
            for (int i = 0; i <= m; d[i, 0] = i++) ;
            for (int j = 0; j <= n; d[0, j] = j++) ;


            /// recorre la matriz llenando cada unos de los pesos.
            /// i columnas, j renglones
            for (int i = 1; i <= m; i++)
            {
                // recorre para j
                for (int j = 1; j <= n; j++)
                {
                    /// si son iguales en posiciones equidistantes el peso es 0
                    /// de lo contrario el peso suma a uno.
                    costo = (s[i - 1] == t[j - 1]) ? 0 : 1;
                    d[i, j] = System.Math.Min(System.Math.Min(d[i - 1, j] + 1,  //Eliminacion
                                  d[i, j - 1] + 1),                             //Insercion 
                                  d[i - 1, j - 1] + costo);                     //Sustitucion
                }
            }

            /// Calculamos el porcentaje de cambios en la palabra.
            if (s.Length > t.Length)
                porcentaje = ((double)d[m, n] / (double)s.Length);
            else
                porcentaje = ((double)d[m, n] / (double)t.Length);
            return d[m, n];
        }

        /// <summary>
        /// Obtiene las preguntas de validacion
        /// </summary>
        /// <param name="idValidacion">Identificación de la validación</param>
        /// <param name="cError">Error</param>
        /// <returns>Preguntas en formato json</returns>
        public string PreguntasValidacion(int idValidacion, ref string cError)
        {
            using (Dao d = new Dao())
            {
                d.RefreshParameters();
                d.AddParameter(new OracleParameter { ParameterName = "P_TD_ID", OracleType = OracleType.Number, Direction = ParameterDirection.Input, Value = idValidacion });
                d.AddParameter(new OracleParameter { ParameterName = "p_Result", OracleType = OracleType.Cursor, Direction = ParameterDirection.Output });
                IDataReader dr = null;

                try
                {
                    dr = d.ExecuteReader("AAA_PROC_TD_QUESTION_VAL", ref cError);
                    if (!(cError == null || cError == string.Empty)) return null;
                }
                catch (Exception ex)
                {
                    cError = ex.Message;
                    return string.Empty;
                }

                List<PreguntaString> json = ComplexDataAccessImplements.MapFromDataReaderI<PreguntaString>(dr, true);
                if (json != null && json.Count > 0)
                    return json.FirstOrDefault().PreguntaJson;

                return string.Empty;
            }
        }
    }
}
