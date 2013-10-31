using System;
using System.Collections.Generic;
using AXNAEngine.com.axna.extension.net;

namespace AXNAEngine.com.axna.utility
{
    public static class LandscapeGenerator
    {
        public static int[,] GenerateLandscape(
            int mapWidth, int mapHeight,
            List<int> availableLandscapeIds,
            List<Direction> availableDirections,
            int seedsCount,
            int growSteps,
            int emptyCellId = 0)
        {
            // Future result
            var resultMap = new int[mapHeight, mapWidth];

            // Clear
            for (int i = 0; i < mapHeight; i++)
            {
                for (int j = 0; j < mapWidth; j++)
                {
                    resultMap[i, j] = emptyCellId;
                }
            }

            // Random
            var rnd = new Random();

            // Seeds
            var seeds = new List<Seed>();
            for (int i = 0; i < seedsCount; i++)
            {
                var seed = new Seed(
                    rnd.Next(0, mapWidth),
                    rnd.Next(0, mapHeight),
                    availableLandscapeIds.GetRandomItem());
                seeds.Add(seed);
                resultMap[seed.Y, seed.X] = seed.LandscapeValue;
            }

            // Growing
            for (int i = 0; i < growSteps; i++)
            {
                for (int j = 0; j < seedsCount; j++)
                {
                    var direction = availableDirections.GetRandomItem();
                    var seed = seeds[j];
                    seed.X += direction.X;
                    seed.Y += direction.Y;

                    if (seed.X >= mapWidth || seed.X < 0 ||
                        seed.Y >= mapHeight || seed.Y < 0)
                    {
                        seed.X = rnd.Next(0, mapWidth);
                        seed.Y = rnd.Next(0, mapHeight);
                    }
                    resultMap[seed.Y, seed.X] = seed.LandscapeValue;
                }
            }

            return resultMap;
        }

        public struct Direction
        {
            public int X;
            public int Y;
        }

        public static List<Direction> GenerateNESW()
        {
            var result = new List<Direction>
            {
                new Direction {X = 0, Y = 1}, // N
                new Direction {X = 1, Y = 0}, // E
                new Direction {X = 0, Y = -1}, // S
                new Direction {X = -1, Y = 0}, // W
            };

            return result;
        }

        public static List<Direction> GenerateRose()
        {
            var result = new List<Direction>
            {
                new Direction {X = 0, Y = 1}, // N
                new Direction {X = 1, Y = 1}, // NE
                new Direction {X = 1, Y = 0}, // E
                new Direction {X = 1, Y = -1}, // SE
                new Direction {X = 0, Y = -1}, // S
                new Direction {X = -1, Y = -1}, // SW
                new Direction {X = -1, Y = 0}, // W
                new Direction {X = -1, Y = 1}, // NW
            };

            return result;
        }

        private class Seed
        {
            public int X;
            public int Y;
            public int LandscapeValue;

            public Seed(int x, int y, int lanscapeValue)
            {
                X = x;
                Y = y;
                LandscapeValue = lanscapeValue;
            }
        }
    }
}