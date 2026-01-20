<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="_Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <link rel="shortcut icon" href="/Utilidades/Imagenes/favicon.ico" />
    <script src="JScripts/jquery-1.8.2.js" type="text/javascript"></script>
    <script src="JScripts/jquery.center.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#mainDiv').center(true);
            $(window).bind('resize', function () {
                $('#mainDiv').center({ transition: 300 });
            });
        });
    </script>
</head>
<body class="bFondo">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table width="100%">
        <tr>
            <td>
                <div>
                    <asp:Image ID="ImgLogo" runat="server" SkinID="imgLogo" CausesValidation="false" Enabled="false" />
                </div>
            </td>
            <td style="text-align: right; vertical-align: top">
                <div>
                    <asp:Label ID="lblTituloNombre" runat="server" Text="Registro Único De Víctimas" SkinID="lblTituloApl"></asp:Label>
                </div>
                <div>
                    <asp:Label ID="lblSubtituloNombre" runat="server" Text="Web" SkinID="lblSubtituloApl"></asp:Label>
                </div>
            </td>
        </tr>
    </table>
    <div id="mainDiv">
        <asp:Label ID="loginMessage" runat="server" Visible="false" />
        <div id="loginDiv" class="dvLogin">
        <asp:MultiView ID="mvLogin" runat="server" ActiveViewIndex="0">
            <asp:View ID="vLogin" runat="server">
                <table cellpadding="0" class="tblLogin">
                    <tr>
                        <td align="center" colspan="2" class="tdTitluloLogin">
                            <asp:Label ID="lblTitulo" runat="server" SkinID="lblBlanco" Text="INICIAR SESION"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 120px">
                            <asp:Label ID="lblUserName" runat="server" AssociatedControlID="txtUserName" Text="Usuario:"></asp:Label>
                        </td>
                        <td style="width: 280px">
                            <ruv:TextBox ID="txtUserName" runat="server" EsRequerido="true" MensajeRequerido="El Nombre de usuario es requerido" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="txtPassword" Text="Contraseña:"></asp:Label>
                        </td>
                        <td>
                            <ruv:TextBox ID="txtPassword" runat="server" EsRequerido="true" MensajeRequerido="La contraseña es requerida"
                                TextMode="Password" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <asp:Label ID="lblError" runat="server" SkinID="lblError"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <asp:Button ID="LoginButton" runat="server" Text="Aceptar" OnClick="LoginButton_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:LinkButton ID="btnOlvidoClave" runat="server" CausesValidation="false" SkinID="lbMenuAzul"
                                Visible="false" OnClick="btnOlvidoClave_Click">Olvido Contraseña</asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </asp:View>
            <asp:View ID="vRecordar" runat="server">
                <table cellpadding="0" class="tblLogin">
                    <tr>
                        <td align="center" colspan="2" class="tdTitluloLogin">
                            <asp:Label ID="lblTituloRecordar" runat="server" SkinID="lblBlanco" Text="RECORDAR CONTRASEÑA"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 120px">
                            <asp:Label ID="lblUsernameRecordar" runat="server" AssociatedControlID="txtUserNameRecordar"
                                Text="Usuario:"></asp:Label>
                        </td>
                        <td style="width: 280px">
                            <ruv:TextBox ID="txtUserNameRecordar" runat="server" EsRequerido="true" MensajeRequerido="El Nombre de usuario es requerido" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <asp:Label ID="lblErrorRecordar" runat="server" SkinID="lblError"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <asp:Button ID="btnRecordar" runat="server" Text="Aceptar" OnClick="btnRecordar_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:LinkButton ID="btnRegresar" runat="server" CausesValidation="false" SkinID="lbMenuAzul"
                                OnClick="btnRegresar_Click">Regresar Login</asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </asp:View>
        </asp:MultiView>
        </div>
    </div>
    </form>
</body>
</html>
