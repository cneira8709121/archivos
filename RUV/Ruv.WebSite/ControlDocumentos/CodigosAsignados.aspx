<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeFile="CodigosAsignados.aspx.cs" Inherits="ControlDocumentos_CodigosAsignados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="upPanel" runat="server">
        <ContentTemplate>
            <ruv:ModalPopUp ID="mpuMensaje" runat="server" DropShadow="true" MostrarBotones="false" BehaviorID="mpuMensajeBehaviorID" 
                Mensaje="sadsadsa" MostrarImagen="true" VisibleBotonCancelar="false" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--<ruv:ModalPopUp ID="mpuMensajeInfo" runat="server" DropShadow="true" MostrarBotones="true" MostrarImagen="false" VisibleBotonCancelar="true" OnOk="" BehaviorID="mpuMensajeInfoBehavior" />--%>
    <%--    <p>
        <asp:Label ID="lblCantidad" runat="server" Text="Cantidad"></asp:Label>
        <asp:TextBox ID="txtCantidad" runat="server"></asp:TextBox>
        <ajax:FilteredTextBoxExtender ID="txtCantidad_FilteredTextBoxExtender" FilterType="Numbers" 
            runat="server" Enabled="True" TargetControlID="txtCantidad">
        </ajax:FilteredTextBoxExtender>
        <asp:Button ID="btnGenerar" runat="server" Text="Generar" 
            onclick="btnGenerar_Click" />
    </p>--%>
    <table width="100%">
        <tr>
            <td>
                <asp:Panel ID="PanelConsulta" Visible="true" runat="server" Width="100%">
                    <asp:Panel ID="pblseparador" runat="server" SkinID="pnlTitulo">
                        <asp:Label ID="lblTitulo" runat="server" Text="FORMULARIOS ASIGNADOS" SkinID="lblSubTitulo" />
                    </asp:Panel>
                    <br />
                    <asp:GridView ID="grdDocumentos" runat="server" AllowPaging="True" AllowSorting="true"
                        PageSize="5" PagerSettings-Mode="Numeric" AutoGenerateColumns="False" DataKeyNames="NId"
                        SkinID="GridViewConPaginacion" Width="100%" OnRowCommand="grdDocumentos_RowCommand"
                        OnPageIndexChanging="grdDocumentos_PageIndexChanging" OnRowDataBound="grdDocumentos_RowDataBound"
                        OnSorting="grdDocumentos_Sorting">
                        <Columns>
                            <asp:TemplateField HeaderText="Seleccionar">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="NId" HeaderText="Id" Visible="false" />
                            <asp:BoundField DataField="CNumeroFormulario" HeaderText="Numero Formulario" SortExpression="CNumeroFormulario" />
                            <asp:TemplateField HeaderText="Descargado" SortExpression="BDescargado">
                                <ItemTemplate>
                                    <%# (Boolean.Parse(Eval("BDescargado").ToString())) ? "Si" : "No"%></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Exportar PDF">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ExamineButton" runat="server" SkinID="imgGenerarPDF" CommandName="ExportarPDF"
                                        CommandArgument='<%# Eval("CNumeroFormulario") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            No hay registros que coincidan con los criterios de busqueda
                        </EmptyDataTemplate>
                    </asp:GridView>
                    <p>
                        <asp:Button ID="btnGenerarPDFs" runat="server" Text="Exportar seleccionados" OnClick="btnGenerarPDFs_Click" />
                    </p>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
