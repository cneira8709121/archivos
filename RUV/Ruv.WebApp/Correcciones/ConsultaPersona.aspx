<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" Inherits="Ruv.WebApp.Presentation.Correcciones.ConsultaPersona" Codebehind="ConsultaPersona.aspx.cs" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Head" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ID="main" ContentPlaceHolderID="MainContent" runat="server">
    <ruv:ConsultaAdmin ID="wuConsulta" runat="server" OnButtonClick="wuConsulta_OnButtonClick" />
    <table width="100%">
        <tr>
            <td>
                <asp:Panel ID="PanelConsulta" Visible="false" runat="server" Width="100%">
                    <asp:UpdatePanel ID="updPnlAceptar" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="GridConsulta" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                DataSourceID="OdsConsulta" DataKeyNames="NIdRegistroPresona" SkinID="GridViewConPaginacion" 
                                OnSelectedIndexChanged="GridConsulta_SelectedIndexChanged" Width="100%">
                                <Columns>
                                    <asp:BoundField DataField="CNumeroFormulario" HeaderText="Numero Formulario" />
                                    <asp:BoundField DataField="CNombresApellidos" HeaderText="Nombres y Apellidos" ItemStyle-Wrap="false" />
                                    <asp:BoundField DataField="CTipoDocumento" HeaderText="Tipo Documento" />
                                    <asp:BoundField DataField="CNumeroDocumento" HeaderText="Documento" />
                                    <asp:BoundField DataField="CEstadoProceso" HeaderText="Estado Proceso" HeaderStyle-Wrap="false" />
                                    <asp:BoundField DataField="DDeclaracion" HeaderText="Fecha Declaracion" DataFormatString="{0:d}" />
                                    <asp:BoundField DataField="CPais" HeaderText="Pais" />
                                    <asp:BoundField DataField="CDepartamento" HeaderText="Departamento" />
                                    <asp:BoundField DataField="CMunicipio" HeaderText="Municipio" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ExamineButton" runat="server" CommandName="Select" SkinID="imgBuscar" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    No hay registros que coincidan con los criterios de busqueda
                                </EmptyDataTemplate>
                            </asp:GridView>
                            <asp:ObjectDataSource ID="OdsConsulta" runat="server" TypeName="DataSourceCorrecciones"
                                StartRowIndexParameterName="startRow" MaximumRowsParameterName="maxRows" SelectCountMethod="VirtualItemCount"
                                SelectMethod="GetData" EnablePaging="True" 
                                OnObjectCreated="OdsConsulta_ObjectCreated">
                            </asp:ObjectDataSource>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
