<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" Inherits="ControlDocumentos_ControlDocumentos" Codebehind="ControlDocumentos.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <ruv:ModalPopUp ID="mpuMensaje" runat="server" DropShadow="true" MostrarBotones="false" MostrarImagen="true" VisibleBotonCancelar="false" BehaviorID="mpuMensajeBehavior"  />
    <ruv:ModalPopUp ID="mpuMensajeInfo" runat="server" DropShadow="true" MostrarBotones="true" MostrarImagen="false" VisibleBotonCancelar="true" OnOk="GenerarFormularios" BehaviorID="mpuMensajeInfoBehavior" />
    <p>
        <asp:Label ID="lblSerie" runat="server" Text="Serie" Visible="true"></asp:Label>
        <asp:TextBox ID="txtSerie" runat="server" Text="D" ReadOnly="true" Visible="true"></asp:TextBox>

        <asp:Label ID="lblCantidad" runat="server" Text="Cantidad"></asp:Label>
        <asp:TextBox ID="txtCantidad" runat="server"></asp:TextBox>
        <ajax:FilteredTextBoxExtender ID="txtCantidad_FilteredTextBoxExtender" FilterType="Numbers" 
            runat="server" Enabled="True" TargetControlID="txtCantidad">
        </ajax:FilteredTextBoxExtender>
        <asp:Button ID="btnGenerar" runat="server" Text="Generar" 
            onclick="btnGenerar_Click" />
    </p>

    <table width="100%">
            <tr>
                <td>
                    <asp:Panel ID="PanelConsulta" Visible="true" runat="server" Width="100%">
<%--                    <asp:UpdatePanel ID="updPnlAceptar" runat="server">
                        <ContentTemplate>--%>
                            <asp:Panel ID="pblseparador" runat="server" SkinID="pnlTitulo">
                                <asp:Label ID="lblTitulo" runat="server" Text="CONTROL DE DOCUMENTOS"
                                    SkinID="lblSubTitulo" />
                            </asp:Panel>
                            <br />
                            <asp:GridView ID="grdDocumentos" runat="server" AllowPaging="True" 
                                PageSize="5" PagerSettings-Mode="Numeric"
                                AutoGenerateColumns="False" DataKeyNames="NId" SkinID="GridViewConPaginacion"
                                Width="100%" onrowcommand="grdDocumentos_RowCommand" 
                                onpageindexchanging="grdDocumentos_PageIndexChanging" 
                                onrowdatabound="grdDocumentos_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="Seleccionar"> 
                                        <ItemTemplate> 
                                            <asp:CheckBox ID="chkSelect" runat="server" /> 
                                        </ItemTemplate> 
                                    </asp:TemplateField> 
                                    <asp:BoundField DataField="NId" HeaderText="Id" Visible="false" /> 
                                    <asp:BoundField DataField="CNumeroFormulario" HeaderText="Numero Formulario" />     
                                    <asp:TemplateField HeaderText="Exportar PDF">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ExamineButton" runat="server" SkinID="imgGenerarPDF" CommandName="ExportarPDF" CommandArgument='<%# Eval("CNumeroFormulario") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            <EmptyDataTemplate>
                                No hay registros que coincidan con los criterios de busqueda
                            </EmptyDataTemplate>
                            </asp:GridView>
                            <p>
                                <asp:Button ID="btnGenerarPDFs" runat="server" Text="Exportar seleccionados" onclick="btnGenerarPDFs_Click" />
                            </p>
<%--                        </ContentTemplate>
                    </asp:UpdatePanel>--%>
                    </asp:Panel>                        
                </td>
            </tr>
        </table>

</asp:Content>

