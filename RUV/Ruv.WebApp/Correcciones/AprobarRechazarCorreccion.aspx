<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" Inherits="Correcciones_AprobarRechazarCorreccion" Codebehind="AprobarRechazarCorreccion.aspx.cs" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Scripts" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .style1
        {
            font-weight: bold;
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <table style="width:100%; text-align:left;">
        <tr>
            <td class="style1">
                <asp:Label ID="lblCampos" runat="server" Text="Campos"></asp:Label>
            </td>
            <td class="style1">
                <asp:Label ID="lblValoresActuales" runat="server" Text="Valores Actuales"></asp:Label>
            </td>
            <td class="style1">
                <asp:Label ID="lvlValoresNuevos" runat="server" Text="Valores Nuevos"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblPrimerNombre" runat="server" Text="Primer nombre:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txbPrimerNombre" runat="server" 
                    Enabled="False"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="txbPrimerNombre0" runat="server" 
                    Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblSegundoNombre" runat="server" Text="Segundo nombre:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txbSegundoNombre" runat="server" 
                    Enabled="False"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="txbSegundoNombre0" runat="server" 
                    Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblPrimerApellido" runat="server" Text="Primer apellido:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txbPrimerApellido" runat="server" 
                    Enabled="False"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="txbPrimerApellido0" runat="server" 
                    Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblSegundoApellido" runat="server" Text="Segundo apellido:"></asp:Label></td>
            <td>
                <asp:TextBox ID="txbSegundoApellido" runat="server" 
                    Enabled="False"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="txbSegundoApellido0" runat="server" 
                    Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblTipoDocumento" runat="server" Text="Tipo de documento:"></asp:Label></td>
            <td>
                <asp:TextBox ID="txtTipoDocumento" runat="server" 
                    Enabled="False"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="txtTipoDocumento0" runat="server" 
                    Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblNumeroDocumento" runat="server" Text="N&uacute;mero de documento:"></asp:Label></td>
            <td>
                <asp:TextBox ID="txbNumeroDocumento" runat="server" 
                    Enabled="False"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="txbNumeroDocumento0" runat="server" 
                    Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblFechaNacimiento" runat="server" Text="Fecha de nacimiento:"></asp:Label></td>
            <td>
                <asp:TextBox ID="txtFechaNacimiento" runat="server" 
                    Enabled="False"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="txtFechaNacimiento0" runat="server" 
                    Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblDireccion" runat="server" Text="Direcci&oacute;n:"></asp:Label></td>
            <td>
                <asp:TextBox ID="txbDireccion" runat="server"
                    Enabled="False"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="txbDireccion0" runat="server"
                    Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblTelefono" runat="server" Text="Tel&eacute;fono:"></asp:Label></td>
            <td>
                <asp:TextBox ID="txbTelefono" runat="server"
                    Enabled="False"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="txbTelefono0" runat="server"
                    Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblCorreoElectronico" runat="server" Text="Correo electr&oacute;nico:"></asp:Label></td>
            <td>
                <asp:TextBox ID="txbCorreoElectronico" runat="server" 
                    Enabled="False"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="txbCorreoElectronico0" runat="server" 
                    Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblGenero" runat="server" Text="G&eacute;nero:"></asp:Label></td>
            <td>
                <asp:TextBox ID="txtGenero" runat="server" 
                    Enabled="False"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="txtGenero0" runat="server" 
                    Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblEtnia" runat="server" Text="&Eacute;tnia:"></asp:Label></td>
            <td>
                <asp:TextBox ID="txtEtnia" runat="server" 
                    Enabled="False" Width="285px"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="txtEtnia0" runat="server" 
                    Enabled="False" Width="320px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblSubEtnia" runat="server" Text="Subetnia:"></asp:Label></td>
            <td>
                <asp:TextBox ID="txtSubEtnia" runat="server" 
                    Enabled="False" Width="281px"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="txtSubEtnia0" runat="server" 
                    Enabled="False" Width="325px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblDiscapacidades" runat="server" Text="Discapacidades:"></asp:Label></td>
            <td>
                <ruv:CheckBoxList ID="ChkBoxListDiscpacidades" runat="server" Valor="2135" Source="Parametros" Enabled="false" />
            </td>
            <td>
                <ruv:CheckBoxList ID="ChkBoxListDiscpacidades0" runat="server" Valor="2135" Source="Parametros" Enabled="false"/>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Button ID="btnDescargarAdjunto" runat="server" 
                    Text="Descargar archivo adjunto" onclick="btnDescargarAdjunto_Click" />
            </td>
        </tr>
    </table>
    <table style="width:100%; text-align:left;">
        <tr>
            <td>
                <asp:Label ID="lblObservaciones" runat="server" Text="Observaciones"></asp:Label>
                <asp:TextBox ID="txtObservaciones" runat="server" Height="80px" TextMode="MultiLine" 
                    Width="99%"></asp:TextBox>
            </td>
        </tr>
    </table>
    <p>
        <asp:Button runat="server" ID="btnAceptarCorreccion" Text="Aceptar Corrección" 
            onclick="btnAceptarCorreccion_Click" />
        <asp:Button runat="server" ID="btnRechazarCorreccion" 
            Text="Rechazar Corrección" onclick="btnRechazarCorreccion_Click" />
    </p>
</asp:Content>

