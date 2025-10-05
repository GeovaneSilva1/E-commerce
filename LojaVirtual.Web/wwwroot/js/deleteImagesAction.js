$(document).off("click", ".deletaImagem").on("click", ".deletaImagem", function (e) {
    e.preventDefault();

    let handle = $(this).data("handle");

    if (!confirm("Deseja realmente deletar esta imagem"))
    {
        return;
    }
    $.ajax({
        url: "/Produtos/DeleteImages",
        type: "POST",
        data: { imageHandle: handle },
        success: function (data) {
            if (data.success) {
                document.getElementById("fileErrorImages").style.display = "none";

                document.getElementById("fileSucessImages").innerText = data.message;
                document.getElementById("fileSucessImages").style.display = "block";
                // remove a div da imagem da tela
                $("#img-" + handle).remove();
            } else {
                document.getElementById("fileSucessImages").style.display = "none";

                document.getElementById("fileErrorImages").innerText = data.message;
                document.getElementById("fileErrorImages").style.display = "block";
            }
        },
        error: function () {
            alert("Erro ao deletar a imagem.");
        }
    });
});