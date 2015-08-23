function CommentsBuilder(container, getListService, createService, fileLoadService, configCkeditor, likeService, objectType) {
    var self = $(this);
    var newCommentContainer = $("<div>");
    var commentDivId = "commentDivId";
    var commentCount = 0;

    var buildCommentList = function () {
        container.empty();
        $("<h2>").attr("id", commentDivId).appendTo(container);
        newCommentContainer.appendTo(container);
        $.post(getListService,
            function (commentList) {
                if (commentList != undefined && commentList.length > 0) {
                    var ul = $("<ul>").attr("class", "comments mainComments");
                    $(commentList).each(function () {
                        var comment = this;
                        $("<a>").attr({
                            "class": "btn btn-default",
                            "data-userName": comment.Author,
                            "data-id": comment.Id,
                            //"href": "#" + commentDivId
                        }).html("Ответить").click(function () {
                            CKEDITOR.instances[newCommentText.attr("id")].destroy();
                            newCommentContainer.appendTo($(this).parent());
                            CKEDITOR.replace(newCommentText.attr("id"), {
                                //filebrowserImageUploadUrl: fileLoadService,
                                customConfig: configCkeditor
                            });
                        });
                        ul.append(buildCommentDiv(comment));

                    });

                    ul.appendTo(container);

                    $('[data-toggle="tooltip"]').tooltip("destroy");
                    $('[data-toggle="tooltip"]').not("[data-original-title*='<div></div>']").tooltip({ html: true });

                    if (location.hash != "")
                        $('body, html').animate({
                            scrollTop: ($(location.hash).position().top - $("#mainNavbar").innerHeight())
                        }, 500);
                    $(location.hash).delay(500).show("highlight", 2000);

                    //$(".imgpreview").each(function () {
                    //    $(this).fancybox({
                    //        'opacity': true,
                    //        'transitionIn': 'elastic',
                    //        'transitionOut': 'fade',
                    //        'href': $(this).attr('href') == undefined ? $(this).attr('src') : $(this).attr('href')
                    //    });
                    //});

                } else {
                    var ul = $("<ul>").attr("class", "comments mainComments");
                    ul.appendTo(container);
                }
                commentLable.html("Комментарии (" + commentCount + ")");
            }).fail(function (xhr, textStatus, errorThrown) {
                alert(xhr.responseText);
            });
    };

    buildCommentList();

    var commentDiv = $("<div>").appendTo(newCommentContainer);
    var commentLable = $("<h3>").html("Комментарии").appendTo(commentDiv);
    var newCommentText = $("<textarea>").attr({ "id": "newCommentText", "class": "form-control", "placeholder": "Комментировать..." }).appendTo(commentDiv).click(function () {
        CKEDITOR.replace(newCommentText.attr("id"), {
            filebrowserImageUploadUrl: fileLoadService,
            customConfig: configCkeditor, on: {
                'instanceReady': function (evt) { //Set the focus to your editor
                    setCkeditorCursor(CKEDITOR.instances["newCommentText"]);
                }
            }
        });
    });
    var sendCommentButton = $("<button>").html("Отправить комментарий").attr("class", "btn btn-primary commentButton").appendTo(commentDiv);

    sendCommentButton.click(function () {
        sendCommentButton.button('loading');
        saveComment(CKEDITOR.instances[newCommentText.attr("id")].getData(),
                 function (savedComment) {
                     CKEDITOR.instances[newCommentText.attr("id")].destroy();
                     newCommentText.val("");
                     var newComment = buildCommentDiv(savedComment);
                     $($("ul.mainComments").first()).prepend(newComment.append($("<ul>").attr("class", "comments")));
                     newComment.show("highlight", 2000);
                     sendCommentButton.button('reset');
                 },
                 0);
    });

    var buildCommentDiv = function (comment) {
        commentCount++;
        var commentLiItem = $("<li>").attr({ "class": "comment" });
        var commentDivBody = $("<div>").attr({ "class": "commentContent", "data-commentId": comment.Id, "id": comment.Guid, "name": comment.Guid }).appendTo(commentLiItem);
        $("<div>").attr("class", "avatar").append($("<img>").attr("src", comment.UserAvatar)).appendTo(commentDivBody);

        var replyButton = $("<button>").attr({ "class": "btn btn-default btn-xs", "data-action": "reply", "data-commentId": comment.Id }).html("Ответить").click(function () {
            var parentContainer = $(this).parent();
            var replyDiv = $("<div>").appendTo(parentContainer);
            $(this).hide();
            var textareaGuid = guid();
            var textarea = $("<textarea>").attr({ "id": textareaGuid, "name": textareaGuid }).appendTo(replyDiv);
            var sendButton = $("<button>").attr({ "class": "btn btn-primary commentButton", "data-action": "send", "data-commentId": $(this).attr("data-commentId") }).html("Ответить").appendTo(replyDiv).click(function () {
                var sendBtn = $(this);
                sendBtn.button('loading');
                saveComment(CKEDITOR.instances[$($(this).parent().find("textarea").first()).attr("id")].getData(),
                    function (savedComment) {
                        var currentTextarea = $(sendBtn.parent().find("textarea").first());
                        CKEDITOR.instances[currentTextarea.attr("id")].destroy();
                        $(currentTextarea.parent()).empty();
                        var newComment = buildCommentDiv(savedComment);
                        $($("div[data-commentid=" + sendBtn.attr("data-commentId") + "]").parent().find("ul").first()).prepend(newComment);
                        newComment.show("highlight", 2000);
                    },
                    $(this).attr("data-commentId"));
            });

            CKEDITOR.replace(textareaGuid, {
                filebrowserImageUploadUrl: fileLoadService,
                customConfig: configCkeditor, on: {
                    'instanceReady': function (evt) { //Set the focus to your editor
                        setCkeditorCursor(CKEDITOR.instances[textareaGuid]);
                    }
                }
            });
        });

        $("<div>").attr("class", "content avatarPadding")
            .append($("<p>").attr("class", "info").html("<b>" + comment.Author + "</b>"))
            .append($("<span>").html(comment.Comment)).appendTo(commentDivBody)
            .append($("<p>").attr("class", "info").html("<time>" + comment.Date + "&nbsp;" + "</time>")
                .append(buildLike(comment.Id, comment.LikeCount, comment.LikeUsers, comment.DisLikeCount, comment.DisLikeUsers))
                .append(replyButton)).find("img").addClass("imgpreview");;

        if (comment.Replys != null) {
            var div = $("<ul>").attr("class", "comments").appendTo($("<div>").attr("class", "reply_comments"));
            $(comment.Replys).each(function () {
                div.append(buildCommentDiv(this));
            });

            commentLiItem.append(div);
        }

        return commentLiItem;
    };

    var saveComment = function (text, afteSaveFunction, answerId) {
        $.ajax({
            url: createService,
            type: "POST",
            data: JSON.stringify({
                text: text,
                parentId: answerId == undefined ? 0 : answerId
            }),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            cache: false,
            success: function (data) {
                if (!data) {
                    alert("Не удалось сохранить комментарий!");
                } else {
                    if (afteSaveFunction != undefined) {
                        afteSaveFunction(data);
                    }
                    refreshCommentLabel();
                }
            },
            error: function errorFunc(jqXHR) {
                $('#errroMessageDiv').css({ 'color': 'red' }).text(jqXHR.message).show();
            }
        });
    };

    var buildLike = function (id, likeCount, likeUsers, dislikeCount, deslikeUsers) {
        var likeContainer = $("<span>").attr("class", "btn-group");

        var dislikeButton = $("<button>").attr({ "class": "btn btn-danger btn-xs", "data-action": "dislike", "data-id": id, "data-toggle": "tooltip", "data-placement": "top", "data-original-title": "<div>" + deslikeUsers + "</div>" })
            .append($("<b>").html(dislikeCount + "&nbsp;"))
            .append($("<span>").attr("class", "glyphicon glyphicon-thumbs-down"))
            .appendTo(likeContainer);
        var likeButton = $("<button>").attr({ "class": "btn btn-success btn-xs", "data-action": "like", "data-id": id, "data-toggle": "tooltip", "data-placement": "top", "data-original-title": "<div>" + likeUsers + "</div>" })
            .append($("<b>").html(likeCount + "&nbsp;"))
            .append($("<span>").attr("class", "glyphicon glyphicon-thumbs-up"))
            .appendTo(likeContainer);

        likeButton.click(function () {
            setLike($(this), true);

        });

        dislikeButton.click(function () {
            setLike($(this), false);

        });

        return likeContainer;
    };

    var setLike = function (button, isLike) {
        $.post(likeService + '?ObjectId=' + button.attr("data-id") + '&ObjectType=' + objectType + '&IsLike=' + (isLike ? 'True' : 'False'),
                           function (res) {
                               if (res.Id > 0)
                                   button.find("b").html(parseInt(button.find("b").html()) + 1 + "&nbsp;");
                               else
                                   button.find("b").html(parseInt(button.find("b").html()) - 1 + "&nbsp;");
                               if (res.IsLikeBe) {
                                   var likeCountContainer = button.parent().find('[data-action="' + (isLike ? 'dislike' : 'like') + '"]').find("b");
                                   likeCountContainer.html(parseInt(likeCountContainer.html()) - 1 + "&nbsp;");
                               }
                           });
    };

    var refreshCommentLabel = function () {
        commentLable.html("Комментарии (" + commentCount + ")");
    };

    function setCkeditorCursor(ckeditor) {
        try {
            ckeditor.focus();
            var selection = ckeditor.getSelection();
            var range = selection.getRanges()[0];
            var pCon = range.startContainer.getAscendant({ p: 2 }, true); //getAscendant('p',true);
            var newRange = new CKEDITOR.dom.range(range.document);
            newRange.moveToPosition(pCon, CKEDITOR.POSITION_BEFORE_START);
            newRange.select();
        } catch (e) { }
    }

    function guid() {
        function s4() {
            return Math.floor((1 + Math.random()) * 0x10000)
              .toString(16)
              .substring(1);
        }
        return s4() + s4() + '-' + s4() + '-' + s4() + '-' +
          s4() + '-' + s4() + s4() + s4();
    }
}
