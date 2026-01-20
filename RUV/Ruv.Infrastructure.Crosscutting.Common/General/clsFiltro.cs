using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

/// <summary>
/// Descripción breve de clsFiltro
/// </summary>
/// 
[DataContract]
public class clsFiltro
{
    public clsFiltro()
    {

    }

    private int filtroPor;
    [DataMember]
    public int FiltroPor
    {
        get { return filtroPor; }
        set { filtroPor = value; }
    }

    private string texto1;
    [DataMember]
    public string Texto1
    {
        get { return texto1; }
        set { texto1 = value; }
    }

    private string texto2;
    [DataMember]
    public string Texto2
    {
        get { return texto2; }
        set { texto2 = value; }
    }
    private string nombreDeclarante;
    [DataMember]
    public string NombreDeclarante
    {
        get { return nombreDeclarante; }
        set { nombreDeclarante = value; }
    }
    private string documentoDeclarante;
    [DataMember]
    public string DocumentoDeclarante
    {
        get { return documentoDeclarante; }
        set { documentoDeclarante = value; }
    }
    private string numeroFormulario;
    [DataMember]
    public string NumeroFormulario
    {
        get { return numeroFormulario; }
        set { numeroFormulario = value; }
    }
    private string totalHv;
    [DataMember]
    public string TotalHv
    {
        get { return totalHv; }
        set { totalHv = value; }
    }
    private string departamento;
    [DataMember]
    public string Departamento
    {
        get { return departamento; }
        set { departamento = value; }
    }
    private string municipio;
    [DataMember]
    public string Municipio
    {
        get { return municipio; }
        set { municipio = value; }
    }
    private string entidad;
    [DataMember]
    public string Entidad
    {
        get { return entidad; }
        set { entidad = value; }
    }

    private DateTime? fechaInicial;
    [DataMember]
    public DateTime? FechaInicial
    {
        get { return fechaInicial; }
        set { fechaInicial = value; }
    }
    private DateTime? fechaFinal;
    [DataMember]
    public DateTime? FechaFinal
    {
        get { return fechaFinal; }
        set { fechaFinal = value; }
    }

    private string regimenEspecial;
    [DataMember]
    public string RegimenEspecial
    {
        get { return regimenEspecial; }
        set { regimenEspecial = value; }
    }

    private string etnia;
    [DataMember]
    public string Etnia
    {
        get { return etnia; }
        set { etnia = value; }
    }

    private string genero;
    [DataMember]
    public string Genero
    {
        get { return genero; }
        set { genero = value; }
    }

    private string estado;
    [DataMember]
    public string Estado
    {
        get { return estado; }
        set { estado = value; }
    }

    private DateTime? fechaVencimientoInicial;
    [DataMember]
    public DateTime? FechaVencimientoInicial
    {
        get { return fechaVencimientoInicial; }
        set { fechaVencimientoInicial = value; }
    }
    private DateTime? fechaVencimientoFinal;
    [DataMember]
    public DateTime? FechaVencimientoFinal
    {
        get { return fechaVencimientoFinal; }
        set { fechaVencimientoFinal = value; }
    }
    private DateTime? fecha1;
    [DataMember]
    public DateTime? Fecha1
    {
        get { return fecha1; }
        set { fecha1 = value; }
    }
    private DateTime? fecha2;
    [DataMember]
    public DateTime? Fecha2
    {
        get { return fecha2; }
        set { fecha2 = value; }
    }
}