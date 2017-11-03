using ButtonCircle.FormsPlugin.Abstractions;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Xaml.Media;
using ButtonCircle.FormsPlugin.UWP;
using Windows.UI.Xaml.Markup;
using System.Diagnostics;

[assembly: ExportRenderer(typeof(CircleButton), typeof(ButtonCircleRenderer))]
namespace ButtonCircle.FormsPlugin.UWP
{
    /// <summary>
    /// ButtonCircle Renderer
    /// </summary>
    public class ButtonCircleRenderer : ButtonRenderer
    {
        /// <summary>
        /// Used for registration with dependency service
        /// </summary>
        public static void Init()
        {
            var temp = DateTime.Now;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);
            if (Control == null || Element == null)
                throw new InvalidOperationException(String.Format("Cannot convert {0} into {1}", Element.Text, typeof(Icons))); ;

            CreateCircle();
            if (!String.IsNullOrEmpty(((CircleButton)Element).Icon))
            {
                Control.FontFamily = new FontFamily("/Assets/Fonts/MaterialIcons-Regular.ttf#Material Icons");
                Control.Content = ((CircleButton)Element).Icon.Icon;
            }
            else if (!String.IsNullOrEmpty(((CircleButton)Element).Text))
            {
                Control.Content = ((CircleButton)Element).Text;
            }
        }
        /// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (Control == null || Element == null)
                return;
            if (e.PropertyName == VisualElement.HeightProperty.PropertyName ||
                e.PropertyName == VisualElement.WidthProperty.PropertyName ||
              e.PropertyName == CircleButton.BorderColorProperty.PropertyName ||
              e.PropertyName == CircleButton.BorderThicknessProperty.PropertyName ||
              e.PropertyName == CircleButton.IconProperty.PropertyName ||
              e.PropertyName == CircleButton.TextProperty.PropertyName)
            {
                CreateCircle();
                if (!String.IsNullOrEmpty(((CircleButton)Element).Icon))
                {
                    Control.FontFamily = new FontFamily("/Assets/Fonts/MaterialIcons-Regular.ttf#Material Icons");
                    Control.Content = ((CircleButton)Element).Icon.Icon;
                }
                else if (!String.IsNullOrEmpty(((CircleButton)Element).Text))
                {
                    Control.Content = ((CircleButton)Element).Text;
                }
            }
        }

        private void CreateCircle()
        {
                var min = Math.Min(Element.Width, Element.Height);
                Control.BorderRadius = (int)(min / 2.0);
                Control.BorderThickness = new Windows.UI.Xaml.Thickness(Convert.ToDouble(((CircleButton)Element).BorderThickness));
                var XFColor = ((CircleButton)Element).BorderColor;
                Windows.UI.Color uwpColor = Windows.UI.Color.FromArgb(
                (byte)(XFColor.A * 255),
                (byte)(XFColor.R * 255),
                (byte)(XFColor.G * 255),
                (byte)(XFColor.B * 255));
                SolidColorBrush borderColor = new SolidColorBrush(uwpColor);
                Control.BorderBrush = borderColor;
        }
    }
}
