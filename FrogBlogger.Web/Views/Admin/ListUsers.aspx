<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/OneColumn.Master" Inherits="System.Web.Mvc.ViewPage<System.Web.Security.MembershipUserCollection>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ListUsers
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>All Users</h2>
    <table>
        <tr>
            <th>User</th>
        </tr>
        <% foreach (MembershipUser user in Model) { %>
            <tr>
                <td><%= user.UserName %></td>
            </tr>
        <% } %>
    </table>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
