using System.Web.Optimization;

namespace Phonebook.Web
{
    public class BundleConfig
    {
        // Per altre informazioni sulla creazione di bundle, vedere https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/core").Include(
                         "~/Content/js/jquery-sortable-min.js",
                         "~/Content/js/jquery-ui-1.12.1.js",
                        "~/Content/js/bootstrap.min.js",
                        "~/Content/js/material.min.js",
                        "~/Scripts/bootbox.js",
                        "~/Scripts/datatables/jquery.datatables.js",
                        "~/Scripts/datatables/datatables.bootstrap.js",
                        "~/Scripts/datatables/datatables.material.min.js",
                        "~/Scripts/bootstrap-select.min.js",
                        "~/Scripts/jquery.validate.min.js",
                        "~/Scripts/jquery.validate.unobtrusive.min.js"
                        ));


            bundles.Add(new ScriptBundle("~/bundles/chartist").Include(
                     "~/Content/js/chartist.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap-notify").Include(
                        "~/Content/js/bootstrap-notify.js"));

            bundles.Add(new ScriptBundle("~/bundles/material-dashboard").Include(
                        "~/Content/js/material-dashboard.js"));

            bundles.Add(new ScriptBundle("~/bundles/Site").Include(
                        "~/Content/js/site.js"));

            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
                      "~/Content/bootstrap-select.min.css",
                      "~/Content/css/bootstrap.min.css"));

            bundles.Add(new StyleBundle("~/Content/material-dashboard").Include(
                      "~/Content/css/material-dashboard.css"
                      ));

            bundles.Add(new StyleBundle("~/Content/website").Include(
                      "~/Content/css/site.css",
                      "~/Content/datatables/css/datatables.bootstrap.css",
                      "~/Content/datatables/css/datatables.material.min.css"
                      ));
        }
    }
}
