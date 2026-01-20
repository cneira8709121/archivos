<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeFile="ConsultaPersona.aspx.cs" Inherits="Ruv.WebSite.Presentation.Consultas.ConsultaPersona" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="updConsulta" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <ruv:ConsultaAdmin ID="wuConsulta" runat="server" OnButtonClick="wuConsulta_OnButtonClick" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <table width="100%">
        <tr>
            <td>
                <asp:UpdatePanel ID="updPnlAceptar" UpdateMode="Conditional"  runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="PanelConsulta" Visible="false" runat="server" Width="100%">
                            <asp:GridView ID="GridConsulta" runat="server" AutoGenerateColumns="False"
                                DataSourceID="OdsConsulta" DataKeyNames="NIdDeclaracion" SkinID="GridViewConPaginacion" 
                                OnSelectedIndexChanged="GridConsulta_SelectedIndexChanged" Width="100%">
                                <Columns>
                                    <asp:BoundField DataField="CNumeroFormulario" HeaderText="Numero Formulario" />
                                    <asp:BoundField DataField="CNombresApellidos" HeaderText="Nombres y Apellidos" ItemStyle-Wrap="false" />
                                    <asp:BoundField DataField="CTipoDocumento" HeaderText="Tipo Documento" />
                                    <asp:BoundField DataField="CNumeroDocumento" HeaderText="Documento" />
                                    <asp:BoundField DataField="CEstadoProceso" HeaderText="Estado Proceso" HeaderStyle-Wrap="false" />
                                    <asp:BoundField DataField="DDeclaracion" HeaderText="Fecha Declaracion" DataFormatString="{0:d}" />
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
                            <asp:ObjectDataSource ID="OdsConsulta" runat="server" TypeName="DataSourceConsulta"
                                StartRowIndexParameterName="startRow" MaximumRowsParameterName="maxRows" SelectCountMethod="VirtualItemCount"
                                SelectMethod="GetData" EnablePaging="true" OnObjectCreated="OdsConsulta_ObjectCreated">
                            </asp:ObjectDataSource>
                        </asp:Panel>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="wuConsulta" EventName="ButtonClick" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
