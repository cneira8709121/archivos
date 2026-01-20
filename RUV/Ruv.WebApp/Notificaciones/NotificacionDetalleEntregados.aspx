<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" Inherits="Notificaciones_NotificacionDetalleEntregados" Codebehind="NotificacionDetalleEntregados.aspx.cs" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <table width="100%">
        <tr>
            <td align="center">
                <asp:DetailsView ID="dtvDetalleNotificacion" runat="server" DataSourceID="ObjectDataSource" AutoGenerateRows="False">
                    <Fields> 
                      <asp:BoundField DataField="cDireccionNotificacion" HeaderText="DIRECCION NOTIFICACION"/>  
                      <asp:BoundField DataField="cPais" HeaderText="PAIS"/>
                      <asp:BoundField DataField="cDepartamento" HeaderText="DEPARTAMENTO"/>
                      <asp:BoundField DataField="cMunicipio" HeaderText="MUNICIPIO"/>
                      <asp:BoundField DataField="cTelefonoNotificacion" HeaderText="TELEFONO NOTIFICACION"/>
                      <asp:BoundField DataField="cTipoDocumento" HeaderText="TIPO DOCUMENTO"/>
                      <asp:BoundField DataField="cDocumentoIdentidad" HeaderText="DOCUMENTO IDENTIDAD"/>
                      <asp:BoundField DataField="cEstadoDeclaracion" HeaderText="ESTADO DECLARACION"/>
                      <asp:BoundField DataField="cNombreDeclarante" HeaderText="NOMBRE DECLARANTE"/>
                    </Fields>
                </asp:DetailsView>
            </td>
        </tr>
    </table>
    <asp:ObjectDataSource ID="ObjectDataSource" runat="server" TypeName="Ruv.WebApp.DataSources.Notificaciones.DataSourceNotificacionDetalle" EnablePaging="false" SelectMethod="DetalleData" OnObjectCreated="ObjectDataSource_ObjectCreated"></asp:ObjectDataSource>
    
    <div class="ActionsBox">
        <asp:Button ID="BtnDescargarDNP" runat="server" Text="Descargar DNP" OnClick="BtnDescargarDNP_Click" />
        <asp:Button ID="BtnDescargarResolucion" runat="server" Text="Descargar Resolución" OnClick="BtnDescargarResolucion_Click" />
        
        <div class="ActionSegment">
            <label>Marcar Edicto como Publicado</label>
            <asp:FileUpload ID="fuCargarReporte" runat="server" />
            <asp:Button ID="BtnSubirDNP" runat="server" Text="Subir Archivo DNP" onclick="BtnSubirDNP_Click" />
        </div>
    </div>
</asp:Content>