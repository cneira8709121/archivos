<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NotificacionDetalle.aspx.cs" MasterPageFile="~/Site.Master" Inherits="Notificaciones_NotificacionDetalle" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <table width="100%">
        <tr>
            <td align="center">
                <asp:DetailsView ID="dtvDetalleNotificacion" SkinID="DisplayEasy" runat="server" DataSourceID="ObjectDataSource" AutoGenerateRows="False" ondatabound="dtvDetalleNotificacion_DataBound">
                    <Fields>
                        <asp:BoundField DataField="NumeroFormulario" HeaderText="Declaración"/>
                        <asp:BoundField DataField="cNombreDeclarante" HeaderText="Declarante"/>
                        <asp:TemplateField HeaderText="Documento">
                            <ItemTemplate>
                                <span class="altInfoBig noWrap"><%# Eval("cTipoDocumento")%></span>
                                <%# Eval("cDocumentoIdentidad")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="cEstadoDeclaracion" HeaderText="Estado Declaracion"/>
                        <asp:TemplateField HeaderText="Citación">
                            <ItemTemplate>
                                <%# ((Eval("cPais") as string).ToLowerInvariant().Trim() == "colombia" ? string.Empty : Eval("cPais") + " - ") + Eval("cDepartamento") + " - " + Eval("cMunicipio")%>
                                <span class="altInfoBig"><%# Eval("cDireccionNotificacion")%></span>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="cTelefonoNotificacion" HeaderText="Teléfono Registrado"/>
                        <asp:BoundField DataField="CUBICACIONNOTIFICACION" HeaderText="Punto Notificación"/>
                    </Fields>
                </asp:DetailsView>
            </td>
        </tr>
    </table>
    <asp:ObjectDataSource ID="ObjectDataSource" runat="server" TypeName="Ruv.WebSite.DataSources.Notificaciones.DataSourceNotificacionDetalle" EnablePaging="false" SelectMethod="DetalleData"  OnObjectCreated="ObjectDataSource_ObjectCreated" UpdateMethod="AprobarNotificacion"></asp:ObjectDataSource>
    
    <div class="ActionsBox">
        <asp:Button ID="btnAprobar" Text="Aprobar Envio de Notificación" runat="server" OnClick="btnAprobar_Click" />
        <asp:Button ID="BtnDescargarCitacion" runat="server" Text="Descargar Citacion" OnClick="BtnDescargarCitacion_Click" />
        <asp:Button ID="BtnDescargarAviso" runat="server" Text="Descargar Aviso" 
            onclick="BtnDescargarAviso_Click" />
        <asp:Button ID="BtnDescargarResolucion" runat="server" 
            Text="Descargar Resolución" onclick="BtnDescargarResolucion_Click"/>
        <asp:Button ID="btnAtras" runat="server" Text="Volver" onclick="btnAtras_Click" />
    </div>

</asp:Content>