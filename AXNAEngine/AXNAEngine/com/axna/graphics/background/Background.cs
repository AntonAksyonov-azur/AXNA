using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AXNAEngine.com.axna.graphics.background
{
    public class Background : EngineEntity
    {
        public bool StretchHorizontal = false;
        public bool StretchVertical = false;
        public Texture2D Texture;
        public int ViewportHeight;
        public int ViewportWidth;
        public int X;
        public int Y;

        public Background(Texture2D texture, int viewportWidth, int viewportHeight)
        {
            Texture = texture;
            ViewportWidth = viewportWidth;
            ViewportHeight = viewportHeight;
        }

        public override void Draw(GameTime gameTime)
        {
            var destinationRectangle =
                new Rectangle(X, Y,
                              StretchHorizontal ? ViewportWidth : Texture.Width,
                              StretchVertical ? ViewportHeight : Texture.Height);

            AXNA.SpriteBatch.Draw(Texture, destinationRectangle, Color.White);
        }
    }
}