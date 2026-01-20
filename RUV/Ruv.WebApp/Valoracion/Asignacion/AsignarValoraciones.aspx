<%@ Page Language="C#" Title="Asignar a Valoración" AutoEventWireup="true" MasterPageFile="~/Site.Master" Inherits="Valoracion_AsignarValoraciones" Codebehind="AsignarValoraciones.aspx.cs" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
        
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <center>
                <asp:Label ID="lblError" runat="server" SkinID="lblError"></asp:Label>
            </center>
            <asp:MultiView ID="mvAsignar" runat="server" ActiveViewIndex="0">
                <asp:View ID="vAsignar" runat="server">
                    <div>
                        <ruv:Filtros ID="filtro" runat="server" Procesos="Asignacion" OnFiltro="filtro_Filtro" />
                    </div>
                    <asp:Panel ID="pblseparador" runat="server" SkinID="pnlTitulo">
                        <asp:Label ID="lblTitulo" runat="server" Text="ASIGNAR DECLARACION" SkinID="lblSubTitulo" />
                    </asp:Panel>
                    <div>
                        <table width="100%" style="border-collapse: collapse">
                            <tr>
                                <td style="width: 200px; text-align: left">
                                    <asp:Label ID="lblValorador" runat="server" Text="Asignar a Valorador:"></asp:Label>
                                </td>
                                <td style="text-align: left">
                                    <ruv:DropDownList ID="ddlValorador" runat="server" EsRequerido="true" MensajeError="Seleccione un Valorador"
                                        DataTextField="NOMBRE_COMPLETO" DataValueField="ID" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <br />
                    <asp:GridView ID="gvDeclSinValorar" runat="server" SkinID="GridViewConPaginacion"
                        DataSourceID="ObjectDataSource1" OnSelectedIndexChanged="gvDeclSinValorar_SelectedIndexChanged"
                        DataKeyNames="ID" AllowSorting="true" OnSorting="gvDeclSinValorar_Sorting" AutoGenerateColumns="false"
                        Width="100%" OnPageIndexChanged="gvDeclSinValorar_PageIndexChanged">
                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="Id" SortExpression="Id" />
                            <asp:BoundField DataField="NombreDeclarante" HeaderText="Declarante" SortExpression="NombreDeclarante" />
                            <asp:BoundField DataField="DocumentoDeclarante" HeaderText="No. Documento" SortExpression="DocumentoDeclarante" />
                            <asp:BoundField DataField="FechaRadicado" HeaderText="Fecha de Radicado" SortExpression="FechaRadicado"
                                DataFormatString="<%$ AppSettings:FechaGrilla %>" />
                            <asp:BoundField DataField="NumeroFormulario" HeaderText="Numero Formulario" SortExpression="NumeroFormulario" />
                            <asp:BoundField DataField="TotalHv" HeaderText="Total Hv" SortExpression="TotalHv" />
                            <asp:BoundField DataField="Departamento" HeaderText="Departamento" SortExpression="Departamento" />
                            <asp:BoundField DataField="Municipio" HeaderText="Municipio" SortExpression="Municipio" />
                            <asp:BoundField DataField="Entidad" HeaderText="Entidad del MP" SortExpression="Entidad" />
                            <asp:TemplateField HeaderText="Asignar">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelec" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Detalle">
                                <ItemTemplate>
                                    <asp:ImageButton ID="img" runat="server" SkinID="imgBuscar" CommandName="Select"
                                        CausesValidation="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" StartRowIndexParameterName="startRow"
                        MaximumRowsParameterName="pageSize" SelectCountMethod="CantidadSinValorar" SelectMethod="ObtenerDeclaracionesSinValorar"
                        TypeName="DataSourceAsignacion" SortParameterName="sortColumns" OnObjectCreated="dataEmpInfo_ObjectCreated"
                        EnablePaging="true"></asp:ObjectDataSource>
                </asp:View>
                <asp:View ID="vDetalle" runat="server">
                    <asp:GridView ID="gvPersonasAnexos" runat="server" SkinID="GridViewSinPaginacion"
                        DataKeyNames="Id" AutoGenerateColumns="false" Width="100%">
                        <Columns>
                            <asp:BoundField DataField="Id" HeaderText="Id" Visible="false" />
                            <asp:BoundField DataField="Persona" HeaderText="Persona" />
                            <asp:BoundField DataField="TipoDocumento" HeaderText="Tipo de Documento" />
                            <asp:BoundField DataField="NumeroDocumento" HeaderText="Numero de Documento" />
                            <asp:BoundField DataField="Relacion" HeaderText="Relación" />
                            <asp:BoundField DataField="GeneroNombre" HeaderText="Sexo" />
                            <asp:BoundField DataField="Edad" HeaderText="Edad" />
                            <asp:BoundField DataField="EtniaNombre" HeaderText="Etnia" />
                            <%--<asp:BoundField DataField="Hechos" HeaderText="Hechos Vicimizantes" />--%>
                            <asp:TemplateField HeaderText="Hechos Victimizantes">
                                <ItemTemplate>
                                    <div style="text-align: left">
                                        <asp:Label ID="lblH" runat="server" Text='<%# Eval("Hechos") %>'></asp:Label>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CheckBoxField DataField="Discapacitado" HeaderText="Discapacitado" />
                        </Columns>
                    </asp:GridView>
                </asp:View>
            </asp:MultiView>
            <ruv:ModalPopUp ID="mpGuardar" runat="server" MostrarImagen="true" filatextBox="false"
                DropShadow="true" BehaviorID="mpGuardarBehavior" MostrarBotones="false" VisibleBotonCancelar="false"
                OnOk="mpGuardar_Ok" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>