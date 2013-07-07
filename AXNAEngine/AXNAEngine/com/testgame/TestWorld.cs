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

        public TestWorld() : base("TestWorld", new Point(1930, 2219), new Rectangle(0, 0, 800, 600))
        {
        }

        public override void OnInitialize()
        {
            AXNA.DebugMode = true;

            Texture2D texture = AXNA.Content.Load<Texture2D>("Textures/morr");
            Image image = new Image(texture);
            image.CenterOrigin();
            //image.SetRotationAngleByDegrees(45);
            //image.SetRelative(t);
            _entity = new GameEntity(image, 0, 0);
            //_entity.SetHitbox(64, 64, 32, 32);

            AddEntity(_entity);

            base.OnInitialize();
        }

        public override void OnUpdate(GameTime gameTime)
        {
            AXNA.Game.Window.Title =
                String.Format("Mouse X:{0}, Y:{1}; Scale:{2}",
                              InputManager.GetMouseX(), InputManager.GetMouseY(), RenderScale);

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