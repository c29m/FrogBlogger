<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FrogBlogger.Dal.Blog>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Create</h2>
    <form action="/Blog/Create" method="post">
        <table>
            <tr>
                <td>Name:</td>
                <td><input id="Name" name="Name" type="text" /></td>
            </tr>
        </table>
        <input type="Submit" value="Submit" />
    </form>
</asp:Content>
