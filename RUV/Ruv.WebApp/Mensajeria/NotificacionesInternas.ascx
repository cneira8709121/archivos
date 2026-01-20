<%@ Control Language="C#" AutoEventWireup="true" Inherits="Mensajeria_WebUserControl" Codebehind="NotificacionesInternas.ascx.cs" %>
  <asp:Panel ID="PanelNotificaciones" runat="server" CssClass="ContentBox">
    <h2>notificaciones pendientes</h2>
    <div class="autoscrollbar" style="max-height: 60px;">
        <asp:Repeater ID="NotificacionesList" runat="server">
            <ItemTemplate>
                <asp:ImageButton ID="IgnoreNotificationButton" runat="server" ImageUrl="~/App_Themes/RUVTheme/Imagenes/Cerrar.png" ToolTip="Ignorar Notificación" CssClass="lefticon" OnClick="IgnoreNotificationButton_Click" CommandName="ID" CommandArgument='<%# Eval("ID") %>' />
                <asp:LinkButton ID="ViewNotificationButton" runat="server" CssClass="notificacionText" OnClick="ViewNotificationButton_click" CommandName="Descripcion" CommandArgument='<%# Eval("cDescripcion") %>'><%# DataBinder.Eval(Container.DataItem, "cTexto")%></asp:LinkButton>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Panel>