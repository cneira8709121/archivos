<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PuntosNotificacion.ascx.cs" 
Inherits="Valoracion_Valoracion_Controles_PuntosNotificacion" %>
<script type="text/javascript" src="../../../JScripts/Valoracion/JPuntosNotificacion.js"></script>
<%--<script type="text/javascript" src="../../../JScripts/JNotificacion.js"></script>--%>

<asp:Panel ID="pnlPuntosNotificacion" runat="server" SkinID="PanelmodalPopup" ClientIDMode="Static">
    <div style="width:500px">
        <asp:Panel ID="pnlTituloPuntosNotificacion" runat="server" SkinID="pnlTitulo">
            <asp:Label ID="lblTituloPuntosNotificacion" runat="server" Text="PUNTOS DE NOTIFICACIÓN"></asp:Label>
        </asp:Panel>
        <div id="dvPuntosNotificacion">
            <div>
                <asp:Label ID="lblPais" Text="Pais" runat="server"></asp:Label>
            </div>
            <div>
                <ruv:DropDownList ID="ruvDdlPais" IdCombo="ddlPais" runat="server" Source="Paises"
                    AutoPostBack="false" />
            </div>
            <div>
                <asp:Label ID="lblDepartamento" Text="Departamento" runat="server"></asp:Label>
            </div>
            <div>
                <ruv:DropDownList ID="ruvDdlDepartamento" IdCombo="ddlDepartamento" runat="server"
                    AutoPostBack="false" />
            </div>
            <div>
                <asp:Label ID="lblMunicipio" Text="Municipio" runat="server"></asp:Label>
            </div>
            <div>
                <ruv:DropDownList ID="ruvDdlMunicipio" IdCombo="ddlMunicipio" runat="server"
                    AutoPostBack="false" />
            </div>
            <div>
                <asp:Label ID="lblEntidadMunicipio" Text="Entidad Municipio" runat="server"></asp:Label>
            </div>
            <div>
                <ruv:DropDownList ID="ruvDdlEntidadMunicipio" IdCombo="ddlEntidadMunicipio" runat="server" Source="EntidadesMunicipio"
                    AutoPostBack="false" />
            </div>
        </div>
        <div>
            <asp:Button ID="btnGuardar" ClientIDMode="Static" runat="server" Text="Guardar"
                CausesValidation="false" OnClientClick="return false;" />
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CausesValidation="false" />
            <div id="respuesta"></div>
        </div>
    </div>
</asp:Panel>
<asp:LinkButton ID="lnkPuntosNotificacion" runat="server"></asp:LinkButton>
<ajax:ModalPopupExtender ID="mpopUpPuntosNotificacion" runat="server" SkinID="PopUp" TargetControlID="lnkPuntosNotificacion"
    DropShadow="true" BehaviorID="mpopUpPuntosNotBehavior" PopupControlID="pnlPuntosNotificacion"
    CancelControlID="btnCancelar">
</ajax:ModalPopupExtender>