<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ClavesUs.aspx.cs" Inherits="ClavesUs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:TextBox ID="txtClave" runat="server"></asp:TextBox>
    <asp:Button ID="Button2" runat="server" Text="Loggin Interno" 
        onclick="Button2_Click" />
    <asp:Panel ID="pnl" runat="server" Visible="false">
        <div>
            clave cifrada:
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        </div>
        <div>
            clave descifrada:<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        </div>
        <asp:Button ID="Button1" runat="server" Text="Descifrar" OnClick="Button1_Click" />
        <div>
            Valoracion Id:<asp:TextBox ID="txtValoracionId" runat="server"></asp:TextBox>
            Motivacion:<asp:TextBox ID="txtMotivacion" runat="server" TextMode="MultiLine"></asp:TextBox>
            <asp:Button ID="Guardar" runat="server" Text="Button" OnClick="Guardar_Click" />
        </div>
    </asp:Panel>
    </form>
</body>
</html>
