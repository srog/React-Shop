﻿using System.Web.Optimization;
using System.Web.Optimization.React;

namespace ReactShop.Web
{
    public static class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new BabelBundle("~/bundles/js").Include(
                "~/Scripts/jquery-3.1.0.min.js",
                "~/Scripts/jquery-ui-1.12.0.min.js",
                "~/Scripts/jquery-validate.min.js",
                "~/Scripts/jquery.unobtrusive-ajax.min.js",
                "~/Scripts/react/react.min.js",
                "~/Scripts/react/react-dom.min.js",
                "~/Scripts/react/react-with-addons.min.js",
                "~/Scripts/react-bootstrap.js",
                "~/Scripts/Components.jsx",
                "~/Scripts/Cart.jsx",
                "~/Scripts/CheckoutConfirm.jsx",
                "~/Scripts/CheckoutSuccess.jsx",
                "~/Scripts/showdown.js",
                "~/Scripts/loginpopup.js"
            ));

              // Forces files to be combined and minified in debug mode
              // Only used here to demonstrate how combination/minification works
              // Normally you would use unminified versions in debug mode.
              BundleTable.EnableOptimizations = true;
        }
    }
}
