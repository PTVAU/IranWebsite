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
	    if ($menu.hasClass("open"))
	        $menu.slideUp().removeClass("open");
	    else
	        $menu.slideDown().addClass("open");
	    e.preventDefault();
    });
    $menu.on('click', "a", function (e) {
        if ($("body").hasClass("_xs") || $("body").hasClass("_sm")) {
            if ($(this).parent().find(".child").length) {
                var $child = $(this).parent().find(".child");
                if ($child.hasClass("open"))
                    $child.slideUp().removeClass("open");
                else
                    $child.slideDown().addClass("open");
                e.preventDefault();
            }
        }
    });
    $(document).on('mouseenter', "#menu li", function (e) {
        if ($("body").hasClass("_md") || $("body").hasClass("_lg")) {
            if ($(this).find(".child").length) {
                var $child = $(this).find(".child");
                if (!$child.hasClass("open"))
                    $child.slideDown(function() { $child.addClass("open") });
                e.preventDefault();
            }
        }
    }).on('mouseleave', "#menu li", function (e) {
        if ($("body").hasClass("_md") || $("body").hasClass("_lg")) {
            if ($(this).find(".child").length) {
                var $child = $(this).find(".child");
                if ($child.hasClass("open"))
                    $child.slideUp(function() { $child.removeClass("open") });
                e.preventDefault();
            }
        }
    });
    
    // Carousel
    $(".panel.showcase ul.items").owlCarousel({
        items: 1
	    , loop: true
	    , autoplay: true
	    , autoplayTimeout: 3000
	    , autoplayHoverPause: true
	    , singleItem: true
	    , autoHeight: true
	    , animateOut: 'slideOutDown'
	    , animateIn: 'flipInX'
    });
});

$(document).ready(function () { // Change width value on page load
    responsive_resize();
});
$(window).resize(function () { // Change width value on user resize, after DOM
    responsive_resize();
});
function responsive_resize() {
    var current_width = $(window).width();
    if (current_width < 768) // XS
        $('body').addClass("_xs").removeClass("_sm").removeClass("_md").removeClass("_lg");
    else if (current_width > 767 && current_width < 992)
        $('body').addClass("_sm").removeClass("_xs").removeClass("_md").removeClass("_lg");
    else if (current_width > 991 && current_width < 1200)
        $('body').addClass("_md").removeClass("_xs").removeClass("_sm").removeClass("_lg");
    else if (current_width > 1199)
        $('body').addClass("_lg").removeClass("_xs").removeClass("_sm").removeClass("_md");
}