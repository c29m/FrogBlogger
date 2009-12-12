<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="post">
        <h1 class="title"><%= Html.Encode(ViewData["Message"]) %></h1>
        <p class="byline"><small>Posted by Sean Fao</small></p>
        <div class="entry">
            <p>
                To learn more about FrogBlogger visit <a href="http://geekswithblogs.net/seanfao" title="Sean Fao blog">http://geekswithblogs.net/seanfao</a>.
            </p>
        </div><!-- end entry -->
        <div class="meta">
				<p class="links"><a href="#" class="comments">Comments (32)</a> &nbsp;&bull;&nbsp;&nbsp; <a href="#" class="more">Read full post &raquo;</a></p>
		</div><!-- end meta -->
    </div><!-- end post -->
    <div class="post">
        <h1 class="title">Test Post</h1>
        <p class="byline"><small>Posted by Sean Fao</small></p>
        <div class="entry">
            <p>Sed lacus. Donec lectus. Nullam pretium nibh ut turpis. Nam bibendum. In nulla tortor, elementum vel,
            tempor at, varius non, purus. Mauris vitae nisl nec metus placerat consectetuer. Donec ipsum. Proin
            imperdiet est. Phasellus dapibus semper urna. Pellentesque ornare, orci in consectetuer hendrerit, urna
            elit eleifend nunc, ut consectetuer nisl felis ac diam. Etiam non felis. Donec ut ante. In id eros.
            Suspendisse lacus turpis, cursus egestas at sem. Mauris quam enim, molestie in, rhoncus ut, lobortis a,
            est.</p>
        </div><!-- end entry -->
        <div class="meta">
				<p class="links"><a href="#" class="comments">Comments (3)</a> &nbsp;&bull;&nbsp;&nbsp; <a href="#" class="more">Read full post &raquo;</a></p>
		</div><!-- end meta -->
    </div>
</asp:Content>
