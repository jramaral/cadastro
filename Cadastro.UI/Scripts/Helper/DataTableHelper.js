var tabelaId;

function tabelaHelperAjax(idTabela, pesquisa, ordem, coluna, legenda, seletorQuantidadeRegistros, orderBy, registrosPorPagina, exibirPaginacao) {

    // Defaults
    if (pesquisa === undefined) pesquisa = true;
    if (!ordem) ordem = "asc";
    if (coluna === undefined) coluna = 0;
    if (seletorQuantidadeRegistros === undefined) seletorQuantidadeRegistros = true;
    if (!orderBy) orderBy = [[coluna, ordem]];
    if (!registrosPorPagina) registrosPorPagina = 10;
    if (exibirPaginacao === undefined) exibirPaginacao = true;

    // Controle de página inicial
    var numeroPagina = 0;
    if ($("#NumeroPagina").val()) {
        numeroPagina = (parseInt($("#NumeroPagina").val()) - 1) * registrosPorPagina;
    }

    // Ordenação de datas (caso não use data-order no HTML)
    let datas = $(idTabela + " tbody .sortDate");
    datas.each(function () {
        let partes = $(this).text().split('/');
        if (partes.length === 3) {
            let valorOrdenacao = partes[2] + partes[1] + partes[0];
            $(this).html("<span style='display:none'>" + valorOrdenacao + "</span>" + $(this).text());
        }
    });

    // Evita erro ao reinicializar
    if ($.fn.DataTable.isDataTable(idTabela)) {
        $(idTabela).DataTable().destroy();
    }

    tabelaId = $(idTabela).DataTable({
        order: orderBy,
        pageLength: registrosPorPagina,
        displayStart: numeroPagina,
        lengthChange: seletorQuantidadeRegistros,
        paging: exibirPaginacao,
        searching: pesquisa,
        pagingType: "full_numbers",

        language: {
            emptyTable: "Nenhum registro encontrado",
            info: "Mostrando de _START_ até _END_ de _TOTAL_ registros",
            infoEmpty: "Mostrando 0 até 0 de 0 registros",
            infoFiltered: "(Filtrados de _MAX_ registros)",
            lengthMenu: "_MENU_ registros por página",
            loadingRecords: "Carregando...",
            processing: "Processando...",
            zeroRecords: "Nenhum registro encontrado",
            search: "Pesquisar:",
            paginate: {
                first: "Primeira",
                last: "Última",
                next: "Próxima",
                previous: "Anterior"
            }
        },

        drawCallback: function () {

            // Legenda
            if (legenda) {
                $(idTabela + "_paginate").append(
                    "<div style='float:left; margin-top:5px;'>" +
                    $(idTabela + "_legenda").html() +
                    "</div>"
                );
            }

            // Foco no search
            if (pesquisa) {
                $("input[type=search]").focus();
            }
        }
    });

    // Seleção de linha
    if (!$(idTabela).hasClass('dont-select')) {
        $(idTabela + " tbody").on('click', 'tr', function () {
            $(idTabela + " tbody tr").removeClass("selected");
            $(this).addClass("selected");
        });

        // Primeira linha selecionada
        $(idTabela + " tbody tr").first().addClass("selected");
    }
}

// Remover linha selecionada
function ExcluirRow() {
    if (tabelaId) {
        tabelaId.row('.selected').remove().draw(false);
    }
}