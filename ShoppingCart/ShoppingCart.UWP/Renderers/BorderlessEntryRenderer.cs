using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using ShoppingCart.Controls;
using ShoppingCart.UWP;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;
using Setter = Windows.UI.Xaml.Setter;
using Style = Windows.UI.Xaml.Style;
using Thickness = Windows.UI.Xaml.Thickness;

[assembly: ExportRenderer(typeof(BorderlessEntry), typeof(BorderlessEntryRenderer))]

namespace ShoppingCart.UWP
{
    public class BorderlessEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.BorderThickness = new Thickness(0);
                Control.VerticalAlignment = VerticalAlignment.Center;

                // Make the text vertically aligned at centre of the entry.
                var style = new Style(typeof(ContentControl));
                style.Setters.Add(new Setter(VerticalAlignmentProperty, VerticalAlignment.Center));
                Control.Resources.Add(typeof(ContentControl), style);
            }
        }
    }
}