$(document).ready(function () {
    var prevData = "";

    $("input.search").each(function (index) {
        $(this).focusout(function () {
            var searchData = getAllSearchData();

            var data = "";
            $(searchData).each(function (i) {
                if (this.value) {
                    data += this.name + "=" + this.value + "&";
                }
            });

            if (data != prevData) {
                var url = window.location.pathname.split("/");
                var controllerName = url[1];

                prevData = data;
                data = data + "rows=10&page=1";
                $.ajax({
                    data: data,
                    url: "/" + controllerName + "/PagesData",
                    success: function (result) {
                        $("#pagesdata").html(result);
                    }
                });
            }
        });
    });

    var getAllSearchData = function () {
        var array = [];
        $("input.search").each(function (index) {
            array[index] = { name: this.attributes["name"].value, value: $(this).val() };
            //this.attributes["name"].value;
        });
        return array;
    }
});