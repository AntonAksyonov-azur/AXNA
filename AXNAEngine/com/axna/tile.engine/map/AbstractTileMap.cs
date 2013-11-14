using System;
using AXNAEngine.com.axna.entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AXNAEngine.com.axna.tile.engine.map
{
    public abstract class AbstractTileMap : GraphicEntity
    {
        public TileSet TileSet { get; private set; }
        public int MapWidth { get; private set; }
        public int MapHeight { get; private set; }
        public TileMapCamera Camera { get; private set; }

        public Point FogOfWarLightPosition { get; protected set; }
        public float FogOfWarLightRadius { get; protected set; }
        protected Texture2D FogOfWarTexture;

        private bool _isFogOfWarEnabled;

        protected AbstractTileMap(Vector2 position, TileSet tileSet, int mapWidth, int mapHeight, TileMapCamera camera)
            : base(null, position.X, position.Y)
        {
            TileSet = tileSet;

            MapWidth = mapWidth;
            MapHeight = mapHeight;

            Camera = camera;
        }

        public void SetupFogOfWar(Point fogOfWarLightPosition,
            float fogOfWarLightRadius,
            Texture2D fogOfWarTexture)
        {
            _isFogOfWarEnabled = true;

            FogOfWarLightPosition = fogOfWarLightPosition;
            FogOfWarLightRadius = fogOfWarLightRadius;
            FogOfWarTexture = fogOfWarTexture;
        }

        public void SetFogOfWarLightPosition(Point position)
        {
            FogOfWarLightPosition = position;
        }

        public void SetFogOfWarLightRadius(float radius)
        {
            FogOfWarLightRadius = radius;
        }

        public override void Draw(GameTime gameTime)
        {
            DrawMap(gameTime);

            if (_isFogOfWarEnabled)
            {
                DrawFogOfWar(gameTime);
            }
        }

        public virtual void DrawMap(GameTime gameTime)
        {
            throw new Exception("Usage of base method");
        }

        public virtual void DrawFogOfWar(GameTime gameTime)
        {
            throw new Exception("Usage of base method");
        }
    }
}