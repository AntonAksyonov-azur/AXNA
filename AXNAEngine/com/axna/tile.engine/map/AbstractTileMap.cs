using System;
using AXNAEngine.com.axna.entity;
using Microsoft.Xna.Framework;

namespace AXNAEngine.com.axna.tile.engine.map
{
    public abstract class AbstractTileMap : EngineEntity
    {
        protected TileSet TileSet;
        public int MapWidth { get; private set; }
        public int MapHeight { get; private set; }
        public TileMapCamera Camera { get; private set; }

        protected AbstractTileMap(Vector2 position, TileSet tileSet, int mapWidth, int mapHeight, TileMapCamera camera)
            : base(position)
        {
            TileSet = tileSet;

            MapWidth = mapWidth;
            MapHeight = mapHeight;

            Camera = camera;
        }

        public override void Draw(GameTime gameTime)
        {
            throw new Exception("Usage of base method");
        }
    }
}