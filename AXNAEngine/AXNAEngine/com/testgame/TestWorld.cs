using System;
using AXNAEngine.com.axna;
using AXNAEngine.com.axna.graphics;
using AXNAEngine.com.axna.managers;
using AXNAEngine.com.axna.worlds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AXNAEngine.com.testgame
{
    public class TestWorld : RenderTargetWorld
    {
        private GameEntity _entity;

        public Spritemap spritemap;

        public TestWorld() : base("TestWorld", new Point(1930, 2219), new Rectangle(0, 0, 800, 600))
        {
        }

        public override void OnInitialize()
        {
            AXNA.DebugMode = true;
            
            Texture2D texture = AXNA.Content.Load<Texture2D>("Textures/attack_up0");
            spritemap = new Spritemap(texture, 177, 165);

            spritemap.SetScaleByValue(1.0f);

            spritemap.AddAnimation("anim", Point.Zero, 13, 15, true);
            //spritemap.AddAnimation("anim", Point.Zero, 23, 10, true);
            spritemap.PlayAnimation("anim");
            
            _entity = new GameEntity(spritemap, 100, 100);

            AddEntity(_entity);

            base.OnInitialize();
        }

        public override void OnUpdate(GameTime gameTime)
        {
            /*
            AXNA.Game.Window.Title =
                String.Format("Mouse X:{0}, Y:{1}; Scale:{2}",
                              InputManager.GetMouseX(), InputManager.GetMouseY(), RenderScale);
            */

            AXNA.Game.Window.Title =
                String.Format("Anim frame {0}", spritemap.CurrentFrame);

            if (InputManager.IsKeyDown(Keys.Up))
            {
                RenderScale += 0.01f;
            }

            if (InputManager.IsKeyDown(Keys.Down))
            {
                RenderScale -= 0.01f;
            }

            //
            if (InputManager.IsKeyDown(Keys.W))
            {
                CameraRectangle.Y -= 10;
            }

            if (InputManager.IsKeyDown(Keys.S))
            {
                CameraRectangle.Y += 10;
            }

            if (InputManager.IsKeyDown(Keys.A))
            {
                CameraRectangle.X -= 10;
            }

            if (InputManager.IsKeyDown(Keys.D))
            {
                CameraRectangle.X += 10;
            }


            base.OnUpdate(gameTime);
        }
    }
}