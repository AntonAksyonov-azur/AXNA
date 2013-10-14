using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AXNAEngine.com.axna.tile.engine.p2
{
    public class TileSet
    {
        public Texture2D TileSetTexture { get; private set; }
        public int TileWidth { get; private set; }
        public int TileHeight { get; private set; }

        public TileSet(Texture2D tilesetTexture, int tileWidth, int tileHeight)
        {
            TileSetTexture = tilesetTexture;
            TileWidth = tileWidth;
            TileHeight = tileHeight;
        }

        public Rectangle GetSourceRectangle(int tileIndex)
        {
            int tileY = tileIndex / (TileSetTexture.Width / TileWidth);
            int tileX = tileIndex % (TileSetTexture.Width / TileWidth);

            return new Rectangle(tileX * TileWidth, tileY * TileHeight, TileWidth, TileHeight);
        }
    }
}