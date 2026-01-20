<%@ Control ClassName="UCTarea" Language="C#" AutoEventWireup="true" Inherits="ListaTareas_UCTarea" Codebehind="UCTarea.ascx.cs" %>

<%--    BackColor="#EEEEEE"
    BorderColor="Silver" 
    BorderStyle="Dotted" 
    HorizontalAlign="Center" --%>
<link rel="Stylesheet" type="text/css" href="~/App_Themes/RUVTheme/DPS.css" id="style" runat="server" visible="false" />

<asp:Panel ID="pnlFormulario" runat="server" Width="250px" Height="70px" HorizontalAlign="Center" CssClass="tarea"><%--SkinID="pnlTarea"--%>
    
    <asp:Label ID="lblFormulario" runat="server" Font-Bold="True" CssClass="lbl" 
        Text="Formulario" Font-Size="Large" Font-Strikeout="False"></asp:Label>
        &nbsp;&nbsp;
<%--    <asp:ImageButton ID="imgTrabajar" runat="server" CausesValidation="false" 
        CommandArgument="" CommandName="Select"
        onclick="imgTrabajar_Click" 
        ImageUrl="~/App_Themes/RUVTheme/Imagenes/Trabajar.png" />--%>
        <%--SkinID="imgTrabajar" --%>
    <asp:HyperLink ID="HLinkTrabajar" runat="server" cssClass="screw" >
           <%-- <img alt="Trabajar" src="../App_Themes/RUVTheme/Imagenes/Trabajar.png"/>--%>
    </asp:HyperLink>
    <div style="text-align: left">
    <asp:Label ID="lblEstado" Text="-" runat="server" CssClass="lbl" />
    <br />
    <asp:Label ID="lblFecha" Text="-" runat="server" CssClass="lbl" />    
    </div>
</asp:Panel>
<asp:HiddenField ID="hfIdDeclaracion" runat="server" />
<asp:HiddenField ID="hfIdCorreccion" runat="server" />