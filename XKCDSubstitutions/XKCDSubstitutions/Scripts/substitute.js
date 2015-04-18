$("#submit").click(function () {
    var url = $("#url").val();
    $("#result").attr("src", "/Home/GetContent?url=" + url);
    $("#result").removeClass("hidden");

//    var success = function (data) {
//        $("#result").html(data);
//    }
//
//    var failure = function(error) {
//        alert("error!");
//    }
//
//    $.ajax({
//        headers: {          
//            Accept : "text/plain; charset=utf-8",         
//            "Content-Type": "text/plain; charset=utf-8"   
//        },
//        url: "/Home/GetContent?url=" + url,
//        type: "GET",
//        crossDomain: true,
//        success: success,
//        error: failure
//    });

    return false;
});