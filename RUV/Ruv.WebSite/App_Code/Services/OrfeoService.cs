using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Ruv.Infrastructure.Crosscutting.Common.Entidades.Orfeo;
using util = Ruv.Infrastructure.Crosscutting.Utilities;
using resx = Ruv.Infrastructure.Crosscutting.Resources;
using buss = Ruv.Business.Orfeo;
using dto = Ruv.Business.DTO.Orfeo;

public class OrfeoService : IOrfeoService
{
    public string GeneraCodigoOrfeo(Dignatario dig, Radicado rad, Direccion dir, Evento evt, ref string cError)
	{
        if (dig == null || rad == null || dir == null || evt == null) return null;
        buss::Services.IManageOrfeo iOrfeo = (buss::Services.IManageOrfeo)util::Spring.GetService(resx::Dependencias.Objetos.OrfeoBusiness);
        return iOrfeo.GeneraCodigoOrfeo
                (
                    new dto::Dignatario
                    {
                        CNombreDeclarante = dig.CNombreDeclarante,
                        CPrimerApellido = dig.CPrimerApellido,
                        CSegundoApellido = dig.CSegundoApellido,
                        CCedula = dig.CCedula,
                        CDireccion = dig.CDireccion,
                        CTelefono = dig.CTelefono,
                        CEntidad = dig.CEntidad,
                        NIdDepartamento = dig.NIdDepartamento,
                        NIdMunicipio = dig.NIdMunicipio,
                        CEmail = dig.CEmail
                    },
                    new dto::Radicado
                    {
                        NDepartamentoRadicado = rad.NDepartamentoRadicado,
                        NDepartamentoDestino = rad.NDepartamentoDestino,
                        NCodigoUsuario = rad.NCodigoUsuario,
                        NCodigoUsuarioDestino = rad.NCodigoUsuarioDestino,
                        CAsunto = rad.CAsunto
                    },
                    new dto::Direccion
                    {
                        coddir = dir.coddir,
                        dirnombre = dir.dirnombre,
                    },
                    new dto::Evento
                    {
                        deprad = evt.deprad,
                        codiusu = evt.codiusu
                    }, ref cError
                );
	}
}