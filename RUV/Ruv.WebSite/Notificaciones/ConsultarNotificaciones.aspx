<%@ Page Title="Notificaciones Pendientes de Envío" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="ConsultarNotificaciones.aspx.cs" Inherits="Notificaciones_ConsultarNotificaciones" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<%@ Register Src="~/Utilidades/Controles/GridCustomPager.ascx" TagPrefix="kaz" TagName="CustomPager" %>
<asp:Content ID="Head" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src='<%= Page.ResolveClientUrl("~/JScripts/search.js") %>'></script>
    <script language="javascript" type="text/javascript">
        function Hidepopup(strPopUpName) {
            ValidatePage();
            if (Page_IsValid) {
                $find(strPopUpName).hide();
            }
            else {
                return false;
            }
        }
        function HidepopupExcel(strPopUpName) {
            $find(strPopUpName).hide();
        }
        function Showpopup(strPopUpName) {
            $find(strPopUpName).show();
        }
        function ValidatePage() {

            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate();
            }
        }
    </script>    
</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="pblseparador" runat="server" SkinID="pnlTitulo">
        <asp:Label ID="lblTitulo" runat="server" Text="Notificaciones Pendientes de Envío" SkinID="lblSubTitulo" />
    </asp:Panel>
    <div class="actionFilterBox">
        <div class="content">
            <p style="width: 20%">
                <label>Declaración:</label>
                <asp:TextBox ID="filterDeclaracion" runat="server" Width="90%" />
            </p>
            <p style="width: 15%">
                <label>Tipo Documento:</label>
                <asp:DropDownList ID="filterTipoDocumento" runat="server" Width="90%" DataValueField="Id" DataTextField="Nombre" />
            </p>
            <p style="width: 20%">
                <label>Documento:</label>
                <asp:TextBox ID="filterDocumento" runat="server" Width="90%" />
            </p>
            <p style="width: 38%">
                <label>Nombre Declarante:</label>
                <asp:TextBox ID="filterNombreDeclarante" runat="server" Width="90%" />
            </p>
            <p style="width: 12%">
                <label>País Notificación:</label>
                <asp:DropDownList ID="filterPaisNotificacion" ClientIDMode="Static" runat="server" Width="95%" DataValueField="Id" DataTextField="Nombre" />
            </p>
            <p style="width: 12%">
                <label>Departamento:</label>
                <asp:DropDownList ID="filterDepartamentoNotificacion" ClientIDMode="Static" runat="server" Width="95%" DataValueField="Id" DataTextField="Nombre" />
            </p>
            <p style="width: 12%">
                <label>Municipio:</label>
                <asp:DropDownList ID="filterMunicipioNotificacion" ClientIDMode="Static" runat="server" Width="95%" DataValueField="Id" DataTextField="Nombre" />
            </p>
            <p style="width: 20%">
                <label>Entidad:</label>
                <asp:DropDownList ID="filterPuntoNotificacion" ClientIDMode="Static" runat="server" Width="95%" DataValueField="HashId" DataTextField="Nombre" />
            </p>
            <p style="width: 36%">
                <label>Dirección de Citación:</label>
                <asp:TextBox ID="filterDireccionCitacion" runat="server" Width="90%" />
            </p>
            
            <div class="actions">
                <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar Registros" OnClick="btnFiltrar_Click" data-filter="true" />
                <asp:Button ID="btnRestaurarFiltros" runat="server" Text="Restaurar" OnClick="btnRestaurarFiltros_Click" />
                <asp:Button ID="btnGenerarPaqueteFiltro" runat="server" Text="Paquete desde Filtro" OnClick="btnGenerarPaqueteFiltro_Click" />
            </div>
        </div>
        
        <div class="panelControl">
            <a id="expandCollapseFilters" class="actionLink" data-state="open">Ocultar Filtros</a>
        </div>
    </div>
    <asp:GridView ID="grdNotificaciones" runat="server" AutoGenerateColumns="False" DataKeyNames="NID,NID_ESTADONOTIFICACION,Aprobado" SkinID="heavyInfoGridView" OnRowDataBound="grdNotificaciones_RowDataBound">
        <Columns>
            <asp:TemplateField>
                <HeaderTemplate>
                    <asp:CheckBox ID="checkAllNotificaciones" runat="server" ClientIDMode="Static" AutoPostBack="false"/>
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="chkNotificacion" runat="server" data-selection="itemCheck" AutoPostBack="false"/>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="NID" HeaderText="NID" SortExpression="ID" Visible="False" />
            <asp:TemplateField HeaderText="Aprobación">
                <ItemTemplate>
                    <%# (Eval("FechaFirma") as DateTime? == null ? string.Empty : (Eval("FechaFirma") as DateTime?).Value.ToString("dd/MM/yyyy"))%>
                    <span class="altInfo noWrap"><%# (Eval("FechaFirma") as DateTime? == null ? string.Empty : (Eval("FechaFirma") as DateTime?).Value.ToString("hh:mm:ss tt"))%></span>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="CNumeroFormulario" HeaderText="Declaración" SortExpression="CNumeroFormulario" />
            <asp:BoundField DataField="CNOMBRECOMPLETO" HeaderText="Declarante" SortExpression="NOMBRECOMPLETO" />
            <asp:TemplateField HeaderText="Documento">
                <ItemTemplate>
                    <span class="altInfo noWrap"><%# Eval("CTIPODOCUMENTO")%></span>
                    <%# Eval("CNUMERODOCUMENTO")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="NID_ESTADONOTIFICACION" HeaderText="NID_ESTADONOTIFICACION" SortExpression="ID_ESTADONOTIFICACION" Visible="False" />
            <asp:BoundField DataField="Aprobado" Visible="False" />
            <asp:BoundField DataField="CESTADONOTIFICACION" HeaderText="Estado de la Notificación" SortExpression="ESTADONOTIFICACION" Visible="true" />
            <asp:BoundField DataField="CUBICACIONNOTIFICACION" HeaderText="Punto Notificación" SortExpression="UBICACIONNOTIFICACION" />
            <asp:TemplateField HeaderText="Citación">
                <ItemTemplate>
                    <%# ((Eval("CNOMBREPAIS") as string).ToLowerInvariant().Trim() == "colombia" ? string.Empty : Eval("CNOMBREPAIS") + " - ") + Eval("CNOMBREDEPARTAMENTO") + " - " + Eval("CNOMBREMUNICIPIO") %>
                    <span class="altInfo"><%# Eval("CDIRECCIONNOTIFICACION") %></span>
                </ItemTemplate>
            </asp:TemplateField>
            
            <%--<asp:TemplateField HeaderText="Aprobada">
                <ItemTemplate>
                    <%# ((bool)Eval("Aprobado") ? "Si" : "No") %>
                </ItemTemplate>
            </asp:TemplateField>--%>
            <asp:BoundField DataField="NID_USUARIO" HeaderText="NID_USUARIO" SortExpression="ID_USUARIO"
                Visible="False" />
            <asp:BoundField DataField="NID_PAQUETENOTIFICACION" HeaderText="NID_PAQUETENOTIFICACION"
                SortExpression="ID_PAQUETENOTIFICACION" Visible="False" />
            <asp:TemplateField HeaderText="Acciones">
                <ItemTemplate>
                  <asp:ImageButton ID="imgBtnEditarCorrespondencia" runat="server" ImageUrl="~/App_Themes/RUVTheme/Imagenes/paper.png" CommandName="Correspondencia" CommandArgument='<%# Eval("NID")%>' CausesValidation="false" ToolTip="Modificar dirección de correspondencia" />
                  <asp:ImageButton ID="imgBtnEditarPuntoNotificacion" runat="server" ImageUrl="~/App_Themes/RUVTheme/Imagenes/house.png" CommandName="PuntoAtencion" CommandArgument='<%# Eval("NID")%>' CausesValidation="false" ToolTip="Modificar punto de atención" />
                  <asp:ImageButton ID="ImgDetalle" runat="server" SkinID="imgBuscar" PostBackUrl='<%# "NotificacionDetalle.aspx?id=" + Eval("NID")%>'
                        CausesValidation="false" ToolTip="Ver detalle notificación" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <kaz:CustomPager ID="GridPager" runat="server" CurrentPageNumber="1" CurrentPageSize="20" OnPageChanged="GridPager_PageChanged" />
    
    <div class="ActionsBox">
        <asp:Button ID="btnGenerarPaquetes" runat="server" Text="Generar Paquetes" onclick="btnGenerarPaquetes_Click" />
        <%--<asp:FileUpload ID="fuCargarReporte" runat="server" />
        <asp:Button ID="btnCargarReporte" runat="server" Text="Cargar Archivo 4-72" onclick="btnCargarReporte_Click" />--%>
    </div>
    
 <%--     <asp:Button ID="btnShowPopup" runat="server" Style="display: none"/>
    <ajax:ModalPopupExtender ID="mdlPopup" BehaviorID="mdlPopup" runat="server" TargetControlID="btnShowPopup"
        PopupControlID="pnlPopup" CancelControlID="btnCancelar" BackgroundCssClass="modalBackground" />
    <asp:Panel ID="pnlPopup" runat="server" Width="500px" SkinID="PanelmodalPopup" GroupingText="Detalles Notificación"
        HorizontalAlign="Left" Style="display: none">
        <asp:UpdatePanel ID="updPnlDetalleNotificacion" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:HiddenField ID="HFieldIdNotificacion" runat="server" />
              <asp:Label ID="lblDireccion" runat="server" Text="Dirección" />
                <asp:TextBox ID="txtDireccion" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rv_txtDireccion" runat="server" ErrorMessage="El Campo es Requerido" ValidationGroup="GrupoNotificacion"
                    ControlToValidate="txtDireccion">*</asp:RequiredFieldValidator>
                <ajax:ValidatorCalloutExtender ID="vce_rv_txtDireccion" BehaviorID="vce_rv_txtDireccion" runat="server" Enabled="True" TargetControlID="rv_txtDireccion">
                </ajax:ValidatorCalloutExtender>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div align="right">
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClientClick="Hidepopup('mdlPopup');" ValidationGroup="GrupoNotificacion"
                OnClick="btnGuardar_Click" />
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" />
        </div>
    </asp:Panel>--%>
    
    <asp:Button ID="btnShowPopupExcel" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="mdlPopupExcel" BehaviorID="mdlPopupExcel" runat="server" TargetControlID="btnShowPopupExcel"
        PopupControlID="pnlPopupExcel" CancelControlID="btnCancelarExcel" BackgroundCssClass="modalBackground" />
    <asp:Panel ID="pnlPopupExcel" runat="server" Width="500px" SkinID="PanelmodalPopup" GroupingText="Paquete Generado"
        HorizontalAlign="Left" Style="display: none">
<%--        <asp:UpdatePanel ID="updPnlExcel" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:HiddenField ID="HFieldExcel" runat="server" />

            </ContentTemplate>
        </asp:UpdatePanel>--%>
        <div align="center">
            <div><asp:Label ID="lblTextoPaquete" runat="server" Text="" /></div>
            <div><asp:HyperLink ID="hlkDetallePaquete" runat="server">Ver detalle</asp:HyperLink></div>
            <div><asp:Button ID="btnGuardarExcel" runat="server" Text="Descargar Excel" 
                OnClientClick="HidepopupExcel('mdlPopupExcel');" onclick="btnGuardarExcel_Click" />
            <asp:Button ID="btnCancelarExcel" runat="server" Text="Cancelar" Style="display: none"/></div>
        </div>
    </asp:Panel>

    <ruv:EdicionDireccionCorrespondencia ID="edicionDireccionCorrespondencia" runat="server" />
    <ruv:EdicionPuntoNotificacion ID="edicionPuntoNotificacion" runat="server" />
    <ruv:ModalPopUp ID="mdPopUpNotificacion" runat="server" Visible="false" />
    <script type="text/javascript" src='<%= Page.ResolveClientUrl("~/JScripts/Notificaciones/ruv.notificaciones-pendienteenvio.js") %>'></script>
</asp:Content>
