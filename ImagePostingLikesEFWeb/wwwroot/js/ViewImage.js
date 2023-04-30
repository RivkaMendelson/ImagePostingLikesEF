$(() => {
    $('#like-button').on('click', function () {
        const id = $(this).data("image-id");
        $.post('/home/likeImage', { id }, function () {
            
        });

    });


    setInterval(() => {
        const id = ("#image-id").val();
        $.get("/home/getlikes", {id}, result => {
            $("#likes-count").value(result);
        })
    }, 1000);




});