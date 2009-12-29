<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<div id="menu">
    <ul>              
        <li class="first"><%= Html.ActionLink("Home", "Index", "Home")%></li>
        <%= Html.AdminMenuItem(Context.User) %>
        <li><a href="#">Syndication <img src="/Content/Images/xml.gif" alt="XML" /></a></li>
	    <li><a href="#">Resume</a></li>
        <li><%= Html.ActionLink("About", "About", "Home")%></li>
    </ul>
</div><!-- end #menu -->


