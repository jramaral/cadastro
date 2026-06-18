var ConfigButton = {
    Remover: function () {
        return `
            <button type="button" class="btn btn-danger btn-acao-remover" data-codigo="{{UrlRemover}}">
                                <i class="fa fa-trash-o" aria-hidden="true"></i>
                                 <span class="sr-only">Remover</span>
                              </button>
            <a href="{{UrlEditar}}" class="btn btn-warning rounded" title="Alterar" ">
                <i class="fa fa-pencil-square-o" aria-hidden="true"></i>
             <span class="sr-only">Alterar</span>
            </a>`


    },
    AcaoHref: function () {
        return `
            <button type="button" class="btn btn-danger btn-acao-remover" data-codigo="{{UrlRemover}}">
                                <i class="fa fa-trash-o" aria-hidden="true"></i>
                                 <span class="sr-only">Remover</span>
                              </button>
            <a href="{{UrlEditar}}" class="btn btn-warning rounded" title="Alterar" ">
                <i class="fa fa-pencil-square-o" aria-hidden="true"></i>
             <span class="sr-only">Alterar</span>
            </a>`


    },
    AcaoButton: function () {
        return `
            <button type="button" class="btn btn-danger btn-acao-remover" data-codigo="{{UrlRemover}}">
                                <i class="fa fa-trash-o" aria-hidden="true"></i>
                                 <span class="sr-only">Remover</span>
                              </button>
             <button type="button" class="btn btn-warning btn-acao-editar" data-codigo="{{UrlEditar}}">
                                <i class="fa fa-pencil-square-o" aria-hidden="true"></i>
                                 <span class="sr-only">Editar</span>
                              </button>`

    }

}

