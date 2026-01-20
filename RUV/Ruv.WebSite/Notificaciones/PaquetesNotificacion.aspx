<%@ Page Title="Paquetes de Notificaciones" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="PaquetesNotificacion.aspx.cs" Inherits="Notificaciones_PaquetesNotificacion" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Head" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src='<%= Page.ResolveClientUrl("~/JScripts/search.js") %>'></script>
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="pnlTitulo" runat="server" SkinID="pnlTitulo">
        <asp:Label ID="lblTitulo" runat="server" Text="Paquetes de Notificaciones" SkinID="lblSubTitulo" />
    </asp:Panel>
    <div class="actionFilterBox">
        <div class="content">
            <p style="width: 46%">
                <label>Orden de Servicio:</label>
                <asp:TextBox ID="filterOrdenDeServicio" runat="server" Width="90%" />
            </p>
            <p style="width: 46%">
                <label>Fecha de Generación:</label>
                <asp:TextBox ID="filterFechaInicio" runat="server" Width="100px" placeholder="dd/mm/yyyy" />
                <asp:TextBox ID="filterFechaFin" runat="server" Width="100px" placeholder="dd/mm/yyyy" />
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
    <asp:GridView ID="grdPaquetes" runat="server" AutoGenerateColumns="False" DataSourceID="odsPaquetes" AllowSorting="false" DataKeyNames="Id" SkinID="GridViewConPaginacion" Width="100%" OnRowCommand="grdPaquetes_RowCommand">
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="Id Paquete" SortExpression="Id" Visible="true" />
            <asp:TemplateField HeaderText="Fecha Generación" SortExpression="Fecha">
                <ItemTemplate>
                    <%# (Eval("Fecha") as DateTime? == null ? string.Empty : (Eval("Fecha") as DateTime?).Value.ToString("dd/MMM/yyyy"))%>
                    <span class="altInfo noWrap"><%# (Eval("Fecha") as DateTime? == null ? string.Empty : (Eval("Fecha") as DateTime?).Value.ToString("hh:mm:ss tt"))%></span>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="OrdenServicio" HeaderText="Orden de Servicio" SortExpression="OrdenServicio" />
            <asp:BoundField DataField="Cantidad" HeaderText="Notificaciones" SortExpression="Cantidad" />
            <asp:BoundField DataField="NombreUsuario" HeaderText="Generado Por" SortExpression="NombreUsuario" />
            <asp:BoundField DataField="Resumen" HeaderText="Resumen" SortExpression="Resumen" />
            <asp:TemplateField HeaderText="Acciones">
                <ItemTemplate>
                    <asp:ImageButton ID="ImgDetalle" runat="server" SkinID="imgBuscar" CommandName="Detalle" CommandArgument='<%# Eval("Id")%>' CausesValidation="false" ToolTip="Ver detalle" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>No hay registros para mostrar</EmptyDataTemplate>
    </asp:GridView>
    <asp:ObjectDataSource ID="odsPaquetes" runat="server" StartRowIndexParameterName="startRow" MaximumRowsParameterName="pageSize" SelectCountMethod="CantidadPaquetes" SelectMethod="ObtenerPaquetes" SortParameterName="SortColumns" EnablePaging="True" TypeName="Ruv.WebSite.DataSources.Notificaciones.DataSourcePaquetesNotificacion" OnObjectCreated="odsPaquetes_ObjectCreated"></asp:ObjectDataSource>
    
    <ruv:ModalPopUp ID="mdPopUpNotificacion" runat="server" Visible="false" />
</asp:Content>
