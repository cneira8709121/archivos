<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HechoVictimizante.ascx.cs"
    Inherits="Utilidades_Controles_dpsHechoVictimizante" %>
<asp:Panel ID="pnlNuevoHecho" runat="server" SkinID="PanelmodalPopup" ClientIDMode="Static">
    <center>
        <div id="dvMensajeValidacionHecho" class="dvMensaje">
            <asp:Label ID="lblMensajeValidación" runat="server" Text="Estado" SkinID="lblSubTitulo"
                ClientIDMode="Static"></asp:Label>
        </div>
        <asp:Label ID="lblError" runat="server" SkinID="lblError"></asp:Label>
    </center>
    <asp:Panel ID="pnlTituNuevoHecho" runat="server" SkinID="pnlTitulo">
        <asp:Label ID="lblTituNuevoHechos" runat="server" Text="NUEVO HECHO VICTIMIZANTE"></asp:Label>
    </asp:Panel>
    <table border="1" style="border-collapse: collapse; text-align: left">
        <tr class="dvRow">
            <td class="dvHeader" style="width: 150px">
                <asp:Label ID="lblTipoHecho" runat="server" Text="Tipo Hecho Victimizante" SkinID="lblBlanco"></asp:Label>
            </td>
            <td>
                <ruv:DropDownList ID="ddlHechosVictimizantes" runat="server" Valor="2137" Source="Parametros"
                    AutoPostBack="true" OnSelectIndexChange="ddlHechosVictimizantes_SelectIndexChange"
                    EsRequerido="false" MensajeError="Seleccione un hecho victimizante" SelectedValue='<%# Bind("TipoHecho") %>' />
                <span id="lblAn11" runat="server" Visible="false">
                    <asp:RadioButton ID="rbInmueble" runat="server" GroupName="rbTipoAnexo11" Text="Inmueble" Checked="true" />
                    <asp:RadioButton ID="rbMueble" runat="server" GroupName="rbTipoAnexo11" Text="Mueble" />
                    <asp:RadioButton ID="rbCredito" runat="server" GroupName="rbTipoAnexo11" Text="Crédito" />
                </span>
            </td>
        </tr>
        <tr class="dvRow">
            <td class="dvHeader" style="width: 150px">
                <asp:Label ID="lblFechaHecho" runat="server" Text="Fecha:" SkinID="lblBlanco"></asp:Label>
            </td>
            <td style="text-align: left">
                <ruv:TextCalendar ID="txtFecha" runat="server" EsRequerido="false" MensajeError="Indique la fecha de ocurrencia del hecho"
                    Fecha='<%# Bind("Fecha") %>' />
            </td>
        </tr>
        <tr class="dvRow">
            <td class="dvHeader" style="width: 150px">
                <asp:Label ID="lblLugar" runat="server" Text="Lugar" SkinID="lblBlanco"></asp:Label>
            </td>
            <td style="text-align: left">
                <ruv:Geografia ID="LugarHecho" runat="server" OnCambioGeografia="LugarHecho_Cambio" />
            </td>
        </tr>
        <tr class="dvRow">
            <td rowspan="2" class="dvHeader" style="width: 150px">
                <asp:Label ID="lblPersonas" runat="server" Text="Personas" SkinID="lblBlanco"></asp:Label>
            </td>
            <td>
                <table>
                    <tr>
                        <td>
                            <ruv:DropDownList ID="ddlPersonas" runat="server" DataValueField="Id" DataTextField="Persona"
                                OnSelectIndexChange="ddlPersonas_SelectIndexChange" AutoPostBack="true" />
                            <asp:ImageButton ID="tbnAgregar" runat="server" SkinID="imgAgregar" OnClick="tbnAgregar_Click"
                                ToolTip="Agregar" CausesValidation="false" />
                            <br />
                        </td>
                    </tr>
                    <tr id="trDatosVictima" runat="server" visible="false">
                        <td>
                            <div>
                                <asp:CheckBox ID="chkVictima1" runat="server" Text="Victima 1 del hecho" />
                            </div>
                            <div id="dvEstadoEnHecho" runat="server" visible="false">
                                <ruv:CheckBoxList ID="chkEstadoHecho" runat="server" RepeatColumns="2" />
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            <ruv:ListBox ID="lbPersonasAnexo" runat="server" Width="300px" />
                        </td>
                        <td valign="top" style="width: 20px">
                            <asp:ImageButton ID="btnRemover" runat="server" SkinID="imgQuitar" OnClick="btnRemover_Click"
                                ToolTip="Quitar" CausesValidation="false" />
                            <asp:ImageButton ID="btnEditarhv" runat="server" SkinID="imgEditar" OnClick="btnEditar_Click"
                                ToolTip="Editar" CausesValidation="false" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div>
        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" CausesValidation="false" />
        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CausesValidation="false" />
    </div>
</asp:Panel>
<asp:LinkButton ID="lnkNuevoHecho" runat="server"></asp:LinkButton>
<ruv:ModalPopUp ID="mpopMensajes" runat="server" MostrarBotones="true" VisibleBotonCancelar="false"
    DropShadow="true" MostrarImagen="false" filatextBox="false" BehaviorID="mpopMensajesBehaviorNH" />
   
<ajax:ModalPopupExtender ID="mpopUpNHecho" runat="server" SkinID="PopUp" TargetControlID="lnkNuevoHecho"
    DropShadow="true" BehaviorID="mpopUpNHechoBehavior" PopupControlID="pnlNuevoHecho"
    CancelControlID="btnCancelar" OnCancelScript="CerrarVentanaHechos()">
</ajax:ModalPopupExtender>
<ruv:ModalPopUp ID="mpopUpNuevoHecho" runat="server" Mensaje="¿Esta seguro de agregar el anexo con esta información, recuerde que una vez ingresado no podra quitarlo?"
    MostrarBotones="true" VisibleBotonCancelar="true" OnOk="mpopUpNuevoHecho_Ok" OnCancel="mpopUpNuevoHecho_Cancel" OnOkScript="GuardandoHechos()"
    DropShadow="true" MostrarImagen="false" filatextBox="false" BehaviorID="mpopUpNuevoHecho_BehaviorNH" />

