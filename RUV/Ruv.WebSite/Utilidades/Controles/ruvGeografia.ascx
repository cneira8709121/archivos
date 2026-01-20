<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ruvGeografia.ascx.cs"
    Inherits="Utilidades_Controles_dpsGeografia" %>
    <%@ Register TagPrefix="ruvv" Src="~/Utilidades/Controles/ruvDropDownList.ascx" TagName="DropDownList" %>
    
<div style="text-align: center">
    <table border="1" style="border-collapse: collapse">
        <tr>
            <td colspan="2" class="dvHeader">
                <asp:Label ID="lblTitLugar" runat="server" Text="LUGAR" SkinID="lblBlanco"></asp:Label>
            </td>
            <td colspan="3" class="dvHeader">
                <asp:Label ID="lblEntorno" runat="server" Text="ENTORNO" SkinID="lblBlanco"></asp:Label>
            </td>
        </tr>
        <tr>
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
            <td>
                <ruvv:DropDownList ID="Departamento" runat="server" DataTextField="Nombre" DataValueField="Id" EsRequerido="false"  MensajeError="Seleccione el departamento" OnSelectIndexChange="ddl_SelectedIndexChanged" AutoPostBack="true" Width="200px" />
            </td>
            <td>
                <ruvv:DropDownList ID="Municipio" runat="server" DataTextField="Nombre" DataValueField="Id" EsRequerido="false"  MensajeError="Seleccione el municipio" OnSelectIndexChange="ddl_SelectedIndexChanged" AutoPostBack="true" Width="200px" />
            </td>
            <td>
                <ruvv:DropDownList ID="Entorno" runat="server" DataTextField="Nombre" DataValueField="Id" EsRequerido="false"  MensajeError="Seleccione el tipo entorno" OnSelectIndexChange="ddl_SelectedIndexChanged" AutoPostBack="true" Width="200px" />
            </td>
            <td>
                <ajax:ComboBox ID="LocCorr" runat="server" DataTextField="Nombre" DataValueField="Id" AppendDataBoundItems="true" Width="200px" />&nbsp;
            </td>
            <td>
                <ajax:ComboBox ID="BarrioVereda" runat="server" DataTextField="Nombre" DataValueField="Id" AppendDataBoundItems="true" Width="200px" />&nbsp;
            </td>
        </tr>
    </table>
</div>
