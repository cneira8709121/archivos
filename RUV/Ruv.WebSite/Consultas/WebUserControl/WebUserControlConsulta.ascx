<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WebUserControlConsulta.ascx.cs" 
Inherits="Consultas_WebUserControl_WebUserControlConsulta"  %>
<table align="center">
    <tr>
        <td>
            <asp:Panel ID="FilterPanel" runat="server" Width="100%" DefaultButton="BtnConsulta">
                <table align="center" class="tblFiltro">
                    <tr align="center">
                        <td align="right">
                            <asp:Label ID="lblCedula" runat="server" Text="Numero de Cedula"></asp:Label>
                        </td>
                        <td align="left">
                            <ruv:TextBox ID="TxtNumeroCedula" runat="server" Width="200px" Numerico="true" EsRequerido="false" />
                        </td>
                    </tr>
                    <tr align="center">
                        <td align="right">
                            <asp:Label ID="lblNombre" runat="server" Text="Primer Nombre"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="TxtPrimerNombre" runat="server" Width="200px" />
                        </td>
                    </tr>
                    <tr align="center">
                        <td align="right">
                            <asp:Label ID="lblApellido" runat="server" Text="Primer Apellido"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="TxtPrimerApellido" runat="server" Width="200px" />
                        </td>
                    </tr>
                    <tr align="center">
                        <td align="right">
                            <asp:Label ID="lblFormulario" runat="server" Text="Numero de Formulario"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="TxtNumeroFormulario" runat="server" Width="200px" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                        <asp:Button ID="BtnConsulta" runat="server" Text="Consultar" onclick="BtnConsulta_Click"  />
                        </td>
                    </tr>                
                </table>
                </asp:Panel>
        </td>
    </tr>
</table>
