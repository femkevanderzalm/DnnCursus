<%@ Control Language="C#" AutoEventWireup="false" Inherits="XCESS.Documents.Settings" Codebehind="Settings.ascx.cs" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<%@ Register TagPrefix="dnnweb" Namespace="DotNetNuke.Web.UI.WebControls" Assembly="DotNetNuke.Web" %>
<div class="dnnFormItem">
<dnn:Label runat ="server" id="lblFolder"></dnn:Label>
<asp:TextBox runat="server" id="txtFolder" visible="false"/>
<dnnweb:DnnFolderDropDownList ID="DropDownListFolder" runat="server" />
</div>
