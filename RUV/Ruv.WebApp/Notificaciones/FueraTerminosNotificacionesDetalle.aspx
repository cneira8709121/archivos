<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" Inherits="FueraTerminosNotificacionesDetalle" Codebehind="FueraTerminosNotificacionesDetalle.aspx.cs" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Panel ID="pblseparador" runat="server" SkinID="pnlTitulo">
        <asp:Label ID="lblTitulo" runat="server" Text="Notificación Pendiente de Publicación Edicto" SkinID="lblSubTitulo" />
    </asp:Panel>
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
    <asp:ObjectDataSource ID="ObjectDataSource" runat="server" TypeName="Ruv.WebApp.DataSources.Notificaciones.DataSourceNotificacionDetalle" EnablePaging="false" SelectMethod="DetalleData" OnObjectCreated="ObjectDataSource_ObjectCreated"></asp:ObjectDataSource>
    
    <div class="ActionsBox">
    <asp:Button ID="btnAtras" runat="server" Text="Volver" Width="127px" OnClick="btnAtras_Click" />    
    <asp:Button ID="BtnDescargarEdicto" runat="server" Text="Descargar Edicto" Width="152px"
        OnClick="BtnDescargarEdicto_Click" />
    <asp:Button ID="MarcarEdicto" runat="server" Text="Marcar Edicto Fijado"
        Width="260px" onclick="MarcarEdicto_Click"/>
    <asp:Button ID="btnMarcarNotificado" runat="server" Text="Marcar Como Notificado" 
            Width="148px" onclick="btnMarcarNotificado_Click" Visible="false" />
    </div>
</asp:Content>
