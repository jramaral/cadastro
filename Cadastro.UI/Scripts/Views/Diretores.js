var ConfigDiretores = function () {

    var tabela;
    let codigoSelecionado = null;

    function configurarEventos() {
        
        $('#tabeladiretores tbody')
            .off('click', '.btn-acao-remover')
            .on('click', '.btn-acao-remover', function () {
               
                codigoSelecionado = $(this).data("codigo");
                console.log('Remover');
               

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
                            url: '/Diretor/RemoverDiretor',
                            type: 'POST',
                            data: {codigo: codigoSelecionado},
                            success: function (response) {

                                if (response.sucesso) {
                                    Swal.fire({
                                        position: "top-end",
                                        showConfirmButton: false,
                                        timer: 1500,
                                        // title: "Excluído!",
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

    return {
        ConfigurarListar: function () {
          
            var temp = ConfigButton.AcaoButton();

            console.log(temp);

            tabela = $('#tabeladiretores').on('xhr.dt', function (e, settings, json, xhr) {

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
                    url: 'Diretor/ListarDiretores',
                    type: 'POST',
                    'data': function (data) {

                    }
                },
                columns: [
                    {data: "Codigo", title: 'Codigo', width: '10%'},
                    {data: "Nome", title: 'Nome', width: '30%'},
                    {data: "Email", title: 'Email', width: '30%'},
                    {data: "DataCadastro", title: 'Data Cad.', width: '10%'},
                    {data: "Cidade", title: 'Cidade', width: '10%'},
                    {data: "UrlExibir", title: 'Ações', width: '10%'},
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
                        targets: 5
                    }
                ],
                initComplete: function () {
                }
            });
            configurarEventos();
        }
    }
}();