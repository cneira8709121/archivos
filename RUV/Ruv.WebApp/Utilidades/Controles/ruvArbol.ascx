<%@ Control Language="C#" AutoEventWireup="true" Inherits="Utilidades_Controles_dpsArbol" Codebehind="ruvArbol.ascx.cs" %>
<div id="divArbol">
    <asp:TreeView ID="trvArbol" runat="server" >
        <DataBindings>
            <asp:TreeNodeBinding TextField="title" DataMember="book" NavigateUrlField="link"  />
        </DataBindings>
    </asp:TreeView>    
</div>
