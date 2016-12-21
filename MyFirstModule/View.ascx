<%@ Control language="C#" Inherits="MyFirstModule.View" AutoEventWireup="true" Codebehind="View.ascx.cs" %>
<%@ Import Namespace="DotNetNuke.Services.FileSystem" %>

<asp:Label runat="server" id="lblMyModuleTitle"></asp:Label><asp:Label runat="server" id="lblCurrentUser"></asp:Label>
<asp:Repeater runat="server" id="rptFiles">
    <HeaderTemplate><ul></HeaderTemplate>
    <ItemTemplate>
        <li><a href="#" target="_blank"><%# ((FileInfo)Container.DataItem).FileName %></a></li>
    </ItemTemplate>
    <FooterTemplate></ul></FooterTemplate>
</asp:Repeater>