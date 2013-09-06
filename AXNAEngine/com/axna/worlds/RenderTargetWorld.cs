using System;
using AXNAEngine.com.axna.entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AXNAEngine.com.axna.worlds
{
    public class RenderTargetWorld : World
    {
        protected RenderTarget2D RenderTarget;
        protected Point RenderTargetSize;
        
        protected float RenderScale = 1.0f;        
        protected Rectangle CameraRectangle;

        public RenderTargetWorld(String name, Point renderTargetSize, Rectangle cameraRectangle) : base(name)
        {
            CameraRectangle = cameraRectangle;
            RenderTargetSize = renderTargetSize;
        }

        public override void OnInitialize()
        {
            RenderTarget = new RenderTarget2D(
                AXNA.GraphicsDevice,
                RenderTargetSize.X,
                RenderTargetSize.Y);

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
                        AXNA.GraphicsDevice.PresentationParameters.BackBufferWidth / 2,
                        AXNA.GraphicsDevice.PresentationParameters.BackBufferHeight / 2),
                CameraRectangle, 
                Color.White, 
                0f,
                new Vector2(
                        CameraRectangle.Width / 2,
                        CameraRectangle.Height / 2),
                RenderScale,
                SpriteEffects.None,
                0f);
            
            AXNA.SpriteBatch.End();
        }
    }
}