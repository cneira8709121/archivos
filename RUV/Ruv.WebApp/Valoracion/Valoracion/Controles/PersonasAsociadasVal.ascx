<%@ Control Language="C#" AutoEventWireup="true" Inherits="Valoracion_Valoracion_Controles_PersonasAsociadasVal" Codebehind="PersonasAsociadasVal.ascx.cs" %>
<asp:Panel ID="pnlConsultaPersonasVal" runat="server" SkinID="PanelmodalPopup" ClientIDMode="Static">
    <div style="width: 740px">
        <asp:Panel ID="pnlTituloConsultaPersonasVal" runat="server" SkinID="pnlTitulo">
            <asp:Label ID="lblTituloConsultaPersonasVal" runat="server" Text="Personas Asociadas a la Declaración"></asp:Label>
        </asp:Panel>
        <asp:HiddenField ID="IdDeclaracionHidden" runat="server" />
        <asp:GridView ID="GridPersonaAsociada" runat="server" AutoGenerateColumns="False" DataSourceID="odsPersonasAsociadas" SkinID="GridViewConPaginacion" Width="100%">
            <Columns>                                    
                <asp:BoundField DataField="cNombreDeclarante" HeaderText="Nombres y Apellidos" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="cTipoDocumento" HeaderText="Tipo Documento" />
                <asp:BoundField DataField="cNumeroDocumento" HeaderText="Documento" />
                <asp:BoundField DataField="cRelacionDeclarante" HeaderText="Relacion Familiar" />
            </Columns>
            <EmptyDataTemplate>
                La declaración no contiene actualmente personas relacionadas
            </EmptyDataTemplate>
        </asp:GridView>
        <asp:ObjectDataSource ID="odsPersonasAsociadas" runat="server" SelectCountMethod="CountAsociadosPersonaDeclaracion" SelectMethod="CargaAsociadosPersonaDeclaracion" EnablePaging="false" TypeName="Ruv.WebApp.DataSources.DataSourcePersonasAsociadasDeclaracion" OnObjectCreated="odsPersonasAsociadas_ObjectCreated"></asp:ObjectDataSource>
        <div class="ActionsBox">
            <asp:Button ID="btnAgregarPersona" ClientIDMode="Static" runat="server" Text="Adicionar Persona" CausesValidation="false" onclick="btnAgregarPersona_Click" />
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CausesValidation="false" />
        </div>
    </div>
</asp:Panel>
<asp:LinkButton ID="lnkPersonasAsociadas" runat="server"></asp:LinkButton>
<ajax:ModalPopupExtender ID="mpopUpPersonasAsociadas" runat="server" SkinID="PopUp" TargetControlID="lnkPersonasAsociadas"
    DropShadow="true" BehaviorID="mpopUpPersonasAsociadasBehavior" PopupControlID="pnlConsultaPersonasVal"
    CancelControlID="btnCancelar">
</ajax:ModalPopupExtender>
