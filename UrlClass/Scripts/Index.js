$(() => {
    $("#shortenBtn").on(`click`, function () {
        const url = $("#url").val();
        $.post("/home/shorten", { originalurl: url }, function (obj) {
            $("#shortenedUrl").html(`<a href='${obj}'>${obj}</a>`);
            $("#shortenedUrl").show();
        });
    });
  
   

    

});