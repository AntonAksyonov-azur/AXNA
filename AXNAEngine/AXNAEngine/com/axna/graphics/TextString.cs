using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AXNAEngine.com.axna.graphics
{
    public class TextString : Graphics
    {
        public SpriteFont SpriteFont;
        public String Text;

        public TextString(String text, SpriteFont spriteFont)
        {
            Text = text;
            SpriteFont = spriteFont;
        }

        public override void Render(SpriteBatch spriteBatch, Vector2 position)
        {
            AXNA.SpriteBatch.DrawString(SpriteFont, Text, GetDrawPosition(position), Color.White);
        }
    }
}