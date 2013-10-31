using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace AXNAEngine.com.axna.tile.engine.map
{
    public class HexArrayTileMap : AbstractTileMap
    {
        public HexArrayTileMap(Vector2 position, TileSet tileSet, int mapWidth, int mapHeight, TileMapCamera camera) : 
            base(position, tileSet, mapWidth, mapHeight, camera)
        {
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
