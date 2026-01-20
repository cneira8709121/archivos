<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EntregaNotificacionesDetalle.aspx.cs"  MasterPageFile="~/Site.Master" Inherits="Notificaciones_EntregaNotificaciones" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Panel ID="pblseparador" runat="server" SkinID="pnlTitulo">
        <asp:Label ID="lblTitulo" runat="server" Text="Notificación Entregada (Citado)" SkinID="lblSubTitulo" />
    </asp:Panel>
    <table width="100%">
        <tr>
            <td align="center">
                <asp:DetailsView ID="dtvDetalleNotificacion" SkinID="DisplayEasy"  runat="server" DataSourceID="ObjectDataSource" AutoGenerateRows="False">
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
    <asp:ObjectDataSource ID="ObjectDataSource" runat="server" TypeName="Ruv.WebSite.DataSources.Notificaciones.DataSourceNotificacionDetalle" EnablePaging="false" SelectMethod="DetalleData" OnObjectCreated="ObjectDataSource_ObjectCreated"></asp:ObjectDataSource>
    
    <div class="ActionsBox">
        <fieldset class="ActionSegment">
            <legend>Descargar Formatos</legend>
            <asp:Button ID="BtnDescargarResolucion" runat="server" Text="Descargar Resolucion" OnClick="BtnDescargarResolucion_Click" />
            <asp:Button ID="BtnDescargarDNP" runat="server" Text="Descargar DNP"  OnClick="BtnDescargarDNP_Click" />
        </fieldset>
        <fieldset class="ActionSegment">
            <legend>Cargar Diligencia de Notificación Personal</legend>
            <asp:FileUpload ID="fuCargarReporte" runat="server" Placeholder="Scan de Diligencia de Notificación Personal" Width="91%" />
            <asp:TextBox ID="ObservacionNotificacion" runat="server" TextMode="MultiLine" Height="50px" Width="90%" MaxLength="1000" placeholder="Observaciones"></asp:TextBox>  
            <asp:Button ID="BtnSubirDNP" runat="server" Text="Guardar Cambios" OnClick="BtnSubirDNP_Click" />
        </fieldset>
        <asp:Button ID="btnAtras" runat="server" Text="Atrás" OnClick="btnAtras_Click" />
    </div>
</asp:Content>
