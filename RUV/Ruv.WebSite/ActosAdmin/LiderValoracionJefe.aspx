<%@ Page Title="Aprobar Valoración" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeFile="LiderValoracionJefe.aspx.cs" Inherits="ActosAdmin_Notificacion" ValidateRequest="false" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Src="~/Valoracion/Valoracion/Controles/ValoracionHistoricoPopUp.ascx" TagName="HistoricoValoracion" TagPrefix="ivan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="/JScripts/jquery-1.8.2.js" type="text/javascript"></script>
    <script src="/JScripts/jquery.expander.js" type="text/javascript"></script>+
    <script type="text/javascript" src="/JScripts/Valoracion/JHistorico.js"></script>
<%--    <script type="text/javascript">
        $(document).ready(function() {
            $('.expander dd:eq(0)').expander({
            //expandPrefix: '<img src="expand.png">',
            expandText: '[More]',
            //userCollapsePrefix: '<img src="collapse.png" border="0">',
            userCollapseText: '[Hide]',
            });
        });
    </script>--%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Panel ID="PanelAccion" runat="server" CssClass="ContentBox">
        <h2>aprobar o rechazar valoración</h2>
        <span>Observaciones</span>
        <asp:TextBox ID="ObservacionLiderValoracion" runat="server" TextMode="MultiLine" Height="50px" MaxLength="1000" placeholder="Observaciones de Aprobación o Rechazo de la Valoración"></asp:TextBox>  
        <div class="ActionsBox">
            <asp:Button ID="btnAprobar" runat="server" Text="Aprobar" OnClick="btnAprobar_Click" />
            <asp:Button ID="btnRechazar" runat="server" Text="Rechazar" OnClick="btnRechazar_Click" />
            <asp:Button ID="btnDescargarDocumento" runat="server" Text="Descargar Documento" OnClick="btnDescargarDocumento_Click"/>
            <asp:Button ID="btnConsultarHistorial" runat="server" Text="Consultar Historial" OnClientClick="ruv.objects.displayAsPopup($('.controlInformationPopup')); return false;" />
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" />
        </div>
    </asp:Panel>
    <asp:Panel ID="InfoGeneral" runat="server" CssClass="DataBox">
        <h2>información de declaración</h2>
        <asp:DetailsView ID="dwDetalleDeclaracion" runat="server" AutoGenerateRows="False" DataSourceID="ObjectDataSourceResumenVal">
            <Fields>
                <%--<asp:BoundField DataField="nIdDeclaracion" HeaderText="ID Declaracion" SortExpression="nIdDeclaracion" />--%>
                <asp:BoundField DataField="CNumeroFormulario" HeaderText="Numero de formulario" SortExpression="CNumeroFormulario" />
                <asp:BoundField DataField="dFechaDeclaracion" DataFormatString="{0:MMMM d / yyyy}" HeaderText="Fecha Declaracion" SortExpression="dFechaDeclaracion" />
                <asp:BoundField DataField="cNombreValorador" HeaderText="Nombre Valorador" SortExpression="cNombreValorador" />
                <asp:BoundField DataField="cNombreDeclarante" HeaderText="Nombre y Apellido Declarante" SortExpression="cNombreDeclarante" />
                <asp:BoundField DataField="cTipoDocumento" HeaderText="Tipo Documento Declarante" SortExpression="cTipoDocumento" />
                <asp:BoundField DataField="nDocumentoIdentidad" HeaderText="Documento Declarante" SortExpression="nDocumentoIdentidad" />
                <asp:BoundField DataField="cEstadoActualProceso" HeaderText="Estado Actual Proceso" SortExpression="cEstadoActualProceso" />
                <asp:BoundField DataField="dFechaValoracion" DataFormatString="{0:MMMM d / yyyy}"  HeaderText="Fecha Valoracion" SortExpression="dFechaValoracion" />
            </Fields>
        </asp:DetailsView>
    </asp:Panel>
    <asp:Panel ID="InfoDetalles" runat="server" CssClass="DataBox">
        <h2></h2>
        <asp:GridView ID="gridResumenValidacion" runat="server" DataSourceID="ObjectDataSourceResumenVal" SkinID="GridViewConPaginacion" AutoGenerateColumns="false" DataKeyNames="NIdDeclaracion">
            <Columns>
                <asp:BoundField DataField="cNombreVictima" HeaderText="Víctima" />
                <asp:BoundField DataField="cTipodocumentoVictima" HeaderText="Tipo Documento" />
                <asp:BoundField DataField="nDocumentoVictima" HeaderText="Documento" />
                <asp:BoundField DataField="cEstadoValoracion" HeaderText="Estado Valoración" />
                <asp:BoundField DataField="cEstado" HeaderText="Estado" />
                <asp:BoundField DataField="cHechoVictimizante" HeaderText="Hecho Victimizante" />
                <asp:BoundField DataField="cInfraccionDerechoHumano" HeaderText="Infracción al DIH" />
                <asp:BoundField DataField="cPrincipio" HeaderText="Principio / Causal" />
            </Columns>
            <EmptyDataTemplate>
                No hay registros que coincidan con los criterios de busqueda
            </EmptyDataTemplate>
        </asp:GridView>
        <asp:ObjectDataSource ID="ObjectDataSourceResumenVal" runat="server" TypeName="DataSourceResumenValoracion" SelectMethod="GetData" OnObjectCreated="ObjectDataResumenVal_ObjectCreated">
        </asp:ObjectDataSource>
    </asp:Panel>
    <ruv:ModalPopUp ID="mpuMensaje" runat="server" DropShadow="true" MostrarBotones="false" MostrarImagen="true" VisibleBotonCancelar="false" BehaviorID="MessageBehavior" />
    <div id="hiddenHistoricoPopup" style="display: none" class="controlInformationPopup" runat="server">
        <ivan:HistoricoValoracion ID="historicoValoracion" runat="server" />
    </div>
 </asp:Content>