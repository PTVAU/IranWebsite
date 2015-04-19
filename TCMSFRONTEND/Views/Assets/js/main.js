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