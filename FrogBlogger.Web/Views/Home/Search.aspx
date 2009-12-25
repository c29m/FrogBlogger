<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/OneColumn.Master" Inherits="System.Web.Mvc.ViewPage<FrogBlogger.Web.Models.BlogListBase>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">Search Results</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Search Results</h2>
    <div id="searchResults">
        <% foreach (FrogBlogger.Dal.BlogPost post in Model.BlogPosts) { %>
            <div class="searchResult">
                <p class="title"><%= post.Title %></p>
                <p class="post"><%= post.Post %></p>
            </div>
        <% } %>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

