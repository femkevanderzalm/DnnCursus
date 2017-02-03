<%@ Control language="C#" Inherits="XCESS.Documents.View" AutoEventWireup="true" Codebehind="View.ascx.cs" %>
<%@ Import Namespace="DotNetNuke.Services.FileSystem" %>

<asp:Label runat="server" id="lblMyModuleTitle"></asp:Label><asp:Label runat="server" id="lblCurrentUser" Font-Bold="True"></asp:Label>

<br />
<asp:TreeView ID="TreeView1" runat="server" Height="399px" ImageSet="BulletedList"  style="margin-left: 2px; margin-right: 6px; margin-top: 34px; margin-bottom: 3px" Width="294px" Visible="False" ShowExpandCollapse="False" NodeWrap="True">
    <HoverNodeStyle Font-Underline="True" />
    <LeafNodeStyle BorderStyle="None" />
    <NodeStyle Font-Size="10pt" HorizontalPadding="20px" NodeSpacing="10px" VerticalPadding="4px" ChildNodesPadding="10px" Width="85%" />
    <ParentNodeStyle Font-Bold="False" ChildNodesPadding="10px" Width="80%" />
    <SelectedNodeStyle Font-Underline="True" VerticalPadding="0px" BorderStyle="None" />
</asp:TreeView>




<br />




