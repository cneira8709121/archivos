<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ruvArbol.ascx.cs" Inherits="Utilidades_Controles_dpsArbol" %>
<div id="divArbol">
    <asp:TreeView ID="trvArbol" runat="server" >
        <DataBindings>
            <asp:TreeNodeBinding TextField="title" DataMember="book" NavigateUrlField="link"  />
        </DataBindings>
    </asp:TreeView>    
</div>
