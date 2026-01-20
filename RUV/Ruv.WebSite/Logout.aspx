<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Logout.aspx.cs" Inherits="Logout" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Logout</title>
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
    <div>
        <asp:ImageButton ID="ImgLogo" runat="server" SkinID="imgLogo" CausesValidation="false" Enabled="false" />
    </div>
    <center>
        <div id="mainDiv">
            <asp:Label ID="lblTiempoFinalizo" runat="server" Text="Se detecto que se supero el tiempo de inactividad del sistema y se cerrara por seguridad"></asp:Label>
            <br />
            <br />
            <asp:Button ID="bntLogin" runat="server" Text="Continuar" PostBackUrl="~/Login.aspx" />
        </div>
    </center>
    </form>
</body>
</html>
