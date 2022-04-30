using System.Collections.Generic;
using System.Web;
using System.Web.Optimization;

namespace eCommerce.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region Public Portal Bundles

            //Public Portal CSS Bundles
            bundles.Add(new StyleBundle("~/bundles/content/css").Include(
                      "~/content/bm3/stylesheets/bootstrap.min.css",
                      "~/content/bm3/stylesheets/cloud-zoom.css",
                      "~/content/bm3/stylesheets/flexslider.css",
                      "~/content/bm3/stylesheets/font-awesome.css",
                      "~/content/bm3/stylesheets/font-themify.css",
                      "~/content/bm3/stylesheets/jquery-ui.css",
                      "~/content/bm3/stylesheets/mCSB_buttons.html",
                      "~/content/bm3/stylesheets/mCustomScrollbar.css",
                      "~/content/bm3/stylesheets/owl.carousel.css",
                      "~/content/bm3/stylesheets/owl.video.play.html",
                      "~/content/bm3/stylesheets/responsive.css",
                      "~/content/bm3/stylesheets/shortcodes.css",
                      "~/content/bm3/stylesheets/style.css",
                      "~/content/bm3/stylesheets/waves.min.css"));

            //Public Portal JavaScript/jQuery for Header
            bundles.Add(new ScriptBundle("~/bundles/content/hscripts").Include(
                         "~/content/bm3/javascript/jquery.min.js",
                         "~/content/bm3/javascript/tether.min.js", 
                         "~/content/bm3/javascript/bootstrap.min.js",
                         "~/content/bm3/javascript/waypoints.min.js",
                         "~/content/bm3/javascript/jquery.circlechart.js", 
                         "~/content/bm3/javascript/easing.js",
                         "~/content/bm3/javascript/jquery.zoom.min.js",
                         "~/content/bm3/javascript/jquery.flexslider-min.js",
                         "~/content/bm3/javascript/owl.carousel.js",
                         "~/content/bm3/javascript/smoothscroll.js",
                         "~/content/bm3/javascript/jquery-ui.js",
                         "~/content/bm3/javascript/jquery.mCustomScrollbar.js",
                         "~/content/bm3/javascript/gmap3.min.js",
                         "~/content/bm3/javascript/waves.min.js",
                         "~/content/bm3/javascript/jquery.countdown.js", 
                         "~/content/bm3/javascript/main.js"));

            bundles.Add(new StyleBundle("~/bundlesbm3/content/css").NonOrdering().Include(
                       "~/Content/bm3/stylesheets/bootstrap.min.css",
                       "~/Content/bm3/stylesheets/cloud-zoom.css",
                       "~/Content/bm3/stylesheets/flexslider.css",
                       "~/Content/bm3/stylesheets/font-awesome.css",
                       "~/Content/bm3/stylesheets/font-themify.css",
                       "~/Content/bm3/stylesheets/jquery-ui.css",
                       "~/Content/bm3/stylesheets/slick.css",
                       "~/Content/bm3/stylesheets/slick-theme.css",
                       "~/Content/bm3/stylesheets/slick-lightbox.css",                         
                       "~/Content/bm3/stylesheets/mCustomScrollbar.css",
                       "~/Content/bm3/stylesheets/owl.carousel.css",
                       "~/Content/bm3/stylesheets/owl.video.play.html",                                            
                       "~/Content/bm3/stylesheets/waves.min.css",
                       "~/content/bm3/stylesheets/shortcodes_2.css",
                       "~/content/bm3/stylesheets/style_2.css",
                       "~/content/bm3/stylesheets/responsive_2.css"));

            //Public Portal JavaScript/jQuery for Header
            bundles.Add(new ScriptBundle("~/bundlesbm3/content/hscripts").NonOrdering().Include(
                         "~/content/bm3/javascript/jquery.min.js",
                         "~/content/bm3/javascript/jquery-ui.js",
                         "~/content/bm3/javascript/easing.js",
                         "~/content/bm3/javascript/gmap3.min.js",
                         "~/content/bm3/javascript/imagesloaded.pkgd.min.js",
                         "~/content/bm3/javascript/isotope.pkgd.min.js",
                         "~/content/bm3/javascript/jquery.circlechart.js",
                         "~/content/bm3/javascript/jquery.countdown.js",
                         "~/content/bm3/javascript/jquery.flexslider-min.js",
                         "~/content/bm3/javascript/jquery.mCustomScrollbar.js",
                         "~/content/bm3/javascript/jquery.zoom.min.js",
                         "~/content/bm3/javascript/owl.carousel.js",
                         "~/content/bm3/javascript/smoothscroll.js",
                         "~/content/bm3/javascript/tether.min.js",
                         "~/content/bm3/javascript/waves.min.js",
                         "~/content/bm3/javascript/slick.js",
                         "~/content/bm3/javascript/slick-lightbox.js",
                         "~/content/bm3/javascript/waypoints.min.js",
                         "~/content/bm3/javascript/bootstrap.min.js",
                         "~/content/lib/sweetalert2-9.10.7/sweetalert2.all.min.js",
                         "~/content/bm3/javascript/main.js"));

            #endregion

            #region Dashboard Bundles
            //Dashboard CSS Bundles
            bundles.Add(new StyleBundle("~/bundles/dashboard/content/css").NonOrdering().Include(
                      "~/content/templates/sbadmin2/css/sb-admin-2.min.css", //this also has Bootstrap in it
                      "~/content/lib/jquery-ui-1.12.1/jquery-ui.min.css",
                      "~/content/css/dashboard.css"));

            //Dashboard CSS Bundles for RTL
            bundles.Add(new StyleBundle("~/bundles/dashboard/content/rtl/css").NonOrdering().Include(
                      "~/content/templates/sbadmin2/css/sb-admin-2.rtl.min.css", //this also has Bootstrap in it
                      "~/content/lib/jquery-ui-1.12.1/jquery-ui.min.css",
                      "~/content/css/dashboard.css"));

            //JavaScript/jQuery for Header
            bundles.Add(new ScriptBundle("~/bundles/dashboard/content/scripts").NonOrdering().Include(
                        "~/content/lib/jquery-3.4.1/jquery.min.js",
                        "~/content/lib/sweetalert2-9.10.7/sweetalert2.all.min.js",
                        "~/content/lib/jquery-ui-1.12.1/jquery-ui.min.js",
                        "~/content/lib/jquery-validation-1.19.1/jquery.validate.min.js",
                        "~/content/lib/jquery.cookie-1.4.1/jquery.cookie.js",
                        "~/content/lib/moment.js-2.24.0/moment.min.js",
                        "~/content/lib/popperjs-1.16.0/popper.js",
                        "~/content/lib/bootstrap-4.4.1/js/bootstrap.min.js",
                        "~/content/lib/ms-dropdown-3.5.2/jquery.dd.min.js",
                        "~/Content/lib/darkreader-4.9.16/darkreader.min.js",
                        "~/content/js/dashboard.js"));
            #endregion
        }
    }

    class NonOrderingBundleOrderer : IBundleOrderer
    {
        public IEnumerable<BundleFile> OrderFiles(BundleContext context, IEnumerable<BundleFile> files)
        {
            return files;
        }
    }
    
    static class BundleExtentions
    {
        public static Bundle NonOrdering(this Bundle bundle)
        {
            bundle.Orderer = new NonOrderingBundleOrderer();
            return bundle;
        }
    }
}
