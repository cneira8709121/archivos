using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Asignar
/// </summary>
public class Asignar
{

	public Asignar()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}
    private HttpContext _context;

    public HttpContext Context
    {
        get { return _context; }
        set { _context = value; }
    }
    private int usuarioId;

    public int UsuarioId
    {
        get { return usuarioId; }
        set { usuarioId = value; }
    }

    public void AsignarDeclaraciones()
    {
        ValoracionService ObjValoracion = new ValoracionService();
        bool AsignOk = ObjValoracion.AsignarTodos(usuarioId);
        _context.Application[ConstantesAplicacion.ASIGNAR] = null;
    }
}