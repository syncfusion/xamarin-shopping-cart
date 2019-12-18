using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace ShoppingCart.Controls
{
    /// <summary>
    /// Customizes the shadow effects of the Frame control in iOS to make the shadow effects looks similar to Android.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class CustomShadowFrame : Frame
    {
        public static readonly BindableProperty RadiusProperty =
            BindableProperty.Create("Radius", typeof(float), typeof(CustomShadowFrame), 0f, BindingMode.Default);

        public static readonly BindableProperty CustomBorderColorProperty =
            BindableProperty.Create("CustomBorderColor", typeof(Color), typeof(CustomShadowFrame), default(Color),
                BindingMode.Default);

        public static readonly BindableProperty BorderWidthProperty =
            BindableProperty.Create("BorderWidth", typeof(int), typeof(CustomShadowFrame), default(int),
                BindingMode.Default);

        public static readonly BindableProperty ShadowOpacityProperty =
            BindableProperty.Create("ShadowOpacity", typeof(float), typeof(CustomShadowFrame), 0.12F,
                BindingMode.Default);

        public static readonly BindableProperty ShadowOffsetWidthProperty =
            BindableProperty.Create("ShadowOffsetWidth", typeof(float), typeof(CustomShadowFrame), 0f,
                BindingMode.Default);

        public static readonly BindableProperty ShadowOffSetHeightProperty =
            BindableProperty.Create("ShadowOffSetHeight", typeof(float), typeof(CustomShadowFrame), 4f,
                BindingMode.Default);

        // Gets or sets the radius of the Frame corners.
        public float Radius
        {
            get => (float) GetValue(RadiusProperty);
            set => SetValue(RadiusProperty, value);
        }

        // Gets or sets the border color of the Frame.
        public Color CustomBorderColor
        {
            get => (Color) GetValue(CustomBorderColorProperty);
            set => SetValue(CustomBorderColorProperty, value);
        }

        // Gets or sets the border width of the Frame.
        public int BorderWidth
        {
            get => (int) GetValue(BorderWidthProperty);
            set => SetValue(BorderWidthProperty, value);
        }

        // Gets or sets the shadow opacity of the Frame.
        public float ShadowOpacity
        {
            get => (float) GetValue(ShadowOpacityProperty);
            set => SetValue(ShadowOpacityProperty, value);
        }

        // Gets or sets the shadow offset width of the Frame.
        public float ShadowOffsetWidth
        {
            get => (float) GetValue(ShadowOffsetWidthProperty);
            set => SetValue(ShadowOffsetWidthProperty, value);
        }

        // Gets or sets the shadow offset height of the Frame.
        public float ShadowOffSetHeight
        {
            get => (float) GetValue(ShadowOffSetHeightProperty);
            set => SetValue(ShadowOffSetHeightProperty, value);
        }
    }
}