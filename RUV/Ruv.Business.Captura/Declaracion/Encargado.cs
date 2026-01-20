using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Ruv.Data;
using Ruv.Data.Reconocimiento;
using Ruv.Infrastructure.Crosscutting.Common;
using Ruv.Infrastructure.Crosscutting.Common.Entidades;
using Ruv.Infrastructure.Crosscutting.Common.Entidades.FirmaDeclaracion;

namespace Ruv.Business.Captura.Declaracion
{
    public class Encargado
    {

        #region Guardar
        public static void Guardar(TBDECLARACIONES declaracionData, ref clsDeclaracion declaracionView, DbTransaction tran)
        {
            Ruv.Data.Reconocimiento.entDeclaraciones entDecl = new entDeclaraciones();
            Ruv.Data.TBENCARGADO encargadoData = new Ruv.Data.TBENCARGADO();

            clsTomaDeclaracion_Encargado encargadoView = declaracionView.TomaDeclaracion.Encargado;
            if (encargadoView.RepresentanteTipoDocumento == null)
                return;
            Encargado.ParseViewToData(encargadoView, declaracionView.VerificacionProcedimiento.FuncionarioCargo, ref encargadoData);

            //Guardar Encargado
            entEncargado entEncarg = new entEncargado();
            switch (encargadoView.EstadoRegistro)
            {
                case eEstadoRegistro.Insertar:
                    entEncarg.setData(encargadoData, tran);
                    if (declaracionView.Firmas != null) {
                        var firma = declaracionView.Firmas.FirstOrDefault(x => x.firmaOwner == FirmaOwner.TUTOR);
                        if (firma != null)
                            entEncarg.insertEncargadoFirma(encargadoData.ID, firma.firma, tran);
                    }

                    encargadoView.ID = encargadoData.ID;
                    break;
                case eEstadoRegistro.Modificado:
                    entEncarg.updateData(encargadoData, tran);
                    if (declaracionView.Firmas != null) {
                        var firma = declaracionView.Firmas.FirstOrDefault(x => x.firmaOwner == FirmaOwner.TUTOR);
                        if (firma != null)
                            entEncarg.updateEncargadoFirma(encargadoData.ID, firma.firma, tran);
                    }

                    break;
                case eEstadoRegistro.Eliminado:
                    break;
                case eEstadoRegistro.SinModificaciones:
                    break;
            }

            //Guardar tiposEncargado
            List<Ruv.Data.TBDECLARACION_ENCARGADO> tiposEncargado = new List<TBDECLARACION_ENCARGADO>();

            Encargado.ParseViewToData_TiposEncargado(encargadoView, declaracionData.ID, tiposEncargado);

            entTipoEncargado entTipEnc = new entTipoEncargado();
            foreach (TBDECLARACION_ENCARGADO tipoEncargadoData in tiposEncargado)
            {
                switch (encargadoView.EstadoRegistro)
                {
                    case eEstadoRegistro.Insertar:
                        entTipEnc.setData(tipoEncargadoData, tran);
                        break;
                    case eEstadoRegistro.Modificado:
                        entTipEnc.updateData(tipoEncargadoData, tran);
                        break;
                    case eEstadoRegistro.Eliminado:
                        break;
                    case eEstadoRegistro.SinModificaciones:
                        break;
                }
            }

            //Reiniciar EstadoRegistro
            if (encargadoView.EstadoRegistro != eEstadoRegistro.Eliminado)
                encargadoView.EstadoRegistro = eEstadoRegistro.SinModificaciones;
        }

        public static void ParseViewToData(clsTomaDeclaracion_Encargado encargadoView, string cargoEncargado, ref Ruv.Data.TBENCARGADO encargadoData)
        {
            //Encargado o Representant H1-4
            //H1-4
            encargadoData.ID = encargadoView.ID ?? -1;

            encargadoData.IDPARAMTIPODOCUMENTO = encargadoView.RepresentanteTipoDocumento;

            encargadoData.PRIMERNOMBRE = encargadoView.RepresentantePrimerNombre;
            encargadoData.SEGUNDONOMBRE = encargadoView.RepresentanteSegundoNombre;
            encargadoData.PRIMERAPELLIDO = encargadoView.RepresentantePrimerApellido;
            encargadoData.SEGUNDOAPELLIDO = encargadoView.RepresentanteSegundoApellido;
            encargadoData.NUMERODOCUMENTO = encargadoView.RepresentanteNumeroDocumento;
            encargadoData.DIRECCION = encargadoView.RepresentanteDireccion;
            encargadoData.TELEFONO = encargadoView.RepresentanteTelefono;
            encargadoData.CARGO = cargoEncargado;
        }
       
        public static void ParseViewToData_TiposEncargado(clsTomaDeclaracion_Encargado encargadoView, int id_declaracion, List<Ruv.Data.TBDECLARACION_ENCARGADO> tiposEncargadosData)
        {
            Ruv.Data.TBDECLARACION_ENCARGADO tipoEncargadoData = new Ruv.Data.TBDECLARACION_ENCARGADO();

            //Encargado o Representant H1-4
            //H1-4
            tipoEncargadoData.TBENCARGADO = new TBENCARGADO();
            tipoEncargadoData.TBENCARGADO.ID = encargadoView.ID ?? -1;

            tipoEncargadoData.TBDECLARACIONES = new TBDECLARACIONES();
            tipoEncargadoData.TBDECLARACIONES.ID = id_declaracion;

            tipoEncargadoData.IDPARAMTIPOENCARGADO = encargadoView.RepresentanteTipo;
            tipoEncargadoData.ENTIDADCOMPETENTE = encargadoView.RepresentanteTipoAutoridadCompetente;

            tiposEncargadosData.Add(tipoEncargadoData);
        }        
        #endregion

        
        #region Obtener Datos
        public static void Obtener(TBDECLARACIONES declaracionData, int id_encargado, ref clsDeclaracion declaracionView)
        {
            entEncargado entEncarg = new entEncargado();

            TBENCARGADO encargadoData = entEncarg.getData(id_encargado, (int)declaracionView.ID);

            clsTomaDeclaracion_Encargado encargadoView = new clsTomaDeclaracion_Encargado();
            string cargoEncargado = null;
            Encargado.ParseDataToView(encargadoData, ref cargoEncargado, ref encargadoView);
            
            List<TBDECLARACION_ENCARGADO> tiposEncargado = new List<TBDECLARACION_ENCARGADO>();
            //TODO: tiposEncargado = sp_getTiposEncargado(id_encargado);
            Encargado.ParseDataToView_TiposEncargado(tiposEncargado, ref encargadoView);

            declaracionView.TomaDeclaracion.Encargado = encargadoView;

            declaracionView.VerificacionProcedimiento.FuncionarioCargo = cargoEncargado;
        }

        public static void ParseDataToView(TBENCARGADO encargadoData, ref string cargoEncargado, ref clsTomaDeclaracion_Encargado encargadoView)
        {
            encargadoView.ID = encargadoData.ID;
            encargadoView.EstadoRegistro = eEstadoRegistro.SinModificaciones;
            encargadoView.RepresentanteTipoDocumento = encargadoData.IDPARAMTIPODOCUMENTO;
            encargadoView.RepresentanteNumeroDocumento = encargadoData.NUMERODOCUMENTO ?? "";
            encargadoView.RepresentantePrimerNombre = encargadoData.PRIMERNOMBRE;
            encargadoView.RepresentantePrimerNombre = encargadoData.PRIMERNOMBRE;
            encargadoView.RepresentanteSegundoNombre = encargadoData.SEGUNDONOMBRE;
            encargadoView.RepresentantePrimerApellido = encargadoData.PRIMERAPELLIDO;
            encargadoView.RepresentanteSegundoApellido = encargadoData.SEGUNDOAPELLIDO;
            encargadoView.RepresentanteDireccion = encargadoData.DIRECCION;
            encargadoView.RepresentanteTelefono = encargadoData.TELEFONO;
                        
            if (encargadoData.TBDECLARACION_ENCARGADO.Count > 0)
            {
                encargadoView.RepresentanteTipo = encargadoData.TBDECLARACION_ENCARGADO.ElementAt(0).IDPARAMTIPOENCARGADO;
                encargadoView.RepresentanteTipoAutoridadCompetente = encargadoData.TBDECLARACION_ENCARGADO.ElementAt(0).ENTIDADCOMPETENTE;
            }

            cargoEncargado = encargadoData.CARGO;
        }

        public static void ParseDataToView_TiposEncargado(List<TBDECLARACION_ENCARGADO> tiposEncargado, ref clsTomaDeclaracion_Encargado encargadoView)
        {
            if (tiposEncargado.Count < 1)
                return;
            TBDECLARACION_ENCARGADO tipoEncargado = tiposEncargado.First();
            encargadoView.RepresentanteTipo = tipoEncargado.IDPARAMTIPOENCARGADO;
            encargadoView.RepresentanteTipoAutoridadCompetente = tipoEncargado.ENTIDADCOMPETENTE;

        }
        #endregion
    }
}
