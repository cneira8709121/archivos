<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" Inherits="Correcciones_SolicitudCorreccion" Codebehind="SolicitudCorreccion.aspx.cs" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnAceptar" />
        </Triggers>
        <ContentTemplate>
            <table style="width:100%; text-align:left;">
                <tr>
                    <td><asp:CheckBox ID="chkPrimerNombre" runat="server" 
                            oncheckedchanged="chkChackedChanged" AutoPostBack="True" /></td>
                    <td>Primer nombre:</td>
                    <td colspan="2">
                        <asp:TextBox ID="txbPrimerNombre" runat="server"
                            Enabled="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td><asp:CheckBox ID="chkSegundoNombre" runat="server" 
                            oncheckedchanged="chkChackedChanged" AutoPostBack="True" /></td>
                    <td>Segundo nombre:</td>
                    <td colspan="2">
                        <asp:TextBox ID="txbSegundoNombre" runat="server" 
                            Enabled="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td><asp:CheckBox ID="chkPrimerApellido" runat="server" 
                            oncheckedchanged="chkChackedChanged" AutoPostBack="True" /></td>
                    <td>Primer apellido:</td>
                    <td colspan="2">
                        <asp:TextBox ID="txbPrimerApellido" runat="server" 
                            Enabled="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td><asp:CheckBox ID="chkSegundoApellido" runat="server" 
                            oncheckedchanged="chkChackedChanged" AutoPostBack="True" /></td>
                    <td>Segundo apellido:</td>
                    <td colspan="2">
                        <asp:TextBox ID="txbSegundoApellido" runat="server" 
                            Enabled="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td><asp:CheckBox ID="chkTipoDocumento" runat="server" 
                            oncheckedchanged="chkChackedChanged" AutoPostBack="True" /></td>
                    <td>Tipo de Documento:</td>
                    <td colspan="2">
                        <ruv:DropDownList ID="ddlTipoDocumento" runat="server" Enabled="False" 
                            Valor="21" Source="Parametros">
                        </ruv:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td><asp:CheckBox ID="chkNumeroDocumento" runat="server" 
                            oncheckedchanged="chkChackedChanged" AutoPostBack="True" /></td>
                    <td>N&uacute;mero de documento:</td>
                    <td colspan="2">
                        <asp:TextBox ID="txbNumeroDocumento" runat="server" 
                            Enabled="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td><asp:CheckBox ID="chkFechaNacimento" runat="server" 
                            oncheckedchanged="chkChackedChanged" AutoPostBack="True" /></td>
                    <td>Fecha de nacimiento:</td>
                    <td colspan="2"><ruv:TextCalendar ID="txbFechaNacimiento" runat="server" EsRequerido="false" 
                            MensajeError="Indique la fecha de ocurrencia del hecho" Enabled="False"/></td>
                </tr>
                <tr>
                    <td><asp:CheckBox ID="chkDireccion" runat="server" 
                            oncheckedchanged="chkChackedChanged" AutoPostBack="True" /></td>
                    <td>Direcci&oacute;n:</td>
                    <td colspan="2">
                        <asp:TextBox ID="txbDireccion" runat="server"
                            Enabled="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td><asp:CheckBox ID="chkTelefono" runat="server" 
                            oncheckedchanged="chkChackedChanged" AutoPostBack="True" /></td>
                    <td>Tel&eacute;fono:</td>
                    <td colspan="2">
                        <asp:TextBox ID="txbTelefono" runat="server"
                            Enabled="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td><asp:CheckBox ID="chkCorreoElectronico" runat="server" 
                            oncheckedchanged="chkChackedChanged" AutoPostBack="True" /></td>
                    <td>Correo electr&oacute;nico:</td>
                    <td colspan="2">
                        <asp:TextBox ID="txbCorreoElectronico" runat="server" 
                            Enabled="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td><asp:CheckBox ID="chkGenero" runat="server" 
                            oncheckedchanged="chkChackedChanged" AutoPostBack="True" /></td>
                    <td>G&eacute;nero:</td>
                    <td colspan="2">
                        <ruv:DropDownList ID="ddlGenero" runat="server" Enabled="False" Valor="24" 
                            Source="Parametros">
                        </ruv:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox ID="chkFallecido" runat="server" OnCheckedChanged="chkChackedChanged" AutoPostBack="True" />
                    </td>
                    <td>
                        Fallecido 
                    </td>
                    <td>
                        <asp:CheckBox ID="chkEsFallecido" runat="server" OnCheckedChanged="chkEsFallecido_CheckedChanged" Enabled="false" AutoPostBack="true" />
                    </td>
                    <td>
                        Número de Registro de Defuncíon
                    </td>
                    <td>
                        <asp:TextBox ID="txtNroRegDefuncion" runat="server"  Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td><asp:CheckBox ID="chkEtnia" runat="server" oncheckedchanged="chkChackedChanged" AutoPostBack="True" /></td>
                    <td>&Eacute;tnia:</td>
                    <td>
                        <ruv:DropDownList ID="ddlEtnia" runat="server" Enabled="False" Valor="31" Source="Parametros" OnSelectIndexChange="ddlEtnia_SelectIndexChange" AutoPostBack="true">
                        </ruv:DropDownList>
                    </td>
                    <td>
                        <ruv:DropDownList ID="ddlSubEtnia" runat="server" Enabled="False" Source="SubEtnias">
                        </ruv:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td><asp:CheckBox ID="chkDiscapacidades" runat="server" 
                            oncheckedchanged="chkChackedChanged" AutoPostBack="True" /></td>
                    <td>Discapacidades:</td>
                    <td colspan="2">
                        <ruv:CheckBoxList ID="cblDiscapacidades" runat="server" Enabled="False" 
                            Valor="2135" Source="Parametros" OnSelectIndexChange="cblSelectIndexChange" AutoPostBack="True">
                        </ruv:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:FileUpload ID="fuAdjunto" runat="server" />
                        <br />
                        <asp:RegularExpressionValidator runat="server" ID="valfuAdjunto" ControlToValidate="fuAdjunto"
                               ErrorMessage="El archivo adjunto solo puede ser de tipo: (.jpg, .tif, .pdf, .zip)" 
                            ValidationExpression="^(.+)(.jpg|.tif|.pdf|.JPG|.TIF|.PDF|.zip|.ZIP)$" 
                            Display="Dynamic" ForeColor="Red" />
                    </td>
                </tr>
            </table>
            <table style="width: 100%;">
                <tr>
                    <td align="right">
                        <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" onclick="btnClick" CausesValidation="true" />
                    </td>
                    <td align="left">
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CausesValidation="false"
                            onclick="btnClick" />
                    </td>
                </tr>
            </table>
            <asp:ObjectDataSource ID="odsCorrecciones" runat="server"></asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>