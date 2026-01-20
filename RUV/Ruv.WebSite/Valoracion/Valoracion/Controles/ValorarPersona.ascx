<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ValorarPersona.ascx.cs" 
    Inherits="Valoracion_Valoracion_Controles_ValorarPersona" %>
<%--Inicio - Detalle Personas--%>
<asp:Panel ID="pnlModalPop" runat="server" ClientIDMode="Static" SkinID="PanelmodalPopup"
    Width="1000px">
    <asp:Panel ID="divDetallePer" runat="server">
        <table width="100%">
            <tr>
                <td valign="middle" align="center">
                    <div id="dvMensajeEstado" class="dvMensaje">
                        <asp:Label ID="lblMensajeEstado" runat="server" Text="Estado" SkinID="lblSubTitulo"
                            ClientIDMode="Static"></asp:Label>
                    </div>
                </td>
                <td style="text-align: right">
                    <asp:ImageButton ID="imgCerrar" runat="server" SkinID="imgCerrar" 
                        onclick="imgCerrar_Click" />
                </td>
            </tr>
            <tr>
                <td align="left" colspan="2">
                    <asp:DetailsView ID="dvPersonaDetalle" runat="server" Width="100%" AutoGenerateRows="false">
                        <Fields>
                            <asp:BoundField DataField="Id" HeaderText="Id" Visible="false" />
                            <asp:BoundField DataField="Persona" HeaderText="Persona" />
                            <asp:TemplateField HeaderText="Víctima">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkVictima" runat="server" Checked='<%# Eval("Victima") %>' onclick="CambioVictima()"
                                        AutoPostBack="false" ClientIDMode="Static" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Afectado">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkAfectado" runat="server" Checked='<%# Eval("Afectado") %>' ClientIDMode="Static"
                                        onclick="CambioAfectado()" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Afectaciones Detectadas">
                                <ItemTemplate>
                                <div id="dvAfectacion">
                                    <ruv:CheckBoxList ID="chkLAfectaciones" runat="server" EsRequerido="false" Valor="2155" IdLista="chkLAfectaciones"
                                        Source="Parametros" Enabled="false" RepeatColumns="1" Seleccionados='<%# Eval("AfectacionesDetectadas") %>' />
                                        </div>
                                    <asp:LinkButton ID="hlAfectaciones" runat="server" Text="Ver Afectaciones..." ClientIDMode="Static" SkinID="lbMenuAzul" OnClientClick="ClickVerAfectacion(); return false;"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Estado">
                                <ItemTemplate>
                                    <ruv:DropDownList ID="ddlEstado" runat="server" EsRequerido="false" Source="EstadosValoracion"
                                        OnChangeScript="CambioEstado()" IdCombo="ddlEstado" AutoPostBack="false" SelectedValue='<%# Eval("EstadoId") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Principio/Causal">
                                <ItemTemplate>
                                    <ruv:CheckBoxList ID="chkLPrincipios" IdLista="chkLPrincipios" runat="server" EsRequerido="false"
                                        RepeatColumns="1" ClientIDMode="Static" Source="PrincipioValoracion" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Observación Estado">
                                <ItemTemplate>
                                    <ruv:DropDownList ID="ddlObservacionEst" runat="server" EsRequerido="false" Source="ObservacionesValoracion"
                                        IdCombo="ddlObservacion" AutoPostBack="false" SelectedValue='<%# Eval("ObservacionId") %>'/>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Observación" Visible="false">
                                <ItemTemplate>
                                    <ruv:TextBox ID="txtObservacionValidacion" ClientIDMode="Static" runat="server" TextMode="MultiLine"
                                        EsRequerido="false" Text='<%# Eval("Observacion") %>' Width="90%" Height="100px" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Fields>
                    </asp:DetailsView>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <div>
        <asp:Button ID="brnGuardar" runat="server" Text="Guardar" CausesValidation="false"
            OnClick="brnGuardar_Click" />
    </div>
</asp:Panel>
<asp:LinkButton ID="lnkPopUpDet" runat="server"></asp:LinkButton>
<ajax:ModalPopupExtender ID="DetailMPopUp" runat="server" SkinID="PopUp" TargetControlID="lnkPopUpDet"
    DropShadow="true" BehaviorID="DetailModalPopUpBehavior" PopupControlID="pnlModalPop"
    >
</ajax:ModalPopupExtender>
<ruv:ModalPopUp ID="mpopReplicarTodosMasivo" runat="server" MostrarBotones="true"
    TextoCancelar="No" TextoOk="Si" VisibleBotonCancelar="true" MostrarImagen="false"
    filatextBox="false" Mensaje="Esta declaración es un masivo. ¿Desea que la información de la valoración de esta persona 
                aplique para todas las personas de la declaración?" DropShadow="true"
    BehaviorID="mpopReplicarTodosMasivoBehavior" OnOk="mpopReplicarTodosMasivo_Ok" />
<%--Fin - Detalle Personas--%>
