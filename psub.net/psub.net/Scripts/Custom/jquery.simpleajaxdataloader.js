(function ($) {
    $.fn.simpleAjaxDataLoader = function (params) {
        params = $.extend({
            serviceUrl: '',
            serviceDelete: '',
            serviceParams: '',
            ejsTemplate: '',
            progressImage: '',
            onLoadComplete: undefined
        }, params);

        var container = this;
        var setInProgress = function () {
            $(container).empty();
            $('<img>').attr({ 'src': params.progressImage }).css({ 'width': '128px', 'height': '15px' }).appendTo(container);
        };
        setInProgress();
        $.post(params.serviceUrl + params.serviceParams,
            function (res) {
                $(container).empty();
                var html = new EJS({ url: params.ejsTemplate }).render({ 'data': res, 'deleteUrl': params.serviceDelete });
                $(container).html(html);
                if (params.onLoadComplete != null && params.onLoadComplete != undefined && typeof params.onLoadComplete == 'function')
                    params.onLoadComplete(container);
                $(".delete").click(function () {
                    var a = $(this);
                    $.post(a.attr("data-url") + "?id=" + a.attr("data-id"),
                          function () {
                              $(a.parent()).remove();
                          });
                });
            });
    }
})(jQuery);