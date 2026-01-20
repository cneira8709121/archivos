<%@ Page Title="Valorar" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false" Inherits="Valoracion_Valoracion_Nueva" ValidateRequest="false" CodeBehind="Nueva.aspx.cs" %>

<%@ MasterType VirtualPath="~/Site.Master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript" src='<%= Page.ResolveClientUrl("~/JScripts/Valoracion/JValoracionHelper.js") %>'></script>
    <script type="text/javascript" src='<%= Page.ResolveClientUrl("~/JScripts/Valoracion/JHechos.js") %>'></script>
    <script type="text/javascript" src='<%= Page.ResolveClientUrl("~/JScripts/Valoracion/JValorarPersona.js") %>'></script>
    <script type="text/javascript" src='<%= Page.ResolveClientUrl("~/JScripts/Valoracion/JValoresAA.js") %>'></script>
    <style>
        #search-container {
            display: flex;
            align-items: center;
            border-radius: 20px;
            padding: 10px;
            border-style: dashed;
        }

            #search-container input[type=text] {
                flex-grow: 1;
                padding: 10px;
                font-size: 16px;
                border: none;
                outline: none;
            }

            #search-container input[type=submit] {
                padding: 10px 20px;
                background-color: #4285f4;
                color: white;
                border: none;
                border-radius: 20px;
                cursor: pointer;
                font-size: 16px;
            }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="dvPrincipal" runat="server" style="width: 98%">
        <%--Inicio - Mensajes Error y Validacion--%>
        <asp:Label ID="lblError" runat="server" SkinID="lblError" ClientIDMode="Static"></asp:Label>
        <asp:UpdatePanel ID="updPanelValidacion" runat="server">
            <ContentTemplate>
                <ruv:Validaciones ID="Validaciones1" runat="server" />
                <ruv:Validaciones ID="Validaciones2" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <%--Fin - Mensajes Error y Validacion--%>
        <%--Inicio - Informacion Basica declaracion--%>
        <input type="hidden" id="hdnPais" value="">
        <input type="hidden" id="hdnDepartamento" value="">
        <input type="hidden" id="hdnMunicipio" value="">
        <input type="hidden" id="hdnEntidadMunicipio" value="">
        <asp:HiddenField ClientIDMode="Static" ID="hdnIdValoracion" Value="0" runat="server" />
        <asp:HiddenField ClientIDMode="Static" ID="hdnIdDeclaracion" Value="0" runat="server" />
        <asp:HiddenField ClientIDMode="Static" ID="hdnLogin" Value="" runat="server" />
        <asp:HiddenField ClientIDMode="Static" ID="hdnPassword" Value="" runat="server" />
        <asp:HiddenField ClientIDMode="Static" ID="hdnUrl" Value="" runat="server" />
        <table width="100%" style="text-align: left">
            <tr valign="top">
                <td style="width: 50%">
                    <asp:DetailsView ID="dvBasicaInfor" runat="server" Width="100%" AutoGenerateRows="false">
                        <Fields>
                            <asp:BoundField DataField="Formulario" HeaderText="Formulario Declaración" HeaderStyle-Width="200px" />
                            <asp:BoundField DataField="FechaRadicado" HeaderText="Fecha de Radicado" DataFormatString="<%$ AppSettings:FechaGrilla %>" />
                            <asp:BoundField DataField="UnidadTerritorial" HeaderText="Dirección Regional" />
                            <asp:BoundField DataField="Departamento" HeaderText="Departamento" />
                            <asp:BoundField DataField="Municipio" HeaderText="Municipio" />
                            <asp:BoundField DataField="Valorador" HeaderText="Valorador" />
                            <asp:TemplateField HeaderText="Fecha Valoración">
                                <ItemTemplate>
                                    <ruv:TextCalendar ID="txtFechaValoracion" runat="server" EsRequerido="true" MensajeRequerido="La fecha de valoración es requerida"
                                        Text='<%# Eval("FechaValoracion") %>' Width="75" MaxLength="10" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Fecha del Sistema">
                                <ItemTemplate>
                                    <asp:Label ID="lblFechaActual" runat="server" Text="<%# DateTime.Now.ToShortDateString() %>"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Fields>
                    </asp:DetailsView>
                </td>
                <td style="width: 50%">
                    <asp:UpdatePanel ID="upDetalle" runat="server">
                        <ContentTemplate>
                            <asp:DetailsView ID="dvBasicaInfo2" runat="server" Width="100%" AutoGenerateRows="false">
                                <Fields>
                                    <asp:TemplateField HeaderText="¿Este documento se considera una declaración?" HeaderStyle-Width="250px">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkValidacion" runat="server" OnCheckedChanged="chkValidacion_CheckedChanged"
                                                AutoPostBack="true" Checked='<%# Eval("EsDeclaracion") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Causal Devolución" Visible="true">
                                        <ItemTemplate>
                                            <ruv:CheckBoxList ID="chkCausales" runat="server" EsRequerido="false" RepeatColumns="1"
                                                Valor="10034" Source="CausalesDevolucion" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Observación Valoración">
                                        <ItemTemplate>
                                            <asp:TextBox ID="ObservacionValoracion" runat="server" TextMode="MultiLine" Height="50px" MaxLength="1000" placeholder="Observaciones de Finalización de la Valoración" Width="451px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Fields>
                            </asp:DetailsView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
        <%--Fin - Informacion Basica declaracion--%>
        <br />
        <%--Inicio - Captura Valoracion --%>
        <asp:UpdatePanel ID="regAnt" runat="server">
            <ContentTemplate>
                <asp:Panel ID="dvCaptura" runat="server" Visible="false">
                    <%--Inicio - Registros Anteriores--%>
                    <div style="text-align: left">
                        <asp:Panel ID="pnlRegAnt" runat="server" SkinID="pnlTitulo">
                            <asp:Label ID="lblTitRegAnte" runat="server" Text="REGISTROS ANTERIORES"></asp:Label>
                        </asp:Panel>
                        <asp:CheckBox ID="chkNoSeEncuentran" runat="server" Text="Ningún miembro de esta declaración se encuentra en registros anteriores"
                            AutoPostBack="true" OnCheckedChanged="chkNoSeEncuentran_CheckedChanged" />
                        <table id="tblRegistros" runat="server" style="text-align: left; width: 100%">
                            <tr>
                                <td>
                                    <asp:GridView ID="gvRegAneteriores" runat="server" SkinID="GridViewSinSeleccion"
                                        DataKeyNames="Id" OnSelectedIndexChanged="gvRegAneteriores_SelectedIndexChanged"
                                        AutoGenerateColumns="False" Width="100%">
                                        <Columns>
                                            <asp:BoundField HeaderText="Id" DataField="Id" Visible="false" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkMarcarRegistro" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Registros" DataField="Nombre" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgCapturar" runat="server" SkinID="imgBuscar" CommandName="Select"
                                                        CausesValidation="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="pnlPersonasRegistro" runat="server" SkinID="PanelmodalPopup" Width="500px">
                            <asp:Panel ID="pnlTituloRegAneterior" runat="server" SkinID="pnlTitulo">
                                <asp:Label ID="lblTituloRegAnterior" runat="server" Text="REGISTRO ANTERIOR" />
                            </asp:Panel>
                            <div style="text-align: left">
                                <asp:CheckBox ID="chkTodas" runat="server" Text="Todas las personas" AutoPostBack="true"
                                    OnCheckedChanged="chkTodas_CheckedChanged" />
                                <br />
                                <ruv:ListBox ID="lbxPersonas" runat="server" Width="100%" SelectionMode="Multiple"
                                    DataValueField="Id" DataTextField="Persona" />
                                <br />
                                <asp:Label ID="lblAdvertencia" runat="server" Text="Ctrl + Clic para seleccionar mas de una persona"
                                    SkinID="lblError"></asp:Label>
                                <br />
                                <ruv:CheckBoxList ID="chkPreguntas" runat="server" Source="PreguntasRegAnteriores" />
                            </div>
                            <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" OnClick="btnAceptar_Click" />
                        </asp:Panel>
                        <asp:LinkButton ID="lnkRegAnte" runat="server"></asp:LinkButton>
                        <ajax:ModalPopupExtender ID="mpExtRegAnt" runat="server" SkinID="PopUp" TargetControlID="lnkRegAnte"
                            DropShadow="true" BehaviorID="mpExtRegAntPopUpBehavior" PopupControlID="pnlPersonasRegistro">
                        </ajax:ModalPopupExtender>
                    </div>
                    <%--Fin - Registros Anteriores--%>
                    <%--Inicio - Captura Hechos--%>
                    <center>
                        <br />
                        
                        <asp:Panel ID="pnlTituHechos" runat="server" SkinID="pnlTitulo">
                            <asp:Label ID="lblTituHechos" runat="server" Text="HECHOS VICTIMIZANTES"></asp:Label>
                        </asp:Panel>
                        <div id="search-container">
                            <input type="text" id="txtBuscar" placeholder="Buscar persona" runat="server" />
                            <asp:Button ID="btnMiBoton" runat="server" Text="Buscar" OnClick="BuscarPersona" />
                        </div>
                        
                        <ajax:Accordion ID="acHechos" runat="server" Width="100%" SkinID="aPrincipal">
                            <HeaderTemplate>
                                <center>
                                    <asp:Label ID="txtHecho" runat="server" Text='<%# Eval("TipoHecho").ToString().ToUpper() %>'
                                        SkinID="lblBlanco"></asp:Label>
                                    <asp:HiddenField ID="hfHechoId" runat="server" Value='<%# Eval("Id") %>' />
                                </center>
                            </HeaderTemplate>
                            <ContentTemplate>
                                <%--Inicio - Datos Hecho Victimizante--%>
                                <div style="text-align: left">
                                    <table id="tblInfHecho" runat="server" border="1" style="text-align: left; border-collapse: collapse">
                                        <tr class="dvRow" runat="server">
                                            <td class="dvHeader" style="width: 200px">
                                                <asp:Label ID="lblFecha" runat="server" Text="Fecha del Hecho Victimizante" SkinID="lblBlanco"></asp:Label>
                                            </td>
                                            <td style="width: 300px">
                                                <asp:Label ID="txtFecha" runat="server" Text='<%# Convert.ToDateTime(Eval("Fecha")).ToShortDateString() %>'></asp:Label>
                                            </td>
                                        </tr>

                                        <tr class="dvRow" id="fechDespojo" visible='<%# Bind("MuestraDespojo") %>' runat="server">
                                            <td class="dvHeader" style="width: 200px">
                                                <asp:Label ID="lblFechDespojo" runat="server" Text="Fecha del Despojo" SkinID="lblBlanco"></asp:Label>
                                            </td>
                                            <td style="width: 300px">
                                                <asp:Label ID="txtFechDespojo" runat="server" Text='<%# Convert.ToDateTime(Eval("FechaDespojo")).ToShortDateString() %>'></asp:Label>

                                            </td>
                                        </tr>

                                        <tr class="dvRow" id="fechAbandono" runat="server" visible='<%# Bind("MuestraAbandono") %>'>
                                            <td class="dvHeader" style="width: 200px">
                                                <asp:Label ID="LblFechAbandono" runat="server" Text="Fecha del Abandono" SkinID="lblBlanco"></asp:Label>
                                            </td>
                                            <td style="width: 300px">
                                                <asp:Label ID="txtFechAbandono" runat="server" Text='<%# Convert.ToDateTime(Eval("FechaAbandono")).ToShortDateString() %>'></asp:Label>

                                            </td>
                                        </tr>



                                        <tr class="dvRow">
                                            <td class="dvHeader">
                                                <asp:Label ID="lblTipoEntorno" runat="server" Text="Tipo de Entorno" SkinID="lblBlanco"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="txtTipoEntorno" runat="server" Text='<%# Eval("TipoEntorno") %>'></asp:Label>
                                            </td>
                                        </tr>
                                        <tr class="dvRow">
                                            <td class="dvHeader">
                                                <asp:Label ID="lblLocalidad" runat="server" Text="Localidad/Corregimiento" SkinID="lblBlanco"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="txtLocalidad" runat="server" Text='<%# Eval("LocalidadCorregimiento") %>'></asp:Label>
                                            </td>
                                        </tr>
                                        <tr class="dvRow">
                                            <td class="dvHeader">
                                                <asp:Label ID="lblBarrio" runat="server" Text="Barrio/Vereda" SkinID="lblBlanco"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="txtBarrio" runat="server" Text='<%# Eval("BarrioVereda") %>'></asp:Label>
                                            </td>
                                        </tr>
                                        <tr class="dvRow">
                                            <td class="dvHeader">
                                                <asp:Label ID="lblDepartamento" runat="server" Text="Departamento" SkinID="lblBlanco"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="txtDepartamento" runat="server" Text='<%# Eval("Departamento") %>'></asp:Label>
                                            </td>
                                        </tr>
                                        <tr class="dvRow">
                                            <td class="dvHeader">
                                                <asp:Label ID="lblMunicipio" runat="server" Text="Municipio" SkinID="lblBlanco"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="txtMunicipio" runat="server" Text='<%# Eval("Municipio") %>'></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <%--Fin - Datos Hecho Victimizante--%>
                                <br />
                                <%--Inicio - Personas Hecho Victimizante--%>
                                <asp:GridView ID="gvPersonasAnexos" runat="server" SkinID="GridViewSinSeleccion"
                                    DataSource='<%# Eval("personas") %>' DataKeyNames="Id" AutoGenerateColumns="false"
                                    Width="100%" OnSelectedIndexChanged="gvPersonasAnexos_SelectedIndexChanged">
                                    <Columns>
                                        <%--Inicio - Informacion Persona--%>
                                        <asp:BoundField DataField="Id" HeaderText="Id" Visible="false" />
                                        <asp:BoundField DataField="Persona" HeaderText="Nombre" />
                                        <asp:BoundField DataField="Estado" HeaderText="Estado de Valoración" />
                                        <asp:BoundField DataField="TipoDocumento" HeaderText="Tipod de Documento" />
                                        <asp:BoundField DataField="NumeroDocumento" HeaderText="Numero de Documento" />
                                        <asp:BoundField DataField="Relacion" HeaderText="Relacion" />
                                        <asp:BoundField DataField="Genero" HeaderText="Genero" />
                                        <asp:BoundField DataField="Edad" HeaderText="Edad" />
                                        <asp:BoundField DataField="Etnia" HeaderText="Etnia" />
                                        <asp:CheckBoxField DataField="Discapacitado" HeaderText="Discapacitado" />
                                        <asp:TemplateField HeaderText="Información Persona" HeaderStyle-Width="350px">
                                            <ItemTemplate>
                                                <table width="100%" border="1" style="text-align: left; border-collapse: collapse">
                                                    <tr class="dvRow">
                                                        <td class="dvHeader" style="width: 150px">
                                                            <asp:Label ID="lblNombre" runat="server" Text="Persona" SkinID="lblBlanco"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txtNombre" runat="server" Text='<%# Eval("Persona") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr class="dvRow">
                                                        <td class="dvHeader" style="width: 150px">
                                                            <asp:Label ID="lblEstado" runat="server" Text="Estado Valoración" SkinID="lblBlanco"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txtEstado" runat="server" Text='<%# Eval("Estado") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr class="dvRow">
                                                        <td class="dvHeader">
                                                            <asp:Label ID="lblSitio" runat="server" Text="Tipo Documento" SkinID="lblBlanco"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txtSitio" runat="server" Text='<%# Eval("TipoDocumento") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr class="dvRow">
                                                        <td class="dvHeader">
                                                            <asp:Label ID="lblDepartamento" runat="server" Text="Número Documento" SkinID="lblBlanco"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txtDepartamento" runat="server" Text='<%# Eval("NumeroDocumento") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr class="dvRow">
                                                        <td class="dvHeader">
                                                            <asp:Label ID="lblMunicipio" runat="server" Text="Relación" SkinID="lblBlanco"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txtMunicipio" runat="server" Text='<%# Eval("Relacion") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr class="dvRow">
                                                        <td class="dvHeader">
                                                            <asp:Label ID="lblGenero" runat="server" Text="Género" SkinID="lblBlanco"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txtGenero" runat="server" Text='<%# Eval("Genero") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr class="dvRow">
                                                        <td class="dvHeader">
                                                            <asp:Label ID="lblEdad" runat="server" Text="Edad" SkinID="lblBlanco"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txtEdad" runat="server" Text='<%# Eval("Edad") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr class="dvRow">
                                                        <td class="dvHeader">
                                                            <asp:Label ID="lblEtnia" runat="server" Text="Étnia" SkinID="lblBlanco"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txtEtnia" runat="server" Text='<%# Eval("Etnia") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr class="dvRow">
                                                        <td class="dvHeader">
                                                            <asp:Label ID="lblDiscapacitado" runat="server" Text="Discapacitado" SkinID="lblBlanco"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:CheckBox ID="chkDiscapacitado" runat="server" Checked='<%# Eval("Discapacitado") %>'
                                                                Enabled="false" />
                                                        </td>
                                                    </tr>
                                                    <tr id="trFallecida" runat="server" visible="false" class="dvRow">
                                                        <td class="dvHeader">
                                                            <asp:Label ID="lblFallecida" runat="server" Text="Fallecida" SkinID="lblBlanco"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:CheckBox ID="chkFallecida" runat="server" Checked='<%# Eval("Fallecida") %>'
                                                                Enabled="false" />
                                                        </td>
                                                    </tr>
                                                    <tr id="trDesaparecida" runat="server" visible="false" class="dvRow">
                                                        <td class="dvHeader">
                                                            <asp:Label ID="lblDesaparecida" runat="server" Text="Desaparecida" SkinID="lblBlanco"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:CheckBox ID="chkDesaparecida" runat="server" Checked='<%# Eval("Desaparecida") %>'
                                                                Enabled="false" />
                                                        </td>
                                                    </tr>
                                                    <tr id="trSecuestrado" runat="server" visible="false" class="dvRow">
                                                        <td class="dvHeader">
                                                            <asp:Label ID="lblSecuestrado" runat="server" Text="Secuestrado" SkinID="lblBlanco"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:CheckBox ID="chkSecuestrado" runat="server" Checked='<%# Eval("Secuestrado") %>'
                                                                Enabled="false" />
                                                        </td>
                                                    </tr>
                                                    <tr id="trEstadoPorMina" runat="server" visible="false" class="dvRow">
                                                        <td class="dvHeader">
                                                            <asp:Label ID="lblEstadoPorMina" runat="server" Text="Estado Por Mina" SkinID="lblBlanco"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txtEstadoPorMina" runat="server" Text='<%# Eval("EstadoPorMina") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr id="trSeDesplazo" runat="server" visible="false" class="dvRow">
                                                        <td class="dvHeader">
                                                            <asp:Label ID="lblSeDesplazo" runat="server" Text="Se Desplazo" SkinID="lblBlanco"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:CheckBox ID="chkSeDesplazo" runat="server" Checked='<%# Eval("SeDesplazo") %>'
                                                                Enabled="false" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--Fin - Informacion Persona--%>
                                        <%--Inicio - Autores, Infracciones, Herramientas, ImgDetalle--%>
                                        <asp:TemplateField HeaderStyle-VerticalAlign="Top" ItemStyle-VerticalAlign="Top"
                                            HeaderStyle-Width="220px">
                                            <HeaderTemplate>
                                                <table width="220px">
                                                    <tr style="text-align: center">
                                                        <td colspan="2">
                                                            <asp:Label ID="lblAutor" runat="server" Text="Autor" SkinID="lblBlanco"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="vertical-align: middle; width: 100%">
                                                        <td style="text-align: left; width: 200px">
                                                            <ruv:DropDownList ID="ddlAutores" runat="server" Source="Autores" Width="200px" />
                                                        </td>
                                                        <td style="text-align: right">
                                                            <asp:ImageButton ID="lbtnAgregarATodosAutor" runat="server" OnClick="lbtnAgregarATodosAutor_Click" CausesValidation="false"
                                                                SkinID="imgAgregar" ToolTip="Agregar a Todos" />
                                                        </td>
                                                    </tr>
                                                    <tr style="text-align: left">
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table width="220px">
                                                    <tr style="vertical-align: middle; width: 100%">
                                                        <td style="text-align: left; width: 200px">
                                                            <ruv:DropDownList ID="ddlLAutores" runat="server" Source="Autores" Width="200px" />
                                                        </td>
                                                        <td style="text-align: right">
                                                            <asp:ImageButton ID="lbtnAgregarAutor" runat="server" OnClick="lbtnAgregarAutor_Click" CausesValidation="false"
                                                                SkinID="imgAgregar" ToolTip="Agregar Autor" />
                                                        </td>
                                                    </tr>
                                                    <tr style="vertical-align: middle; width: 100%">
                                                        <td style="text-align: left; width: 200px">
                                                            <ruv:ListBox ID="lbxAutores" runat="server" Width="200px" DataTextField="Nombre"
                                                                DataValueField="Id" Height="150px" DataSource='<%# Eval("Autores") %>' />
                                                        </td>
                                                        <td style="text-align: right">
                                                            <asp:ImageButton ID="lbtnQuitarAutor" runat="server" SkinID="imgQuitar" OnClick="lbtnQuitarAutor_Click" CausesValidation="false"
                                                                ToolTip="Quitar" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-VerticalAlign="Top" ItemStyle-VerticalAlign="Top"
                                            HeaderStyle-Width="220px">
                                            <HeaderTemplate>
                                                <table width="220px">
                                                    <tr style="text-align: Center">
                                                        <td colspan="2">
                                                            <asp:Label ID="lblInfraccion" runat="server" Text="Infracción DIH" SkinID="lblBlanco"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="vertical-align: middle; width: 100%">
                                                        <td style="text-align: left; width: 200px">
                                                            <ruv:DropDownList ID="ddlInfraccionesAnexo" runat="server" Source="Infracciones"
                                                                Width="200px" />
                                                        </td>
                                                        <td style="text-align: right">
                                                            <asp:ImageButton ID="lbtnAgregarATodos" runat="server" OnClick="lbtnAgregarATodos_Click" CausesValidation="false"
                                                                SkinID="imgAgregar" ToolTip="Agregar a Todos" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table width="220px">
                                                    <tr style="vertical-align: middle; width: 100%">
                                                        <td style="text-align: left; width: 200px">
                                                            <ruv:DropDownList ID="ddlLInfracciones" runat="server" Source="Infracciones" Width="200px" />
                                                        </td>
                                                        <td style="text-align: right">
                                                            <asp:ImageButton ID="lbtnAgregar" runat="server" OnClick="lbtnAgregar_Click" SkinID="imgAgregar" CausesValidation="false"
                                                                ToolTip="Agregar Infraccion" />
                                                        </td>
                                                    </tr>
                                                    <tr style="vertical-align: middle; width: 100%">
                                                        <td style="text-align: left; width: 200px">
                                                            <ruv:ListBox ID="lbxInfracciones" runat="server" Width="200px" DataValueField="Id"
                                                                Height="150px" DataTextField="Nombre" DataSource='<%# Eval("InfraccionesDHI") %>' />
                                                        </td>
                                                        <td style="text-align: right">
                                                            <asp:ImageButton ID="lbtnQuitar" runat="server" SkinID="imgQuitar" OnClick="lbtnQuitar_Click" CausesValidation="false"
                                                                ToolTip="Quitar" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Herramientas" HeaderStyle-Width="220px">
                                            <ItemTemplate>
                                                <ruv:Herramientas ID="Herramientas" runat="server" Persona='<%# Eval("Id") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Width="20px">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgDetalle" runat="server" SkinID="imgBuscar" CommandName="Select" CausesValidation="false" OnClientClick="ShowModConsult(null, 'Cargando...')" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--Inicio - Autores, Infracciones, Herramientas, ImgDetalle--%>
                                    </Columns>
                                </asp:GridView>
                                <%--Fin - Personas Hecho Victimizante--%>
                            </ContentTemplate>
                        </ajax:Accordion>
                    </center>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <%--Fin - Captura Hechos--%>
        <br />
        <br />
    </div>
    <%--Fin - Captura Valoracion --%>
    <%--Inicio- Nuevo Hecho Victimizante--%>
    <br />

    <ruv:HechoVictimizante ID="hvNuevo" runat="server" OnNuevoHecho="btnGuardarAnexo"
        OnErrores="ErrorGuardarHecho" />

    <%--Fin- Nuevo Hecho Victimizante--%>
    <%--Inicio - Valorar Persona--%>
    <asp:UpdatePanel ID="upDetallePersona" runat="server">
        <ContentTemplate>
            <ruv:ValorarPersona ID="personasDetalle" runat="server" OnGuardarOk="personaDetalle_OnGuardarOk" />
        </ContentTemplate>
    </asp:UpdatePanel>

    <%-- Fin - Valorar Persona--%>
    <%--Inicio - Valores AA--%>
    <asp:UpdatePanel ID="upValoresAA" runat="server">
        <ContentTemplate>
            <ruv:ValoresAA ID="valoresAA" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>

    <ruv:PersonasAsociadasDeclaracion ID="PersonasAsociadas" runat="server" />

    <%--Inicio - Mensajes--%>
    <asp:UpdatePanel ID="UdpMensajes" runat="server">
        <ContentTemplate>
            <ruv:ModalPopUp ID="mpoup" runat="server" MostrarBotones="true" VisibleBotonCancelar="true"
                DropShadow="true" MostrarImagen="false" filatextBox="false" OnOk="mpoup_Ok" OnCancel="mpoup_Cancel"
                BehaviorID="mpopBasic" />
            <ruv:ModalPopUp ID="mpopGuardar" runat="server" MostrarBotones="true" VisibleBotonCancelar="true" Mensaje="¿Esta seguro que desea finalizar la valoración? Esta operación puede tardar varios minutos."
                DropShadow="true" MostrarImagen="false" filatextBox="false" OnOk="mpopGuardar_Ok" OnOkScript="FinalizarValoracion()"
                BehaviorID="mpopGuardarBehavior" />
            <ruv:ModalPopUp ID="mpupError" runat="server" MostrarBotones="true" VisibleBotonCancelar="false"
                DropShadow="true" Mensaje="Ourrio un error al guardar, intente de nuevo de persistir el error comuniquese con el administrador"
                MostrarImagen="false" filatextBox="true" BehaviorID="mpupErrorBehavior" />
            <ruv:ModalPopUp ID="mpopMensajes" runat="server" MostrarBotones="true" VisibleBotonCancelar="false"
                DropShadow="true" MostrarImagen="false" filatextBox="false" BehaviorID="mpopMensajesBehavior" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--Fin - Mensajes y Ventanas Emergentes--%>

    <asp:Panel ID="pnlEditar" runat="server" SkinID="PanelmodalPopup" ClientIDMode="Static">
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblTitObservacioin" Text="Indique las razones para ingresar a editar la declaración" runat="server" SkinID="lblSubTitulo"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <ruv:TextBox ID="txtObservacionEditar" IdTextbox="txtObservacionEditar" runat="server" EsRequerido="false" TextMode="MultiLine" Height="100px" Width="400px" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnCancelarEditar" runat="server" Text="Cancelar" />
                    <asp:Button ID="btnAceptarEditar" runat="server" Text="Aceptar" OnClientClick="return EditarValoracionWPF();" />
                </td>
            </tr>
        </table>

    </asp:Panel>
    <asp:LinkButton ID="lnkeditar" runat="server"></asp:LinkButton>
    <ajax:ModalPopupExtender ID="mpopUpEditar" runat="server" SkinID="PopUp" TargetControlID="lnkeditar"
        DropShadow="true" BehaviorID="mpopUpEditarBehavior" PopupControlID="pnlEditar"
        CancelControlID="btnCancelarEditar" OnCancelScript="HidePopUp('mpopUpEditarBehavior')">
    </ajax:ModalPopupExtender>

    <asp:Panel ID="pnlNuevoActo" runat="server" SkinID="PanelmodalPopup" ClientIDMode="Static">
        <br />
        <table>
            <tr>
                <td>
                    <span class="lbl">Ya existe un acto administrativo con para esta declaración,
                        <br />
                        ¿desea crear un nuevo numero de resolución</span>
                </td>
            </tr>
        </table>
        <br />
        <div>
            <asp:Button ID="btnOkActo" runat="server" Text="Aceptar" OnClick="mpopNuevoActo_Ok" />
            <asp:Button ID="btnCancelActo" runat="server" Text="Cancelar" OnClick="mpopNuevoActo_Cancel" />
        </div>
    </asp:Panel>
    <asp:LinkButton ID="LinkButton1" runat="server"></asp:LinkButton>
    <ajax:ModalPopupExtender ID="pnlNuevoActoEx" runat="server" SkinID="PopUp" TargetControlID="LinkButton1"
        DropShadow="true" BehaviorID="pnlNuevoActoBehavior" PopupControlID="pnlNuevoActo">
    </ajax:ModalPopupExtender>
</asp:Content>
