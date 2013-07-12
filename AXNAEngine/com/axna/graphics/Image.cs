using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AXNAEngine.com.axna.graphics
{
    /// <summary>
    /// Изображение, поддерживающее эффекты трансформации
    /// Повороты, искажения размера, перекрытие цвета, отражение
    /// </summary>
    public class Image : Graphic
    {
        public float Scale { get; private set; }
        public float Angle { get; private set; }
        public SpriteEffects SpriteEffect = SpriteEffects.None;
        public Color OverlayColor { get; protected set; }
        public Vector2 Origin { get; protected set; }

        public Image(Texture2D texture2D, float scale = 1.0f, float angle = 0.0f)
        {
            Texture = texture2D;
            Scale = scale;
            Angle = angle;
        }

        public override void Render(SpriteBatch spriteBatch, Vector2 position)
        {
            if (!IsVisible) return;

            spriteBatch.Draw(
                Texture,
                GetDrawPosition(position) + Origin,
                null,
                GetColor(),
                Angle,
                Origin,
                Scale,
                SpriteEffect, 0);
        }

        #region Transformations

        #region Color
        public void SetOverlayColor(Color overlayColor)
        {
            OverlayColor = overlayColor;
        }

        protected Color GetColor()
        {
            return OverlayColor.ToVector4() != Vector4.Zero ? OverlayColor : Color.White;
        }
        #endregion

        #region Origin
        public void SetOrigin(Vector2 newOrigin)
        {
            Origin = newOrigin;
        }

        /// <summary>
        /// Устанавливает точку трансформации изображения в центр текстуры
        /// </summary>
        public virtual void CenterOrigin()
        {
            Origin = new Vector2(Texture.Width / 2, Texture.Height / 2);
        }
        #endregion

        #region SpriteEffects
        public void SetSpriteEffect(SpriteEffects spriteEffect)
        {
            SpriteEffect = spriteEffect;
        }
        #endregion

        #region Scale
        public virtual void SetScaleByValue(float scaleValue)
        {
            Scale = scaleValue;
        }
        #endregion

        #region Rotation
        /// <summary>
        /// Устанавливает угол поворота текстуры изображения. Значение должно быть указано в радианах
        /// </summary>
        /// <param name="angle"></param>
        public void SetRotationAngleByRadians(float angle)
        {
            Angle = angle;
        }

        /// <summary>
        /// Устанавливает угол поворота текстуры изображения. Значение должно быть указано в градусах
        /// </summary>
        /// <param name="degrees"></param>
        public void SetRotationAngleByDegrees(int degrees)
        {
            Angle = degrees * MathHelper.Pi / 180;
        }
        #endregion

        #endregion

        //        
        protected readonly Texture2D Texture;
    }
}
