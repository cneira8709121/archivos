
namespace Ruv.WPF.Captura
{

    #region DELEGADOS

    public delegate void EstadoRedChangedDelegate(eEstadoRed nuevoEstado);

    #endregion

    #region ENUMERACIONES

    /// <summary>
    /// Los posibles estados de la red
    /// </summary>
    public enum eEstadoRed
    {
        Disponible,
        NoDisponible,
        EnProcesoDeVerificacion
    }

    public enum eFondoSubTitulos
    {
        Normal,
        Inverso,
        FondoBlanco
    }

    public enum eTipoOpciones
    {
        Unica,
        Multiple
    }

    public enum eSeccionRegistro
    {
        H01_TomaDeclaracion,
        H02_PersonasAfectadas,
        H03_DescripcionHechos,
        H04_VerificacionProcedimiento,
        A01,
        A02,
        A03,
        A04,
        A05,
        A06,
        A07,
        A08,
        A09,
        A10,
        A11,
        A13,
        //ActoTerrorista,
        //Amenaza,
        //DelitosContraLaLibertad,
        //DesapariciónForzada,
        //DesplazamientoForzado,
        //Homocidio,
        //Masacre,
        //MinasAntipersonal,
        //Secuestro,
        //Tortura,
        //VinculacionNiños,
        //AbandonoODespojoForzado,
        Ninguna
    }

    public enum eEstadoIngreso
    {
        NoRequiereIngreso,
        IngresoIncompleto,
        IngresoCompleto
    }


    public enum eTipoOperacionRegistro
    {
        Editar,
        Insertar,
        Borrar,
        Ninguna
    }

    public enum eTipoPapel
    {
        Carta,
        Oficio
    }

    public enum eOrientacionPapel
    {
        Portrait,
        Landscape
    }

    public enum eTipoContenido
    {
        BloqueIndependiente,
        EncabezadoLista,
        DetalleLista,
        TextoLargo,
        EncajarEnPagina,
        FinalSeccion,
        FinalSeccionSinMarca,
        FinalDeclaracion
    }

    public enum eEstadoProcesoCola
    {
        /// <summary>
        /// El item está pendiente por ser transmitido.
        /// </summary>
        PendienteTransmitir,
        /// <summary>
        /// El item se está transmitiendo en este momento.
        /// </summary>
        Transmitiendo,
        /// <summary>
        /// El item fué transmitido exitosamente
        /// </summary>
        Transmitido,
        /// <summary>
        /// El item fué transmitido pero se reportó una inconsistencia que debe ser corregida.
        /// </summary>
        RequiereRevision,
        /// <summary>
        /// Estado indeterminado.
        /// </summary>
        Ninguno
    }

    public enum eResultadoValidacion
    {
        PasaGlosa,
        PasaValoracion,
        NoPasaValidaciones
    }

    #endregion

}
