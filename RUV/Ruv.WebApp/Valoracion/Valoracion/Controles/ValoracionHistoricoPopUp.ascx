<%@ Control Language="C#" AutoEventWireup="true" Inherits="Valoracion_Valoracion_Controles_ValoracionHistoricoPopUp" Codebehind="ValoracionHistoricoPopUp.ascx.cs" %>
    
    <script src="/JScripts/jquery-1.8.2.js" type="text/javascript"></script>
    <script src="/JScripts/jquery.expander.js" type="text/javascript"></script>
    <script type="text/javascript" src="/JScripts/Valoracion/JHistorico.js"></script>

<asp:Panel ID="Panel1" runat="server">
    <asp:GridView ID="grdHistorico" runat="server" SkinID="GridViewSinSeleccion" AutoGenerateColumns="False" Width="100%" ShowHeaderWhenEmpty="true">
        <Columns>
            <asp:BoundField DataField="nId" HeaderText="NID" visible="false" />
            <asp:BoundField DataField="cObservacion" HeaderText="Observación" />
            <asp:BoundField DataField="nUsuario" HeaderText="Usuario" />
            <%--<asp:BoundField DataField="nIdValoracion" HeaderText="Valoración" />--%>
            <%--<asp:BoundField DataField="cValoracion" HeaderText="Motivación Valoración" />--%>
            <asp:BoundField DataField="dFechaActualizacion" HeaderText="Fecha de Actualización" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" />
        </Columns>
    </asp:GridView>

    <div class="expandible">
        <asp:Label ID="lblTituloMotivacion" runat="server" Text="MOTIVACION"/>
        <dd class="maxHeightOverflowScroll"><asp:Label ID="lblMotivacion" runat="server"/></dd>
    </div>

    <div class="ActionsBox">
        <asp:Button ID="btnCerrar" runat="server" Text="Cerrar" />
    </div>
    
</asp:Panel>