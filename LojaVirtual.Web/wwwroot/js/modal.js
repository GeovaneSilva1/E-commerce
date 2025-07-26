$(document).on('click', '.abrir-modal', function () {
    var url = $(this).data('url');

    $.get(url, function (html) {
        $('#modalContainer').html(html); 
        var modal = new bootstrap.Modal(document.getElementById('modalExclusao'));
        modal.show();
    });
});