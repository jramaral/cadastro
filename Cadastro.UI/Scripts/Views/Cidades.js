var ConfigCidade = function () {

    var tabela;
    let codigoSelecionado = null;

    function configurarEventos() {
        $('#tabelacidades tbody')
            .off('click', '.btn-acao-remover')
            .on('click', '.btn-acao-remover', function () {
                codigoSelecionado = $(this).data("codigo");
                console.log('Remover');
                //$("#modalConfirmacao").modal();

                Swal.fire({
                    title: "Tem certeza?",
                    text: "Você não poderá reverter isso!",
                    icon: "warning",
                    showCancelButton: true,
                    confirmButtonColor: "#3085d6",
                    cancelButtonColor: "#d33",
                    cancelButtonText: 'Cancelar',
                    confirmButtonText: "Sim, excluir!"
                }).then((result) => {

                    console.log(result);
                    if (result.isConfirmed) {
                        $.ajax({
                            url: '/Cidade/RemoverCidade',
                            type: 'POST',
                            data: {codigo: codigoSelecionado},
                            success: function (response) {

                                if (response.sucesso) {
                                    Swal.fire({
                                        position: "top-end",
                                        showConfirmButton: false,
                                        timer: 1500,
                                        title: "Excluído!",
                                        text: "Registro removido.",
                                        icon: "success"
                                    });

                                } else {
                                    Swal.fire("Erro!", response.mensagem, "error");
                                }
                                // opcional: atualizar tabela, remover linha, etc
                                tabela.clear().ajax.reload();
                            },
                            error: function () {
                                Swal.fire("Erro!", "Falha na requisição!", "error");
                            }
                        });
                    }
                });

            })
    }

    function gerarPdf() {

       
    }

    return {
        ConfigurarListar: function () {
            console.log("Cidades");


            var temp = ConfigButton.Remover();

            console.log(temp);

            tabela = $('#tabelacidades').on('xhr.dt', function (e, settings, json, xhr) {

            }).on('processing.dt', function (e, settings, processing) {

            }).DataTable({
                processing: true,
                serverSide: true,
                lengthMenu: [10, 25, 50],
                pageLength: 10,
                colReorder: true,
                searching: false,
                autoWidth: false,

                ajax: {
                    url: 'Cidade/ListarCidades',
                    type: 'POST',
                    'data': function (data) {

                    }
                },
                columns: [
                    {data: "Codigo", title: 'Codigo', width: '10%'},
                    {data: "Cidade", title: 'Cidade', width: '40%'},
                    {data: "Uf", title: 'UF', width: '10%'},
                    {data: "Cep", title: 'CEP', width: '5%'},
                    {
                        data: "UrlExibir", title: 'Ações', width: '10%'
                        // title: 'Ações',
                        // data: "UrlEditar",
                        // width: '10%',
                        // render: function (data) {
                        //     return `<a href="${data}" 
                        //                  class="btn btn-sm btn-primary" 
                        //                  title="Editar">
                        //                  <i class="fa fa-pencil-square-o" aria-hidden="true"></i>
                        //              </a>`;
                        // }
                    },
                ], language: DataTables.Linguagem(),
                columnDefs: [
                    {
                        render: function (data, type, row) {
                            // var btn1 = Mustache.render(templateAlterar, row);
                            var btn1 = Mustache.render(temp, row);

                            var $divBotoes = $("<div>")
                                // .append(btn1)
                                .append(btn1);

                            return $divBotoes.html();
                        },
                        targets: 4
                    }
                ],
                initComplete: function () {
                    let api = this.api();
                    console.log(this.api);
                }
            });
            configurarEventos();
           
        },
        ImprimirPdf: function () {
            $.ajax({

                url: '/Cidade/GerarPdf',
                type: 'POST',
                data: {
                    id: 123
                },
                xhrFields: {
                    responseType: 'blob'
                }, // importante para receber o arquivo como blob
                success: function (blob) {
                    var fileURL = URL.createObjectURL(blob);

                    window.open(fileURL, '_blank');
                }

            });
        }
    }
}();