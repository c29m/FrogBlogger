<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/OneColumn.Master" Inherits="System.Web.Mvc.ViewPage<FrogBlogger.Web.Models.AdminViewModel>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">Administer Blog Posts</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="server">
    <link rel="Stylesheet" href="/Content/ui.tabs.css" type="text/css" />
    <script type="text/javascript" src="/Scripts/ui/ui.tabs.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#container-1 > ul').tabs();
        });
        
        function confirmDelete() {
            return confirm("Are you sure you want to delete?");  
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h2>BlogName Admin</h2>
    <div id="container-1">
        <ul>
            <li><a href="#posts"><span>Posts</span></a></li>
            <li><a href="#users"><span>Users</span></a></li>

            <li><a href="#fragment-3"><span>Three</span></a></li>
        </ul>
        <div id="posts">
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
                    <td><%= Html.ActionLink("Delete", "Delete", new { id = item.BlogPostId }, new { onclick = "return confirmDelete()" })%></td>
                </tr>
            <% } %>
            </table>
            <p>
                <%= Html.ActionLink("Create New", "Create") %>
            </p>
        </div>
        <div id="users">
            <table>
                <tr>
                    <th>Author</th>
                    <th>&nbsp;</th>
                </tr>
                <% foreach (FrogBlogger.Dal.aspnet_Users user in Model.Authors) { %>
                    <tr>
                        <td><%= user.UserName %></td>
                        <td><%= Html.ActionLink("Delete", "DeleteAuthor", new { id = user.UserId }, new { onclick = "return confirmDelete()" })%></td>
                    </tr>
                <% } %>
            </table>
        </div>
        <div id="fragment-3">
            <p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat.</p>
            <p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat.</p>
            <p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat.</p>
        </div>
    </div>
</asp:Content>