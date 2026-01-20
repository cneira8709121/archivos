<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" Inherits="_Default" Codebehind="Default.aspx.cs" %>
    <%@ Reference Control="~/ListaTareas/UCTarea.ascx" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    <Ruv:ListaDeTareas ID="UCListaTareas1" runat="server" />
</asp:Content>
