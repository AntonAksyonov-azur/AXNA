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
        /// <summary>
        /// Если true, все частицы будут создаваться одновременно, с интервалом EmitInterval
        /// Если false, частицы будут создаваться равномерно в течении EmitInterval
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

        /// <summary>
        ///     Минимальный срок жизни частицы
        /// </summary>
        public int MinEnergy = 3000;

        /// <summary>
        ///     Максимальный срок жизни частицы
        /// </summary>
        public int MaxEnergy = 3000;

        /// <summary>
        ///     Скорость частицы
        /// </summary>
        public Vector2 LocalVelocity = Vector2.Zero;

        /// <summary>
        ///     Случайное значение между -value и value, добавляемое к скорости
        /// </summary>
        public Vector2 RandomVelocity = Vector2.Zero;

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

            Particle particle =
                new Particle(
                    _texture,
                    new Vector2(X, Y),
                    finalVelocity,
                    lifetime,
                    Color.White);

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
                    _particles.Add(GenerateNewParticle());
                    _elapsedTimeForNonOneShot = 0;
                }

                if (_elapsedTime >= EmitInterval)
                {
                    SetupContinuousEmit();
                }
            }

            

            // Удаляем частицы с истекшим сроком жизни
            for (int particle = 0; particle < _particles.Count; particle++)
            {
                _particles[particle].UpdateParticle(gameTime);
                if (_particles[particle].LifeTime <= 0)
                {
                    _particles.RemoveAt(particle);
                    particle--;
                }
            }
        }

        private void SetupContinuousEmit()
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