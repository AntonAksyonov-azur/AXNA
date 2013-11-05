using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AXNAEngine.com.axna.tile.engine
{
    public class TileSet
    {
        public Texture2D TileSetTexture { get; private set; }
        public int TileWidth { get; private set; }
        public int TileHeight { get; private set; }
        public int TileStepX { get; private set; }
        public int TileStepY { get; private set; }
        public int OddRowXOffset { get; private set; }
        public int GlobalOffsetX { get; private set; }
        public int GlobalOffsetY { get; private set; }

        public TileSet(Texture2D tilesetTexture,
            int tileWidth, int tileHeight,
            int tileStepX = 0, int tileStepY = 0,
            int oddRowOffsetX = 0,
            int globalOffsetX = 0, int globalOffsetY = 0)
        {
            TileSetTexture = tilesetTexture;
            TileWidth = tileWidth;
            TileHeight = tileHeight;

            TileStepX = tileStepX;
            TileStepY = tileStepY;

            OddRowXOffset = oddRowOffsetX;

            GlobalOffsetX = globalOffsetX;
            GlobalOffsetY = globalOffsetY;
        }

        public Rectangle GetSourceRectangle(int tileIndex)
        {
            int tileY = tileIndex / (TileSetTexture.Width / TileWidth);
            int tileX = tileIndex % (TileSetTexture.Width / TileWidth);

            return new Rectangle(tileX * TileWidth, tileY * TileHeight, TileWidth, TileHeight);
        }
    }
}