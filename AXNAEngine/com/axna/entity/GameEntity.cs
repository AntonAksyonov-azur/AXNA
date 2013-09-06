using AXNAEngine.com.axna.graphics;
using Microsoft.Xna.Framework;
using StarDefenceTutorial.com.c3;

namespace AXNAEngine.com.axna.entity
{
    public class GameEntity : EngineEntity
    {
        public float Angle;
        public Graphic Graphic;
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
            Rectangle entityRectangle = new Rectangle(
                (int) Position.X + Hitbox.X,
                (int) Position.Y + Hitbox.Y,
                Hitbox.Width,
                Hitbox.Height);

            return entityRectangle;
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

        /// <summary>
        ///     Creates new Entity
        /// </summary>
        /// <param name="graphic"></param>
        /// <param name="x">X position of entity</param>
        /// <param name="y">Y position of entity</param>
        public GameEntity(Graphic graphic, float x, float y)
        {
            Graphic = graphic;
            Position = new Vector2(x, y);
        }
    }
}