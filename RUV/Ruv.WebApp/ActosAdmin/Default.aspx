<%@ Page Title="Actos Administrativos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" Inherits="Ruv.WebApp.ActosAdmin.Default" Codebehind="Default.aspx.cs" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <center>
                <ruv:Filtros ID="Adfiltro" runat="server" Procesos="ActoAdmin" OnFiltro="Adfiltro_Filtro" />
                <asp:Panel ID="pblseparador" runat="server" SkinID="pnlTitulo">
                    <asp:Label ID="lblTitulo" runat="server" Text="ACTO ADMINISTRATIVO"
                        SkinID="lblSubTitulo" />
                </asp:Panel>
                <asp:Label ID="lblError" runat="server" SkinID="lblError"></asp:Label>
                <br />
                <asp:GridView ID="grdActosAdministrativos" runat="server" DataKeyNames="Id" SkinID="GridViewConPaginacion"
                    DataSourceID="ActosAdminSource" Width="100%" AutoGenerateColumns="false"
                    AllowSorting="true">
                    <Columns>
                        <asp:BoundField DataField="ID" HeaderText="Id" />
                        <asp:BoundField DataField="Consecutivo" HeaderText="Consecutivo" />
                        <asp:BoundField DataField="Fecha" HeaderText="Fecha" />
                        <asp:BoundField DataField="Documento" HeaderText="Documento" />
                        <asp:BoundField DataField="Persona" HeaderText="Solicitante" />
                        <asp:BoundField DataField="NroFormulario" HeaderText="Nro. Formulario" />
                        <asp:BoundField DataField="Dirigido" HeaderText="Dirigido" />
                        <asp:BoundField DataField="Estado" HeaderText="Estado" />
                        <asp:TemplateField HeaderStyle-Width="30px" HeaderText="Seleccione">
                            <ItemTemplate>
                                <asp:ImageButton ID="img" runat="server" SkinID="imgBuscar" CommandName="Select" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:ObjectDataSource ID="ActosAdminSource" runat="server" StartRowIndexParameterName="startRow"
                    MaximumRowsParameterName="pageSize" SelectCountMethod="Cantidad" SelectMethod="ObtenerActosAdministrativos"
                    TypeName="DataSourceActosAdmin" SortParameterName="sortColumns" OnObjectCreated="dataEmpInfo_ObjectCreated"
                    EnablePaging="true"></asp:ObjectDataSource>
            </center>
            <ruv:ModalPopUp ID="mpopGuardar" runat="server" MostrarBotones="true" DropShadow="true"
                VisibleBotonCancelar="true" MostrarImagen="false" filatextBox="false" OnOk="mpopGuardar_Ok"
                BehaviorID="mpopGuardarBehavior" />
            <ruv:ModalPopUp ID="mpupError" runat="server" MostrarBotones="true" VisibleBotonCancelar="false"
                DropShadow="true" Mensaje="Ourrio un error al guardar, intente de nuevo de persistir el error comuniquese con el administrador"
                MostrarImagen="false" filatextBox="true" BehaviorID="mpupErrorBehavior" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
