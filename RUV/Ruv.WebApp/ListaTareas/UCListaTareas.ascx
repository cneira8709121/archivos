<%@ Control Language="C#" AutoEventWireup="true" Inherits="ListaTareas_UCListaTareas" Codebehind="UCListaTareas.ascx.cs" %>
<%@ Reference Control="~/ListaTareas/UCTarea.ascx" %>
<script type="text/javascript" src="../JScripts/JListaTareasHelper.js"></script>
    <script type="text/javascript">
//        var count = 0;
//        $(window).scroll(function () {
//            if ($(window).scrollTop() == $(document).height() - $(window).height()) {
//                //alert('More data ' + count);
//                CargarTareas();
//                count++;
//            }
//        });   
    </script>
    <div>
        <ruv:Filtros ID="Filtros1" runat="server" Procesos="ListaTareas" OnFiltro="filtro_Filtro" blnOrderByVisible="true" />
        <asp:HiddenField ID="HFFiltroPor" runat="server" />
        <asp:HiddenField ID="HFOrdenPor" runat="server" />
    </div>
    <asp:Panel ID="pblseparador" runat="server" SkinID="pnlTitulo">
        <asp:Label ID="lblTitulo" runat="server" Text="LISTA DE TAREAS" SkinID="lblSubTitulo" />
    </asp:Panel>
    <br />
    <asp:Panel ID="pnlTareasPendientes" runat="server" ScrollBars="None" Height="100%">
    </asp:Panel>
    <div id="divTareas"></div>
<%--    <asp:GridView ID="grvTareas" runat="server" SkinID="GridViewConPaginacion" 
        DataKeyNames="Declaracion,Correccion" AllowSorting="true" Width="100%" 
        AutoGenerateColumns="False" DataSourceID="odsTareas" 
        onrowcommand="grvTareas_RowCommand">
        <Columns>
            <asp:BoundField DataField="Declaracion" HeaderText="ID" SortExpression="ID" Visible="false" />
            <asp:BoundField DataField="Fecha" HeaderText="Fecha" SortExpression="Fecha" DataFormatString="{0:d}" />
            <asp:BoundField DataField="Accion" HeaderText="Estado" SortExpression="Accion" />
            <asp:BoundField DataField="Formulario" HeaderText="Formulario" SortExpression="Formulario" />
            <asp:BoundField DataField="Declaracion" HeaderText="Declaracion" SortExpression="Declaracion" Visible="false" />
            <asp:TemplateField HeaderText="Ir">
                <ItemTemplate>
                    <asp:ImageButton ID="img" runat="server" SkinID="imgBuscar" CommandName="Select" CommandArgument='<%# Eval("Declaracion") + "|"+ Eval("Correccion")%>'
                        CausesValidation="false" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="odsTareas" runat="server" 
        StartRowIndexParameterName="startRow" MaximumRowsParameterName="pageSize"
        SelectCountMethod="CantidadTareas" SelectMethod="ObtenerListaTareas" 
        SortParameterName="sortColumns" OnObjectCreated="odsTareas_ObjectCreated"
        EnablePaging="True" TypeName="DataSourceListaTareas">
    </asp:ObjectDataSource>--%>