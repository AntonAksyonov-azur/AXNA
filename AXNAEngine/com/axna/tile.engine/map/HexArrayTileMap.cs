using Microsoft.Xna.Framework;

namespace AXNAEngine.com.axna.tile.engine.map
{
    public class HexArrayTileMap : AbstractTileMap
    {
        private int[,] _mapData;
        private int _tileStepX;
        private int _tileStepY;
        private int _oddRowXOffset;
        private int _globalOffsetX;
        private int _globalOffsetY;

        public HexArrayTileMap(
            int[,] mapData,
            int mapWidth, int mapHeight,
            Vector2 position,
            TileSet tileSet,
            TileMapCamera camera,
            int tileStepX, int tileStepY, int oddRowOffsetX,
            int globalOffsetX = 0, int globalOffsetY = 0) :
                base(position, tileSet, mapWidth, mapHeight, camera)
        {
            _mapData = mapData;

            _tileStepX = tileStepX;
            _tileStepY = tileStepY;
            _oddRowXOffset = oddRowOffsetX;

            _globalOffsetX = globalOffsetX;
            _globalOffsetY = globalOffsetY;
        }

        public override void Draw(GameTime gameTime)
        {
            var firstSquare = new Vector2(
                Camera.Location.X / _tileStepX,
                Camera.Location.Y / _tileStepY);
            var firstX = (int) firstSquare.X;
            var firstY = (int) firstSquare.Y;

            var squareOffset = new Vector2(
                Camera.Location.X % _tileStepX,
                Camera.Location.Y % _tileStepY);

            for (int y = 0; y < Camera.SquaresDown; y++)
            {
                for (int x = 0; x < Camera.SquaresAcross; x++)
                {
                    int rowOffset = 0;
                    if ((firstY + y) % 2 == 1)
                    {
                        rowOffset = _oddRowXOffset;
                    }

                    if (_mapData[y + firstY, x + firstX] > -1)
                    {
                        AXNA.SpriteBatch.Draw(
                            TileSet.TileSetTexture,
                            new Rectangle(
                                (int)((x * _tileStepX) - squareOffset.X + rowOffset + _globalOffsetX),
                                (int)((y * _tileStepY) - squareOffset.Y + _globalOffsetY),
                                TileSet.TileWidth, TileSet.TileHeight),
                            TileSet.GetSourceRectangle(_mapData[y + firstY, x + firstX]),
                            Color.White);
                    }
                }
            }
        }
    }
}