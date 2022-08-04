using System;
using System.Security.Cryptography;

namespace Core.Utilities.Generator
{
    public  static class RandomGenerator
    {
        public static string Get6Digits()
        {
            var bytes = new byte[4];
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);
            var random = BitConverter.ToUInt32(bytes, 0) % 1000000;
            return $"{random:D6}";
        }

        public static string Get8Digits()
        {
            var bytes = new byte[4];
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);
            uint random = BitConverter.ToUInt32(bytes, 0) % 100000000;
            return $"{random:D8}";
        }
    }
}
