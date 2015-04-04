var token = '';
var Global = {
    userGroups: ''
    , groupsusers: ''
    , init: function() {
        Global.registerHandlebarHelpers();
        Global.getUserGroups();
        Global.periodicals();
    }
    , isInt: function(n) {
        return typeof parseInt(n) === "number" && isFinite(parseInt(n)) && parseInt(n) % 1 === 0;
    }
    , getVal: function($obj, attr, deflt) {
        if ($obj.length) {
            var val = $obj.attr(attr);
            if (typeof val === "undefined") {
                val = deflt;
            }
            return val;
        }
        return false;
    }
    , ucfirst: function(string) {
        if (typeof string !== "undefined") {
            return string.charAt(0).toUpperCase() + string.slice(1);
        } else {
            return string;
        }
    }
    , cleanText: function(str) {
        return str.replace(/[^\w\s]/gi, '');
    }
    , getUserGroups: function() {
        if (Global.userGroups === "" || Global.groupsusers === "") {
            groups = '';
            groupsusers = '';
            users = '';
            $.ajax({
                url: Services.base + Services.userGroups
                , success: function(d) {
                    var html = '<select id="group-selector" name="group" class="user-groups-selector change-group form-control"><option value="0">-- Select Group --</option>';
                    $.each(d, function(i, item) {
                        html += '<option value="' + item.Id + '">' + item.Title + '</option>';
                    });
                    html += '<select class="user-groups-selector change-group form-control">';
                    Global.userGroups = html;
                }
            });
        }
    }
    , registerHandlebarHelpers: function() {
        Handlebars.registerHelper('times', function(n, block) { // Loop a block starting at 0
            var accum = '';
            for (var i = 0; i < n; ++i)
                accum += block.fn(i);
            return accum;
        });
        Handlebars.registerHelper('date', function(offset, options) {
            var output = '';
            if (typeof offset === 'undefined' || offset === '')
                offset = 0;
            var date = new Date();
            date.setDate(date.getDate() + offset);
            var dd = date.getDate();
            var mm = date.getMonth() + 1; //January is 0!
            var yyyy = date.getFullYear();
            if (dd < 10)
                dd = '0' + dd;
            if (mm < 10)
                mm = '0' + mm;
            output = mm + '/' + dd + '/' + yyyy;
            return output;
        });
        Handlebars.registerHelper('date2', function(offset, options) {
            var output = '';
            if (typeof offset === 'undefined' || offset === '')
                offset = 0;
            var date = new Date();
            date.setDate(date.getDate() + offset);
            var dd = date.getDate();
            var mm = date.getMonth() + 1; //January is 0!
            var yyyy = date.getFullYear();
            if (dd < 10)
                dd = '0' + dd;
            if (mm < 10)
                mm = '0' + mm;
            output = yyyy + '-' + mm + '-' + dd;
            return output;
        });
        Handlebars.registerHelper('htimes', function(n, block) { // Loop a block starting at 1 [human-readable times]
            var accum = '';
            for (var i = 1; i < (n + 1); ++i)
                accum += block.fn(i);
            return accum;
        });
        Handlebars.registerHelper('for', function(from, to, incr, block) { // For loop {{#for i to steps}} -> {{#for 0 10 2}}
            var accum = '';
            for (var i = from; i < to; i += incr)
                accum += block.fn(i);
            return accum;
        });
        Handlebars.registerHelper('ifCond', function(v1, v2, options) {
            if (v1 === v2) {
                return options.fn(this);
            }
            return options.inverse(this);
        });
        Handlebars.registerHelper('ifCondNot', function(v1, v2, options) {
            if (v1 !== v2) {
                return options.fn(this);
            }
            return options.inverse(this);
        });
        Handlebars.registerHelper('ifActive', function(val, options) {
            var currentID = (typeof Location.parts[2] === "undefined") ? 0 : Location.parts[2];
            if (parseInt(val) === parseInt(currentID)) {
                return "grey-cascade";
            }
            return "btn-default";
        });
        Handlebars.registerHelper('ifHasId', function(options) {
            if (typeof Location.parts[2] !== "undefined" && Location.parts[2] !== null) {
                return options.fn(this);
            }
            return '<div class="note note-warning"><h4 class="block">Please select category!</h4><p></p></div>';
        });
        Handlebars.registerHelper('Bool2Label', function(val, options) {
            var output = '';
            if (val === true) {
                output = '<span class="label label-success">Yes</label>';
            } else {
                output = '<span class="label label-warning">No</label>';
            }
            return output;
        });
        Handlebars.registerHelper('Num2Label', function(val, options) {
            var output = '';
            switch (val) {
                case 0:
                    output = '<span class="label label-warning">No</label>';
                    break;
                case 1:
                    output = '<span class="label label-success">Yes</label>';
                    break;
            }
            return output;
        });
        Handlebars.registerHelper('isChecked', function(val, options) {
            var output = '';
            if (val === true || val === 1) {
                output = 'checked';
            }
            return output;
        });
        Handlebars.registerHelper('cycle', function(value, block) {
            var values = value.split(' ');
            return values[block.data.index % (values.length + 1)];
        });
        window.Handlebars.registerHelper('select', function(value, options) {
            var $el = $('<select />').html(options.fn(this));
            $el.find('[value=' + value + ']').attr({'selected': 'selected'});
            return $el.html();
        });
    }
    , initDatepicker: function() {
        var $datepicker = $("[data-picker=datepicker]");
        $datepicker.parent().datetimepicker({
            pick12HourFormat: true
            , format: 'MM/dd/yyyy HH:mm:ss PP'
        });
    }
    , periodicals: function() {
        Items.initSelects(500);
    }
};
var Sidebar = {
    $container: $("#sidebar")
    , init: function(href) {
        Sidebar.$container.find("li").removeClass('active');
        if (typeof href !== "undefined") {
            Sidebar.$container.find('a[href="#' + href + '"]').parents("li").addClass('active');
        } else {
            Sidebar.$container.find("li:first").addClass('active');
        }
        Sidebar.handleClicks();
    }
    , handleClicks: function() {
        Sidebar.$container.delegate("a", 'click', function(e) {
            $a = $(this);
            Sidebar.$container.find("li").removeClass('active');
            $a.parents("li").addClass('active');
        });
    }
    , indentGen: function(n) {
        var ret = '';
        for (i = 0; i < n - 1; i++)
            ret += '&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;';
        if (n > 0)
            ret += '*';
        return ret;
    }
    , convertToDropdown: function() {
        var $items = Sidebar.$container.find("[data-src=sections] ul");
        $items.find("li").each(function() {
            if ($(this).text().indexOf('*') === 1) {
                $(this).deepestChild().parent().text(Global.cleanText($(this).text()));
                var $prev = $(this).prev();
                if (!$prev.find("ul.sub-menu").length) {
                    $prev.append('<ul class="sub-menu"></ul>');
                }
                var $submenu = $prev.find("ul.sub-menu");
                $(this).appendTo($submenu);
            }
        });
    }
};
var Location = {
    splitter: '#'
    , parts: []
    , history: []
    , 'setParts': {set: function(x) {
            this.parts = x;
        }}
    , init: function() {
        $(window).on('hashchange', function() {
            var fragments = location.href.split(Location.splitter)[1];
            Location.parts = Location.parse(fragments);
            if (Location.parts[0] === 'history' && Location.parts[1] === 'back')
                if (Location.history.length > 1)
                    Location.goBack();
                else
                    Location.redirect(false, 'dashboard/items');
            Location.history.push(fragments);
            Sidebar.init(fragments);
            Form.clearAutosaveTimeout();
        });
        Global.init();
        Location.getCurrent();
        User.init();
        Notifications.init();
    }
    , getCurrent: function() {
        var fragments = location.href.split(Location.splitter)[1];
        Location.history.push(fragments);
        Sidebar.init(fragments);
        Location.parts = Location.parse(fragments);
    }
    , parse: function(fragments) {
        if (typeof fragments !== "undefined") {
            var parts = fragments.split('/');
            Location.parts = parts;
            console.log(parts);
            if (parts[0] !== 'history') {
                Blocks.init(parts);
                Blocks.headers(Blocks.parse(parts));
            }
            return parts;
        } else {
            console.warn('no url fragments');
            Location.redirect(false, 'dashboard/items');
        }
        return false;
    }
    , goBack: function() {
        Location.redirect(true);
//        Sidebar.init(Location.parts[0] + '/' + Location.parts[1]);
    }
    , redirect: function(back, url) {
        if (typeof back !== "undefined" && back === true) {
            console.log('Redirecting to: Previous page');
            var redirect = Location.history[Location.history.length - 2];
            window.location.href = '#' + redirect;
        } else if (typeof url !== "undefined") {
            console.log('Redirecting to: #' + url);
            window.location.href = '#' + url;
        } else {
            return false;
        }
        return true;
    }
    , refresh: function() {
        location.reload();
    }
};
var User = {
    msg: '<blockquote>"{user}" has been added.</blockquote>'
    , ms: ''
    , users: []
    , init: function() {
        User.loadData();
    }
    , add: function(form, method, results) {
        var data = $(form).serializeObject();
        $.ajax({
            url: Services.base + Services.users
            , type: method
            , headers: {"Authorization": token}
            , data: JSON.stringify(data)
            , success: function(d) {
                $(results).append(User.msg.replace(/{user}/g, data.Username));
            }
        });
    }
    , initManage: function() {
        if ($(".x-editable").length) {
            $(".x-editable").editable({
                send: 'never'
            }).on('save', function(e, params) {
                User.saveInline(e, params);
            });
        }
        User.resetPass();
        User.changeState();
        User.initGroupSelect();
    }
    , initInbox: function() {
        User.initSuggest();
//        if ($("#destination-select").length) {
//            $("#destination-select").html(Global.userGroups).find("select").select2();
//        }
    }
    , initSuggest: function() {
        if (User.users !== []) {
            $.ajax({
                url: Services.base + Services.users
                , headers: {"Authorization": token}
                , success: function(d) {
                    var options = '';
                    $.each(d, function(i, user) {
                        options += '<option value="' + user.Id + '">' + user.Name + '</option>';
                    });
                    User.users = options;
                    $("#userselect").html(options).select2();
                }
            });
        } else {
            alert();
            $("#userselect").html(User.users).select2();
        }
    }
    , disableSuggest: function() {
        if (typeof User.ms === 'object') {
            User.ms.empty();
            User.ms.disable();
        }
    }
    , sendMessage: function(form, method, results) {
        var params = $(form).serializeObject();
        var data = [];
        if (typeof params["to[]"] !== "undefined") {
            if (typeof params["to[]"] === "string") {
                data.push({MessageToId: params["to[]"], MessageBody: params["Message"]});
            } else {
                $.each(params['to[]'], function(i, user) {
                    data.push({MessageToId: user, MessageBody: params["Message"]});
                });
            }
            delete params['to[]'];
        }
        $.each(data, function(i, d) {
            $.ajax({
                url: Services.base + Services.users + 'messages'
                , type: method
                , headers: {"Authorization": token}
                , data: JSON.stringify(d)
                , success: function(d) {
                    $(results).html("<blockquote>Message has been sent.</blockquote>");
                    $(results).parents(".modal").modal('hide');
                }
            });
        });
    }
    , changePass: function(form, method) {
        var params = $(form).serializeObject();
        $.ajax({
            url: Services.base + Services.user + '/password'
            , type: method
            , headers: {"Authorization": token}
            , data: JSON.stringify({Password: params.newPass})
            , success: function(d) {
                User.logout();
            }
            , error: function(jqXHR, textStatus, errorThrown) {
                $("#error").text(jqXHR.responseJSON.Message);
                $("#messages").slideDown('fast');
                $("html, body").animate({'scrollTop': 0}, 700);
                window.setTimeout(function() {
                    $("#messages").slideUp('fast');
                }, 10000);
            }
        });
    }
    , initGroupSelect: function() {
        if ($(".user-groups-selector").length) {
            $select = $(".user-groups-selector");
            $select.select2();
            $select.change(function(e) {
                var id = $(this).parents("tr").attr('data-userid');
                var GroupUserID = $(this).parents("tr").attr('data-siteusergroupid');
                var SiteGroupID = $(this).find("option:selected").val();
                var method = (GroupUserID === "") ? 'post' : 'put';
                var params = {};
                var url = Services.base + Services.users;
                switch (method) {
                    case 'post':
                        params = {SiteGroupID: SiteGroupID, SiteUserID: id};
                        break;
                    case 'put':
                        params = {SiteGroupID: SiteGroupID, SiteUserID: id};
                        url += '/' + GroupUserID;
                        break;
                }
                $.ajax({
                    url: url
                    , type: method
                    , headers: {"Authorization": token}
                    , data: params
                    , success: function(d) {
                        $("#messages").find(".note-info").removeClass('hide');
                        $("#messages").slideDown('fast');
                        $("html, body").animate({'scrollTop': 0}, 700);
                        window.setTimeout(function() {
                            $("#messages").slideUp('fast').find(".note-info").addClass('hide');
                        }, 10000);
                    }
                });
            });
        }
    }
    , getData: function($obj, state) {
        params = {};
        if (typeof $obj !== "undefined") {
            var state = (typeof state !== "undefined") ? state : parseInt($obj.find(".user-state").attr('data-state'));
            var groups = $obj.find("select").val();
            var userGroups = [];
            if (groups !== null && typeof groups !== "undefined" && groups.length > 0) {
                for (var i = 0; i < groups.length; i++) {
                    userGroups.push({Id: groups[i]});
                }
            }
            var params = {
                Name: $obj.find(".user-name").text()
                , ActiveState: state
                , Groups: userGroups
            };
        }
        return params;
    }
    , saveInline: function(e, item) {
        var $this = $(e.delegateTarget);
        var $parent = $this.parents("tr");
        var id = parseInt($parent.attr('data-userid'));
        window.setTimeout(function() {
            var params = User.getData($parent);
            if (typeof id !== "undefined" && id > 0) {
                $.ajax({
                    url: Services.base + Services.users + '/' + id
                    , type: 'put'
                    , headers: {"Authorization": token}
                    , data: JSON.stringify(params)
                    , success: function(d) {
                        $parent.find(".editable-unsaved").removeClass("editable-unsaved");
                    }
                });
            }
        }, 100);
    }
    , updateGroups: function($obj) {
        var params = User.getData($obj);
        var id = $obj.attr("data-userid");
//        console.log(id + params);
        window.setTimeout(function() {
            if (typeof id !== "undefined" && id > 0) {
                $.ajax({
                    url: Services.base + Services.users + id
                    , type: 'put'
                    , headers: {"Authorization": token}
                    , data: JSON.stringify(params)
                    , success: function(d) {
                        alert("OK!");
                    }
                });
            }
        });
    }
    , resetPass: function() {
        $("#users").delegate("[data-do=reset-pass]", 'click', function(e) {
            if (confirm("This will reset user's password to \"123456\". Are you sure?") === true) {
                var $this = $(this);
                var $parent = $this.parents("tr");
                var id = parseInt($parent.attr('data-userid'));
                if (typeof id !== "undefined" && id > 0) {
                    $.ajax({
                        url: Services.base + Services.resetPass + '/' + id
//                        , type: 'put'
//                        , data: {Password: '123456'}
                        , headers: {"Authorization": token}
                        , success: function(d) {
                            $("#messages").find(".note-success").removeClass('hide');
                            $("#messages").slideDown('fast');
                            $("html, body").animate({'scrollTop': 0}, 700);
                            window.setTimeout(function() {
                                $("#messages").slideUp('fast').find(".note-success").addClass('hide');
                            }, 10000);
                        }
                    });
                }
            }
            e.preventDefault();
            return false;
        });
    }
    , changeState: function() {
        $("#users").delegate("[data-do=change-state]", 'click', function(e) {
            var $this = $(this);
            var $parent = $this.parents("tr");
            var id = parseInt($parent.attr('data-userid'));
            if (typeof id !== "undefined" && id > 0) {
                var state = parseInt($this.attr("data-change-state-to"));
                var params = User.getData($parent, state);
                $.ajax({
                    url: Services.base + Services.users + id
                    , type: 'put'
                    , headers: {"Authorization": token}
                    , data: JSON.stringify(params)
                    , success: function(d) {
                        var button = '';
                        switch (d.ActiveState) {
                            case 0:
                                button = '<a href="#" data-do="change-state" data-change-state-to="1" class="btn default red-stripe btn-xs"><i class="fa fa-times font-red"></i> Deactive</a>';
                                break;
                            case 1:
                                button = '<a href="#" data-do="change-state" data-change-state-to="0" class="btn default green-stripe btn-xs"><i class="fa fa-check font-green"></i> Active</a>';
                                break;
                        }
                        $parent.find(".user-state").html(button);
                    }
                });
            } else {
                console.log('no item found!');
            }
            e.preventDefault();
            return false;
        });
    }
    , loadData: function() {
        $.ajax({
            url: Services.base + Services.userData
            , headers: {"Authorization": token}
            , success: function(data) {
                User.showData(data);
            }
        });
    }
    , showData: function(d) {
        $(".top-menu .username").text(d.Name);
        // TODO
        $("#my-avatar").attr('src', d.ProfilePicture);
    }
    , logout: function() {
        Cookie.delete();
        window.location.href = 'login';
    }
};
var Items = {
    priorities: ["", "info", "warning", "danger"]
    , prioritiesTitles: ["Low", "Normal", "High"]
    , stateTitles: ["Unpublished", "Published", "Archived"]
    , itemsTemplate: '{{#each []}}<tr data-id="{{this.SiteItemID}}" class="{{this.Priority}}">\n\
                        <td class="item-id">{{this.SiteItemID}}</td>\n\
                        <td class="item-title"><a href="#dashboard/item/{{this.SiteItemID}}" class="show-item">{{this.Title}}</a></td>\n\
                        <td class="item-state">{{this.StateHTML}}</td>\n\
                        <td class="item-sections">{{{this.SectionsHtml}}}</td>\n\
                        <td class="item-crated">{{{this.CreateDate}}}</td>\n\
                        </tr>{{/each}}'
    , inboxSelect: ''
    , handle: function(data) {
        if (data.length >= 1) {
            $.each(data, function(i, item) {
                if (typeof item.Priority !== "undefined") // Priority as tr class
                    item.Priority = Items.priorities[parseInt(item.Priority)];
                if (typeof item.Categories !== "undefined" && item.Categories !== null) {
                    // Section labels in list table
                    var sections = '';
                    $.each(this.Categories, function() {
                        sections += '<span class="label label-info">' + this.Title + '</span> ';
                    });
                    item.SectionsHtml = sections;
                }
                /*
                 if (typeof item.IsPublished !== "undefined" && item.IsPublished !== null) {
                 // State label in list table
                 var StateHTML = '';
                 if (parseInt(item.IsPublished) === 1) {
                 StateHTML = '<span class="label label-success">Published</span>';
                 } else {
                 StateHTML = '<span class="label label-success">Unpublished</span>';
                 }
                 item.StateHTML = StateHTML;
                 }
                 */
                if (typeof item.SitePollItems !== "undefined") {
                    item.SitePollItemsCount = item.SitePollItems.length;
                }
                if (typeof item.CreateDate !== "undefined") {
                    item.CreateDate = item.CreateDate.split('.')[0].replace('T', '&nbsp;');
                }
                if (typeof item.LastLoginDate !== "undefined") {
                    item.LastLoginDate = item.LastLoginDate.split('.')[0].replace('T', '&nbsp;');
                }
                if (typeof item.ConductorDate !== "undefined") {
                    item.ConductorDate = item.ConductorDate.split('.')[0].replace('T', '&nbsp;');
                }
                if (typeof item.FilePath !== "undefined") {
                    item.Image = Services.baseMedia + item.FilePath;
                }
                if (typeof item.Tags !== "undefined" && item.Tags !== null) {
                    var tags = '';
                    $.each(item.Tags, function() {
                        tags += '<span class="label label-success">' + this.name + '</span>&nbsp;';
                    });
                    item.TagsHtml = tags;
                }
                if (typeof item.Groups !== "undefined") {
                    var userGroups = $(Global.userGroups);
                    item.SiteGroupUsersHtml = '<select class="form-control select2" name="Groups" multiple>';
                    $.each(item.Groups, function(i, item) {
                        userGroups.find('option[value=' + item.Id + ']').attr('selected', 'selected');
                    });
                    item.SiteGroupUsersHtml += userGroups.html();
                    item.SiteGroupUsersHtml += '</select>';
                }
            });
        }
        if (typeof data.ItemPriority !== "undefined") {
            data.PriorityTitle = Items.prioritiesTitles[parseInt(data.ItemPriority)];
        }
        if (typeof data.IsPublished !== "undefined") {
            data.StateTitle = Items.stateTitles[parseInt(data.IsPublished)];
        }
        if (typeof data.Kind !== "undefined") {
	    switch(data.Kind) {
		case 1:
		    data.mediaKind = 'image';
		    break;
		case 2:
		    data.mediaKind = 'video';
		    break;
	    }
        }
        // Handle created date for single item
        if (typeof data.CreateDate !== "undefined") {
            data.CreateDate = data.CreateDate.split('.')[0].replace('T', '&nbsp;');
        }
        if (typeof data.ModifiedDate !== "undefined") {
            data.ModifiedDate = data.ModifiedDate.split('.')[0].replace('T', '&nbsp;');
        }
        // Item edit pre-selected sections
        if (typeof data.Categories !== "undefined" && data.Categories !== null) {
            var SectionIDs = [];
            data.SectionsHtml = '';
            $.each(data.Categories, function(i, section) {
                SectionIDs.push({id: section.Id});
                // Section Labels
                data.SectionsHtml += '<span class="label label-info">' + section.Title + '</span> ';
            });
            data.SectionIDs = JSON.stringify(SectionIDs);
        }
        if (typeof data.Tags !== "undefined") {
            var TagIDs = [];
            data.TagsHtml = '';
            $.each(data.Tags, function(i, tag) {
                data.TagsHtml += '<span class="label label-info">' + tag.name + '</span> ';
                TagIDs.push({id: tag.id, name: tag.name});
            });
            data.TagIDs = JSON.stringify(TagIDs);
        }
        if (typeof data.Repositories !== "undefined") {
            data.Repositories.ImageUrl = [];
            $.each(data.Repositories, function(i, media) { // TODO
                switch (media.Kind) {
                    case 1:
                        data.Repositories[i].ImageUrl = Services.baseMedia + media.FilePath;
//                        data.Repositories[i].Type = 1;
			data.Repositories[i].mediaKind = 'image';
                        break;
                    case 2:
                    case 'video':
                    case 'master':
//			data.Repositories[i].Type = 2;
			data.Repositories[i].mediaKind = 'video';
//                        ImageUrl = '';
                        break;
                }
            });
        }
        if (typeof data.Flow !== "undefined" && data.Flow !== null) {
            data.FlowHtml = '';
            $.each(data.Flow, function(i, flow) {
                if (flow.GroupName !== null) {
                    name = flow.GroupName;
                } else if (flow.UserName !== null) {
                    name = flow.UserName;
                }
                data.FlowHtml += '<span class="label label-success">' + name + '</span> ';
            });
        }
        return data;
    }
    , reOrder: function(obj, method) {
        method = (typeof method === "undefined") ? 'put' : method;
        if ($(obj).length) {
            var orderings = [];
            $(obj).find(".item").each(function(i, item) {
                orderings.push({
                    Priority: (i + 1)
                    , Contents_Id: parseInt($(item).attr('data-id'))
                    , Categories_Id: Location.parts[2]
                });
            });
            $.ajax({
                url: Services.base + Services.sectionOrder
                , method: method
                , headers: {"Authorization": token}
                , data: JSON.stringify(orderings)
                , success: function(data) {
                    $("#messages").slideDown('fast');
                    $("html, body").animate({'scrollTop': 0}, 700);
                    window.setTimeout(function() {
                        $("#messages").slideUp('fast');
                    }, 10000);
                }
            });
        }
    }
    , send: function(form, method, results, parameters) {
        if (typeof parameters === "undefined") {
            var params = $(form).serializeObject();
            if (params.group === '0') {
                alert('You need to specify a receiver!');
                return false;
            }
        } else {
            var params = parameters;
        }
        if (parseInt(params.User_To) === 0) {
            params.User_To = '-' + params.group;
        }
        delete(params['group']);
        $.ajax({
            url: Services.base + Services.siteFlow
            , type: method
            , headers: {"Authorization": token}
            , data: JSON.stringify(params)
            , success: function(d) {
                $(results).html("<blockquote>Item has been sent.</blockquote>");
//                alert("success");
            }
        });
    }
    , initSelects: function(interval) {
        var intv = window.setInterval(function() {
            // Change-user-groups select
            if ($("select#change-inbox").length) {
                $inboxGroupSelect = $("#change-inbox");
                if ($inboxGroupSelect.html() === '') {
                    if (Items.inboxSelect === '') {
                        $.ajax({
                            url: Services.base + Services.myGroups
                            , type: 'get'
                            , headers: {"Authorization": token}
                            , success: function(d) {
                                Items.inboxSelect = '<option value="0">My Inbox</option>';
                                Items.inboxSelect += '<optgroup label="Your Groups">';
                                $.each(d, function(i, item) {
                                    Items.inboxSelect += '<option value="' + item.Id + '">' + item.Title + '</option>';
                                });
                                Items.inboxSelect += '</optgroup>';
                            }
                            , fail: function() {
                                alert('failed to get your groups!');
                            }
                        });
                    }
                    $inboxGroupSelect.html(Items.inboxSelect);
                }
                var currentId = Location.parts[2];
                if (typeof currentId !== "undefined" && currentId !== null) {
                    $inboxGroupSelect.find('option[value=' + currentId + ']').attr('selected', 'selected');
                }
            }
            // Change-caregories select
            if ($("select#change-category").length) {
                if ($("#change-category").html() === '') {
                    if (Form.sectionsHtml !== '') {
                        var html = '<option value="0">All Categories</option>';
                        $("#change-category").html(html + Form.sectionsHtml);
                    }
                }
                var currentId = Location.parts[2];
                if (typeof currentId !== "undefined" && currentId !== null) {
                    $("#change-category").find('option[value=' + currentId + ']').attr('selected', 'selected');
                }
            }
            // Change-caregories-ordering select
            if ($("select#change-ordering-category").length) {
                if ($("#change-ordering-category").html() === '') {
                    if (Form.sectionsHtml !== '') {
                        var html = '<option value="0">All Categories</option>';
                        $("#change-ordering-category").html(html + Form.sectionsHtml);
                    }
                }
                var currentId = Location.parts[2];
                if (typeof currentId !== "undefined" && currentId !== null) {
                    $("#change-ordering-category").find('option[value=' + currentId + ']').attr('selected', 'selected');
                }
            }
            // Change-state select
            if ($("select#change-state").length) {
                var currentState = Location.parts[3];
                if (typeof currentState !== "undefined" && currentState !== null) {
                    $("#change-state").find('option[value=' + currentState + ']').attr('selected', 'selected');
                }
            }
            if ($("select#change-videos-state").length) {
                var currentState = Location.parts[3];
                if (typeof currentState !== "undefined" && currentState !== null) {
                    $("#change-videos-state").find('option[value=' + currentState + ']').attr('selected', 'selected');
                }
            }
            // Change-kind select
            if ($("select#change-kind").length) {
                var currentKind = Location.parts[2];
                if (typeof currentKind !== "undefined" && currentKind !== null) {
                    $("#change-kind").find('option[value=' + currentKind + ']').attr('selected', 'selected');
                }
            }
        }, interval);
    }
};
var Blocks = {
    container: '#mainbody'
    , init: function(parts) {
        Blocks.clear();
        Blocks.load(Blocks.parse(parts));
        Blocks.resetHeight();
    }
    , load: function(request) {
        var tmpl = Templates.load(request, '');
        var method = Blocks.getMethod(request);
        if (tmpl) {
            if (method !== '') {
                $.ajax({
                    url: Services.base + method
                    , headers: {"Authorization": token}
                    , global: false
                    , success: function(data) {
                        Blocks.attach(tmpl, data);
                    }
                });
            } else {
                Blocks.attach(tmpl, []);
            }
        } else {
            Blocks.raiseError('404 - Page not found!', 'Please contact administrator.');
        }
    }
    , attach: function(tmpl, data) {
        var items = Items.handle(data);
        var template = $(tmpl).html();
        var handlebarsTemplate = Handlebars.compile(template);
        var output = handlebarsTemplate(items);
        $("#mainbody").html(output);
        Blocks.afterAttach();
    }
    , afterAttach: function() {
        User.initInbox();
        if ($(Blocks.container).find("table").length)
            TableAdvanced.init();
        if ($("#mainbody").find("form").length)
            Form.afterRender();
        if ($(".sortable").length)
            $(".sortable").sortable();
        if ($("#comments").length)
            Comment.init();
        if ($("#tags").length)
            Tag.init();
        if ($("#images").length) {
            Media.initList();
            Media.changeState();
        }
        if ($("#users").length)
            User.initManage();
        if ($("#item-edit").length) {
            Form.checkIn();
            Form.initAutoSave();
        }
        if ($("#cont-file-upload").length)
            Contributor.initUploader();
        if ($("#mainbody").find("#remove-unread").length)
            Notifications.removeUnread();
        if ($("[data-picker=datepicker]").length)
            Global.initDatepicker();
        if ($("#destination-select").length)
            $("#destination-select").html(Global.userGroups).find("select").select2();
        if ($(".select2").length) {
//	    $(".select2").each(function() {
//		alert();
//		$('select2').select2_sortable();
//	    });
            $(".select2").select2();
	}

    }
    , getMethod: function(request) {
        var method = 'index';
        switch (request.model) {
            case 'dashboard':
                method = '';
                switch (request.view) {
                    default:
                        if (request.id !== 'undefined' && request.id !== null) {
                            method = Services.items + request.id;
                        } else {
                            method = Services.items + 'box/0';
                        }
                        break;
                    case 'draft':
                    case 'index':
                        method = Services.items + 'box/-100';
                        break;
                    case 'sent':
                        method = Services.sent;
                        break;
                    case 'inbox':
                        method = Services.items;
                        if (typeof request.id === 'undefined' || request.id === '' || request.id === null || request.id === 0 || request.id === '0') {
                            method = Services.inbox + '/0';
                        } else {
                            method = Services.inbox + '/-' + request.id;
                        }
                        break;
                    case 'items':
                        method = Services.items;
                        if (typeof request.id === 'undefined' || request.id === '' || request.id === null || request.id === 0 || request.id === '0') { // no id present
                            if (typeof request.state === 'undefined' || request.state === '' || request.state === null || request.state === -1 || request.state === '-1') { // no state present
                                method += 'list/0/?Ordering=List&published=-1'; // no id and no state present
                            } else { // state present
                                method += 'list/0/?Ordering=List&published=' + request.state; // no id but state is present
                            }
                        } else { // id is present
                            if (typeof request.state === 'undefined' || request.state === '' || request.state === null || request.state === -1 || request.state === '-1') { // no state present
                                method += 'list/' + request.id + '/?Ordering=List&published=-1'; // id is present but no state
                            } else {
                                method += 'list/' + request.id + '/?Ordering=List&published=' + request.state; // id and state present
                            }
                        }
                        break;
                    case 'ordering':
                        method = Services.items;
                        if (typeof request.id === 'undefined' || request.id === '' || request.id === null || request.id === 0 || request.id === '0') {
                            method += 'list/0/?published=1&Ordering=Ordering';
                        } else {
                            method += 'list/' + request.id + '/?published=1&Ordering=Ordering';
                        }
                        break;
                }
                break;
            case 'user':
                method = Services.userInbox;
                switch (request.view) {
                    case 'profile':
                        method = Services.user;
                        break;
                    case 'logout':
                        User.logout();
                        return false;
                        break;
                    case 'manage':
                        method = Services.usersManage;
                        break;
                }
                break;
            case 'messages':
                switch (request.view) {
                    case 'index':
                    case 'inbox':
                        method = Services.userInbox;
                        break;
                    case 'sent':
                        method = Services.userSent;
                        break;
                }
                break;
            case 'wires':
                method = Services.wires;
                if (request.view !== '') {
                    if (request.view === 'item') {
                        method = Services.wires + '/' + request.id;
                    } else {
                        method = Services.wires + '/?query=' + request.view;
                    }
                }
                break;
            case 'poll':
                method = Services.polls;
                if (request.id !== 'undefined' && request.id !== null) {
                    method = Services.polls + '/' + request.id;
                }
                break;
            case 'comment':
                method = '';
                if (request.view !== "search") {
                    method = Services.comments;
                }
                break;
            case 'section':
                method = '';
                if (request.view !== "search")
                    method = Services.sectionItems + '/' + request.id;
                break;
            case 'headlines':
                method = Services.sectionItems + '/' + 2;
                break;
            case 'schedule':
                method = Services.schedule;
                if (request.id !== 'undefined' && request.id !== null) {
                    method = Services.schedule + '/?added=' + request.id;
                }
                break;
            case 'tag':
                method = Services.tags;
                break;
            case 'contributors':
                method = Services.contributors;
                break;
            case 'media':
                method = Services.media;
                break;
            case 'videos':
                method = '';
                if (request.view !== '') {
                    switch (request.view) {
                        case 'items':
                        case 'index':
                            method = Services.programs;
                            if (typeof request.id === 'undefined' || request.id === '' || request.id === null || request.id === 0 || request.id === '0') { // no id present
                                if (typeof request.state === 'undefined' || request.state === '' || request.state === null || request.state === -1 || request.state === '-1') { // no state present
                                    method += '/?Kind=-1&Status=-1'; // no id and no state present
                                } else { // state present
                                    method += '/?Kind=-1&Status=' + request.state; // no id but state is present
                                }
                            } else { // id is present
                                if (typeof request.state === 'undefined' || request.state === '' || request.state === null || request.state === -1 || request.state === '-1') { // no state present
                                    method += '/?Kind=' + request.id + '&Status=-1'; // id is present but no state
                                } else {
                                    method += '/?Kind=' + request.id + '&Status=' + request.state; // id and state present
                                }
                            }
                            break;
                        case 'item':
                        case 'edit':
                        case 'addEpisode':
                            method = Services.program + request.id;
                            break;
                        case 'episode':
                        case 'editEpisode':
                            method = Services.episode + '/' + request.id;
                            break;
                    }
                }
                break;
        }
        return method;
    }
    , raiseError: function(msg, msg2) {
        var $container = $(Blocks.container);
        if (typeof msg2 === "undefinded")
            msg2 = '';
        $container.html('<div class="note note-danger"><h4 class="block">' + msg + '</h4><p>' + msg2 + '</p></div>');
    }
    , parse: function(parts) {
        if (parts instanceof Array && parts[0] !== '') {
            var output = {
                'model': parts[0]
                , 'view': (Global.isInt(parts[1])) ? null : parts[1]
                , 'id': (Global.isInt(parts[2])) ? parts[2] : null
                , 'state': (Global.isInt(parts[3])) ? parts[3] : null
            };
            return output;
        } else {
            return {'model': 'dashboard', 'view': 'index', 'id': null};
        }
    }
    , setHeight: function() {
        var $container = $(Blocks.container);
        $container.css({'height': $container.height()});
    }
    , resetHeight: function() {
        var $container = $(Blocks.container);
        $container.css({'height': ''});
    }
    , clear: function() {
        var $container = $(Blocks.container);
        Blocks.setHeight();
        $container.empty();
    }
    , headers: function(parts) {
        Blocks.breadcrumbs(parts);
        Blocks.pageHeader(parts);
    }
    , breadcrumbs: function(parts) {
        var output = '';
        if (typeof parts !== "undefined") {
            output += '<li><i class="fa fa-sitemap"></i> ' + Global.ucfirst(parts.model) + '</li>';
            if (typeof parts.view !== "undefined")
                output += '<li>&nbsp;<i class="fa fa-angle-right"></i>' + Global.ucfirst(parts.view) + '</li>';
            if (parts.id !== null)
                output += '<li>&nbsp;<i class="fa fa-angle-right"></i>' + parts.id + '</li>';
        }
        $("ul.page-breadcrumb").html(output);
    }
    , pageHeader: function(parts) {
        $pageTitle = $("h3.page-title");
        $pageTitle.text(Global.ucfirst(parts.model));
    }
};
var Templates = {
    path: 'templates/'
    , $container: '#templates'
    , load: function(request, callback) {
        var view = (typeof request.view === "undefined") ? 'index' : request.view;
        if (!Templates.checkIfExists(request.model, view)) { // console.log('template does not exist in page, trying to load it via ajax');
            callback = 'empty';
            $.ajax({
                url: Templates.path + request.model + '/' + view + '.html'
                , async: false
                , headers: {"Authorization": token}
                , success: function(realData) {
                    $(Templates.$container).append(realData);
                    callback = '#' + request.model + '-' + view + '-template';
                }
                , error: function() {
                    callback = false;
                }
            });
            return callback;
        } else { // console.log('template exista, returning it\'s id');
            return '#' + request.model + '-' + view + '-template';
        }
        return false;
    }
    , checkIfExists: function(model, view) {
        if ($('#' + model + '-' + view + '-template').length) {
            return true;
        } else {
            return false;
        }
    }
};
var Services = {
    base: 'http://WIN-L5QSB3HRGRD:81/pl/'
    , 'items': 'contents.svc/'
    , 'sections': 'categories.svc/'
    , 'sectionOrder': 'contents.svc/updatePriority'
//    , 'sectionItems': 'items/getbysection'
    , 'notSeen': 'contentsflow.svc/inbox/notseen'
    , 'inbox': 'contentsflow.svc/inbox'
    , 'sent': 'contentsflow.svc/sent'
    , 'userData': 'users.svc/current'
    , 'userInbox': 'users.svc/messages/inbox' // TODO
    , 'userSent': 'users.svc/messages/sent'
    , 'checkMsg': 'users.svc/messages/seen/'
//    , 'userDraft': 'siteItemFlow?type=draft'
    , 'siteFlow': 'contentsflow.svc/sendFlow'
    , 'user': 'users.svc/current'
    , 'users': 'users.svc/'
    , 'usersManage': 'users.svc/manage/'
    , 'resetPass': 'users.svc/resetpassword'
    , 'userGroups': 'users.svc/groups'
    , 'myGroups': 'users.svc/mygroups'
    , 'polls': 'polls.svc/'
    , 'comments': 'comments.svc/'
//    , 'wires': 'wire'
    , 'tags': 'tags.svc/'
//    , 'afp': 'afpPic'
//    , 'temp': 'temp'
    , 'messages': 'users.svc/messages/inbox?limit=8'
    , 'baseMedia': 'http://217.218.67.243/Images/Original/'
    , 'media': 'repositories.svc/'
    , 'tempUpload': 'repositories.svc/upload'
    , 'tempCrop': 'repositories.svc/crop'
//    , 'afpUpload': 'afptransfer/postasync'
//    , 'schedule': 'conductor'
//    , 'contributors': 'contributor'
//    , 'staticUpload': 'staticTransfer'
    , 'videos': 'http://192.168.55.136/mc.svc/files/Search/50/files'
    , 'video': 'http://192.168.55.136/mc.svc/files/'
    , programs: 'programs.svc/programs'
    , program: 'programs.svc/program/'
    , episodes: 'programs.svc/episodes/'
    , episode: 'programs.svc/episode/'
};
var Cookie = {
    lifetime: 1209600 // exp in minutes
    , title: 'htvpanel='
    , init: function() {
        var Cookie = this;
    }
    , extend: function(id, username, cname) {
        // Cookie.delete();
        Cookie.set(id, username, cname);
        debug && console.log('Cookies: Session Extended');
        return true;
    }
    , check: function() {
        return Cookie.get(Cookie.title);
    }
    , parse: function(data) {
        if (typeof data !== 'undefined') {
            return data;
        }
        return false;
    }
    , delete: function(cname) {
        if (typeof cname === 'undefined')
            var cname = Cookie.title;
        var expires = 'Thu, 01 Jan 1970 00:00:01 GMT';
        document.cookie = cname + '' + '; ' + expires + '; path=/';
    }
    , set: function(token) {
        // validating paramters
        var cname = Cookie.title;
        var data = token;
        var d = new Date();
        d.setTime(d.getTime() + (Cookie.lifetime * 1000));
        var expires = 'expires=' + d.toGMTString();
        document.cookie = cname + data + '; ' + expires + '; path=/';
        return data;
    }
    , get: function(name) {
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
var Form = {
    sectionsHtml: ''
    , handlebarsSections: ''
    , autosaveInterval: ''
    , listTree: []
    , idx: 0
    , messageTmpl: '<div class="note note-danger"><h4 class="block">{title}</h4><p>{msg}</p></div>'
    , beforeRender: function() {
        Form.initSections();
    }
    , afterRender: function() {
        Form.tags();
        Form.editor();
        Uploader.init();
        Form.initPreselectedSections();
        Form.initPreselectedTags();
        Form.initTooltip();
        $("#sections-select").select2Sortable({
            matcher: function(term, text) {
                return text.toUpperCase().indexOf(term.toUpperCase()) === 0;
            }
        });
        if ($("#item-media ul").find("li").length)
            $("#item-media ul").sortable();
        if ($("#destination-select").length)
            $("#destination-select").html(Global.groupsusers).find("select").select2();
    }
    , initSections: function() {
        $.ajax({
            url: Services.base + Services.sections
            , headers: {"Authorization": token}
            , cache: true
            , success: function(data) {
                Form.registerSections(data);
            }
        });
    }
    , registerSections: function(sections) {
        Form.sectionsHtml = '';
        for (var i = 0; i < sections.length; i++) {
            sections[i].childs = [];
            sections[i].depth = 0;
        }
        Form.buildSectionTree(sections);
        for (var i = 0; i < Form.listTree.length; i++) {
            if (Form.listTree[i].childs.length === 0) {
		Form.sectionsHtml += "<optgroup label='" + Form.listTree[i].Title + "'>";
                Form.sectionsHtml += "<option value='" + Form.listTree[i].Id + "'>" + Form.listTree[i].Title + "</option>";
		Form.sectionsHtml += "</optgroup>";
	    } else {
                Form.sectionsHtml += "<optgroup label='" + Form.listTree[i].Title + "'>";
		Form.sectionsHtml += "<option value='" + Form.listTree[i].Id + "'>" + Form.listTree[i].Title + "</option>";
                for (var j = 0; j < Form.listTree[i].childs.length; j++)
                    Form.sectionsHtml += "<option value='" + Form.listTree[i].childs[j].Id + "'>" + Form.listTree[i].childs[j].Title + "</option>";
                Form.sectionsHtml += "</optgroup>";
            }
        }
//        for (var i = 0; i < sections.length; i++) {
//            Form.BuidSectionOptions(sections[i]);
//        }
        Form.registerHelper();
    }
    , registerHelper: function() {
        if (Form.handlebarsSections === '') {
            Form.handlebarsSections = Form.sectionsHtml;
        }
        Handlebars.registerHelper('sections', function() {
            return Form.handlebarsSections;
        });
    }
    , buildSectionTree: function(tree, item) {
        while (Form.idx < tree.length) {
            if (String(tree[Form.idx].Parent_Id) === "0") {
                var itm = tree[Form.idx];
                Form.listTree.push(itm);
                Form.idx++;
                while (Form.idx < tree.length && tree[Form.idx].Parent_Id === itm.Id) {
                    Form.buildSectionTree(tree, itm);
                }
            } else {
                var itm = tree[Form.idx];
                item.childs.push(itm);
                Form.idx++;
                while (Form.idx < tree.length && tree[Form.idx].Parent_Id === itm.Id) {
                    Form.buildSectionTree(tree, itm);
                }
            }
        }
////        console.log(tree);
//        if (typeof item !== "undefined") {
//            for (var i = 0; i < tree.length; i++) {
//                if (String(tree[i].Parent_Id) === String(0)) {
//                    item.depth = tree[i].depth + 1;
//                    tree[i].childs.push(item);
//                    break;
//                }
//                else
//                    Form.buildSectionTree(tree[i].childs, item);
//            }
//        } else {
//            var idx = 0;
//            while (idx < tree.length)
////                if (tree[idx].Parent_Id !== 0)
//                    Form.buildSectionTree(tree, tree.splice(idx, 1)[0]);
////                else
//                    idx++;
//        }
    }
    , indentGen: function(n) {
        var ret = '';
        for (i = 0; i < n - 1; i++)
            ret += '&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;';
        if (n > 0)
            ret += '|---';
        return ret;
    }
    , initTooltip: function() {
        if ($("[data-toggle=tooltip]").length) {
            $("[data-toggle=tooltip]").tooltip();
        }
    }
    , tags: function() {
        var ms = $("#tag-suggest").magicSuggest({
            maxSuggestions: 10
            , maxSelection: 20
	    , minChars: 2
            , allowFreeEntries: false
            , hideTrigger: true
            , useCommaKey: true
            , useZebraStyle: true
            , typeDelay: 500
            , method: 'get'
            , data: Services.base + Services.tags
        });
        var mms = $("#media-tag-suggest").magicSuggest({
            maxSuggestions: 10
            , maxSelection: 20
            , allowFreeEntries: false
            , hideTrigger: true
            , useCommaKey: true
            , useZebraStyle: true
            , typeDelay: 500
            , method: 'get'
            , data: Services.base + Services.tags
        });
        var ams = $("#afp-tag-suggest").magicSuggest({
            maxSuggestions: 10
            , maxSelection: 20
            , allowFreeEntries: false
            , hideTrigger: true
            , useCommaKey: true
            , useZebraStyle: true
            , typeDelay: 500
            , method: 'get'
            , data: Services.base + Services.tags
        });
        if ($("#tags-map").length) {
            var $map = $("#tags-map");
            if ($.trim($map.text()) !== "") {
                var items = JSON.parse($map.text());
                ms.addToSelection(items);
            }
        }
    }
    , editor: function(obj) {
        var el = (typeof obj !== "undefined") ? obj : ".has-editor";
        $(el).ckeditor();
    }
    , raiseError: function($obj, msg) {
        var alert = Form.messageTmpl.replace(/{title}/g, 'Error saving item!').replace(/{msg}/g, msg);
        $("#messages").empty().html(alert).slideDown('fast', function() {
            $("html, body").animate({'scrollTop': 0}, 700);
            window.setTimeout(function() {
                $("#messages").slideUp('fast');
            }, 10000);
        });
    }
    , submit: function(forms, method, endpoint, id, nextAction) {
        var e = []
                , fs = forms.split(",")
                , oParams = []
                , params = {};
        for (var f = 0; f < fs.length; f++) {
            if ($(fs[f]).find("[required]").length) {
                $(fs[f]).find("[required]").each(function() {
                    var tagName = $(this).prop('tagName').toLowerCase();
                    if ($(this).val() === '') {
                        e.push($(this));
                    }
                });
            }
        }
        if (e.length > 0) {
            $.each(e, function(i, o) {
                e[i].parent().addClass('has-error');
            });
            Form.raiseError(e[0], 'Please make sure no required field is empty.');
            return false;
        }
        for (var i = 0; i < fs.length; i++) {
            oParams[i] = $(fs[i]).serializeObject();
        }
        for (instance in CKEDITOR.instances)
            CKEDITOR.instances[instance].updateElement();
        if ($("[name=Body]").val() === "") {
            Form.raiseError('', 'Body cannot be empty.');
            return false;
        } else {
            oParams[0]["Body"] = $("[name=Body]").val();
        }
        for (var j = 0; j < oParams.length; j++) {
            for (var attr in oParams[j]) {
                params[attr] = oParams[j][attr];
            }
        }
        params["Repositories"] = [];
        if (typeof params["media[]"] !== "undefined") {
            if (typeof params["media[]"] === "string") {
		var kind = params["kind[]"];
                var relPath = params["filePath[]"];
                params["Repositories"].push({Id: params["media[]"], Priority: 1, Kind: kind, FilePath: relPath});
            } else {
                var media = params["media[]"];
//                var mediaE = params["mediaE[]"];
                var kind = params["kind[]"];
                var relPath = params["relPath[]"];
                for (var m = 0; m < media.length; m++) {
                    params["Repositories"].push({Id: media[m], Priority: (m + 1), Kind: kind[m], FilePath: relPath[m]});
                }
            }
            delete params['media[]'];
            delete params['mediaE[]'];
            delete params['kind[]'];
            delete params['filePath[]'];
            delete params['SiteAttachmentID[]'];
        } else {
//            Form.raiseError('', 'At least one media should be selected.');
//            return false;
        }
        params["Tags"] = [];
        if (typeof params["tags[]"] !== "undefined") {
            if (typeof params["tags[]"] === "string") {
                params["Tags"].push({Id: params["tags[]"]});
            } else {
                $.each(params['tags[]'], function(i, tag) {
                    params["Tags"].push({Id: tag});
                });
            }
            delete params['tags[]'];
        } else {
//            Form.raiseError('', 'Tags field cannot be empty.');
//            return false;
        }
        params["Categories"] = [];
        if (typeof params["sections[]"] !== "undefined") {
            if (typeof params["sections[]"] === "string") {
                params["Categories"].push({Id: params["sections[]"]});
            } else {
                $.each(params['sections[]'], function(i, o) {
                    params["Categories"].push({Id: o});
                });
            }
            delete params['sections[]'];
        } else {
            // Form.raiseError('', 'At least one section should be selected.');
//            return false;
        }
        /*
         params["SiteFlows"] = [];
         var SiteUserID = null, SiteGroupID = null;
         if (typeof params["Group"] !== "undefined") {
         if (params["Group"].indexOf('u') === 0)
         SiteUserID = parseInt(params["Group"].replace('u', ''));
         else if (params["Group"].indexOf('g') === 0)
         SiteGroupID = parseInt(params["Group"].replace('g', ''));
         }
         params["SiteFlows"].push({SiteGroupID: SiteGroupID, SiteUserID: SiteUserID});
         delete(params["Group"]);
         */
        var url = Services.base + Services.items;
        url += (typeof id !== "undefined" && id !== null) ? +id : '';
//        console.log(id); return;
        $.ajax({
            url: url
            , headers: {"Authorization": token}
            , type: method
            , data: JSON.stringify(params)
            , success: function(d) {
                if (typeof nextAction !== "undefined") {
                    if (nextAction === "$$")
                        Form.raiseAutosaveMsg();
                    else
                        Location.redirect(false, nextAction);
                } else {
                    Location.redirect(false, 'dashboard/items');
                }
            }
            , fail: function(jqXHR, textStatus, errorThrown) {
                alert(textStatus);
            }
        });
        return;
    }
    , initPreselectedSections: function() {
        if ($("#sections-map").length) {
            var $map = $("#sections-map");
            if ($.trim($map.text()) !== "") {
                var items = JSON.parse($map.text());
                $.each(items, function(i, item) {
                    $('#sections-select option[value=' + item.id + ']').attr('selected', 'selected');
                });
            }
        }
    }
    , initPreselectedTags: function() {

    }
    , initAutoSave: function() { // TODO
//        if ($("select[name=State]").length) {
//            var state = $("select[name=State] option:selected").val();
//            if (parseInt(state) === 0) {
//                Form.autosaveInterval = setInterval(Form.autoSave, 30000);
//            }
//        }
    }
    , autoSave: function() {
        $("#autosave-button").trigger('click');
    }
    , raiseAutosaveMsg: function() {
        $(".autosave-msg").fadeIn(500).delay(3500).fadeOut(500);
    }
    , clearAutosaveTimeout: function() {
        if (Form.autosaveInterval !== "undefined")
            clearInterval(Form.autosaveInterval);
    }
    , checkIn: function() {
        var id = $("#item-edit").attr('data-itemid');
        var editing = $("#item-edit").attr('data-editing');
        if (parseInt(editing) === 0) {
            $.ajax({
                url: Services.base + Services.items + '/' + id + '/?query=editing'
                , type: 'put'
                , headers: {"Authorization": token}
                , success: function(d) {

                }
            });
        }
    }
};
var Comment = {
    ////////////////////////////// TODO: load from template file ///////////////////////////////////
    itemsTemplate: '{{#each []}}<tr data-id="{{this.Id}}">\n\
                    <td class="item-id">{{this.Id}}</td>\n\
                    <td class="item-itemid"><a href="http://217.218.67.246/newsdetail/item/{{this.SiteItemID}}/alias#comment-{{this.SiteCommentID}}" target="_blank">{{this.Content_Id}}</a></td>\n\
                    <td class="item-name"><span class="x-editable" data-type="text" data-title="Edit Name" data-name="name">{{this.Name}}</span></td>\n\
                    <td class="item-comment"><span class="x-editable" data-type="textarea" data-title="Edit Comment" data-name="comment">{{this.Text}}</span></td>\n\
                    <td class="item-ip">{{this.IP}}</td>\n\
                    <td class="item-date">{{this.Datetime_Insert}}</td>\n\
                    <td class="item-reply">{{#ifCondNot this.Parent_Id 0}}<span class="show-reply">{{this.Parent_Id}} <i class="fa fa-circle-o-notch fa-spin font-blue-hoki hide"></i></span>{{/ifCondNot}}</td>\n\
                    <td class="item-state" data-state="{{this.Published}}">{{#ifCond this.Published 1}}<a href="#" data-do="change-state" data-change-state-to="0" class="btn default green-stripe btn-xs"><i class="fa fa-check"></i> Published</a>{{/ifCond}}{{#ifCond this.Published 0}}<a href="#" data-do="change-state" data-change-state-to="1" class="btn default red-stripe btn-xs"><i class="fa fa-times"></i> Unpublished</a>{{/ifCond}}</td>\n\
                    <td class="item-trash" data-state="{{this.Published}}"><a href="#" data-do="change-state" data-change-state-to="-1" class="btn default red-stripe btn-xs"><i class="fa fa-trash-o"></i> Trash</a></td>\n\
                    </tr>{{/each}}'
    , itemState: '<a href="#" data-do="change-state" data-change-state-to="{newState}" class="btn default {color}-stripe btn-xs"><i class="fa fa-{icon}"></i> {text}</a>'
    , init: function() {
        if ($(".x-editable").length) {
            $(".x-editable").editable({
                send: 'never'
            }).on('save', function(e, params) {
                Comment.saveInline(e, params);
            });
        }
        Comment.changeState();
        Comment.highlightCurseWords();
    }
    , highlightCurseWords: function() {
        var wordlist = ['4r5e', '5h1t', '5hit', 'b!tch', 'bi+ch', 'blow job', 'carpet muncher', 'cock', 'suck', 'cunt', 'ejaculate', 'fuck', 'boob', 'breast', 'coon', 'cock', 'd1ck', 'dick', 'dike', 'dipshit', 'dildo', 'cunt', 'dink', 'f u c k', 'f_u_c_k', 'gayass', 'homo', 'hotsex', 'jerk', 'jerk off', 'kunt', 'mothafuckin', 'penis', 'orgasm', 'pecker', 'pawn', 'slut', 'poop', 'porno', 'shit', 'vagina', 'whore', 'xxx'];
        for (i = 0; i < wordlist.length; i++) {
            $('.item-name').highlight(wordlist[i]);
            $('.item-comment').highlight(wordlist[i]);
        }
    }
    , getData: function($obj, state) {
        params = {};
        if (typeof $obj !== "undefined") {
            var state = (typeof state !== "undefined") ? state : parseInt($obj.find(".item-state").attr('data-state'));
            var params = {
                Name: $obj.find(".item-name").text()
                , Text: $obj.find(".item-comment").text()
                , Published: state
            };
        }
        return params;
    }
    , saveInline: function(e, item) {
        var $this = $(e.delegateTarget);
        var $parent = $this.parents("tr");
        var id = parseInt($parent.attr('data-id'));
        window.setTimeout(function() {
            var params = Comment.getData($parent);
            if (typeof id !== "undefined" && id > 0) {
                $.ajax({
                    url: Services.base + Services.comments + id
                    , type: 'put'
                    , headers: {"Authorization": token}
                    , data: JSON.stringify(params)
                    , success: function(d) {
                        $parent.find(".editable-unsaved").removeClass("editable-unsaved");
                    }
                });
            }
        }, 100);
    }
    , changeState: function() {
        $("#comments").delegate("[data-do=change-state]", 'click', function(e) {
            var $this = $(this);
            var $parent = $this.parents("tr");
            var id = parseInt($parent.attr('data-id'));
            if (typeof id !== "undefined" && id > 0) {
                var state = parseInt($this.attr("data-change-state-to"));
                var params = Comment.getData($parent, state);
                $.ajax({
                    url: Services.base + Services.comments + id
                    , type: 'put'
                    , headers: {"Authorization": token}
                    , data: JSON.stringify(params)
                    , success: function(d) {
                        if (state === -1) {
                            $parent.slideUp(500, function() {
                                $(this).remove();
                            });
                        } else {
                            switch (d.Published) {
                                case 0:
                                    var html = Comment.itemState.replace(/{newState}/g, 1).replace(/{color}/g, 'red').replace(/{icon}/g, 'times').replace(/{text}/g, 'Unpublished');
                                    break;
                                case 1:
                                    var html = Comment.itemState.replace(/{newState}/g, 0).replace(/{color}/g, 'green').replace(/{icon}/g, 'check').replace(/{text}/g, 'Published');
                                    break;
                            }
                            $this.parent().html(html);
                        }
                    }
                });
            } else {
                console.log('no item found!');
            }
            e.preventDefault();
            return false;
        });
    }
};
var Contributor = {
    msg: '<blockquote>"{user}" has been added.</blockquote>'
    , add: function(form, method, results) {
        var data = $(form).serializeObject();
        $.ajax({
            url: Services.base + Services.contributors
            , type: method
            , headers: {"Authorization": token}
            , data: data
            , success: function(d) {
                $(results).append(Contributor.msg.replace(/{user}/g, data.Name));
            }
        });
    }
    , initUploader: function() {
        var $uploader = $("#cont-file-upload");
        $uploader.fineUploader({
            request: {endpoint: Services.base + Services.staticUpload}
            , multiple: false
            , template: 'qq-template'
        }).on('complete', function(event, id, filename, responseJSON) {
            if (responseJSON.success) {
                $("#file-upload").slideUp('fast');
                $("input[name=StaticAddress]").val(responseJSON.filename);
                $("#image-preview").append('<div class="img" stye="width: 100%;"><img src="' + Services.baseMedia + '/' + Services.temp + '/' + responseJSON.filename + '" alt="" /></div>');
            } else {
                $(".qq-upload-status-text").text($(".qq-upload-status-text").text() + ' (' + responseJSON.error + ')');
            }
        });
    }
};
var Tag = {
    msg: '<blockquote>"{tag}" has been added.</blockquote>'
    , init: function() {
        if ($(".x-editable").length) {
            $(".x-editable").editable({
                send: 'never'
            }).on('save', function(e, params) {
                Tag.saveInline(e, params);
            });
        }
        Tag.changeState();
    }
    , changeState: function() {
        $("#tags").delegate("[data-do=change-state]", 'click', function(e) {
            var $this = $(this);
            var $parent = $this.parents("tr");
            var id = parseInt($parent.attr('data-id'));
            if (typeof id !== "undefined" && id > 0) {
                var state = parseInt($this.attr("data-change-state-to"));
                var params = Tag.getData($parent, state);
                $.ajax({
                    url: Services.base + Services.tags
                    , type: 'put'
                    , headers: {"Authorization": token}
                    , data: JSON.stringify(params)
                    , success: function(d) {
                        var button = '';
                        switch (d.Published) {
                            case 0:
                                button = '<a href="#" data-do="change-state" data-change-state-to="1" class="btn default red-stripe btn-xs"><i class="fa fa-times font-red"></i> Unpublished</a>';
                                break;
                            case 1:
                                button = '<a href="#" data-do="change-state" data-change-state-to="0" class="btn default green-stripe btn-xs"><i class="fa fa-check font-green"></i> Published</a>';
                                break;
                        }
                        $parent.find(".tag-state").html(button);
                    }
                });
            } else {
                console.log('no item found!');
            }
            e.preventDefault();
            return false;
        });
    }
    , getData: function($obj, state) {
        params = {};
        if (typeof $obj !== "undefined") {
            var params = {
                id: $obj.attr("data-id")
                , name: $obj.find(".tag-name").text()
                , Published: (typeof state !== "undefined") ? state : $obj.attr("data-state")
            };
        }
        return params;
    }
    , saveInline: function(e, item) {
        var $this = $(e.delegateTarget);
        var $parent = $this.parents("tr");
        var id = parseInt($parent.attr('data-id'));
        window.setTimeout(function() {
            var params = Tag.getData($parent);
            if (typeof id !== "undefined" && id > 0) {
                $.ajax({
                    url: Services.base + Services.tags
                    , type: 'put'
                    , data: JSON.stringify(params)
                    , headers: {"Authorization": token}
                    , success: function(d) {
                        $parent.find(".editable-unsaved").removeClass("editable-unsaved");
                    }
                });
            }
        });
    }
    , add: function(form, method, results) {
        var data = $(form).serializeObject();
	if (data.name !== '') {
	    $.ajax({
		url: Services.base + Services.tags
		, type: method
		, data: JSON.stringify(data)
		, headers: {"Authorization": token}
		, success: function(d) {
		    $(results).append(Tag.msg.replace(/{tag}/g, data.name));
		}
	    });
	} else {
	    alert('Tag name cannot be empty!');
	}
    }
};
var Uploader = {
    init: function() {
        var $uploader = $("#file-upload");
        $uploader.fineUploader({
            request: {endpoint: Services.base + Services.tempUpload}
            , multiple: false
            , template: 'qq-template'
//            , debug: true
//            , validation: { allowedExtensions: ['jpg', 'jpeg', 'png', 'gif', 'bmp'] }
        }).on('complete', function(event, id, filename, responseJSON) {
            if (responseJSON.success) {
                $("#file-upload").slideUp('fast');
                $("#media-preview > div").empty().append('<img id="crop" src="' + responseJSON.FullFileName + '" />');
                $("#media-temp-form").slideDown('fast', function() {
                    $("input[name=FullFileName]").val(responseJSON.Src);
                    Form.tags();
                    $('#crop').Jcrop({
                        boxWidth: 562
                        , aspectRatio: 16 / 9
                        , setSelect: [0, 325, 182.8125, 0]
                        , onChange: Media.setCropCoords
                    });
                });
            } else {
                $(".qq-upload-status-text").text($(".qq-upload-status-text").text() + ' (' + responseJSON.error + ')');
            }
        });
    }
};
var Poll = {
    raiseError: function(msg) {
        var alert = Form.messageTmpl.replace(/{title}/g, 'Error saving item!').replace(/{msg}/g, msg);
        $("#messages").empty().html(alert).slideDown('fast');
        $("html, body").animate({'scrollTop': 0}, 700);
        window.setTimeout(function() {
            $("#messages").slideUp('fast');
        }, 10000);
    }
    , save: function(form, method, id, nextAction) {
        var params = $(form).find("input, textarea, select").serializeObject();
        var e = [];
        var fs = form.split(',');
        for (var f = 0; f < $(fs).length; f++) {
            if ($(fs[f]).find("[required]").length) {
                $(fs[f]).find("[required]").each(function() {
                    if ($(this).val() === "undefined" || $(this).val() === "")
                        e.push($(this));
                });
            }
        }
        if (e.length) {
            for (var g = 0; g < e.length; g++) {
                $(e[g]).parent().addClass("has-error");
            }
            $("html, body").animate({'scrollTop': 0}, 700);
            return false;
        }
        params["Polls_Options"] = [];
        if (typeof params["item[]"] !== "undefined") {
            if (typeof params["item[]"] === "string") {
                params["Polls_Options"].push({Title: params["item[]"], Priority: 1});
            } else {
                $.each(params['item[]'], function(i, item) {
		    var id = (typeof params["id[]"] === "undefined") ? 0 : params["id[]"][i];
                    params["Polls_Options"].push({Title: item, Id: id, Priority: (i + 1)});
                });
            }
            delete params['item[]'];
            delete params['id[]'];
            var url = Services.base + Services.polls;
            if (typeof id !== "undefined" && id !== null)
                url += id;
            $.ajax({
                url: url
                , data: JSON.stringify(params)
//                , data: params
                , type: method
                , headers: {"Authorization": token}
                , success: function(d) {
                    console.log(d); //////////////////
                    Location.redirect(false, nextAction);
                }
                , error: function(jqXHR, textStatus, errorThrown) {
                    Poll.raiseError(jqXHR.responseJSON.Message);
                }
            });
        } else {
            Poll.raiseError('Error!');
        }
    }
    , resortIndexes: function() {
        var $items = $("#poll-form .poll-items").find(".form-group");
        var itemsCount = $items.length + 1;
        for (var n = 1; n < itemsCount; n++) {
            $items.find("label span").eq(n - 1).text(n);
        }
    }
};
var Video = {
    raiseError: function(msg) {
        var alert = Form.messageTmpl.replace(/{title}/g, 'Error saving item!').replace(/{msg}/g, msg);
        $("#messages").empty().html(alert).slideDown('fast');
        $("html, body").animate({'scrollTop': 0}, 700);
        window.setTimeout(function() {
            $("#messages").slideUp('fast');
        }, 10000);
    }
    , save: function(form, method, id, nextAction) {
        var params = $(form).find("input, textarea, select").serializeObject();
        var e = [];
        var fs = form.split(',');
        for (var f = 0; f < $(fs).length; f++) {
            if ($(fs[f]).find("[required]").length) {
                $(fs[f]).find("[required]").each(function() {
                    if ($(this).val() === "undefined" || $(this).val() === "")
                        e.push($(this));
                });
            }
        }
        if (e.length) {
            for (var g = 0; g < e.length; g++) {
                $(e[g]).parent().addClass("has-error");
            }
            Video.raiseError('Please make sure no required field is empty.');
            return false;
        }
        var url = Services.base + Services.program;
        if (typeof id !== "undefined" && id !== null)
            url += id;
        $.ajax({
            url: url
            , data: JSON.stringify(params)
            , type: method
            , headers: {"Authorization": token}
            , success: function(d) {
                Location.redirect(false, nextAction);
            }
            , error: function(jqXHR, textStatus, errorThrown) {
                Video.raiseError(jqXHR.responseJSON.Message);
            }
        });
    }
    , saveEpisode: function(form, method, id, nextAction) {
        var params = $(form).find("input, textarea, select").serializeObject();
        var e = [];
        var fs = form.split(',');
        for (var f = 0; f < $(fs).length; f++) {
            if ($(fs[f]).find("[required]").length) {
                $(fs[f]).find("[required]").each(function() {
                    if ($(this).val() === "undefined" || $(this).val() === "")
                        e.push($(this));
                });
            }
        }
        if (e.length) {
            for (var g = 0; g < e.length; g++) {
                $(e[g]).parent().addClass("has-error");
            }
            Video.raiseError('Please make sure no required field is empty.');
            return false;
        }
        var url = Services.base + Services.episode;
        if (method !== "post")
            url += '/' + id;
        $.ajax({
            url: url
            , data: JSON.stringify(params)
            , type: method
            , headers: {"Authorization": token}
            , success: function(d) {
                Location.redirect(false, nextAction);
            }
            , error: function(jqXHR, textStatus, errorThrown) {
                Video.raiseError(jqXHR.responseJSON.Message);
            }
        });
    }
    , reInitUpload: function(type) {
        if (type !== 'afp') {
            $("#media-preview").find(".inner").empty();
            $("#media-fields").find("input").val('');
            $("#media-temp-form").slideUp(200).find("textarea").empty();
            $("#file-upload").empty().show(1);
            Uploader.init();
        } else {
            $("#afp-preview").find(".inner").empty();
            $("#afp-fields").find("input").val('');
            $("#afp-temp-form").slideUp(200).find("textarea").empty();
            $("#afp-serach").slideDown('fast');
        }
    }
    , initiTiles: function() {
        $(".tiles").delegate(".tile", 'click', function(e) {
            var params = {
                type: $(this).hasClass('image') ? 'image' : 'video'
                , id: $(this).attr('data-id')
                , title: $(this).attr('data-original-title')
                , src: $(this).find("img:first").attr('src')
                , vidSrc: $(this).hasClass('image') ? '' : $(this).attr('data-relative-path')
            };
//            alert(params.vidSrc);
            Video.addMedia(params);
            e.preventDefault();
        });
    }
    , addMedia: function(params) {
        var extID = null, id = params.id;
        var $container = (params.type === 'image') ? $("#item-image") : $("#item-video");
        var $list = $container.find(".list ul");
        if ($list.find("li").length > 0) {
            alert('Only one item is allowed!');
            return;
        }
        var $inputs = $container.find(".inputs");
        var item = '<li data-type="{type}" data-id="{id}"><div class="inner"><div class="img"><img src="{src}" alt="{title}" /></div><div class="desc"><h3>{title}</h3></div>{removeEl}</div>{input}</li>';
        var input = '<input type="hidden" name="{type}" value="{src}" />';
        var remove = '<div class="remove-button"><i class="fa fa-trash-o"></i></div>';
        if (!$list.find("li[data-id=" + params.id + "]").length && !$inputs.find("input[value=" + params.id + "]").length) {
            var src = params.src.replace(Services.baseMedia, '');
            if (typeof params.vidSrc !== "undefined" && params.vidSrc !== '')
                src = params.vidSrc;
            input = input.replace(/{src}/g, src).replace(/{type}/g, Global.ucfirst(params.type));
            item = item.replace(/{type}/g, params.type)
                    .replace(/{id}/g, params.id)
                    .replace(/{src}/g, params.src)
                    .replace(/{title}/g, params.title)
                    .replace(/{removeEl}/g, remove)
                    .replace(/{input}/g, input);
            $list.append(item);
        }
    }
};
var Media = {
    minWidth: 650
    , tileHTML: '<div class="tile {type}" data-toggle="tooltip" data-placement="bottom" data-relative-path="{relPath}" data-kind={kind} data-id="{id}" data-title-old=\'{title}\' title=\'{Description}\'><div class="tile-body"><img src="{src}" alt=\'{title}\' /></div></div>'
    , itemState: '<a href="#" data-do="change-state" data-change-state-to="{newState}" class="btn default {color}-stripe btn-xs"><i class="fa fa-{icon}"></i> {text}</a>'
    , itemsTemplate: '{{#each []}}<tr data-id="{{this.Id}}">\n\
                    <td class="item-id">{{this.Id}}</td>\n\
                    <td class="item-thumb"><img src="{{this.Image}}" alt="{{this.Description}}" /></td>\n\
                    <td class="item-title"><span class="x-editable" data-type="text" data-title="Edit Title" data-name="title">{{this.Title}}</span></td>\n\
                    <td class="item-desc"><span class="x-editable" data-type="textarea" data-title="Edit Description" data-name="description">{{this.Description}}</span></td>\n\
                    <td class="item-created"><span>{{this.Created}}</span></td>\n\
                    <td class="item-tag">{{{this.TagsHtml}}}</td>\n\
                    </tr>{{/each}}'
    , saveAfpPic: function(addr) {
        if (typeof addr !== "undefined") {
            $.ajax({
                url: Services.base + Services.afpUpload + '/?q=' + addr
                , type: 'post'
                , headers: {"Authorization": token}
                , success: function(d) {
                    $("#afp-serach").slideUp('fast');
                    $("#afp-preview > div").append('<img id="afp-crop" src="' + Services.baseMedia + '/' + Services.temp + '/' + d.filename + '" />');
                    $("#afp-temp-form").slideDown('fast', function() {
                        $("input[name=Address]").val(d.filename);
                        Form.tags();
                        $('#afp-crop').Jcrop({
                            boxWidth: 562
                            , aspectRatio: 16 / 9
                            , setSelect: [0, 325, 182.8125, 0]
                            , minSize: [Media.minWidth, 0]
                            , onChange: Media.setCropCoords
                        });
                    });
                }
                , fail: function() {
                    alert('Error uploading file');
                }
                , error: function() {
                    alert('Error uploading file');
                }
            });
        }
        return false;
    }
    , initList: function() {
        if ($(".x-editable").length) {
            $(".x-editable").editable({
                send: 'never'
            }).on('save', function(e, params) {
                Media.saveInline(e, params);
            });
        }
    }
    , getInlineData: function($obj, state) {
        params = {};
        if (typeof $obj !== "undefined") {
            var params = {
                Description: $obj.find(".item-desc").text()
                , Title: $obj.find(".item-title").text()
                , Tags: [] // TODO
                , IsPublished: (typeof state !== "undefined") ? state : $obj.find(".item-state").attr('data-state')
            };
        }
        return params;
    }
    , saveInline: function(e, item) {
        var $this = $(e.delegateTarget);
        var $parent = $this.parents("tr");
        var id = parseInt($parent.attr('data-id'));
        window.setTimeout(function() {
            var params = Media.getInlineData($parent);
            if (typeof id !== "undefined" && id > 0) {
                $.ajax({
                    url: Services.base + Services.media + id
                    , type: 'put'
                    , headers: {"Authorization": token}
                    , data: JSON.stringify(params)
                    , success: function(d) {
                        $parent.find(".editable-unsaved").removeClass("editable-unsaved");
                    }
                });
            }
        });
    }
    , changeState: function() {
        $("#images").delegate("[data-do=change-state]", 'click', function(e) {
            var $this = $(this);
            var $parent = $this.parents("tr");
            var id = parseInt($parent.attr('data-id'));
            if (typeof id !== "undefined" && id > 0) {
                var state = parseInt($this.attr("data-change-state-to"));
                var params = Media.getInlineData($parent, state);
                $.ajax({
                    url: Services.base + Services.media + id
                    , type: 'put'
                    , headers: {"Authorization": token}
                    , data: JSON.stringify(params)
                    , success: function(d) {
                        switch (d.IsPublished) {
                            case 0:
                                var html = Media.itemState.replace(/{newState}/g, 1).replace(/{color}/g, 'red').replace(/{icon}/g, 'times').replace(/{text}/g, 'Unpublished');
                                break;
                            case 1:
                                var html = Media.itemState.replace(/{newState}/g, 0).replace(/{color}/g, 'green').replace(/{icon}/g, 'check').replace(/{text}/g, 'Published');
                                break;
                        }
                        $this.parent().html(html);
                    }
                });
            } else {
                console.log('no item found!');
            }
            e.preventDefault();
            return false;
        });
    }
    , search: function(f, m, r, t) {
        var method = (typeof m !== "undefined") ? m : 'get';
        var $results = $(r);
        $results.addClass('loading').empty();
        var url = Services.base;
        switch (t) {
            case 'images':
                url += Services.media;
                break;
            case 'videos':
                url = Services.videos;
                break;
            case 'afp':
                url += Services.afp;
                break;
        }
        $.ajax({
            url: url
            , type: method
            , data: $(f).serialize()
            , dataType: "json"
            , headers: (t === 'images') ? {"Authorization": token} : ''
            , success: function(d) {
                if (t !== 'afp') {
                    if (d.length) {
                        $results.append('<div class="tiles"></div>');
                        var $tiles = $results.find(".tiles");
                        for (var i = 0; i < d.length; i++) {
                            if (t === 'images') {
                                var item = Media.tileHTML.replace(/{title}/g, d[i].Title).replace(/{relPath}/g, '').replace(/{Description}/g, d[i].Description).replace(/{type}/g, 'image').replace(/{kind}/g, 1).replace(/{src}/g, Services.baseMedia + d[i].FilePath).replace(/{id}/g, d[i].Id);
                            } else if (t === 'videos') {
                                var desc = d[i].Filename.split('/')[d[i].Filename.split('/').length - 1];
                                var item = Media.tileHTML.replace(/{title}/g, desc).replace(/{relPath}/g, d[i].relativePath).replace(/{Description}/g, desc).replace(/{type}/g, 'video').replace(/{kind}/g, 2).replace(/{src}/g, d[i].Thumbnail).replace(/{id}/g, d[i].Id);
                            }
                            $tiles.append(item);
                        }
                    }
                } else {
                    if (d.response.docs.length) {
                        $results.append('<div class="tiles"></div>');
                        var $tiles = $results.find(".tiles");
                        for (var i = 0; i < d.response.docs.length; i++) {
                            var res = d.response.docs[i];
                            var item = Media.tileHTML.replace(/{title}/g, res.Body).replace(/{relPath}/g, '').replace(/{type}/g, 'afp').replace(/{Description}/g, '').replace(/{src}/g, res.Thumb).replace(/{id}/g, res.ID);
                            $tiles.append(item);
                        }
                    }
                }
                Media.initiTiles();
                Video.initiTiles();
                $("[data-toggle=tooltip]").tooltip({'html': true});
            }
        });
    }
    , setCropCoords: function(c) {
        var co = {X: c.x, Y: c.y, WidthCrop: c.w, HeightCrop: c.h};
        for (var k in co)
            $("input[name=" + k + "]").val(parseInt(co[k]));
    }
    , save: function(form, method, add, type) {
        if (typeof add === "undefined")
            add = false;
        var params = $(form).find("input[data-method!=get], textarea").serializeObject();
        //var gParams = $(form).find("input[data-method=get]").serialize();
        if ((typeof params["Description"] === "undefined" || !params["Description"]) || (typeof params["Title"] === "undefined" || !params["Title"])) {
            alert('Title and description fields are required.');
            return false;
        }
        if (parseInt($("input[name=w]").val()) < Media.minWidth) {
            alert('Cropped image is too small.');
            return false;
        }
        params["Tags"] = [];
        if (typeof params["tags[]"] !== "undefined") {
            if (typeof params["tags[]"] === "string") {
                params["Tags"].push({id: params["tags[]"]});
            } else {
                $.each(params['tags[]'], function(i, tag) {
                    params["Tags"].push({id: tag});
                });
            }
            delete params['tags[]'];
        }
        $.ajax({
            url: Services.base + Services.tempCrop
            , type: method
            , headers: {"Authorization": token}
            , data: JSON.stringify(params)
            , success: function(d) {
                if (add) {
                    var params = {
                        type: 'image'
                        , id: d.Id
                        , title: d.Description
                        , src: Services.baseMedia + d.FilePath
			, relPath: ''
			, kind: 1
                    };
                    Media.add(params);
                }
                Media.reInitUpload(type);
            }
            , fail: function() {
                console.log('failed');
            }
        });
        return;
    }
    , reInitUpload: function(type) {
        if (type !== 'afp') {
	    $("#media-preview").find(".inner").empty();
            $("#media-fields").find("input").not("[name=Type]").val('');
            $("#media-fields").find("textarea").val('');
            $("#media-temp-form").slideUp(200).find("textarea").val('');
            $("#file-upload").empty().show(1);
            Uploader.init();
	    console.log($("#media-preview").html());
        } else {
            $("#afp-preview").find(".inner").empty();
            $("#afp-fields").find("input").val('');
            $("#afp-temp-form").slideUp(200).find("textarea").empty();
            $("#afp-serach").slideDown('fast');
        }
    }
    , initiTiles: function() {
        $(".tiles").delegate(".tile:not(.afp)", 'click', function(e) {
            var params = {
                type: $(this).hasClass('image') ? 'image' : 'video'
		, kind: $(this).hasClass('image') ? 1 : 2 // i:image, 2: video
                , id: $(this).attr('data-id')
                , title: $(this).attr('data-original-title')
                , src: $(this).find("img:first").attr('src')
                , relPath: $(this).attr('data-relative-path')
            };
	    if (!$("#item-image").length && !$("#item-video").length) // So dirty, but one of only ways to determine if we are in video and episode pages
		Media.add(params);
            e.preventDefault();
        });
    }
    , add: function(params) {
	if ($("#item-image").length && $("#item-video").length) { // So dirty, but one of only ways to determine if we are in video and episode pages
	    Video.addMedia(params);
	    return;
	}
        var extID = null, id = params.id;
        var $list = $("#item-media .list ul");
        var $inputs = $("#item-media .inputs");
        var item = '<li data-type="{type}" data-id="{id}"><div class="inner"><div class="img"><img src="{src}" alt="{title}" /></div><div class="desc"><h3>{title}</h3></div>{removeEl}</div>{input}{inputE}{relPath}{kind}</li>';
        var input = '<input type="hidden" name="media[]" value="{id}" />';
        var inputE = '<input type="hidden" name="mediaE[]" value="{id}" />';
        var relativePath = '<input type="hidden" name="relPath[]" value="{relativePath}" />';
        var kind = '<input type="hidden" name="kind[]" value="{kind}" />';
        var remove = '<div class="remove-button"><i class="fa fa-trash-o"></i></div>';
        if (!$list.find("li[data-id=" + params.id + "]").length && !$inputs.find("input[value=" + params.id + "]").length) {
            if (params.type !== 'image') {
                extID = params.id;
                id = 0;
            }
            input = input.replace(/{id}/g, id);
            inputE = inputE.replace(/{id}/g, extID);
            relativePath = relativePath.replace(/{relativePath}/g, params.relPath);
            kind = kind.replace(/{kind}/g, params.kind);
            item = item.replace(/{type}/g, params.type)
                    .replace(/{id}/g, params.id)
                    .replace(/{src}/g, params.src)
                    .replace(/{title}/g, params.title)
                    .replace(/{removeEl}/g, remove)
                    .replace(/{input}/g, input)
                    .replace(/{inputE}/g, inputE)
                    .replace(/{relPath}/g, relativePath)
                    .replace(/{kind}/g, kind);
            $list.append(item);
            $list.sortable().disableSelection();
        }
    }
};
var Schedule = {
    tableTmpl: '<table class="table table-condensed table-hover table-striped table-border"><tbody>{items}</tbody></table>'
    , itemTmpl: '<tr><td class="col-xs-1">#{NewsNo}</td><td class="col-xs-2">{Title}</td><td class="col-xs-9">{Body}</td></tr>'
    , load: function($obj) {
        if (typeof $obj !== "undefined") {
            var $parent = $obj.parents('.panel');
            var id = $obj.attr('data-id');
            if ($parent.find(".panel-body").text().trim() === "") {
                $.ajax({
                    url: Services.base + Services.schedule + '/' + id
                    , type: 'get'
                    , headers: {"Authorization": token}
                    , success: function(d) {
                        if (d.length > 0) {
                            var output = Schedule.tableTmpl;
                            var items = '';
                            $.each(d, function(i, item) {
                                items += Schedule.itemTmpl.replace(/{NewsNo}/g, item.NewsNo)
                                        .replace(/{Title}/g, item.Title)
                                        .replace(/{Body}/g, item.Body);
                            });
                            output = output.replace(/{items}/g, items);
                            $parent.find(".panel-body").html(output);
                        } else {
                            $parent.find(".panel-body").html('<div class="alert alert-warning"><h4>No Items!</h4></div>');
                        }
                    }
                });
            }
        }
    }
};
var Notifications = {
    msgTmpl: '<li><a href="#messages/inbox"><span class="photo"><img src="{img}" alt=""/></span><span class="subject"><span class="from">{sender}</span><span class="time">{time}</span></span><span class="message">{msg}</span></a></li>'
    , init: function() {
        if ($("#header-messages").length) {
            Notifications.loadItems();
            Notifications.loadNotSeen();
            window.setInterval(Notifications.loadItems, 10000);
            window.setInterval(Notifications.loadNotSeen, 10000);
        }
    }
    , loadItems: function() {
        $.ajax({
            url: Services.base + Services.messages
            , headers: {"Authorization": token}
            , global: false
            , success: function(d) {
                Notifications.handleMsg(d);
            }
        });
    }
    , loadNotSeen: function() {
        $.ajax({
            url: Services.base + Services.notSeen
            , headers: {"Authorization": token}
            , global: false
            , success: function(d) {
                Notifications.handleInboxUnread(d);
            }
        });
    }
    , handleInboxUnread: function(data) {
//	if (data.length) {
	    if (data !== 0) {
		$("#header-inbox-unread").find("span.badge").text(data);
	    } else {
		$("#header-inbox-unread").find("span.badge").text('');
	    }
//	}
    }
    , handleMsg: function(data) {
        if (data.length) {
            var count = data[0].TotalUnread;
            var output = '';
            $.each(data, function(i, msg) {
                output += Notifications.msgTmpl.replace(/{img}/g, msg.MessageFromPicture)
                        .replace(/{sender}/g, msg.MessageFrom)
                        .replace(/{time}/g, msg.MessageSendDate)
                        .replace(/{msg}/g, msg.MessageBody);
            });
            $("#header-messages").html(output);
            if (count !== 0) {
                $("#header-messages-unread span:last").text(count);
            } else {
                $("#header-messages-unread span:last").text('');
            }
            $("#header_inbox_bar").removeClass("hide");
        } else {
            $("#header_inbox_bar").addClass("hide");
        }
    }
    , removeUnread: function() {
//        $.ajax({
//            url: Services.base + Services.messages
//            , type: 'put'
//            , headers: {"Authorization": token}
//            , success: function(d) {
////                console.log(d); 
//            }
//        });
    }
};
Form.beforeRender();
$(function() {
    var auth = Cookie.get('htvpanel=');
    if (auth !== '') {
        token = auth;
        Location.init();
    } else { // alert('Token NOT found: redirecting to login page');
        window.location.href = 'login';
    }
});
$(document).delegate("a[data-task=submit]", 'click', function(e) {
    var $a = $(this); // Anchor or whatever
    var t = $a.attr('data-action'); // endpoint: type of submitted form(s) [item, tags, whatever]
    var f = $a.attr('data-forms'); // forms to submit
    var m = $a.attr('data-method'); // method [post, put, get, whatever]
    var r = $a.attr('data-results'); // results container
    var n = $a.attr('data-next'); // redirect after submit
    var i = Global.getVal($a, 'data-itemid', null); // Item id (for edit scenarios)
    switch (t) {
        case 'item':
            if (f !== "undefined" && m !== "undefined") {
                Form.submit(f, m, t, i, n);
            } else {
                return false;
            }
            break;
        case 'images':
        case 'videos':
        case 'afp':
            if (f !== "undefined" && m !== "undefined" && r !== "undefined") {
                Media.search(f, m, r, t);
            }
            break;
        case 'save-media':
            if (f !== "undefined" && m !== "undefined") {
                Media.save(f, m, false, r);
            }
            break;
        case 'save-add-media':
            if (f !== "undefined" && m !== "undefined") {
                Media.save(f, m, true, r);
            }
            break;
        case 'poll':
            if (f !== "undefined" && m !== "undefined") {
                Poll.save(f, m, i, n);
            }
            break;
        case 'headlines':
            if (f !== "undefined" && m !== "undefined") {
                Items.reOrder(f, m);
            }
            break;
        case 'tag':
            if (f !== "undefined" && m !== "undefined") {
                Tag.add(f, m, r);
            }
            break;
        case 'user':
            if (f !== "undefined" && m !== "undefined") {
                User.add(f, m, r);
            }
            break;
        case 'change-pass':
            if (f !== "undefined" && m !== "undefined") {
                User.changePass(f, m);
            }
            break;
        case 'message':
            if (f !== "undefined" && m !== "undefined") {
                User.sendMessage(f, m, r);
            }
            break;
        case 'send':
            if (f !== "undefined" && m !== "undefined") {
                if (f !== "null") {
                    Items.send(f, m, r);
                } else {
                    var p = {Content_Id: $a.attr('data-itemid'), User_To: -100};
                    Items.send(f, m, r, p);
                }
            }
            break;
        case 'contributor':
            Contributor.add(f, m, r);
            break;
        case 'program':
            if (f !== "undefined" && m !== "undefined") {
                Video.save(f, m, i, n);
            }
            break;
        case 'episode':
            if (f !== "undefined" && m !== "undefined") {
                Video.saveEpisode(f, m, i, n);
            }
            break;
    }
    e.preventDefault();
    return false;
}).delegate("#schedule .panel-title a", 'click', function(e) {
    Schedule.load($(this));
    e.preventDefault();
    return false;
}).delegate(".tile.afp", 'click', function(e) {
    var src = $(this).find("img").attr('src');
    Media.saveAfpPic(src);
    e.preventDefault();
    return false;
}).delegate("#images-form, #afp-form, #videos-form", 'submit', function(e) {
    $(this).find("a[data-identifier=do-search]").trigger('click');
    e.preventDefault();
    return false;
}).delegate("#media-form", 'submit', function(e) {
    $(this).find("a[data-identifier=do-search]").trigger('click');
    e.preventDefault();
    return false;
}).delegate("#tag-form", 'submit', function(e) {
    $(this).find("a[data-identifier=do-add]").trigger('click');
    e.preventDefault();
    return false;
}).delegate("#tag-modal", 'hidden.bs.modal', function() { // Refresh page after tag add modal closes
    if ($("#tag-results").text() !== "")
        Location.refresh();
}).delegate("#user-modal", 'hidden.bs.modal', function() { // Refresh page after user add modal closes
    if ($("#user-results").text() !== "")
        Location.refresh();
}).delegate("a[data-task=cancel-upload]", 'click', function(e) { // Cancel Upload
    var type = $(this).attr("data-type");
    console.log(type);
    Media.reInitUpload(type);
    e.preventDefault();
    return false;
}).delegate("#tag-modal input[name=name]", 'keyup', function(e) { // Cancel Upload
    $this = $(this);
    if (e.key === 'enter')
        return false;
    if ($this.val().length > 1) {
        if (e.which !== 0 && !e.ctrlKey && !e.metaKey && !e.altKey) {
	    var source = Services.base + Services.tags + '?query=' + $this.val();
            delay(function() {
                $.ajax({
		    url: source
		    , headers: {"Authorization": token}
                    , type: 'get'
		    , success: function(d) {
			$("#suggestions h4").removeClass("hide");
			$("#suggestions ul").empty();
			$.each(d, function(i, item) {
			    console.log(item);
			    $("#suggestions ul").append('<li><span class="label label-warning">' + item.id + '</span>&nbsp;&nbsp;&nbsp;<span class="tag">' + item.name + '</span></li>');
			});
		    }
		});
	    });
	}
    }
    return;
}).delegate("input, select, textarea", 'focusin', function() {
    if ($(this).parent().hasClass("has-error")) {
        $(this).parent().removeClass("has-error");
    }
}).delegate(".workspace-content a[data-toggle=modal]", 'click', function(e) {
    var id = $(this).attr('data-id');
    $("#send-item-id").val(id);
    e.preventDefault();
    return false;
}).delegate("#poll-form a[data-task=add-item]", 'click', function(e) { // Add poll item [clone last item]
    var $items = $(".poll-items");
    var $lastItem = $(".poll-items > div:last");
    $lastItem.clone().appendTo($items);
    var $newItem = $(".poll-items > div:last");
    $newItem.find("label span").text(parseInt($(".poll-items > div:last").find("label span").text()) + 1);
    $newItem.find("input").val('');
    $newItem.find('input[name="id[]"]').val("0");
    e.preventDefault();
    return false;
}).delegate("#poll-form a[data-task=remove-item]", 'click', function(e) { // Remove poll item
    if ($(".poll-items").find(".form-group").length > 1) {
        $(this).parents(".form-group").remove();
        Poll.resortIndexes();
    }
    e.preventDefault();
    return false;
}).delegate(".show-reply", 'click', function(e) { // Media item remove in item form
    var $obj = $(this);
    if (typeof $obj.attr("data-original-title") === "undefined") {
        var id = $(this).attr('title');
        $obj.find("i").removeClass('hide');
        $.ajax({
            url: Services.base + Services.comments + '/' + id
            , type: 'get'
            , headers: {"Authorization": token}
            , success: function(d) {
                $obj.find("i.fa-spin").addClass('hide');
                $obj.attr("data-toggle", "popover").attr("title", d.Name).attr("data-content", d.Text).attr("data-placement", 'left');
                $obj.popover('show');
            }
        });
    }
    e.preventDefault();
}).delegate("#send-msg-toall", 'change', function(e) { // Send to all button in send message modal
    if (this.checked) {
        User.disableSuggest();
    } else {
        User.initSuggest();
    }
}).delegate(".portlet .remove-button", 'click', function(e) { // Media item remove in item form
    $(this).parents("li:first").slideUp('fast', function() {
        $(this).remove();
    });
    e.preventDefault();
}).delegate("a[data-edited=1]", 'click', function(e) { // Cancel item editing: check-in item
    var id = $("#item-edit").attr('data-itemid');
    $.ajax({
        url: Services.base + Services.items + '/' + id + '/?query=edited'
        , type: 'put'
        , headers: {"Authorization": token}
        , success: function(d) {
        }
    });
}).delegate("[data-src=sections] .sub-menu a", 'click', function(e) { // Parent menus in sidebar-sections click activations
    var href = $(this).attr('href').replace('#', '');
    Location.redirect(false, href);
}).delegate("#comments-search", 'keyup', function(e) { // Comments search
    $this = $(this);
    if (e.key === 'enter')
        return false;
    if ($this.val().length > 2) {
        if (e.which !== 0 && !e.ctrlKey && !e.metaKey && !e.altKey) {
            $this.parent().find("span").removeClass("hide");
            var source = Services.base + Services.comments + '/?query=' + $this.val();
            delay(function() {
                $.ajax({
                    url: source
                    , headers: {"Authorization": token}
                    , type: 'get'
                    , success: function(d) {
                        $this.parent().find("span").addClass("hide");
                        var items = Items.handle(d);
                        var template = Comment.itemsTemplate;
                        var handlebarsTemplate = Handlebars.compile(template);
                        var output = handlebarsTemplate(items);
                        $("#comment-results").find("tbody").html(output);
                        Comment.init();
                    }
                });
            }, 500);
        }
    }
}).delegate("#item-search", 'keyup', function(e) { // Items search
    $this = $(this);
    if (e.key === 'enter')
        return false;
    if ($this.val().length > 2) {
        if (e.which !== 0 && !e.ctrlKey && !e.metaKey && !e.altKey) {
            $this.parent().find("span").removeClass("hide");
            var source = Services.base + Services.items + '/?query=' + $this.val();
            delay(function() {
                $.ajax({
                    url: source
                    , headers: {"Authorization": token}
                    , type: 'get'
                    , success: function(d) {
                        $this.parent().find("span").addClass("hide");
                        var items = Items.handle(d);
                        var template = Items.itemsTemplate;
                        var handlebarsTemplate = Handlebars.compile(template);
                        var output = handlebarsTemplate(items);
                        $("#serachitems").find("tbody").html(output);
                    }
                });
            }, 500);
        }
    }
}).delegate("#media-search", 'keyup', function(e) { // Media search
    $this = $(this);
    if (e.key === 'enter')
        return false;
    if ($this.val().length > 2) {
        if (e.which !== 0 && !e.ctrlKey && !e.metaKey && !e.altKey) {
            $this.parent().find("span").removeClass("hide");
            var source = Services.base + Services.media + '?query=' + $this.val();
            delay(function() {
                $.ajax({
                    url: source
                    , type: 'get'
                    , headers: {"Authorization": token}
                    , success: function(d) {
                        $this.parent().find("span").addClass("hide");
                        var items = Items.handle(d);
                        var template = Media.itemsTemplate;
                        var handlebarsTemplate = Handlebars.compile(template);
                        var output = handlebarsTemplate(items);
                        $("#media-results").find("tbody").html(output);
                        Media.initList();
                    }
                });
            }, 500);
        }
    }
}).delegate("select#change-inbox", 'change', function() {
    var id = $(this).find('option:selected').val();
    Location.redirect(false, 'dashboard/inbox/' + id);
}).delegate("#destination-select select", 'change', function() {
    var group = $(this).find('option:selected').val();
    if (parseInt(group) !== 0) {
        $.ajax({
            url: Services.base + Services.userGroups + '/' + group
            , headers: {"Authorization": token}
            , success: function(d) {
                var html = '<option value="0"> -- Send to group -- </option>';
                $.each(d, function(i, user) {
                    html += '<option value="' + user.Id + '">' + user.Name + '</option>';
                });
                $("#user-select select:first").removeAttr('disabled').empty().html(html).select2();
            }
        });
    } else {
        $("#user-select select:first").attr('disabled', true).empty().html('<option>Select group first</option>').select2();
    }
}).delegate("select#change-category", 'change', function() {
    var id = $(this).find('option:selected').val();
    var state = $("select#change-state").val();
    Location.redirect(false, 'dashboard/items/' + id + '/' + state);
}).delegate("select#change-kind", 'change', function() {
    var kind = $(this).find('option:selected').val();
    Location.redirect(false, 'videos/index/' + kind);
}).delegate("select#change-state", 'change', function() {
    var state = $(this).find('option:selected').val();
    var id = $("select#change-category").val();
    Location.redirect(false, 'dashboard/items/' + id + '/' + state);
}).delegate("select#change-videos-state", 'change', function() {
    var state = $(this).find('option:selected').val();
    var kind = $("select#change-kind").val();
    Location.redirect(false, 'videos/index/' + kind + '/' + state);
}).delegate("select#change-ordering-category", 'change', function() {
    var id = $(this).find('option:selected').val();
    if (parseInt(id) !== 0)
        Location.redirect(false, 'dashboard/ordering/' + id);
}).delegate("#users .save-groups", 'click', function(e) {
    var $obj = $(this).parents("tr");
//    console.log($obj.find("select").val());
    User.updateGroups($obj);
    e.preventDefault();
}).delegate("#inbox.messages tr", 'click', function(e) {
    var $msg = $(this);
    if (!$msg.hasClass('seen')) {
        var id = $msg.attr('data-id');
        $.ajax({
            url: Services.base + Services.checkMsg + id
            , headers: {"Authorization": token}
            , success: function(d) {
                $msg.removeClass("info");
            }
        });
        e.preventDefault();
    }
}).delegate(".send-episode", 'click', function(e) {
    $("textarea[name=Message]").empty().val('"' + $(this).attr("data-title")  + ' - Episode ' + $(this).attr("data-episode-number") + '" is ready to publish!');
}).delegate(".send-program", 'click', function(e) {
    $("textarea[name=Message]").empty().val('"' + $(this).attr("data-title")  + '" is ready to publish!');
}).delegate("a[data-do=delete]", 'click', function(e) {
    var id = $(this).attr('data-id');
    var row = $(this).parents("tr:first");
    if (confirm('Are you sure?')) {
	$.ajax({
	    url: Services.base + Services.items + id + '/-200'
	    , type: 'delete'
	    , success: function(d) {
		row.fadeOut('fast', function() {
		    row.remove();
		})
	    }
	});
    }
//    $("textarea[name=Message]").empty().val('"' + $(this).attr("data-title")  + '" is ready to publish!');
    e.preventDefault();
});
$(document).ajaxStart(function(e) {
    if ($("#progress").length === 0) { //only add progress bar if added yet.
        $("body").append($("<div><dt/><dd/></div>").attr("id", "progress"));
        $("#progress").width((50 + Math.random() * 30) + "%");
    }
});
$(document).ajaxComplete(function() {
    $("#progress").width("101%").delay(200).fadeOut(400, function() { //End loading animation
        $(this).remove();
    });
});
var delay = (function() {
    var timer = 0;
    return function(callback, ms) {
        clearTimeout(timer);
        timer = setTimeout(callback, ms);
    };
})();
// Registering jQuery plugins
$.fn.extend({// isPluginBound plugin
    isPluginBound: function(pluginName) {
        if (jQuery().pluginName) {
            var name = pluginName.toLowerCase();
            return this.data(pluginName) || this.data(name)
                    || this.attr('class').toLowerCase().indexOf(name) !== -1 // vs hasClass()
                    || this.attr('id').toLowerCase().indexOf(name) !== -1;
        }
    }
});
$.fn.extend({
    select2Sortable: function(data){
	var select = $(this);
	$(select).select2(data);
	var ul = $(select).prev('.select2-container').first('ul');
	ul.addClass("farid");
	ul.sortable({
	    placeholder : 'ui-state-highlight',
	    items       : 'li:not(.select2-search-field)',
	    tolerance   : 'pointer',
	    stop: function() {
		$($(ul).find('.select2-search-choice').get().reverse()).each(function() {
		    var id = $(this).data('select2Data').id;
		    var option = select.find('option[value="' + id + '"]')[0];
		    $(select).prepend(option);
		});
	    }
	});
    }
});
$.fn.serializeObject = function() { // serializeArray - serialize form as an array instead of default object
    var o = {};
    var a = this.serializeArray();
    $.each(a, function() {
        if (o[this.name] !== undefined) {
            if (!o[this.name].push) {
                o[this.name] = [o[this.name]];
            }
            o[this.name].push(this.value || '');
        } else {
            o[this.name] = this.value || '';
        }
    });
    return o;
};
$.fn.deepestChild = function() { // Find the deepest child of an element
    if ($(this).children().length === 0)
        return $(this);
    var $target = $(this).children(),
            $next = $target;
    while ($next.length) {
        $target = $next;
        $next = $next.children();
    }
    return $target;
};
jQuery.fn.highlight = function(pat) {
    function innerHighlight(node, pat) {
        var skip = 0;
        if (node.nodeType === 3) {
            var pos = node.data.toUpperCase().indexOf(pat);
            if (pos >= 0) {
                var spannode = document.createElement('span');
                spannode.className = 'highlight';
                var middlebit = node.splitText(pos);
                var endbit = middlebit.splitText(pat.length);
                var middleclone = middlebit.cloneNode(true);
                spannode.appendChild(middleclone);
                middlebit.parentNode.replaceChild(spannode, middlebit);
                skip = 1;
            }
        }
        else if (node.nodeType === 1 && node.childNodes && !/(script|style)/i.test(node.tagName)) {
            for (var i = 0; i < node.childNodes.length; ++i) {
                i += innerHighlight(node.childNodes[i], pat);
            }
        }
        return skip;
    }
    return this.length && pat && pat.length ? this.each(function() {
        innerHighlight(this, pat.toUpperCase());
    }) : this;
};
jQuery.fn.removeHighlight = function() {
    return this.find("span.highlight").each(function() {
        this.parentNode.firstChild.nodeName;
        with (this.parentNode) {
            replaceChild(this.firstChild, this);
            normalize();
        }
    }).end();
};
$.fn.editable.defaults.mode = 'inline'; // Bootstrap editable plugin to init in inline mode
$(document).ajaxSend(function(event, jqxhr, settings) {
    jqxhr.setRequestHeader('authorization', token);
});