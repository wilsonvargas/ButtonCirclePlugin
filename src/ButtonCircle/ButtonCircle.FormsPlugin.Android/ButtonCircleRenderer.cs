using ButtonCircle.FormsPlugin.Abstractions;
using System;
using Xamarin.Forms;
using ButtonCircle.FormsPlugin.Droid;
using Xamarin.Forms.Platform.Android;
using Android.Graphics.Drawables;
using Android.Util;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(CircleButton), typeof(ButtonCircleRenderer))]
namespace ButtonCircle.FormsPlugin.Droid
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
        /// <param name="e"></param>
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                //Only enable hardware accelleration on lollipop
                if ((int)Android.OS.Build.VERSION.SdkInt < 21)
                {
                    SetLayerType(LayerType.Software, null);
                }
            }

            if (!String.IsNullOrEmpty(((CircleButton)Element).Icon))
            {
                string fontName = Abstractions.Helpers.Extensions.FindNameFileForFont(((CircleButton)Element).FontIcon);
                Control.Typeface = Typeface.CreateFromAsset(Forms.Context.Assets, fontName);

                string iconString = ((CircleButton)Element).Icon;
                IIcon icon = Abstractions.Helpers.Extensions.FindIconForKey(iconString,
                    ((CircleButton)Element).FontIcon);

                Element.Text = $"{icon.Character}";
            }
            else
            {
                Control.Typeface = Typeface.Create(Element.FontFamily, TypefaceStyle.Normal);
                Element.Text = ((CircleButton)Element).Text;                
            }

            if (((CircleButton)Element).Image != null)
            {
                Android.Widget.Button thisButton = Control as Android.Widget.Button;
                thisButton.Touch += (object sender, Android.Views.View.TouchEventArgs e2) => {
                    if (e2.Event.Action == MotionEventActions.Down)
                    {
                        Control.SetBackgroundColor(Element.BackgroundColor.ToAndroid());
                        System.Diagnostics.Debug.WriteLine("TouchDownEvent");
                    }
                    else if (e2.Event.Action == MotionEventActions.Up)
                    {
                        Control.SetBackgroundColor(Element.BackgroundColor.ToAndroid());
                        Control.SetShadowLayer(0, 0, 0, Android.Graphics.Color.Transparent);
                        System.Diagnostics.Debug.WriteLine("TouchUpEvent");
                        Control.CallOnClick();
                    }
                };
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == CircleButton.BorderColorProperty.PropertyName ||
              e.PropertyName == CircleButton.BorderThicknessProperty.PropertyName ||
              e.PropertyName == CircleButton.IconProperty.PropertyName ||
              e.PropertyName == CircleButton.TextProperty.PropertyName || 
              e.PropertyName == CircleButton.FontIconProperty.PropertyName )
            {
                if (!String.IsNullOrEmpty(((CircleButton)Element).Icon))
                {
                    string fontName = Abstractions.Helpers.Extensions.FindNameFileForFont(((CircleButton)Element).FontIcon);
                    Control.Typeface = Typeface.CreateFromAsset(Forms.Context.Assets, fontName);

                    IIcon icon = Abstractions.Helpers.Extensions.FindIconForKey(Element.Text, 
                        ((CircleButton)Element).FontIcon);

                    Element.Text = $"{icon.Character}";

                }
                else
                {
                    Control.Typeface = Typeface.Create(Element.FontFamily, TypefaceStyle.Normal);
                    Element.Text = ((CircleButton)Element).Text;
                }

                this.Invalidate();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="child"></param>
        /// <param name="drawingTime"></param>
        /// <returns></returns>
        protected override bool DrawChild(Canvas canvas, Android.Views.View child, long drawingTime)
        {
            try
            {

                var radius = Math.Min(Width, Height) / 2;

                var borderThickness = (float)((CircleButton)Element).BorderThickness;

                int strokeWidth = 0;

                if (borderThickness > 0)
                {
                    var logicalDensity = Xamarin.Forms.Forms.Context.Resources.DisplayMetrics.Density;
                    strokeWidth = (int)Math.Ceiling(borderThickness * logicalDensity + .5f);
                }

                radius -= strokeWidth / 2;

                var path = new Path();
                path.AddCircle(Width / 2.0f, Height / 2.0f, radius, Path.Direction.Ccw);

                canvas.Save();
                canvas.ClipPath(path);



                var paint = new Paint();
                paint.AntiAlias = true;
                paint.SetStyle(Paint.Style.Fill);
                canvas.DrawPath(path, paint);
                paint.Dispose();


                var result = base.DrawChild(canvas, child, drawingTime);

                path.Dispose();
                canvas.Restore();

                path = new Path();
                path.AddCircle(Width / 2, Height / 2, radius, Path.Direction.Ccw);


                if (strokeWidth > 0.0f)
                {
                    paint = new Paint();
                    paint.AntiAlias = true;
                    paint.StrokeWidth = strokeWidth;
                    paint.SetStyle(Paint.Style.Stroke);
                    paint.Color = ((CircleButton)Element).BorderColor.ToAndroid();
                    canvas.DrawPath(path, paint);
                    paint.Dispose();
                }

                path.Dispose();
                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Unable to create circle button: " + ex);
            }

            return base.DrawChild(canvas, child, drawingTime);
        }
    }
}