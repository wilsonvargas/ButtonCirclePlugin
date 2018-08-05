using ButtonCircle.FormsPlugin.Abstractions;
using ButtonCircle.FormsPlugin.UWP;
using System;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

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

        public CircleButton CircleButtonElement => Element as CircleButton;

        private Color OriginalBackgroundColor { get; set; }

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                if (Control == null || Element == null)
                    throw new InvalidOperationException(String.Format("Cannot convert {0} into {1}", Element?.Text ?? "unknown element", typeof(Icon))); ;

                OriginalBackgroundColor = CircleButtonElement.BackgroundColor == default(Color)
                    ? Color.Transparent
                    : CircleButtonElement.BackgroundColor;

                CreateCircle();
                if (!String.IsNullOrEmpty(CircleButtonElement.Icon))
                {
                    Control.FontFamily = new FontFamily(Abstractions.Helpers.Extensions.FindPathForFont(
                        CircleButtonElement.FontIcon));

                    IIcon icon = Abstractions.Helpers.Extensions.FindIconForKey(CircleButtonElement.Icon,
                        CircleButtonElement.FontIcon);

                    Control.Content = $"{icon.Character}";
                }
                else if (!String.IsNullOrEmpty(CircleButtonElement.Text))
                {
                    Control.Content = CircleButtonElement.Text;
                }
            }
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (Control == null || Element == null)
                return;

            if (e.PropertyName != VisualElement.HeightProperty.PropertyName &&
                e.PropertyName != VisualElement.WidthProperty.PropertyName &&
                e.PropertyName != CircleButton.BorderColorProperty.PropertyName &&
                e.PropertyName != CircleButton.BorderThicknessProperty.PropertyName &&
                e.PropertyName != CircleButton.IconProperty.PropertyName &&
                e.PropertyName != CircleButton.TextProperty.PropertyName &&
                e.PropertyName != CircleButton.FontIconProperty.PropertyName) return;

            CreateCircle();

            if (!String.IsNullOrEmpty(CircleButtonElement.Icon))
            {
                Control.FontFamily = new FontFamily(Abstractions.Helpers.Extensions.FindPathForFont(
                    CircleButtonElement.FontIcon));

                IIcon icon = Abstractions.Helpers.Extensions.FindIconForKey(CircleButtonElement.Icon,
                    CircleButtonElement.FontIcon);

                Control.Content = $"{icon.Character}";
            }
            else if (!String.IsNullOrEmpty(CircleButtonElement.Text))
            {
                Control.Content = CircleButtonElement.Text;
            }
        }

        private void CreateCircle()
        {
            var min = Math.Min(CircleButtonElement.WidthRequest, CircleButtonElement.HeightRequest);
            Control.BorderRadius = (int)(min / 2.0);

            Control.BorderThickness = new Windows.UI.Xaml.Thickness(Convert.ToDouble(CircleButtonElement.BorderThickness));
            Control.BorderBrush =  new SolidColorBrush(XamarinColorToUwp(CircleButtonElement.BorderColor));

            Resources = (Windows.UI.Xaml.ResourceDictionary)XamlReader.Load(PillButtonStyleDictionary);

            Resources["PillHeight"] = min;
            Resources["PillWidth"] = min;

            Resources["PillCornerRadius"] = Control.BorderRadius; //CircleButtonElement.CornerRadius;
            Resources["PillBorderWidth"] = Control.BorderThickness; //CircleButtonElement.BorderWidth;
            
            // set normal style
            Resources["PillBackgroundColor"] = new SolidColorBrush(XamarinColorToUwp(OriginalBackgroundColor));
            Resources["PillTextColor"] = new SolidColorBrush(XamarinColorToUwp(CircleButtonElement.TextColor));

            // todo create bindable UWP-specific CircleButton props for user to override the style assumptions below

            // hover color will be lighter version of background color, unless background color is transparent in which case it will be the border color
            var hoverColor = OriginalBackgroundColor == Color.Transparent
                    ? CircleButtonElement.BorderColor
                    : ChangeColorBrightness(OriginalBackgroundColor, 0.12);
            Resources["PillFillColorOnHover"] = new SolidColorBrush(XamarinColorToUwp(hoverColor));

            // make pressed color a darker shade of the fill color on hover
            Resources["PillFillColorOnPressed"] = new SolidColorBrush(XamarinColorToUwp(ChangeColorBrightness(hoverColor, -0.06)));

            // make icon color black or white on hover/pressed depending on how dark the hover color is
            Resources["PillTextColorOnHoverOrPressed"] = new SolidColorBrush(XamarinColorToUwp(BlackOrWhiteForegroundTextColor(hoverColor)));

            CircleButtonElement.BackgroundColor = Color.Transparent; // hack

            Control.Style = Resources["PillButtonStyle"] as Windows.UI.Xaml.Style;
        }

        /// <summary>
        /// Returns black or white Xamarin Forms Color depending on the Color provided
        /// </summary>
        /// <param name="bg">The background XF Color to base the selection of white or black foreground color on</param>
        /// <returns>Black or White XF Color</returns>
        public static Color BlackOrWhiteForegroundTextColor(Color bg)
        {
            var nThreshold = 105;
            var bgDelta = Convert.ToInt32((bg.R * 0.299) + (bg.G * 0.587) + 
                                          (bg.B * 0.114));

            return (255*(1 - bgDelta) < nThreshold) ? Color.Black : Color.White;
        }

        /// <summary>
        /// Creates color with corrected brightness.
        /// </summary>
        /// <param name="color">Color to correct.</param>
        /// <param name="correctionFactor">The brightness correction factor. Must be between -1 and 1. 
        /// Negative values produce darker colors.</param>
        /// <returns>
        /// Corrected <see cref="Color"/> structure.
        /// </returns>
        public static Color ChangeColorBrightness(Color color, double correctionFactor)
        {
            var red = (color.R * 255);
            var green = (color.G * 255);
            var blue = (color.B * 255);

            if (correctionFactor < 0)
            {
                correctionFactor = 1 + correctionFactor;
                red *= correctionFactor;
                green *= correctionFactor;
                blue *= correctionFactor;
            }
            else
            {
                red = (255 - red) * correctionFactor + red;
                green = (255 - green) * correctionFactor + green;
                blue = (255 - blue) * correctionFactor + blue;
            }

            return Color.FromRgba(red/255, green/255, blue/255, color.A);
        }

        /// <summary>
        /// Converts a Xamarin Forms Color to a Windows UI Color
        /// </summary>
        /// <param name="color">The Xamarin Forms Color to convert</param>
        /// <returns>Windows.UI.Color</returns>
        private Windows.UI.Color XamarinColorToUwp(Xamarin.Forms.Color color)
        {
            return Windows.UI.Color.FromArgb((byte)(color.A * 255),
                (byte)(color.R * 255),
                (byte)(color.G * 255),
                (byte)(color.B * 255));
        }

        /// <summary>
        /// Resource dictionary Xaml as string to use for the button
        /// </summary>
        private const string PillButtonStyleDictionary = @"<ResourceDictionary
            xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
            xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml"">

            <x:Double x:Key=""PillHeight"">0</x:Double>
            <x:Double x:Key=""PillWidth"">0</x:Double>
            <x:Double x:Key=""PillCornerRadius"">0</x:Double>
            <x:Double x:Key=""PillBorderWidth"">0</x:Double>

            <SolidColorBrush
                x:Key=""PillBackgroundColor""
                Color=""Black"" />
            <SolidColorBrush
                x:Key=""PillTextColor""
                Color=""Black"" />
            <SolidColorBrush
                x:Key=""PillFillColorOnHover""
                Color=""Black"" />
            <SolidColorBrush
                x:Key=""PillFillColorOnPressed""
                Color=""Black"" />
            <SolidColorBrush
                x:Key=""PillTextColorOnHoverOrPressed""
                Color=""Black"" />

            <Style
                x:Key=""PillButtonStyle""
                TargetType=""Button"">
                <Setter
                    Property=""Background""
                    Value=""{ThemeResource SystemControlBackgroundBaseLowBrush}"" />
                <Setter
                    Property=""Foreground""
                    Value=""{ThemeResource SystemControlForegroundBaseHighBrush}"" />
                <Setter
                    Property=""BorderBrush""
                    Value=""{ThemeResource SystemControlForegroundTransparentBrush}"" />
                <Setter
                    Property=""BorderThickness""
                    Value=""{ThemeResource ButtonBorderThemeThickness}"" />
                <Setter
                    Property=""Padding""
                    Value=""8,4,8,4"" />
                <Setter
                    Property=""HorizontalAlignment""
                    Value=""Left"" />
                <Setter
                    Property=""VerticalAlignment""
                    Value=""Center"" />
                <Setter
                    Property=""FontFamily""
                    Value=""{ThemeResource ContentControlThemeFontFamily}"" />
                <Setter
                    Property=""FontWeight""
                    Value=""Normal"" />
                <Setter
                    Property=""FontSize""
                    Value=""{ThemeResource ControlContentThemeFontSize}"" />
                <Setter
                    Property=""UseSystemFocusVisuals""
                    Value=""True"" />
                <Setter Property=""Template"">
                    <Setter.Value>
                        <ControlTemplate TargetType=""Button"">
                            <Grid x:Name=""RootGrid"">
                                <VisualStateManager.VisualStateGroups>
                                    <VisualStateGroup x:Name=""CommonStates"">
                                        <VisualState x:Name=""Normal"">
                                            <Storyboard>
                                                <ObjectAnimationUsingKeyFrames
                                                    Storyboard.TargetName=""Pill""
                                                    Storyboard.TargetProperty=""Fill"">
                                                    <DiscreteObjectKeyFrame
                                                        KeyTime=""0""
                                                        Value=""{StaticResource PillBackgroundColor}"" />
                                                </ObjectAnimationUsingKeyFrames>
                                                <ObjectAnimationUsingKeyFrames
                                                    Storyboard.TargetName=""ContentPresenter""
                                                    Storyboard.TargetProperty=""Foreground"">
                                                    <DiscreteObjectKeyFrame
                                                        KeyTime=""0""
                                                        Value=""{StaticResource PillTextColor}"" />
                                                </ObjectAnimationUsingKeyFrames>
                                                <PointerUpThemeAnimation Storyboard.TargetName=""RootGrid"" />
                                            </Storyboard>
                                        </VisualState>
                                        <VisualState x:Name=""PointerOver"">
                                            <Storyboard>
                                                <ObjectAnimationUsingKeyFrames
                                                    Storyboard.TargetName=""Pill""
                                                    Storyboard.TargetProperty=""Fill"">
                                                    <DiscreteObjectKeyFrame
                                                        KeyTime=""0""
                                                        Value=""{StaticResource PillFillColorOnHover}"" />
                                                </ObjectAnimationUsingKeyFrames>
                                                <ObjectAnimationUsingKeyFrames
                                                    Storyboard.TargetName=""ContentPresenter""
                                                    Storyboard.TargetProperty=""Foreground"">
                                                    <DiscreteObjectKeyFrame
                                                        KeyTime=""0""
                                                        Value=""{StaticResource PillTextColorOnHoverOrPressed}"" />
                                                </ObjectAnimationUsingKeyFrames>

                                                <PointerUpThemeAnimation Storyboard.TargetName=""RootGrid"" />
                                            </Storyboard>
                                        </VisualState>
                                        <VisualState x:Name=""Pressed"">
                                            <Storyboard>
                                                <ObjectAnimationUsingKeyFrames
                                                    Storyboard.TargetName=""Pill""
                                                    Storyboard.TargetProperty=""Fill"">
                                                    <DiscreteObjectKeyFrame
                                                        KeyTime=""0""
                                                        Value=""{StaticResource PillFillColorOnPressed}"" />
                                                </ObjectAnimationUsingKeyFrames>
                                                <ObjectAnimationUsingKeyFrames
                                                    Storyboard.TargetName=""ContentPresenter""
                                                    Storyboard.TargetProperty=""Foreground"">
                                                    <DiscreteObjectKeyFrame
                                                        KeyTime=""0""
                                                        Value=""{StaticResource PillTextColorOnHoverOrPressed}"" />
                                                </ObjectAnimationUsingKeyFrames>
                                                <PointerDownThemeAnimation Storyboard.TargetName=""RootGrid"" />
                                            </Storyboard>
                                        </VisualState>
                                        <VisualState x:Name=""Disabled"">
                                            <Storyboard>
                                                <ObjectAnimationUsingKeyFrames
                                                    Storyboard.TargetName=""Pill""
                                                    Storyboard.TargetProperty=""Fill"">
                                                    <DiscreteObjectKeyFrame
                                                        KeyTime=""0""
                                                        Value=""{ThemeResource SystemControlBackgroundBaseLowBrush}"" />
                                                </ObjectAnimationUsingKeyFrames>
                                                <ObjectAnimationUsingKeyFrames
                                                    Storyboard.TargetName=""ContentPresenter""
                                                    Storyboard.TargetProperty=""Foreground"">
                                                    <DiscreteObjectKeyFrame
                                                        KeyTime=""0""
                                                        Value=""{ThemeResource SystemControlDisabledBaseMediumLowBrush}"" />
                                                </ObjectAnimationUsingKeyFrames>
                                                <ObjectAnimationUsingKeyFrames
                                                    Storyboard.TargetName=""Pill""
                                                    Storyboard.TargetProperty=""Stroke"">
                                                    <DiscreteObjectKeyFrame
                                                        KeyTime=""0""
                                                        Value=""{ThemeResource SystemControlDisabledTransparentBrush}"" />
                                                </ObjectAnimationUsingKeyFrames>
                                            </Storyboard>
                                        </VisualState>
                                    </VisualStateGroup>
                                </VisualStateManager.VisualStateGroups>
                                <Rectangle
                                    x:Name=""Pill""
                                    Height=""{StaticResource PillHeight}""
                                    Width=""{StaticResource PillWidth}""
                                    RadiusX=""{StaticResource PillCornerRadius}""
                                    RadiusY=""{StaticResource PillCornerRadius}""
                                    Stroke=""{TemplateBinding BorderBrush}""
                                    StrokeThickness=""{StaticResource PillBorderWidth}"" />
                                <ContentPresenter
                                    x:Name=""ContentPresenter""
                                    Padding=""{TemplateBinding Padding}""
                                    HorizontalContentAlignment=""{TemplateBinding HorizontalContentAlignment}""
                                    VerticalAlignment=""Center""
                                    VerticalContentAlignment=""{TemplateBinding VerticalContentAlignment}""
                                    AutomationProperties.AccessibilityView=""Raw""
                                    Content=""{TemplateBinding Content}""
                                    ContentTemplate=""{TemplateBinding ContentTemplate}""
                                    ContentTransitions=""{TemplateBinding ContentTransitions}"" />
                            </Grid>
                        </ControlTemplate>
                    </Setter.Value>
                </Setter>
            </Style>
        </ResourceDictionary>";
    }
}
