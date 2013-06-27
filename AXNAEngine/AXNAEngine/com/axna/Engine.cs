using AXNAEngine.com.axna.managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AXNAEngine.com.axna
{
    public class Engine : Game
    {
        protected readonly GraphicsDeviceManager Graphics;

        public Engine(int width, int height, bool isMouseVisible = true)
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            Graphics.PreferredBackBufferWidth = width;
            Graphics.PreferredBackBufferHeight = height;
            Graphics.ApplyChanges();

            IsMouseVisible = isMouseVisible;
        }

// ReSharper disable RedundantOverridenMember
        protected override void Initialize()
        {
            base.Initialize();
        }
// ReSharper restore RedundantOverridenMember

        protected override void LoadContent()
        {
            AXNA.Game = this;
            AXNA.Content = Content;
            AXNA.GraphicsDevice = Graphics.GraphicsDevice;
            AXNA.WorldManager = new WorldManager();
            AXNA.SpriteBatch = new SpriteBatch(Graphics.GraphicsDevice);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            InputManager.Update(gameTime);
            AXNA.WorldManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);
        }
    }
}