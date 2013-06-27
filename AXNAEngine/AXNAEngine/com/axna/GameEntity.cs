using AXNAEngine.com.axna.graphics;
using Microsoft.Xna.Framework;
using StarDefenceTutorial.com.c3;

namespace AXNAEngine.com.axna
{
    public class GameEntity : EngineEntity
    {
        public float Angle;
        public Graphics Graphic;
        public Rectangle Hitbox;

        public bool IsActive = true;
        public bool IsVisible = true;
        public Vector2 Position;

        #region Basic actions

        public override void Draw(GameTime gameTime)
        {
            if (Graphic != null && IsVisible)
            {
                Graphic.Render(AXNA.SpriteBatch, Position);
            }

            if (AXNA.DebugMode)
            {
                AXNA.SpriteBatch.DrawRectangle(GetEntityRectangle(), Color.Red);
                AXNA.SpriteBatch.DrawRectangle(new Rectangle((int) Position.X - 3, (int) Position.Y - 3, 6, 6),
                                               Color.Lime);
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (!IsActive) return;

            if (Graphic != null)
            {
                Graphic.Update(gameTime);
            }
        }

        #endregion

        #region Collide functions

        public bool CollideWith(GameEntity gameEntity)
        {
            Rectangle thisRectange = GetEntityRectangle();
            Rectangle collideRectange = gameEntity.GetEntityRectangle();

            return thisRectange.Intersects(collideRectange);
        }

        public bool CollidePoint(int x, int y)
        {
            Rectangle thisRectange = GetEntityRectangle();

            return thisRectange.Contains(x, y);
        }

        public Rectangle GetEntityRectangle()
        {
            var entityRectangle =
                new Rectangle(
                    (int) Position.X + Hitbox.X, (int) Position.Y + Hitbox.Y, Hitbox.Width, Hitbox.Height);

            return entityRectangle;
        }

        #endregion

        protected GameEntity(Graphics graphics, float x, float y, int width, int height)
        {
            Graphic = graphics;
            Hitbox = new Rectangle(0, 0, width, height);
            Position = new Vector2(x, y);
        }
    }
}