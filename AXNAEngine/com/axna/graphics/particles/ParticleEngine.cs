using System;
using System.Collections.Generic;
using AXNAEngine.com.axna.configuration;
using AXNAEngine.com.axna.net.extension;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AXNAEngine.com.axna.graphics.particles
{
    public class ParticleEngine : Graphic
    {
        #region Emission

        /// <summary>
        ///     Если true, все частицы будут создаваться одновременно, с интервалом EmitInterval
        ///     Если false, частицы будут создаваться равномерно в течении EmitInterval
        /// </summary>
        public bool IsOneShot = false;

        /// <summary>
        ///     Интервал (в ms) создания новых частиц
        /// </summary>
        public int EmitInterval = 1000;

        /// <summary>
        ///     Минимальное число создаваемых частиц
        /// </summary>
        public int MinEmission = 10;

        /// <summary>
        ///     Максимальное число создаваемых частиц
        /// </summary>
        public int MaxEmission = 30;

        #endregion

        #region Lifetime

        /// <summary>
        ///     Минимальный срок жизни частицы
        /// </summary>
        public int MinEnergy = 3000;

        /// <summary>
        ///     Максимальный срок жизни частицы
        /// </summary>
        public int MaxEnergy = 3000;

        #endregion

        #region Velocity

        /// <summary>
        ///     Скорость частицы
        /// </summary>
        public Vector2 LocalVelocity = Vector2.Zero;

        /// <summary>
        ///     Случайное значение между -value и value, добавляемое к скорости
        /// </summary>
        public Vector2 RandomVelocity = Vector2.Zero;

        #endregion

        #region Scale

        /// <summary>
        ///     Скорость изменения размера.
        ///     Положительные числа увеличивают размер, отрицательные уменьшают
        /// </summary>
        public Vector2 ScaleSpeed = Vector2.Zero;

        #endregion

        #region Angle

        /// <summary>
        ///     Скорость поворота частицы
        /// </summary>
        public float AngleSpeed = 0;


        /// <summary>
        ///     Начальное значение угла (минимальнное значение)
        /// </summary>
        public float RandomMinAngle = 0;

        /// <summary>
        ///     Начальное значение угла (максимальное значение)
        /// </summary>
        public float RandomMaxAngle = 0;

        #endregion

        #region Transparency

        /// <summary>
        ///     Скорость изменения прозрачности
        /// </summary>
        public float FadeSpeed = 0;

        #endregion

        // Private
        private readonly Random _random;
        private readonly List<Particle> _particles;
        private readonly Texture2D _texture;

        //
        private int _elapsedTime = 0;
        private int _elapsedTimeForNonOneShot = 0;
        private int _emitIntervalForNonOneShot = 0;

        public ParticleEngine(Texture2D texture, Point location, bool isOneShot)
        {
            X = location.X;
            Y = location.Y;
            _texture = texture;
            _particles = new List<Particle>();
            _random = new Random();

            IsOneShot = isOneShot;
            if (!IsOneShot)
            {
                SetupContinuousEmit();
            }
        }

        public ParticleEngine(ParticleEngineConfiguration configuration, Texture2D texture, Point location)
        {
            X = location.X;
            Y = location.Y;
            _texture = texture;
            _particles = new List<Particle>();
            _random = new Random();

            IsOneShot = configuration.IsOneShot;
            EmitInterval = configuration.EmitInterval;
            MinEmission = configuration.MinEmission;
            MaxEmission = configuration.MaxEmission;

            MinEnergy = configuration.MinEnergy;
            MaxEnergy = configuration.MaxEnergy;

            LocalVelocity = configuration.LocalVelocity;
            RandomVelocity = configuration.RandomVelocity;

            ScaleSpeed = configuration.ScaleSpeed;

            AngleSpeed = configuration.AngleSpeed;
            RandomMinAngle = configuration.RandomMinAngle;
            RandomMaxAngle = configuration.RandomMaxAngle;

            FadeSpeed = configuration.FadeSpeed;

            IsOneShot = configuration.IsOneShot;
            if (!IsOneShot)
            {
                SetupContinuousEmit();
            }
        }

        /// <summary>
        ///     Создание новой частицы
        /// </summary>
        /// <returns></returns>
        private Particle GenerateNewParticle()
        {
            Vector2 randomCalculatedVelocity =
                new Vector2(
                    _random.NextDoubleInRange(-RandomVelocity.X, RandomVelocity.X),
                    _random.NextDoubleInRange(-RandomVelocity.Y, RandomVelocity.Y));
            Vector2 finalVelocity = LocalVelocity + randomCalculatedVelocity;

            int lifetime = _random.Next(MinEnergy, MaxEnergy);

            float startAngle = _random.NextDoubleInRange(RandomMinAngle, RandomMaxAngle);

            Particle particle =
                new Particle(
                    _texture, new Vector2(X, Y), finalVelocity, lifetime, Color.White,
                    startAngle, AngleSpeed, ScaleSpeed, FadeSpeed);

            return particle;
        }

        public override void Update(GameTime gameTime)
        {
            _elapsedTime += gameTime.ElapsedGameTime.Milliseconds;
            if (IsOneShot)
            {
                if (_elapsedTime >= EmitInterval)
                {
                    int count = _random.Next(MinEmission, MaxEmission);
                    for (int i = 0; i < count; i++)
                    {
                        _particles.Add(GenerateNewParticle());
                    }

                    _elapsedTime = 0;
                }
            }
            else
            {
                _elapsedTimeForNonOneShot += gameTime.ElapsedGameTime.Milliseconds;
                if (_elapsedTimeForNonOneShot >= _emitIntervalForNonOneShot)
                {
                    for (int i = 0; i < _elapsedTimeForNonOneShot / _emitIntervalForNonOneShot; i++)
                    {
                        _particles.Add(GenerateNewParticle());
                    }

                    _elapsedTimeForNonOneShot = _elapsedTimeForNonOneShot % _emitIntervalForNonOneShot;
                }

                if (_elapsedTime >= EmitInterval)
                {
                    SetupContinuousEmit();
                }
            }


            // Удаляем частицы с истекшим сроком жизни
            for (int i = 0; i < _particles.Count; i++)
            {
                _particles[i].UpdateParticle(gameTime);
                if (_particles[i].LifeTime <= 0 || _particles[i].Alpha <= 0)
                {
                    _particles.RemoveAt(i);
                    i--;
                }
            }
        }

        public void SetupContinuousEmit()
        {
            int count = _random.Next(MinEmission, MaxEmission);
            _emitIntervalForNonOneShot = EmitInterval / count;
            _elapsedTime = 0;
        }

        public override void Render(SpriteBatch spriteBatch, Vector2 position)
        {
            //Drawing all the particles of the system
            foreach (Particle t in _particles)
            {
                t.DrawParticle(spriteBatch, position);
            }
        }
    }
}