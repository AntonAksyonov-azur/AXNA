using System;

namespace AXNAEngine.com.axna.net.extension
{
    public static class RandomExtensions
    {
        public static double NextDoubleInRange(this Random random, double minValue, double maxValue)
        {
            return random.NextDouble() * (maxValue - minValue) + minValue;
        }

        public static float NextDoubleInRange(this Random random, float minValue, float maxValue)
        {
            return (float)random.NextDouble() * (maxValue - minValue) + minValue;
        }
    }
}