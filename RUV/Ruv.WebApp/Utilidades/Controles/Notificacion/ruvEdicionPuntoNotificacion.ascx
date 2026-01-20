<%@ Control Language="C#" AutoEventWireup="true" Inherits="Utilidades_Controles_Notificacion_ruvEdicionPuntoNotificacion" Codebehind="ruvEdicionPuntoNotificacion.ascx.cs" %>
<asp:Panel ID="pnlEdicionPuntoNotificacion" runat="server" SkinID="PanelmodalPopup" ClientIDMode="Static">
    <div style="width:500px">
        <asp:Panel ID="tituloEdicionPuntoNotificacion" runat="server" SkinID="pnlTitulo">
            <asp:Label ID="lblTituloEdicionPuntoNotificacion" runat="server" Text="Punto de Notificación"></asp:Label>
        </asp:Panel>
        <div class="multipleSelectionPopup">
            <asp:HiddenField ID="puntoNotificacionIdNotificacion" runat="server" ClientIDMode="Static" />
            <span>País</span>
            <div>
                <ruv:DropDownList ID="puntoNotificacionPais" IdCombo="puntoNotificacionPais" runat="server" AutoPostBack="false" />
            </div>
            <span>Departamento</span>
            <div>
                <ruv:DropDownList ID="puntoNotificacionDepartamento" IdCombo="puntoNotificacionDepartamento" runat="server" AutoPostBack="false" />
            </div>
            <span>Municipio</span>
            <div>
                <ruv:DropDownList ID="puntoNotificacionMunicipio" IdCombo="puntoNotificacionMunicipio" runat="server" AutoPostBack="false" />
            </div>
            <span>Punto de Notificación</span>
            <div>
                <ruv:DropDownList ID="puntoNotificacionPuntoNotificacion" IdCombo="puntoNotificacionPuntoNotificacion" runat="server" AutoPostBack="false" />
            </div>
            <span>Dirección</span>
            <div>    
                <asp:TextBox ID="puntoNotificacionDireccion" runat="server" Width="100%" ClientIDMode="Static"></asp:TextBox>
                <%--<asp:RequiredFieldValidator ID="rv_txtDireccion" runat="server" ErrorMessage="El Campo es Requerido" ValidationGroup="GrupoNotificacion" ControlToValidate="direccionCorrespondenciaDireccion">*</asp:RequiredFieldValidator>
                <ajax:ValidatorCalloutExtender ID="vce_rv_txtDireccion" BehaviorID="vce_rv_txtDireccion" runat="server" Enabled="True" TargetControlID="rv_txtDireccion">
                </ajax:ValidatorCalloutExtender>--%>            
            </div>
        </div>
        <div>
            <br />
            <asp:Button ID="btnGuardarPuntoNotificacion" ClientIDMode="Static" runat="server" Text="Guardar" CausesValidation="false" />
            <asp:Button ID="btnCancelarPuntoNotificacion" runat="server" Text="Cancelar" CausesValidation="false" />
        </div>
    </div>
</asp:Panel>
<asp:LinkButton ID="lnkEdicionPuntoNotificacion" runat="server"></asp:LinkButton>
<ajax:ModalPopupExtender ID="mpopUpEdicionPuntoNotificacion" runat="server" SkinID="PopUp" TargetControlID="lnkEdicionPuntoNotificacion" DropShadow="true" BehaviorID="mpopUpEdicionPuntoNotificacionBehavior" PopupControlID="pnlEdicionPuntoNotificacion" CancelControlID="btnCancelarPuntoNotificacion"></ajax:ModalPopupExtender>
<script type="text/javascript" src='<%= Page.ResolveClientUrl("~/JScripts/Notificaciones/ruv.notificaciones-editarpuntonotificacion.js") %>'></script>