function handleFormUpload(formId, url, listaId, errorId, successId) {
    const formElement = document.getElementById(formId);

    if (!formElement) {
        console.error(`Formulário com id '${formId}' não encontrado.`);
        return;
    }

    formElement.addEventListener("submit", function (e) {
        e.preventDefault();

        let formData = new FormData(formElement);

        fetch(url, {
            method: "POST",
            body: formData
        })
            .then(response => response.json())
            .then(data => {
                if (data.success) {
                    if (listaId) document.getElementById(listaId).innerHTML = data.html;
                    if (errorId) {
                        document.getElementById(errorId).innerText = "";
                        document.getElementById(errorId).style.display = "none";
                    }
                    if (successId) {
                        document.getElementById(successId).innerText = data.message;
                        document.getElementById(successId).style.display = "block";
                    }
                    formElement.reset();
                } else {
                    if (successId) document.getElementById(successId).innerText = "";
                    if (errorId) {
                        document.getElementById(successId).style.display = "none";
                        document.getElementById(errorId).innerText = data.message;
                        document.getElementById(errorId).style.display = "block";
                    }
                }
            })
            .catch(err => {
                if (errorId) {
                    document.getElementById(errorId).innerText = "Erro inesperado ao enviar imagem: " + err.message;
                    document.getElementById(errorId).style.display = "block";
                }
                console.error(err);
            });
    });
}
