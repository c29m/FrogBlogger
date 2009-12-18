<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FrogBlogger.Web.Models.HomeViewModel>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">FrogBlogger</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="blogStats">Blog Stats - <%= Model.BlogPostCount %> post<%= Model.BlogPostCount != 1 ? "s" : String.Empty %></div>
    <% foreach (FrogBlogger.Dal.BlogPost post in Model.BlogPosts) { %>
        <div class="post">
            <div class="title"><%= post.Title %></div>
            <p class="author"><%= String.Format("{0} - {1}", "Sean Fao", post.PostedDate) %></p>
            <div class="entry">
                <%= post.Post %>
            </div><!-- end entry -->
            <div class="meta">
				<p class="links"><a href="#" class="comments"><%= Html.ActionLink("Comments", "Details", "Post", new { id = post.BlogPostId }, null) %> (<%= Model.BlogPostCommentCount[post.BlogPostId] %>)</a> - <%= Html.ActionLink("Read full post", "Details", "Post", new { id = post.BlogPostId }, null)%></p>
		    </div><!-- end meta -->
        </div><!-- end post -->
    <% } %>
</asp:Content>