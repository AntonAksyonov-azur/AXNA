using Microsoft.Xna.Framework;

namespace AXNAEngine.com.axna.tile.engine.map
{
    public class HexArrayTileMap : AbstractTileMap
    {
        private int[,] _mapData;

        public HexArrayTileMap(
            int[,] mapData,
            int mapWidth, int mapHeight,
            Vector2 position,
            TileSet tileSet,
            TileMapCamera camera) :
                base(position, tileSet, mapWidth, mapHeight, camera)
        {
            _mapData = mapData;
        }

        public override void Draw(GameTime gameTime)
        {
            var firstSquare = new Vector2(
                Camera.Location.X / TileSet.TileStepX,
                Camera.Location.Y / TileSet.TileStepY);
            var firstX = (int) firstSquare.X;
            var firstY = (int) firstSquare.Y;

            var squareOffset = new Vector2(
                Camera.Location.X % TileSet.TileStepX,
                Camera.Location.Y % TileSet.TileStepY);

            for (int y = 0; y < Camera.SquaresDown; y++)
            {
                for (int x = 0; x < Camera.SquaresAcross; x++)
                {
                    int rowOffset = 0;
                    if ((firstY + y) % 2 == 1)
                    {
                        rowOffset = TileSet.OddRowXOffset;
                    }

                    if (_mapData[y + firstY, x + firstX] > -1)
                    {
                        AXNA.SpriteBatch.Draw(
                            TileSet.TileSetTexture,
                            new Rectangle(
                                (int)((x * TileSet.TileStepX) - squareOffset.X + rowOffset + TileSet.GlobalOffsetX),
                                (int)((y * TileSet.TileStepY) - squareOffset.Y + TileSet.GlobalOffsetY),
                                TileSet.TileWidth, TileSet.TileHeight),
                            TileSet.GetSourceRectangle(_mapData[y + firstY, x + firstX]),
                            Color.White);
                    }
                }
            }
        }
    }
}