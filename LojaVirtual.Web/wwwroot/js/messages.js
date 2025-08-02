window.addEventListener("DOMContentLoaded", () => {
    const erro = document.getElementById("mensagem-erro");
    const sucesso = document.getElementById("mensagem-sucesso");

    if (erro) {
        Swal.fire({
            icon: 'error',
            title: 'Erro',
            text: erro.value
        });
    }

    if (sucesso) {
        Swal.fire({
            icon: 'success',
            title: 'Sucesso',
            text: sucesso.value
        });
    }
});