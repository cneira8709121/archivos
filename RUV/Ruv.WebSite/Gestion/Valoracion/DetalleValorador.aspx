<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DetalleValorador.aspx.cs" MasterPageFile="~/Site.Master" Inherits="Gestion_Valoracion_DetalleValorador" %>

<%@ MasterType VirtualPath="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">  
    <table align="center" width="500px">
    <tr class="fila1">
        <td align="right">
        <label id="FechaElige" skinid="lblTituloApl">Elige Una Fecha a Consultar(MM/YYYY)</label>
        </td>
        <td align="left">
            <asp:TextBox ID="TxtFechaInicial" runat="server" MaxLength="12" 
                ValidationGroup="grpRegister" Width="70px" EnableTheming="True"></asp:TextBox>
                <asp:Button ID="Consultar" runat="server" Text="Consultar" 
                onclick="Consultar_Click" />
        </td>
    </tr>
    </table>
 <div>    
            <asp:GridView ID="gridDetalleValorador" runat="server" align="center"
                DataSourceID="ObjectDataSource2" SkinID="GridViewConPaginacion" 
                AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="DFechaDeclaracion" HeaderText="FECHA VALORACION" />
                    <asp:BoundField DataField="NDeclaracionesValoradas" HeaderText="DECLARACIONES VALORADAS"></asp:BoundField>                    
                </Columns>
                <EmptyDataTemplate>
                                    No hay registros que coincidan con los criterios de busqueda
                </EmptyDataTemplate>
            </asp:GridView>
            <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" TypeName="DataSourceDetalleValorador"
                EnablePaging="true" SelectMethod="GetData" SelectCountMethod="VirtualItemCount"
                StartRowIndexParameterName="startRow" MaximumRowsParameterName="maxRows" OnObjectCreated="ObjectDataSource2_ObjectCreated">
            </asp:ObjectDataSource>     
    </div>
    </asp:Content>
