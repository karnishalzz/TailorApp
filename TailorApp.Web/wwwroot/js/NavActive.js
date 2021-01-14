
        $(document).ready(function() {
            $('li.active').removeClass('active');
            $('li.pcoded-hasmenu').removeClass('active');
            $('li.pcoded-hasmenu').removeClass('pcoded-trigger');
            $('a[href="' + location.pathname + '"]').closest('li.pcoded-hasmenu').addClass('active'); 
            $('a[href="' + location.pathname + '"]').closest('li.pcoded-hasmenu').addClass('pcoded-trigger'); 
            $('a[href="' + location.pathname + '"]').closest('li').addClass('active'); 
        });
   
    