<%@ Page Language="C#" AutoEventWireup="true" Inherits="_Login" Codebehind="Login.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" class="no-js" lang="">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="x-ua-compatible" content="ie=edge">
    <title>Login</title>
    <link rel="shortcut icon" href="/Utilidades/Imagenes/favicon.ico" />
    <script src="JScripts/jquery-1.8.2.js" type="text/javascript"></script>
    <script src="JScripts/jquery.center.js" type="text/javascript"></script>
    <link rel="stylesheet" href="NewStyle/css/normalize.css">
    <link rel="stylesheet" href="NewStyle/css/main.css">
    <script src="NewStyle/js/vendor/modernizr-2.8.3.min.js"></script>
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
    <header>
       <img src="NewStyle/img/logo-unidad-herramienta.jpg" alt="Logo Unidad para las Víctimas"/>
    </header>
    <form id="form1" runat="server">
    <section id="content">
            <div class="log-box">
                <div class="header-box">
                    <h2>Inicio de sesión</h2>
                </div>
                <div class="input-box">
                    <div class="input-group">
                        <span class="input-group-label icon i-user"></span>
                        <input id="txtUserName" runat="server" class="input-group-field" type="text" placeholder="Nombre de usuario">
                    </div>

                    <div class="input-group">
                        <span class="input-group-label icon i-key"></span>
                        <input id="txtPassword" runat="server" class="input-group-field" type="password" placeholder="Contraseña">
                    </div>

                    <asp:Button ID="LoginButton" runat="server" Text="Aceptar" OnClick="LoginButton_Click" />
                    <asp:Label ID="lbMsg" runat="server" Text="" ForeColor="GrayText"></asp:Label>
                </div>
            </div>


            <div class="social text-center">
                <p>Redes sociales</p>
                <ul>
                    <li><a class="ig" href="https://www.instagram.com/unidadvictimas/" target="_blank">Instagram</a></li>
                    <li><a class="fb" href="https://www.facebook.com/unidadvictimas" target="_blank">Facebook</a></li>
                    <li><a class="tw" href="https://twitter.com/unidadvictimas" target="_blank">Twitter</a></li>
                    <li><a class="yt" href="https://www.youtube.com/user/UPARIV" target="_blank">Youtube</a></li>
                    <li><a class="fl" href="https://www.flickr.com/photos/uariv/" target="_blank">Flickr</a></li>

                </ul>
            </div>

            <div class="contact">
                <ul>
                    <li class="icon i-tel">Linea gratuita 018000-911119</li>
                    <li class="icon i-mail">correo@unidadvictimas.gov.co</li>
                    <li class="icon i-world">www.unidadvictimas.gov.co</li>
                </ul>
            </div>
        </section>
    <%--<asp:ScriptManager ID="ScriptManager1" runat="server">
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
                            <asp:Label ID="lblUserName" runat="server" AssociatedControlID="txtUserName" Text="Usuario SIRAV:"></asp:Label>
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
                        <td align="center" colspan="2" >
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
    </div>--%>
    </form>
     <script src="https://code.jquery.com/jquery-1.12.0.min.js"></script>
        <script>window.jQuery || document.write('<script src="js/vendor/jquery-1.12.0.min.js"><\/script>')</script>
        <script src="NewStylejs/plugins.js"></script>
        <script src="NewStyle/js/main.js"></script>

        <!-- Google Analytics: change UA-XXXXX-X to be your site's ID. -->
        <script>
            (function(b,o,i,l,e,r){b.GoogleAnalyticsObject=l;b[l]||(b[l]=
            function(){(b[l].q=b[l].q||[]).push(arguments)});b[l].l=+new Date;
            e=o.createElement(i);r=o.getElementsByTagName(i)[0];
            e.src='https://www.google-analytics.com/analytics.js';
            r.parentNode.insertBefore(e,r)}(window,document,'script','ga'));
            ga('create','UA-XXXXX-X','auto');ga('send','pageview');
        </script>
</body>
</html>
