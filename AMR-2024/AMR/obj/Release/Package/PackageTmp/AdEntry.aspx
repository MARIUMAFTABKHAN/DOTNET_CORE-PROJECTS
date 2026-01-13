<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdEntry.aspx.cs" Inherits="AMR.AdEntry" EnableEventValidation="false"%>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<%--<asp:UpdatePanel ID="up" runat="server" UpdateMode="Conditional">--%>
    <ContentTemplate>
        
<style>



     .client-result-item {
        padding: 6px 10px;
        cursor: pointer;
     }

    .client-result-item:hover,
    .client-result-item.highlight {
        background-color: #007BFF;
        color: blue;
    }

    .client-result-item {
    padding: 5px 10px;
    cursor: pointer;
}
.client-result-item:hover {
    background-color: #f0f0f0;
}
    .compact-dropdown {
        width: 50px; 
        height: 24px; 
        font-size: 12px; 
        padding: 2px;
    }
    .click {
        background-color: darkgray;
        color: black;          
        border: none;          
        padding: 10px 20px;    
        font-size: 14px;       
        cursor: pointer;       
    }

    .click:hover {
        background-color: gray;
        color: black;  
    }
</style>
<script type="text/javascript">
  
    function togglebackpage() {
        var ddlno = document.getElementById('<%= ddlno.ClientID %>');
        
        if (ddlno.disabled) {
            ddlno.disabled = false; 
        } else {
            ddlno.disabled = true; 
        }
    }

    function toggleMisc() {
        var ddlType = document.getElementById('<%= ddltype.ClientID %>');
        var ddlBrand = document.getElementById('<%= ddlBrand.ClientID %>');
        var clientId = document.getElementById('<%= hiddenClientId.ClientID %>').value; // Assuming a hidden field holds clientId
        <%--var txtBrand = document.getElementById('<%= txtBrand.ClientID %>');--%>

        if (!clientId) {
            console.log("Client ID is missing!");
            return;
        }


        if (ddlType.disabled) {
            ddlType.disabled = false; // Enable dropdown
            // Call function to populate the dropdown
            populateTypeDropdown();
            /*txtBrand.disabled = true; */// Disable text box
            ddlBrand.disabled = true;
            
        } else {
            

            //ddlType.selectedIndex = 0; // Reset dropdown selection
            ddlType.disabled = true; // Disable dropdown
            /* txtBrand.disabled = false;*/ // Enable text box
            ddlBrand.disabled = false;
            populateBrandDropdown(clientId);
            
            
        }
    }

    function populateTypeDropdown(selectedTypeId) {
        $.ajax({
            type: "POST",
            url: "AdEntry.aspx/GetTypes",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                var types = response.d; // 'd' contains the returned data
                var ddltype = $('#<%= ddltype.ClientID %>'); // Get dropdown by its ClientID
                ddltype.empty(); // Clear existing items
                ddltype.append('<option value="">Select Type</option>'); // Add default option

                $.each(types, function (index, item) {
                    ddltype.append('<option value="' + item.Id + '">' + item.Type1 + '</option>');
                });
                if (selectedTypeId) {
                    ddltype.val(selectedTypeId);
                    $('#<%= hiddenTypeId.ClientID %>').val(selectedTypeId);
                 }
            },
            error: function (xhr, status, error) {
                console.error("Error retrieving types: ", error);
            }
        });
    }

    $(document).on('change', '#<%= ddltype.ClientID %>', function () {
    var selectedtypeId = $(this).val();
        $('#<%= hiddenTypeId.ClientID %>').val(selectedtypeId); // Set the hidden field
        console.log('Selected Type ID:', selectedtypeId);
     });

    function populateBrandDropdown(clientId, selectedBrandId) {
        console.log("Fetching brands for Client ID:", clientId); // Debugging log
        $.ajax({
            type: "POST",
            url: "AdEntry.aspx/PopulateBrandDropdown",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ clientId: clientId }), // Pass clientId
            dataType: "json",
            success: function (response) {
                var brands = response.d;
                var ddlBrand = $('#<%= ddlBrand.ClientID %>');
                ddlBrand.empty();
                ddlBrand.append('<option value="">Select Brand</option>');

                $.each(brands, function (index, item) {
                ddlBrand.append('<option value="' + item.Id + '">' + item.Brand_Name + '</option>');
            });

            if (selectedBrandId) {
                ddlBrand.val(selectedBrandId);
                $('#<%= hiddenBrandId.ClientID %>').val(selectedBrandId);
            }
        },
        error: function (xhr, status, error) {
            console.error("Error retrieving types: ", error);
        }
    });
    }

    $(document).on('change', '#<%= ddlBrand.ClientID %>', function () {
    var selectedbrandId = $(this).val();
        $('#<%= hiddenBrandId.ClientID %>').val(selectedbrandId); // Set the hidden field
        console.log('Selected Brand ID:', selectedbrandId);
    });

    <%--function searchBrands() {
        var query = $('#<%= txtBrand.ClientID %>').val();
       
        if (query.length > 0) {
            $.ajax({
                type: "POST",
                url: "AdEntry.aspx/SearchBrands", // Update with your page's path
                data: JSON.stringify({ searchText: query }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var results = response.d;
                    $('#brandResults').empty();
                    if (results.length > 0) {
                        $.each(results, function (i, item) {
                            $('#brandResults').append('<div onclick="selectBrand(\'' + item.Id + '\', \'' + item.Brand_Name + '\')">' + item.Brand_Name + '</div>');
                        });
                        $('#brandResults').show();
                    } else {
                        $('#brandResults').hide();
                    }
                }
            });
        } else {
            $('#brandResults').hide();
        }
    }
    
    function selectBrand(id, name) {
        $('#<%= txtBrand.ClientID %>').val(name);
        $('#<%= hiddenBrandId.ClientID %>').val(id);

        console.log('Hidden Client ID:', $('#<%= hiddenBrandId.ClientID %>').val());

        $('#brandResults').hide();
    }--%>

    function searchClients() {

        var query = $('#<%= txtClient.ClientID %>').val();
        if (query.length > 0) {
            $.ajax({
                type: "POST",
                url: "AdEntry.aspx/SearchClients",
                data: JSON.stringify({ searchText: query }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var results = response.d;
                    $('#clientResults').empty();
                    if (results.length > 0) {
                        $.each(results, function (i, item) {
                            $('#clientResults').append('<div class="client-result-item" onclick="selectClient(\'' + item.Id + '\', \'' + item.Client_Name + '\')">' + item.Client_Name + '</div>');
                        });
                       
                        $('#clientResults').show();
                    } else {
                        $('#clientResults').hide();
                    }
                }
            });
        } else {
            $('#clientResults').hide();
        }
    }

   
    function selectClient(id, name) {
        $('#<%= txtClient.ClientID %>').val(name);
        $('#<%= hiddenClientId.ClientID %>').val(id);

        console.log('Hidden Client ID:', $('#<%= hiddenClientId.ClientID %>').val());
        $('#clientResults').hide();

        console.log('Before PostBack Hidden Client ID:', $('#<%= hiddenClientId.ClientID %>').val());
        PageMethods.PopulateMainCatDropdown(id, onSuccess, onError);

        // Call PopulateClientCity
        $.ajax({
            type: "POST",
            url: "AdEntry.aspx/PopulateClientCity",
            data: JSON.stringify({ clientId: id }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                $('#<%= lblclientcity.ClientID %>').text(response.d); 
            },
            error: function (error) {
                console.log('Error:', error);
            }
        });

        //  Call populateBrandDropdown immediately after selecting a client
        populateBrandDropdown(id);

        // Make input read-only after selection
        $('#<%= txtClient.ClientID %>').prop('disabled', true);

    }

    $(document).ready(function () {
        const $input = $('#<%= txtClient.ClientID %>');

        $input.on('keyup', function (e) {
            if (!['ArrowUp', 'ArrowDown', 'Enter'].includes(e.key)) {
                searchClients(); // Trigger search only for character keys
            }
        });

        $input.on('keydown', function (e) {
            var $items = $('#clientResults .client-result-item');
            var $highlighted = $items.filter('.highlight');
            var index = $items.index($highlighted);

            if (e.key === 'ArrowDown') {
                e.preventDefault();
                if (index < $items.length - 1) {
                    $highlighted.removeClass('highlight');
                    $items.eq(index + 1).addClass('highlight')[0].scrollIntoView({ block: 'nearest' });
                } else if (index === -1 && $items.length > 0) {
                    $items.eq(0).addClass('highlight')[0].scrollIntoView({ block: 'nearest' });
                }
            } else if (e.key === 'ArrowUp') {
                e.preventDefault();
                if (index > 0) {
                    $highlighted.removeClass('highlight');
                    $items.eq(index - 1).addClass('highlight')[0].scrollIntoView({ block: 'nearest' });
                }
            } else if (e.key === 'Enter') {
                e.preventDefault();
                if ($highlighted.length > 0) {
                    var id = $highlighted.attr('onclick').match(/'([^']+)'/)[1];
                    var name = $highlighted.text();
                    selectClient(id, name);
                }
            }
        });

        var clientId = $('#<%= hiddenClientId.ClientID %>').val();
        var mainCatId = $('#<%= hdnSelectedMainCat.ClientID %>').val();
        var subCatId = $('#<%= hiddenSubCatId.ClientID %>').val();
        var selectedBrandId = $('#<%= hiddenBrandId.ClientID %>').val();
        var selectedTypeId = $('#<%= hiddenTypeId.ClientID %>').val();
        var isMisc = $('#<%= btnMisc.ClientID %>').is(':checked');

        if (clientId && mainCatId && subCatId) {
            populateClientMainCatSubcatAndCity(clientId, mainCatId, subCatId);
        }
        if (clientId) {
            if (isMisc) {
                populateTypeDropdown(selectedTypeId);
                $('#<%= ddltype.ClientID %>').prop('disabled', false);
            $('#<%= ddlBrand.ClientID %>').prop('disabled', true);
        } else {
            populateBrandDropdown(clientId, selectedBrandId);
            $('#<%= ddlBrand.ClientID %>').prop('disabled', false);
                 $('#<%= ddltype.ClientID %>').prop('disabled', true);
             }
         }
    });

    function onSuccess(result) {
        console.log('onSuccess called with result:', result);
        
        var ddlmaincat = $('#<%= ddlmaincat.ClientID %>');

        ddlmaincat.empty();

        if (result && result.length > 0) {
            $.each(result, function (index, item) {
                ddlmaincat.append($('<option>', { value: item.MainCategoryId, text: item.CategoryTitle }));
            });
            console.log("Selected value after populating: ", ddlmaincat.val());
            
            ddlmaincat.val(result[0].MainCategoryId);  
            $('#<%= hdnSelectedMainCat.ClientID %>').val(result[0].MainCategoryId);


        } else {
            ddlmaincat.append($('<option>', { value: '', text: 'No categories available' }));
        }

        ddlmaincat.prop('disabled', true);
                
        console.log('Calling populateSubCategories after main category dropdown is populated.');
        if (ddlmaincat.val()) {
            console.log("Main Category ID before calling populateSubCategories:", ddlmaincat.val());
            populateinsertSubCategories();
        }

        console.log('Binding change event after dropdown population.');
        if (result.length > 0 && (result[0].MainCategoryId == 1 || result[0].MainCategoryId == 20000001)) {
            $('#<%= txtidro.ClientID %>').prop('disabled', false); 
        }
       
        else {
                $('#<%= txtidro.ClientID %>').prop('disabled', true);
        }
    }

    function onError(error) {
        console.log('Error:', error);
    }
    function populateinsertSubCategories() {
        var selectedMainCatId = $('#<%= ddlmaincat.ClientID %>').val();
        console.log("Selected Main Category ID: " + selectedMainCatId); 

        if (selectedMainCatId) {
            $.ajax({
                type: "POST",
                url: "AdEntry.aspx/PopulateSubCategories",
                data: JSON.stringify({ mainCategoryId: selectedMainCatId }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var results = response.d;
                    var ddlsubcat = $('#<%= ddlsubcat.ClientID %>');
                    ddlsubcat.empty(); // Clear existing items
                    ddlsubcat.append('<option value="">Select Sub Category</option>');
                if (results.length > 0) {
                    $.each(results, function (index, item) {
                        ddlsubcat.append($('<option>', { value: item.Id, text: item.CategoryTitle }));
                    });
                    ddlsubcat.prop('disabled', false); // Enable ddlsubcat
                } else {
                    ddlsubcat.prop('disabled', true); // Disable ddlsubcat if no results
                }
            },
                error: function (error) {
                    console.log('Error:', error);
                }
            });
        }
        else {
            $('#<%= ddlsubcat.ClientID %>').empty().prop('disabled', true); // Clear and disable if no main category selected
        }
    }
    console.log('Hidden Sub Category ID before postback:', $('#<%= hiddenSubCatId.ClientID %>').val());

    $(document).on('change', '#<%= ddlsubcat.ClientID %>', function () {
    var selectedSubCatId = $(this).val();
       $('#<%= hiddenSubCatId.ClientID %>').val(selectedSubCatId); // Set the hidden field
       console.log('Selected Subcategory ID:', selectedSubCatId);
   });

    function populateClientMainCatSubcatAndCity(clientId, mainCategoryId, subCategoryId) {
        // Fetch and set the client name in txtClient
        $.ajax({
            type: "POST",
            url: "AdEntry.aspx/PopulateClientDetails",
            data: JSON.stringify({ clientId: clientId }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                console.log(response);
                $('#<%= txtClient.ClientID %>').val(response.d.ClientName);
                $('#<%= hiddenClientId.ClientID %>').val(clientId); 

                if (response.d.GroupCompName !== "No Group Comp Found") {
                    $('#<%= lblclientcity.ClientID %>').text(response.d.GroupCompName); // Set city correctly
                    
                } else {
                            $('#<%= lblclientcity.ClientID %>').text("No City Found");
                        }
            },
            error: function (error) {
                console.log("Error fetching client city:", error);
            }
        });

        // Fetch and populate main category dropdown
        PageMethods.PopulateMainCatDropdown(clientId, function (result) {
            var ddlmaincat = $('#<%= ddlmaincat.ClientID %>');
            ddlmaincat.empty();
            $.each(result, function (index, item) {
                ddlmaincat.append($('<option>', { value: item.MainCategoryId, text: item.CategoryTitle }));
            });
            ddlmaincat.val(mainCategoryId); // Set selected main category

           //ddlmaincat.val(result[0].mainCategoryId);
            $('#<%= hdnSelectedMainCat.ClientID %>').val(mainCategoryId);

            ddlmaincat.prop('disabled', false);

            // After setting the main category, fetch and populate subcategories
            populateSubCategories(mainCategoryId, subCategoryId);
        });
    }

    function populateSubCategories(mainCategoryId, subCategoryId) {
        if (mainCategoryId) {
            $.ajax({
                type: "POST",
                url: "AdEntry.aspx/PopulateSubCategories",
                data: JSON.stringify({ mainCategoryId: mainCategoryId }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var ddlsubcat = $('#<%= ddlsubcat.ClientID %>');
                    ddlsubcat.empty(); // Clear existing items
                    $.each(response.d, function (index, item) {
                        ddlsubcat.append($('<option>', { value: item.Id, text: item.CategoryTitle }));
                    });
                    ddlsubcat.val(subCategoryId); // Set selected subcategory

                    console.log('Hidden Sub Category at fetch:', $('#<%= hiddenSubCatId.ClientID %>').val());
                    $('#<%= hiddenSubCatId.ClientID %>').val(subCategoryId);

                    $(document).on('change', '#<%= ddlsubcat.ClientID %>', function () {
                        var selectedSubCatId = $(this).val();
                        $('#<%= hiddenSubCatId.ClientID %>').val(selectedSubCatId); // Set the hidden field
                        console.log('Selected Subcategory ID:', selectedSubCatId);
                    });

                    ddlsubcat.prop('disabled', false);
                },
                error: function (error) {
                    console.log('Error:', error);
                }
            });
        }
    }

    function populatefetchbrand(clientId)
    {
        $.ajax({
            type: "POST",
            url: "AdEntry.aspx/PopulateBrandDetails",
            data: JSON.stringify({ clientId: clientId }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {

                console.log("Received brands:", response);

                var ddlBrand = $('#<%=ddlBrand.ClientID%>');
                console.log('Selected Brand ID:1');
                ddlBrand.empty();
                $.each(response.d, function (index, item) {
                    ddlBrand.append($('<option>', { value: item.Id, text: item.BrandName }));
                });

                ddlBrand.val(clientId);
                $('#<%= hiddenBrandId.ClientID %>').val(clientId);

                ddlBrand.prop('disabled', false);


            },
            error: function (error) {
                console.log("Error fetching client city:", error);
            }
        });

    }

    function populatefetchtype(subCategoryId) {
        //if (mainCategoryId) {
            $.ajax({
                    type: "POST",
                    url: "AdEntry.aspx/GetTypes",
                    data: JSON.stringify({ subCategoryId: subCategoryId }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        var ddltype = $('#<%= ddltype.ClientID %>');
                        ddltype.empty(); // Clear existing items
                    $.each(response.d, function (index, item) {
                        ddltype.append($('<option>', { value: item.Id, text: item.Type1 }));
                    });

                       ddltype.val(subCategoryId); // Set selected subcategory
                        $('#<%= hiddenTypeId.ClientID %>').val(subCategoryId); // Set the hidden field
                        ddltype.prop('disabled', false);

                        
                },
                error: function (error) {
                    console.log('Error:', error);
                }
            });
        //}
    }
    $(document).on('change', '#<%= ddltype.ClientID %>', function () {
    var selectedtypeId = $(this).val();
        $('#<%= hiddenTypeId.ClientID %>').val(selectedtypeId); // Set the hidden field
        console.log('Selected Type ID:', selectedtypeId);
    });

    



</script>
        <%--<asp:UpdateProgress ID="pu" runat="server">
            <ProgressTemplate>
                <div class="dialog-background">
                    <div class="dialog-loading-wrapper">
                        <img src="Content/Images/loading6.gif" />
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>--%>
         <asp:HiddenField ID="hdnSelectedMainCat" runat="server" />
        <div class="form-horizontal">
            <div class="col-md-10 col-md-offset-1">
                <div class="form-group">
                    <div class="div-md-12 text-center">
                        <asp:Label ID="lblmessage" runat="server" ForeColor="Red"></asp:Label>
                    </div>
                </div>
                <h4>Advertisments</h4>
                <br />
                <div class="form-group">
                    <div class="div-md-12">
                        <asp:Button ID="btnview" runat="server" Text="View/Update Record" CssClass="click" OnClick="btnview_Click" CausesValidation="false" />
                    </div>
                </div>
                <br />
                 <asp:HiddenField ID="hdnPubDate" runat="server" />
                <div class="form-group">
                    <div class="row">
                        <div class="col-md-1">
                            Publication Date
                        </div>
                        <div class="col-md-3">
                            <input id="txtpubdate" type="date" class="form-control" runat="server" style="font-size: small;" />

                        </div>
                        <div class="col-md-1"></div>
                        <div class="col-md-1">
                            City Edition
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlcity" runat="server" CssClass="form-control" Style="font-size: small;"></asp:DropDownList>
                        </div>
                    </div>
                </div>
               <%-- <br />--%>
                <div class="form-group">
                    <div class="row">
                        <div class="col-md-1">
                            Publication
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlpub" runat="server" CssClass="form-control" Style="font-size: small;"></asp:DropDownList>
                        </div>
                        <div class="col-md-1"></div>
                        <div class="col-md-1">
                            Page No
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlno" runat="server" CssClass="compact-dropdown" Style="font-size: small; width: 50px;"></asp:DropDownList>
                            &nbsp;&nbsp;<asp:CheckBox ID="chbackpage" runat="server" onclick="togglebackpage()"/>&nbsp;&nbsp;Back Page
                        </div>
                    </div>
                </div>
                <%--<br />--%>
                <div class="form-group">
                    <div class="row">
                        <div class="col-md-1">
                            Client Company
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtClient" runat="server"  CssClass="form-control" />
                            <div id="clientResults" style="
                                    display:none;
                                    max-height: 500px;
                                    overflow-y: auto;
                                    border: 1px solid #ccc;
                                    border-radius: 4px;
                                    background-color: lightblue;
                                    z-index: 1000;
                                    position: absolute;
                                    width: 100%;
                                    box-shadow: 0 2px 6px rgba(0,0,0,0.2);"></div>
                            <asp:HiddenField ID="hiddenClientId" runat="server" EnableViewState="true" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                ControlToValidate="txtClient" ErrorMessage="* Enter Client Name" ForeColor="Red"></asp:RequiredFieldValidator>

                        </div>
                        <div class="col-md-1"></div>
                        <div class="col-md-1">
                        </div>
                        <div class="col-md-3">
                            <label id="lblclientcity" runat="server" style="font-weight: bold; font-size: large; border: 1px solid #000; padding: 5px; display: inline-block; color:red; width:600%; text-align:center;"></label>
                        </div>
                    </div>
                </div>
                <%--<br />--%>
               <%-- <asp:HiddenField ID="hdnSelectedMainCat" runat="server" />--%>
                <div class="form-group">
                    <div class="row">
                        <div class="col-md-1">
                            Main Category
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlmaincat" runat="server" CssClass="form-control" Style="font-size: small; font-weight:bold; border: 1px solid #000; padding: 5px; display: inline-block;" AutoPostBack="false" EnableViewState="true"></asp:DropDownList>
                        </div>
                        <div class="col-md-1"></div>
                        <div class="col-md-1">
                            Sub Category
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlsubcat" runat="server" CssClass="form-control" Style="font-size: small;"></asp:DropDownList>
                            <asp:HiddenField ID="hiddenSubCatId" runat="server" EnableViewState="true" />
                            <asp:RequiredFieldValidator
                                ID="rfvSubCategory"
                                runat="server"
                                ControlToValidate="ddlsubcat"
                                InitialValue="0"
                                ErrorMessage="Please select a sub category."
                                CssClass="text-danger"
                                Display="Dynamic" />

                        </div>
                    </div>
                </div>
              <%--  <br />--%>

                <div class="form-group">
                    <div class="row">
                        <div class="col-md-1">
                            Type
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddltype" runat="server" CssClass="form-control" Style="font-size: small;"></asp:DropDownList>
                            &nbsp;&nbsp;<asp:CheckBox ID="btnMisc" runat="server" onclick="toggleMisc()"/>&nbsp;&nbsp;Miscellaneous
                           <asp:HiddenField ID="hiddenTypeId" runat="server" EnableViewState="true" />
                            
                        </div>
                        <div class="col-md-1"></div>
                        <div class="col-md-1">
                            Add Id(RO)
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtidro" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div class="form-group">
                    <div class="row">
                        <div class="col-md-1">
                            Brand
                        </div>
                        <div class="col-md-3">
                           
                            <%--<asp:TextBox ID="txtBrand" runat="server" onkeyup="searchBrands()" CssClass="form-control" />--%>
                             <asp:DropDownList ID="ddlBrand" runat="server" CssClass="form-control" Style="font-size: small;"></asp:DropDownList>
                            <div id="brandResults" style="display:none;"></div>
                            <asp:HiddenField ID="hiddenBrandId" runat="server" EnableViewState="true" />
                        </div>
                        <div class="col-md-1"></div>
                        <div class="col-md-1">
                            Agency
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlagency" runat="server" CssClass="form-control" Style="font-size: small;"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <%--<br />--%>
                <div class="form-group">
                    <div class="row">
                        <div class="col-md-1">
                            Campaign
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlcampaign" runat="server" CssClass="form-control" Style="font-size: small;"></asp:DropDownList>
                        </div>
                        <div class="col-md-1"></div>
                        <div class="col-md-1">
                            Color BW
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlcolor" runat="server" CssClass="form-control" Style="font-size: small;">
                                <asp:ListItem Text="Select Color" Value=""/>
                                <asp:ListItem Text="Four Color" Value="F" />
                                <asp:ListItem Text="Spot Color" Value="S" />
                                <asp:ListItem Text="Black & White" Value="B" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator2"
                                    runat="server"
                                    ControlToValidate="ddlcolor"
                                    InitialValue="0"
                                    ErrorMessage="Please select a Color."
                                    CssClass="text-danger"
                                    Display="Dynamic" />
                        </div>
                    </div>
                </div>
               <%-- <br />--%>
                <div class="form-group">
                    <div class="row">
                        <div class="col-md-1">Size CM</div>
                        <div class="col-md-1">
                            <asp:DropDownList ID="ddlcm" runat="server" CssClass="compact-dropdown" Style="font-size: small; width: 50px;"></asp:DropDownList>
                        </div>
                        <div class="col-md-1">Size Column</div>
                        <div class="col-md-2">
                            <asp:DropDownList ID="ddlcol" runat="server" CssClass="compact-dropdown" Style="font-size: small; width: 50px;"></asp:DropDownList>
                        </div>
                        <div class="col-md-1">Supplement</div>
                        <div class="col-md-1">
                            <asp:CheckBox ID="chsup" runat="server" />
                        </div>
                        <div class="col-md-1">FOC / FMG</div>
                        <div >
                            <asp:CheckBox ID="chfoc" runat="server" />
                        </div>
                        <%--<div class="col-md-1">
                            Size Col
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlcol" runat="server" CssClass="compact-dropdown" Style="font-size: small; width: 50px;"></asp:DropDownList>
                        </div>--%>
                    </div>
                </div>
               <%-- <br />--%>
                <%--<div class="form-group">
                    <div class="row">
                        <%--<div class="col-md-1">
                            Supplement
                        </div>
                        <div class="col-md-1">
                            <asp:CheckBox ID="chsup" runat="server" />
                        </div>
                        <div class="col-md-1">
                            FOC / FMG
                        </div>
                        <div class="col-md-3">
                            <asp:CheckBox ID="chfoc" runat="server" />
                        </div>--%>
                        <%--<div class="col-md-1"></div>--%>
                        <%--<div class="col-md-1">
                            FOC / FMG
                        </div>
                        <div class="col-md-3">
                            <asp:CheckBox ID="chfoc" runat="server" />
                        </div>--%>
                  <%--  </div>
                </div>--%>
                <br />

                <%--grid start--%>
                <div class="form-group">
                    <%--<div class="div-md-12">--%>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtCopyCount" runat="server" CssClass="form-control" placeholder="Enter number of copies" style="width:200px;"/>
                            <asp:Button ID="btnCopy" runat="server" Text="Copy" CssClass="btn btn-success" OnClick="btnCopy_Click"/>
                         </div>
                        <br />
                    <%--</div>--%>
                </div>

                
                <div class="form-group">
                    <div class="div-md-12">
                        <div class="col-md-2 col-md-offset-2">
                            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success" Text="Save" OnClick="btnSave_Click" />
                            <asp:Button ID="btnCancel" runat="server" ValidationGroup="c" CssClass="btn btn-dark" Text="Cancel" OnClick="btnCancel_Click" />
                        </div>

                        <br />
                        
                    </div>
                </div>

                <br />
                <br />
                <div class="form-group">
                    <div class="row">
                        <div class="col-md-1">
                            Publication Date
                        </div>
                        <div class="col-md-2">
                            <input id="filterpubdate" type="date" class="form-control" runat="server" style="font-size: small;" disabled="disabled"/>
                        </div>
                    </div>
                </div>

                <div class="div-md-12" style="margin-top: 50px;">
                    <asp:GridView ID="gv" PageSize="25" runat="server" CssClass="EU_DataTable"  DataKeyNames="Id" AutoGenerateColumns="false"
                        AllowPaging="True" OnPageIndexChanging="gv_PageIndexChanging">
                        <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <Columns>
                            <asp:BoundField DataField="PubDate" HeaderText="Pub Date" DataFormatString="{0:yyyy-MM-dd}" />
                            <asp:BoundField DataField="Client" HeaderText="Client" />
                            <asp:BoundField DataField="CM" HeaderText="CM" />
                            <asp:BoundField DataField="COL" HeaderText="COL" />
                            <asp:BoundField DataField="Pub" HeaderText="Pub" />
                            <asp:BoundField DataField="Main_Cat" HeaderText="Main_Cat" />
                            
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:HyperLink ID="hypEdit" runat="server" Text="Edit" NavigateUrl='<%# "AdEntry.aspx?Id=" + Convert.ToString(DataBinder.Eval(Container.DataItem,"Id")) + "&Mode=Edit" %>'></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    Delete
                                </HeaderTemplate>
                                <HeaderStyle Width="40px" />
                                <ItemStyle Width="40px" />
                                <ItemTemplate>
                                    <center>
                                        <asp:ImageButton runat="server" ID="DelButton" ImageUrl="~/Content/images/Trash-Cancel.ico"
                                            CommandName="Delete" CommandArgument='<%# Eval("Id") %>' OnClick="DeleteButton_Click" ValidationGroup="a" />
                                    </center>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>

                <%--grid end--%>
            </div>
        </div>
    </ContentTemplate>
<%--    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="chmiss" EventName="CheckedChanged" />
    </Triggers>
</asp:UpdatePanel>--%>
</asp:Content>
