<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/OneColumn.Master" Inherits="System.Web.Mvc.ViewPage<FrogBlogger.Web.Models.AdminViewModel>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">Administer Blog Posts</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="server">
    <link rel="Stylesheet" href="/Content/ui.tabs.css" type="text/css" />
	<link rel="stylesheet" href="/Content/jquery-ui-1.7.2.custom.css" type="text/css" media="all" />
    <script type="text/javascript" src="/Scripts/ui/ui.tabs.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#container-1 > ul').tabs();
        });

        $(function () {
            var name = $("#name"),
			email = $("#email"),
			allFields = $([]).add(name).add(email),
			tips = $("#validateTips");

            function updateTips(t) {
                tips.text(t).effect("highlight", {}, 1500);
            }

            function checkLength(o, n, min, max) {

                if (o.val().length > max || o.val().length < min) {
                    o.addClass('ui-state-error');
                    updateTips("Length of " + n + " must be between " + min + " and " + max + ".");
                    return false;
                } else {
                    return true;
                }
            }

            function checkRegexp(o, regexp, n) {

                if (!(regexp.test(o.val()))) {
                    o.addClass('ui-state-error');
                    updateTips(n);
                    return false;
                } else {
                    return true;
                }
            }

            $("#createUserDialog").dialog({
                bgiframe: true,
                autoOpen: false,
                height: 300,
                modal: true,
                buttons: {
                    'Create an account': function () {
                        var bValid = true;
                        allFields.removeClass('ui-state-error');

                        bValid = bValid && checkLength(name, "username", 3, 16);
                        bValid = bValid && checkLength(email, "email", 6, 80);

                        bValid = bValid && checkRegexp(name, /^[a-z]([0-9a-z_])+$/i, "Username may consist of a-z, 0-9, underscores, begin with a letter.");
                        // From jquery.validate.js (by joern), contributed by Scott Gonzalez: http://projects.scottsplayground.com/email_address_validation/
                        bValid = bValid && checkRegexp(email, /^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$/i, "eg. ui@jquery.com");

                        if (bValid) {
                            $('#userList tbody').append('<tr>' +
							'<td>' + name.val() + '</td>' +
							'<td>' + email.val() + '</td>' +
							'</tr>');

                            var f = $("#createUser");
                            var action = f.attr("action");
                            var serializedForm = f.serialize();

                            $.post(action, serializedForm, function () { alert("User created!"); });
                            $(this).dialog('close');
                        }
                    },
                    Cancel: function () {
                        $(this).dialog('close');
                    }
                },
                close: function () {
                    allFields.val('').removeClass('ui-state-error');
                }
            });

            $('#create-user').click(function () {
                $('#createUserDialog').dialog('open');
            })
		.hover(
			function () {
			    $(this).addClass("ui-state-hover");
			},
			function () {
			    $(this).removeClass("ui-state-hover");
			}
		).mousedown(function () {
		    $(this).addClass("ui-state-active");
		})
		.mouseup(function () {
		    $(this).removeClass("ui-state-active");
		});

        });

        function confirmDelete(action) {
            if (confirm("Are you sure you want to delete?")) {
                $.ajax({
                    callback: deleteCompleted(),
                    type: "DELETE",
                    url: action
                });
            }
        }
        
        function deleteCompleted()  
        {  
            // Reload page  
            window.location.reload();  
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h2>BlogName Admin</h2>
    <div id="container-1">
        <ul>
            <li><a href="#posts"><span>Posts</span></a></li>
            <li><a href="#users"><span>Users</span></a></li>
        </ul>
        <div id="posts">
            <table>
                <tr>
                    <th>Title</th>
                    <th>Date Posted</th>
                    <th>Active</th>
                    <th>Web Views</th>
                    <th>Agg Views</th>
                    <th>&nbsp;</th>
                    <th>&nbsp;</th>
                    <th>&nbsp;</th>
                    <th>&nbsp;</th>
                </tr>
            <% foreach (var item in Model.BlogPosts) { %>
                <tr>
                    <td><%= Html.ActionLink(item.Title, "Edit", new { id = item.BlogPostId })%></td>
                    <td><%= item.PostedDate.Value.ToShortDateString() %></td>
                    <td><%= item.Visible.ToString() %></td>
                    <td>10</td>
                    <td>10</td>
                    <td><a href="#">Referrals</a></td>
                    <td><%= Html.ActionLink("View", "Details", "Post", new { id = item.BlogPostId  }, null)%></td>
                    <td><a onclick="confirmDelete('<%= string.Format("/Admin/Delete/{0}", item.BlogPostId) %>')" href="JavaScript:void(0)">Delete</a></td>
                </tr>
            <% } %>
            </table>
            <p>
                <%= Html.ActionLink("Create New", "Create") %>
            </p>
        </div>
        <div id="users">
            <div id="createUserDialog" title="Create new user">
                <p id="validateTips">All form fields are required.</p>
                <form id="createUser" action="/Admin/CreateUser" method="post">
                    <fieldset>
                        <label for="name">Name:</label>
                        <input type="text" name="name" id="name" class="text ui-widget-content ui-corner-all" />
                        <label for="email">Email:</label>
                        <input type="text" name="email" id="email" value="" class="text ui-widget-content ui-corner-all" />
                    </fieldset>
                </form>
            </div>
            <table id="userList">
                <tr>
                    <th>Author</th>
                    <th>&nbsp;</th>
                </tr>
                <% foreach (FrogBlogger.Dal.aspnet_Users user in Model.Authors) { %>
                    <tr>
                        <td><%= user.UserName %></td>
                        <td><a onclick="confirmDelete('<%= string.Format("/Admin/DeleteAuthor/{0}", user.UserId) %>')" href="JavaScript:void(0)">Delete</a></td>
                    </tr>
                <% } %>
            </table>
            <button id="create-user" class="ui-button ui-state-default ui-corner-all">Create new user</button>
        </div>
    </div>
</asp:Content>