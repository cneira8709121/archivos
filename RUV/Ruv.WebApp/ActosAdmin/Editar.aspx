<%@ Page Title="Modificar" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" Inherits="ActosAdmin_Editar" Codebehind="Editar.aspx.cs" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script language="javascript" type="text/javascript">
        function Mostrar() {
            var imagen = document.getElementById('dvImagen');
            imagen.style.display = 'block';
            setTimeout("OcultarAdvertencia()", 10000);
        }
        function OcultarAdvertencia() {
            var imagen = document.getElementById('dvImagen');
            var texto = document.getElementById('lblAdvertenciaNoExiste');
            if (imagen != null) {
                imagen.style.display = 'none';
                texto.innerHTML = "";
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Label ID="lblError" runat="server" SkinID="lblError"></asp:Label>
            <div style="text-align: justify">
                <br />
                <asp:Panel ID="pblseparador" runat="server" SkinID="pnlTitulo">
                    <asp:Label ID="lblTitulo" runat="server" Text="GENERAR CONSECUTIVO ACTO ADMINISTRATIVO"
                        SkinID="lblSubTitulo" />
                </asp:Panel>
                <br />
                <center>
                    <table width="50%" border="1" style="border-collapse: collapse; text-align: left">
                        <tr class="dvRow">
                            <td colspan="2">
                                <asp:Label ID="lblFecha" runat="server" Text="Fecha de Solicitud: {0}" SkinID="lblSubTitulo"></asp:Label>
                            </td>
                        </tr>
                        <tr class="dvRow">
                            <td class="dvHeader" style="width: 200px">
                                <asp:Label ID="lblDocumento" runat="server" Text="Documento" SkinID="lblBlanco"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="txtDocumento" runat="server" ></asp:Label>
                            </td>
                        </tr>
                        <tr class="dvRow">
                            <td class="dvHeader" style="width: 200px">
                                <asp:Label ID="lblNumeroInterno" runat="server" Text="Numero Interno" SkinID="lblBlanco" ></asp:Label>
                            </td>
                            <td>
                                <ruv:TextBox ID="txtNumeroInterno" runat="server" EsRequerido="false" />
                            </td>
                        </tr>
                        <tr class="dvRow">
                            <td class="dvHeader" style="width: 200px">
                                <asp:Label ID="lblNroFormulario" runat="server" Text="Numero Formulario" SkinID="lblBlanco"></asp:Label>
                            </td>
                            <td>
                                <table style="border-collapse: collapse">
                                    <tr>
                                        <td>
                                            <ruv:TextBox ID="txtNroFormulario" runat="server" Script="Mostrar()" AutoPostBack="true" EsRequerido="false"
                                                OnTextChanged="txtNroFormulario_TextChanged" />
                                        </td>
                                        <td>
                                            <div id="dvImagen" style="display: none">
                                                <asp:Image ID="imgConsultando" runat="server" SkinID="imgCargandoG" ClientIDMode="Static" />
                                            </div>
                                            <asp:Label ID="lblAdvertenciaNoExiste" runat="server" SkinID="lblError" ClientIDMode="Static"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr class="dvRow">
                            <td class="dvHeader" style="width: 200px">
                                <asp:Label ID="lblDescripción" runat="server" Text="Descripción" SkinID="lblBlanco"></asp:Label>
                            </td>
                            <td>
                                <ruv:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" Width="400px" />
                            </td>
                        </tr>
                        <tr class="dvRow">
                            <td class="dvHeader" style="width: 200px">
                                <asp:Label ID="lblDirigido" runat="server" Text="Dirigido a" SkinID="lblBlanco"></asp:Label>
                            </td>
                            <td>
                                <ruv:TextBox ID="txtDirigido" runat="server" Width="300px" EsRequerido="true" MensajeRequerido="Ingrese para quien va dirigido" />
                            </td>
                        </tr>
                    </table>
                    <ruv:ModalPopUp ID="mpopGuardar" runat="server" MostrarBotones="true" DropShadow="true"
                        MostrarImagen="false" filatextBox="false" VisibleBotonCancelar="false" OnOk="mpopGuardar_Ok" BehaviorID="mpopGuardarBehavior" />
                    <ruv:ModalPopUp ID="mpupError" runat="server" MostrarBotones="true" VisibleBotonCancelar="false"
                        DropShadow="true" Mensaje="Ourrio un error al guardar, intente de nuevo de persistir el error comuniquese con el administrador"
                        MostrarImagen="false" filatextBox="true" BehaviorID="mpupErrorBehavior" />
                </center>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
