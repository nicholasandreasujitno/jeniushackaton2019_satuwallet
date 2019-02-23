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

namespace satuwallet_android.Helpers
{
    public class RandomGenerator
    {
        Random random = new Random();

        public RandomGenerator()
        {
            random = new Random();
        }
        public RandomGenerator(int number)
        {
            random = new Random(number);
        }
        public RandomGenerator(DateTime dt)
        {
            random = new Random((int)dt.Ticks);
        }

        public Random GetRandom()
        {
            return random;
        }

        /// <summary>
        /// Random alphabet and numeric generator
        /// </summary>
        /// <param name="Size">Length of output string</param>
        /// <returns></returns>
        public string RandomString(int size)
        {
            string input = "abcdefghijklmnopqrstuvwxyz0123456789";
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = input[random.Next(0, input.Length)];
                builder.Append(ch);
            }
            return builder.ToString();
        }

        public string RandomNumber(int size)
        {
            string input = "0123456789";
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = input[random.Next(0, input.Length)];
                builder.Append(ch);
            }
            return builder.ToString();
        }

        public int RandomInt(int size = 6)
        {
            if (size < 1)
                size = 1;

            var max = Math.Pow(10, size);
            if (max >= int.MaxValue)
                max = int.MaxValue;
            return random.Next(1, (int)max); // a number >= 1 and < 10 * N
        }

        public long RandomLong(int size = 12)
        {
            if (size < 1)
                size = 1;
            var maxSize = (long.MaxValue + "").Count();
            if (size > maxSize)
                size = maxSize;

            long result = 0;
            if (!long.TryParse(RandomNumber(size), out result))
            {
                result = long.MaxValue;
            }
            return result;
        }
    }
}