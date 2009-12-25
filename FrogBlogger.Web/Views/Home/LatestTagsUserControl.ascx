<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<FrogBlogger.Web.Models.HomeViewModel>" %>
<h2>Latest Tags</h2>
<ul>
	<% foreach (FrogBlogger.Dal.Keyword keyword in Model.LatestTags) { %>
        <li><%= keyword.Keyword1 %></li>
    <% } %>
</ul>