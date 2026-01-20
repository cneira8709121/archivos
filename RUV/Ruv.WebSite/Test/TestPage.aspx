<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeFile="TestPage.aspx.cs" Inherits="Test_TestPage" %>
<%@ Reference Control="~/ListaTareas/UCTarea.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Scripts" runat="Server">
    <%--    <script type="text/javascript" src="../JScripts/jquery-1.8.2.js"></script>
    <script type="text/javascript" src="../JScripts/Valoracion/JPuntosNotificacion.js"></script>
    <script type="text/javascript" src="../JScripts/JListaTareasHelper.js"></script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="Server">

    <style type="text/css">
        .style1
        {
            width: 353px;
        }
        .style2
        {
            width: 251px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    <script type="text/javascript" src="../JScripts/jquery-1.8.2.js"></script>
<script type="text/javascript" src="../JScripts/JListaTareasHelper.js"></script>
    <asp:Panel ID="pblseparador" runat="server" SkinID="pnlTitulo">
        <asp:Label ID="lblTitulo" runat="server" Text="NUEVA LISTA DE TAREAS" SkinID="lblSubTitulo" />
    </asp:Panel>

    <br />

<%--    <asp:GridView ID="grvTareas" runat="server" SkinID="GridViewListaTareas" DataKeyNames="Declaracion"
        AllowSorting="True" Width="100%" AutoGenerateColumns="False" DataSourceID="odsTareas"
        OnRowCommand="grvTareas_RowCommand">
        <Columns>
            <asp:BoundField DataField="Declaracion" HeaderText="ID" SortExpression="ID" Visible="false" />
            <asp:TemplateField HeaderText="" SortExpression="Formulario">
                <ItemTemplate>
                            <table style="width: 100%;">
                                <tr>
                                    <td class="style2" valign="top" align="left">
                                        <asp:Panel ID="pnlFormulario" runat="server" BackColor="#EEEEEE" 
                                            BorderColor="Silver" BorderStyle="Dotted" HorizontalAlign="Center" Width="90%">
                                            <br />
                                            <asp:Label ID="Label1" runat="server" Font-Bold="True" 
                                                Text='<%# Bind("Formulario") %>'></asp:Label>
                                            &nbsp;&nbsp;
                                            <asp:ImageButton ID="img" runat="server" CausesValidation="false" 
                                                CommandArgument='<%# Eval("Declaracion") + "|"+ Eval("Correccion")%>' 
                                                CommandName="Select" SkinID="imgTrabajar" />
                                            <br />
                                            <br />
                                        </asp:Panel>
                                    </td>
                                    <td rowspan="2" valign="bottom">
                                        <asp:TextBox ID="TextBox1" runat="server" Height="100%" TextMode="MultiLine" Text='<%# Eval("Accion") + "<br />" + Eval("Declaracion") + "<br />" + Eval("Correccion") %>'
                                            Width="100%" ReadOnly="True" Visible="false"></asp:TextBox>
                                        <asp:Label ID="lblDatos" runat="server" Text='<%# Eval("Accion") + "<br />" + Eval("Fecha") %>' />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style2">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="odsTareas" runat="server" StartRowIndexParameterName="startRow"
        MaximumRowsParameterName="pageSize" SelectCountMethod="CantidadTareas" SelectMethod="ObtenerListaTareas"
        SortParameterName="sortColumns" EnablePaging="True" TypeName="DataSourceListaTareas"
        OnObjectCreated="odsTareas_ObjectCreated"></asp:ObjectDataSource>
    <br />--%>
    <%--        <Columns>
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
        </Columns>--%>

    <br />

    <asp:Panel ID="pnlTareas" runat="server" ScrollBars="Vertical">
    </asp:Panel>
    <br /><br />
    <div id="divTareas">Reemplazar esto</div>

    <%--<asp:Button ID="btnAdd" runat="server" Text="Add" OnClientClick="return false;" />--%>
    <button id="btnAdd">Show Text</button>
    <br /><br />
    ---
    <ruv:DropDownList ID="ddlTipoDocumento" runat="server" Enabled="True" 
        Valor="29" Source="Parametros">
    </ruv:DropDownList>
    ---

    <asp:Button ID="btnGenerar" runat="server" Text="Generar" OnClick="btnGenerar_Click" />
    <asp:Button ID="btnRegenerar" runat="server" Text="Re-Generar" OnClick="btnRegenerar_Click" />
    <br />
    <p>
        <asp:Button ID="btnSolicitarCorreccion" runat="server" Text="Solicitar Corrección"
            OnClick="btnSolicitarCorreccion_Click" />
        <asp:Button ID="btnConsultarCorreccion" runat="server" Text="Consultar Corrección"
            OnClick="btnConsultarCorreccion_Click" />
    </p>
    <p>
        <asp:TextBox ID="txtObservaciones" runat="server" TextMode="MultiLine"></asp:TextBox>
        <asp:Button ID="btnRechazar" runat="server" Text="Rechazar Corrección" OnClick="btnRechazar_Click" />
    </p>
    <p>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
    </p>
    <p>
        <asp:TextBox ID="txtNuevaDireccion" runat="server"></asp:TextBox>
        <asp:Button ID="Button2" runat="server" Text="Actualizar Notificacion" OnClick="Button2_Click" />
    </p>
    <p>
        <asp:Button ID="btnNotificaciones" runat="server" Text="Consultar Notificaciones"
            OnClick="btnNotificaciones_Click" />
        <asp:GridView ID="grdNotificaciones" runat="server">
        </asp:GridView>
    </p>
    <p>
        <asp:TextBox ID="txtNumeroDocumentos" runat="server"></asp:TextBox>
        <asp:Button ID="btnGenerarDocumentos" runat="server" Text="Generar" OnClick="btnGenerarDocumentos_Click" />
        <br />
        <asp:Label ID="lblComienza" runat="server" Text="Comienza: "></asp:Label>
        <br />
        <asp:Label ID="lblTermina" runat="server" Text="Termina: "></asp:Label>
    </p>
    <p>
        <asp:Button ID="btnTest" runat="server" Text="Hacer Test" />
    </p>
    <div id="divTest">
        aqui</div>
</asp:Content>
