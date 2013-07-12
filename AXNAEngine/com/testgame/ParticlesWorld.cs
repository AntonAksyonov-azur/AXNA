using AXNAEngine.com.axna;
using AXNAEngine.com.axna.graphics.particles;
using AXNAEngine.com.axna.worlds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AXNAEngine.com.testgame
{
    public class ParticlesWorld : World
    {
        public ParticleEngine OwnParticleEngine;
        public Texture2D ParticleTexture;

        private float _angle = 0.0f;
        private float _alpha = 1.0f;

        public ParticlesWorld() : base("Particles")
        {
        }

        public override void OnInitialize()
        {
            ParticleTexture = AXNA.Content.Load<Texture2D>("Textures/Particles/cross");

            OwnParticleEngine = new ParticleEngine(ParticleTexture, new Point(0, 0), false);

            GameEntity particleSystem = new GameEntity(OwnParticleEngine, 400, 300);
            OwnParticleEngine.RandomVelocity = new Vector2(5, 5);
            //OwnParticleEngine.LocalVelocity = new Vector2(1, 1);
            OwnParticleEngine.EmitInterval = 1000;
            OwnParticleEngine.MinEmission = 100;
            OwnParticleEngine.MaxEmission = 300;
            OwnParticleEngine.SetupContinuousEmit();

            OwnParticleEngine.RandomMinAngle = 0;
            OwnParticleEngine.RandomMaxAngle = 3;
            OwnParticleEngine.AngleSpeed = 0.1f;

            OwnParticleEngine.ScaleSpeed = new Vector2(-0.01f, 0.1f);

            OwnParticleEngine.FadeSpeed = -0.01f;

            //OwnParticleEngine.IsOneShot = false;

            AddEntity(particleSystem);
            
        }

        public override void OnUpdate(GameTime gameTime)
        {
            _angle += 0.1f;
            _alpha += _alpha > 0 ? -0.01f : 0;


            base.OnUpdate(gameTime);
        }


        public override void OnDraw(GameTime gameTime)
        {/*
            AXNA.SpriteBatch.Begin();

            AXNA.SpriteBatch.Draw(ParticleTexture,
                                  new Vector2(100, 100),
                                  null,
                                  Color.White * _alpha,
                                  _angle,
                                  new Vector2(ParticleTexture.Width / 2, ParticleTexture.Height / 2),
                                  1.0f, SpriteEffects.None, 1.0f);

            AXNA.SpriteBatch.End();
            */
            base.OnDraw(gameTime);
        }
    }
}