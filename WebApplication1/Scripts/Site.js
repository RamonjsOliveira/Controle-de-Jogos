
    function BloqueiaAmigo(url) {
        event.preventDefault();
        swal({
            title: "Bloquer amigo?",
            text: "Ao bloquear, o amigo não podera pegar jogos emprestado!",
            icon: "warning",
            confirmButtonText: 'Sim, Bloquear!',
            cancelButtonText: 'Não, Cancelar',
            showCancelButton: true,
            dangerMode: true,
        }).then(function (isConfirm) {
            if (isConfirm.dismiss != 'cancel') {
                $.ajax({
                    url: url,

                    type: "POST",
                    data: {

                    },
                    dataType: "html",
                    success: function () {
                        swal("Feito!", "O amigo foi bloqueado.", "success").then(function () { location.reload() });
                        //setTimeout(function () {location.reload()},3000);
                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        swal("Ooooops!", "Não foi possível bloquear o amigo.", "error");
                    }
                });
            } else {
                swal("Cancelado", "Seu amigo não foi bloqueado.", "error");
            }
        });
    }

function LiberaAmigo(url) {
        event.preventDefault();
        swal({
            title: "Liberar amigo?",
            text: "Ao liberar, o amigo podera pegar jogos emprestado novamente!",
            icon: "warning",
            confirmButtonText: 'Sim, Liberar!',
            cancelButtonText: 'Não, Cancelar',
            showCancelButton: true,
            dangerMode: true,
        }).then(function (isConfirm) {
            if (isConfirm.dismiss != 'cancel') {
                $.ajax({
                    url: url,

                    type: "POST",
                    data: {

                    },
                    dataType: "html",
                    success: function () {
                        swal("Feito!", "O amigo foi liberado.", "success").then(function () { location.reload() });
                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        swal("Ooooops!", "Não foi possível liberar o amigo.", "error");
                    }
                });
            } else {
                swal("Cancelado", "Seu amigo não foi liberado.", "error");
            }
        });
    }

function DevolverJogo(url) {
        event.preventDefault();
        swal({
            title: "Devolver jogo",
            text: "O jogo foi devolvido?",
            icon: "warning",
            confirmButtonText: 'Sim, continuar',
            cancelButtonText: 'Não, cancelar',
            showCancelButton: true,
            dangerMode: true,
        }).then(function (isConfirm) {
            if (isConfirm.dismiss != 'cancel') {
                $.ajax({
                    url: url,

                    type: "POST",
                    data: {

                    },
                    dataType: "html",
                    success: function () {
                        swal("Feito!", "O jogo está disponível novamente", "success").then(function () { location.reload() });
                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        swal("Ooooops!", "O jogo continua emprestado.", "error");
                    }
                });
            } else {
                swal("Cancelado", "Nenhuma alteração foi feita", "error");
            }
        });
    }

function Emprestar(url, amigos) {
        event.preventDefault();
        swal({
            title: 'Para que amigo quer esmprestar?',
            input: 'select',
            inputOptions:
                amigos
            ,
            inputPlaceholder: 'Selecione o amigo',
            confirmButtonText: 'Continuar',
            cancelButtonText: 'Cancelar',
            showCancelButton: true,
            inputValidator: function (value) {
                return new Promise(function (resolve, reject) {
                    if (value !== '') {
                        url += "?amigoId=" + value;
                        resolve($.ajax({
                            url: url,

                            type: "POST",
                            data: {

                            },
                            dataType: "html",
                            success: function () {
                                swal("Feito!", "O jogo foi emprestado.", "success").then(function () { location.reload() });
                            },
                            error: function (xhr, ajaxOptions, thrownError) {
                                swal("Ooooops!", "O jogo continua disponível.", "error");
                            }
                        }));
                    } else {
                        reject('Selecione um Amigo')
                    }
                })
            }
        }).then(function (result) {
            swal({
                type: 'warning',
                html: 'Operação Cancelada!!'
            })
        })
    }