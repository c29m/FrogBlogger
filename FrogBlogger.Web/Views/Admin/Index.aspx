<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/OneColumn.Master" Inherits="System.Web.Mvc.ViewPage<FrogBlogger.Web.Models.AdminViewModel>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">Administer Blog Posts</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Index</h2>
    <table>
        <tr>
            <th>Title</th>
            <th>Date Posted</th>
            <th>Active</th>
            <th>Web Views</th>
            <th>Agg Views</th>
            <th>&nbsp;</th>
            <th>&nbsp;</th>
            <th>&nbsp;</th>
            <th>&nbsp;</th>
        </tr>
    <% foreach (var item in Model.BlogPosts) { %>
        <tr>
            <td><%= Html.ActionLink(item.Title, "Edit", new { /* id=item.PrimaryKey */ })%></td>
            <td><%= item.PostedDate.Value.ToShortDateString() %></td>
            <td><%= item.Visible.ToString() %></td>
            <td>10</td>
            <td>10</td>
            <td><a href="#">Referrals</a></td>
            <td><%= Html.ActionLink("View", "View", "Post", new { id = item.BlogPostId  }, null)%></td>
            <td><%= Html.ActionLink("Delete", "Delete", new { id = item.BlogPostId  })%></td>
        </tr>
    <% } %>
    </table>
    <p>
        <%= Html.ActionLink("Create New", "Create") %>
    </p>
</asp:Content>

