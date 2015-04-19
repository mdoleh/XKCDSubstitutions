$("#submit").click(function () {
    var url = $("#url").val();
    $("#result").attr("src", "/Home/GetContent?url=" + url);
    $("#result").removeClass("hidden");

    return false;
});