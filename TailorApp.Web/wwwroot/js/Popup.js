$(document).ready(function () {
    var placeHolderElemet = $('#placeHolder');
    $('a[data-toggle="ajax-modal"]').click(function (event) {
       
        var url = $(this).data('url');
        var decodeUrl = decodeURIComponent(url);
        $.get(decodeUrl).done(function (data) {
            placeHolderElemet.html(data);
            placeHolderElemet.find('.modal').modal('show');
        });
    });
});
//$(document).ready(function () {
//    var placeHolderElemet = $('#placeHolder');
//    placeHolderElemet.on('click', '[data-save="modal"]', function (event) {
//        $(this).prop('disabled', true);

//    })
//});
$(document).ready(function () {
    var placeHolderElemet = $('#placeHolder');
    placeHolderElemet.on('click', '[data-save="modal"]', function (event) {
    event.preventDefault();

    var form = $(this).parents('.modal').find('form');
    var actionUrl = form.attr('action');
    var dataToSend = form.serialize();

    $.post(actionUrl, dataToSend).done(function (data) {
        var newBody = $('.modal-body', data);
     
        
        var x = newBody.find('input#IsValid').val();
  
        if (x != "False") {
            placeHolderElemet.find('.modal').modal('hide');
            location.reload();
        }
        placeHolderElemet.find('.modal-body').replaceWith(newBody);
    });
});
});