using AXNAEngine.com.axna.managers;
using AXNAEngine.com.c3;
using Microsoft.Xna.Framework;

namespace AXNAEngine.com.axna.entity
{
    public class UiEntity : EngineEntity
    {
        public Rectangle Hitbox;

        public UiEntity(Vector2 position) : base(position)
        {
        }

        public bool IsMouseClick()
        {
            return CollideMouse() && InputManager.IsMouseLeftClick();
        }

        public bool CollideMouse()
        {
            Rectangle thisRectange = GetEntityRectangle();
            Point mousePosition = InputManager.GetMousePositionToPoint();

            return thisRectange.Contains(mousePosition.X, mousePosition.Y);
        }

        public Rectangle GetEntityRectangle()
        {
            Rectangle entityRectangle = new Rectangle(
                (int)Position.X + Hitbox.X,
                (int)Position.Y + Hitbox.Y,
                Hitbox.Width,
                Hitbox.Height);

            return entityRectangle;
        }

        #region Collide functions

        public bool CollideWith(UiEntity gameEntity)
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

        #endregion

        #region initialization functions

        /// <summary>
        ///     Sets the Entity's hitbox properties.
        /// </summary>
        /// <param name="width"> Width of the hitbox.</param>
        /// <param name="height">Height of the hitbox.</param>
        /// <param name="originX">X origin of the hitbox.</param>
        /// <param name="originY">Y origin of the hitbox.</param>
        public void SetHitbox(int width = 0, int height = 0, int originX = 0, int originY = 0)
        {
            Hitbox = new Rectangle(originX, originY, width, height);
        }

        #endregion

        public override void Draw(GameTime gameTime)
        {
            if (AXNA.DebugMode)
            {
                AXNA.SpriteBatch.DrawRectangle(GetEntityRectangle(), Color.Red);
                AXNA.SpriteBatch.DrawRectangle(new Rectangle((int)Position.X - 3, (int)Position.Y - 3, 6, 6),
                                               Color.Lime);
            }
        }
    }
}
