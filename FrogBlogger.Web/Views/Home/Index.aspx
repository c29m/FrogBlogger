<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FrogBlogger.Web.Models.HomeViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">FrogBlogger</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% foreach (FrogBlogger.Dal.BlogPost post in Model.BlogPosts) { %>
        <div class="post">
            <h1 class="title"><%= post.Title %></h1>
            <p class="author"><%= String.Format("{0} - {1}", "Sean Fao", post.PostedDate) %></p>
            <div class="entry">
                <%= post.Post %>
            </div><!-- end entry -->
            <div class="meta">
				<p class="links"><a href="#" class="comments">Comments (<%= Model.BlogPostCommentCount[post.BlogPostId] %>)</a> - <%= Html.ActionLink("Read full post", "View", "Post", new { id = post.BlogPostId }, null)%></p>
		    </div><!-- end meta -->
        </div><!-- end post -->
    <% } %>
</asp:Content>

