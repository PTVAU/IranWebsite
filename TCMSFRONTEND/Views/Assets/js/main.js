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
    
    // Menu and its toggler for responsive
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

    // Panorama
    $("#iran360").on('click', "a", function (e) {
        var swf = $(this).find("img").attr('src').replace('.jpg', '.swf');
        $("#parorama-modal").find(".modal-body").html('<object width="600" height="400" data="' + swf + '"></object>');
        $("#parorama-modal").modal('show');
        e.preventDefault();
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

    var $itemMedia = $(".item-media");
    if ($itemMedia.find("figure").length > 1) {
        var itemCaousel = $itemMedia.find(".item-media-inner").owlCarousel({
            items: 1
	        , loop: true
	        , autoplay: false
	        , singleItem: true
	        , autoHeight: true
        });
        $itemMedia.find("a.next").click(function (e) {
            itemCaousel.trigger('next.owl.carousel');
            e.preventDefault();
        });
        $itemMedia.find("a.prev").click(function (e) {
            itemCaousel.trigger('prev.owl.carousel');
            e.preventDefault();
        });
    }

    var Periodicals = {
        init: function () {
            if (window.location.hostname.indexOf('localhost') === -1) {
                Periodicals.reload();
            }
        }
    , reload: function () {
        // Reload homepage
        setInterval(function () {
            var pathname = window.location.pathname;
            if (pathname == "/")
                location.reload();
        }, 30000);
    }
    };
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
setInterval(function () {
    var pathname = window.location.pathname;
    if (pathname == "/")
        location.reload();
}, 30000);