$(document).ready(function () {
    $("#submitNewBook").click(function (e) {
        $.ajax({
            url: "api/Books/CreateBook",
            method: "POST",
            data: $("#AddBook").serialize(),
            success: function (response) {
                console.log(response)
                if (response == "Ok") {
                    if (response == "Ok") {
                        toastr.success('Comentário incluído com sucesso!', 'Sucesso!!')
                    } else {
                        toastr.error(response, 'Opsss!!')
                    }
                }
            }
        });
    });
})