<%@ Page Title="Valoración: Adicionar Persona" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="AgregarPersonaValoracion.aspx.cs" Inherits="Valoracion_Valoracion_AgregarPersonaValoracion" %>
<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Head" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="pnlDetalle" runat="server" SkinID="pnlTitulo">
        <asp:Label ID="lblDetalle" runat="server" Text="Adicionar Persona a Declaración" SkinID="lblSubTitulo" />
    </asp:Panel>
    <div class="fieldform content">
        <p style="width: 46%">
            <label>Primer Nombre:</label>
            <asp:TextBox ID="txbPrimerNombre" runat="server" Enabled="true" Width="90%"></asp:TextBox>
        </p>
        <p style="width: 46%">
            <label>Segundo Nombre:</label>
            <asp:TextBox ID="txbSegundoNombre" runat="server" Enabled="true" Width="90%"></asp:TextBox>
        </p>
        <p style="width: 46%">
            <label>Primer Apellido:</label>
            <asp:TextBox ID="txbPrimerApellido" runat="server" Enabled="true" Width="90%"></asp:TextBox>
        </p>
        <p style="width: 46%">
            <label>Segundo Apellido:</label>
            <asp:TextBox ID="txbSegundoApellido" runat="server" Enabled="true" Width="90%"></asp:TextBox>
        </p>
        <p style="width: 18%">
            <label>Tipo de Documento:</label>
            <ruv:DropDownList ID="ddlTipoDocumento" runat="server" Enabled="true" Valor="21" Source="Parametros" Width="90%"></ruv:DropDownList>
        </p>
        <p style="width: 27%">
            <label>N&uacute;mero:</label>
            <asp:TextBox ID="txbNumeroDocumento" runat="server" Enabled="true" Width="83%"></asp:TextBox>
        </p>
        <p style="width: 20%">
            <label>Fecha de Nacimiento:</label>
            <ruv:TextCalendar ID="txbFechaNacimiento" runat="server" EsRequerido="false" MensajeError="Indique la fecha de ocurrencia del hecho" Enabled="true" Width="85%" />
        </p>
        <p style="width: 25%">
            <label>Relación:</label>
            <ruv:DropDownList ID="ddlRelacionFamiliar" runat="server" Enabled="true" Valor="29" Source="Parametros" Width="85%"></ruv:DropDownList>
        </p>

        <p style="width: 27%">
            <label>Direcci&oacute;n:</label>
            <asp:TextBox ID="txbDireccion" runat="server" Enabled="true" Width="95%"></asp:TextBox>
        </p>
        <p style="width: 18%">
            <label>Tel&eacute;fono:</label>
            <asp:TextBox ID="txbTelefono" runat="server" Enabled="true" Width="75%"></asp:TextBox>
        </p>
        <p style="width: 46%">
            <label>Correo Electr&oacute;nico:</label>
            <asp:TextBox ID="txbCorreoElectronico" runat="server" Enabled="true" Width="90%"></asp:TextBox>
        </p>

        <p style="width: 27%">
            <label>G&eacute;nero:</label>
            <ruv:DropDownList ID="ddlGenero" runat="server" Enabled="true" Valor="24" Source="Parametros" Width="95%"></ruv:DropDownList>
        </p>
        <p style="width: 18%">
            <label>Estado Civil:</label>
            <ruv:DropDownList ID="ddlEstadoCivil" runat="server" Enabled="true" Valor="22" Source="Parametros" Width="75%"></ruv:DropDownList>
        </p>
        <p style="width: 46%">
            <label>Régimen Especial:</label>
            <ruv:DropDownList ID="ddlRegimenEspecial" runat="server" Enabled="true" Valor="2134" Source="Parametros" Width="90%"></ruv:DropDownList>
        </p>
        
        <p style="width: 46%">
            <label>&Eacute;tnia a la que pertenece:</label>
            <ruv:DropDownList ID="ddlEtnia" runat="server" Enabled="true" Valor="31" Source="Parametros" Width="90%"></ruv:DropDownList>
        </p>
        <p style="width: 46%">
            <label>Comunidad:</label>
            <asp:TextBox ID="txbComunidad" runat="server" Enabled="true" Width="90%"></asp:TextBox>
        </p>
        <p style="width: 46%">
            <label>Mujer Cabeza de Hogar:</label>
            <asp:RadioButtonList ID="rblMujerCabezaHogar" runat="server" Width="90%" RepeatLayout="Flow" RepeatDirection="Horizontal">
                <asp:ListItem Value="1">Sí</asp:ListItem>
                <asp:ListItem Value="0">No</asp:ListItem>
            </asp:RadioButtonList>
        </p>
        <p style="width: 46%">
            <label>Gestante o Lactante:</label>
            <asp:RadioButtonList ID="rblGestanteLactante" runat="server" Width="90%" RepeatLayout="Flow" RepeatDirection="Horizontal">
                <asp:ListItem Value="1">Sí</asp:ListItem>
                <asp:ListItem Value="0">No</asp:ListItem>
            </asp:RadioButtonList>
        </p>
        <p style="width: 46%">
            <label>Discapacidades:</label>
            <ruv:CheckBoxList ID="cblDiscapacidades" runat="server" Enabled="true" Valor="2135" source="Parametros" RepeatLayout="Table" RepeatColumns="2"></ruv:CheckBoxList>
        </p>
        
        <p style="width: 100%">
            <label>Comentarios sobre la adición:</label>
            <asp:TextBox ID="ComentariosPersonaAgrgada" runat="server" TextMode="MultiLine" Height="50px" Width="90%" MaxLength="1000" placeholder="Comentarios"></asp:TextBox>
        </p>
    </div>
    
    <asp:Panel ID="pnlAgregarSoporte" runat="server" SkinID="pnlTitulo">
        <asp:Label ID="lblAgregarSoporte" runat="server" Text="Asociar Soporte" SkinID="lblSubTitulo" />
    </asp:Panel>
    <div class="fieldform content">
        <p style="width: 100%">
            <label>Seleccionar Archivo:</label>
            <asp:FileUpload ID="fuCargarImagen" runat="server" align="center" Width="90%" />
        </p>
    </div>

    <div class="ActionsBox">
        <asp:Button ID="btn_SubirImagen" runat="server" Text="Subir Imagen" onclick="btnSubirImagen_Click" />
        <asp:Button ID="btnAceptar" runat="server" CausesValidation="true" onclick="btnAceptar_Click" Text="Aceptar" />
        <asp:Button ID="btnCancelar" runat="server" CausesValidation="false" onclick="btnCancelar_Click" Text="Cancelar" />
    </div>
</asp:Content>