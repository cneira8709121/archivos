<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ModalPopUp.ascx.cs" Inherits="Ruv.WebSite.Utilidades.Controles.ModalPopUp" %>
<asp:HiddenField ID="UrlRedireccionHiddenField" runat="server" />
<asp:Panel ID="ModalPanel" runat="server" SkinID="PanelmodalPopup" Width="360px">
    <asp:Panel ID="TituloPanel" runat="server" SkinID="pnlTitulo">
        <asp:Label ID="TituloLabel" runat="server" Text="Título" />
    </asp:Panel>
    <asp:Label ID="MensajeLabel" runat="server" Text="Mensaje"></asp:Label>
    <div class="ActionsBox">
        <asp:Button ID="OkButton" runat="server" Text="Aceptar" OnClick="OkButton_Click" CausesValidation="false" />
        <asp:Button ID="CancelarButton" runat="server" Text="Cancelar" OnClick="CancelarButton_Click" CausesValidation="false" />
    </div>
</asp:Panel>
<asp:LinkButton ID="LinkButton1_Modalpopup" runat="server"></asp:LinkButton>
<ajax:ModalPopupExtender ID="programmaticModalPopup" runat="server" SkinID="PopUp" TargetControlID="LinkButton1_Modalpopup" PopupDragHandleControlID="TituloPanel" PopupControlID="ModalPanel" BehaviorID="programmaticModalPopupBehavior"></ajax:ModalPopupExtender>
