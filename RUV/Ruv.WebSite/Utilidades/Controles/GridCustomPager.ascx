<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GridCustomPager.ascx.cs" Inherits="Utilidades_Controles_GridCustomPager" %>
<div class="customPager">
    <asp:Panel ID="PagingEnabled" runat="server">
        <div class="pageInformation" style="float:left;">
            <span>Ver página </span>
            <asp:DropDownList ID="ddlPageNumber" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPageNumber_SelectedIndexChanged"></asp:DropDownList>
            <span> de </span>
            <asp:Label ID="lblShowRecords" runat="server"></asp:Label>
            <span> páginas </span>
        </div>
        <div class="pageSize" style="float:right;">
          <span>Ver </span>
          <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
            <asp:ListItem Text="10" Value="10"></asp:ListItem>
            <asp:ListItem Text="20" Value="20" Selected="True"></asp:ListItem>
            <asp:ListItem Text="50" Value="50"></asp:ListItem>
            <asp:ListItem Text="100" Value="100"></asp:ListItem>
          </asp:DropDownList>
          <span> registros por página</span>
        </div>
    </asp:Panel>
    <asp:Panel ID="PagingDisabled" runat="server" Visible="false">
        <div class="noPagingInformation" style="float:right;">
            <span>No existen registros</span>
        </div>
    </asp:Panel>
</div>