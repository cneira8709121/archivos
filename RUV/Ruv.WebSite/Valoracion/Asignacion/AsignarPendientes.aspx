<%@ Page Title="Asignar" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeFile="AsignarPendientes.aspx.cs" Inherits="Valoracion_Asignacion_AsignarPendientes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div>
        <center>
            <asp:Label ID="lblMensajeAsignar" runat="server" Text="Asignar Todas las declaraciones pendientes a valoración"></asp:Label>
            <asp:Panel ID="pblAsignar" runat="server" SkinID="pnlImagenAsignar">
                <asp:Image ID="imgCargar" runat="server" SkinID="imgCargandoGrande" Visible="false" />
            </asp:Panel>
            <asp:Button ID="btnAsignar" runat="server" Text="Asignar" OnClick="btnAsignar_Click" />
        </center>
    </div>
</asp:Content>
