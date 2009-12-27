<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<string>" %>
<script type="text/javascript" src="/Scripts/ui/jquery.form.js"></script>
<script type="text/javascript">
    $(function () {
        $("#<%=Model %>").ajaxForm({
            // dataType identifies the expected content type of the server response 
            dataType: 'json',

            // success identifies the function to invoke when the server response 
            // has been received 
            success: processJson
        });

    });

    // Processes the JSON result from the server after user provides a rating
    function processJson(data) {
        // 'data' is the json object returned from the server and Status is
        // the status property for the type created in the action method
        if (data.Status != "Successful") {
            alert('Failed to apply rating. Status - ' + data.Status);
        }
    }
</script>