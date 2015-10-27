function CustomModalDialog(bodyContainer, title, isSmsize) {
    var self = this;

    var size = "modal-lg";
    if (isSmsize) {
        size = "modal-sm";
    }

    var container = $('<div>').attr({ "class": "modal fade", "tabindex": "-1" }).appendTo('body');
    var dialog = $('<div>').attr({ "class": "modal-dialog " + size }).appendTo(container);
    var modalContent = $('<div>').attr({ "class": "modal-content" }).appendTo(dialog);
    var modalHead = $('<div>').attr({ "class": "modal-header" })
        .html(" <button type='button' class='close' data-dismiss='modal' aria-label='Close><span aria-hidden='true>&times;</span></button>")
        .appendTo(modalContent);
    var modalBody = $('<div>').attr({ "class": "modal-body" }).appendTo(modalContent);
    var modalFooter = $('<div>').attr({ "class": "modal-footer" }).appendTo(modalContent);

    modalHead.append($("<h4>").html(title));
    if ($(bodyContainer).length > 0)
        $(bodyContainer).appendTo(modalBody);
    else
        bodyContainer.appendTo(modalBody);

    self.showDialog = function () {
        container.modal('show');
        setTimeout('SetDialogFocus();', 500);
    };
    self.hideDialog = function () {
        container.modal('hide');
    };

    self.setControlButtonPanel = function (buttonPanel) {
        if ($(buttonPanel).length > 0)
            $(buttonPanel).appendTo(modalFooter);
        else
            buttonPanel.appendTo(modalFooter);
    };

    self.getBodyContainer = function () {
        return bodyContainer;
    };
}

function HideAllDialog() {
    $("div.modal").modal('hide');
}

function SetDialogFocus() {
    $($("div.modal").find("form input[type=text]").first()).focus();
}