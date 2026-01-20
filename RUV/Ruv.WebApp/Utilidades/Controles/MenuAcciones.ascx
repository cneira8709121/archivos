<%@ Control Language="C#" AutoEventWireup="true" Inherits="Utilidades_Controles_MenuAcciones" Codebehind="MenuAcciones.ascx.cs" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:Table ID="tblAcciones" runat="server">
            <asp:TableRow ID="tblRow" runat="server">
            </asp:TableRow>
        </asp:Table>
    </ContentTemplate>
</asp:UpdatePanel>
