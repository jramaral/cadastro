var tabelaId;

function tabelaBoostrapListaSelecao(idTabela, pesquisa, ordem, coluna, legenda, seletorQuantidadeRegistros, orderBy) {

    tabelaBoostrap(idTabela, pesquisa, ordem, coluna, legenda, seletorQuantidadeRegistros, orderBy, 100, false);

}

function tabelaBoostrap(idTabela, pesquisa, ordem, coluna, legenda, seletorQuantidadeRegistros, orderBy, registrosPorPagina, exibirPaginacao) {

    /// <summary>Transoforma tabela em tabela paginado e pesquisas</summary>  
    /// <param name="idTabela" type="text">Id da Tabela</param>  
    /// <param name="pesquisa" type="bool">Se quer com pesquisa ou não</param>  
    /// <param name="ordem" type="text">asc or desc</param>  
    /// <param name="coluna" type="Number">Número da coluna que quer ordenar</param>  
    /// <param name="legenda" type="bool">Tem que ter uma legenda com class legendaTabela</param> 
    /// <param name="seletorQuantidadeRegistros" type="bool">Ativa o seletor para usuario definir quantidade de regristros da tabela</param>
    /// <param name="orderBy" type="array">Passe um array com os parametros no seguinte formato [[0,'desc'],[3,'asc']]</param>
    /// <param name="registrosPorPagina" type="int">Quantidade de linhas por tabela.</param>

    /// <returns>datatable</returns>  
    if (undefined == pesquisa) {
        pesquisa = false;
    }
    if (undefined === ordem || "" === ordem || false) {
        ordem = "desc";
    }
    if (undefined === coluna || "" === coluna || false) {
        coluna = 0;
    }

    if (undefined == seletorQuantidadeRegistros) {
        seletorQuantidadeRegistros = false;
    }


    //order by da tabela com varios parametros
    if (undefined === orderBy || "" === orderBy || false) {
        orderBy = [coluna, ordem];
    }

    //tamanho da pagina

    if (undefined === registrosPorPagina) {
        registrosPorPagina = 10;
    }


    if (undefined === exibirPaginacao || "" === exibirPaginacao) {
        exibirPaginacao = true;
    }

    var numeroPagina = 0;
    if ($("#NumeroPagina").val() !== undefined && $("#NumeroPagina").val() !== "" && $("#NumeroPagina").val() !== "0") {
        numeroPagina = (parseInt($("#NumeroPagina").val()) - 1) * registrosPorPagina;
    }
    // Datas Sort Desc or Asc


    let datas = $(idTabela + " tbody tr .sortDate");


    if (datas.length > 0) {
        datas.each(function () {
            var day = $(this).text().split('/')[0];
            var month = $(this).text().split('/')[1];
            var year = $(this).text().split('/')[2];
            var soma = year + month + day;
            $(this).html("<span style='display:none'>" + soma + "</span>" + $(this).text());
        });
    }

    $(idTabela).DataTable().destroy();

    tabelaId = $(idTabela).DataTable({
        "order": orderBy,
        "pageLength": registrosPorPagina,
        "iDisplayStart": numeroPagina,
        "bLengthChange": seletorQuantidadeRegistros,
        "bPaginate": exibirPaginacao,
        "filter": pesquisa, // this is for disable filter (search box)
        "language": {
            "sEmptyTable": "Nenhum registro encontrado",
            "sInfo": "Mostrando de _START_ até _END_ de _TOTAL_ registros",
            "sInfoEmpty": "Mostrando 0 até 0 de 0 registros",
            "sInfoFiltered": "(Filtrados de _MAX_ registros)",
            "sInfoPostFix": "",
            "sInfoThousands": ".",
            "sLengthMenu": "_MENU_",
            "sLoadingRecords": "Carregando...",
            "sProcessing": "Processando...",
            "sZeroRecords": "Nenhum registro encontrado",
            "sSearch": "Pesquisar",
            "oPaginate": {
                "sNext": "Próximo",
                "sPrevious": "Anterior",
                "sFirst": "Primeiro",
                "sLast": "Último"
            },
            "oAria": {
                "sSortAscending": ": Ordenar colunas de forma ascendente",
                "sSortDescending": ": Ordenar colunas de forma descendente"
            }
        },
        "fnDrawCallback": function () {
            // Depois de desenhar tabela
            if (legenda) {
                $(idTabela + "_paginate").append("<div style='float: left;'>" + $(idTabela + "_legenda").html() + "</div>");
            }


            if (pesquisa) {

                let search_input = $("input.form-control.input-sm");

                search_input.focus();


            }
        }
    });

    if (!$(idTabela).hasClass('dont-select')) {

        // Selecteds
        $(idTabela + " tbody tr").first("tr").addClass("selected");
        // remove selecao
        $(".dataTables_filter input[type=search]").keydown(function () {
            $(this).closest(".dataTables_wrapper").find("table tbody .selected").removeClass("selected");
        });
    }
}

function ExcluirRow() {
    tabelaId.row('.selected').remove().draw(false);
}