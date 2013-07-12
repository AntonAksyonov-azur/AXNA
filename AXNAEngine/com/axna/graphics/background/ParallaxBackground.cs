using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace AXNAEngine.com.axna.graphics.background
{
    class ParallaxBackground : EngineEntity
    {
        public List<ParallaxTexture> Textures = new List<ParallaxTexture>();
        public int ViewportWidth;
        public int ViewportHeight;

        public ParallaxBackground(ParallaxTexture mainTexture, int viewportWidth, int viewportHeight)
        {
            Textures.Add(mainTexture);
            ViewportWidth = viewportWidth;
            ViewportHeight = viewportHeight;
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (ParallaxTexture current in Textures)
            {
                float value = current.X + current.Texture.Width;
                float endValue = ViewportWidth + current.Texture.Width * 2;

                // Бесконечный цикл
                for (int i = -1; i > -999; i++)
                {
                    AXNA.SpriteBatch.Draw(current.Texture,
                        new Rectangle(
                            (int)current.X + i * current.Texture.Width,
                            (int)current.Y,
                            current.Texture.Width,
                            current.IsStretchVertical ? ViewportHeight : current.Texture.Height),
                            Color.White);

                    value += current.Texture.Width;
                    if (value > endValue)
                    {
                        break;
                    }
                }
            }
        }

        public void Move(float xOffset, float yOffset)
        {
            foreach (ParallaxTexture current in Textures)
            {
                current.X += current.ParallaxSpeed * xOffset;
                current.Y += current.ParallaxSpeed * yOffset;
            }
        }

    }
}
