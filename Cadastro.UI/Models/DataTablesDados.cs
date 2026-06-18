namespace Cadastro.UI.Models
{
    public class DataTablesDados
    {
        public int draw { get; set; }

        public int start { get; set; }

        public int length { get; set; }

        // public Search search { get; set; }
        //
        // public Order[] order { get; set; }

        public Column[] columns { get; set; }

        public int id { get; set; }
        public string FiltroSelecionado { get; set; }
    }

    public class Column
    {
        public string data { get; set; }
        public string name { get; set; }
        public bool searchable { get; set; }

        public bool orderable { get; set; }

        // public Search search { get; set; }
        public bool visible { get; set; }
    }
}