using System;
using AXNAEngine.com.axna.managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AXNAEngine.com.axna.ui
{
    class ClickableLabel : DrawableGameComponent
    {
        internal String Text { get; private set; }
        internal SpriteFont Font { get; private set; }
        internal Vector2 Position { get; private set; }
        internal Rectangle Rect { get; private set; }
        internal Action Action { get; private set; }

        public ClickableLabel(Game game, string text, SpriteFont font, Vector2 position, Action action)
            : base(game)
        {
            Text = text;
            Font = font;
            Vector2 measure = font.MeasureString(Text);

            Position = new Vector2(
              (int)(position.X - measure.X / 2),
              (int)(position.Y - measure.Y / 2));

            Rect = new Rectangle(
              (int)Position.X,
              (int)Position.Y,
              (int)measure.X,
              (int)measure.Y);

            Action = action;
        }

        internal bool IsHovered()
        {
            return Rect.Contains(InputManager.GetMousePositionToPoint());
        }

        internal bool IsClicked()
        {
            return IsHovered() && InputManager.IsMouseLeftClick();
        }

        public override void Draw(GameTime gameTime)
        {
            AXNA.SpriteBatch.DrawString(Font, Text, Position, Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            if (IsClicked())
            {
                Action.Invoke();
            }
        }
    }
}
