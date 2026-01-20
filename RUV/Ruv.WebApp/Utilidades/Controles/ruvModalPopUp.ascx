<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="Utilidades_Controles_dpsModalPopUp" Codebehind="ruvModalPopUp.ascx.cs" %>
<asp:HiddenField ID="UrlRedireccionHidden" runat="server" />
<asp:Panel ID="pnlModal" runat="server" SkinID="PanelmodalPopup" Width="360px">
    <asp:Panel ID="PPModal" runat="server">
        <table width="100%">
            <tr>
                <td>
                    <asp:Panel ID="pnlTituloPrograma" runat="server" SkinID="pnlTitulo">
                        <asp:Label ID="lblTitulo" runat="server" Text="RUV VALORACION"></asp:Label>
                    </asp:Panel>
                </td>
            </tr>
            <tr runat="server" id="FilaLabel">
                <td >
                    <asp:Label ID="lblMensajeModalpoup" runat="server" Text="Consultando..."></asp:Label>
                </td>
            </tr>
            <tr runat="server" id="FilaTextBox" visible="false">
                <td>
                    <asp:TextBox ID="txtMensaje" runat="server" Width="100%" Height="200px"  CausesValidation="false"
                        TextMode="MultiLine" ></asp:TextBox>
                </td>                
            </tr>
            <tr>
                <td>
                    <asp:Image ID="imgCargando" runat="server" SkinID="imgCargando" Width="100%" Height="20"
                        Visible="false" />
                </td>
            </tr>
        </table>
        <div id="dvBotones" runat="server" visible="false" style="text-align:center">
            <asp:Button ID="OkButton" runat="server" Text="Aceptar" OnClick="OkButton_Click" CausesValidation="false" />
            <asp:Button ID="CancelButton" runat="server" Text="Cancelar" OnClick="CancelButton_Click" CausesValidation="false" />
        </div>
    </asp:Panel>
</asp:Panel>
<asp:LinkButton ID="LinkButton1_Modalpopup" runat="server"></asp:LinkButton>
<ajax:ModalPopupExtender ID="programmaticModalPopup" runat="server" SkinID="PopUp" PopupDragHandleControlID="pnlTituloPrograma"
    TargetControlID="LinkButton1_Modalpopup" PopupControlID="pnlModal" BehaviorID="mpGeneralBehavior">
</ajax:ModalPopupExtender>
