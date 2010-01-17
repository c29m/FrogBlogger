<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/OneColumn.Master" Inherits="System.Web.Mvc.ViewPage<FrogBlogger.Dal.BlogPost>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">New Blog Post</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="server">
    <link rel="stylesheet" href="/Content/jquery.autocomplete.css" type="text/css" />
    <script type="text/javascript" src="/Scripts/tinymce/jscripts/tiny_mce/tiny_mce.js"></script>
    <script type="text/javascript" src="/Scripts/ui/jquery.autocomplete.js"></script>
    <script type="text/javascript">
        tinyMCE.init({
            // General options
            mode: "textareas",
            theme: "advanced",
            plugins: "safari,spellchecker,pagebreak,style,layer,table,save,advhr,advimage,advlink,emotions,iespell,inlinepopups,insertdatetime,preview,media,searchreplace,print,contextmenu,paste,directionality,fullscreen,noneditable,visualchars,nonbreaking,xhtmlxtras,template,wordcount,imagemanager,filemanager",

            // Theme options
            theme_advanced_buttons1: "save,newdocument,|,bold,italic,underline,strikethrough,|,justifyleft,justifycenter,justifyright,justifyfull,|,styleselect,formatselect,fontselect,fontsizeselect",
            theme_advanced_buttons2: "cut,copy,paste,pastetext,pasteword,|,search,replace,|,bullist,numlist,|,outdent,indent,blockquote,|,undo,redo,|,link,unlink,anchor,image,cleanup,help,code,|,insertdate,inserttime,preview,|,forecolor,backcolor",
            theme_advanced_buttons3: "tablecontrols,|,hr,removeformat,visualaid,|,sub,sup,|,charmap,emotions,iespell,media,advhr,|,print,|,ltr,rtl,|,fullscreen",
            theme_advanced_buttons4: "insertlayer,moveforward,movebackward,absolute,|,styleprops,spellchecker,|,cite,abbr,acronym,del,ins,attribs,|,visualchars,nonbreaking,template,blockquote,pagebreak,|,insertfile,insertimage",
            theme_advanced_toolbar_location: "top",
            theme_advanced_toolbar_align: "left",
            theme_advanced_statusbar_location: "bottom",
            theme_advanced_resizing: true,

            // Example content CSS (should be your site CSS)
            content_css: "css/example.css",

            // Drop lists for link/image/media/template dialogs
            template_external_list_url: "js/template_list.js",
            external_link_list_url: "js/link_list.js",
            external_image_list_url: "js/image_list.js",
            media_external_list_url: "js/media_list.js",

            // Replace values for the template plugin
            template_replace_values: {
                username: "Some User",
                staffid: "991234"
            }
        });
</script>
<script type="text/javascript">
    $(document).ready(function () {
        var data = "<%= (string)ViewData["tagdata"] %>".split(" ");
        $("#tags").autocomplete(data, 
        {
            multiple: true,
        });
    });
</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h2>New Blog Post</h2>
    <fieldset>
        <%= Html.ValidationSummary("Create was unsuccessful. Please correct the errors and try again.") %>
        <% using (Html.BeginForm()) {%>
            <p>
                <label for="Title">Title:</label>
                <%= Html.TextBox("Title") %>
                <%= Html.ValidationMessage("Title", "*") %>
            </p>
            <p>
                <%= Html.TextArea("Post") %>
                <%= Html.ValidationMessage("Post", "*") %>
            </p>
            <p>
                <label for="tags">Tags:</label>
                <%= Html.TextBox("tags") %>
            </p>
            <p>
                <label for="Visible">Visible:</label>
                <%= Html.CheckBox("Visible", true) %>
                <%= Html.ValidationMessage("Visible", "*") %>
            </p>
            <p>
                <input type="submit" value="Create" />
            </p>
        <% } %>
        <div>
            <%=Html.ActionLink("Back to List", "Index") %>
        </div>
    </fieldset>
</asp:Content>

