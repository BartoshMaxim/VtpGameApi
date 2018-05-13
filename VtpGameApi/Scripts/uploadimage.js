function uploadImage(e) {
    var filename = $(e).val().split("\\").pop(-1);
    var filenameWithoutExt = filename.replace(/\.[^/.]+$/, "");
    $("label.upload-image").text(filename);
    $("input#ImageName").val(filenameWithoutExt);
}