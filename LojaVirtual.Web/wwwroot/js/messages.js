window.addEventListener("DOMContentLoaded", () => {
    const erro = document.getElementById("mensagem-erro");
    const sucesso = document.getElementById("mensagem-sucesso");

    if (erro) {
        document.getElementById("fileSucess").style.display = "none";

        document.getElementById("fileErrorMessage").innerText = erro.value;
        document.getElementById("fileError").style.display = "block";

    }

    if (sucesso) {
        document.getElementById("fileError").style.display = "none";

        document.getElementById("fileSucessMessage").innerText = sucesso.value;
        document.getElementById("fileSucess").style.display = "block";
    }
});