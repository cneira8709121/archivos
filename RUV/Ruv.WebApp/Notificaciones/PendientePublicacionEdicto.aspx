<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" Inherits="PendientePublicacionEdicto" Codebehind="PendientePublicacionEdicto.aspx.cs" %>
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
                <asp:DetailsView ID="dtvDetalleNotificacion" runat="server" DataSourceID="ObjectDataSource" AutoGenerateRows="False">
                    <Fields>
                        <asp:BoundField DataField="cDireccionNotificacion" HeaderText="DIRECCIÓN NOTIFICACIÓN" />
                        <asp:BoundField DataField="cPais" HeaderText="PAÍS" />
                        <asp:BoundField DataField="cDepartamento" HeaderText="DEPARTAMENTO" />
                        <asp:BoundField DataField="cMunicipio" HeaderText="MUNICIPIO" />
                        <asp:BoundField DataField="cTelefonoNotificacion" HeaderText="TELÉFONO NOTIFICACIÓN" />
                        <asp:BoundField DataField="cTipoDocumento" HeaderText="TIPO DOCUMENTO" />
                        <asp:BoundField DataField="cDocumentoIdentidad" HeaderText="DOCUMENTO IDENTIDAD" />
                        <asp:BoundField DataField="cEstadoDeclaracion" HeaderText="ESTADO DECLARACIÓN" />
                        <asp:BoundField DataField="cNombreDeclarante" HeaderText="NOMBRE DECLARANTE" />
                    </Fields>
                </asp:DetailsView>
            </td>
        </tr>
    </table>
    <asp:ObjectDataSource ID="ObjectDataSource" runat="server" TypeName="Ruv.WebApp.DataSources.Notificaciones.DataSourceNotificacionDetalle" EnablePaging="false" SelectMethod="DetalleData" OnObjectCreated="ObjectDataSource_ObjectCreated"></asp:ObjectDataSource>
    
    
    <div class="ActionsBox">
    <fieldset class="ActionSegment">
        <legend>Notificación Por Edicto</legend>
        <asp:Button ID="BtnDescargarEdicto" runat="server" Text="Descargar Edicto" Width="152px"
        OnClick="BtnDescargarEdicto_Click" />
        <asp:Label ID="lblEdicto" runat="server" Text="Edicto: "></asp:Label>
        <asp:FileUpload ID="fuCargarReporte" runat="server"  />
        <asp:Button ID="MarcarEdicto" runat="server" Text="Marcar Edicto Fijado"
        Width="260px" onclick="MarcarEdicto_Click"/>
    </fieldset>
    <fieldset class="ActionSegment">
        <legend>Descargar Resolucion y DNP</legend>
         <asp:Button ID="BtnDescargarResolucion" runat="server" Text="Descargar Resolucion"
        Width="152px" OnClick="BtnDescargarResolucion_Click" />    
    <asp:Button ID="BtnDescargarFCA" runat="server" Text="Descargar Formato Constancia Atención"
        Width="260px" OnClick="BtnDescargarFCA_Click" />
        <asp:Label ID="lblCA" runat="server" Text="Constancia de Atención: "></asp:Label>
        <asp:FileUpload ID="fuCargarFCA" runat="server" />
        <div>
        <asp:TextBox ID="txtObservacionNotificacion" runat="server" TextMode="MultiLine" Height="50px"
            MaxLength="1000" placeholder="Observaciones"></asp:TextBox>
            </div>
        <div>
        <asp:Button ID="btnFinalizar" runat="server" Text="Finalizar" Width="126px" 
        onclick="btnFinalizar_Click"/>
        </div>
    </fieldset>    
        </div>
        <asp:Button ID="btnAtras" runat="server" Text="Atrás" Width="127px" OnClick="btnAtras_Click" />  
</asp:Content>
