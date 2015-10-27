(function ($) {
    $.fn.lastActionsHistoryOfSection = function (params) {
        params = $.extend({
            serviceUrl: ''
        }, params);

        return this.each(function () {
            var container = this;
        
            $("<span>").html("Идет загрузка, подождите...").appendTo($(container));

            $.post(params.serviceUrl,
                function (res) {
                    var table = $("<table>").attr("class", "table table-condensed table-hover table-bordered table-striped");
                    $(res.Items).each(function (index, element) {
                        $(table).append($("<tr>").append($("<td>").html(element.Text)));
                    });
                    $(table).appendTo($(container).empty());
                });
          
        });
    };

    $.fn.uespSimpleActionLogList = function (params) {
        params = $.extend({
            serviceUrl: '',
            ejsTemplate: '',
            acquaintTasksCreater: '',
            executeTaskCreater: '',
            saveUrl: '',
            progressImage: '',
            closeImageLink: '',
            openImageLink: ''
        }, params);

        return this.each(function () {
            var $container = $(this);
            var $list = $("<table>").addClass("table table-striped table-hover table-bordered");

            $.post(params.serviceUrl,
                function (res) {
                    $(res).each(function (index, element) {
                        $("<tr>").append($("<td>").html(element.DateJson + " <b>" + element.UserName + "</b> " + element.ActionName)).appendTo($list);
                    });
                });

            $container.append($list);
        });
    };
})(jQuery);