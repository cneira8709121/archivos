<%@ Control Language="C#" AutoEventWireup="true" Inherits="Utilidades_Controles_dpsFiltros" Codebehind="ruvFiltros.ascx.cs" %>
<%@ Register TagPrefix="ruvv" Src="~/Utilidades/Controles/ruvTextBox.ascx" TagName="TextBox" %>
<%@ Register TagPrefix="ruvv" Src="~/Utilidades/Controles/ruvTextCalendar.ascx" TagName="TextCalendar" %>
<div>
    <center>
        <asp:Label ID="lblError" runat="server" SkinID="lblError"></asp:Label>
    </center>
    <table width="100%" >
        <tr style="text-align:left">
            <td style="width:100px">
                <asp:Label ID="lblFiltro" runat="server" Text="Filtro Por:"></asp:Label>
            </td>
            <td style="width:250px">
                <asp:DropDownList ID="ddlFiltro" runat="server" Width="200px" DataTextField="Descripcion" DataValueField="Id" OnSelectedIndexChanged="ddlFiltro_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true" />
            </td>
            <td style="width:600px">
                <table id="tbValoresTexto" runat="server" visible="false" >
                    <tr>
                        <td style="width:100px; vertical-align:middle" >
                            <asp:Label ID="lblValorTexto1" runat="server" Text="Valor:"></asp:Label>
                        </td>
                        <td style="width:200px; vertical-align:middle" >
                            <ruvv:TextBox ID="txtValor1" runat="server" />
                        </td>
                        <td style="width:100px; vertical-align:middle" >
                            <asp:Label ID="lblValor2" runat="server" Text="Segundo Valor:"></asp:Label>
                        </td>
                        <td style="width:200px; vertical-align:middle" >
                            <ruvv:TextBox ID="txtValor2" runat="server" />
                        </td>
                    </tr>
                </table>
                <table id="tbValoresFecha" runat="server" visible="false" border="0" style="vertical-align:middle">
                    <tr>
                        <td style="width:100px; vertical-align:middle">
                            <asp:Label ID="lblValorFecha1" runat="server" Text="Valor:"></asp:Label>
                        </td>
                        <td style="width:200px; vertical-align:middle">
                            <ruvv:TextCalendar ID="txtFecha1" runat="server" EsRequerido="true" />
                        </td>
                        <td style="width:100px; vertical-align:middle">
                            <asp:Label ID="lblValorFecha2" runat="server" Text="Segundo Valor:"></asp:Label>
                        </td>
                        <td style="width:200px; vertical-align:middle">
                            <ruvv:TextCalendar ID="txtFecha2" runat="server" EsRequerido="true" />
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" CausesValidation="false" Visible="false" ValidationGroup="vgFiltro"
                    OnClick="btnFiltrar_Click" OnClientClick="return ShowModConsult(null, null, 'vgFiltro')" />
                <asp:Button ID="btnReset" runat="server" Text="Restablecer" CausesValidation="false" OnClick="btnReset_Click" />
            </td>
        </tr>
        <tr style="text-align:left">
            <td style="width:100px">
                <asp:Label ID="lblOrder" runat="server" Text="Ordenar Por:" Visible="false"></asp:Label>
            </td>
            <td style="width:250px">
                <asp:DropDownList ID="ddlOrder" runat="server" Width="200px" 
                    DataTextField="Descripcion" DataValueField="Id" AutoPostBack="true" 
                    AppendDataBoundItems="true"  Visible="false" />
            </td>
            <td style="width:600px">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
<%--    <asp:HiddenField ID="hfFilterExpression" runat="server" />--%>
</div>
