<%@ Page Title="Preparación de Notificaciones" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="PrepararNotificaciones.aspx.cs" Inherits="Notificaciones_PrepararNotificaciones" %>
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
        <asp:Label ID="lblTitulo" runat="server" Text="Notificaciones" SkinID="lblSubTitulo" />
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
                <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar Registros" OnClick="btnFiltrar_Click" />
                <asp:Button ID="btnRestaurarFiltros" runat="server" Text="Restaurar" OnClick="btnRestaurarFiltros_Click" />
                <%--<asp:Button ID="btnGenerarPaqueteFiltro" runat="server" Text="Paquete desde Filtro" OnClick="btnGenerarPaqueteFiltro_Click" />--%>
            </div>
        </div>
        
        <div class="panelControl">
            <a id="expandCollapseFilters" class="actionLink" data-state="open">Ocultar Filtros</a>
        </div>
    </div>
   <asp:GridView ID="grdNotificaciones" runat="server" AutoGenerateColumns="False" DataKeyNames="NID,NID_ESTADONOTIFICACION,Aprobado" SkinID="heavyInfoGridView" OnRowDataBound="grdNotificaciones_RowDataBound">
        <Columns>
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
            <asp:BoundField DataField="CESTADONOTIFICACION" HeaderText="Estado" SortExpression="ESTADONOTIFICACION" Visible="false" />
            <asp:BoundField DataField="CUBICACIONNOTIFICACION" HeaderText="Punto Notificación" SortExpression="UBICACIONNOTIFICACION" />
            <asp:TemplateField HeaderText="Citación">
                <ItemTemplate>
                    <%# ((Eval("CNOMBREPAIS") as string).ToLowerInvariant().Trim() == "colombia" ? string.Empty : Eval("CNOMBREPAIS") + " - ") + Eval("CNOMBREDEPARTAMENTO") + " - " + Eval("CNOMBREMUNICIPIO") %>
                    <span class="altInfo"><%# Eval("CDIRECCIONNOTIFICACION") %></span>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="NID_USUARIO" HeaderText="NID_USUARIO" SortExpression="ID_USUARIO" Visible="False" />
            <asp:BoundField DataField="NID_PAQUETENOTIFICACION" HeaderText="NID_PAQUETENOTIFICACION" SortExpression="ID_PAQUETENOTIFICACION" Visible="False" />
            <asp:TemplateField HeaderText="Acciones">
                <ItemTemplate>
                  <asp:ImageButton ID="imgBtnEditarCorrespondencia" runat="server" ImageUrl="~/App_Themes/RUVTheme/Imagenes/paper.png" CommandName="Correspondencia" CommandArgument='<%# Eval("NID")%>' CausesValidation="false" ToolTip="Modificar dirección de correspondencia" />
                  <asp:ImageButton ID="imgBtnEditarPuntoNotificacion" runat="server" ImageUrl="~/App_Themes/RUVTheme/Imagenes/house.png" CommandName="PuntoAtencion" CommandArgument='<%# Eval("NID")%>' CausesValidation="false" ToolTip="Modificar punto de atención" />
                  <asp:ImageButton ID="ImgDetalle" runat="server" SkinID="imgBuscar" PostBackUrl='<%# "NotificacionDetalle.aspx?id=" + Eval("NID")%>' CausesValidation="false" ToolTip="Ver detalle notificación" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <kaz:CustomPager ID="GridPager" runat="server" CurrentPageNumber="1" CurrentPageSize="20" OnPageChanged="GridPager_PageChanged" />
    
    <asp:Button ID="btnShowPopupExcel" runat="server" Style="display: none" />
    <ajax:ModalPopupExtender ID="mdlPopupExcel" BehaviorID="mdlPopupExcel" runat="server" TargetControlID="btnShowPopupExcel" PopupControlID="pnlPopupExcel" CancelControlID="btnCancelarExcel" BackgroundCssClass="modalBackground" />
    <asp:Panel ID="pnlPopupExcel" runat="server" Width="500px" SkinID="PanelmodalPopup" GroupingText="Paquete Generado"
        HorizontalAlign="Left" Style="display: none">
        <div align="center">
            <asp:Button ID="btnGuardarExcel" runat="server" Text="Descargar Excel" OnClientClick="HidepopupExcel('mdlPopupExcel');" onclick="btnGuardarExcel_Click" />
            <asp:Button ID="btnCancelarExcel" runat="server" Text="Cancelar" Style="display: none"/>
        </div>
    </asp:Panel>

    <ruv:EdicionDireccionCorrespondencia ID="edicionDireccionCorrespondencia" runat="server" />
    <ruv:EdicionPuntoNotificacion ID="edicionPuntoNotificacion" runat="server" />
    <ruv:ModalPopUp ID="mdPopUpNotificacion" runat="server" Visible="false" />
    <script type="text/javascript" src='<%= Page.ResolveClientUrl("~/JScripts/Notificaciones/ruv.notificaciones-pendienteenvio.js") %>'></script>
</asp:Content>