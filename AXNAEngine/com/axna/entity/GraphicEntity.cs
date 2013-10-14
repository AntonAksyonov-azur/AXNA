using AXNAEngine.com.axna.graphics;
using Microsoft.Xna.Framework;

namespace AXNAEngine.com.axna.entity
{
    public class GraphicEntity : UiEntity
    {
        public float Angle;
        public Graphic Graphic;

        #region Basic actions

        public override void Draw(GameTime gameTime)
        {
            if (Graphic != null)
            {
                Graphic.Render(AXNA.SpriteBatch, Position);
            }

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            if (Graphic != null)
            {
                Graphic.Update(gameTime);
            }
        }

        #endregion

        /// <summary>
        ///     Creates new Entity
        /// </summary>
        /// <param name="graphic"></param>
        /// <param name="x">X position of entity</param>
        /// <param name="y">Y position of entity</param>
        public GraphicEntity(Graphic graphic, float x, float y) : base(new Vector2(x, y))
        {
            Graphic = graphic;
        }
    }
}