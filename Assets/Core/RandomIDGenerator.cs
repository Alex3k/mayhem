using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mayhem.Core
{
    public static class RandomIDGenerator
    {
        /// <summary>
        /// Removed 0, o, O, 1, I
        /// </summary>
        private static char[] _base62chars ="23456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnpqrstuvwxyz".ToCharArray();

        private static Random _random = new Random((int)(DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond));
        private static int ID_LENGTH = 10;

        public static string GenerateID()
        {
            var sb = new StringBuilder(ID_LENGTH);

            for (int i = 0; i < ID_LENGTH; i++)
            {
                sb.Append(_base62chars[_random.Next(_base62chars.Length)]);
            }
            return sb.ToString();
        }
    }
}
