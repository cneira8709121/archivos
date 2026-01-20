<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="HechoVictimizanteC" CodeBehind="HechoVictimizante.ascx.cs" %>
<script type="text/javascript" src="../../../JScripts/Valoracion/JValoracionTierras.js"></script>
<script type="text/javascript" src="../../../JScripts/JGeografia.js"></script>
<asp:Panel ID="pnlNuevoHecho" runat="server" SkinID="PanelmodalPopup" ClientIDMode="Static">
    <center>
        <div id="dvMensajeValidacionHecho" class="dvMensaje">
            <asp:Label ID="lblMensajeValidacion" runat="server" SkinID="lblSubTitulo" ClientIDMode="Static"></asp:Label>        
        </div>
    </center>
    <asp:Label ID="lblError" runat="server" SkinID="lblError"></asp:Label>
    <asp:HiddenField ID="hdFechaDeclaracion" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdEstadoHecho" runat="server" ClientIDMode="Static" />
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
                    OnChangeScript="CambioHechoVictimizante()" IdCombo="ddlHechosVictimizantes"
                    EsRequerido="false" MensajeError="Seleccione un hecho victimizante" />
                <span id="lblAn11" runat="server" style="display: none" enableviewstate="true" clientidmode="Static">
                    <asp:RadioButton ID="rbInmueble" runat="server" GroupName="rbTipoAnexo11" Text="Inmueble" onclick="MostrarInmuebles(); checked" ClientIDMode="Static" />
                    <asp:RadioButton ID="rbMueble" runat="server" GroupName="rbTipoAnexo11" Text="Mueble" onclick="MostrarMuebles(); checked" ClientIDMode="Static" />
                </span>
                <div id="divInmuebles" style="display: none" runat="server" enableviewstate="true" clientidmode="Static">
                    <asp:CheckBox ID="chkAbandono" runat="server" Text="Abandono" GroupName="chkTipoAnexo" onclick="MuestraAbandono()" ClientIDMode="Static" />
                    <asp:CheckBox ID="chkDespojo" runat="server" Text="Despojo" GroupName="chkTipoAnexo" onclick="MuestraDespojo()" ClientIDMode="Static" />
                </div>
                <span id="lblAnexo12" style="display: none" runat="server" enableviewstate="true" clientidmode="Static">
                    <label id="lblotro" runat="server">Cual: </label>
                    <ruv:DropDownList ID="ddlHechosOtros" runat="server" Valor="2175" Source="Parametros"
                        MensajeError="Seleccione un Hecho" EsRequerido="false" ClientIDMode="Static" IdCombo="ddlHechosOtros" />
                </span>
            </td>

        </tr>
        <tr class="dvRow">
            <td class="dvHeader" style="width: 150px">
                <asp:Label ID="lblFechaHecho" runat="server" Text="Fecha:" SkinID="lblBlanco"></asp:Label>
            </td>
            <td>
                <span id="divfecha">
                    <ruv:TextCalendar ID="txtFecha" runat="server" IdTextbox="txtFecha" EsRequerido="false" Fecha='<%# Bind("Fecha") %>' MaxLength="10" MensajeError="Indique la fecha de ocurrencia del hecho" Width="75" Visible="false" />
                </span>
                <span id="spAbandono" style="display: none">
                    <asp:Label ID="lblAbandono" runat="server" Text="Abandono" />
                    <ruv:TextCalendar ID="txtfechaAbandono" IdTextbox="txtfechaAbandono" runat="server" EsRequerido="false" Fecha='<%# Bind("FechaAbandono") %>' MaxLength="10" MensajeError="Indique la fecha de ocurrencia del hecho" Width="75" />
                </span>
                <span id="spDespojo" style="display: none">
                    <asp:Label ID="lblDespojo" runat="server" Text="Despojo" />
                    <ruv:TextCalendar ID="TxtFechadespojo" IdTextbox="TxtFechadespojo" runat="server" EsRequerido="false" Fecha='<%# Bind("FechaDespojo") %>' MaxLength="10" MensajeError="Indique la fecha de ocurrencia del hecho" Width="75" />
                </span>
            </td>

        </tr>
        <tr class="dvRow">
            <td class="dvHeader" style="width: 150px">
                <asp:Label ID="lblLugar" runat="server" SkinID="lblBlanco" Text="Lugar"></asp:Label>
            </td>
            <td class="auto-style1" style="text-align: left">
                <ruv:Geografia ID="LugarHecho" runat="server" ClientIDPais="ddlPaisHecho" ClientIDDepto="ddlDptoHecho" ClientIDMunicipio="ddlMunHecho" Nivel="Departamento" />
            </td>
        </tr>
    </table>
    <asp:UpdatePanel ID="upPanel" runat="server">
        <ContentTemplate>
            <table border="1" style="border-collapse: collapse; text-align: left">
                <tr class="dvRow">
                    <td class="dvHeader" rowspan="2" style="width: 150px">
                        <asp:Label ID="lblPersonas" runat="server" SkinID="lblBlanco" Text="Personas"></asp:Label>
                    </td>
                    <td>
                        <table style="width: auto">
                            <tr>
                                <td>
                                    <ruv:DropDownList ID="ddlPersonas" runat="server" DataTextField="Persona" DataValueField="Id" IdCombo="ddlPersonas" OnChangeScript="return checkVictima();" />
                                    <asp:ImageButton ID="tbnAgregar" runat="server" CausesValidation="false" OnClick="tbnAgregar_Click" SkinID="imgAgregar" ToolTip="Agregar" OnClientClick="return ValidarPersonas();" />
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                            </tr>
                        </table>
                        <div id="dvVictima1" style="display: none">
                            <asp:CheckBox ID="chkVictima1" runat="server" Text="Victima 1 del hecho" ClientIDMode="Static" />
                        </div>
                        <div id="dvEstadoEnHecho" style ="display: none">
                            <ruv:DropDownList  ID="chkEstadodelHecho" runat="server" IdCombo="chkEstadodelHecho" OnChangeScript="return EstadoHecho();"/>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <ruv:ListBox ID="lbPersonasAnexo" runat="server" Width="300px" />
                                </td>
                                <td style="width: 20px" valign="top">
                                    <asp:ImageButton ID="btnRemover" runat="server" CausesValidation="false" OnClick="btnRemover_Click" SkinID="imgQuitar" ToolTip="Quitar" />
                                    <asp:ImageButton ID="btnEditarhv" runat="server" CausesValidation="false" OnClick="btnEditar_Click" SkinID="imgEditar" ToolTip="Editar" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>

    </asp:UpdatePanel>

    <div>
        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" CausesValidation="false" OnClientClick="return ValidarGuardar();" />
        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CausesValidation="false" autoposback="false" />
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
<ruv:ModalPopUp ID="mpopUpValidaciones" runat="server" Mensaje="Validaciones" MostrarBotones="true" VisibleBotonCancelar="false" DropShadow="true" OnOkScript="return CerrarVentanaHechos();" BehaviorID="mpopUpValidacionesBH" IDTitulo="lblTituloValidacion" />

