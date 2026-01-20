using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

/// <summary>
/// Descripción breve de clsTipoFiltro
/// </summary>
/// 
[DataContract]
public class clsTipoFiltro
{
	public clsTipoFiltro()
	{
		
	}

    private int id;
    [DataMember]
    public int Id
    {
        get { return id; }
        set { id = value; }
    }
    private string nombre;
    [DataMember]
    public string Nombre
    {
        get { return nombre; }
        set { nombre = value; }
    }
    private string descripcion;
    [DataMember]
    public string Descripcion
    {
        get { return descripcion; }
        set { descripcion = value; }
    }
    private TypeCode tipoDato;
    [DataMember]
    public TypeCode TipoDato
    {
        get { return tipoDato; }
        set { tipoDato = value; }
    }
    private Proceso proceso;
    [DataMember]
    public Proceso Proceso
    {
        get { return proceso; }
        set { proceso = value; }
    }

}