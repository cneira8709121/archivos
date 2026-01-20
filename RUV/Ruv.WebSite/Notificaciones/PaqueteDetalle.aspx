<%@ Page Title="Detalle de Paquete de Notificaciones" Language="C#" AutoEventWireup="true" CodeFile="PaqueteDetalle.aspx.cs" MasterPageFile="~/Site.Master" Inherits="Notificaciones_PaqueteDetalle" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Head" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="pnlTitulo" runat="server" SkinID="pnlTitulo">
        <asp:Label ID="lblTitulo" runat="server" Text="Información de Paquete de Notificación" SkinID="lblSubTitulo" />
    </asp:Panel>
    <asp:DetailsView ID="dvPaqueteNotificacion" runat="server" Width="100%" AutoGenerateRows="false" DataSourceID="odsPaquete">
        <Fields>
            <asp:BoundField DataField="Id" HeaderText="Id" Visible="false" />
            <asp:BoundField DataField="Fecha" HeaderText="Fecha Generación" />
            <asp:BoundField DataField="OrdenServicio" HeaderText="Orden de Servicio" />
            <asp:BoundField DataField="Cantidad" HeaderText="Notificaciones" />
            <asp:BoundField DataField="NombreUsuario" HeaderText="Generado Por" SortExpression="NombreUsuario" />
        </Fields>
        <EmptyDataTemplate>No se pudo encontrar el registro</EmptyDataTemplate>
    </asp:DetailsView>
    <asp:ObjectDataSource ID="odsPaquete" runat="server" SelectMethod="ObtenerPaquete" TypeName="DataSourcePaqueteNotificacion" OnObjectCreated="odsPaquete_ObjectCreated"></asp:ObjectDataSource>
    <asp:Panel ID="pnlDetalle" runat="server" SkinID="pnlTitulo">
        <asp:Label ID="lblDetalle" runat="server" Text="Notificaciones Asociadas" SkinID="lblSubTitulo" />
    </asp:Panel>
    <asp:GridView ID="grdNotificacionesPaquete" runat="server" AutoGenerateColumns="False" DataSourceID="odsNotificacionesPaquete" AllowSorting="True" DataKeyNames="Id" SkinID="GridViewConPaginacion" Width="100%">
        <Columns> 
            <asp:BoundField DataField="ID" HeaderText="ID" />
            <asp:BoundField DataField="NumeroFormulario" HeaderText="Declaración" />
            <asp:TemplateField HeaderText="Ubicación de Citación">
                <ItemTemplate>
                    <%# Eval("NOMBREPAIS") + " / " + Eval("NOMBREDEPARTAMENTO") + " / " + Eval("NOMBREMUNICIPIO") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="DIRECCIONNOTIFICACION" HeaderText="Dirección de Citación"/> 
            <asp:BoundField DataField="UBICACIONNOTIFICACION" HeaderText="Punto de Notificación" /> 
            <asp:BoundField DataField="NOMBRECOMPLETO" HeaderText="Declarante"/>
            <asp:BoundField DataField="TIPODOCUMENTO" HeaderText="Tipo" />
            <asp:BoundField DataField="NUMERODOCUMENTO" HeaderText="Documento" />
            <asp:BoundField DataField="ESTADONOTIFICACION" HeaderText="Estado"/>
            <asp:BoundField DataField="cIdCodigoGuia" HeaderText="Codigo de guia" />
            <asp:TemplateField>
                <ItemTemplate>
                    <a href="" runat="server" class="historicoNotificacion" data-idnotificacion='<%# Eval("ID") %>'>
                        <img src='<%= Page.ResolveClientUrl("~/App_Themes/RUVTheme/Imagenes/history.png") %>' alt="Ver Histórico" title="Ver Histórico" width="20px" height="20px" />
                    </a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>No hay notificaciones asociadas al paquete seleccionado</EmptyDataTemplate>
    </asp:GridView>
    <asp:ObjectDataSource ID="odsNotificacionesPaquete" runat="server" StartRowIndexParameterName="startRow" MaximumRowsParameterName="pageSize" SelectCountMethod="CantidadPaqueteNotificaciones" SelectMethod="ObtenerPaqueteNotificaciones" SortParameterName="sortColumns" EnablePaging="True" TypeName="DataSourcePaqueteNotificacionDetalle" OnObjectCreated="odsNotificacionesPaquete_ObjectCreated"></asp:ObjectDataSource>
    <div class="ActionsBox">
        <asp:Button ID="btnDescargarExcel"       runat="server" Text="Descargar Archivo Excel"   onclick="btnDescargarExcel_Click" />
        <asp:Button ID="btnDescargarCitaciones"  runat="server" Text="Descargar Documentos"      onclick="btnDescargarCitaciones_Click" />
        <asp:Button ID="btnAsociarOrdenServicio" runat="server" Text="Asociar Orden de Servicio" onclick="btnAsociarOrdenServicio_Click" />        
        <asp:Button ID="btnConfirmar"            runat="server" Text="Confirmar Envio"           onclick="btnConfirmar_Click" />
        <asp:Button ID="btnVolver"               runat="server" Text="Volver"                    onclick="btnVolver_Click" />
    </div>
    <div class="ActionsBox">
     <asp:FileUpload ID="fuCargacodigos" runat="server" />
     <asp:Button ID="btnAsociarCodigoGuia"    runat="server" Text="Asociar Codigos de guia" onclick="btnAsociarCodigoGuia_Click"  />
    </div>
    <div  class="ActionsBox">
        <%--<asp:Button ID="btnGenerarPaquetes" runat="server" Text="Generar Paquetes" onclick="btnGenerarPaquetes_Click" />--%>
        <asp:FileUpload ID="fuCargarReporte" runat="server" />
        <asp:Button ID="btnCargarReporte" runat="server" Text="Cargar Archivo 4-72" onclick="btnCargarReporte_Click" />
    </div>
    <ajax:ModalPopupExtender ID="mdlPopupOrdenServicio" BehaviorID="mdlPopupOrdenServicio" runat="server" TargetControlID="btnAsociarOrdenServicio" PopupControlID="pnlPopupOrdenServicio" CancelControlID="btnCerrarPopup" BackgroundCssClass="modalBackground" />
    <asp:Panel ID="pnlPopupOrdenServicio" runat="server" Width="500px" SkinID="PanelmodalPopup" GroupingText="Asociar Orden de Servicio" HorizontalAlign="Left" Style="display: none">
        <asp:UpdatePanel ID="updPnlOrdenServicio" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:HiddenField ID="hfIdPaqueteNotificacion" runat="server" />
                <asp:Label ID="lblOrdenServicio" runat="server" Text="Código de Orden de Servicio" />
                <asp:TextBox ID="txtOrdenServicio" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rvOrdenServicio" runat="server" ErrorMessage="Debe especificar un código" ValidationGroup="vgOrdenServicio" ControlToValidate="txtOrdenServicio">*</asp:RequiredFieldValidator>
                <ajax:ValidatorCalloutExtender ID="vce_rv_txtOrdenServicio" BehaviorID="vce_rv_txtOrdenServicio" runat="server" Enabled="True" TargetControlID="rvOrdenServicio"></ajax:ValidatorCalloutExtender>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="ActionsBox">
            <asp:Button ID="btnGuardarOrdenServicio" runat="server" Text="Guardar" ValidationGroup="vgOrdenServicio" OnClick="btnGuardarOrdenServicio_Click" />
            <asp:Button ID="btnCerrarPopup" runat="server" Text="Cancelar" />
        </div>
    </asp:Panel>
    <script type="text/javascript" src='<%= Page.ResolveClientUrl("~/JScripts/Notificaciones/ruv.notificaciones-paquetedetalle.js") %>'></script>
</asp:Content>