<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Dashboard.aspx.cs" Inherits="CMSModules_Ecommerce_Pages_Tools_Dashboard_Dashboard"
    Theme="Default" EnableEventValidation="false" %>

<%@ Register Src="~/CMSModules/Widgets/Controls/Dashboard.ascx" TagName="Dashboard" TagPrefix="cms" %>
<%=DocType%>
<html xmlns="http://www.w3.org/1999/xhtml" <%=XmlNamespace%>>
<head id="Head1" runat="server" enableviewstate="false">
    <title id="Title1" runat="server">Dashboard</title>
    <asp:Literal runat="server" ID="ltlTags" EnableViewState="false" />
    <style type="text/css">
        body
        {
            margin: 0px;
            padding: 0px;
            height: 100%;
            font-family: Arial;
            font-size: small;
        }
    </style>
</head>
<body  class="<%=BodyClass%>">
    <form id="form1" runat="server">
        <cms:Dashboard ID="ucDashboard" runat="server" ShortID="d" />
    </form>
</body>
</html>
