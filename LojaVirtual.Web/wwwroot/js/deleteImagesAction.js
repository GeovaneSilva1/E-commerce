$(document).off("click", ".deletaImagem").on("click", ".deletaImagem", function () {
    let handle = $(this).data("handle");

    $.ajax({
        url: "/Produtos/DeleteImages",
        type: "POST",
        data: { imageHandle: handle },
        success: function (response) {
            alert("Imagem deletada com sucesso!");
            // aqui você pode remover a div da imagem da tela, ex:
            $("#img-" + handle).remove();
        },
        error: function () {
            alert("Erro ao deletar a imagem.");
        }
    });
});