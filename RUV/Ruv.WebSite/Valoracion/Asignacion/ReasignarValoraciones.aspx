<%@ Page Title="Reasignar Valoracion" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeFile="ReasignarValoraciones.aspx.cs" Inherits="Valoracion_ReasignarValoraciones" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">--%>
        <ContentTemplate>
            <center>
                <asp:Label ID="lblError" runat="server" SkinID="lblError"></asp:Label>
            </center>
            <div>
                <ruv:Filtros ID="filtro" runat="server" Procesos="Reasignacion" OnFiltro="filtro_Filtro" />
            </div>
            <asp:Panel ID="pblseparador" runat="server" SkinID="pnlTitulo">
                <asp:Label ID="lblTitulo" runat="server" Text="REASIGNAR VALORACION" SkinID="lblSubTitulo" />
            </asp:Panel>
            <div>
                <table width="100%">
                    <tr>
                        <td style="width: 200px; text-align: left">
                            <asp:Label ID="lblValorador" runat="server" Text="Asignar a Valorador:"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <ruv:DropDownList ID="ddlValorador" runat="server" EsRequerido="true" MensajeError="Seleccione Valorador"
                                DataTextField="Nombre" DataValueField="Id" />
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <asp:GridView ID="gvDeclSinValorar" runat="server" SkinID="GridViewConPaginacion"
                DataSourceID="odtSinValorar" OnSelectedIndexChanged="gvDeclSinValorar_SelectedIndexChanged" AllowSorting="true" DataKeyNames="ID" AutoGenerateColumns="false"
                Width="100%">
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="Id" SortExpression="Id" Visible="false" />
                    <asp:BoundField DataField="NombreDeclarante" HeaderText="Declarante" SortExpression="Nombre_Persona" />
                    <asp:BoundField DataField="DocumentoDeclarante" HeaderText="No. Documento" SortExpression="Documento" />
                    <asp:BoundField DataField="FechaRadicado" HeaderText="Fecha de Radicado" SortExpression="Fecha_Radicacion"
                        DataFormatString="<%$ AppSettings:FechaGrilla %>" />
                    <asp:BoundField DataField="NumeroFormulario" HeaderText="Numero Formulario" SortExpression="Formulario" />
                    <asp:BoundField DataField="TotalHv" HeaderText="Total Hv" SortExpression="Total_Hv" />
                    <asp:BoundField DataField="Departamento" HeaderText="Departamento" SortExpression="Departamento" />
                    <asp:BoundField DataField="Municipio" HeaderText="Municipio" SortExpression="Municipio" />
                    <asp:BoundField DataField="Entidad" HeaderText="Entidad del MP" SortExpression="Entidad" />
                    <asp:BoundField DataField="Valorador" HeaderText="Valorador" SortExpression="Valorador" />
                    <asp:TemplateField HeaderText="Reasignar">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelec" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Deshacer ">
                        <ItemTemplate>
                            <asp:ImageButton ID="imgDeshacer" runat="server" SkinID="imgDeshacer" CommandName="Select" ToolTip="Deshacer Asignación" CausesValidation="false" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:ObjectDataSource ID="odtSinValorar" runat="server" TypeName="DataSourceDeclSinValorar" EnablePaging="true"
                SelectMethod="GetData" SelectCountMethod="VirtualItemCount" StartRowIndexParameterName="startRow" SortParameterName="sortColumns"
                MaximumRowsParameterName="maxRows" OnObjectCreated="odtSinValorar_ObjectCreated">
            </asp:ObjectDataSource>
            <ruv:ModalPopUp ID="mpGuardar" runat="server" MostrarImagen="false" filatextBox="false" BehaviorID="mpGuardarBehavior"
                DropShadow="true" MostrarBotones="true" VisibleBotonCancelar="false" Mensaje="Se realizo Correctamente la Reasignacion"
                OnOk="mpGuardar_Ok" />
            <ruv:ModalPopUp ID="mpAdvertenciaDeshacer" runat="server" MostrarImagen="false" BehaviorID="mpAdvertenciaDeshacerBehavior"
                MostrarBotones="true" Mensaje="Indique una observación para deshacer la asignación"
                filatextBox="true" DropShadow="true" OnOk="mpAdvertenciaDeshacer_Ok" />
        </ContentTemplate>
    <%--</asp:UpdatePanel>--%>
</asp:Content>
