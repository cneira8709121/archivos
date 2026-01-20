<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EntregaNotificacionesDetalleENotificado.aspx.cs" MasterPageFile="~/Site.Master" Inherits="Notificaciones_EntregaNotificacionesDetalleENotificado" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <table width="100%">
        <tr>
            <td align="center">
                <asp:DetailsView ID="dtvDetalleNotificacion" SkinID="DisplayEasy" runat="server" DataSourceID="ObjectDataSource" AutoGenerateRows="False">
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
        <asp:Button ID="BtnDescargarConstanciaEntrega" runat="server" Text="Descargar Formato Constancia de Atención" OnClick="BtnDescargarConstanciaEntrega_Click" />
        <asp:Button ID="BtnDescargarResolucion" runat="server" Text="Descargar Resolucion" OnClick="BtnDescargarResolucion_Click" />
        <asp:Button ID="BtnDesfijarEdicto" runat ="server" 
            Text="Marcar Como Edicto Desfijado" onclick="BtnDesfijarEdicto_Click" />
        <div class="ActionSegment">
            <label>Finalizar Notificación</label>
            <asp:TextBox ID="txtObservacionNotificacion" runat="server" TextMode="MultiLine" Height="50px" MaxLength="1000" placeholder="Observaciones"></asp:TextBox>
            <asp:FileUpload ID="fuCargarFCA" runat="server" />
            <asp:Button ID="btnFinalizar" runat="server" Text="Finalizar" OnClick="btnFinalizar_Click"/>
        </div>

        <asp:Button ID="btnAtras" runat="server" Text="Volver" OnClick="btnAtras_Click" />
    </div>
</asp:Content>