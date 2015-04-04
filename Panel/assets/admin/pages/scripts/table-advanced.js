var TableAdvanced = function() {

    var initItemsTable = function() {
        var table = $('#items');
        var oTable = table.dataTable({
            "aaSorting": [[0, 'desc']]
            , "aLengthMenu": [
                [-1, 10, 25, 100],
                ["All", 10, 25, 100] // change per page values here
            ]
            , "iDisplayLength": -1 // set the initial value
                    // horizobtal scrollable datatable
            , "sDom": "<'row' <'col-md-12'T>><'row'<'col-md-6 col-sm-12'l><'col-md-6 col-sm-12'f>r><'table-scrollable't><'row'<'col-md-5 col-sm-12'i><'col-md-7 col-sm-12'p>>"
            , 'aoColumnDefs': [{
                    'bSortable': false,
                    'aTargets': ['nosort']
                }]
        });

    };
    return {
        //main function to initiate the module
        init: function() {
            if (!jQuery().dataTable) {
                return;
            }
            initItemsTable();
        }
    };
}();