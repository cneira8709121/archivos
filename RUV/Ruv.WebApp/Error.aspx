<%@ Page Language="C#" AutoEventWireup="true" Inherits="Error"
    Title="Error" Codebehind="Error.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%">
        <tr>
            <td>
                <div>
                    <asp:Image ID="ImgLogo" runat="server" SkinID="imgLogo" CausesValidation="false"
                        Enabled="false" />
                </div>
            </td>
            <td style="text-align: right; vertical-align: top">
                <div>
                    <asp:Label ID="lblTituloNombre" runat="server" Text="Registro Único De Víctimas"
                        SkinID="lblTituloApl"></asp:Label>
                </div>
                <div>
                    <asp:Label ID="lblSubtituloNombre" runat="server" Text="Web" SkinID="lblSubtituloApl"></asp:Label>
                </div>
            </td>
        </tr>
    </table>
    <div style="text-align: center">
        <asp:Button ID="btnRegresar" runat="server" Text="Retornar a Login" PostBackUrl="~/Login.aspx" />
        <br />
        <br />
        <asp:Label ID="txtMensajeDisculpas" runat="server" Text="Lamentamos los inconvenientes, estamos trabajando para resolver los problemas presentados <br /> Por favor intente de nuevo, de persistir el problema comuniquese con el administrador informandole el siguiente mensaje" />
        <br />
    </div>
    <div style="text-align: center">
        <br />
        <asp:TextBox ID="txtStackTrack" runat="server" TextMode="MultiLine" Width="70%" Height="500px" />
    </div>
    </form>
</body>
</html>
