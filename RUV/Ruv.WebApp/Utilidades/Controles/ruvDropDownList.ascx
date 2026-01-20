<%@ Control Language="C#" AutoEventWireup="true" Inherits="Utilidades_Controles_ruvDropDownList" Codebehind="ruvDropDownList.ascx.cs" %>
<asp:DropDownList ID="ddl" runat="server" AppendDataBoundItems="True" AutoPostBack="false"
    onselectedindexchanged="ddl_SelectedIndexChanged" >
</asp:DropDownList>
<asp:CompareValidator ID="cv_ddl" runat="server"
    ControlToValidate="ddl" ValueToCompare="0" Type="Integer" Operator="NotEqual">*</asp:CompareValidator>
<ajax:ValidatorCalloutExtender ID="vce_cv_ddl" runat="server" Enabled="True" TargetControlID="cv_ddl">
</ajax:ValidatorCalloutExtender>