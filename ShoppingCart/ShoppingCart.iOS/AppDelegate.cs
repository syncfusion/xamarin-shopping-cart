using Foundation;
using Syncfusion.ListView.XForms.iOS;
using Syncfusion.SfBusyIndicator.XForms.iOS;
using Syncfusion.SfMaps.XForms.iOS;
using Syncfusion.SfPullToRefresh.XForms.iOS;
using Syncfusion.SfRating.XForms.iOS;
using Syncfusion.SfRotator.XForms.iOS;
using Syncfusion.XForms.iOS.BadgeView;
using Syncfusion.XForms.iOS.Border;
using Syncfusion.XForms.iOS.Buttons;
using Syncfusion.XForms.iOS.Cards;
using Syncfusion.XForms.iOS.ComboBox;
using Syncfusion.XForms.iOS.Core;
using Syncfusion.XForms.iOS.Expander;
using Syncfusion.XForms.iOS.Graphics;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace ShoppingCart.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public class AppDelegate : FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Forms.Init();
            SfMapsRenderer.Init();
            SfCardViewRenderer.Init();
            SfSegmentedControlRenderer.Init();
            SfRotatorRenderer.Init();
            Core.Init();
            SfExpanderRenderer.Init();
            SfCheckBoxRenderer.Init();
            SfBadgeViewRenderer.Init();
            SfGradientViewRenderer.Init();
            SfRatingRenderer.Init();
            SfComboBoxRenderer.Init();
            SfListViewRenderer.Init();
            SfBorderRenderer.Init();
            SfButtonRenderer.Init();
            SfPullToRefreshRenderer.Init();
            SfAvatarViewRenderer.Init();
            new SfBusyIndicatorRenderer();
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}