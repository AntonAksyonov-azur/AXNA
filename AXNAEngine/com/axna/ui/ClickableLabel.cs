using System;
using AXNAEngine.com.axna.entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AXNAEngine.com.axna.ui
{
    public class ClickableLabel : UiEntity
    {
        public String Text { get; private set; }
        protected SpriteFont Font { get; private set; }
        protected Action Action { get; private set; }
        protected Color ColorState = Color.Black;

        public ClickableLabel(string text, SpriteFont font, Vector2 position, Action action) : base(position)
        {
            Text = text;
            Font = font;
            Vector2 measure = font.MeasureString(Text);
            /*
            Position = new Vector2(
                (int) (position.X - measure.X / 2),
                (int) (position.Y - measure.Y / 2));
            */
            Position = position;

            Hitbox = new Rectangle(0, 0, (int) measure.X, (int) measure.Y);

            Action = action;
        }

        public override void Draw(GameTime gameTime)
        {
            if (IsVisible)
            {
                AXNA.SpriteBatch.DrawString(Font, Text, Position, ColorState);
            }

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            if (CollideMouse() && ColorState != Color.Red)
            {
                ColorState = Color.Red;
            }

            if (!CollideMouse() && ColorState != Color.Black)
            {
                ColorState = Color.Black;
            }

            if (IsMouseClick())
            {
                Action.Invoke();
            }
        }

        public void ChangeText(string text)
        {
            Text = text;
        }
    }
}