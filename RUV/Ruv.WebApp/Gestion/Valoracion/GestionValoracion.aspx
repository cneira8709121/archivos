<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" Inherits="Gestion_Valoracion_GestionValoracion" Codebehind="GestionValoracion.aspx.cs" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">  
            <asp:GridView ID="gridGestionValorador" runat="server" 
                DataSourceID="ObjectDataSource1" SkinID="GridViewConPaginacion" 
                AutoGenerateColumns="false" DataKeyNames="NIdValorador"
                onselectedindexchanged="gridGestionValorador_SelectedIndexChanged">
                <Columns>
                    <asp:BoundField DataField="NIdValorador" HeaderText="ID Usuario" />
                    <asp:BoundField DataField="CNombreUsuario" HeaderText="Nombre Valorador"></asp:BoundField>
                    <asp:BoundField DataField="NPromedioValoracion" HeaderText="PROMEDIO DE TIEMPO VALORACION">
                    </asp:BoundField>
                    <asp:BoundField DataField="NVALORACIONDEVUELTA" HeaderText="VALORACION DEVUELTA"></asp:BoundField>
                    <asp:BoundField DataField="NVALORACIONFINALIZADA" HeaderText="VALORACION FINALIZADA"></asp:BoundField>
                    <asp:BoundField DataField="NVALORACIONENPROCESO" HeaderText="VALORACION EN PROCESO"></asp:BoundField>
                    <asp:BoundField DataField="NVALORACIONASIGNADA" HeaderText="VALORACION ASIGNADA"></asp:BoundField>
                    <asp:BoundField DataField="NValoracionDevuelAsig" HeaderText="Valoracion Devuelta Asignar">
                    </asp:BoundField>
                    <asp:CommandField SelectText="Detalle" ShowSelectButton="True" />
                </Columns>
                <EmptyDataTemplate>
                                    No hay registros que coincidan con los criterios de busqueda
                </EmptyDataTemplate>
            </asp:GridView>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="DataSourceConsultaValorador"
                EnablePaging="true" SelectMethod="GetData" SelectCountMethod="VirtualItemCount"
                StartRowIndexParameterName="startRow" MaximumRowsParameterName="maxRows" OnObjectCreated="ObjectDataSource1_ObjectCreated">
            </asp:ObjectDataSource>     
</asp:Content>