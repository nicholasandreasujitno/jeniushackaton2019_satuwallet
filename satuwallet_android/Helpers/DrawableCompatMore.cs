using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Content;
using Android.Support.V4.Graphics.Drawable;
using Android.Views;
using Android.Widget;

namespace satuwallet_android.Helpers
{
    public static class DrawableCompatMore
    {
        public static Drawable GetTintedDrawableOfColorResId(Context context, Bitmap inputBitmap, int colorResId)
        {
            return GetTintedDrawable(context, new BitmapDrawable(context.Resources, inputBitmap), ContextCompat.GetColor(context, colorResId));
        }

        public static Drawable GetTintedDrawableOfColorResId(Context context, Drawable inputDrawable, int colorResId)
        {
            return GetTintedDrawable(context, inputDrawable, ContextCompat.GetColor(context, colorResId));
        }

        public static Drawable GetTintedDrawable(Context context, Bitmap inputBitmap, int color)
        {
            return GetTintedDrawable(context, new BitmapDrawable(context.Resources, inputBitmap), color);
        }

        public static Drawable GetTintedDrawable(Context context, Drawable inputDrawable, int color)
        {
            Drawable wrapDrawable = DrawableCompat.Wrap(inputDrawable);
            DrawableCompat.SetTint(wrapDrawable, color);
            DrawableCompat.SetTintMode(wrapDrawable, PorterDuff.Mode.SrcIn);
            return wrapDrawable;
        }

    }
}