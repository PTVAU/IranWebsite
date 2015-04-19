$(function() {
    // Init
    Periodicals.init();
    Tooltip.init(); // Initializing Tooltip
    Ticker.init(); // Ticker initiation
    Item.init(); // item methods initialization
    Calendar.init(); // Calendar for schedule page
    Poll.init();
    Slider.init();
    Carousel.init();
    Slideshow.init();
    Itemlist.init();
    PanelRequests.init();
    Live.init();
    Frequencies.init();
    Comments.init();
    Search.init();
    Select.init();

    $(window).scroll(function () {
        if ($(window).scrollTop() > 300) {
            $(".totop").stop().fadeIn('slow');
        } else {
            $(".totop").stop().fadeOut('slow');
        }
    });
    $(".totop a").click(function (e) {
        $("html, body").animate({ "scrollTop": 0 }, 700);
        e.preventDefault();
    });
    $(".filter-scroll").on('click', "a", function (e) {
        $('html, body').animate({ scrollTop: $("#" + this.href.split("#")[1]).offset().top }, 'slow');
        e.preventDefault();
    });
    if ($(".nano").length) {
        $(".nano").nanoScroller({ flash: true, preventPageScrolling: true, tabIndex: 0 });
        $(".panel.timeline select").on('change', function () {
            var selected = $(this).find("option:selected").val();
            $("#timeline-inner-content > div").hide(1);
            $("#timeline-inner-content").find(".item-" + selected).show(1);
        });
        $(".enlarge-map").on('click', function (e) {
            $(".backdrop").fadeIn(300);
            $(".map").delay(150).fadeIn(300);
            e.preventDefault();
        });
        $(".map .close, .backdrop").on('click', function (e) {
            $(".backdrop").fadeOut(300);
            $(".map").fadeOut(300);
            e.preventDefault();
        });
    }
    $("#menu-toggle").click(function (e) {
        if ($("body").hasClass("_xs") || $("body").hasClass("_sm")) {
            if ($("#menu ul").is(':visible')) {
                $("#menu ul").slideUp();
            } else {
                $("#menu ul").slideDown();
            }
            e.preventDefault();
        }
    });
    $("#menu .haschild > a").on('click', function (e) {
        if ($("body").hasClass("_xs") || $("body").hasClass("_sm")) {
            var $child = $(this).parent().find(".child");
            if ($child.is(':visible')) {
                $child.slideUp();
            } else {
                $child.slideDown();
            }
            e.preventDefault();
        }
    });
    responsive_resize();
});
$(window).resize(function () { // Change width value on user resize, after DOM
    responsive_resize();
});
function responsive_resize() {
    var current_width = $(window).width();
    if (current_width < 768) {
        $('body').addClass("_xs").removeClass("_sm").removeClass("_md").removeClass("_lg"); // XS
    } else if (current_width > 767 && current_width < 992) {
        $('body').addClass("_sm").removeClass("_xs").removeClass("_md").removeClass("_lg"); // SM
    } else if (current_width > 991 && current_width < 1200) {
        $('body').addClass("_md").removeClass("_xs").removeClass("_sm").removeClass("_lg"); // MD
    } else if (current_width > 1199) {
        $('body').addClass("_lg").removeClass("_xs").removeClass("_sm").removeClass("_md"); // LG
    }
}
// Objects
var Periodicals = {
    init: function () {
        if (window.location.hostname.indexOf('localhost') !== -1)
            Periodicals.reload();
    }
    , reload: function () {
        // Reload homepage
        setInterval(function () {
            var pathname = window.location.pathname;
            if (pathname == "/")
                location.reload();
        }, 120000);
    }
}
var Search = {
    init: function () {
        var $item = $("#menu li.search");
        $item.on('click', "a", function (e) {
            if (!$item.hasClass("active")) {
                $(".search-form").animate({ 'width': 196 }, function () {
                    $item.addClass('active');
                    $(".search-form").find("input").focus();
                });
                e.preventDefault();
            }
        });
        $(".search-form").find("input").focusout(function () {
            $(".search-form").animate({ 'width': '0' }, function () {
                $item.removeClass('active');
            });
        });
    }
};
var Item = {
    init: function() {
        Item.Media.slideshow();
        Item.setFontSize();
        Item.fontResizer();
    }
    , Media: {
        slideshow: function() {
            $(".item-media").each(function() {
                if ($(this).find("ul").length) {
                    $slideshow = $(this).find("ul");
                    if ($slideshow.find("li").length > 1) {
                        $slideshow.carouFredSel({
                            auto: false
                            , items: 1
                            , scroll: {
                                fx: 'crossfade'
                                , items: 1
                            }
                            , next: $slideshow.parent().find(".next")
                            , prev: $slideshow.parent().find(".prev")
                        });
                    } else {
                        $(".item-media .carousel-control").addClass("hide");
                    }
                }
            });
        }
    }
    , fontResizer: function () {
        if ($(".font-resizer").length) {
            $(".font-resizer").on('click', "a", function (e) {
                var fontSize = parseInt($("#item p:first").css('font-size'));
                var oFontSize = parseInt(Item.getFontSize());
                switch ($(this).attr('class')) {
                    case 'larger':
                        if (fontSize < 23)
                            $("#item p").css({ 'font-size': fontSize + 2 });
                        break;
                    case 'reset':
                        $("#item p").css({ 'font-size': oFontSize });
                        break;
                    case 'smaller':
                        if (fontSize > 5)
                            $("#item p").css({ 'font-size': fontSize - 2 });
                        break;
                }
                e.preventDefault();
            });
        }
    }
    , setFontSize: function (size) {
        if ($(".font-resizer").length) {
            if (typeof size === "undefined" || size === '') {
                size = Item.getFontSize();
                if (typeof size === "undefined" || size === '') {
                    var size = $("#item p:first").css('font-size');
                    $("#item").attr('data-font-size', size);
                }
            }
        }
    }
    , getFontSize: function () {
        return $("#item").attr('data-font-size');
    }
};
var Frequencies = {
    init: function () {
        if ($("#frequencies").length) {
            $("#frequencies").delegate(".items-tags a", 'click', function (e) {
                var $overlay = $("#frequencies").find("img.overlay");
                $("#frequencies").find(".items-tags li").removeClass("active");
                $(this).parent().addClass("active");
                console.log($(this).attr("data-src"));
                $overlay.attr('src', $(this).attr("data-src"));
                e.preventDefault();
            });
        }
    }
};
var Comments = {
    init: function () {
        if ($(".item-comments form").length) {
            $(".comment-form-body").on('submit', function (e) {
                Comments.submit($(this), e);
                return false;
            });
            $(".comment-reply ").delegate("a", 'click', function (e) {
                Comments.reply($(this), e);
            });
            $(".cancel-reply").on('click', function (e) {
                Comments.cancelReply(e);
            });
            $("a.vote").on('click', function (e) {
                Comments.vote($(this), e);
            });
        }
    }
    , submit: function ($form, e) {
        $.ajax({
            url: $form.attr('action')
            , type: 'post'
            , data: $form.serialize()
            , success: function (d) {
                $("#comment-alert").slideToggle('fast', function () {
                    setTimeout(function () {
                        $("#comment-alert").slideToggle('fast');
                    }, 5000);
                });
                $("#comment-form").find("input[name=Parent_Id]").val(0);
                $form.find("textarea").val('');
            }
        });
        
    }
    , vote: function ($a, e) {
        // TODO: set and check cookie
        var params  = 'CommentId=' + $a.parents("li:first").attr('data-id') + '&Up=';
            params += ($a.attr('data-type') === "up") ? 1 : 0;
        $.ajax({
            url: '/callback.asmx/CommentsVote'
            , data: params
            , success: function (d) {
                Comments.updateVotes($a, d);
            }
        });
        e.preventDefault();
    }
    , updateVotes: function ($a, vote) {
        $a.parent().find(".rate-count").text(vote);
        return true;
    }
    , load: function() {
        // TODO
    }
    , reply: function ($a, e) {
        $("#comment-form").find("input[name=Parent_Id]").val($a.attr('data-id'));
        $(".active-reply").slideToggle('fast').find("strong").text($a.parents("li:first").find("span.name:first").text());
        e.preventDefault();
    }
    , cancelReply: function (e) {
        $("#comment-form").find("input[name=Parent_Id]").val(0);
        $(".active-reply").slideUp('fast').find("strong").text('');
        e.preventDefault();
    }
}
var Select = {
    init: function () {
        $("[data-type=select]").select();
        Select.initWeatherSelect();
    }
    , initWeatherSelect: function() {
        $(document).on('change', "#weather [data-type=select]", function () {
            var i = parseInt($(this).find("option:selected").val());
            var $wo = $("#weather > .inner > div");
            var wi = $(this).find("option:selected").index();
            $wo.not(".choose-location").fadeOut().addClass("hide");
            $wo.eq(wi).removeClass('hide').fadeIn();
        }).on('focusout', $("#weather > .inner > div .choose-location"), function () {
            //alert();
        })
    }
}
$.fn.select = function (options) {
    return this.each(function () {
        var $select = $(this);
        var o = { attribs: { id: $(this).attr("id"), name: $(this).attr("name"), placeholder: $(this).attr("data-placeholder") } };
        var items = '';
        var selectTmpl = '<div class="select-box" data-id="#{id}" data-name="{name}"><a class="select-handler" data-bind="#{id}">{placeholder}</a><ul class="items" style="display: none;">{items}</ul></div>';
        var selectsOptionTmpl = '<li data-val="{val}"><a href="#">{title}</a></li>';
        $.each($select.find("option"), function (i, item) {
            items += selectsOptionTmpl.replace(/{val}/g, $(this).attr('value')).replace(/{title}/g, $(this).text());
        });
        var $html = selectTmpl.replace(/{id}/g, o.attribs.id).replace(/{name}/g, o.attribs.name).replace(/{items}/g, items).replace(/{placeholder}/g, o.attribs.placeholder);
        if ($select.children().length) {
            $select.before($html);
            $select.css({ 'display': 'none' });

            $(document).on('click', ".select-handler", function (e) {
                var $list = $(this).parent().find("ul");
                if (!$list.hasClass("open")) {
                    $list.addClass('open').show();
                } else {
                    $list.removeClass('open').hide();
                }
                e.preventDefault();
            }).mouseup(function (e) {
                var $list = $("#weather > .inner > .choose-location");
                if (!$list.is(e.target) // if the target of the click isn't the container...
                    && $list.has(e.target).length === 0) // ... nor a descendant of the container
                {
                    var $list = $list.find("ul");
                    if ($list.hasClass('open')) {
                        $list.removeClass('open').hide();
                    }
                }
            });
            $(document).on('click', ".select-box .items a", function (e) {
                var $val = $(this).parent().attr('data-val');
                var $select = $(this).parents(".select-box").parent().find("select:first");
                $select.val($val).change();
                e.preventDefault();
            });
        }
    });
};
var Live = {
    init: function () {
        /*
        if ($("#live-page").length) {
            if (navigator.userAgent.match(/iPhone/i) || navigator.userAgent.match(/iPod/i) || navigator.userAgent.match(/Android/i)) {
                Live.playerHtml5();
            } else {
                Live.playerFlash();
            }
            $(".live-links").delegate("a", 'click', function (e) {
                switch($(this).attr('data-type')) {
                    case 'html5':
                        Live.clearMediaplayer();
                        Live.playerHtml5();
                        break;
                    case 'flash':
                        Live.clearMediaplayer();
                        Live.playerFlash();
                        break;
                }
            });
        }
        */
    }
    , clearMediaplayer: function () {
        $("#mediaplayer").empty();
        return true;
    }
    , playerHtml5: function () {
        $(".live-links").find("li").removeClass("active");
        $(".live-links").find("a[data-type=html5]").parent().addClass("active");
        var hlsUrl = $("#live-page").attr("data-hls-url");
        var html = '<video class="img" controls autoplay poster="' + $('#inpPlayback').attr("data-placeholder") + '"><source src="' + hlsUrl + '" type="application/x-mpegURL"></video>'
        $("#mediaplayer").html(html);
    }
    , playerFlash: function () {
        $(".live-links").find("li").removeClass("active");
        $(".live-links").find("a[data-type=flash]").parent().addClass("active");
        jwplayer("mediaplayer").setup({
            playlist: [{
                image: $('#inpPlayback').attr("data-placeholder")
                , sources: [{
                    file: "/Views/Assets/player/live.smil"
                }]
            }],
            height: 576,
            autostart: true,
            startparam: "start",
            width: '100%',
            primary: "flash",
            skin: "/Views/Assets/player/six.xml",
            stretching: "uniform"
        });
    }
}
var Carousel = {
    init: function () {
        if ($(".panel.carousel").length) {
            $(".panel.carousel").each(function () {
                var $carousel = $(this);
                Carousel.createCarousel($carousel);
            });
        }
    }
    , createCarousel: function ($carousel) {
        if (typeof $carousel !== "undefined") {
            $carousel.find("span.total").text($carousel.find("li.item").length);
            var $controls = $carousel.find(".carousel-control");
            var im = imagesLoaded($carousel, function () {
                $carousel.find("ul").carouFredSel({
                    items: 1
                    , auto: false
                    //, responsive: true
                    , next: $controls.find(".next")
                    , direction: 'up'
                    , prev: $controls.find(".prev")
                    , scroll: {
                        onBefore: function (d) {
                            var pos = $carousel.find("ul:first").triggerHandler("currentPosition");
                            $carousel.find("span.current").text(pos + 1);
                        }
                    }
                });
            });
        }
    }
}
var Slider = {
    init: function ($obj) {
        if (typeof $obj !== "undefined") {
            var $slider = $obj;
            window.setTimeout(function () {
                var im = imagesLoaded($slider, function () {
                    Slider.createSlider($slider);
                });
            }, 200);
        } else {
            if ($(".panel.slider").length) {
                $(".panel.slider").each(function () {
                    var $slider = $(this);
                    Slider.createSlider($slider);
                });
            }
        }
    }
    , createSlider: function ($slider) {
        if (typeof $slider !== "undefined") {
            var $controls = $slider.find(".carousel-control");
            var id = (typeof $slider.attr('id') !== "undefined") ? $slider.attr('id') : false;
            var count = $slider.hasClass("slider-wide") ? 4 : 3;
            var auto = (typeof $slider.find("ul").attr('data-auto') !== "undefined" && $slider.find("ul").attr('data-auto') === 'false') ? false : { items: 1, timeoutDuration: 6000, pauseOnHover: true };
            var im = imagesLoaded($slider, function () {
                $slider.find("ul").carouFredSel({
                    items: count
                    , auto: auto
                    , responsive: true
                    , scroll: {
                        items: 1
                        , onAfter: function (d) {
                            if (id) {
                                var $obj = $(this).find("li:first");
                                var params = {
                                    title: $obj.find("span.title").text()
                                    , introtext: $obj.find("p.introtext").text()
                                    , introtext: $obj.find("p.introtext").text()
                                    , categoryTitle: $obj.find(".section").text()
                                    , categoryLink: $obj.find(".section a").attr('href')
                                    , link: $obj.find(".inner > a").attr('href')
                                    , image: $obj.find(".inner > a img").attr('src').replace('_m.jpg', '_xl.jpg')
                                    , hasVideo: ($obj.find(".inner .img").hasClass('video')) ? true : false
                                }
                                Slider.changeTopNews(params);
                            }
                        }
                    }
                    , next: $controls.find(".next")
                    , prev: $controls.find(".prev")
                });
            });
        }
    }
    , changeTopNews: function (params) {
        var $topNews = $(".panel.top-news");
        $topNews.find("img").attr('src', params.image).attr('alt', params.title);
        if (params.hasVideo)
            $topNews.find(".img").addClass('video');
        else
            $topNews.find(".img").removeClass('video');
        $topNews.find("a").attr('href', params.link);
        $topNews.find("h2 a").text(params.title);
        $topNews.find("p").text(params.introtext);
        $topNews.find(".section a").attr('href', params.categoryLink).text(params.categoryTitle);
    }
};
var Slideshow = {
    init: function () {
        if ($("#slideshow .gallery").length) {
            var $gallery = $(".slideshow.gallery");
            $(".slideshow.gallery").find("span.total").text($gallery.find("li.item").length);
            $gallery.find("ul:first").carouFredSel({
                items: 1
                , next: $gallery.find(".next")
                , prev: $gallery.find(".prev")
                , auto: {
                    play: 7000
                }
                , responsive: true
                , scroll: {
                    fx: 'crossfade'
                    , onBefore: function (d) {
                        var pos = $gallery.find("ul:first").triggerHandler("currentPosition");
                        $(".slideshow.gallery").find("span.current").text(pos + 1);
                    }
                }
            });
        }
    }
}
var Itemlist = {
    init: function() {
        $(".load-more").on('click', ".more a", function (e) {
            Itemlist.prepareLoad();
            e.preventDefault();
        });
        if ($("#itemlist").length) {
            $(window).scroll(function () {
                if (!$("body").hasClass("_xs") && !$("body").hasClass("_sm")) {
                    if (parseInt($("#itemlist").attr('data-tries')) < 5) {
                        if ($(window).scrollTop() >= $(document).height() - $(window).height() - 100) {
                            Itemlist.prepareLoad();
                        }
                    }
                }
            });
        }
    }
    , prepareLoad: function(e) {
        var $obj = $("#itemlist");
        var params = JSON.parse($obj.attr('data-params'));
        var count = parseInt($obj.attr('data-count'));
        var tmpl = $obj.attr('data-template');
        if (parseInt($obj.attr('data-offset')) === 0)
            $obj.attr('data-offset', parseInt(params.count) + parseInt($obj.attr('data-offset')));
        var offset = parseInt($obj.attr('data-offset'));
        params.count = count; // change params count to custom number
        params.offset = offset;
        params.viewPath = tmpl; // 
        Itemlist.load(params, $obj);
        if (typeof e !== "undefined")
            e.preventDefault();
    }
    , load: function (params, $obj) {
        $obj.attr('data-offset', parseInt($obj.attr('data-offset')) + params.count);
        if (!$obj.hasClass("loading")) {
            $obj.addClass("loading");
            $.ajax({
                url: '/callback.asmx/ContentsList'
                , data: decodeURIComponent($.param(params))
                , success: function (d) {
                    $obj.find(".itemlist .panel-body > ul").append(d);
                    var tries = (typeof $obj.attr('data-tries') === "undefined") ? 0 : parseInt($obj.attr('data-tries'));
                    $obj.attr('data-tries', tries + 1);
                    $obj.removeClass("loading");
                }
            });
        }
    }
};
var Ticker = {
    titles: { 'breaking': 'Último Momento', 'latest': 'Síganos En' }
    , init: function () {
        if ($("#ticker .titles ul").length) {
            if ($("#ticker .titles ul").find("li").length > 1) {
                setInterval(function () {
                    Ticker.tick();
                }, 3000);
            }
        }
    }
    , tick: function () {
        $("#ticker .titles ul").each(function () {
            var $ticker = $(this);
            $ticker.find('li:first').slideUp(function () {
                if ($ticker.find('li').eq(1).hasClass('breaking')) {
                    $("#ticker").find(".header").addClass('breaking').find("span").text(Ticker.titles.breaking);
                } else {
                    $("#ticker").find(".header").removeClass('breaking').find("span").text(Ticker.titles.latest);
                }
                $(this).appendTo($ticker).slideDown();
            });
        });
    }
};
var PanelRequests = {
    popularUrl: '/callback.asmx/ContentsMostViewed?range={range}'
    , init: function () {
        $(".panel.item-pick").on('click', ".categories a", function (e) {
            var $this = $(this);
            PanelRequests.prepareItemPicker($this);
            e.preventDefault();
        });
        $(".panel.title-tabs.itemlist").on('click', ".nav-tabs a", function (e) {
            var $this = $(this);
            if (typeof $this.attr("data-type") === "undefined") {
                PanelRequests.prepareLoadLatest($this);
            } else {
                if ($this.attr("data-type") === "popular")
                    PanelRequests.loadPopular($this);
            }
            e.preventDefault();
        });
        $(".panel.tabs.has-request").on('click', ".nav-tabs a", function (e) {
            var $this = $(this);
            PanelRequests.prepareLoadMediaItems($this);
            e.preventDefault();
        });
        $(".sub-tabs li").on('click', "a", function (e) {
            $(".sub-tabs li").removeClass('active');
            $(this).parents().addClass('active');
            $(".sub-tab-content > div").fadeOut('fast');
            if ($($(this).attr('href')).find("ul").html() === "") {
                PanelRequests.loadPopular($(this), $($(this).attr('href')), true);
            } else {
                $(this), $($(this).attr('href')).fadeIn();
            }
            e.preventDefault();
        });
    }
    , prepareLoadMediaItems: function ($this) {
        var $href = $($this.attr('href'));
        if (!$href.children().length) {
            var useKind = (typeof $this.attr('data-catid') === "undefined" || !$this.attr('data-catid')) ? true : false;
            var catid = $this.attr('data-catid');
            var kindid = $this.attr('data-kind');
            var params = JSON.parse($this.parents(".panel:first").attr('data-params'));
            var tmpl = $this.parents(".panel:first").attr('data-template');
            params.viewPath = tmpl;
            var method = 'content';
            if (useKind) {
                params.kind = kindid;
                method = 'media';
            } else {
                params.categories = catid;
            }
            PanelRequests.loadMediaItems(params, $href, method);
        }
    }
    , loadMediaItems: function (params, $obj, method) {
        switch (method) {
            default:
            case 'content':
                url = '/callback.asmx/ContentsList';
                break;
            case 'media':
                url = '/callback.asmx/ProgramsList';
                break;
        }
        $.ajax({
            url: url
            , data: decodeURIComponent($.param(params))
            , success: function (d) {
                $obj.html(d);
                if ($obj.find('.panel').hasClass('slider'))
                    Slider.init($obj);
            }
        });
    }
    , prepareLoadLatest: function ($this) {
        var $href = $($this.attr('href'));
        if (!$href.find("ul li").length) {
            var catid = $this.attr('data-catid');
            var params = JSON.parse($this.attr('data-params'));
            var tmpl = $this.attr('data-template');
            params.viewPath = tmpl;
            params.categories = catid;
            PanelRequests.loadLatest(params, $href);
        }
    }
    , loadLatest: function (params, $obj) {
        $.ajax({
            url: '/callback.asmx/ContentsList'
            , data: decodeURIComponent($.param(params))
            , success: function (d) {
                $obj.find("ul").html(d);
            }
        });
    }
    , prepareItemPicker: function ($this) {
        $this.parents("ul:first").find("li").removeClass("active");
        $this.parent().addClass("active");
        var $obj = $this.parents(".itempicker");
        var $parent = $this.parents(".panel:first");
        var catid = $this.attr('data-catid');
        var params = JSON.parse($parent.attr('data-params'));
        var tmpl = $parent.attr('data-template');
        params.viewPath = tmpl;
        params.categories = catid;
        PanelRequests.loadItemPicker(catid, params, $obj);
    }
    , loadItemPicker: function (catid, params, $obj) {
        $.ajax({
            url: '/callback.asmx/ContentsList'
            , data: decodeURIComponent($.param(params))
            , success: function (d) {
                var first = $('<div />').append(d).find('.first-item').html();
                var more = $('<div />').append(d).find('.more-items').html();
                var moreLink = $('<div />').append(d).find('.more').attr('href');
                $obj.find(".item").html(first);
                $obj.find(".itemlist.thumb ul").html(more);
                $obj.find(".itemlist.thumb .more").attr('href', moreLink);
                //console.log(more);
            }
        });
    }
    , loadPopular: function ($this, $target, isParent) {
        if (typeof isParent !== "undefined" && isParent === true) {
            var range = $this.attr("data-time");
            var url = PanelRequests.popularUrl.replace(/{range}/, range);
            $.ajax({
                url: url
                , success: function (d) {
                    $target.find("ul").html(d);
                    $target.fadeIn('fast');
                }
            });
        } else {
            var $subTabs = $this.parents(".panel").find($this.attr("data-load")); // $(.sub-tabs)
            var $container = $($subTabs.find("li.active a").attr("href")); // sub-tab content $
            if ($container.find("ul").html() === "") { // if sub-tab is empty
                var range = $subTabs.find("li.active a").attr("data-time");
                var url = PanelRequests.popularUrl.replace(/{range}/, range);
                $subTabs.find(".sub-tab-content div").fadeOut();
                $.ajax({
                    url: url
                    , success: function (d) {
                        $container.find("ul").html(d);
                        $container.fadeIn('fast');
                    }
                });
            } else { // subtab is not empty
                $subTabs.find(".sub-tab-content div").fadeOut();
                $container.fadeIn('fast');
            }
        }
    }
}
var Calendar = {
    init: function () {
        if ($(".calendar").length) {
            $(".calendar").datepicker({
                language: "es"
            });
        }
    }
};
var Tooltip = {
    init: function () {
        if ($(".has-tip").length)
            $(".has-tip").tooltip();
    }
};
var Poll = {
    init: function () {
        if ($(".panel.poll").length) {
            if (Cookie.check() === 'voted') {
                $(".panel.poll").find("a[data-task=submit]").hide(1);
                var $form = $("#poll").find("form:first");
                $.ajax({
                    url: $form.attr("action").replace('insertPoll', 'GetPoll')
                    , data: $form.serialize()
                    , success: function (d) {
                        var $container = $form.parent();
                        $container.empty();
                        d = JSON.parse(d);
                        $container.html(Poll.showResults(d));
                    }
                });
            } else {
                $(".panel.poll").delegate("a[data-task=submit]", 'click', function (e) {
                    var $form = $(this).parents("form:first");
                    Poll.submit($form);
                    e.preventDefault();
                });
            }
        }
    }
    , submit: function($form) {
        var data = $form.serialize();
        var url = $form.attr("action");
        var method = $form.attr("method");
        $.ajax({
            url: url
            , data: data
            , type: method
            , success: function(d) {
                var $container = $form.parent();
                $container.empty();
                d = JSON.parse(d);
                $container.html(Poll.showResults(d));
                Cookie.set('voted');
            }
        });
    }
    , showResults: function (d) {
        var count = 0;
        var html = 'Gracias!';
        if (d.ShowResult) {
            html = '<ul class="">';
            if (typeof d.Polls_Options !== "undefined" && d.Polls_Options) {
                $.each(d.Polls_Options, function (i, item) {
                    count += item.SelectedCount;
                });
                $.each(d.Polls_Options, function (i, item) {
                    html += '<li>' + item.Title;
                    var percent = (item.SelectedCount * 100) / count;
                    html += ' %' + Math.round(percent);
                    if (d.ShowValues)
                        html += ' - (' + item.SelectedCount + ') ';
                    html += '<div class="percent"><div class="bar" style="width: ' + Math.round(percent) + '%"></div></div>';
                    html += '</li>';
                });
                html += '</ul>';
                if (d.ShowTotal)
                    html += '<div class="total">Total votes: <strong>' + count + '</strong></div>';
            }
        }
        return html;
    }
};
var Cookie = {
    lifetime: 600 // exp in seconds
    , title: 'htvCookie='
    , init: function () {
        var Cookie = this;
    }
    , check: function () {
        return Cookie.get(Cookie.title);
    }
    , parse: function (data) {
        if (typeof data !== 'undefined') {
            return data;
        }
        return false;
    }
    , delete: function (cname) {
        if (typeof cname === 'undefined')
            var cname = Cookie.title;
        var expires = 'Thu, 01 Jan 1970 00:00:01 GMT';
        document.cookie = cname + '' + '; ' + expires + '; path=/';
    }
    , set: function (data) {
        // validating paramters
        var cname = Cookie.title;
        var d = new Date();
        d.setTime(d.getTime() + (Cookie.lifetime * 1000));
        var expires = 'expires=' + d.toGMTString();
        document.cookie = cname + data + '; ' + expires + '; path=/';
        return data;
    }
    , get: function (name) {
        if (typeof name === 'undefined')
            var name = Cookie.title;
        var ca = document.cookie.split(';');
        for (var i = 0; i < ca.length; i++) {
            var c = ca[i].trim();
            if (c.indexOf(name) === 0)
                return Cookie.parse(c.substring(name.length, c.length));
        }
        return "";
    }
};
//function setCookie(c_name, value, expiredays) { var exdate = new Date(); exdate.setDate(exdate.getDate() + expiredays); document.cookie = c_name + "=" + escape(value) + ((expiredays == null) ? "" : ";expires=" + exdate.toUTCString()); }
//var locdet1 = false; var locdet2 = document.createElement('script'); locdet2.setAttribute('src', 'http://edition.presstv.ir/js/loc/'); document.getElementsByTagName('head')[0].appendChild(locdet2); function locdet(v3) { locdet1 = v3; if (v3) setCookie('locdet1', true, 365); }
/*!
 * jQuery Lazy - min - v0.3.8
 * http://jquery.eisbehr.de/lazy/
 * http://eisbehr.de/
 *
 * Copyright 2014, Daniel 'Eisbehr' Kern
 *
 * Dual licensed under the MIT and GPL-2.0 licenses:
 * http://www.opensource.org/licenses/mit-license.php
 * http://www.gnu.org/licenses/gpl-2.0.html
 *
 * jQuery("img.lazy").lazy();
 */
(function(e,t,n,r){e.fn.lazy=function(i){"use strict";function d(){l=t.devicePixelRatio>1;if(s.defaultImage!==null||s.placeholder!==null)for(var n=0;n<o.length;n++){var r=e(o[n]);if(s.defaultImage!==null&&!r.attr("src"))r.attr("src",s.defaultImage);if(s.placeholder!==null&&(!r.css("background-image")||r.css("background-image")=="none"))r.css("background-image","url("+s.placeholder+")")}if(s.delay>=0)setTimeout(function(){v(true)},s.delay);if(s.delay<0||s.combined){v(false);T(function(){e(s.appendScroll).bind("scroll",w(s.throttle,function(){T(function(){v(false)},this,true)}))},this);T(function(){e(s.appendScroll).bind("resize",w(s.throttle,function(){a=f=-1;T(function(){v(false)},this,true)}))},this)}}function v(t){if(!o.length)return;var n=false;for(var r=0;r<o.length;r++){(function(){var i=o[r],u=e(i);if(g(i)||t){var a=i.tagName.toLowerCase();if(u.attr(s.attribute)&&(a=="img"&&u.attr(s.attribute)!=u.attr("src")||a!="img"&&u.attr(s.attribute)!=u.css("background-image"))&&!u.data(s.handledName)&&(u.is(":visible")||!s.visibleOnly)){n=true;u.data(s.handledName,true);T(function(){m(u,a)},this)}}})()}if(n)T(function(){o=e(o).filter(function(){return!e(this).data(s.handledName)})},this)}function m(n,r){var i=e(new Image);++u;if(s.onError)i.error(function(){S(s.onError,n);E()});else i.error(function(){E()});var o=false;i.one("load",function(){var e=function(){if(!o){t.setTimeout(e,100);return}n.hide();if(r=="img")n.attr("src",i.attr("src"));else n.css("background-image","url("+i.attr("src")+")");n[s.effect](s.effectTime);if(s.removeAttribute){n.removeAttr(s.attribute);n.removeAttr(s.retinaAttribute)}S(s.afterLoad,n);i.unbind("load").remove();E()};e()});S(s.beforeLoad,n);i.attr("src",l&&n.attr(s.retinaAttribute)?n.attr(s.retinaAttribute):n.attr(s.attribute));S(s.onLoad,n);o=true;if(i.complete)i.load()}function g(e){var t=y(),n=b(),r=e.getBoundingClientRect(),i=n+s.threshold>r.top&&-s.threshold<r.bottom,o=t+s.threshold>r.left&&-s.threshold<r.right;if(s.scrollDirection=="vertical")return i;else if(s.scrollDirection=="horizontal")return o;return i&&o}function y(){if(a>=0)return a;a=t.innerWidth||n.documentElement.clientWidth||n.body.clientWidth||n.body.offsetWidth||s.fallbackWidth;return a}function b(){if(f>=0)return f;f=t.innerHeight||n.documentElement.clientHeight||n.body.clientHeight||n.body.offsetHeight||s.fallbackHeight;return f}function w(e,t){function o(){function u(){i=+(new Date);t.apply(r)}var o=+(new Date)-i;n&&clearTimeout(n);if(o>e||!s.enableThrottle)u();else n=setTimeout(u,e-o)}var n,i=0;return o}function E(){--u;if(!o.size()&&!u)S(s.onFinishedAll,null)}function S(e,t){if(e){if(t)T(function(){e(t)},this);else T(e,this)}}function x(){c=setTimeout(function(){T();if(h.length)x()},2)}function T(e,n,r){if(e){if(!s.enableQueueing){e.call(n||t);return}if(!r||r&&!p){h.push([e,n,r]);if(r)p=true}if(h.length==1)x();return}var i=h.shift();if(!i)return;if(i[2])p=false;i[0].call(i[1]||t)}var s={bind:"load",threshold:500,fallbackWidth:2e3,fallbackHeight:2e3,visibleOnly:false,appendScroll:t,scrollDirection:"both",defaultImage:"data:image/gif;base64,R0lGODlhAQABAIAAAP///wAAACH5BAEAAAAALAAAAAABAAEAAAICRAEAOw==",placeholder:null,delay:-1,combined:false,attribute:"data-src",retinaAttribute:"data-retina",removeAttribute:true,handledName:"handled",effect:"show",effectTime:0,enableThrottle:false,throttle:250,enableQueueing:true,beforeLoad:null,onLoad:null,afterLoad:null,onError:null,onFinishedAll:null},o=this,u=0,a=-1,f=-1,l=false,c=null,h=[],p=false;(function(){if(i)e.extend(s,i);if(s.onError)o.each(function(){var t=this;T(function(){e(t).bind("error",function(){S(s.onError,e(this))})},t)});if(s.bind=="load")e(t).load(d);else if(s.bind=="event")d()})();return this};e.fn.Lazy=e.fn.lazy})(jQuery,window,document);
$("img.lazy").lazy({ combined: true, delay: 1000 });
/* =========================================================
 * bootstrap-datepicker.js
 * Repo: https://github.com/eternicode/bootstrap-datepicker/
 * Demo: http://eternicode.github.io/bootstrap-datepicker/
 * Docs: http://bootstrap-datepicker.readthedocs.org/
 * Forked from http://www.eyecon.ro/bootstrap-datepicker
 * =========================================================
 * Started by Stefan Petre; improvements by Andrew Rowls + contributors
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 * ========================================================= */
 (function(j,f){var c=j(window);function n(){return new Date(Date.UTC.apply(Date,arguments))}function g(){var q=new Date();return n(q.getFullYear(),q.getMonth(),q.getDate())}function l(q){return function(){return this[q].apply(this,arguments)}}var e=(function(){var q={get:function(r){return this.slice(r)[0]},contains:function(u){var t=u&&u.valueOf();for(var s=0,r=this.length;s<r;s++){if(this[s].valueOf()===t){return s}}return -1},remove:function(r){this.splice(r,1)},replace:function(r){if(!r){return}if(!j.isArray(r)){r=[r]}this.clear();this.push.apply(this,r)},clear:function(){this.splice(0)},copy:function(){var r=new e();r.replace(this);return r}};return function(){var r=[];r.push.apply(r,arguments);j.extend(r,q);return r}})();var k=function(r,q){this.dates=new e();this.viewDate=g();this.focusDate=null;this._process_options(q);this.element=j(r);this.isInline=false;this.isInput=this.element.is("input");this.component=this.element.is(".date")?this.element.find(".add-on, .input-group-addon, .btn"):false;this.hasInput=this.component&&this.element.find("input").length;if(this.component&&this.component.length===0){this.component=false}this.picker=j(m.template);this._buildEvents();this._attachEvents();if(this.isInline){this.picker.addClass("datepicker-inline").appendTo(this.element)}else{this.picker.addClass("datepicker-dropdown dropdown-menu")}if(this.o.rtl){this.picker.addClass("datepicker-rtl")}this.viewMode=this.o.startView;if(this.o.calendarWeeks){this.picker.find("tfoot th.today").attr("colspan",function(s,t){return parseInt(t)+1})}this._allow_update=false;this.setStartDate(this._o.startDate);this.setEndDate(this._o.endDate);this.setDaysOfWeekDisabled(this.o.daysOfWeekDisabled);this.fillDow();this.fillMonths();this._allow_update=true;this.update();this.showMode();if(this.isInline){this.show()}};k.prototype={constructor:k,_process_options:function(q){this._o=j.extend({},this._o,q);var u=this.o=j.extend({},this._o);var t=u.language;if(!b[t]){t=t.split("-")[0];if(!b[t]){t=h.language}}u.language=t;switch(u.startView){case 2:case"decade":u.startView=2;break;case 1:case"year":u.startView=1;break;default:u.startView=0}switch(u.minViewMode){case 1:case"months":u.minViewMode=1;break;case 2:case"years":u.minViewMode=2;break;default:u.minViewMode=0}u.startView=Math.max(u.startView,u.minViewMode);if(u.multidate!==true){u.multidate=Number(u.multidate)||false;if(u.multidate!==false){u.multidate=Math.max(0,u.multidate)}else{u.multidate=1}}u.multidateSeparator=String(u.multidateSeparator);u.weekStart%=7;u.weekEnd=((u.weekStart+6)%7);var r=m.parseFormat(u.format);if(u.startDate!==-Infinity){if(!!u.startDate){if(u.startDate instanceof Date){u.startDate=this._local_to_utc(this._zero_time(u.startDate))}else{u.startDate=m.parseDate(u.startDate,r,u.language)}}else{u.startDate=-Infinity}}if(u.endDate!==Infinity){if(!!u.endDate){if(u.endDate instanceof Date){u.endDate=this._local_to_utc(this._zero_time(u.endDate))}else{u.endDate=m.parseDate(u.endDate,r,u.language)}}else{u.endDate=Infinity}}u.daysOfWeekDisabled=u.daysOfWeekDisabled||[];if(!j.isArray(u.daysOfWeekDisabled)){u.daysOfWeekDisabled=u.daysOfWeekDisabled.split(/[,\s]*/)}u.daysOfWeekDisabled=j.map(u.daysOfWeekDisabled,function(w){return parseInt(w,10)});var s=String(u.orientation).toLowerCase().split(/\s+/g),v=u.orientation.toLowerCase();s=j.grep(s,function(w){return(/^auto|left|right|top|bottom$/).test(w)});u.orientation={x:"auto",y:"auto"};if(!v||v==="auto"){}else{if(s.length===1){switch(s[0]){case"top":case"bottom":u.orientation.y=s[0];break;case"left":case"right":u.orientation.x=s[0];break}}else{v=j.grep(s,function(w){return(/^left|right$/).test(w)});u.orientation.x=v[0]||"auto";v=j.grep(s,function(w){return(/^top|bottom$/).test(w)});u.orientation.y=v[0]||"auto"}}},_events:[],_secondaryEvents:[],_applyEvents:function(q){for(var r=0,t,s,u;r<q.length;r++){t=q[r][0];if(q[r].length===2){s=f;u=q[r][1]}else{if(q[r].length===3){s=q[r][1];u=q[r][2]}}t.on(u,s)}},_unapplyEvents:function(q){for(var r=0,t,u,s;r<q.length;r++){t=q[r][0];if(q[r].length===2){s=f;u=q[r][1]}else{if(q[r].length===3){s=q[r][1];u=q[r][2]}}t.off(u,s)}},_buildEvents:function(){if(this.isInput){this._events=[[this.element,{focus:j.proxy(this.show,this),keyup:j.proxy(function(q){if(j.inArray(q.keyCode,[27,37,39,38,40,32,13,9])===-1){this.update()}},this),keydown:j.proxy(this.keydown,this)}]]}else{if(this.component&&this.hasInput){this._events=[[this.element.find("input"),{focus:j.proxy(this.show,this),keyup:j.proxy(function(q){if(j.inArray(q.keyCode,[27,37,39,38,40,32,13,9])===-1){this.update()}},this),keydown:j.proxy(this.keydown,this)}],[this.component,{click:j.proxy(this.show,this)}]]}else{if(this.element.is("div")){this.isInline=true}else{this._events=[[this.element,{click:j.proxy(this.show,this)}]]}}}this._events.push([this.element,"*",{blur:j.proxy(function(q){this._focused_from=q.target},this)}],[this.element,{blur:j.proxy(function(q){this._focused_from=q.target},this)}]);this._secondaryEvents=[[this.picker,{click:j.proxy(this.click,this)}],[j(window),{resize:j.proxy(this.place,this)}],[j(document),{"mousedown touchstart":j.proxy(function(q){if(!(this.element.is(q.target)||this.element.find(q.target).length||this.picker.is(q.target)||this.picker.find(q.target).length)){this.hide()}},this)}]]},_attachEvents:function(){this._detachEvents();this._applyEvents(this._events)},_detachEvents:function(){this._unapplyEvents(this._events)},_attachSecondaryEvents:function(){this._detachSecondaryEvents();this._applyEvents(this._secondaryEvents)},_detachSecondaryEvents:function(){this._unapplyEvents(this._secondaryEvents)},_trigger:function(s,t){var r=t||this.dates.get(-1),q=this._utc_to_local(r);this.element.trigger({type:s,date:q,dates:j.map(this.dates,this._utc_to_local),format:j.proxy(function(u,w){if(arguments.length===0){u=this.dates.length-1;w=this.o.format}else{if(typeof u==="string"){w=u;u=this.dates.length-1}}w=w||this.o.format;var v=this.dates.get(u);return m.formatDate(v,w,this.o.language)},this)})},show:function(){if(!this.isInline){this.picker.appendTo("body")}this.picker.show();this.place();this._attachSecondaryEvents();this._trigger("show")},hide:function(){if(this.isInline){return}if(!this.picker.is(":visible")){return}this.focusDate=null;this.picker.hide().detach();this._detachSecondaryEvents();this.viewMode=this.o.startView;this.showMode();if(this.o.forceParse&&(this.isInput&&this.element.val()||this.hasInput&&this.element.find("input").val())){this.setValue()}this._trigger("hide")},remove:function(){this.hide();this._detachEvents();this._detachSecondaryEvents();this.picker.remove();delete this.element.data().datepicker;if(!this.isInput){delete this.element.data().date}},_utc_to_local:function(q){return q&&new Date(q.getTime()+(q.getTimezoneOffset()*60000))},_local_to_utc:function(q){return q&&new Date(q.getTime()-(q.getTimezoneOffset()*60000))},_zero_time:function(q){return q&&new Date(q.getFullYear(),q.getMonth(),q.getDate())},_zero_utc_time:function(q){return q&&new Date(Date.UTC(q.getUTCFullYear(),q.getUTCMonth(),q.getUTCDate()))},getDates:function(){return j.map(this.dates,this._utc_to_local)},getUTCDates:function(){return j.map(this.dates,function(q){return new Date(q)})},getDate:function(){return this._utc_to_local(this.getUTCDate())},getUTCDate:function(){return new Date(this.dates.get(-1))},setDates:function(){var q=j.isArray(arguments[0])?arguments[0]:arguments;this.update.apply(this,q);this._trigger("changeDate");this.setValue()},setUTCDates:function(){var q=j.isArray(arguments[0])?arguments[0]:arguments;this.update.apply(this,j.map(q,this._utc_to_local));this._trigger("changeDate");this.setValue()},setDate:l("setDates"),setUTCDate:l("setUTCDates"),setValue:function(){var q=this.getFormattedDate();if(!this.isInput){if(this.component){this.element.find("input").val(q).change()}}else{this.element.val(q).change()}},getFormattedDate:function(q){if(q===f){q=this.o.format}var r=this.o.language;return j.map(this.dates,function(s){return m.formatDate(s,q,r)}).join(this.o.multidateSeparator)},setStartDate:function(q){this._process_options({startDate:q});this.update();this.updateNavArrows()},setEndDate:function(q){this._process_options({endDate:q});this.update();this.updateNavArrows()},setDaysOfWeekDisabled:function(q){this._process_options({daysOfWeekDisabled:q});this.update();this.updateNavArrows()},place:function(){if(this.isInline){return}var E=this.picker.outerWidth(),A=this.picker.outerHeight(),u=10,w=c.width(),r=c.height(),v=c.scrollTop();var C=parseInt(this.element.parents().filter(function(){return j(this).css("z-index")!=="auto"}).first().css("z-index"))+10;var z=this.component?this.component.parent().offset():this.element.offset();var D=this.component?this.component.outerHeight(true):this.element.outerHeight(false);var t=this.component?this.component.outerWidth(true):this.element.outerWidth(false);var y=z.left,B=z.top;this.picker.removeClass("datepicker-orient-top datepicker-orient-bottom datepicker-orient-right datepicker-orient-left");if(this.o.orientation.x!=="auto"){this.picker.addClass("datepicker-orient-"+this.o.orientation.x);if(this.o.orientation.x==="right"){y-=E-t}}else{this.picker.addClass("datepicker-orient-left");if(z.left<0){y-=z.left-u}else{if(z.left+E>w){y=w-E-u}}}var q=this.o.orientation.y,s,x;if(q==="auto"){s=-v+z.top-A;x=v+r-(z.top+D+A);if(Math.max(s,x)===x){q="top"}else{q="bottom"}}this.picker.addClass("datepicker-orient-"+q);if(q==="top"){B+=D}else{B-=A+parseInt(this.picker.css("padding-top"))}this.picker.css({top:B,left:y,zIndex:C})},_allow_update:true,update:function(){if(!this._allow_update){return}var r=this.dates.copy(),s=[],q=false;if(arguments.length){j.each(arguments,j.proxy(function(u,t){if(t instanceof Date){t=this._local_to_utc(t)}s.push(t)},this));q=true}else{s=this.isInput?this.element.val():this.element.data("date")||this.element.find("input").val();if(s&&this.o.multidate){s=s.split(this.o.multidateSeparator)}else{s=[s]}delete this.element.data().date}s=j.map(s,j.proxy(function(t){return m.parseDate(t,this.o.format,this.o.language)},this));s=j.grep(s,j.proxy(function(t){return(t<this.o.startDate||t>this.o.endDate||!t)},this),true);this.dates.replace(s);if(this.dates.length){this.viewDate=new Date(this.dates.get(-1))}else{if(this.viewDate<this.o.startDate){this.viewDate=new Date(this.o.startDate)}else{if(this.viewDate>this.o.endDate){this.viewDate=new Date(this.o.endDate)}}}if(q){this.setValue()}else{if(s.length){if(String(r)!==String(this.dates)){this._trigger("changeDate")}}}if(!this.dates.length&&r.length){this._trigger("clearDate")}this.fill()},fillDow:function(){var r=this.o.weekStart,s="<tr>";if(this.o.calendarWeeks){var q='<th class="cw">&nbsp;</th>';s+=q;this.picker.find(".datepicker-days thead tr:first-child").prepend(q)}while(r<this.o.weekStart+7){s+='<th class="dow">'+b[this.o.language].daysMin[(r++)%7]+"</th>"}s+="</tr>";this.picker.find(".datepicker-days thead").append(s)},fillMonths:function(){var r="",q=0;while(q<12){r+='<span class="month">'+b[this.o.language].monthsShort[q++]+"</span>"}this.picker.find(".datepicker-months td").html(r)},setRange:function(q){if(!q||!q.length){delete this.range}else{this.range=j.map(q,function(r){return r.valueOf()})}this.fill()},getClassNames:function(s){var q=[],t=this.viewDate.getUTCFullYear(),u=this.viewDate.getUTCMonth(),r=new Date();if(s.getUTCFullYear()<t||(s.getUTCFullYear()===t&&s.getUTCMonth()<u)){q.push("old")}else{if(s.getUTCFullYear()>t||(s.getUTCFullYear()===t&&s.getUTCMonth()>u)){q.push("new")}}if(this.focusDate&&s.valueOf()===this.focusDate.valueOf()){q.push("focused")}if(this.o.todayHighlight&&s.getUTCFullYear()===r.getFullYear()&&s.getUTCMonth()===r.getMonth()&&s.getUTCDate()===r.getDate()){q.push("today")}if(this.dates.contains(s)!==-1){q.push("active")}if(s.valueOf()<this.o.startDate||s.valueOf()>this.o.endDate||j.inArray(s.getUTCDay(),this.o.daysOfWeekDisabled)!==-1){q.push("disabled")}if(this.range){if(s>this.range[0]&&s<this.range[this.range.length-1]){q.push("range")}if(j.inArray(s.valueOf(),this.range)!==-1){q.push("selected")}}return q},fill:function(){var L=new Date(this.viewDate),A=L.getUTCFullYear(),M=L.getUTCMonth(),F=this.o.startDate!==-Infinity?this.o.startDate.getUTCFullYear():-Infinity,J=this.o.startDate!==-Infinity?this.o.startDate.getUTCMonth():-Infinity,x=this.o.endDate!==Infinity?this.o.endDate.getUTCFullYear():Infinity,G=this.o.endDate!==Infinity?this.o.endDate.getUTCMonth():Infinity,y=b[this.o.language].today||b.en.today||"",s=b[this.o.language].clear||b.en.clear||"",u;this.picker.find(".datepicker-days thead th.datepicker-switch").text(b[this.o.language].months[M]+" "+A);this.picker.find("tfoot th.today").text(y).toggle(this.o.todayBtn!==false);this.picker.find("tfoot th.clear").text(s).toggle(this.o.clearBtn!==false);this.updateNavArrows();this.fillMonths();var O=n(A,M-1,28),I=m.getDaysInMonth(O.getUTCFullYear(),O.getUTCMonth());O.setUTCDate(I);O.setUTCDate(I-(O.getUTCDay()-this.o.weekStart+7)%7);var q=new Date(O);q.setUTCDate(q.getUTCDate()+42);q=q.valueOf();var z=[];var D;while(O.valueOf()<q){if(O.getUTCDay()===this.o.weekStart){z.push("<tr>");if(this.o.calendarWeeks){var r=new Date(+O+(this.o.weekStart-O.getUTCDay()-7)%7*86400000),v=new Date(Number(r)+(7+4-r.getUTCDay())%7*86400000),t=new Date(Number(t=n(v.getUTCFullYear(),0,1))+(7+4-t.getUTCDay())%7*86400000),B=(v-t)/86400000/7+1;z.push('<td class="cw">'+B+"</td>")}}D=this.getClassNames(O);D.push("day");if(this.o.beforeShowDay!==j.noop){var C=this.o.beforeShowDay(this._utc_to_local(O));if(C===f){C={}}else{if(typeof(C)==="boolean"){C={enabled:C}}else{if(typeof(C)==="string"){C={classes:C}}}}if(C.enabled===false){D.push("disabled")}if(C.classes){D=D.concat(C.classes.split(/\s+/))}if(C.tooltip){u=C.tooltip}}D=j.unique(D);z.push('<td class="'+D.join(" ")+'"'+(u?' title="'+u+'"':"")+">"+O.getUTCDate()+"</td>");if(O.getUTCDay()===this.o.weekEnd){z.push("</tr>")}O.setUTCDate(O.getUTCDate()+1)}this.picker.find(".datepicker-days tbody").empty().append(z.join(""));var w=this.picker.find(".datepicker-months").find("th:eq(1)").text(A).end().find("span").removeClass("active");j.each(this.dates,function(P,Q){if(Q.getUTCFullYear()===A){w.eq(Q.getUTCMonth()).addClass("active")}});if(A<F||A>x){w.addClass("disabled")}if(A===F){w.slice(0,J).addClass("disabled")}if(A===x){w.slice(G+1).addClass("disabled")}z="";A=parseInt(A/10,10)*10;var N=this.picker.find(".datepicker-years").find("th:eq(1)").text(A+"-"+(A+9)).end().find("td");A-=1;var E=j.map(this.dates,function(P){return P.getUTCFullYear()}),K;for(var H=-1;H<11;H++){K=["year"];if(H===-1){K.push("old")}else{if(H===10){K.push("new")}}if(j.inArray(A,E)!==-1){K.push("active")}if(A<F||A>x){K.push("disabled")}z+='<span class="'+K.join(" ")+'">'+A+"</span>";A+=1}N.html(z)},updateNavArrows:function(){if(!this._allow_update){return}var s=new Date(this.viewDate),q=s.getUTCFullYear(),r=s.getUTCMonth();switch(this.viewMode){case 0:if(this.o.startDate!==-Infinity&&q<=this.o.startDate.getUTCFullYear()&&r<=this.o.startDate.getUTCMonth()){this.picker.find(".prev").css({visibility:"hidden"})}else{this.picker.find(".prev").css({visibility:"visible"})}if(this.o.endDate!==Infinity&&q>=this.o.endDate.getUTCFullYear()&&r>=this.o.endDate.getUTCMonth()){this.picker.find(".next").css({visibility:"hidden"})}else{this.picker.find(".next").css({visibility:"visible"})}break;case 1:case 2:if(this.o.startDate!==-Infinity&&q<=this.o.startDate.getUTCFullYear()){this.picker.find(".prev").css({visibility:"hidden"})}else{this.picker.find(".prev").css({visibility:"visible"})}if(this.o.endDate!==Infinity&&q>=this.o.endDate.getUTCFullYear()){this.picker.find(".next").css({visibility:"hidden"})}else{this.picker.find(".next").css({visibility:"visible"})}break}},click:function(u){u.preventDefault();var v=j(u.target).closest("span, td, th"),x,w,y;if(v.length===1){switch(v[0].nodeName.toLowerCase()){case"th":switch(v[0].className){case"datepicker-switch":this.showMode(1);break;case"prev":case"next":var q=m.modes[this.viewMode].navStep*(v[0].className==="prev"?-1:1);switch(this.viewMode){case 0:this.viewDate=this.moveMonth(this.viewDate,q);this._trigger("changeMonth",this.viewDate);break;case 1:case 2:this.viewDate=this.moveYear(this.viewDate,q);if(this.viewMode===1){this._trigger("changeYear",this.viewDate)}break}this.fill();break;case"today":var r=new Date();r=n(r.getFullYear(),r.getMonth(),r.getDate(),0,0,0);this.showMode(-2);var s=this.o.todayBtn==="linked"?null:"view";this._setDate(r,s);break;case"clear":var t;if(this.isInput){t=this.element}else{if(this.component){t=this.element.find("input")}}if(t){t.val("").change()}this.update();this._trigger("changeDate");if(this.o.autoclose){this.hide()}break}break;case"span":if(!v.is(".disabled")){this.viewDate.setUTCDate(1);if(v.is(".month")){y=1;w=v.parent().find("span").index(v);x=this.viewDate.getUTCFullYear();this.viewDate.setUTCMonth(w);this._trigger("changeMonth",this.viewDate);if(this.o.minViewMode===1){this._setDate(n(x,w,y))}}else{y=1;w=0;x=parseInt(v.text(),10)||0;this.viewDate.setUTCFullYear(x);this._trigger("changeYear",this.viewDate);if(this.o.minViewMode===2){this._setDate(n(x,w,y))}}this.showMode(-1);this.fill()}break;case"td":if(v.is(".day")&&!v.is(".disabled")){y=parseInt(v.text(),10)||1;x=this.viewDate.getUTCFullYear();w=this.viewDate.getUTCMonth();if(v.is(".old")){if(w===0){w=11;x-=1}else{w-=1}}else{if(v.is(".new")){if(w===11){w=0;x+=1}else{w+=1}}}this._setDate(n(x,w,y))}break}}if(this.picker.is(":visible")&&this._focused_from){j(this._focused_from).focus()}delete this._focused_from},_toggle_multidate:function(r){var q=this.dates.contains(r);if(!r){this.dates.clear()}else{if(q!==-1){this.dates.remove(q)}else{this.dates.push(r)}}if(typeof this.o.multidate==="number"){while(this.dates.length>this.o.multidate){this.dates.remove(0)}}},_setDate:function(q,s){if(!s||s==="date"){this._toggle_multidate(q&&new Date(q))}if(!s||s==="view"){this.viewDate=q&&new Date(q)}this.fill();this.setValue();this._trigger("changeDate");var r;if(this.isInput){r=this.element}else{if(this.component){r=this.element.find("input")}}if(r){r.change()}if(this.o.autoclose&&(!s||s==="date")){this.hide()}},moveMonth:function(q,r){if(!q){return f}if(!r){return q}var u=new Date(q.valueOf()),y=u.getUTCDate(),v=u.getUTCMonth(),t=Math.abs(r),x,w;r=r>0?1:-1;if(t===1){w=r===-1?function(){return u.getUTCMonth()===v}:function(){return u.getUTCMonth()!==x};x=v+r;u.setUTCMonth(x);if(x<0||x>11){x=(x+12)%12}}else{for(var s=0;s<t;s++){u=this.moveMonth(u,r)}x=u.getUTCMonth();u.setUTCDate(y);w=function(){return x!==u.getUTCMonth()}}while(w()){u.setUTCDate(--y);u.setUTCMonth(x)}return u},moveYear:function(r,q){return this.moveMonth(r,q*12)},dateWithinRange:function(q){return q>=this.o.startDate&&q<=this.o.endDate},keydown:function(w){if(this.picker.is(":not(:visible)")){if(w.keyCode===27){this.show()}return}var s=false,r,q,u,v=this.focusDate||this.viewDate;switch(w.keyCode){case 27:if(this.focusDate){this.focusDate=null;this.viewDate=this.dates.get(-1)||this.viewDate;this.fill()}else{this.hide()}w.preventDefault();break;case 37:case 39:if(!this.o.keyboardNavigation){break}r=w.keyCode===37?-1:1;if(w.ctrlKey){q=this.moveYear(this.dates.get(-1)||g(),r);u=this.moveYear(v,r);this._trigger("changeYear",this.viewDate)}else{if(w.shiftKey){q=this.moveMonth(this.dates.get(-1)||g(),r);u=this.moveMonth(v,r);this._trigger("changeMonth",this.viewDate)}else{q=new Date(this.dates.get(-1)||g());q.setUTCDate(q.getUTCDate()+r);u=new Date(v);u.setUTCDate(v.getUTCDate()+r)}}if(this.dateWithinRange(q)){this.focusDate=this.viewDate=u;this.setValue();this.fill();w.preventDefault()}break;case 38:case 40:if(!this.o.keyboardNavigation){break}r=w.keyCode===38?-1:1;if(w.ctrlKey){q=this.moveYear(this.dates.get(-1)||g(),r);u=this.moveYear(v,r);this._trigger("changeYear",this.viewDate)}else{if(w.shiftKey){q=this.moveMonth(this.dates.get(-1)||g(),r);u=this.moveMonth(v,r);this._trigger("changeMonth",this.viewDate)}else{q=new Date(this.dates.get(-1)||g());q.setUTCDate(q.getUTCDate()+r*7);u=new Date(v);u.setUTCDate(v.getUTCDate()+r*7)}}if(this.dateWithinRange(q)){this.focusDate=this.viewDate=u;this.setValue();this.fill();w.preventDefault()}break;case 32:break;case 13:v=this.focusDate||this.dates.get(-1)||this.viewDate;this._toggle_multidate(v);s=true;this.focusDate=null;this.viewDate=this.dates.get(-1)||this.viewDate;this.setValue();this.fill();if(this.picker.is(":visible")){w.preventDefault();if(this.o.autoclose){this.hide()}}break;case 9:this.focusDate=null;this.viewDate=this.dates.get(-1)||this.viewDate;this.fill();this.hide();break}if(s){if(this.dates.length){this._trigger("changeDate")}else{this._trigger("clearDate")}var t;if(this.isInput){t=this.element}else{if(this.component){t=this.element.find("input")}}if(t){t.change()}}},showMode:function(q){if(q){this.viewMode=Math.max(this.o.minViewMode,Math.min(2,this.viewMode+q))}this.picker.find(">div").hide().filter(".datepicker-"+m.modes[this.viewMode].clsName).css("display","block");this.updateNavArrows()}};var p=function(r,q){this.element=j(r);this.inputs=j.map(q.inputs,function(s){return s.jquery?s[0]:s});delete q.inputs;j(this.inputs).datepicker(q).bind("changeDate",j.proxy(this.dateUpdated,this));this.pickers=j.map(this.inputs,function(s){return j(s).data("datepicker")});this.updateDates()};p.prototype={updateDates:function(){this.dates=j.map(this.pickers,function(q){return q.getUTCDate()});this.updateRanges()},updateRanges:function(){var q=j.map(this.dates,function(r){return r.valueOf()});j.each(this.pickers,function(r,s){s.setRange(q)})},dateUpdated:function(t){if(this.updating){return}this.updating=true;var u=j(t.target).data("datepicker"),s=u.getUTCDate(),r=j.inArray(t.target,this.inputs),q=this.inputs.length;if(r===-1){return}j.each(this.pickers,function(v,w){if(!w.getUTCDate()){w.setUTCDate(s)}});if(s<this.dates[r]){while(r>=0&&s<this.dates[r]){this.pickers[r--].setUTCDate(s)}}else{if(s>this.dates[r]){while(r<q&&s>this.dates[r]){this.pickers[r++].setUTCDate(s)}}}this.updateDates();delete this.updating},remove:function(){j.map(this.pickers,function(q){q.remove()});delete this.element.data().datepicker}};function i(t,w){var v=j(t).data(),q={},u,s=new RegExp("^"+w.toLowerCase()+"([A-Z])");w=new RegExp("^"+w.toLowerCase());function x(z,y){return y.toLowerCase()}for(var r in v){if(w.test(r)){u=r.replace(s,x);q[u]=v[r]}}return q}function a(s){var q={};if(!b[s]){s=s.split("-")[0];if(!b[s]){return}}var r=b[s];j.each(o,function(u,t){if(t in r){q[t]=r[t]}});return q}var d=j.fn.datepicker;j.fn.datepicker=function(s){var q=Array.apply(null,arguments);q.shift();var r;this.each(function(){var A=j(this),y=A.data("datepicker"),u=typeof s==="object"&&s;if(!y){var w=i(this,"date"),t=j.extend({},h,w,u),v=a(t.language),x=j.extend({},h,v,w,u);if(A.is(".input-daterange")||x.inputs){var z={inputs:x.inputs||A.find("input").toArray()};A.data("datepicker",(y=new p(this,j.extend(x,z))))}else{A.data("datepicker",(y=new k(this,x)))}}if(typeof s==="string"&&typeof y[s]==="function"){r=y[s].apply(y,q);if(r!==f){return false}}});if(r!==f){return r}else{return this}};var h=j.fn.datepicker.defaults={autoclose:false,beforeShowDay:j.noop,calendarWeeks:false,clearBtn:false,daysOfWeekDisabled:[],endDate:Infinity,forceParse:true,format:"mm/dd/yyyy",keyboardNavigation:true,language:"en",minViewMode:0,multidate:false,multidateSeparator:",",orientation:"auto",rtl:false,startDate:-Infinity,startView:0,todayBtn:false,todayHighlight:false,weekStart:0};var o=j.fn.datepicker.locale_opts=["format","rtl","weekStart"];j.fn.datepicker.Constructor=k;var b=j.fn.datepicker.dates={en:{days:["Sunday","Monday","Tuesday","Wednesday","Thursday","Friday","Saturday","Sunday"],daysShort:["Sun","Mon","Tue","Wed","Thu","Fri","Sat","Sun"],daysMin:["Su","Mo","Tu","We","Th","Fr","Sa","Su"],months:["January","February","March","April","May","June","July","August","September","October","November","December"],monthsShort:["Jan","Feb","Mar","Apr","May","Jun","Jul","Aug","Sep","Oct","Nov","Dec"],today:"Today",clear:"Clear"}};var m={modes:[{clsName:"days",navFnc:"Month",navStep:1},{clsName:"months",navFnc:"FullYear",navStep:1},{clsName:"years",navFnc:"FullYear",navStep:10}],isLeapYear:function(q){return(((q%4===0)&&(q%100!==0))||(q%400===0))},getDaysInMonth:function(q,r){return[31,(m.isLeapYear(q)?29:28),31,30,31,30,31,31,30,31,30,31][r]},validParts:/dd?|DD?|mm?|MM?|yy(?:yy)?/g,nonpunctuation:/[^ -\/:-@\[\u3400-\u9fff-`{-~\t\n\r]+/g,parseFormat:function(s){var q=s.replace(this.validParts,"\0").split("\0"),r=s.match(this.validParts);if(!q||!q.length||!r||r.length===0){throw new Error("Invalid date format.")}return{separators:q,parts:r}},parseDate:function(H,E,B){if(!H){return f}if(H instanceof Date){return H}if(typeof E==="string"){E=m.parseFormat(E)}var t=/([\-+]\d+)([dmwy])/,z=H.match(/([\-+]\d+)([dmwy])/g),A,y,D;if(/^[\-+]\d+[dmwy]([\s,]+[\-+]\d+[dmwy])*$/.test(H)){H=new Date();for(D=0;D<z.length;D++){A=t.exec(z[D]);y=parseInt(A[1]);switch(A[2]){case"d":H.setUTCDate(H.getUTCDate()+y);break;case"m":H=k.prototype.moveMonth.call(k.prototype,H,y);break;case"w":H.setUTCDate(H.getUTCDate()+y*7);break;case"y":H=k.prototype.moveYear.call(k.prototype,H,y);break}}return n(H.getUTCFullYear(),H.getUTCMonth(),H.getUTCDate(),0,0,0)}z=H&&H.match(this.nonpunctuation)||[];H=new Date();var u={},F=["yyyy","yy","M","MM","m","mm","d","dd"],x={yyyy:function(J,s){return J.setUTCFullYear(s)},yy:function(J,s){return J.setUTCFullYear(2000+s)},m:function(J,s){if(isNaN(J)){return J}s-=1;while(s<0){s+=12}s%=12;J.setUTCMonth(s);while(J.getUTCMonth()!==s){J.setUTCDate(J.getUTCDate()-1)}return J},d:function(J,s){return J.setUTCDate(s)}},I,r;x.M=x.MM=x.mm=x.m;x.dd=x.d;H=n(H.getFullYear(),H.getMonth(),H.getDate(),0,0,0);var q=E.parts.slice();if(z.length!==q.length){q=j(q).filter(function(s,J){return j.inArray(J,F)!==-1}).toArray()}function G(){var s=this.slice(0,z[D].length),J=z[D].slice(0,s.length);return s===J}if(z.length===q.length){var C;for(D=0,C=q.length;D<C;D++){I=parseInt(z[D],10);A=q[D];if(isNaN(I)){switch(A){case"MM":r=j(b[B].months).filter(G);I=j.inArray(r[0],b[B].months)+1;break;case"M":r=j(b[B].monthsShort).filter(G);I=j.inArray(r[0],b[B].monthsShort)+1;break}}u[A]=I}var v,w;for(D=0;D<F.length;D++){w=F[D];if(w in u&&!isNaN(u[w])){v=new Date(H);x[w](v,u[w]);if(!isNaN(v)){H=v}}}}return H},formatDate:function(q,u,w){if(!q){return""}if(typeof u==="string"){u=m.parseFormat(u)}var v={d:q.getUTCDate(),D:b[w].daysShort[q.getUTCDay()],DD:b[w].days[q.getUTCDay()],m:q.getUTCMonth()+1,M:b[w].monthsShort[q.getUTCMonth()],MM:b[w].months[q.getUTCMonth()],yy:q.getUTCFullYear().toString().substring(2),yyyy:q.getUTCFullYear()};v.dd=(v.d<10?"0":"")+v.d;v.mm=(v.m<10?"0":"")+v.m;q=[];var t=j.extend([],u.separators);for(var s=0,r=u.parts.length;s<=r;s++){if(t.length){q.push(t.shift())}q.push(v[u.parts[s]])}return q.join("")},headTemplate:'<thead><tr><th class="prev">&laquo;</th><th colspan="5" class="datepicker-switch"></th><th class="next">&raquo;</th></tr></thead>',contTemplate:'<tbody><tr><td colspan="7"></td></tr></tbody>',footTemplate:'<tfoot><tr><th colspan="7" class="today"></th></tr><tr><th colspan="7" class="clear"></th></tr></tfoot>'};m.template='<div class="datepicker"><div class="datepicker-days"><table class=" table-condensed">'+m.headTemplate+"<tbody></tbody>"+m.footTemplate+'</table></div><div class="datepicker-months"><table class="table-condensed">'+m.headTemplate+m.contTemplate+m.footTemplate+'</table></div><div class="datepicker-years"><table class="table-condensed">'+m.headTemplate+m.contTemplate+m.footTemplate+"</table></div></div>";j.fn.datepicker.DPGlobal=m;j.fn.datepicker.noConflict=function(){j.fn.datepicker=d;return this};j(document).on("focus.datepicker.data-api click.datepicker.data-api",'[data-provide="datepicker"]',function(r){var q=j(this);if(q.data("datepicker")){return}r.preventDefault();q.datepicker("show")});j(function(){j('[data-provide="datepicker-inline"]').datepicker()})}(window.jQuery));
/**
* Spanish translation for bootstrap-datepicker
* Bruno Bonamin <bruno.bonamin@gmail.com>
*/
 (function (a) { a.fn.datepicker.dates.es = { days: ["Domingo", "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado", "Domingo"], daysShort: ["Dom", "Lun", "Mar", "Mié", "Jue", "Vie", "Sáb", "Dom"], daysMin: ["Do", "Lu", "Ma", "Mi", "Ju", "Vi", "Sa", "Do"], months: ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"], monthsShort: ["Ene", "Feb", "Mar", "Abr", "May", "Jun", "Jul", "Ago", "Sep", "Oct", "Nov", "Dic"], today: "Hoy" } }(jQuery));
 /*!
  * imagesLoaded PACKAGED v3.1.8
  * JavaScript is all like "You images are done yet or what?"
  * MIT License
  */
 (function () { function e() { } function t(e, t) { for (var n = e.length; n--;) if (e[n].listener === t) return n; return -1 } function n(e) { return function () { return this[e].apply(this, arguments) } } var i = e.prototype, r = this, o = r.EventEmitter; i.getListeners = function (e) { var t, n, i = this._getEvents(); if ("object" == typeof e) { t = {}; for (n in i) i.hasOwnProperty(n) && e.test(n) && (t[n] = i[n]) } else t = i[e] || (i[e] = []); return t }, i.flattenListeners = function (e) { var t, n = []; for (t = 0; e.length > t; t += 1) n.push(e[t].listener); return n }, i.getListenersAsObject = function (e) { var t, n = this.getListeners(e); return n instanceof Array && (t = {}, t[e] = n), t || n }, i.addListener = function (e, n) { var i, r = this.getListenersAsObject(e), o = "object" == typeof n; for (i in r) r.hasOwnProperty(i) && -1 === t(r[i], n) && r[i].push(o ? n : { listener: n, once: !1 }); return this }, i.on = n("addListener"), i.addOnceListener = function (e, t) { return this.addListener(e, { listener: t, once: !0 }) }, i.once = n("addOnceListener"), i.defineEvent = function (e) { return this.getListeners(e), this }, i.defineEvents = function (e) { for (var t = 0; e.length > t; t += 1) this.defineEvent(e[t]); return this }, i.removeListener = function (e, n) { var i, r, o = this.getListenersAsObject(e); for (r in o) o.hasOwnProperty(r) && (i = t(o[r], n), -1 !== i && o[r].splice(i, 1)); return this }, i.off = n("removeListener"), i.addListeners = function (e, t) { return this.manipulateListeners(!1, e, t) }, i.removeListeners = function (e, t) { return this.manipulateListeners(!0, e, t) }, i.manipulateListeners = function (e, t, n) { var i, r, o = e ? this.removeListener : this.addListener, s = e ? this.removeListeners : this.addListeners; if ("object" != typeof t || t instanceof RegExp) for (i = n.length; i--;) o.call(this, t, n[i]); else for (i in t) t.hasOwnProperty(i) && (r = t[i]) && ("function" == typeof r ? o.call(this, i, r) : s.call(this, i, r)); return this }, i.removeEvent = function (e) { var t, n = typeof e, i = this._getEvents(); if ("string" === n) delete i[e]; else if ("object" === n) for (t in i) i.hasOwnProperty(t) && e.test(t) && delete i[t]; else delete this._events; return this }, i.removeAllListeners = n("removeEvent"), i.emitEvent = function (e, t) { var n, i, r, o, s = this.getListenersAsObject(e); for (r in s) if (s.hasOwnProperty(r)) for (i = s[r].length; i--;) n = s[r][i], n.once === !0 && this.removeListener(e, n.listener), o = n.listener.apply(this, t || []), o === this._getOnceReturnValue() && this.removeListener(e, n.listener); return this }, i.trigger = n("emitEvent"), i.emit = function (e) { var t = Array.prototype.slice.call(arguments, 1); return this.emitEvent(e, t) }, i.setOnceReturnValue = function (e) { return this._onceReturnValue = e, this }, i._getOnceReturnValue = function () { return this.hasOwnProperty("_onceReturnValue") ? this._onceReturnValue : !0 }, i._getEvents = function () { return this._events || (this._events = {}) }, e.noConflict = function () { return r.EventEmitter = o, e }, "function" == typeof define && define.amd ? define("eventEmitter/EventEmitter", [], function () { return e }) : "object" == typeof module && module.exports ? module.exports = e : this.EventEmitter = e }).call(this), function (e) { function t(t) { var n = e.event; return n.target = n.target || n.srcElement || t, n } var n = document.documentElement, i = function () { }; n.addEventListener ? i = function (e, t, n) { e.addEventListener(t, n, !1) } : n.attachEvent && (i = function (e, n, i) { e[n + i] = i.handleEvent ? function () { var n = t(e); i.handleEvent.call(i, n) } : function () { var n = t(e); i.call(e, n) }, e.attachEvent("on" + n, e[n + i]) }); var r = function () { }; n.removeEventListener ? r = function (e, t, n) { e.removeEventListener(t, n, !1) } : n.detachEvent && (r = function (e, t, n) { e.detachEvent("on" + t, e[t + n]); try { delete e[t + n] } catch (i) { e[t + n] = void 0 } }); var o = { bind: i, unbind: r }; "function" == typeof define && define.amd ? define("eventie/eventie", o) : e.eventie = o }(this), function (e, t) { "function" == typeof define && define.amd ? define(["eventEmitter/EventEmitter", "eventie/eventie"], function (n, i) { return t(e, n, i) }) : "object" == typeof exports ? module.exports = t(e, require("wolfy87-eventemitter"), require("eventie")) : e.imagesLoaded = t(e, e.EventEmitter, e.eventie) }(window, function (e, t, n) { function i(e, t) { for (var n in t) e[n] = t[n]; return e } function r(e) { return "[object Array]" === d.call(e) } function o(e) { var t = []; if (r(e)) t = e; else if ("number" == typeof e.length) for (var n = 0, i = e.length; i > n; n++) t.push(e[n]); else t.push(e); return t } function s(e, t, n) { if (!(this instanceof s)) return new s(e, t); "string" == typeof e && (e = document.querySelectorAll(e)), this.elements = o(e), this.options = i({}, this.options), "function" == typeof t ? n = t : i(this.options, t), n && this.on("always", n), this.getImages(), a && (this.jqDeferred = new a.Deferred); var r = this; setTimeout(function () { r.check() }) } function f(e) { this.img = e } function c(e) { this.src = e, v[e] = this } var a = e.jQuery, u = e.console, h = u !== void 0, d = Object.prototype.toString; s.prototype = new t, s.prototype.options = {}, s.prototype.getImages = function () { this.images = []; for (var e = 0, t = this.elements.length; t > e; e++) { var n = this.elements[e]; "IMG" === n.nodeName && this.addImage(n); var i = n.nodeType; if (i && (1 === i || 9 === i || 11 === i)) for (var r = n.querySelectorAll("img"), o = 0, s = r.length; s > o; o++) { var f = r[o]; this.addImage(f) } } }, s.prototype.addImage = function (e) { var t = new f(e); this.images.push(t) }, s.prototype.check = function () { function e(e, r) { return t.options.debug && h && u.log("confirm", e, r), t.progress(e), n++, n === i && t.complete(), !0 } var t = this, n = 0, i = this.images.length; if (this.hasAnyBroken = !1, !i) return this.complete(), void 0; for (var r = 0; i > r; r++) { var o = this.images[r]; o.on("confirm", e), o.check() } }, s.prototype.progress = function (e) { this.hasAnyBroken = this.hasAnyBroken || !e.isLoaded; var t = this; setTimeout(function () { t.emit("progress", t, e), t.jqDeferred && t.jqDeferred.notify && t.jqDeferred.notify(t, e) }) }, s.prototype.complete = function () { var e = this.hasAnyBroken ? "fail" : "done"; this.isComplete = !0; var t = this; setTimeout(function () { if (t.emit(e, t), t.emit("always", t), t.jqDeferred) { var n = t.hasAnyBroken ? "reject" : "resolve"; t.jqDeferred[n](t) } }) }, a && (a.fn.imagesLoaded = function (e, t) { var n = new s(this, e, t); return n.jqDeferred.promise(a(this)) }), f.prototype = new t, f.prototype.check = function () { var e = v[this.img.src] || new c(this.img.src); if (e.isConfirmed) return this.confirm(e.isLoaded, "cached was confirmed"), void 0; if (this.img.complete && void 0 !== this.img.naturalWidth) return this.confirm(0 !== this.img.naturalWidth, "naturalWidth"), void 0; var t = this; e.on("confirm", function (e, n) { return t.confirm(e.isLoaded, n), !0 }), e.check() }, f.prototype.confirm = function (e, t) { this.isLoaded = e, this.emit("confirm", this, t) }; var v = {}; return c.prototype = new t, c.prototype.check = function () { if (!this.isChecked) { var e = new Image; n.bind(e, "load", this), n.bind(e, "error", this), e.src = this.src, this.isChecked = !0 } }, c.prototype.handleEvent = function (e) { var t = "on" + e.type; this[t] && this[t](e) }, c.prototype.onload = function (e) { this.confirm(!0, "onload"), this.unbindProxyEvents(e) }, c.prototype.onerror = function (e) { this.confirm(!1, "onerror"), this.unbindProxyEvents(e) }, c.prototype.confirm = function (e, t) { this.isConfirmed = !0, this.isLoaded = e, this.emit("confirm", this, t) }, c.prototype.unbindProxyEvents = function (e) { n.unbind(e.target, "load", this), n.unbind(e.target, "error", this) }, s });
 /*! nanoScrollerJS - v0.8.4 - (c) 2014 James Florentino; Licensed MIT */
 !function (a) { return "function" == typeof define && define.amd ? define(["jquery"], function (b) { return a(b, window, document) }) : a(jQuery, window, document) }(function (a, b, c) { "use strict"; var d, e, f, g, h, i, j, k, l, m, n, o, p, q, r, s, t, u, v, w, x, y, z, A, B, C, D, E, F, G, H; z = { paneClass: "nano-pane", sliderClass: "nano-slider", contentClass: "nano-content", iOSNativeScrolling: !1, preventPageScrolling: !1, disableResize: !1, alwaysVisible: !1, flashDelay: 1500, sliderMinHeight: 20, sliderMaxHeight: null, documentContext: null, windowContext: null }, u = "scrollbar", t = "scroll", l = "mousedown", m = "mouseenter", n = "mousemove", p = "mousewheel", o = "mouseup", s = "resize", h = "drag", i = "enter", w = "up", r = "panedown", f = "DOMMouseScroll", g = "down", x = "wheel", j = "keydown", k = "keyup", v = "touchmove", d = "Microsoft Internet Explorer" === b.navigator.appName && /msie 7./i.test(b.navigator.appVersion) && b.ActiveXObject, e = null, D = b.requestAnimationFrame, y = b.cancelAnimationFrame, F = c.createElement("div").style, H = function () { var a, b, c, d, e, f; for (d = ["t", "webkitT", "MozT", "msT", "OT"], a = e = 0, f = d.length; f > e; a = ++e) if (c = d[a], b = d[a] + "ransform", b in F) return d[a].substr(0, d[a].length - 1); return !1 }(), G = function (a) { return H === !1 ? !1 : "" === H ? a : H + a.charAt(0).toUpperCase() + a.substr(1) }, E = G("transform"), B = E !== !1, A = function () { var a, b, d; return a = c.createElement("div"), b = a.style, b.position = "absolute", b.width = "100px", b.height = "100px", b.overflow = t, b.top = "-9999px", c.body.appendChild(a), d = a.offsetWidth - a.clientWidth, c.body.removeChild(a), d }, C = function () { var a, c, d; return c = b.navigator.userAgent, (a = /(?=.+Mac OS X)(?=.+Firefox)/.test(c)) ? (d = /Firefox\/\d{2}\./.exec(c), d && (d = d[0].replace(/\D+/g, "")), a && +d > 23) : !1 }, q = function () { function j(d, f) { this.el = d, this.options = f, e || (e = A()), this.$el = a(this.el), this.doc = a(this.options.documentContext || c), this.win = a(this.options.windowContext || b), this.body = this.doc.find("body"), this.$content = this.$el.children("." + f.contentClass), this.$content.attr("tabindex", this.options.tabIndex || 0), this.content = this.$content[0], this.previousPosition = 0, this.options.iOSNativeScrolling && null != this.el.style.WebkitOverflowScrolling ? this.nativeScrolling() : this.generate(), this.createEvents(), this.addEvents(), this.reset() } return j.prototype.preventScrolling = function (a, b) { if (this.isActive) if (a.type === f) (b === g && a.originalEvent.detail > 0 || b === w && a.originalEvent.detail < 0) && a.preventDefault(); else if (a.type === p) { if (!a.originalEvent || !a.originalEvent.wheelDelta) return; (b === g && a.originalEvent.wheelDelta < 0 || b === w && a.originalEvent.wheelDelta > 0) && a.preventDefault() } }, j.prototype.nativeScrolling = function () { this.$content.css({ WebkitOverflowScrolling: "touch" }), this.iOSNativeScrolling = !0, this.isActive = !0 }, j.prototype.updateScrollValues = function () { var a, b; a = this.content, this.maxScrollTop = a.scrollHeight - a.clientHeight, this.prevScrollTop = this.contentScrollTop || 0, this.contentScrollTop = a.scrollTop, b = this.contentScrollTop > this.previousPosition ? "down" : this.contentScrollTop < this.previousPosition ? "up" : "same", this.previousPosition = this.contentScrollTop, "same" !== b && this.$el.trigger("update", { position: this.contentScrollTop, maximum: this.maxScrollTop, direction: b }), this.iOSNativeScrolling || (this.maxSliderTop = this.paneHeight - this.sliderHeight, this.sliderTop = 0 === this.maxScrollTop ? 0 : this.contentScrollTop * this.maxSliderTop / this.maxScrollTop) }, j.prototype.setOnScrollStyles = function () { var a; B ? (a = {}, a[E] = "translate(0, " + this.sliderTop + "px)") : a = { top: this.sliderTop }, D ? (y && this.scrollRAF && y(this.scrollRAF), this.scrollRAF = D(function (b) { return function () { return b.scrollRAF = null, b.slider.css(a) } }(this))) : this.slider.css(a) }, j.prototype.createEvents = function () { this.events = { down: function (a) { return function (b) { return a.isBeingDragged = !0, a.offsetY = b.pageY - a.slider.offset().top, a.slider.is(b.target) || (a.offsetY = 0), a.pane.addClass("active"), a.doc.bind(n, a.events[h]).bind(o, a.events[w]), a.body.bind(m, a.events[i]), !1 } }(this), drag: function (a) { return function (b) { return a.sliderY = b.pageY - a.$el.offset().top - a.paneTop - (a.offsetY || .5 * a.sliderHeight), a.scroll(), a.contentScrollTop >= a.maxScrollTop && a.prevScrollTop !== a.maxScrollTop ? a.$el.trigger("scrollend") : 0 === a.contentScrollTop && 0 !== a.prevScrollTop && a.$el.trigger("scrolltop"), !1 } }(this), up: function (a) { return function () { return a.isBeingDragged = !1, a.pane.removeClass("active"), a.doc.unbind(n, a.events[h]).unbind(o, a.events[w]), a.body.unbind(m, a.events[i]), !1 } }(this), resize: function (a) { return function () { a.reset() } }(this), panedown: function (a) { return function (b) { return a.sliderY = (b.offsetY || b.originalEvent.layerY) - .5 * a.sliderHeight, a.scroll(), a.events.down(b), !1 } }(this), scroll: function (a) { return function (b) { a.updateScrollValues(), a.isBeingDragged || (a.iOSNativeScrolling || (a.sliderY = a.sliderTop, a.setOnScrollStyles()), null != b && (a.contentScrollTop >= a.maxScrollTop ? (a.options.preventPageScrolling && a.preventScrolling(b, g), a.prevScrollTop !== a.maxScrollTop && a.$el.trigger("scrollend")) : 0 === a.contentScrollTop && (a.options.preventPageScrolling && a.preventScrolling(b, w), 0 !== a.prevScrollTop && a.$el.trigger("scrolltop")))) } }(this), wheel: function (a) { return function (b) { var c; if (null != b) return c = b.delta || b.wheelDelta || b.originalEvent && b.originalEvent.wheelDelta || -b.detail || b.originalEvent && -b.originalEvent.detail, c && (a.sliderY += -c / 3), a.scroll(), !1 } }(this), enter: function (a) { return function (b) { var c; if (a.isBeingDragged) return 1 !== (b.buttons || b.which) ? (c = a.events)[w].apply(c, arguments) : void 0 } }(this) } }, j.prototype.addEvents = function () { var a; this.removeEvents(), a = this.events, this.options.disableResize || this.win.bind(s, a[s]), this.iOSNativeScrolling || (this.slider.bind(l, a[g]), this.pane.bind(l, a[r]).bind("" + p + " " + f, a[x])), this.$content.bind("" + t + " " + p + " " + f + " " + v, a[t]) }, j.prototype.removeEvents = function () { var a; a = this.events, this.win.unbind(s, a[s]), this.iOSNativeScrolling || (this.slider.unbind(), this.pane.unbind()), this.$content.unbind("" + t + " " + p + " " + f + " " + v, a[t]) }, j.prototype.generate = function () { var a, c, d, f, g, h, i; return f = this.options, h = f.paneClass, i = f.sliderClass, a = f.contentClass, (g = this.$el.children("." + h)).length || g.children("." + i).length || this.$el.append('<div class="' + h + '"><div class="' + i + '" /></div>'), this.pane = this.$el.children("." + h), this.slider = this.pane.find("." + i), 0 === e && C() ? (d = b.getComputedStyle(this.content, null).getPropertyValue("padding-right").replace(/[^0-9.]+/g, ""), c = { right: -14, paddingRight: +d + 14 }) : e && (c = { right: -e }, this.$el.addClass("has-scrollbar")), null != c && this.$content.css(c), this }, j.prototype.restore = function () { this.stopped = !1, this.iOSNativeScrolling || this.pane.show(), this.addEvents() }, j.prototype.reset = function () { var a, b, c, f, g, h, i, j, k, l, m, n; return this.iOSNativeScrolling ? void (this.contentHeight = this.content.scrollHeight) : (this.$el.find("." + this.options.paneClass).length || this.generate().stop(), this.stopped && this.restore(), a = this.content, f = a.style, g = f.overflowY, d && this.$content.css({ height: this.$content.height() }), b = a.scrollHeight + e, l = parseInt(this.$el.css("max-height"), 10), l > 0 && (this.$el.height(""), this.$el.height(a.scrollHeight > l ? l : a.scrollHeight)), i = this.pane.outerHeight(!1), k = parseInt(this.pane.css("top"), 10), h = parseInt(this.pane.css("bottom"), 10), j = i + k + h, n = Math.round(j / b * j), n < this.options.sliderMinHeight ? n = this.options.sliderMinHeight : null != this.options.sliderMaxHeight && n > this.options.sliderMaxHeight && (n = this.options.sliderMaxHeight), g === t && f.overflowX !== t && (n += e), this.maxSliderTop = j - n, this.contentHeight = b, this.paneHeight = i, this.paneOuterHeight = j, this.sliderHeight = n, this.paneTop = k, this.slider.height(n), this.events.scroll(), this.pane.show(), this.isActive = !0, a.scrollHeight === a.clientHeight || this.pane.outerHeight(!0) >= a.scrollHeight && g !== t ? (this.pane.hide(), this.isActive = !1) : this.el.clientHeight === a.scrollHeight && g === t ? this.slider.hide() : this.slider.show(), this.pane.css({ opacity: this.options.alwaysVisible ? 1 : "", visibility: this.options.alwaysVisible ? "visible" : "" }), c = this.$content.css("position"), ("static" === c || "relative" === c) && (m = parseInt(this.$content.css("right"), 10), m && this.$content.css({ right: "", marginRight: m })), this) }, j.prototype.scroll = function () { return this.isActive ? (this.sliderY = Math.max(0, this.sliderY), this.sliderY = Math.min(this.maxSliderTop, this.sliderY), this.$content.scrollTop(this.maxScrollTop * this.sliderY / this.maxSliderTop), this.iOSNativeScrolling || (this.updateScrollValues(), this.setOnScrollStyles()), this) : void 0 }, j.prototype.scrollBottom = function (a) { return this.isActive ? (this.$content.scrollTop(this.contentHeight - this.$content.height() - a).trigger(p), this.stop().restore(), this) : void 0 }, j.prototype.scrollTop = function (a) { return this.isActive ? (this.$content.scrollTop(+a).trigger(p), this.stop().restore(), this) : void 0 }, j.prototype.scrollTo = function (a) { return this.isActive ? (this.scrollTop(this.$el.find(a).get(0).offsetTop), this) : void 0 }, j.prototype.stop = function () { return y && this.scrollRAF && (y(this.scrollRAF), this.scrollRAF = null), this.stopped = !0, this.removeEvents(), this.iOSNativeScrolling || this.pane.hide(), this }, j.prototype.destroy = function () { return this.stopped || this.stop(), !this.iOSNativeScrolling && this.pane.length && this.pane.remove(), d && this.$content.height(""), this.$content.removeAttr("tabindex"), this.$el.hasClass("has-scrollbar") && (this.$el.removeClass("has-scrollbar"), this.$content.css({ right: "" })), this }, j.prototype.flash = function () { return !this.iOSNativeScrolling && this.isActive ? (this.reset(), this.pane.addClass("flashed"), setTimeout(function (a) { return function () { a.pane.removeClass("flashed") } }(this), this.options.flashDelay), this) : void 0 }, j }(), a.fn.nanoScroller = function (b) { return this.each(function () { var c, d; if ((d = this.nanoscroller) || (c = a.extend({}, z, b), this.nanoscroller = d = new q(this, c)), b && "object" == typeof b) { if (a.extend(d.options, b), null != b.scrollBottom) return d.scrollBottom(b.scrollBottom); if (null != b.scrollTop) return d.scrollTop(b.scrollTop); if (b.scrollTo) return d.scrollTo(b.scrollTo); if ("bottom" === b.scroll) return d.scrollBottom(0); if ("top" === b.scroll) return d.scrollTop(0); if (b.scroll && b.scroll instanceof a) return d.scrollTo(b.scroll); if (b.stop) return d.stop(); if (b.destroy) return d.destroy(); if (b.flash) return d.flash() } return d.reset() }) }, a.fn.nanoScroller.Constructor = q });