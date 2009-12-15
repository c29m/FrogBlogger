<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/OneColumn.Master" Inherits="System.Web.Mvc.ViewPage<FrogBlogger.Web.Models.ViewPostViewModel>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server"><%= Model.Post.Title %></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="title"><%= Html.Encode(Model.Post.Title) %></div>
    <div class="post">
        <%= Model.Post.Post %>
    </div>
    <p>
        PostedDate:
        <%= Html.Encode(String.Format("{0:g}", Model.Post.PostedDate)) %>
    </p>
    <div id="comments">
        <% if (Model.Comments.Count < 1)
           { %>
            <a href="#comments">No comments</a>
        <% }
           else
           {
               %> <a href="#comments"><h3>Comments</h3></a> <%
               foreach (FrogBlogger.Dal.UserComment comment in Model.Comments)
               { %>
                    <div class="comment">
                        <p><strong><%= comment.Subject %></strong> - <%= comment.PostedDate %> by <%= comment.Author %>
                        <%= Html.Gravatar(comment.EmailAddress, 25) %></p>
                        <p><%= comment.Comment %></p>
                    </div> <%
               }
           } %>
    </div>
    <div id="leaveComment">
        <h3>Leave Comment</h2>
        <%= Html.ValidationSummary("Create was unsuccessful. Please correct the errors and try again.") %>
        <form action="/Post/Comment" method="post">
            <p>
                <label for="Subject">Subject:</label>
                <%= Html.TextBox("Subject")%>
                <%= Html.ValidationMessage("Subject", "*")%>
            </p>
            <p>
                <label for="Author">Name:</label>
                <%= Html.TextBox("Author")%>
                <%= Html.ValidationMessage("Author", "*")%>
            </p>
            <p>
                <label for="Url">Your URL:</label>
                <%= Html.TextBox("Url")%>
            </p>
            <p>
                <label for="EmailAddress">Email:</label>
                <%= Html.TextBox("EmailAddress")%>
                <%= Html.ValidationMessage("EmailAddress", "*")%>
            </p>
            <p>
                <label for="Comment">Comment:</label>
                <%= Html.TextArea("Comment") %>
                <%= Html.ValidationMessage("Comment", "*")%>
            </p>
            <%= Html.Hidden("BlogPostId", Model.Post.BlogPostId) %>
            <input type="submit" value="Submit" />
        </form>
    </div>
</asp:Content>
