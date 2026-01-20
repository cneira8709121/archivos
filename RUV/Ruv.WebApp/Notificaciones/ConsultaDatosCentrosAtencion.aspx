<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" Inherits="Notificaciones_ConsultaDatosCentrosAtencion" Codebehind="ConsultaDatosCentrosAtencion.aspx.cs" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Src="~/Utilidades/Controles/GridCustomPager.ascx" TagPrefix="kaz" TagName="CustomPager" %>
<asp:Content ID="Head" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src='<%= Page.ResolveClientUrl("~/JScripts/search.js") %>'></script>
    <script language="javascript" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="pblseparador" runat="server" SkinID="pnlTitulo">
        <asp:Label ID="lblTitulo" runat="server" Text="Notificaciones" SkinID="lblSubTitulo" />
    </asp:Panel>
    <div class="actionFilterBox">
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
            
            <div class="actions">
                <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar Registros" 
                    data-filter="true" onclick="btnFiltrar_Click" />
                <asp:Button ID="btnRestaurarFiltros" runat="server" Text="Restaurar" 
                    onclick="btnRestaurarFiltros_Click" />   
            </div>
        </div>
        
        <div class="panelControl">
            <a id="expandCollapseFilters" class="actionLink" data-state="open">Ocultar Filtros</a>
        </div>
    </div>
    <asp:GridView ID="grdConsultaCentrosAtencion" runat="server" AutoGenerateColumns="False" AllowSorting="True" 
                  DataKeyNames="nIdCentro,nTipo" SkinID="GridViewConPaginacion"
                  Width="100%" OnRowDataBound="grdConsultaCentrosAtencion_RowDataBound" PagerSettings-Visible="false">
        <Columns>
            <asp:BoundField DataField="nIdCentro" HeaderText="NIDCENTRO" SortExpression="nIdCentro" Visible="False" />
            <asp:BoundField DataField="nTipo" HeaderText="nTipoCentro" SortExpression="nTipo" Visible="False" />
            <asp:BoundField DataField="nCantidadNotificaciones" HeaderText="Cantidad De notificaciones" SortExpression="nCantidadNotificaciones" />
            <asp:BoundField DataField="cNombreCentroAtencion" HeaderText="Centro de Atencion" SortExpression="cNombreCentroAtencion" />
            <asp:BoundField DataField="cNombreMunicipio" HeaderText="Municipio" SortExpression="cNombreMunicipio" />
            <asp:BoundField DataField="cNombreDepartamento" HeaderText="Departamento" SortExpression="cNombreDepartamento"/>
            <asp:BoundField DataField="cNombrePais" HeaderText="País" SortExpression="cNombrePais"/>
            <asp:TemplateField HeaderText="Acciones">
                <ItemTemplate>
                  <asp:ImageButton ID="ImgDetalle" runat="server" SkinID="imgBuscar" CommandName="Detalle" 
                       PostBackUrl='<%# "DetallesDatosCentroAtencion.aspx?id=" + Eval("nIdCentro") + "&tipo=" + Eval("nTipo") %>'
                        CausesValidation="false" ToolTip="Ver detalle notificación" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <kaz:CustomPager ID="GridPager" runat="server" CurrentPageNumber="1" CurrentPageSize="20" OnPageChanged="GridPager_PageChanged" />
    <script type="text/javascript" src='<%= Page.ResolveClientUrl("~/JScripts/Notificaciones/ruv.notificaciones-centrosatencion.js") %>'></script>
</asp:Content>

