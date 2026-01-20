<%@ Page Title="Valoración" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" Inherits="Valoracion_Default" Codebehind="Default.aspx.cs" %>
<%@ MasterType VirtualPath="~/Site.Master" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript" src='<%= Page.ResolveClientUrl("~/JScripts/Valoracion/ruv.valoracion-listavaloraciones.js") %>'></script>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div>
                <ruv:Filtros ID="filtro" runat="server" Procesos="Valoracion" OnFiltro="filtro_Filtro" />
            </div>
            <center>
                <asp:Label ID="lblError" runat="server" SkinID="lblError"></asp:Label>
                <br />
                <asp:GridView ID="grdValoraciones" runat="server" DataKeyNames="ValoracionId" SkinID="GridViewConPaginacion" DataSourceID="odsListaTareas" AllowSorting="true" Width="100%" AutoGenerateColumns="false" OnSelectedIndexChanged="grdValoraciones_SelectedIndexChanged"  onrowdatabound="grdValoraciones_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="ValoracionId" HeaderText="Valoracion" Visible="false" />
                        <asp:BoundField DataField="ValoradorId" HeaderText="ValoradorId" Visible="false" />
                        <asp:BoundField DataField="Declarante" HeaderText="Declarante" SortExpression="Declarante" />
                        <asp:BoundField DataField="DocumentoDeclarante" HeaderText="Docuemento Declarante" SortExpression="DocumentoDeclarante" />
                        <asp:BoundField DataField="FechaRadicacion" HeaderText="Fecha Radicacion" SortExpression="FechaRadicacion"
                            DataFormatString="<%$ AppSettings:FechaGrilla %>" />
                        <%--<asp:BoundField DataField="NumeroFormulario" HeaderText="Numero de Formulario" SortExpression="Formulario" />--%>
                        <asp:HyperLinkField DataNavigateUrlFields="IdDeclaracion" DataNavigateUrlFormatString="../../Consultas/DetalleFormulario.aspx?id={0}&urlEvio=../Valoracion/Valoracion/Default.aspx" 
                            DataTextField="NumeroFormulario" HeaderText="Numero de Formulario" SortExpression="Formulario" />
                        <asp:TemplateField HeaderText="Total Hechos">
                            <ItemTemplate>
                                <asp:HyperLink ID="lblH" runat="server" Text='<%# Eval("TotalHv") + " Hechos" %>'
                                    ToolTip='<%# Eval("HechosVictimizantes") %>' SkinID="lnkNegro"></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Hechos Victimizantes">
                            <ItemTemplate>
                                <asp:Label ID="lblNombres" runat="server" Text='<%# Eval("HechosVictimizantes") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="FechaAsignacion" HeaderText="Fecha Asignacion" SortExpression="FechaAsignacion"
                            DataFormatString="<%$ AppSettings:FechaGrilla %>" />
                        <asp:BoundField DataField="Estado" HeaderText="Estado" SortExpression="Estado" />
                        <asp:BoundField DataField="Observacion" HeaderText="Observacion" Visible="false" />
                        <asp:BoundField DataField="FechaActualizacion" HeaderText="Fecha Actualización" Visible="false" />
                        <asp:TemplateField HeaderStyle-Width="30px" HeaderText="Valorar">
                            <ItemTemplate>
                                <asp:ImageButton ID="img" ToolTip="Valorar" runat="server" SkinID="imgBuscar" CommandName="Select" OnClientClick="ShowModConsult(null, 'Abriendo Declaración...')" />
                                <input type="hidden" runat="server" class="gridWarningPrefix" value='<%# (Eval("FechaActualizacion") as DateTime? ?? DateTime.Now).ToString("D", new System.Globalization.CultureInfo("es-CO")) %>' />
                                <input type="hidden" runat="server" class="gridWarningValue" value='<%# Eval("Observacion") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:ObjectDataSource ID="odsListaTareas" runat="server" TypeName="DataSourceTareas"
                    EnablePaging="true" SelectMethod="GetData" SelectCountMethod="VirtualItemCount"
                    StartRowIndexParameterName="startRow" SortParameterName="sortColumns" MaximumRowsParameterName="maxRows"
                    OnObjectCreated="odsListaTareas_ObjectCreated"></asp:ObjectDataSource>
            </center>
            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
