using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace satuwallet_android.Constants
{
    public enum Platform
    {
        OVO = 0,
        GoPay = 3,
        DANA = 6,
        iSaku = 9
    }

    public static class PlatformExt
    {
        static Dictionary<Platform, int> dictPlatformLogo = new Dictionary<Platform, int>()
        {
            { Platform.OVO, Resource.Drawable.platform_ovo },
            { Platform.GoPay, Resource.Drawable.platform_gopay },
            { Platform.DANA, Resource.Drawable.platform_dana }
        };

        public static int GetLogoResId(this Platform p)
        {
            if (dictPlatformLogo.ContainsKey(p))
            {
                return dictPlatformLogo[p];
            }
            return Resource.Drawable.platform_default;
        }

        public static Dictionary<Platform, string> GetKeyValue()
        {
            var listP = new Dictionary<Platform, string>();
            foreach (Platform p in Enum.GetValues(typeof(Platform)))
            {
                listP.Add(p, p.ToString());
            }
            return listP;
        }
    }

    //public class PlatformKeyValue
    //{
    //    public Platform PlatformEnum { get; set; }
    //    public string Name { get; set; }
    //}
}
