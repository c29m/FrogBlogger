<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FrogBlogger.Dal.Blog>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Index</h2>
    <table>
        <% foreach (FrogBlogger.Dal.Blog blog in Model) { %>
            <tr>
                <td><%= blog.Name%></td>
            </tr>
        <% } %>
    </table>
</asp:Content>
