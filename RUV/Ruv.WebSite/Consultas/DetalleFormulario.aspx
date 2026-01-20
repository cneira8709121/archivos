<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="DetalleFormulario.aspx.cs" Inherits="Consultas_DetalleFormulario" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <ruv:ModalPopUp ID="mpuMensaje" runat="server" DropShadow="true" MostrarBotones="true" MostrarImagen="false" VisibleBotonCancelar="false" />
    <asp:Panel ID="Panel2" runat="server" BackColor="White" Width="1200px">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <table align="center" width="90%">
                    <tr>
                        <td align="center">
                            <asp:DetailsView ID="dwDetalleDeclaracion" runat="server" AutoGenerateRows="False">
                                <Fields>
                                    <asp:BoundField DataField="CNumeroFormulario" HeaderText="Numero de formulario" SortExpression="CNumeroFormulario" />
                                    <asp:BoundField DataField="CNombresApellidosDeclarante" HeaderText="Nombre y Apellido Declarante" SortExpression="CNombresApellidosDeclarante" />
                                    <asp:BoundField DataField="CTipoDocumentoDeclarante" HeaderText="Tipo Documento Declarante" SortExpression="CTipoDocumentoDeclarante" />
                                    <asp:BoundField DataField="CDocumentoDeclarante" HeaderText="Documento Declarante" SortExpression="CDocumentoDeclarante" />
                                    <asp:TemplateField HeaderText="Estado Actual Proceso">
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "CEstadoActualProceso")%>
                                            <img runat="server" src="../App_Themes/RUVTheme/Imagenes/Buscar.png" alt='<%# DataBinder.Eval(Container.DataItem, "CEstadoActualProceso")%>' title='<%# DataBinder.Eval(Container.DataItem, "CEstadoActualProcesotooltip")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Fields>
                            </asp:DetailsView>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            <table class="tblLeyenda" cellspacing="0">
                                <thead>
                                    <tr>
                                        <td colspan="2">Estados de las declaraciones</td>
                                    </tr>
                                </thead>
                                <tr>
                                    <td>Radicado</td>
                                    <td>Solicitud que ha sido recibida por la UARIV que cumple con los requisitos de Ley
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Radicación Pendiente por Verificar</td>
                                    <td>
                                        Solicitud que se encuentra en trámite de verificación datos de distribución y 
                                        recepción del FUD
                                    </td>
                                <tr>
                                    <td>Radicado Pendiente Captura</td>
                                    <td>Solicitud recibida en la UARIV que no ha iniciado proceso de digitación del FUD
                                    </td>
                                </tr>
                                <tr>
                                    <td>Radicación Pendiente Critica N</td>
                                    <td>Solicitud que está en proceso de verificación si el FUD reúne los requisitos para tener validez jurídica
                                    </td>
                                </tr>
                               <tr>
                                    <td>Radicado Inicia Captura</td>
                                    <td>Solicitud que está en trámite de digitación en la UARIV
                                    </td>
                                </tr>
                                <tr>
                                    <td>Captura Pendiente Validar</td>
                                    <td>Solicitud que está en proceso de verificación campos faltantes del FUD
                                    </td>
                                </tr>
                                <tr>
                                    <td>Valoración Pendiente Por Asignar</td>
                                    <td>Solicitud que finalizó digitación y en trámite de asignación al proceso de valoración
                                    </td>
                                </tr>
                                <tr>
                                    <td>Pendiente por Valorar</td>
                                    <td>FUD digitado en proceso de análisis de reconocimiento de la condición de víctima, asignado en la lista de tareas de un usuario con perfil de Valorador
                                    </td>
                                </tr>
                                <tr>
                                    <td>Inicia Valoración</td>
                                    <td>FUD que está asignado a un valorador y está en trámite de terminación del proceso de valoración en el sistema
                                    </td>
                                </tr>
                                <tr>
                                    <td>Pendiente por Notificar</td>
                                    <td>Solicitud con estado de valoración en el Sistema, pendiente por ser Notificada al declarante
                                    </td>
                                </tr>
                                <tr>
                                    <td>No Valorado - Devuelto</td>
                                    <td>Solicitud  que no cumple con los requisitos de ley y se devuelve al Ministerio Público para que de respuesta sobre las inconsistencias presentadas
                                    </td>
                                </tr>
                                <tr>
                                    <td>Valorada pendiente aprobación acto administrativo</td>
                                    <td>Declaración con estado de valoración en proceso aprobación Acto Administrativo
                                    </td>
                                </tr>
                                <tr>
                                    <td>Valoración pendiente firma acto administrativo</td>
                                    <td> Declaración con acto administrativo pendiente de firma
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Repeater ID="HechosVictimizantesRepeater" runat="server" OnItemDataBound="HechosVictimizantesRepeater_ItemDataBound">
                                <ItemTemplate>
                                    <br />
                                    <asp:Label ID="HechoVictimizanteLabel" runat="server" />
                                    <asp:GridView ID="GridViewDetalle" runat="server" AllowPaging="false" AutoGenerateColumns="False" SkinID="GridViewConPaginacion" OnPageIndexChanging="GridViewDetalle_PageIndexChanging" Width="100%">
                                        <Columns>
                                            <asp:BoundField DataField="DHecho" HeaderText="Fecha del Hecho" DataFormatString="{0:d}" />
                                            <asp:BoundField DataField="CTipoDocumentoVictima" HeaderText="Tipo Documento" ItemStyle-Wrap="false" />
                                            <asp:BoundField DataField="CDocumentoVictima" HeaderText="Documento" />
                                            <asp:BoundField DataField="CNombresApellidosVictima" HeaderText="Nombre y Apellidos" ItemStyle-Wrap="false" />
                                            <asp:BoundField DataField="CEstadoValoracion" HeaderText="Valoración" ItemStyle-Wrap="false" />
                                            <asp:BoundField DataField="CResultadoValoracion" HeaderText="Estado"/>
                                            <asp:BoundField DataField="DValoracion" HeaderText="Fecha Valoracion" DataFormatString="{0:d}" />
                                        </Columns>
                                    </asp:GridView>
                                </ItemTemplate>
                            </asp:Repeater>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table class="tblLeyenda" cellspacing="0">
                                <thead>
                                    <tr>
                                        <td colspan="2">Estados de Valoracion</td>
                                    </tr>
                                </thead>
                                <tr>
                                    <td>Incluido</td>
                                    <td>Persona a la que se le reconoce la condición de victima
                                    </td>
                                </tr>
                                <tr>
                                    <td>No Incluido</td>
                                    <td>Persona a la que NO se le reconoce la condición de victima
                                    </td>
                                </tr>
                               <tr>
                                    <td>En Valoración</td>
                                    <td>Estado en el Registro que indica que la solicitud de inscripción se encuentra en trámite de verificación por parte del equipo de valoración.
                                    </td>
                                </tr>
                                <tr>
                                    <td>Excluido</td>
                                    <td>Persona que pierde el reconocimiento de su condición de víctima por causales contempladas en las normas vigentes
                                    </td>
                                </tr>
                                <tr>
                                    <td>No Valorado - Devuelto</td>
                                    <td>Solicitud  que no cumple con los requisitos de ley y se devuelve al Ministerio Público para que de respuesta sobre las inconsistencias presentadas
                                    </td>
                                </tr>
                                <tr>
                                    <td>Afectado - No Valorado</td>
                                    <td>Persona que según el FUD no sufre victimización y luego de realizar el análisis de la valoración se mantiene en dicho estado
                                    </td>
                                </tr>
                                <tr>
                                    <td>No Afectado - No Valorado</td>
                                    <td>Persona que según el FUD no es Víctima
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>

