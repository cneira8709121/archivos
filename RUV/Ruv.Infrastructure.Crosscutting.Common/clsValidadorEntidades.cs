using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;
using System.Runtime.Remoting;

namespace Ruv.Infrastructure.Crosscutting.Common
{
    public class clsValidadorEntidades
    {
        /// <summary>
        /// Valida los errores en las entidades
        /// </summary>
        /// <param name="objeto"></param>
        /// <param name="propiedades"></param>
        /// <param name="ValidacionesRequeridas"></param>
        /// <param name="validacionesSaltadas"></param>
        /// <returns></returns>
        private List<string> ValidarPropiedades(object objeto, IEnumerable<PropertyInfo> propiedades, List<eEstadoValidacion> ValidacionesRequeridas, ref int validacionesSaltadas)
        {
            var Lista = new List<string>();
            IDataErrorInfo Interfase = objeto as IDataErrorInfo;

            if (Interfase == null)
                return Lista;

            var configuracionValidaciones = clsDeclaracion.ConfiguracionValidaciones;


            foreach (var property in propiedades)
            {
                var validationEntity = objeto as IValidationEntity;
                if (validationEntity != null)
                {
                    var configurationMatch = configuracionValidaciones.FirstOrDefault(x => x.NombreHoja == validationEntity.Scope && x.Propiedad == property.Name);
                    if (configurationMatch != null)
                    {
                        try {
                            var error = Interfase[property.Name];
                            if (error != null) {
                                if (!ValidacionesRequeridas.Contains(configurationMatch.Valor)) {
                                    if (configurationMatch.Valor == eEstadoValidacion.Flexible)
                                        validacionesSaltadas += 1;
                                }
                                else {
                                    Lista.Add(error);
                                }
                            }
                        }
                        catch (NullReferenceException) {
                            Lista.Add(string.Format("Debe especificar el valor de la propiedad '{0}' en {1}.", property.Name, validationEntity.Scope));
                        }
                        catch (Exception ex) {
                            Lista.Add(string.Format("No se pudo validar la propiedad '{0}' en '{1}': {2}", property.Name, validationEntity.Scope, ex.Message));
                        }
                    }
                }
            }

            return Lista;
        }
        /// <summary>
        /// True: Ninguna regla de la entidad reportó error.
        /// </summary>
        /// <param name="objeto"></param>
        /// <returns></returns>
        public bool EntidadEsValida(object objeto, List<eEstadoValidacion> ValidacionesRequeridas, ref int validacionesSaltadas)
        {

            bool resultado = false;

            // Si el objeto no implementa la interfaz se da por válido.
            IEnumerable<PropertyInfo> propiedades = objeto.GetType().GetProperties().Where(x => x.CanRead && x.CanWrite && x.DeclaringType == x.ReflectedType && !x.GetGetMethod().ReturnType.IsArray);
            var Lista = new List<string>();

            Lista = ValidarPropiedades(objeto, propiedades, ValidacionesRequeridas, ref validacionesSaltadas);

            if (Lista == null || !Lista.Any())
            {
                resultado = true;
            }
            return resultado;
        }

        /// <summary>
        /// Retorna una lista de los errores de validación para la entidad entregada.
        /// </summary>
        /// <param name="objeto"></param>
        /// <returns></returns>
        public List<string> ObtenerErroresEntidad(object objeto, List<eEstadoValidacion> ValidacionesRequeridas, ref int validacionesSaltadas)
        {
            IEnumerable<PropertyInfo> propiedades = objeto.GetType().GetProperties().Where(x => x.CanRead && x.CanWrite && x.DeclaringType == x.ReflectedType && !x.GetGetMethod().ReturnType.IsArray);
            var configuracionValidaciones = clsDeclaracion.ConfiguracionValidaciones;

            var Lista = new List<string>();

            Lista = ValidarPropiedades(objeto, propiedades, ValidacionesRequeridas, ref validacionesSaltadas);

            if (Lista == null || !Lista.Any()) return null;

            return Lista;
        }

        public void ListProperties(object objeto)
        {
            IDataErrorInfo Interfase = objeto as IDataErrorInfo;
            // Si el objeto no implementa a interfaz se da por válido.
            if (Interfase == null)
                return;

            objeto.GetType().GetProperties()
              .Where(x =>
                x.CanRead
                && x.CanWrite
                && x.DeclaringType == x.ReflectedType // Omitir las propiedades heredadas.
                && !x.PropertyType.FullName.StartsWith("System.Collections") // Omitir las colecciones.
                && !x.GetGetMethod().ReturnType.IsArray // Omitir los arrays.
                ).ToList().ForEach(x =>
                  System.Diagnostics.Debug.WriteLine(
                  string.Format("case \"{0}\":\nresultado = \"\";\nbreak;\n",
                  x.Name)));
        }

        /// <summary>
        /// Valida un número de documento de identificación.
        /// </summary>
        /// <param name="esObligatorio">true: El dato no es opcional.</param>
        /// <param name="tipoDocumento">El tipo de documento</param>
        /// <param name="numeroDocumento">El número de documento</param>
        /// <returns></returns>
        public static Tuple<Boolean, string> ValidarDocumentoIdentificacion(
          Boolean esObligatorio, int? tipoDocumento, string numeroDocumento)
        {
            Tuple<Boolean, string> Resultado = new Tuple<bool, string>(true, null);
            string PatronRegEx = null;
            string ExpLetrasNumeros = "^[0-9a-zA-Z ]*$";
            string ExpNumeros = "^[0-9 ]*$";

            // Estos tipos de documento no requieren número de identificación.
            int[] TiposOpcionales = { 114, 115, 5134, 5135 };
            Boolean TipoEsOpcional = tipoDocumento.HasValue
              && TiposOpcionales.Contains(tipoDocumento.Value);

            if (esObligatorio
              && !TipoEsOpcional
              && string.IsNullOrWhiteSpace(numeroDocumento))
                Resultado = new Tuple<bool, string>(false,
                  "El número de identificación es obligatorio");

            //else if (esObligatorio
            //  && !tipoDocumento.HasValue)
            //  Resultado = new Tuple<bool, string>(false,
            //    "El tipo de identificación es obligatorio");

            else if (tipoDocumento.HasValue
              && !TipoEsOpcional
              && string.IsNullOrWhiteSpace(numeroDocumento))
                Resultado = new Tuple<bool, string>(false,
                  "Al especificar el tipo de documento también debe ingresar el número de identificación");
            else if (tipoDocumento.HasValue)
            {
                // Validar el número de acuerdo al tipo de documento.
                switch ((eTipoDocumento)tipoDocumento.Value)
                {
                    case eTipoDocumento.NoInforma: // No informa.
                    case eTipoDocumento.Indocumentado: // Indocumentado.
                    case eTipoDocumento.IndocumentadoExtranjero: // Indocumentado.
                    case eTipoDocumento.NoResponde: // No Responde.
                    case eTipoDocumento.NoSabe: // No Sabe.
                        // No requiere número de identificación.
                        break;

                    case eTipoDocumento.RegistroCivil: // Registro Civil.
                    case eTipoDocumento.NUIP: // NUIP.
                    case eTipoDocumento.NIP: // NIP.
                    case eTipoDocumento.CedulaExtranjeria:  //Cedula de Extranjeria.
                        PatronRegEx = ExpLetrasNumeros;
                        break;

                    case eTipoDocumento.CedulaCiudadania: // Cédula.
                    case eTipoDocumento.LibretaMilitar: // Libreta militar.
                    case eTipoDocumento.TarjetaIdentidad: // Tarjeta identidad.
                        PatronRegEx = ExpNumeros;
                        break;
                }

                if (PatronRegEx != null)
                {
                    System.Text.RegularExpressions.Regex RE = new System.Text.RegularExpressions.Regex(PatronRegEx);
                    if (!RE.IsMatch(numeroDocumento))
                        Resultado = new Tuple<bool, string>(false,
                          "El número de identificación no es válido al tipo de documento elegido");
                }
            }

            return Resultado;
        }

        public static Tuple<Boolean, string> ValidarNumeroTelefonico(string valor, string nombreCampo, int longitudMax, int longitudMin, bool esObligatorio)
        {
            Tuple<Boolean, string> Resultado = new Tuple<bool, string>(true, null);
            string ExpNumeros = "^[0-9]*$";

            if (esObligatorio && string.IsNullOrWhiteSpace(valor))
                return new Tuple<bool, string>(false, string.Format("El {0} es obligatorio", nombreCampo));

            System.Text.RegularExpressions.Regex RE = new System.Text.RegularExpressions.Regex(ExpNumeros);
            if (!RE.IsMatch(valor))
                return new Tuple<bool, string>(false, string.Format("El {0} no es válido, solo se admiten numeros", nombreCampo));
            else if (valor != null && valor.Length > longitudMax)
                return new Tuple<bool, string>(false, string.Format("La longitud máxima para el {0} es de {1}", nombreCampo, longitudMax));
            else if (valor != null && longitudMin > 0 && valor.Length < longitudMin)
                return new Tuple<bool, string>(false, string.Format("La longitud mínima para el {0} es de {1}", nombreCampo, longitudMin));

            return new Tuple<bool, string>(true, null);
        }

        public static Tuple<Boolean, string> ValidarCorreoElectronico(string valor, string nombreCampo, int longitudMin, bool esObligatorio)
        {
            Tuple<Boolean, string> Resultado = new Tuple<bool, string>(true, null);
            string ExpReg = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*@((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$";

            if (esObligatorio && string.IsNullOrWhiteSpace(valor))
                return new Tuple<bool, string>(false, string.Format("El {0} es obligatorio", nombreCampo));

            System.Text.RegularExpressions.Regex RE = new System.Text.RegularExpressions.Regex(ExpReg);
            if (!RE.IsMatch(valor) && !valor.Equals("NO INFORMA"))
                return new Tuple<bool, string>(false, string.Format("El {0} no es válido", nombreCampo));
            else if (valor != null && valor.Length < longitudMin)
                return new Tuple<bool, string>(false, string.Format("La longitud minima para el {0} es de {1}", nombreCampo, longitudMin));

            return new Tuple<bool, string>(true, null);
        }

        public static string ValidarFechaNacimiento(Boolean esObligatorio, DateTime? fechaNacimiento, int? tipoDocumento)
        {

            if (esObligatorio && !fechaNacimiento.HasValue)
                return "La fecha de nacimientio es obligatorio y debe tener un formato valido (dd/mm/aaaa)";

            string Resultado = null;
            if (tipoDocumento.HasValue && fechaNacimiento.HasValue)
            {

                // Validar el número de acuerdo al tipo de documento.
                switch ((eTipoDocumento)tipoDocumento.Value)
                {

                    case eTipoDocumento.NoInforma: // No informa.
                    case eTipoDocumento.Indocumentado: // Indocumentado.
                    case eTipoDocumento.NoResponde: // No Responde.
                    case eTipoDocumento.NoSabe: // No Sabe.
                        // No requiere número de identificación.
                        break;

                    case eTipoDocumento.RegistroCivil: // Registro Civil.
                    case eTipoDocumento.NUIP: // NUIP.
                    case eTipoDocumento.NIP: // NIP.
                    case eTipoDocumento.LibretaMilitar: // Libreta militar.
                    case eTipoDocumento.CedulaExtranjeria:  //Cedula de Extranjeria.
                        break;

                    case eTipoDocumento.CedulaCiudadania: // Cédula.
                        //No puede ser menor de edad
                        if ((DateTime.Today - (DateTime)fechaNacimiento).Days / 365 < 18)
                            Resultado = "Debe ser mayor de edad para tener cedula o libreta militar";
                        break;
                    case eTipoDocumento.TarjetaIdentidad: // Tarjeta identidad.
                        //Debe ser menor edad  - no necesariamente
                        break;
                }
            }

            return Resultado;
        }

        public static string ValidarN1(string value, string valueName, bool esObligatorio)
        {
            if (esObligatorio && string.IsNullOrWhiteSpace(value))
                return string.Format("El {0} es obligatorio", valueName);

            if (value == null)
                return null;

            if (value != value.Trim())
                return string.Format("El {0} no debe tener espacios al principio o al final", valueName);

            if (value.IndexOf(" ") >= 0)
                return string.Format("El {0} no debe tener espacios", valueName);

            Regex reg = new Regex("[^a-zA-ZñÑ]");
            if (reg.IsMatch(value))
                return string.Format("El {0} solo acepta valores alfabeticos sin tildes, ni caracteres especiales", valueName);

            int lonMinima = 2;
            if (value.Length > 0 && value.Length < lonMinima)
                return string.Format("La longitud de {0} debe ser maryor o igual a {1}", valueName, lonMinima);

            //Validar que no tenga letras repetidas
            //string repetidas;
            //repetidas = ValidarCaracteresRepetidas(value, valueName);
            //if (repetidas != null)
            //    return repetidas;

            //Validar que no tenga letras repetidas
            //string repetidas;
            //repetidas = ValidarLetrasRepetidas(value);
            //if (repetidas != null)
            //    return repetidas;

            return null;

        }

        public static string ValidarAlfanumerico(string value, string valueName, bool esObligatorio = false, int minLenght = 0, bool valAcentos = false, bool valPalRepet = false, bool valEspacios = false, bool valCarRepet = false)
        {
            if (esObligatorio && string.IsNullOrWhiteSpace(value))
                return string.Format("El valor de {0} es obligatorio", valueName);

            if (value == null)
                return null;

            Regex regNum = new Regex("[0-9]+");
            if (regNum.IsMatch(value))
                return string.Format("El valor de {0} solo acepta valores alfabeticos, no puede introducir numeros", valueName);

            if (valAcentos)
            {
                Regex regAcent = new Regex("[^a-zA-ZñÑ ]");
                if (regAcent.IsMatch(value))
                    return string.Format("El valor de {0} solo acepta valores alfabeticos sin tildes, ni caracteres especiales", valueName);
            }

            if (valPalRepet)
            {
                string[] allWords = value.ToUpper().Split(' ');
                var contentWords = from w in allWords
                                   where w != string.Empty
                                   select w;
                int i = 0;
                foreach (string w in contentWords)
                {
                    if (i > 0)
                    {
                        if (w == contentWords.ElementAt(i - 1))
                            return string.Format("El valor de {0} no debe tener palabras repetidas", valueName);
                    }
                    i++;
                }
            }

            if (valEspacios)
            {
                if (value != value.Trim())
                    return string.Format("El valor de {0} no debe tener espacios al principio o al final", valueName);

                if (value.IndexOf("  ") >= 0)
                    return string.Format("El valor de {0} no debe tener mas de un espacio entre palabras", valueName);
            }

            int lonMinima = minLenght;
            if (value.Length > 0 && value.Length < lonMinima)
                return string.Format("La longitud de {0} debe ser maryor o igual a {1}", valueName, lonMinima);

            ////Validar que no tenga letras repetidas
            //if (valCarRepet)
            //{
            //    string repetidas;
            //    repetidas = ValidarCaracteresRepetidas(value, valueName);
            //    if (repetidas != null)
            //        return repetidas;
            //}

            //Validar que no tenga letras repetidas
            //string repetidas;
            //repetidas = ValidarLetrasRepetidas(value);
            //if (repetidas != null)
            //    return repetidas;

            return null;
        }

        public static string ValidarN2A1A2(string value, string valueName, bool esObligatorio)
        {
            string error = ValidarAlfanumerico(value, valueName, esObligatorio, 2, true, true, true, false);
            ////if (error == null)
            ////    error = ValidarLetrasRepetidas(value);

            return error;
        }

        public static string ValidarCaracteresRepetidas(string value, string valueName)
        {
            Regex reg = new Regex("(.+)\\1");
            if (reg.IsMatch(value))
                return string.Format("El valor de {0} no pueden tener caracteres repetidos. Por favor ingrese de nuevo los datos", valueName);

            return null;
        }

        public static string ValidarLetrasRepetidas(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre)) return null;

            Boolean result = true;
            string charAnt = "";
            int repeted = 0;

            for (int i = 0; i < nombre.Length; i++)
            {
                string charAct = nombre.ElementAt(i).ToString();
                if (charAnt == charAct)
                {
                    if ((charAct.ToLower() == "l") ||
                        (charAct.ToLower() == "r") ||
                        (charAct.ToLower() == "s") ||
                        (charAct.ToLower() == "n") ||
                        (charAct.ToLower() == "f") ||
                        (charAct.ToLower() == "m") ||
                        (charAct.ToLower() == "t") ||
                        (charAct.ToLower() == "g") ||
                        (charAct.ToLower() == "d") ||
                        (charAct.ToLower() == "c") ||
                        (charAct.ToLower() == "o") ||
                        (charAct.ToLower() == "a") ||
                        (charAct.ToLower() == "e") ||
                        (charAct.ToLower() == "z") ||
                        (charAct.ToLower() == Convert.ToChar(241).ToString()) || //Es la ñ, genera problemas si se coloca la letra directamente.	
                        (charAct.ToLower() == "w")
                        )
                    {
                        repeted++;
                        if (repeted > 1)
                        {
                            result = false;
                            repeted = 0;
                        }
                    }
                    else
                        result = false;
                }
                else
                    repeted = 0;

                charAnt = charAct;
            }




            if (!result)
                return "Los nombres y apellidos no pueden tener caracteres repetidos. Por favor ingrese de nuevo los datos";
            else
                return null;
        }
    }
}
