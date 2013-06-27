using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AXNAEngine.com.axna;
using AXNAEngine.com.axna.graphics;
using AXNAEngine.com.axna.managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AXNAEngine.com.testgame
{
    public class TestWorld : World
    {
        private GameEntity _entity;

        public TestWorld() : base("TestWorld")
        {
        }

        public override void OnInitialize()
        {
            AXNA.DebugMode = true;

            Texture2D texture = AXNA.Content.Load<Texture2D>("Textures/TestTexture");
            Image image = new Image(texture);
            image.CenterOrigin();
            image.SetRotationAngleByDegrees(45);
            //image.SetRelative(t);
            _entity = new GameEntity(image, 100, 100);
            //_entity.SetHitbox(64, 64, 32, 32);

            AddEntity(_entity);
        }

        public override void OnUpdate(GameTime gameTime)
        {
            AXNA.Game.Window.Title = 
                String.Format("Mouse X:{0}, Y:{1}", InputManager.GetMouseX(), InputManager.GetMouseY());

            base.OnUpdate(gameTime);
        }
    }
}
