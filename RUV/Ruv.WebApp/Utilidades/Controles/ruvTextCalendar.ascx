<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="Utilidades_Controles_dpsTextCalendar" Codebehind="ruvTextCalendar.ascx.cs" %>
<asp:TextBox ID="txt" runat="server" Enabled="true" ValidationGroup="vgFiltro" ></asp:TextBox>
<asp:Image ID="imgCalendar" runat="server" SkinID="imgCalendar" CausesValidation="false" />
<ajax:CalendarExtender ID="ce_txtFechaNacimiento" runat="server" TargetControlID="txt"
    PopupButtonID="imgCalendar" Format='<%$ AppSettings:Fecha %>'>
</ajax:CalendarExtender>
<asp:RequiredFieldValidator ID="rv_txt" runat="server" ErrorMessage="Seleccione Fecha" ValidationGroup="vgFiltro"
    ControlToValidate="txt" Display="Dynamic">*</asp:RequiredFieldValidator>
<%--<ajax:MaskedEditExtender ID="me_txt" runat="server" TargetControlID="txt" Mask="99/99/9999"
    MaskType="Date" EnableViewState="true">
</ajax:MaskedEditExtender>--%>
<ajax:ValidatorCalloutExtender ID="vc_rv_txt" runat="server" Enabled="True" TargetControlID="rv_txt">
</ajax:ValidatorCalloutExtender>
<asp:CustomValidator ID="cv_txt" runat="server" ErrorMessage="Fecha No valida" ControlToValidate="txt" OnServerValidate="cv_txt_ServerValidate">*</asp:CustomValidator>
<ajax:ValidatorCalloutExtender ID="vce_rangev_txt" runat="server" Enabled="True" TargetControlID="cv_txt">
</ajax:ValidatorCalloutExtender>
