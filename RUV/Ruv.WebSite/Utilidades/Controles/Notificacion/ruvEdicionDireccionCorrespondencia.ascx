<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ruvEdicionDireccionCorrespondencia.ascx.cs" Inherits="Utilidades_Controles_Notificacion_ruvEdicionDireccionCorrespondencia" %>
<asp:Panel ID="pnlEdicionDireccionCorrespondencia" runat="server" SkinID="PanelmodalPopup" ClientIDMode="Static">
    <div style="width:500px">
        <asp:Panel ID="tituloEdicionDireccionCorrespondencia" runat="server" SkinID="pnlTitulo">
            <asp:Label ID="lblTituloEdicionDireccionCorrespondencia" runat="server" Text="Dirección de Correspondencia"></asp:Label>
        </asp:Panel>
        <div class="multipleSelectionPopup">
            <asp:HiddenField ID="direccionCorrespondenciaIdNotificacion" runat="server" ClientIDMode="Static" />
            <span>País</span>
            <div>
                <ruv:DropDownList ID="direccionCorrespondenciaPais" IdCombo="direccionCorrespondenciaPais" runat="server" AutoPostBack="false" />
            </div>
            <span>Departamento</span>
            <div>
                <ruv:DropDownList ID="direccionCorrespondenciaDepartamento" IdCombo="direccionCorrespondenciaDepartamento" runat="server" AutoPostBack="false" />
            </div>
            <span>Municipio</span>
            <div>
                <ruv:DropDownList ID="direccionCorrespondenciaMunicipio" IdCombo="direccionCorrespondenciaMunicipio" runat="server" AutoPostBack="false" />
            </div>
            <span>Dirección</span>
            <div>    
                <asp:TextBox ID="direccionCorrespondenciaDireccion" runat="server" Width="100%" ClientIDMode="Static"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rv_txtDireccion" runat="server" ErrorMessage="El Campo es Requerido" ValidationGroup="GrupoNotificacion" ControlToValidate="direccionCorrespondenciaDireccion">*</asp:RequiredFieldValidator>
                <ajax:ValidatorCalloutExtender ID="vce_rv_txtDireccion" BehaviorID="vce_rv_txtDireccion" runat="server" Enabled="True" TargetControlID="rv_txtDireccion">
                </ajax:ValidatorCalloutExtender>            
            </div>
        </div>
        <div>
            <br />
            <asp:Button ID="btnGuardar" ClientIDMode="Static" runat="server" Text="Guardar" CausesValidation="false" />
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CausesValidation="false" />
            <div id="respuesta"></div>
        </div>
    </div>
</asp:Panel>
<asp:LinkButton ID="lnkEdicionDireccionCorrespondencia" runat="server"></asp:LinkButton>
<ajax:ModalPopupExtender ID="mpopUpEdicionDireccionCorrespondencia" runat="server" SkinID="PopUp" TargetControlID="lnkEdicionDireccionCorrespondencia" DropShadow="true" BehaviorID="mpopUpEdicionDireccionCorrespondenciaBehavior" PopupControlID="pnlEdicionDireccionCorrespondencia" CancelControlID="btnCancelar"></ajax:ModalPopupExtender>
<script type="text/javascript" src='<%= Page.ResolveClientUrl("~/JScripts/Notificaciones/ruv.notificaciones-editardireccioncorrespondencia.js") %>'></script>