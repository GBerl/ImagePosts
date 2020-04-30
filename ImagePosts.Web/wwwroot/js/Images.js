$(() => {
    $("#btn").on('click', function(){
        var id = $(this).data('id');
        $("#btn").attr('disabled', true);
        var image = {
            id: id,
            likes:$("#likes").text()
        }
        console.log(image);
        $.post(`/home/likeimage`, image, function(i){
            $("#likes").val(i);
        });
    })
    setInterval(() => {
        var id = $("#id").val();
        $.get(`/home/likescount?id=${id}`, function (count) {
            $("#likes").val(count);
        });
    }, 500)
});