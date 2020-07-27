$(function () {

    $("#button").click(function () {
        var val = $('#item').val()
        var xyz = $('#items option').filter(function () {
            return this.value == val;
        }).data('xyz');
        var msg = xyz ? 'xyz=' + xyz : 'No Match';
        alert(msg)

    })

})