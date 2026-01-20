<%@ Page Title="Resumen Valoración" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeFile="Resumen.aspx.cs" Inherits="Valoracion_Valoracion_Resumen" %>

<%@ MasterType VirtualPath="~/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    <table width="100%" style="text-align:left">
        <tr>
            <td style="width:50%; vertical-align:top">
                <asp:DetailsView ID="dvInforDeclaracion1" runat="server" AutoGenerateRows="false" Width="100%">
                <Fields>
                    <asp:BoundField DataField="fechadeclaracion" HeaderText="Fecha Declaración" DataFormatString="<%$ AppSettings:FechaGrilla %>" />
                    <asp:BoundField DataField="FechaRadicado" HeaderText="Fecha Radicado" DataFormatString="<%$ AppSettings:FechaGrilla %>" />
                    <asp:BoundField DataField="numeroformulario" HeaderText="Numero Formulario" />
                    <asp:BoundField DataField="UnidadTerritorial" HeaderText="Unidad Regional" />
                    <asp:BoundField DataField="Departamento" HeaderText="Departamento" />
                    <asp:BoundField DataField="Municipio" HeaderText="Municipio" />
                </Fields>
                </asp:DetailsView>
            </td>
            <td style="width:50%; vertical-align:top">
                <asp:DetailsView ID="dvInforDeclaracion2" runat="server" AutoGenerateRows="false" Width="100%">
                    <Fields>
                        <asp:BoundField DataField="EstadoDeclaracion" HeaderText="Estado Declaracion" />
                        <asp:BoundField DataField="Valorador" HeaderText="Valorador" />
                        <asp:BoundField DataField="fechaasignacion" HeaderText="Fecha Asignación" DataFormatString="<%$ AppSettings:FechaGrilla %>" />
                        <asp:BoundField DataField="fechavaloracion" HeaderText="Fecha Valoración" DataFormatString="<%$ AppSettings:FechaGrilla %>" />
                    </Fields>
                </asp:DetailsView>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Panel ID="pnlTitulo" runat="server" SkinID="pnlTitulo">
                    <asp:Label ID="lblTitulo" runat="server" Text="DETALLE" />
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td style="vertical-align:top" colspan="2">
                <asp:GridView ID="gvHechos" runat="server" AutoGenerateColumns="false" SkinID="GridViewSinPaginacion" DataKeyNames="Id" OnSelectedIndexChanged="gvHechos_SelectedIndexChanged" >
                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="Id" Visible="false" />
                        <asp:BoundField DataField="HechoVictimizante" HeaderText="Hecho Victimizante" />
                        <asp:BoundField DataField="fechasiniestro" HeaderText="Fecha" />
                        <asp:BoundField DataField="LocalidadCorregimiento" HeaderText="Localidad/Corregimiento" />
                        <asp:BoundField DataField="BarrioVereda" HeaderText="Barrio/Vereda" />
                        <asp:BoundField DataField="Departamento" HeaderText="Departamento" />
                        <asp:BoundField DataField="Municipio" HeaderText="Municipio" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="img" runat="server" SkinID="imgBuscar" CommandName="Select" CausesValidation="false" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>    
            </td>
            </tr>
            <tr>
            <td colspan="2">
                <asp:Panel ID="pnlTitPer" runat="server" SkinID="pnlTitulo" Visible="false">
                    <asp:Label ID="lbltituloper" runat="server" Text="DETALLE PERSONA" />
                </asp:Panel>
            </td>
        </tr>
            <tr>
            <td style="vertical-align:top" colspan="2">
                <asp:GridView ID="dvInforPersona" runat="server" SkinID="GridViewSinPaginacion" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="Id" Visible="false" />
                        <asp:BoundField DataField="id_val_anexo" HeaderText="id_val_anexo" Visible="false" />
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                        <asp:BoundField DataField="TipoDocumento" HeaderText="Tipo Documento" />
                        <asp:BoundField DataField="numerodocumento" HeaderText="Numero Documento" />
                        <asp:BoundField DataField="Relacion" HeaderText="Relación" />
                        <asp:BoundField DataField="Genero" HeaderText="Genero" />
                        <asp:BoundField DataField="Edad" HeaderText="Edad" />
                        <asp:BoundField DataField="Etnia" HeaderText="Etnia" />
                        <asp:BoundField DataField="Discapacitado" HeaderText="Discapacitado" />
                        <asp:BoundField DataField="Fallecida" HeaderText="Fallecida" />
                        <asp:BoundField DataField="Desaparecida" HeaderText="Desaparecida" />
                        <asp:BoundField DataField="Secuestrado" HeaderText="Secuestrado" />
                        <asp:BoundField DataField="EstadoPorMina" HeaderText="EstadoPorMina" />
                        <asp:BoundField DataField="SeDesplazo" HeaderText="Se Desplazo" />
                        <asp:BoundField DataField="esafectado" HeaderText="Es Afectado" />
                        <asp:BoundField DataField="esvicitma" HeaderText="Es Victima" />
                        <asp:BoundField DataField="Afectaciones" HeaderText="Afectaciones" />
                        <asp:BoundField DataField="EstadoValoracion" HeaderText="Estado" />
                        <asp:BoundField DataField="ObservacionValoracion" HeaderText="Observacion Valoracion" />
                        <asp:BoundField DataField="Principios" HeaderText="Principios" />
                    </Columns>
                </asp:GridView>
            </td>
            
        </tr>
    </table>
</asp:Content>
