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
                Control.Typeface = Typeface.CreateFromAsset(Forms.Context.Assets, "MaterialIcons-Regular.ttf");
                Element.Text = ((CircleButton)Element).Icon;
            }
            else
            {
                Control.Typeface = Typeface.Create(Element.FontFamily, TypefaceStyle.Normal);
                Element.Text = ((CircleButton)Element).Text;                
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == CircleButton.BorderColorProperty.PropertyName ||
              e.PropertyName == CircleButton.BorderThicknessProperty.PropertyName ||
              e.PropertyName == CircleButton.IconProperty.PropertyName ||
              e.PropertyName == CircleButton.TextProperty.PropertyName)
            {
                if (!String.IsNullOrEmpty(((CircleButton)Element).Icon))
                {
                    Control.Typeface = Typeface.CreateFromAsset(Forms.Context.Assets, "MaterialIcons-Regular.ttf");
                    Element.Text = ((CircleButton)Element).Icon;
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