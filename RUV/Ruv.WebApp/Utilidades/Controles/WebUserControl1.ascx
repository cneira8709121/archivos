<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControl1.ascx.cs" 
    Inherits="Ruv.WebApp.Utilidades.Controles.WebUserControl1" %>
<asp:TextBox ID="txt" runat="server" OnTextChanged="txt_TextChanged" ></asp:TextBox>
<asp:RequiredFieldValidator ID="rv_txt" runat="server" ErrorMessage="El Campo es Requerido" 
    ControlToValidate="txt">*</asp:RequiredFieldValidator>
<ajax:ValidatorCalloutExtender ID="vce_rv_txt" runat="server" Enabled="True" TargetControlID="rv_txt">
</ajax:ValidatorCalloutExtender>
<asp:RegularExpressionValidator ID="rev_txt" runat="server" ErrorMessage="No Cumple con el formato" ControlToValidate="txt" Enabled="false">*</asp:RegularExpressionValidator>
<ajax:ValidatorCalloutExtender ID="vc_rev_txt" runat="server" Enabled="True" TargetControlID="rev_txt">
</ajax:ValidatorCalloutExtender>
<ajax:FilteredTextBoxExtender ID="ft_txt" runat="server" FilterType="Numbers" TargetControlID="txt" Enabled="false">
</ajax:FilteredTextBoxExtender>