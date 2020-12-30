var decimal = /^[-+]?[0-9]+\.[0-9]+$/; 
$(function () {
    $("#add").click(function () {
        var isValid = true;

        if (document.getElementById("productsItems").selectedIndex == 0) {
            $('#productsItems').siblings('span.error').text('Please select item');
            isValid = false;
        }
        else {
            $('#productsItems').siblings('span.error').text('');
        }

        if (!($("#quantity").val().trim() != '' && (parseInt($('#quantity').val()) || 0))) {
            $('#quantity').siblings('span.error').text('Please enter quantity');
            isValid = false;
        }
        else {
            $('#quantity').siblings('span.error').text('');
        }

        if (!($("#price").val().trim() != '' && (parseFloat($('#price').val()) || 0))) {
            $('#price').siblings('span.error').text('Please enter price');
            isValid = false;
        }
        else {
            $('#price').siblings('span.error').text(' ');
      
        }
        if (!($("#paid").val().trim() != '' && (parseInt($('#paid').val()))>=0  )) {
            $('#paid').siblings('span.error').text('Please enter paid amount');
            isValid = false;
        }
        else {
            $('#paid').siblings('span.error').text('');
        }
        if (document.getElementById("customers").selectedIndex == 0) {
            $('#customers').siblings('span.error').text('Please select a customer');
            isValid = false;
        }
        else {
            $('#customers').siblings('span.error').text('');
        }
        if (!($("#date").val().trim() != '')) {
            $('#date').siblings('span.error').text('Please select a delivery date');
            isValid = false;
        }
        else {
            $('#date').siblings('span.error').text('');
        }
        

        if (isValid) {

            var total = parseInt($("#quantity").val()) * parseFloat($("#price").val());
            $("#total").val(total);

            var CategoryID = document.getElementById("productsItems").value;

            
            var $newRow = $("#MainRow").clone();
            var $subRow = $("#SubRow").clone();;//how to clone rows with values

            $('.productsItems', $newRow).val(CategoryID);

            $('#add', $newRow).addClass('remove').html('Remove').removeClass('btn-success').addClass('btn-danger');
           
            $('#productsItems, #quantity, #price, #paid', $newRow).attr('disabled', true);
        

            $("#productsItems, #quantity, #price,#paid, #total", $newRow).removeAttr("id");
            $("span.error", $newRow).remove();

            $("#OrderItems").append($newRow[0]);
            //var TD = document.createElement('td');
            //TD.innerHTML = '<input type="text" id="sub" value="" class="form - control" /> </td>';
            $newRow.append($subRow[0]);

            document.getElementById("productsItems").selectedIndex = 0;
            $("#price").val('');
            $("#quantity").val('');
            $("#total").val('');
            $("#paid").val('');
            RemoveCells();
           
        }
    });

    $("#OrderItems").on("click", ".remove", function () {
        $(this).parents("tr").remove();
       
    });

   

    //submit order
    $("#submit").click(function () {
        var isValid = true;

        var itemsList = [];
        var measurementList = [];
        var table = document.querySelector("#OrderItems");

        $("#MainRow", table).each(function () {
            var item = {
                CategoryID: $('select.productsItems', this).val(),
                Price: $('.price', this).val(),
                Quantity: $('.quantity', this).val(),
                TotalPrice: $('.total', this).val(),
                Paid: $('.paid', this).val(),
               
            }
            itemsList.push(item);
            var row = [];
            var x = $('#SubRow', this)
            var y = x[0].cells.length;
            if (y != 0) {
                for (var c = 0; c < y; c++) {
                    var text = x[0].cells[c].childNodes[1].id;
                    var value = x[0].cells[c].childNodes[1].value;
                    
                    if (value != "") {
                        row.push({
                            Id: text,
                            Name: value
                        });
                    }
                    else {
                        row.push({
                            Id: "",
                            Name: 0
                        });
                    }
                }
            }
            else {
                row.push({
                    Id: "",
                    Name: 0
                });
            }
               
            
            measurementList.push(row);
            console.log(measurementList);
            
        });
        
        
        if (itemsList.length == 0) {
            $('#orderMessage').text('Please add items !');
            isValid = false;
        }

        if (isValid) {
            var data = new Object();
                data.Date = $("#date").val(),
                data.CustomerID = $("#customers :selected").val(),
                data.Items = itemsList
                data.ListOfMeasurement = measurementList
           

            $("#submit").attr("disabled", true);
            $("#submit").html('Please wait ...');
            if (data != null) {
              
                $.ajax({
                    type: "POST",
                    url: "/Orders/AddOrderAndOrderDetials",
                    data: data,
                    
                    success: function (data) {
                        if (data.data.status) {
                            window.location = '/Orders/ViewOrder/';
                        }
                        else {
                            $('#orderMessage').text(data.data.message);
                            $("#submit").attr("disabled", false);
                            $("#submit").html('Submit');
                        }
                    }
                });
            }
        }
    });
});


//dropdown
$(document).ready(function () {

    $('#productsItems').change(function () {

        RemoveCells();
        if (this.value >0) {
            var selectedItemValue = $(this).val();
            $.ajax({
                cache: false,
                type: "GET",
                url: "/Orders/GetMeasurementsByCategoryId/",
                data: { "id": selectedItemValue },
                success: function (data) {
                    var i = 0;
                    $.each(data.result, function (k, v) {

                        $('#SubRow').append('<td>' + v.name + ' <input type="text" id="' + v.measurementID+'" value="" class="form-control" /> </td>');
                        $('#SubRow').show();
                        i++;
                    });
                  
             
                  
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert('Found error to load product!!.');
                }
            });
           
        } else {
          
            $('#SubRow').hide();
        }

    });
});




function RemoveCells() {
    var x = document.getElementById("tblItems").rows[2].cells.length;
    var i = 0;
    for (; i < x; i++) {
        document.getElementById("SubRow").deleteCell(-1);

    }
}
