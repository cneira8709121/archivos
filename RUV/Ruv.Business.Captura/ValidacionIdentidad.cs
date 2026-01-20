using Elmah;
using Newtonsoft.Json;
using Ruv.Business.Captura.Declaracion;
using Ruv.Business.DTO.IdentidadPersona;
using Ruv.Data.General;
using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.Infrastructure.Crosscutting.Common.Entidades.Validacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ruv.Business.Captura
{
    public class ValidacionIdentidad
    {
        public ValidacionIdentidad()
        {

        }

        public List<Ruv.Infrastructure.Crosscutting.Common.Entidades.Validacion.clsPersonaIdentidad> ValidarPersonaRNEC(int idTipoToma, int idUsuario, int idTipoDoc, string numDocumento, string primerNombre, string segundoNombre,
            string primerApellido, string segundoApellido, string celular, string email, ref string cError)
        {
            string mail_encode = string.Empty;
            if (!string.IsNullOrEmpty(email))
            {
                byte[] byt = System.Text.Encoding.UTF8.GetBytes(email);
                mail_encode = Convert.ToBase64String(byt);
            }
            entValidacionIdentidad entValidacionIdentidad = new entValidacionIdentidad();
            List<Ruv.Business.DTO.IdentidadPersona.clsPersonaIdentidad> clsPersonaIdentidad = entValidacionIdentidad.ValidarPersonaRNECSServicio(idTipoToma, idUsuario, idTipoDoc, numDocumento, primerNombre, segundoNombre, primerApellido, segundoApellido, celular, mail_encode, ref cError);
            List<Ruv.Infrastructure.Crosscutting.Common.Entidades.Validacion.clsPersonaIdentidad> persona = new List<Infrastructure.Crosscutting.Common.Entidades.Validacion.clsPersonaIdentidad>();
            if (clsPersonaIdentidad != null)
            {

                foreach (var item in clsPersonaIdentidad)
                {
                    string preguntas = string.Empty;
                    Ruv.Business.DTO.IdentidadPersona.Preguntas resu = new Preguntas();
                    bool Deserializado = false;
                    List<clsPreguntasValidacion> preguntasValidacion = new List<clsPreguntasValidacion>();
                    while (!Deserializado)
                    {
                        try
                        {
                            preguntas = entValidacionIdentidad.PreguntasValidacion(item.Identificador, ref cError);

                            if (preguntas != null)
                            {
                                resu = JsonConvert.DeserializeObject<Ruv.Business.DTO.IdentidadPersona.Preguntas>(preguntas);
                                //Add Fecha Expedicion
                                List<clsOpcionesPreguntas> opcionesPreguntasFechaExpedicion = new List<clsOpcionesPreguntas>();
                                int tieneFechaExped = resu.FechaExpedicion.Count(x => x.Valida == true);
                                if (tieneFechaExped == 0)
                                    throw new Exception("No tiene fecha de expedicion");
                                foreach (var opciones in resu.FechaExpedicion)
                                {
                                    opcionesPreguntasFechaExpedicion.Add(new clsOpcionesPreguntas
                                    {
                                        Pregunta = "FechaExpedicion",
                                        PosibleOpcion = opciones.Opcion,
                                        Valida = opciones.Valida
                                    });
                                }
                                preguntasValidacion.Add(new clsPreguntasValidacion()
                                {
                                    Pregunta = "Valide la fecha de Expedición del Documento",
                                    OpcionesPreguntas = opcionesPreguntasFechaExpedicion
                                });
                                // Add Fecha de Nacimiento
                                int tieneFechaDeNacimiento = resu.FechaDeNacimiento.Count(x => x.Valida == true);
                                if (tieneFechaDeNacimiento == 0)
                                    throw new Exception("No tiene fecha de nacimiento");
                                List<clsOpcionesPreguntas> opcionesPreguntasFechaDeNacimiento = new List<clsOpcionesPreguntas>();
                                foreach (var opciones in resu.FechaDeNacimiento)
                                {
                                    opcionesPreguntasFechaDeNacimiento.Add(new clsOpcionesPreguntas
                                    {
                                        Pregunta = "FechaDeNacimiento",
                                        PosibleOpcion = opciones.Opcion,
                                        Valida = opciones.Valida
                                    });
                                }
                                preguntasValidacion.Add(new clsPreguntasValidacion()
                                {
                                    Pregunta = "Valide la fecha de nacimiento",
                                    OpcionesPreguntas = opcionesPreguntasFechaDeNacimiento
                                });
                                //Departamento de expedición
                                int tieneDepartamentoExpedicion = resu.DepartamentoExpedicion.Count(x => x.Valida == true);
                                if (tieneDepartamentoExpedicion == 0)
                                    throw new Exception("No tiene Departamento de expedición");
                                List<clsOpcionesPreguntas> opcionesPreguntasDepartamentoExpedicion = new List<clsOpcionesPreguntas>();
                                foreach (var opciones in resu.DepartamentoExpedicion)
                                {
                                    opcionesPreguntasDepartamentoExpedicion.Add(new clsOpcionesPreguntas
                                    {
                                        Pregunta = "DepartamentoExpedicion",
                                        PosibleOpcion = opciones.Opcion,
                                        Valida = opciones.Valida
                                    });
                                }
                                preguntasValidacion.Add(new clsPreguntasValidacion()
                                {
                                    Pregunta = "Valide el Departamento de expedión del documento",
                                    OpcionesPreguntas = opcionesPreguntasDepartamentoExpedicion
                                });
                                //Municipio de expedición
                                int tieneMunicipioExpedicion = resu.MunicipioExpedicion.Count(x => x.Valida == true);
                                if (tieneMunicipioExpedicion == 0)
                                    throw new Exception("No tiene Municipio de expedición");
                                List<clsOpcionesPreguntas> opcionesPreguntasMunicipioExpedicion = new List<clsOpcionesPreguntas>();
                                foreach (var opciones in resu.MunicipioExpedicion)
                                {
                                    opcionesPreguntasMunicipioExpedicion.Add(new clsOpcionesPreguntas
                                    {
                                        Pregunta = "MunicipioExpedicion",
                                        PosibleOpcion = opciones.Opcion,
                                        Valida = opciones.Valida
                                    });
                                }
                                preguntasValidacion.Add(new clsPreguntasValidacion()
                                {
                                    Pregunta = "Valide el Municipio de expedión del documento",
                                    OpcionesPreguntas = opcionesPreguntasMunicipioExpedicion
                                });
                            }

                            Deserializado = true;

                        }
                        catch (Exception ex)
                        {
                            Elmah.ErrorLog.GetDefault(null).Log(new Elmah.Error(ex));
                            preguntasValidacion = new List<clsPreguntasValidacion>();
                            Deserializado = false;
                        }
                    }

                    persona.Add(new Infrastructure.Crosscutting.Common.Entidades.Validacion.clsPersonaIdentidad()
                    {
                        PrimerNombre = item.PrimerNombre,
                        SegundoNombre = item.SegundoNombre,
                        PrimerApellido = item.PrimerApellido,
                        SegundoApellido = item.SegundoApellido,
                        Vigencia = item.Vigencia,
                        Resultado = item.Resultado,
                        Celular = celular,
                        Correo = email,
                        IdTipoDocumento = idTipoDoc,
                        NumeroDocumento = numDocumento,
                        PreguntasValidacion = preguntasValidacion,
                        TipoDeclaracion = idTipoToma
                    });


                }
            }
            else
            {
                Elmah.ErrorLog.GetDefault(null).Log(new Elmah.Error(new Exception("No se encontro la persona")));
            }
            return persona;
        }

        public bool EnviarValidacion(string numDocumento, string nombre, string celular, string correo, bool alCelular, ref string cError)
        {
            bool result = false;
            try
            {
                entMensajeSMS mensajeSMS = new entMensajeSMS();
                if (alCelular)
                {
                    mensajeSMS.EnviarSMS(new clsMensajeSMS()
                    {
                        Cedula = numDocumento,
                        Celular = celular,
                        NombrePersona = nombre,
                        Mensaje = "Codigo de Validación"
                    });
                }
                else
                {
                    mensajeSMS.EnviarCorreo(new clsMensajeCorreo()
                    {
                        Asunto = "RUV (Registro Unico de Victimas) - Verifique el codigo",
                        Mensaje = $"{nombre} el siguiente codigo es el que debe informar para ingresar al Registro Unico de Victimas (RUV): ",
                        Cedula = numDocumento,
                        Correo = correo,
                        NombrePersona = nombre
                    });
                }
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
                cError = ex.Message;
            }
            return result;
        }

        public bool ValidarCodigo(string numDocumento, string celular, string correo, string codigo, bool alCelular, ref string cError)
        {
            entMensajeSMS mensajeSMS = new entMensajeSMS();
            bool result = false;
            try
            {
                clsCodigoValidacion codigoValidacion = new clsCodigoValidacion()
                {
                    Cedula = numDocumento,
                    Codigo = codigo,
                };
                if (!alCelular)
                    codigoValidacion.Correo = correo;
                else
                    codigoValidacion.Celular = celular;
                result = mensajeSMS.ValidarCodigo(codigoValidacion);
            }
            catch (Exception ex)
            {
                result = false;
                cError = ex.Message;
            }
            return result;
        }

        public List<clsPersonaRNEC> BuscarPersonaRNEC(string numDocumento, string tipoDocumento)
        {
            Ruv.Data.General.entValidacionIdentidad validacionIdentidad = new Ruv.Data.General.entValidacionIdentidad();
            return validacionIdentidad.BuscarPersonaRNEC(numDocumento, tipoDocumento);
        }
    }
}
