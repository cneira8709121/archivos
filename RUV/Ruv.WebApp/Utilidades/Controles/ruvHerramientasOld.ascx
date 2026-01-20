<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="Utilidades_Controles_dpsHerramientasOld" Codebehind="ruvHerramientasOld.ascx.cs" %>
<%@ Register TagPrefix="ruvv" Src="~/Utilidades/Controles/ruvDropDownList.ascx" TagName="DropDownList" %>
<%@ Register TagPrefix="ruvv" Src="~/Utilidades/Controles/ruvTextBox.ascx" TagName="TextBox" %>
<%@ Register TagPrefix="ruvv" Src="~/Utilidades/Controles/ruvTextCalendar.ascx" TagName="TextCalendar" %>
<%@ Register TagPrefix="ruvv" Src="~/Utilidades/Controles/ruvListBox.ascx" TagName="ListBox" %>
<%--<asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="vCargadas" runat="server">--%>
<asp:Panel ID="Panel2" runat="server">
    <div id="dvAgregadas" runat="server" style="text-align: left; width: 100%; margin: 1,1,1,1">
        <ruvv:ListBox ID="lbHerramientas" runat="server" Width="210px" Height="150px" />
        <br />
        <asp:LinkButton ID="btnAgregar" runat="server" Text="Agregar" SkinID="lbkNegro" CausesValidation="false" OnClick="btnAgregar_Click" />
        &nbsp;&nbsp;
        <asp:LinkButton ID="btnEditar" runat="server" Text="Modificar" SkinID="lbkNegro" CausesValidation="false" OnClick="btnEditar_Click" Visible="false" />
        &nbsp;&nbsp;
        <asp:LinkButton ID="tbnQuitar" runat="server" Text="Quitar" SkinID="lbkNegro" OnClick="tbnQuitar_Click" CausesValidation="false" />
    </div>
</asp:Panel>
<%--/<asp:View>
        <asp:View ID="vNueva" runat="server">--%>
<asp:Panel ID="Panel1" runat="server" Visible="false">
    <div id="dvNueva" runat="server" style="text-align: left; width: 100%; margin: 1,1,1,1">
        <asp:Label ID="lblHerramienta" runat="server" Text="Herramienta:"></asp:Label><br />
        <ruvv:DropDownList ID="ddlTipoHerramienta" runat="server" AutoPostBack="true" EsRequerido="false" Width="200px"
            OnSelectIndexChange="ddlTipoHerramienta_SelectIndexChange" Source="TipoHerramientas" />
        <br />
        <asp:Label ID="lblFuentes" runat="server" Text="Fuente:"></asp:Label><br />
        <ruvv:DropDownList ID="ddlFuentes" runat="server" EsRequerido="true" OnSelectIndexChange="ddlFuentes_SelectIndexChange" Width="200px"
            MensajeError="Seleccione la fuente" AutoPostBack="true" />
        <br />
        <asp:Label ID="lblFecha" runat="server" Text="Fecha:"></asp:Label><br />
        <ruvv:TextCalendar ID="txtFecha" runat="server" Width="80px" EsRequerido="true" MensajeError="Seleccione la Fecha" /><br />
        <div id="dvNuevaFuente" runat="server" visible="false">
            <asp:Label ID="lblNuevaFuente" runat="server" Text="Cual?:"></asp:Label><br />
            <ruvv:TextBox ID="txtFuente" runat="server" EsRequerido="true" MensajeRequerido="Indique la fuente" />
        </div>
        <asp:CheckBox ID="chkUsadoParaDesicio" runat="server" Text="¿Es Usada Para la Desicion?" />
        <br />
        <asp:Label ID="lblDescripcion" runat="server" Text="Descripcion:"></asp:Label><br />
        <ruvv:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" EsRequerido="false"
            Width="90%" Height="100px" />
        <asp:LinkButton ID="btnAccion" runat="server" Text="Guardar" OnClick="btnAccion_Click"
            SkinID="lbkNegro" />&nbsp;&nbsp;
        <asp:LinkButton ID="btnCancel" runat="server" Text="Cancelar" SkinID="lbkNegro" CausesValidation="false" OnClick="btnCancel_Click" />
    </div>
    <%--</asp:View>
    </asp:MultiView>--%>
</asp:Panel>
