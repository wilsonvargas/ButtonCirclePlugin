using ButtonCircle.FormsPlugin.Abstractions;
using ButtonCircle.FormsPlugin.iOS;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using System;
using System.ComponentModel;
using System.Diagnostics;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ButtonCircle.FormsPlugin.Abstractions.CircleButton), typeof(ButtonCircleRenderer))]

namespace ButtonCircle.FormsPlugin.iOS
{
    /// <summary>
    /// ButtonCircle Renderer
    /// </summary>
    [Preserve(AllMembers = true)]
    public class ButtonCircleRenderer : ButtonRenderer
    {
        /// <summary>
        /// Used for registration with dependency service
        /// </summary>
        public async static void Init()
        {
            var temp = DateTime.Now;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            try
            {
                base.Dispose(disposing);
            }
            catch (NullReferenceException)
            {
                // Random bug in Dispose method
                // https://bugzilla.xamarin.com/show_bug.cgi?id=21457
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);
            if (Control == null || Element == null)
                throw new InvalidOperationException(String.Format("Cannot convert {0} into {1}", Element.Text, typeof(Icon))); ;

            CreateCircle();
            if (!String.IsNullOrEmpty(((CircleButton)Element).Icon))
            {
                Control.Font = UIFont.FromName(Abstractions.Helpers.Extensions.FindNameForFont
                        (((CircleButton)Element).FontIcon), (nfloat)Element.FontSize);

                IIcon icon = Abstractions.Helpers.Extensions.FindIconForKey(((CircleButton)Element).Icon,
                       ((CircleButton)Element).FontIcon);

                Control.SetTitle($"{icon.Character}", UIControlState.Normal);
                //Control.Font = Font.OfSize("MaterialIcons-Regular", Element.FontSize).WithAttributes(Element.FontAttributes).ToUIFont();
                //Element.Text = ((CircleButton)Element).Icon;
            }
            else
            {
                Control.Font = Font.OfSize(Element.FontFamily, Element.FontSize).WithAttributes(Element.FontAttributes).ToUIFont();
                Control.TitleLabel.LineBreakMode = UILineBreakMode.WordWrap;
                Control.TitleLabel.TextAlignment = UITextAlignment.Center;
                //Element.Text = ((CircleButton)Element).Text;
                Control.SetTitle(((CircleButton)Element).Text, UIControlState.Normal);
            }

            if (((CircleButton)Element).Image != null)
            {
                UIButton thisButton = Control as UIButton;
                thisButton.TouchDown += delegate
                {
                    System.Diagnostics.Debug.WriteLine("TouchDownEvent");
                };
                thisButton.TouchUpInside += delegate
                {
                    System.Diagnostics.Debug.WriteLine("TouchUpEvent");
                };
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (Control == null || Element == null)
                return;
            if (e.PropertyName == VisualElement.HeightProperty.PropertyName ||
                e.PropertyName == VisualElement.WidthProperty.PropertyName ||
              e.PropertyName == CircleButton.BorderColorProperty.PropertyName ||
              e.PropertyName == CircleButton.BorderThicknessProperty.PropertyName ||
              e.PropertyName == CircleButton.IconProperty.PropertyName ||
              e.PropertyName == CircleButton.TextProperty.PropertyName ||
              e.PropertyName == CircleButton.FontIconProperty.PropertyName)
            {
                CreateCircle();

                if (!String.IsNullOrEmpty(((CircleButton)Element).Icon))
                {
                    Control.Font = UIFont.FromName(Abstractions.Helpers.Extensions.FindNameForFont
                        (((CircleButton)Element).FontIcon), (nfloat)Element.FontSize);

                    IIcon icon = Abstractions.Helpers.Extensions.FindIconForKey(((CircleButton)Element).Icon,
                       ((CircleButton)Element).FontIcon);

                    Control.SetTitle($"{icon.Character}", UIControlState.Normal);

                    //Control.Font = Font.OfSize("MaterialIcons-Regular", Element.FontSize).WithAttributes(Element.FontAttributes).ToUIFont();
                    //Element.Text = ((CircleButton)Element).Icon;
                }
                else
                {
                    Control.Font = Font.OfSize(Element.FontFamily, Element.FontSize).WithAttributes(Element.FontAttributes).ToUIFont();
                    Control.TitleLabel.LineBreakMode = UILineBreakMode.WordWrap;
                    Control.TitleLabel.TextAlignment = UITextAlignment.Center;
                    //Element.Text = ((CircleButton)Element).Text;
                    Control.SetTitle(((CircleButton)Element).Text, UIControlState.Normal);
                }
            }
        }

        private void CreateCircle()
        {
            try
            {
                var min = Math.Min(Element.Width, Element.Height);
                Control.Layer.CornerRadius = (nfloat)(min / 2.0);
                Control.Layer.MasksToBounds = false;
                Control.ClipsToBounds = true;

                var borderThickness = ((CircleButton)Element).BorderThickness;
                var externalBorder = new CALayer();
                externalBorder.CornerRadius = Control.Layer.CornerRadius;
                externalBorder.Frame = new CGRect(-.5, -.5, min + 1, min + 1);
                externalBorder.BorderColor = ((CircleButton)Element).BorderColor.ToCGColor();
                externalBorder.BorderWidth = ((CircleButton)Element).BorderThickness;
                Control.Layer.AddSublayer(externalBorder);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to create circle button: " + ex);
            }
        }
    }
}
