using ShoppingCart.Controls;
using ShoppingCart.UWP;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;
using Thickness = Windows.UI.Xaml.Thickness;

[assembly: ExportRenderer(typeof(BorderlessEditor), typeof(BorderlessEditorRenderer))]

namespace ShoppingCart.UWP
{
    /// <summary>
    /// Implementation of Borderless editor control.
    /// </summary>
    public class BorderlessEditorRenderer : EditorRenderer
    {
        #region Methods

        /// <summary>
        /// Used to set the zero border thickness for editor control .
        /// </summary>
        /// <param name="e">The editor</param>
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.BorderThickness = new Thickness(0);
                Control.Margin = new Thickness(0, 4, 0, 0);
            }
        }

        #endregion
    }
}