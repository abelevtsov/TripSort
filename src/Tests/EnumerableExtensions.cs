using System.Collections.Generic;
using System.Security.Cryptography;

using TripSort;

namespace Tests
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> Shuffle<T>(this IList<T> source) where T : Trip
        {
            var provider = new RNGCryptoServiceProvider();
            var n = source.Count;
            var result = new List<T>(source);
            while (n > 1)
            {
                var box = new byte[1];
                do
                {
                    provider.GetBytes(box);
                }
                while (!(box[0] < n * (int.MaxValue / n)));

                var k = box[0] % n;
                n--;
                var value = result[k];
                result[k] = result[n];
                result[n] = value;
            }

            return result;
        }
    }
}
