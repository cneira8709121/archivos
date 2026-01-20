<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" Inherits="Notificaciones_NotificacionesEntregadas" Codebehind="NotificacionesEntregadas.aspx.cs" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Head" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src='<%= Page.ResolveClientUrl("~/JScripts/search.js") %>'></script>
</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="pblseparador" runat="server" SkinID="pnlTitulo">
        <asp:Label ID="lblTitulo" runat="server" Text="Notificaciones en Proceso" SkinID="lblSubTitulo" />
    </asp:Panel>
    <div class="actionFilterBox">
        <div class="content">
            <p style="width: 15%">
                <label>Estado:</label>
                <asp:DropDownList ID="filterEstado" runat="server" Width="90%" DataValueField="nIdEstado" DataTextField="cNombre" />
            </p>
            <p style="width: 20%">
                <label>Declaración:</label>
                <asp:TextBox ID="filterDeclaracion" runat="server" Width="90%" />
            </p>
            <p style="width: 15%">
                <label>Tipo Documento:</label>
                <asp:DropDownList ID="filterTipoDocumento" runat="server" Width="90%" DataValueField="Id" DataTextField="Nombre" />
            </p>
            <p style="width: 20%">
                <label>Documento:</label>
                <asp:TextBox ID="filterDocumento" runat="server" Width="90%" />
            </p>
            <p style="width: 30%">
                <label>Nombre Declarante:</label>
                <asp:TextBox ID="filterNombreDeclarante" runat="server" Width="90%" />
            </p>
            
            <div class="actions">
                <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar Registros" OnClick="btnFiltrar_Click" />
                <asp:Button ID="btnRestaurarFiltros" runat="server" Text="Restaurar" OnClick="btnRestaurarFiltros_Click" />
            </div>
        </div>
        
        <div class="panelControl">
            <a id="expandCollapseFilters" class="actionLink" data-state="open">Ocultar Filtros</a>
        </div>
    </div>

    <asp:GridView ID="grdNotificacionesEntregadas" runat="server" AutoGenerateColumns="False" DataSourceID="NotificacionesEntregadas" AllowSorting="True" DataKeyNames="NID" SkinID="GridViewConPaginacion" Width="100%" OnRowCommand="grdNotificaciones_RowCommand">
        <Columns>
            <asp:BoundField DataField="NID" HeaderText="NID" SortExpression="NID" Visible="false" />
            
            <asp:BoundField DataField="CNumeroFormulario" HeaderText="Formulario" SortExpression="CID_DECLARACION" />
            <asp:BoundField DataField="CNOMBRECOMPLETO" HeaderText="Declarante" SortExpression="CNOMBRECOMPLETO" />
            <asp:BoundField DataField="CTIPODOCUMENTO" HeaderText="Tipo Documento" SortExpression="CNUMERODOCUMENTO" />
            <asp:BoundField DataField="CNUMERODOCUMENTO" HeaderText="Documento" SortExpression="CNUMERODOCUMENTO" />

            <asp:BoundField DataField="CESTADONOTIFICACION" HeaderText="Estado Notificación" SortExpression="CESTADONOTIFICACION" />
            <asp:TemplateField HeaderText="Ubicación">
                <ItemTemplate>
                    <%# Eval("CNOMBREPAIS") + " - " + Eval("CNOMBREDEPARTAMENTO") + " - " + Eval("CNOMBREMUNICIPIO") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="CDIRECCIONNOTIFICACION" HeaderText="Dirección" SortExpression="CDIRECCIONNOTIFICACION" />
            <asp:BoundField DataField="FechaFinal" HeaderText="Vencimiento" SortExpression="FechaFinal" HtmlEncode=false DataFormatString="{0:dd/MMM/yyyy}" />
            <asp:BoundField DataField="CUBICACIONNOTIFICACION" HeaderText="Punto Notificación" SortExpression="CUBICACIONNOTIFICACION" />
            
            <asp:TemplateField HeaderText="Acciones">
                <ItemTemplate>
                    <asp:ImageButton ID="ImgDetalle" runat="server" SkinID="imgBuscar" CommandName='<%# Eval("NID_ESTADONOTIFICACION")%>' CommandArgument='<%# Eval("NID")%>' CausesValidation="false" Visible='<%# (int)Eval("NID_ESTADONOTIFICACION") == 5 || (int)Eval("NID_ESTADONOTIFICACION") == 6 || (int)Eval("NID_ESTADONOTIFICACION") == 8 || (int)Eval("NID_ESTADONOTIFICACION") == 10 || (int)Eval("NID_ESTADONOTIFICACION") == 11 || (int)Eval("NID_ESTADONOTIFICACION") == 12|| (int)Eval("NID_ESTADONOTIFICACION") == 13 || (int)Eval("NID_ESTADONOTIFICACION") == 14 %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="NotificacionesEntregadas" runat="server" StartRowIndexParameterName="startRow" MaximumRowsParameterName="pageSize" SelectCountMethod="CantidadNotificacionesEntregadas" SelectMethod="ObtenerNotificacionesEntregadas" SortParameterName="sortColumns" EnablePaging="True" TypeName="Ruv.WebApp.DataSources.Notificaciones.DataSourceNotificacionesEntregadas" OnObjectCreated="NotificacionesEntregadas_ObjectCreated"></asp:ObjectDataSource>
</asp:Content>