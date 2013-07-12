using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AXNAEngine.com.axna.graphics.particles
{
    public class Particle
    {
        /// <summary>
        ///     Положение частицы
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        ///     Направление движения
        /// </summary>
        public Vector2 Direction { get; set; }

        /// <summary>
        ///     Длительность существования частицы (в ms)
        /// </summary>
        public int LifeTime { get; set; }

        /// <summary>
        ///     Графическое представление частицы
        /// </summary>
        public Texture2D Texture { get; set; }

        /// <summary>
        ///     Цветовое перекрытие частицы
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        ///     Размер частицы
        /// </summary>
        public Vector2 Scale { get; set; }

        private Vector2 _scaleSpeed;

        /// <summary>
        ///     Угол поворота частицы
        /// </summary>
        public float Angle { get; set; }

        private float _angleSpeed;

        /// <summary>
        ///     Прозрачность
        /// </summary>
        public float Alpha { get; set; }

        private float _fadeSpeed;

        private readonly Random _random;
        private readonly Vector2 _origin;

        public Particle(Texture2D texture, Vector2 position, Vector2 direction, int lifeTime, Color color)
        {
            Texture = texture;
            Position = position;
            Direction = direction;
            LifeTime = lifeTime;
            Color = color;

            _random = new Random();

            _origin = new Vector2(texture.Width / 2, texture.Height / 2);
        }

        public Particle(Texture2D texture, Vector2 position, Vector2 direction, int lifeTime, Color color,
                        float startAngle, float angleSpeed, Vector2 scaleSpeed, float fadeSpeed)
        {
            Texture = texture;
            Position = position;
            Direction = direction;
            LifeTime = lifeTime;
            Color = color;

            Angle = startAngle;
            _angleSpeed = angleSpeed;

            Scale = Vector2.One;
            _scaleSpeed = scaleSpeed;

            Alpha = 1.0f;
            _fadeSpeed = fadeSpeed;

            _random = new Random();

            _origin = new Vector2(texture.Width / 2, texture.Height / 2);
        }

        public void UpdateParticle(GameTime gameTime)
        {
            LifeTime -= gameTime.ElapsedGameTime.Milliseconds;
            Position += Direction;
            Angle += _angleSpeed;
            Alpha += _fadeSpeed;
            Scale += _scaleSpeed;
            //Color = new Color(1f, (float)_random.NextDouble(), 0f);
        }

        public void DrawParticle(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(Texture, position + Position, null, Color * Alpha, Angle, _origin, Scale,
                             SpriteEffects.None, 1.0f);
            //spriteBatch.Draw(Texture, position + Position, Color.White);
        }
    }
}