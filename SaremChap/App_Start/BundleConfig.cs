using System.Web.Optimization;

namespace SaremChap
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/common")
                .Include("~/Scripts/bootstrap.min.js",
                    "~/Scripts/respond.js",
                    "~/Content/plugins/dropzone/dropzone.js",
                    "~/Content/plugins/jquery.validetta/validetta.min.js",
                    "~/Content/plugins/jquery.validetta/validettaLang-fa-IR.js",
                    "~/Content/plugins/owl.carousel/owl-carousel/owl.carousel.min.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/Front/js").Include(
                "~/Content/js/script.js",
                "~/Content/js/jquery.appear.js",
                "~/Content/js/jquery.fitvids.js",
                "~/Content/js/jquery.isotope.min.js",
                "~/Content/js/jquery.lettering.js",
                "~/Content/js/jquery.migrate.js",
                "~/Content/js/jquery.nicescroll.min.js",
                "~/Content/js/jquery.parallax.js",
                "~/Content/js/jquery.textillate.js",
                "~/Content/js/nivo-lightbox.min.js"
                ));


            bundles.Add(new StyleBundle("~/Content/common")
                .Include(
                    "~/Content/css/bootstrap.min.css",
                    "~/Content/css/style.css",
                    "~/Content/css/animate.css",
                    "~/Content/plugins/dropzone/basic.css",
                    "~/Content/plugins/dropzone/dropzone.css",
                    "~/Content/plugins/hover-effect/hover.css",
                    "~/Content/css/responsive.css",
                    "~/Content/css/colors/purple.css",
                    "~/Content/plugins/jquery.validetta/validetta.css")
                .Include("~/Content/css/font-awesome.css",
                    new CssRewriteUrlTransform()
                )
                .Include("~/Content/plugins/owl.carousel/owl-carousel/owl.carousel.css",
                new CssRewriteUrlTransform()
                ));
        }
    }
}
