$(document).on('click', '.abrir-modal-exclusao', function () {
    var url = $(this).data('url');

    $.get(url, function (html) {
        $('#modalContainer').html(html); 
        var modal = new bootstrap.Modal(document.getElementById('modalExclusao'));
        modal.show();
    });
});

$(document).on('click', '.abrir-modal-edicao', function () {
    var url = $(this).data('url');

    $.get(url, function (html) {
        $('#modalContainer').html(html);
        var modal = new bootstrap.Modal(document.getElementById('modalEdicao'));
        modal.show();
    });
});