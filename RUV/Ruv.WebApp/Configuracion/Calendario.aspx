<%@ Page Title="Calendario de Festivos" Language="C#" AutoEventWireup="true" Inherits="Configuracion_Calendario" MasterPageFile="~/Site.Master" Codebehind="Calendario.aspx.cs" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Head" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript" src='<%= Page.ResolveClientUrl("~/JScripts/search.js") %>'></script>
</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Panel ID="pnlTitulo" runat="server" SkinID="pnlTitulo">
        <asp:Label ID="lblTitulo" runat="server" Text="Festivos Configurados" SkinID="lblSubTitulo" />
    </asp:Panel>
    <div class="actionFilterBox">
        <div class="content">
            <p style="width: 46%">
                <label>Año:</label>
                <asp:DropDownList ID="filterAno" runat="server" Width="90%" />
            </p>
            
            <div class="actions">
                <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar Registros" OnClick="btnFiltrar_Click" />
            </div>
        </div>
        
        <div class="panelControl">
            <a id="expandCollapseFilters" class="actionLink" data-state="open">Ocultar Filtros</a>
        </div>
    </div>
    <asp:GridView ID="grdFeriados" runat="server" AutoGenerateColumns="False" DataKeyNames="id" DataSourceID="odsFeriados" SkinID="GridViewConPaginacion" Width="100%">
        <Columns>
            <asp:TemplateField>
                <HeaderTemplate>
                    <asp:CheckBox ID="CheckAll" runat="server" AutoPostBack="true" OnCheckedChanged="CheckAll_CheckedChanged"/>
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckItem" runat="server" AutoPostBack="false"/>
                </ItemTemplate>                   
            </asp:TemplateField>
            <asp:BoundField DataField="Id" HeaderText="Id" Visible="False" />
            <asp:BoundField DataField="Fecha" HeaderText="Fecha" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" />                   
            <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
            <asp:BoundField DataField="Comentario" HeaderText="Comentario" HeaderStyle-Width="60%" />
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="odsFeriados" runat="server" SelectMethod="ObtenerNotificaciones" EnablePaging="false" TypeName="Ruv.WebApp.DataSources.Feriados.DataSourceFeriados" OnObjectCreated="odsFeriados_ObjectCreated"></asp:ObjectDataSource>
    
    <div class="ActionsBox">
        <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" onclick="btnEliminar_Click" />
    </div>

    <asp:Panel ID="pnlDetalle" runat="server" SkinID="pnlTitulo">
        <asp:Label ID="lblDetalle" runat="server" Text="Adicionar Festivo" SkinID="lblSubTitulo" />
    </asp:Panel>
    
    <div class="fieldform content">
        <p style="width: 46%">
            <label>Nombre:</label>
            <asp:TextBox ID="txtNombre" runat="server" Width="90%" />
        </p>

        <p style="width: 46%">
            <label>Recurrente:</label>
            <asp:CheckBox ID="chkRecurrente" runat="server" />
        </p>

        <div class="contentfield" style="width: 46%">
            <label>Fecha:</label>
            <asp:UpdatePanel ID="updCalendar" runat="server">
                <ContentTemplate>
                    <asp:Calendar ID="clnAdicionar" runat="server" Width="90%" CssClass="calendar"/>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
            
        <p style="width: 46%">
            <label>Descripción:</label>
            <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" Width="90%" />
        </p>
    </div>
        
    <div class="ActionsBox">
        <asp:Button ID="btnAdicionarFestivo" runat="server" Text="Adicionar" OnClick="btnAdicionarFestivo_Click" />
    </div>
</asp:Content>
