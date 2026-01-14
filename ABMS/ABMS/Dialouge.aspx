<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dialouge.aspx.cs" Inherits="ABMS.Dialouge" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="Scripts/jquery-1.12.4.js"></script>
    <script src="Scripts/jquery-ui-1.12.1.js"></script>
    <link href="jquery-ui-1.11.0.custom/jquery-ui.css" rel="stylesheet" />
    
    <script type="text/javascript">
        var dialog;
        //$("#dialog-form").dialog({
        //    width: 'auto', // overcomes width:'auto' and maxWidth bug
        //    maxWidth: 600,
        //    height: 'auto',
        //    modal: true,
        //    fluid: true, //new option
        //    resizable: false
        //});


        // on window resize run function
        $(window).resize(function () {
            fluidDialog();
        });

        // catch dialog if opened within a viewport smaller than the dialog width
        $(document).on("dialogopen", ".ui-dialog", function (event, ui) {
            fluidDialog();
        });

        function fluidDialog() {
            var $visible = $(".ui-dialog:visible");
            // each open dialog
            $visible.each(function () {
                var $this = $(this);
                var dialog = $this.find(".ui-dialog-content").data("dialog");
                // if fluid option == true
                if (dialog.options.fluid) {
                    var wWidth = $(window).width();
                    // check window width against dialog width
                    if (wWidth < dialog.options.maxWidth + 50) {
                        // keep dialog from filling entire screen
                        $this.css("max-width", "90%");
                    } else {
                        // fix maxWidth bug
                        $this.css("max-width", dialog.options.maxWidth);
                    }
                    //reposition dialog
                    dialog.option("position", dialog.options.position);
                }
            });

        }
       
        $(function () {
           
          
          
          
            dialog = $("#dialog-form").dialog({
           
                modal: true,
                buttons: {                    
                    Cancel: function () {
                        dialog.dialog("close");
                    }
                },
                close: function () {
                    //form[0].reset();
                    //allFields.removeClass("ui-state-error");
                }
            });
            $("#create-user").button().on("click", function () {
                dialog.dialog("open");
                return false;
            });
        });

    </script>
    <div class="row">
        <button id="create-user">Create new user</button>
        <div id="dialog-form" title="Create new user">
            <p class="validateTips">All form fields are required.</p>
        </div>
    </div>
</asp:Content>
