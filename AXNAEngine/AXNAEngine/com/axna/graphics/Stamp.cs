using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AXNAEngine.com.axna.graphics
{
    /// <summary>
    /// Простой класс для отрисовки 2d текстур без трансформаций
    /// </summary>
    public class Stamp : Graphics
    {
        private readonly Texture2D _texture2D;

        public Stamp(Texture2D texture2D)
        {
            _texture2D = texture2D;
        }

        public override void Render(SpriteBatch spriteBatch, Vector2 position)
        {
            if (!IsVisible) return;

            spriteBatch.Draw(_texture2D, GetDrawPosition(position), Color.White);
        }
    }
}
