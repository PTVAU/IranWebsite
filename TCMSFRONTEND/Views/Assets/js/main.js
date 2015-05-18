$(function () {
    
    // Search popover
    $('.search [data-toggle=popover]').popover({
	content: $("#search-form").html()
	, html: true
    }).on('shown.bs.popover', function () {
	$('#search-form-inner input:first').focus();
    });

    // Close popovers on focusout
    $(document).on('blur', '.popover', function () {
	$(this).popover('hide');
    });
    
    // Menu toggler for responsive
    var $menu = $("#menu");
    $("#menu-toggle").click(function(e) {
	if ($menu.hasClass("open")) {
	    $menu.slideUp().removeClass("open");
	} else {
	    $menu.slideDown().addClass("open");
	}
	e.preventDefault();
    });
    
    // Carousel
    $(".panel.showcase ul.items").owlCarousel({
	items: 1
	, loop: true
	, autoplay: true
	, autoplayTimeout: 3000
	, autoplayHoverPause: true
	, singleItem : true
	, autoHeight : true
	, animateOut: 'slideOutDown'
	, animateIn: 'flipInX'
    });
});