using System;
using System.Linq;
using System.Windows.Data;
using Ruv.Infrastructure.Crosscutting.Common;

namespace Ruv.WPF.Captura.Converters
{

    /// <summary>
    /// Converter genérico para la búsqueda de un dato en las listas generales.
    /// Retorna el nombre del dato.
    /// </summary>
    public class ConsultaDatoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {



            if (value == null || parameter == null) return null;
            string Resultado = null;

            Int64? Valor = null;

            if (parameter.ToString().ToUpper() == "TIPOENTORNO")
            {
                eTipoPoblacion TP;
                if (Enum.TryParse<eTipoPoblacion>(value.ToString(), out TP))
                    Valor = (int)TP;
                else
                    return null;
            }
            else if (parameter.ToString().ToUpper() == "TIPOENTORNOPOBLACIONES")
            {
                eTipoEntorno TE;
                if (Enum.TryParse<eTipoEntorno>(System.Convert.ToString(value), out TE))
                    return "Entorno " + TE.ToString();
                else
                    return null;
            }
            else if (parameter.ToString().ToUpper() == "ESTADO_REGISTRO")
            {
                Valor = (int)value;
            }
            else
            {
                //Valor = (Int64?)value;
                Valor = System.Convert.ToInt64(value);
            }

            if (Valor == null || !Valor.HasValue) return null;

            switch (System.Convert.ToString(parameter).ToUpper())
            {
                case "PARAMETRO":
                    var ParamDato = RUV.I.InfoGeneral.ListaParametros
                      .FirstOrDefault(x => x.Id == Valor.Value);
                    if (ParamDato != null) Resultado = ParamDato.Nombre;
                    break;

                case "PAIS":
                    var DatoPais = RUV.I.InfoGeneral.ListaPaises
                      .FirstOrDefault(x => x.Id == Valor.Value);
                    if (DatoPais != null) Resultado = DatoPais.Nombre;
                    break;

                case "INDICATIVO_CIUDAD":
                    
                    var DatoMunTel = RUV.I.InfoGeneral.ListaMunicipiosTodos
                        .FirstOrDefault(x => x.Id == Valor.Value);

                    if (DatoMunTel != null && DatoMunTel.CodigoTelefono.HasValue)
                    {
                        Resultado = DatoMunTel.CodigoTelefono.Value.ToString();
                    }
                    break;

                case "INDICATIVO_PAIS":
                    var DatoPaisTel = RUV.I.InfoGeneral.ListaPaises
                        .FirstOrDefault(x => x.Id == Valor.Value);

                    if (DatoPaisTel != null && DatoPaisTel.CodigoTelefono.HasValue)
                    {
                        Resultado = DatoPaisTel.CodigoTelefono.Value.ToString();
                    }
                    else
                    {
                        Resultado = Valor.Value.ToString();
                    }
                    break;
                case "DEPARTAMENTO":
                    var DatoDepto = RUV.I.InfoGeneral.ListaDepartamentosTodos
                      .FirstOrDefault(x => x.Id == Valor.Value);
                    if (DatoDepto != null) Resultado = DatoDepto.Nombre;
                    break;

                case "MUNICIPIO":
                    var DatoMcpio = RUV.I.InfoGeneral.ListaMunicipiosTodos
                      .FirstOrDefault(x => x.Id == Valor.Value);
                    if (DatoMcpio != null) Resultado = DatoMcpio.Nombre;
                    break;
                case "ENTIDAD_MUNICIPIO":
                    var DatoEntMcpio = RUV.I.InfoGeneral.ListaEntidadesMunicipiosTodos
                      .FirstOrDefault(x => x.NId == Valor.Value);
                    if (DatoEntMcpio != null) Resultado = DatoEntMcpio.CNombreOtraEntidad;
                    break;
                case "ETNIA":
                    var Dato0 = RUV.I.InfoGeneral.ListaEtnias.
                      Where(x => x.Id == Valor.Value).FirstOrDefault();
                    if (Dato0 != null) Resultado = Dato0.Nombre;
                    break;

                case "GRUPOETNICOPORCOMUNIDAD":
                    // Dado un código de comunidad étnica, retorna el nombre del grupo étnico.
                    var Dato1 = (from ce in RUV.I.InfoGeneral.ListaComunidadesEtnicas
                                 join ge in RUV.I.InfoGeneral.ListaGruposEtnicos on ce.GrupoEtnicoId equals ge.Id
                                 where ce.Id == Valor.Value
                                 select ge).FirstOrDefault();
                    if (Dato1 != null) Resultado = Dato1.Nombre;
                    break;

                case "COMUNIDADETNICA":
                    var Dato2 = RUV.I.InfoGeneral.ListaComunidadesEtnicas.
                      Where(x => x.Id == Valor.Value).FirstOrDefault();
                    if (Dato2 != null) Resultado = Dato2.Nombre;
                    break;

                case "CONSECUTIVOAFECTADO":
                    var Persona =
                      RUV.I.DeclaracionActual.PersonasAfectadas.ListaPersonas
                      .FirstOrDefault(x => x.ID == Valor.Value);
                    if (Persona != null)
                        Resultado = Persona.NumeroConsecutivo.ToString();
                    else
                        Resultado = null;
                    break;

                case "NOMBRECOMPLETOAFECTADO":

                    var Afectado =
                      RUV.I.DeclaracionActual.PersonasAfectadas.ListaPersonas
                      .FirstOrDefault(x => x.ID == Valor.Value);
                    if (Afectado != null)
                    {
                        System.Diagnostics.Debug.WriteLine(
                          string.Format("\n{0}\n", Afectado.NombreCompleto));
                        Resultado = Afectado.NombreCompleto;
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine(
                             string.Format("\n{0}\n", Valor.Value.ToString()));
                        Resultado = null;
                    }
                        
                    break;

                case "TIPOENTORNO":
                    var TipoEntorno = RUV.I.InfoGeneral.ListaTiposPoblaciones
                      .FirstOrDefault(x => x.Id == Valor);
                    if (TipoEntorno == null)
                        Resultado = null;
                    else
                        Resultado = TipoEntorno.Nombre;
                    break;

                case "CATEGORIAGLOSA":
                    var CatGlosa = RUV.I.InfoGeneral.ListaCategoriasGlosa.FirstOrDefault(
                        x => x.Id == Valor);
                    if (CatGlosa != null)
                        Resultado = CatGlosa.Nombre;

                    break;

                case "CONCEPTOGLOSA":
                    var ConGlosa = RUV.I.InfoGeneral.ListaParametros.FirstOrDefault(
                        x => x.Id == Valor);
                    if (ConGlosa != null)
                        Resultado = ConGlosa.Nombre;

                    break;

                case "CATEGORIAINTENCIONGLOSA":
                    var CatiGlosa = RUV.I.InfoGeneral.ListaCategoriasIntentoGlosa.FirstOrDefault(
                        x => x.Id == Valor);
                    if (CatiGlosa != null)
                        Resultado = CatiGlosa.Nombre;
                    break;

                case "ESTADOGLOSA":
                    switch (Valor)
                    {
                        case 1:
                            Resultado = "Creada aún NO Atendida";
                            break;
                        case 2:
                            Resultado = "Ya Asignada NO atendida";
                            break;
                        case 3:
                            Resultado = "Atendida";
                            break;
                        case 4:
                            Resultado = "Glosa Perdida";
                            break;
                        case 5:
                            Resultado = "Eliminada";
                            break;

                        default: Resultado = "-";
                            break;
                    }
                    break;

                case "ESTADO_REGISTRO":
                    switch (Valor)
                    {
                        case (int)eEstadoRegistro.Eliminado:
                            Resultado = "Eliminado";
                            break;
                        case (int)eEstadoRegistro.Insertar:
                            Resultado = "Creado";
                            break;
                        case (int)eEstadoRegistro.Modificado:
                            Resultado = "Modificado";
                            break;
                        case (int)eEstadoRegistro.SinModificaciones:
                            Resultado = "Sin Cambios";
                            break;
                        default: Resultado = "-";
                            break;
                    }
                    break;





            }

            return Resultado;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }


    }
}
