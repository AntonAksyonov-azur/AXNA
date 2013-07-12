using System;
using AXNAEngine.com.axna.managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AXNAEngine.com.axna
{
// ReSharper disable InconsistentNaming
    public class AXNA
// ReSharper restore InconsistentNaming
    {
        // Класс хранит ссылки на все общие объекты, необходимые для работы основных компонентов XNA
        // Все ссылки должны быть инициализированы до непосредственного использования остальных классов
        public static Game Game;
        public static ContentManager Content;
        public static GraphicsDevice GraphicsDevice;
        public static SpriteBatch SpriteBatch;
        public static WorldManager WorldManager;

        public static Random Rnd = new Random();

        public static bool DebugMode = true;
        //        public static bool DebugMode = false;

        public static float GetTimeIntervalValue(GameTime gameTime)
        {
            return (float) gameTime.ElapsedGameTime.Milliseconds / 1000;
        }
    }
}