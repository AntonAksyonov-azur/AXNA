using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AXNAEngine.com.axna.graphics
{
    public class TextString : Graphic
    {
        public SpriteFont SpriteFont;
        public String Text;
        public Color OverColor;

        public TextString(String text, SpriteFont spriteFont, Color overColor)
        {
            Text = text;
            SpriteFont = spriteFont;
            OverColor = overColor;
        }

        public override void Render(SpriteBatch spriteBatch, Vector2 position)
        {
            AXNA.SpriteBatch.DrawString(SpriteFont, Text, GetDrawPosition(position), OverColor);
        }
    }
}