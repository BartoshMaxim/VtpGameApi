//$(document).ready(function () {
//    $("div.cake-button").each(function (index) {
//        $(this).click(function (e) {
//            if (!$(this).hasClass("active")) {
//                if (this.id == "exists-image") {
//                    $("#new-image").removeClass("active");
//                    ajaxExistsImage();
//                    $(this).addClass("active");
//                }
//                else if (this.id == "new-image") {
//                    $("#exists-image").removeClass("active");
//                    ajaxNewImage();
//                    $(this).addClass("active");
//                }

//            }
//        });
//    });

//    var ajaxExistsImage = function () {
//        var data = "rows=10&page=1";
//        $.ajax({
//            data: data,
//            url: "/Image/PagesData",
//            success: function (result) {
//                $("#update").html(result);
//            }
//        });
//    }

//    var ajaxNewImage = function () {
//        $.ajax({
//            url: "/Image/UploadImageModel",
//            success: function (result) {
//                $("#update").html(result);
//            }
//        });
//    }
//});

//var changeParentColor = function (e) {
//    $('#update').find('input[type="radio"]').each(function () {
//        var ischecked = $(this).is(":checked");
//        if (!ischecked && $(this).parent().parent().hasClass("green-back")) {
//            $(this).parent().parent().removeClass("green-back");
//        }
//        else if (ischecked) {
//            $(this).parent().parent().addClass("green-back");
//        }
//    });
//}

function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();


        $(reader).on("load", { value: $(input).attr('id') }, function (e) {
            var eventdata = e.data.value;

            $('div.' + eventdata + ' label').text("");
            $('div.' + eventdata).append('<a class="icon remove"></a>');

            $('div.' + eventdata + ' label').append("<img />");
            $('div.' + eventdata + ' label img').attr('src', e.target.result);
            $('div.' + eventdata + ' label').removeAttr("for");
            $('div.' + eventdata + ' a').on("click", { value: eventdata }, function (e) {
                var eventval = e.data.value;
                $('#' + eventval + '').val("");
                //$('#' + eventval).attr("type", "file");
                $('div.' + eventval + ' label').text("+");
                $('div.' + eventval + ' a').remove();
                $('div.' + eventval + ' label img').remove();
                $('div.' + eventval + ' label').attr("for", eventval);
            });
        });
        reader.readAsDataURL(input.files[0]);
    }
};

