using System.Web;
using System.Web.Optimization;
using TeduShop.Common;

namespace TeduShop.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/js/jquery").Include("~/Assets/client/js/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/js/plugins").Include(
                 "~/Scripts/jquery-ui-1.12.1.js",
                "~/Scripts/mustache/mustache.js",
                "~/Scripts/numeral/numeral.js",
                "~/Scripts/jquery-validation/dist/jquery.validate.js",
                 "~/Assets/client/js/common.js",
                  "~/Assets/client/js/alertify.js",
                 "~/Assets/client/js/UI.js"
                ));

            bundles.Add(new StyleBundle("~/css/base")
                .Include("~/Assets/client/css/bootstrap.css", new CssRewriteUrlTransform())
                //.Include(@"https://use.fontawesome.com/releases/v5.3.1/css/all.css", new CssRewriteUrlTransform())
                .Include("~/Content/themes/base/jquery-ui.css", new CssRewriteUrlTransform())
                .Include("~/Assets/client/css/style.css", new CssRewriteUrlTransform())
                .Include("~/Assets/client/css/custom.css", new CssRewriteUrlTransform())
                );
            BundleTable.EnableOptimizations = bool.Parse(ConfigHelper.GetByKey("EnableBundles"));
        }
    }
}
