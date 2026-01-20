using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Data;
using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.Infrastructure.Crosscutting.Common.General;
using ServiceStack.Text;

namespace Ruv.WPF.Captura.Converters
{
    /// <summary>
    /// Retorna los parámetros para un tipo de parámetro.
    /// </summary>
    public class ListaParametrosConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {


                if (RUV.I.Util.EnModoDeDiseño) return null;

                string NombreTipoParametro = System.Convert.ToString(parameter);
                if (string.IsNullOrWhiteSpace(NombreTipoParametro)) return null;

                string[] parametros;
                string parametro1 = string.Empty;
                //Parametro adicional para identificar control que llama al converter
                if (NombreTipoParametro.Contains("|"))
                {
                    parametros = NombreTipoParametro.Split(System.Convert.ToChar("|"));
                    NombreTipoParametro = parametros[0];
                    parametro1 = parametros[1];
                }

                eTipoParametros TipoParametro;
                if (!Enum.TryParse<eTipoParametros>(NombreTipoParametro, out TipoParametro))
                    return null;

                // El régimen especial tiene el nulo como "Ninguno", los demás como "Sin información".
                var ParNulo = RUV.I.InfoGeneral.ParametroNulo;

                // Diego Alvarez - 13/09/2013 - Se incluye ninguno en la base de datos
                //if (TipoParametro == eTipoParametros.RegimenEspecial)
                //{
                //  ParNulo.ElementAt(0).Nombre = "ninguno";
                //}

                var param = new List<clsParametroGeneral>();

                if (TipoParametro == eTipoParametros.TipoSecuestro
                    || TipoParametro == eTipoParametros.AfiliaciónASalud
                    || TipoParametro == eTipoParametros.Relacion
                    || TipoParametro == eTipoParametros.EstadoCivil
                    || TipoParametro == eTipoParametros.RegimenEspecial
                    || TipoParametro == eTipoParametros.Genero
                    || TipoParametro == eTipoParametros.OrientacionSexual
                    || TipoParametro == eTipoParametros.DiscapacidadEnActividades
                    || TipoParametro == eTipoParametros.TipoDeAccidente
                    || TipoParametro == eTipoParametros.EstadoActualLote
                    || TipoParametro == eTipoParametros.TipoTomaDeclaracion)
                {
                    ParNulo = ParNulo.Concat(RUV.I.InfoGeneral.ListaParametros
                    .Where(x => x.Tipo == TipoParametro)
                    .OrderBy(x => x.Numero).ToList());
                }
                else if (TipoParametro == eTipoParametros.TipoDeDocumentoDeIdentidad && parametro1 == "TipoDocTutor")
                {
                    ParNulo = ParNulo.Concat(RUV.I.InfoGeneral.ListaParametros
                        .Where(x => x.Tipo == TipoParametro && (x.Id == (int)eTipoDocumento.CedulaCiudadania || x.Id == (int)eTipoDocumento.CedulaExtranjeria))
                        .OrderBy(x => x.Nombre).ToList());
                }
                else if (TipoParametro == eTipoParametros.Entidades)
                {
                    ParNulo = ParNulo.Concat(RUV.I.InfoGeneral.ListaParametros
                        .Where(x => x.Tipo == TipoParametro)
                        .OrderBy(x => x.Nombre).ToList());
                }
                else
                {
                    ParNulo = ParNulo.Concat(RUV.I.InfoGeneral.ListaParametros
                      .Where(x => x.Tipo == TipoParametro)
                      .OrderBy(x => x.Nombre).ToList());
                }

                List<clsParametroGeneral> ResultadoParametro = new List<clsParametroGeneral>();
                ResultadoParametro = RUV.I.InfoGeneral.ParametroNulo.ToList();
                foreach (var item in ParNulo)
                {
                    if (!string.IsNullOrEmpty(item.Valor))
                    {
                        try
                        {
                            var variableDiscapacidades = JsonSerializer.DeserializeFromString<clsParametrosExtendidosVersionFUD>(item.Valor);
                            var VersionFUD = RUV.I.DeclaracionActual.VersionFUD;
                            if (VersionFUD == 1 && variableDiscapacidades.fud1)
                            {
                                ResultadoParametro.Add(item);
                            }
                            if (VersionFUD == 2 && variableDiscapacidades.fud2)
                            {
                                ResultadoParametro.Add(item);
                            }
                        }
                        catch
                        {
                            Console.Write("No tiene parametro extendidos de FUD para el listado de parametros " + TipoParametro.ToString());
                        }
                    }
                }
                if (ResultadoParametro.Count > 1)
                    ParNulo = ResultadoParametro.OrderBy(x => x.Numero);

                return ParNulo;
            }
            catch (Exception)
            {

                return RUV.I.InfoGeneral.ParametroNulo;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
