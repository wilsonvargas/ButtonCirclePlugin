using ButtonCircle.FormsPlugin.Abstractions.Enums;
using Xamarin.Forms;

namespace ButtonCircle.FormsPlugin.Abstractions
{
    /// <summary>
    /// ButtonCircle Interface
    /// </summary>
    public class CircleButton : Button
    {
        /// <summary>
        /// Border Color of circle image
        /// </summary>
        new public Color BorderColor
        {
            get { return (Color)GetValue(BorderColorProperty); }
            set { SetValue(BorderColorProperty, value); }
        }

        /// <summary>
        /// Border thickness of circle image
        /// </summary>
        public int BorderThickness
        {
            get { return (int)GetValue(BorderThicknessProperty); }
            set { SetValue(BorderThicknessProperty, value); }
        }

        /// <summary>
        /// Gets or sets the font.
        /// </summary>
        /// <value>
        /// The icon.
        /// </value>
        public Fonts FontIcon
        {
            get { return (Fonts)GetValue(FontIconProperty); }
            set { SetValue(FontIconProperty, value); }
        }

        /// <summary>
        /// Gets or sets the icon.
        /// </summary>
        /// <value>
        /// The icon.
        /// </value>
        public string Icon
        {
            get { return (string)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        /// <summary>
        /// Color property of border
        /// </summary>
        new public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(CircleButton), Color.White);

        /// <summary>
        /// Thickness property of border
        /// </summary>
        public static readonly BindableProperty BorderThicknessProperty = BindableProperty.Create(nameof(BorderThickness), typeof(int), typeof(CircleButton), 0);

        /// <summary>
        /// Property definition for the <see cref="Fonts"/> Property
        /// </summary>
        public static readonly BindableProperty FontIconProperty = BindableProperty.Create(nameof(FontIcon), typeof(Fonts), typeof(CircleButton), Fonts.Material);

        /// <summary>
        /// Property definition for the <see cref="Icon"/> Property
        /// </summary>
        public static readonly BindableProperty IconProperty = BindableProperty.Create(nameof(Icon), typeof(string), typeof(CircleButton), string.Empty);
    }
}
