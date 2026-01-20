<%@ Page Language="C#" Title="Asignar a Valoración" AutoEventWireup="true" MasterPageFile="~/Site.Master" Inherits="Valoracion_AsignarValoraciones" CodeBehind="AsignarValoraciones.aspx.cs" %>

<%@ Register TagPrefix="ruvv" Src="~/Utilidades/Controles/ruvTextBox.ascx" TagName="TextBox" %>
<%@ Register TagPrefix="ruvv" Src="~/Utilidades/Controles/ruvTextCalendar.ascx" TagName="TextCalendar" %>
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

                    <asp:Panel ID="pblseparador" runat="server" SkinID="pnlTitulo">
                        <asp:Label ID="lblTitulo" runat="server" Text="ASIGNAR DECLARACION" SkinID="lblSubTitulo" />
                    </asp:Panel>
                    <div id="filtro" style="border: 1px solid">
                        <asp:Panel ID="pnlTituloFiltro" runat="server" SkinID="pnlTitulo" >
                            <asp:Label ID="Label1" runat="server" Text="FILTRO" SkinID="lblSubTitulo" ></asp:Label>
                        </asp:Panel>
                        <ajax:CollapsiblePanelExtender ID="cpePnlFiltro" runat="server" TargetControlID="pnlFiltro" ExpandControlID="pnlTituloFiltro" CollapseControlID="pnlTituloFiltro" Collapsed="true" SuppressPostBack="true">
                        </ajax:CollapsiblePanelExtender>
                        <asp:Panel ID="pnlFiltro" runat="server">
                            <table id="Table1" runat="server" visible="true">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblNombreDeclarante" runat="server" Text="Nombre Declarante:"></asp:Label>
                                        <ruvv:TextBox ID="txtNombreDeclarante" runat="server" EsRequerido="false" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblDocumentoDeclarante" runat="server" Text="Documento Declarante:"></asp:Label>
                                        <ruvv:TextBox ID="txtDocumentoDeclarante" runat="server" EsRequerido="false" ViewStateMode="Enabled" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblNumeroFormulario" runat="server" Text="Numero Formulario:"></asp:Label>
                                        <ruvv:TextBox ID="txtNumeroFormulario" runat="server" EsRequerido="false" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblEstadoValoracion" runat="server" Text="Estado Valoracion:"></asp:Label>
                                        <ruvv:TextBox ID="txtEstadoValoracion" runat="server" EsRequerido="false" ViewStateMode="Enabled" />
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <table id="Table4" runat="server" visible="true">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblRegimenEspecial" runat="server" Text="Regimen Especial:"></asp:Label>
                                        <ruvv:TextBox ID="txtRegimenEspecial" runat="server" EsRequerido="false" ViewStateMode="Enabled" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblEtnia" runat="server" Text="Etnia:"></asp:Label>
                                        <ruvv:TextBox ID="txtEtnia" runat="server" EsRequerido="false" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblGenero" runat="server" Text="Genero:"></asp:Label>
                                        <ruvv:TextBox ID="txtGenero" runat="server" EsRequerido="false" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblValorFecha1" runat="server" Text="Fecha Inicial:"></asp:Label>
                                        <ruvv:TextCalendar ID="txtFecha1" runat="server" EsRequerido="false" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblValorFecha2" runat="server" Text="Fecha Final:"></asp:Label>
                                        <ruvv:TextCalendar ID="txtFecha2" runat="server" EsRequerido="false" />
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <table id="Table5" runat="server" visible="true">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblEntidad" runat="server" Text="Entidad:"></asp:Label>
                                        <ruvv:TextBox ID="txtEntidad" runat="server" EsRequerido="false" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblMunicipio" runat="server" Text="Municipio:"></asp:Label>
                                        <ruvv:TextBox ID="txtMunicipio" runat="server" EsRequerido="false" ViewStateMode="Enabled" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblDepartamento" runat="server" Text="Departamento:"></asp:Label>
                                        <ruvv:TextBox ID="txtDepartamento" runat="server" EsRequerido="false" ViewStateMode="Enabled" />
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <table id="Table2" runat="server" visible="true">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblValorador" runat="server" Text="Asignar a Valorador:"></asp:Label>
                                    </td>
                                    <td>
                                        <ruv:DropDownList ID="ddlValorador" runat="server" DataTextField="NOMBRE_COMPLETO" DataValueField="ID" EsRequerido="true" MensajeError="Seleccione un Valorador" />
                                    </td>
                                </tr>
                            </table>
                            `                               
                            <br />
                            <div style="text-align: right">
                                <table id="Table3" runat="server" visible="true">
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" CausesValidation="false" ValidationGroup="vgFiltro"
                                                OnClick="btnFiltrar_Click" OnClientClick="return ShowModConsult(null, null, 'vgFiltro')" />
                                            <td>
                                                <asp:Button ID="btnReset" runat="server" Text="Restablecer" CausesValidation="false" OnClick="btnReset_Click" />
                                            </td>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:Panel>
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
                            <asp:BoundField DataField="Estado" HeaderText="Estado" SortExpression="Estado" />
                            <asp:BoundField DataField="RegimenEspecial" HeaderText="Regimen Especial" SortExpression="RegimenEspecial" />
                            <asp:BoundField DataField="Genero" HeaderText="Genero" SortExpression="Genero" />
                            <asp:BoundField DataField="Etnia" HeaderText="Etnia" SortExpression="Etnia" />
                            <asp:BoundField DataField="FechaDeclaracion" HeaderText="Fecha Declaracion" SortExpression="FechaDeclaracion" DataFormatString="<%$ AppSettings:FechaGrilla %>" />
                            <asp:BoundField DataField="FechaVencimiento" HeaderText="Fecha Vencimiento" SortExpression="FechaVencimiento" DataFormatString="<%$ AppSettings:FechaGrilla %>" />

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
