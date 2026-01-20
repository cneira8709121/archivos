<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Herramientas.ascx.cs"
    Inherits="Utilidades_Controles_dpsHerramientas" %>
<asp:Panel ID="Panel2" runat="server">
    <div id="dvAgregadas" runat="server" style="text-align: left; width: 100%; margin: 1,1,1,1">
        <ruv:ListBox ID="lbHerramientas" runat="server" Width="210px" Height="150px" />
        <br />
        <asp:LinkButton ID="btnAgregar" runat="server" Text="Agregar" SkinID="lbkNegro" CausesValidation="false" OnClick="btnAgregar_Click" />
        &nbsp;&nbsp;
        <asp:LinkButton ID="btnEditar" runat="server" Text="Modificar" SkinID="lbkNegro" CausesValidation="false" OnClick="btnEditar_Click" />
        &nbsp;&nbsp;
        <asp:LinkButton ID="tbnQuitar" runat="server" Text="Quitar" SkinID="lbkNegro" OnClick="tbnQuitar_Click" CausesValidation="false" />
    </div>
</asp:Panel>
<asp:Panel ID="Panel1" runat="server" Visible="false">
    <div id="dvNueva" runat="server" style="text-align: left; width: 100%; margin: 1,1,1,1">
        <asp:Label ID="lblHerramienta" runat="server" Text="Herramienta:"></asp:Label><br />
        <ruv:DropDownList ID="ddlTipoHerramienta" runat="server" AutoPostBack="true" EsRequerido="false" Width="200px"
            OnSelectIndexChange="ddlTipoHerramienta_SelectIndexChange" Source="TipoHerramientas" />
        <br />
        <asp:Label ID="lblFuentes" runat="server" Text="Fuente:"></asp:Label><br />
        <ruv:DropDownList ID="ddlFuentes" runat="server" Width="200px" EsRequerido="true"
            MensajeError="Seleccione la fuente" />
        <br />
        <asp:Label ID="lblFecha" runat="server" Text="Fecha:"></asp:Label><br />
        <ruv:TextCalendar ID="txtFecha" runat="server" Width="80px" EsRequerido="true" MensajeError="Seleccione la Fecha" ClientScript="Desbloquear()" /><br />
        <div id="dvNuevaFuente" runat="server" visible="false">
            <asp:Label ID="lblNuevaFuente" runat="server" Text="Cual?:"></asp:Label><br />
            <ruv:TextBox ID="txtFuente" runat="server" EsRequerido="true" MensajeRequerido="Indique la fuente" Script="Desbloquear()" />
        </div>
        <asp:CheckBox ID="chkUsadoParaDesicio" runat="server" Text="¿Es Usada Para la Decisión?" />
        <br />
        <asp:Label ID="lblDescripcion" runat="server" Text="Descripcion:"></asp:Label><br />
        <ruv:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" EsRequerido="false"
            Width="90%" Height="100px" />
        <asp:LinkButton ID="btnAccion" runat="server" Text="Guardar" 
            OnClick="btnAccion_Click" OnClientClick="Bloquear()" 
            SkinID="lbkNegro" ClientIDMode="Static" />&nbsp;&nbsp;
        <asp:LinkButton ID="btnCancel" runat="server" Text="Cancelar" SkinID="lbkNegro" CausesValidation="false" OnClick="btnCancel_Click" />
    </div>
</asp:Panel>
