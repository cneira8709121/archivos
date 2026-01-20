<%@ Control Language="C#" AutoEventWireup="true" Inherits="Valoracion_Valoracion_Controles_CamposActoAdministrativo" Codebehind="CamposActoAdministrativo.ascx.cs" %>
<asp:Panel ID="pnlValoresAA" runat="server" SkinID="PanelmodalPopup" ClientIDMode="Static">
    <div style="width:500px">
        <asp:Panel ID="pnlTituValoresAA" runat="server" SkinID="pnlTitulo">
            <asp:Label ID="lblTituValoresAA" runat="server" Text="VALORES ACTOS ADMINISTRATIVOS"></asp:Label>
        </asp:Panel>
        <div>
            <asp:Label ID="lblTipo" Text="Tipo Acto Administrativo" runat="server" />
            <asp:RadioButtonList ID="rbtLTipoActo" runat="server" RepeatDirection="Horizontal" ClientIDMode="Static" onclick="CambioTipoAA()">
                <asp:ListItem Text="Inclusión" Value="1"></asp:ListItem>
                <asp:ListItem Text="No Inclusión" Value="2"></asp:ListItem>
                <asp:ListItem Text="Mixto" Value="3" Selected="True"></asp:ListItem>
            </asp:RadioButtonList>
        </div>
        <div id="dvIncluidos" class="valoresPanel">
            <asp:Label ID="lblMovivacionInclusion" Text="Motivación Inclusión" runat="server" CssClass="smallbold"></asp:Label>
            <div>
                <ruv:TextBox ID="txtMotivacionInclusion" runat="server" TextMode="MultiLine" Width="99%" EsRequerido="false" CausesValidation="false" />
            </div>
        </div>
        <div id="dvNoIncluido" class="valoresPanel">
            <asp:Label ID="lblMotivacionNoInclusion" Text="Motivación No Inclusión" runat="server" CssClass="smallbold"></asp:Label>
            <div>
                <ruv:TextBox ID="txtMotivacionNoInclusion" runat="server" TextMode="MultiLine" Width="99%" EsRequerido="false" CausesValidation="false" />
            </div>
        </div>
        <div id="dvArticulo1" class="valoresPanel">
            <asp:Label ID="lblResuelveInclusion" Text="Resuelve Articulo 1" runat="server" CssClass="smallbold"></asp:Label>
            <div>
                <ruv:TextBox ID="txtResuelveArticulo1" runat="server" TextMode="MultiLine" Width="99%" EsRequerido="false" CausesValidation="false" />
            </div>
        </div>
        <div id="dvArticulo2" class="valoresPanel">
            <asp:Label ID="lblResuelveNoInclusion" Text="Resuelve Articulo 2" runat="server" CssClass="smallbold"></asp:Label>
            <div>
                <ruv:TextBox ID="txtResuelveArticulo2" runat="server" TextMode="MultiLine" Width="99%" EsRequerido="false" CausesValidation="false" />
            </div>
        </div>
        <div>
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" CausesValidation="false" />
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CausesValidation="false" />
        </div>
    </div>
</asp:Panel>
<asp:LinkButton ID="lnkValoresAA" runat="server"></asp:LinkButton>
<ajax:ModalPopupExtender ID="mpopUpValoresAA" runat="server" SkinID="PopUp" TargetControlID="lnkValoresAA"
    DropShadow="true" BehaviorID="mpopUpValoresAABehavior" PopupControlID="pnlValoresAA"
    CancelControlID="btnCancelar" OnCancelScript="CerrarVentanaCamposAA()">
</ajax:ModalPopupExtender>
<script type="text/javascript" src='<%= Page.ResolveClientUrl("~/JScripts/Shared/ext.nicedit.min.js") %>'></script>
<script type="text/javascript" src='<%= Page.ResolveClientUrl("~/JScripts/Valoracion/ruv.valoracion-valoresactos.js") %>'></script>
<script type="text/javascript">
//<![CDATA[
    ruv.valoracion_valoresactos.initialize();
//]]>
</script>