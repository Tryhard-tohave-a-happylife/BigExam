$(document).ready(function() {

    $("button").click(function () {
        var val = $('#NameEnteprise').val();
        var id = $('#selectedCareer option').filter(function () {
            return $(this).val() == val;
        }).data('id');
        var msg = id ? 'xyz=' + id : 'No Match';
        alert(msg)

    })

})