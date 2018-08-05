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
        public Color BorderColor
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
        public static readonly BindableProperty BorderColorProperty =
            BindableProperty.Create(propertyName: nameof(BorderColor),
              returnType: typeof(Color),
              declaringType: typeof(CircleButton),
              defaultValue: Color.White);

        /// <summary>
        /// Thickness property of border
        /// </summary>
        public static readonly BindableProperty BorderThicknessProperty =
          BindableProperty.Create(propertyName: nameof(BorderThickness),
              returnType: typeof(int),
              declaringType: typeof(CircleButton),
              defaultValue: 0);

        /// <summary>
        /// Property definition for the <see cref="Fonts"/> Property
        /// </summary>
        public static readonly BindableProperty FontIconProperty =
        BindableProperty.Create(propertyName: nameof(FontIcon),
              returnType: typeof(Fonts),
              declaringType: typeof(CircleButton),
              defaultValue: Fonts.Material);

        /// <summary>
        /// Property definition for the <see cref="Icon"/> Property
        /// </summary>
        public static readonly BindableProperty IconProperty =
        BindableProperty.Create(propertyName: nameof(Icon),
              returnType: typeof(string),
              declaringType: typeof(CircleButton),
              defaultValue: string.Empty);
    }
}
