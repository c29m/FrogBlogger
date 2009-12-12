<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="aboutTitle" ContentPlaceHolderID="TitleContent" runat="server">
    About Us
</asp:Content>

<asp:Content ID="aboutContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>FrogBlogger</h2>
    <img src="/Content/Images/LogoLarge.png" alt="Logo" />
    <p>
        FrogBlogger is a blogging engine built on top of ASP.NET MVC 2 in Visual Studio 2010.
    </p>
</asp:Content>
