<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="Utilidades_Controles_dpsGeografia" CodeBehind="ruvGeografia.ascx.cs" %>
<%@ Register TagPrefix="ruvv" Src="~/Utilidades/Controles/ruvDropDownList.ascx" TagName="DropDownList" %>
<%@ Register TagPrefix="ruvv" Src="~/Utilidades/Controles/ruvTextBox.ascx" TagName="TextBox" %>

<div style="text-align: center">
    <table border="1" style="border-collapse: collapse">
        <tr>
            <td colspan="3" class="dvHeader">
                <asp:Label ID="lblTitLugar" runat="server" Text="LUGAR" SkinID="lblBlanco"></asp:Label>
            </td>
            <td colspan="3" class="dvHeader">
                <asp:Label ID="lblEntorno" runat="server" Text="ENTORNO" SkinID="lblBlanco"></asp:Label>
            </td>
        </tr>
        <tr>
            <td id="lPais" runat="server" class="dvHeader">
                <asp:Label ID="lblPais" runat="server" Text="Pais" SkinID="lblBlanco" />
            </td>
            <td class="dvHeader">
                <asp:Label ID="lblDepartamento" runat="server" Text="Departamento" SkinID="lblBlanco"></asp:Label>
            </td>
            <td class="dvHeader">
                <asp:Label ID="lblMunicipio" runat="server" Text="Municipio" SkinID="lblBlanco"></asp:Label>
            </td>
            <td class="dvHeader">
                <asp:Label ID="lblTipoEnt" runat="server" Text="Tipo" SkinID="lblBlanco"></asp:Label>
            </td>
            <td class="dvHeader">
                <asp:Label ID="lblCorLoc" runat="server" SkinID="lblBlanco"></asp:Label>
            </td>
            <td class="dvHeader">
                <asp:Label ID="lblBarVere" runat="server" SkinID="lblBlanco"></asp:Label>
            </td>
        </tr>
        <tr valign="middle">
            <td id="cPais" runat="server">
                <select id="ddlPais" runat="server" style="width: 150px" />
                <asp:HiddenField ID="hfPais" runat="server" />
            </td>
            <td id="cDpto" runat="server">
                <select id="ddlDepartamento" runat="server" style="width: 150px" />
                <asp:HiddenField ID="hfDpto" runat="server" />
            </td>
            <td id="cMun" runat="server">
                <select id="ddlMunicipio" runat="server" style="width: 150px" />
                <asp:HiddenField ID="hfMun" runat="server" />
            </td>
            <td>
                <ruvv:DropDownList ID="Entorno" runat="server" DataTextField="Nombre" DataValueField="Id" EsRequerido="false" MensajeError="Seleccione el tipo entorno"  Width="200px" />
            </td>
            <td>
                <ruvv:TextBox ID="LocCorr" runat="server" Width="200" EsRequerido="false" />
                &nbsp;
            </td>
            <td>
                <ruvv:TextBox ID="BarrioVereda" runat="server" Width="200" EsRequerido="false" />
                &nbsp;
            </td>
        </tr>
    </table>
</div>
