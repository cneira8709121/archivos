<%@ Page Title="Detalles de Centro de Atención" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeFile="DetallesDatosCentroAtencion.aspx.cs" Inherits="Notificaciones_DetallesDatosCentroAtencion" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Src="~/Utilidades/Controles/GridCustomPager.ascx" TagPrefix="kaz" TagName="CustomPager" %>
<asp:Content ID="Head" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src='<%= Page.ResolveClientUrl("~/JScripts/search.js") %>'></script>
    <script language="javascript" type="text/javascript"> </script>    
</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="pblseparador" runat="server" SkinID="pnlTitulo">
        <asp:Label ID="lblTitulo" runat="server" Text="Notificaciones" SkinID="lblSubTitulo" />
    </asp:Panel>
    <%--<div class="actionFilterBox">
        <div class="content">
            <p style="width: 12%">
                <label>País:</label>
                <asp:DropDownList ID="filterPais" ClientIDMode="Static" runat="server" Width="95%" DataValueField="Id" DataTextField="Nombre" />
            </p>
            <p style="width: 12%">
                <label>Departamento:</label>
                <asp:DropDownList ID="filterDepartamento" ClientIDMode="Static" runat="server" Width="95%" DataValueField="Id" DataTextField="Nombre" />
            </p>
            <p style="width: 12%">
                <label>Municipio:</label>
                <asp:DropDownList ID="filterMunicipio" ClientIDMode="Static" runat="server" Width="95%" DataValueField="Id" DataTextField="Nombre" />
            </p>
            
            <%--<div class="actions">
                <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar Registros" OnClick="btnFiltrar_Click" data-filter="true" />
                <asp:Button ID="btnRestaurarFiltros" runat="server" Text="Restaurar" OnClick="btnRestaurarFiltros_Click" />
               
            </div>
        </div>
        
        <div class="panelControl">
            <a id="expandCollapseFilters" class="actionLink" data-state="open">Ocultar Filtros</a>
        </div>
    </div>--%>
    <asp:GridView ID="grdDetalleCentrosAtencion" runat="server" AutoGenerateColumns="False" AllowSorting="True" 
                  DataKeyNames="cIdCodigoGuia" SkinID="GridViewConPaginacion"
                  Width="100%" OnRowDataBound="grdDetalleCentrosAtencion_RowDataBound" PagerSettings-Visible="false">
        <Columns>
            <asp:BoundField DataField="cDireccionNotifica" HeaderText="Direccion de Notificacion" SortExpression="cDireccionNotifica"/>
            <asp:BoundField DataField="cTelefononotifica" HeaderText="Telefono de notificacion" SortExpression="cTelefononotifica" />
            <asp:BoundField DataField="cEstadoCourier" HeaderText="Estado del Courier" SortExpression="cEstadoCourier" />
            <asp:BoundField DataField="dFechafinalNotifica" HeaderText="Fecha final para Notificar" SortExpression="dFechafinalNotifica" />
            <asp:BoundField DataField="cNombreEstado" HeaderText="Estodo de Notificacion" SortExpression="cNombreEstado"/>
            <asp:BoundField DataField="cIdCodigoGuia" HeaderText="Codigo Guia de Notificacion" SortExpression="cIdCodigoGuia"/>            
        </Columns>
    </asp:GridView>
    <div class="ActionsBox">
     <asp:Button ID="btnVolver" runat="server" Text="Volver" onclick="btnVolver_Click" />
    </div>
    <kaz:CustomPager ID="GridPager" runat="server" CurrentPageNumber="1" CurrentPageSize="20" OnPageChanged="GridPager_PageChanged" />

    <asp:Panel ID="pnlSeparadorEncargado" runat="server" SkinID="pnlTitulo">
        <asp:Label ID="lblEncargado" runat="server" Text="Encargados" SkinID="lblSubTitulo" />
    </asp:Panel>
    <asp:GridView ID="grdEncargados" runat="server" AutoGenerateColumns="False" AllowSorting="True" SkinID="GridViewConPaginacion"
                  DataKeyNames="nIdEncargado" Width="100%" PagerSettings-Visible="false">
        <Columns>
            <asp:BoundField DataField="cNombre" HeaderText="Nombre" SortExpression="cNombre"/>
            <%--<asp:BoundField DataField="cCargo" HeaderText="Cargo" SortExpression="cCargo" />--%>
            <asp:BoundField DataField="cDireccion" HeaderText="Dirección" SortExpression="cDireccion" />
            <asp:BoundField DataField="cTelefono" HeaderText="Teléfono" SortExpression="cTelefono" />
        </Columns>
    </asp:GridView>
    <kaz:CustomPager ID="GridPagerEncargados" runat="server" CurrentPageNumber="1" CurrentPageSize="20" OnPageChanged="GridPagerEncargados_PageChanged" />

  </asp:Content>