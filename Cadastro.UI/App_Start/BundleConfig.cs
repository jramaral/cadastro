using System.Web.Optimization;

namespace Cadastro.UI
{
    public class BundleConfig
    {
        // Para obter mais informações sobre o agrupamento, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"));

            // Use a versão em desenvolvimento do Modernizr para desenvolver e aprender. Em seguida, quando estiver
            // pronto para a produção, utilize a ferramenta de build em https://modernizr.com para escolher somente os testes que precisa.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/datatables.js",
                "~/Scripts/mustache-2.3.2.min.js",
                "~/Scripts/sweetalert2.all.min.js",
                "~/Scripts/respond.js",
                "~/Scripts/Helper/CustomButton.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/datatables.css",
                "~/Content/dataTables.bootstrap4.css",
                "~/Content/vendor/font-awesome/css/font-awesome.min.css",
                "~/Content/sweetalert2.min.css",
                "~/Content/site.css",
                "~/Content/stilo.css",
                "~/Content/menu.css"
            ));

            #region JSViews

            bundles.Add(new ScriptBundle("~/Scripts/alunos").Include(
                "~/Scripts/Compartilhado/ConfiguracaoDataTable.js",
                "~/Scripts/Views/Alunos.js"
            ));
            bundles.Add(new ScriptBundle("~/Scripts/cidades").Include(
                "~/Scripts/Compartilhado/ConfiguracaoDataTable.js",
                "~/Scripts/Views/Cidades.js"
            ));
            bundles.Add(new ScriptBundle("~/Scripts/diretores").Include(
                "~/Scripts/Compartilhado/ConfiguracaoDataTable.js",
                "~/Scripts/Views/Diretores.js"
            ));

            #endregion
        }
    }
}