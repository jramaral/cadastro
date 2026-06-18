var ConfigAlunos = function () {

    var tabela;

    return {
        ConfigurarListar: function () {
            console.log("Alunos");


            tabela = $('#tabela_alunos').on('xhr.dt', function (e, settings, json, xhr) {

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
                    url: 'Alunos/ListarAlunos',
                    type: 'POST',
                    'data': function (data) {

                    }
                },
                columns: [
                    {data: "Codigo", title: 'Codigo', width: '10%'},
                    {data: "Nome", title: 'Nome', width: '40%'},
                    {data: "CPF", title: 'CPF', width: '10%'},
                    {data: "Sexo", title: 'Sexo', width: '5%'},
                    {data: "Cidade", title: 'Cidade', width: '20%'},
                    {
                        title: 'Ações',
                        data: "UrlEditar",
                        width: '10%',
                        render: function (data) {
                            return `<a href="${data}" 
                                         class="btn btn-sm btn-primary" 
                                         title="Editar">
                                         <i class="fa fa-pencil-square-o" aria-hidden="true"></i>
                                     </a>`;
                        }
                    },
                ], language: {
                    info: 'Mostrando página _PAGE_ de _PAGES_',
                    lengthMenu: ' _MENU_  registros por página',
                    emptyTable: "Nenhum registro",
                    infoFiltered: '(filtrado de _MAX_ registros totais)'
                    // paginate: {
                    //     first: "Primeira",
                    //     last: "Última",
                    //     next: "Próxima",
                    //     previous: "Anterior"
                    // }
                }
            });

        }
    }
}();