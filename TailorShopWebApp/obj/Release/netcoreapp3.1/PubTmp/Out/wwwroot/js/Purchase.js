
$('#renderSupplierForm')
    .html("Loading...")
    .load('@Url.Action("_SupplierCreatePV", "Supplier")');

$('#addSupplier').on('click', function () {
    $.ajax({
        type: 'POST',
        dataType: 'json',
        url: '/Suppliers/Create', //url your posting to
        data: $('#FormAddSupplier').serialize(), //serialize the data in your form
        success: function (data) {
            $('#modal_supplier').modal('toggle');
            if (data == 'duplicate') {

                alert("Duplicate data!");
            }
            else {
                populateSupplier();
                alert("Success");
            }

        }
    });
    return false;
});



//Compare Selling price and cost price ...

$('#SP').on('blur', function () {
    var cp = Number($('#CP').val());
    var sp = Number($('#SP').val());

    if (sp <= cp) {
        alert('Selling Price cannot be lesser or equal to Cost Price');
        $('#SP').val('');
    }
});


//modal popoulate with data
function PopulateModalFields() {
    var subTotal = 0;
    var total = 0;
    $('#Amount').val('');
    $('#Discount').val('');
    $('#Tax').val('');
    $('#GrandTotal').val('');

    $('#mytable tr').each(function () {
        var qty = $.trim($(this).find(".tdQty").html());
        var cp = $.trim($(this).find(".tdCp").html());
        total = Number(qty) * Number(cp);
        subTotal += total;
    });

    $('#Amount').val(subTotal);
    $('#GrandTotal').val(subTotal);
}
//for auto calculate grandtotal with discount
function CalculateTotal() {
    var amount = Number($('#Amount').val());
    var discount = Number($('#Discount').val());
    var tax = Number($('#Tax').val());

    if (amount == null && discount == null) {
        $('#GrandTotal').val(amount);
    }

    var grandTotal = (amount - discount + tax).toFixed(2);
    $('#GrandTotal').val(grandTotal);
}

$(document).on('ready', function () {
    
   
    //for icheckbox
    $('.x').iCheck({
        checkboxClass: 'icheckbox_minimal',
        radioClass: 'iradio_minimal',
        increaseArea: '20%' // optional
    });


   
});

var purchaseItems = [];
$('#btnAdd').on('click', function () {

    //jQuery Validations
    var isValid = true;
  
    if (document.getElementById("SelectSupplier").selectedIndex == 0) {
        $('#SelectSupplier').siblings('span.error').text('Please select a Supplier');
        isValid = false;
    }
    else {
        $('#SelectSupplier').siblings('span.error').text('');
    }
    if (!($("#InvocingDate").val().trim() != '')) {
        $('#InvocingDate').siblings('span.error').text('Please Select A Valid Date');
        isValid = false;
    }
    else {
        $('#InvocingDate').siblings('span.error').text('');
    }
    if (document.getElementById("selectItem").selectedIndex == 0) {
        $('#selectItem').siblings('span.error').text('Please select a Item');
        isValid = false;
    }
    else {
        $('#selectItem').siblings('span.error').text('');
    }
    if (document.getElementById("selectCategoryType").selectedIndex == 0) {
        $('#selectCategoryType').siblings('span.error').text('Please select a category for this Purchase');
        isValid = false;
    }
    else {
        $('#selectCategoryType').siblings('span.error').text('');
    }

    if (!($("#Quantity").val().trim() != '' && (parseInt($('#Quantity').val()) || 0))) {
        $('#Quantity').siblings('span.error').text('Please Enter A Valid Quantity');
        isValid = false;
    }
    else {
        $('#Quantity').siblings('span.error').text('');
    }
    if (!($("#CP").val().trim() != '' && (parseInt($('#CP').val()) || 0))) {
        $('#CP').siblings('span.error').text('Please Enter A Valid Price');
        isValid = false;
    }
    else {
        $('#CP').siblings('span.error').text('');
    }
    if (!($("#SP").val().trim() != '' && (parseInt($('#SP').val()) || 0))) {
        $('#SP').siblings('span.error').text('Please Enter A Valid Price');
        isValid = false;
    }
    else {
        $('#SP').siblings('span.error').text('');
    }

  

    if (isValid) {
        purchaseItems.push({
            ItemID: $('#selectItem').val(),
            ItemName: $('#selectItem option:selected').text(),
            Quantity: $('#Quantity').val(),
            CostPrice: $('#CP').val(),
            SellingPrice: $('#SP').val(),
            Category: $("#selectCategoryType :selected").val()
        });
        GeneratedItemsTable();
        $('#selectItem').val('0').focus();
        $('#selectCategoryType').val('0').focus();
        $('#Quantity, #CP, #SP').val('');
    }
   
});

$('#btnSubmit').on('click', function () {
    var isValid = true;

    if (document.getElementById("SelectSupplier").selectedIndex == 0) {
        $('#SelectSupplier').siblings('span.error').text('Please select a Supplier');
        isValid = false;
    }
    else {
        $('#SelectSupplier').siblings('span.error').text('');
    }
    if (!($("#InvocingDate").val().trim() != '')) {
        $('#InvocingDate').siblings('span.error').text('Please Select A Valid Date');
        isValid = false;
    }
    else {
        $('#InvocingDate').siblings('span.error').text('');
    }
   

    if (!($("#Amount").val().trim() != '' && (parseInt($('#Amount').val()) || 0))) {
        $('#Amount').siblings('span.error').text('Invalid');
        isValid = false;
    }
    else {
        $('#Amount').siblings('span.error').text('');
    }
   
   
    if (!($("#Tax").val().trim() != '' && (parseInt($('#Tax').val()) ))) {
        $('#Tax').siblings('span.error').text('Please Enter A Valid Number');
        isValid = false;
    }
    else {
        $('#Tax').siblings('span.error').text('');
    }

    if (isValid) {
        var data = {
            Date: $('#InvocingDate').val().trim(),
            SupplierID: $('#SelectSupplier').val(),
            Amount: $('#Amount').val(),
            Discount: $('#Discount').val(),
            Tax: $('#Tax').val(),
            GrandTotal: $('#GrandTotal').val(),
            IsPaid: $('input[name="payment"]:checked').val(),
            Description: $('#Remarks').val(),
            PurchaseDetails: purchaseItems
        }
        $(this).val('Please wait...');


        //post data to server
        if (data != null) {

            $.ajax({
                type: "POST",
                url: "/PurchaseEntries/SavePurchaseEntry",
                data: data,

                success: function (data) {
                    if (data.data.status) {
                        window.location ="/Purchases/Index/"
                        
                        
                    }
                    else {
                        alert('Failed');
                    }
                     $('#submit').val('Save');
                },
                error: function () {
                    alert('Error. Please try again.');
                    $('#btnSubmit').val('Save');
                }
            });
        }
       
    }
});

function GeneratedItemsTable() {
    if (purchaseItems.length > 0) {
        var $table = $('<table id="mytable" class="table table-striped table-hover"/>');
        $table.append('<thead><tr style="background-color:rgb(201, 211, 218);"><th>Item</th><th>Qty</th><th>CP</th><th>SP</th><th>Delete</th></tr></thead>');
        var $tbody = $('<tbody/>');
        $.each(purchaseItems, function (i, val) {
            var $row = $('<tr/>');
            $row.append($('<td/>').html(val.ItemName));
            $row.append($('<td class="tdQty"/>').html(val.Quantity));
            $row.append($('<td class="tdCp"/>').html(val.CostPrice));
            $row.append($('<td/>').html(val.SellingPrice));
            $row.append($('<td/>').html('<a href=# onclick="removeItem(this)" ><span class="ti-trash"></span></a>'));
            $tbody.append($row);
        });
        $table.append($tbody);
        $('#orderItems').html($table);
    }
    else {
        alert("List is empty !");
    }
}

//removes record on clicking remove icon and associated array
function removeItem(obj) {
    var $index = $(obj).parent().parent()[0].sectionRowIndex;
    purchaseItems.splice($index, 1);
    $(obj).parent().parent().remove();
    GeneratedItemsTable();
}