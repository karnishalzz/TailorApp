
    //On clicking table row
document.getElementById('maintable').onclick = function (event) {
        event = event || window.event;
        var target = event.target || event.srcElement;
        while (target && target.nodeName != 'TR') {
            target = target.parentElement;
        }
        var cells = target.cells;
        if (!cells.length || target.parentNode.nodeName == 'THEAD') {
            return;
        }
        //alert(cells[1].innerHTML);
        $(function () {
            $('#getStockID').val($.trim(cells[0].innerHTML));
            $('#getItem').val($.trim(cells[1].innerHTML));
            $('#getAvailability').val($.trim(cells[2].innerHTML));
            $('#getRate').val($.trim(cells[3].innerHTML));
        });
        //clears qty and amt field
        $('#getQty').val('');
        $('#getAmount').val('');
        //focuses cursor on Qty field
        document.getElementById('getQty').focus();
    }
function CalculateRate() {
    var available = Number($('#getAvailability').val());
    var quantity = Number($('#getQty').val());
    var rate = Number($('#getRate').val());
    if (quantity > available) {
        $('#getQty').val(available);
        quantity = available;
    }
    var amount = (quantity * rate).toFixed(2);
    $('#getAmount').val(amount);
}

    //Calculate amount based on input qty
    //$('#getQty').keyup(function () {
       
    //});

    //populates RHS list
    $('#saleButtonAdd').on('click', function () {
        //Validation : Check if salesID and Qty is null
        if ($('#getQty').val() == '' || $('#getStockID').val() == '' || $('#getQty').val() == 0 || !$.isNumeric($('#getQty').val())) {
            sweetAlert("Oops...", 'Looks like you forgot to enter quantity.', "error");
            //alert();
        }
        else {
            //check for duplication
            if (CheckStockDuplication($('#getStockID').val())) {
                sweetAlert("Redundant Record", 'This Stock has already been added. If you want to re-enter, please remove it form the list first!', "error");

            }
            else {
                var $table = $('#tblAppendHere');
                $table.append(
                    '<tr class="dynamicRows">' +
                    //gets sales ID for name , value (for form collection) but shows stock id in the list (for UX purpose)
                    '<td>'+ $('#getStockID').val() + '</td>' +
                    '<td><input type="hidden" class="txtStockID" name="StockID"  value= "' + $('#getStockID').val() + '"/>' + $('#getItem').val() + '</td>' +
                    '<td><input type="hidden" name="Qty"     value="' + $('#getQty').val() + '"  style="width:60px;"/>' + $('#getQty').val() + '</td>' +
                    '<td><input type="hidden" name="Rate"    value= "' + $('#getRate').val() + '"/>' + $('#getRate').val() + '</td>' +
                    '<td><input type="hidden" name="Amount" value="' + $('#getAmount').val() + '"  class="subAmt"/>' + $('#getAmount').val() + '</td>' +
                    //remove icon
                    '<td><a href="#" class="ti-trash" onclick="removeItem(this)"></a></td>'
                    + '</tr>'
                    );

                //cleans form
                $(function () {
                    $clear = '';
                    $('#getStockID').val('');
                    $('#getItem').val('');
                    $('#getQty').val('');
                    $('#getRate').val('');
                    $('#getAvailability').val('');
                    $('#getAmount').val('');
                });

                //calculate sub total
                update_total();
                return false;
            }
        }
    });

    //check if stock ID already exists in the list
    function CheckStockDuplication(stockid) {
        var flag = false;
        $('.dynamicRows').each(function () {
            if ($(this).find('.txtStockID').val() == stockid) {
                flag = true;
            }
        });
        return flag;
    };

    //calculate sub total amount
    function update_total() {
        var rows = $('.dynamicRows');
        var total = Number();

        $.each(rows, function (index, item) {
            total += Number($(this).children('td').eq(4).text());
        });
        $('.setTotal').val(total.toFixed(2));
        $('.setTotalText').text(total.toFixed(2));
    };

    //on clicking Next button
    $('#btnModalTrigger').click(function () {
        $('#discountPercent').val('');
        $('#discountAmount').val('0');
        $('#grandTotal').val('');
        $('#receivedAmt').val('');
        $('#returnAmt').val('');
    });

    //focuses cursor on discount txtbox on modal load
    $('#myModal').on('shown.bs.modal', function () {
        $('#discountPercent').focus();
    })

    //Calculate Discount
    $('#discountPercent').keyup(function () {
        var total = Number($('.setTotal').val());
        
        var tax = 0;
        var grandTotal = 0;

        var percent = Number($('#discountPercent').val());
        var discountAmount = ((percent / 100) * total).toFixed(2);
        if (tax != null || tax != "" ) {
             tax = Number($('#taxAmount').val());
        }
        if (isNaN(tax))
            grandTotal = total - discountAmount;
        else
            grandTotal = (total + tax) - discountAmount;
            
        $('#discountAmount').val(discountAmount);
        $('#grandTotal').val(grandTotal.toFixed(2));
    });
$('#taxAmount').keyup(function () {
    var total = Number($('.setTotal').val());
    var tax = Number($('#taxAmount').val());
    var percent = Number($('#discountPercent').val());
    var discountAmount = 0;
    if (percent != null || percent != "") {
         discountAmount = ((percent / 100) * total).toFixed(2);
    }
    var grandTotal = (total + tax) - discountAmount;
    $('#grandTotal').val(grandTotal.toFixed(2));
});

    //calculate return amount
    $('#receivedAmt').keyup(function () {
        var grandTotal = Number($('#grandTotal').val());
        var received = Number($('#receivedAmt').val());
        var returnAmt = (received - grandTotal).toFixed(2);
        $('#returnAmt').val(returnAmt);
    });

    //removes record on clicking remove icon
    function removeItem(obj) {
        $(obj).parent().parent().remove();
        //updatae total amount
        update_total();
};


    //on clicking checkout button
    $('#btnCheckOut').on('click', function () {
        //$('#btnCheckOut').removeAttr("disabled");
        //$("#btnCheckOut").attr("disabled", true);
       
        if (($('.setTotal').val() == "" || $('.setTotal').val() == NaN || $('.setTotal').val() == null) || $('#grandTotal').val() == "")
        {
            $("#discountPercent").css('border-color', 'red');
            
   
        }
        else {

            var formData = new Object();
            formData = $('#formSerialized').serialize();
            $("#btnCheckOut").attr("disabled", true);
            $("#btnCheckOut").html('Please wait ...');
            if (formData != null) {
                $.ajax({
                    type: "POST",
                    url: "/SalesEntries/SerializeFormData",
                    dataType: 'json',
                   // contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
                    data: formData,
                   

                    success: function (data) {
                        if (data!=null) {
                            $('#myModal').modal('hide');
                            window.location = "/Sales/Index/";
                        }
                        else {
                            $('#myModal').modal('hide');
                            swal("Some inputs may be missing!");
                        }
                    }
                });
            }
            return false;
        }      
        
    });


