using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AXNAEngine.com.axna.worlds
{
    public class RenderTargetWorld : World
    {
        protected RenderTarget2D RenderTarget;
        protected float RenderScale = 1.0f;
        protected Vector2 RenderCenterOffset = Vector2.Zero;

        public RenderTargetWorld(String name) : base(name)
        {
        }

        public override void OnInitialize()
        {
            RenderTarget = new RenderTarget2D(
                AXNA.GraphicsDevice,
                AXNA.GraphicsDevice.PresentationParameters.BackBufferWidth,
                AXNA.GraphicsDevice.PresentationParameters.BackBufferHeight);

            base.OnInitialize();
        }


        public override void OnDraw(GameTime gameTime)
        {
            AXNA.GraphicsDevice.Clear(ClearColor);
            AXNA.GraphicsDevice.SetRenderTarget(RenderTarget);
            AXNA.GraphicsDevice.Clear(Color.CornflowerBlue);

            AXNA.SpriteBatch.Begin();
            foreach (GameEntity entity in Entities)
            {
                entity.Draw(gameTime);
            }
            AXNA.SpriteBatch.End();

            AXNA.GraphicsDevice.SetRenderTarget(null);
            AXNA.GraphicsDevice.Clear(Color.CornflowerBlue);

            AXNA.SpriteBatch.Begin();
            AXNA.SpriteBatch.Draw(
                RenderTarget,
                new Vector2(
                    AXNA.GraphicsDevice.PresentationParameters.BackBufferWidth / 2 + RenderCenterOffset.X,
                    AXNA.GraphicsDevice.PresentationParameters.BackBufferHeight / 2 + RenderCenterOffset.Y),
                null, Color.White, 0f,
                new Vector2(
                    RenderTarget.Width / 2,
                    RenderTarget.Height / 2),
                RenderScale,
                SpriteEffects.None,
                0f);
            AXNA.SpriteBatch.End();
        }
    }
}