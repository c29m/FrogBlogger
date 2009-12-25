<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<FrogBlogger.Web.Models.HomeViewModel>" %>
<h2>Archive</h2>
<ul>
	<%foreach (FrogBlogger.Dal.BlogPost post in Model.BlogPosts) { %>
        <li><%= post.Title%></li>
    <% } %>
</ul>