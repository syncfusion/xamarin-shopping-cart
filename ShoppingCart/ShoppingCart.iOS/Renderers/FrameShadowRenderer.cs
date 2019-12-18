using CoreGraphics;
using ShoppingCart.Controls;
using ShoppingCart.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomShadowFrame), typeof(FrameShadowRenderer))]

namespace ShoppingCart.iOS
{
    /// <summary>
    /// Customize the shadow effects of the Frame control in iOS to make the shadow effects looks similar to Android
    /// </summary>
    public class FrameShadowRenderer : FrameRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Frame> element)
        {
            base.OnElementChanged(element);
            var customShadowFrame = (CustomShadowFrame) Element;
            if (customShadowFrame != null)
            {
                Layer.CornerRadius = customShadowFrame.Radius;
                Layer.ShadowOpacity = customShadowFrame.ShadowOpacity;
                Layer.ShadowOffset =
                    new CGSize(customShadowFrame.ShadowOffsetWidth, customShadowFrame.ShadowOffSetHeight);
                Layer.Bounds.Inset(customShadowFrame.BorderWidth, customShadowFrame.BorderWidth);
                Layer.BorderColor = customShadowFrame.CustomBorderColor.ToCGColor();
                Layer.BorderWidth = (float) customShadowFrame.BorderWidth;
            }
        }
    }
}