<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/OneColumn.Master" Inherits="System.Web.Mvc.ViewPage<FrogBlogger.Web.Models.ViewPostViewModel>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server"><%= Model.Post.Title %></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="server">
    <link href="/Content/ui.stars.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Scripts/ui/ui.stars.js"></script>
    <script type="text/javascript">
        $(function () {            
            // Ratings
            $rating = $("#rating");
            $rating.children().not("select").hide();

            $rating.stars({
                inputType: "select",
                captionEl: $("#hover_ocv2"),
                oneVoteOnly: true,
                callback: function (ui, type, value) {
                    $rating.submit();
                    //alert("Callback! Clicked: " + type + ", value: " + value);
                },
                cancelClass: 'ui-crystal-cancel',
                starClass: 'ui-crystal-star',
                starOnClass: 'ui-crystal-star-on',
                starHoverClass: 'ui-crystal-star-hover',
                cancelHoverClass: 'ui-crystal-cancel-hover',
                cancelShow: false
            });
        });
    </script>
    <% Html.RenderPartial("jQueryFormUserControl", "rating"); %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
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
                        <p><strong><%= Html.Encode(comment.Subject) %></strong> - <%= comment.PostedDate %> by <%= Html.FormatAuthor(comment) %>
                        <%= Html.Gravatar(comment.EmailAddress, 25) %></p>
                        <p><%= Html.Encode(comment.Comment) %></p>
                    </div> <%
               }
           } %>
    </div>
    <div class="rating"><span id="hover_ocv2" class="rating-cap"></span>
		<form id="rating" action="/Post/Rate" method="post">
			<select id="rating" name="rating" style="width: 120px">
				<option value="1">Very poor</option>
				<option value="2">Not that bad</option>
				<option value="3" selected="selected">Average</option>
				<option value="4">Good</option>
				<option value="5">Perfect</option>
			</select>
            <%= Html.Hidden("id", Model.Post.BlogPostId) %>
			<input type="button" value="Rate!">
		</form>
        <div id="averageRating" style="clear: both;"><%= Html.AverageRating(Model.TotalRatings, Model.AverageRating, 5) %></div>
	</div>
    <fieldset id="leaveComment">
        <legend>Leave Comment</legend>
        <%= Html.ValidationSummary("Create was unsuccessful. Please correct the errors and try again.") %>
        <form action="/Post/Comment" method="post">
            <label for="Subject">Subject: <em>*</em></label>
            <input id="Subject" type="text" />
            <%= Html.ValidationMessage("Subject", "*")%>
            
            <label for="Author">Name: <em>*</em></label>
            <%= Html.TextBox("Author")%>
            <%= Html.ValidationMessage("Author", "*")%>

            <label for="Url">Your URL:</label>
            <%= Html.TextBox("Url")%>

            <label for="EmailAddress">Email: <em>*</em></label>
            <%= Html.TextBox("EmailAddress")%>
            <%= Html.ValidationMessage("EmailAddress", "*")%>

            <label for="Comment">Comment: <em>*</em></label>
            <%= Html.TextArea("Comment") %>
            <%= Html.ValidationMessage("Comment", "*")%>

            <%= Html.Hidden("BlogPostId", Model.Post.BlogPostId) %>
            <input type="submit" value="Submit" class="button" />
        </form>
    </fieldset>
</asp:Content>
