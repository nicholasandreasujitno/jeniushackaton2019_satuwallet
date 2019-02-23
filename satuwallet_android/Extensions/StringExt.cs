using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace satuwallet_android.Extensions
{
    public static class StringExt
    {
        public static string Left(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            maxLength = Math.Abs(maxLength);

            return (value.Length <= maxLength
                   ? value
                   : value.Substring(0, maxLength)
                   );
        }

        public static string TrimMore(this string input)
        {
            if (input == null)
                return null;
            return input.Trim();
        }

        public static string ToTitleCase(this string title, bool isForcedToLower = false)
        {
            if (title == null)
            {
                return null;
            }
            if (isForcedToLower)
            {
                title = title.ToLower();
            }
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(title);
        }

        public static string ToCapitalCase(this string input, bool isForcedToLower = false)
        {
            if (string.IsNullOrEmpty(input))
                return input;
            if (isForcedToLower)
            {
                input = input.ToLower();
            }
            var sb = new StringBuilder();
            foreach (var singleText in input.Split(' '))
            {
                sb.Append(FirstCharToUpper(singleText) + " ");
            }
            var str = sb.ToString();
            return str.Trim();
        }

        public static string FirstCharToUpper(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;
            return input.First().ToString().ToUpper() + input.Substring(1);
        }
        public static string LimitLength(this string input, int maxLength, string addSuffix = "")
        {
            if (input.Length > (maxLength - addSuffix.Length))
                input = input.Substring(0, maxLength - addSuffix.Length) + addSuffix;

            return input;
        }

        public static IEnumerable<string> SplitInParts(this string s, int partLength)
        {
            if (s == null)
                throw new ArgumentNullException("String is null");
            if (partLength <= 0)
                throw new ArgumentException("Part length has to be positive.", "partLength");

            for (var i = 0; i < s.Length; i += partLength)
                yield return s.Substring(i, Math.Min(partLength, s.Length - i));
        }

        //public static string ReplaceFlags<T>(this string s, T obj)
        //{
        //    if (string.IsNullOrEmpty(s))
        //        return s;
        //    return ((new StringBuilder(s)).ReplaceFlags(obj)).ToString();
        //}

        public static string ReplaceStart(this string str, int length, string prefix)
        {
            StringBuilder s = new StringBuilder(str);
            if (s.Length < length)
            {
                length = s.Length;
            }
            s = s.Remove(0, length);
            for (int i = 0; i < length; i++)
            {
                s.Insert(0, prefix);
            }
            return s.ToString();
        }

        public static string ReplaceEnd(this string str, int length, string suffix)
        {
            StringBuilder s = new StringBuilder(str);
            if (s.Length < length)
            {
                length = s.Length;
            }
            s = s.Remove((s.Length) - length, length);
            for (int i = 0; i < length; i++)
            {
                s.Append(suffix);
            }
            return s.ToString();
        }
        public static string FixPathSeperator(this string str)
        {
            return str.Replace('\\', System.IO.Path.AltDirectorySeparatorChar);
        }
    }
}