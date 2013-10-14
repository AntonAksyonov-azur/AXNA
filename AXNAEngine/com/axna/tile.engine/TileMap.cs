using AXNAEngine.com.axna.entity;
using AXNAEngine.com.axna.tile.engine.p2;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TiledSharp;

namespace AXNAEngine.com.axna.tile.engine
{
    public class TileMap : EngineEntity
    {
        private TileSet _tileSet;
        private TmxMap _tmxFormatData;

        public int MapWidth { get; private set; }
        public int MapHeight { get; private set; }
        public TileMapCamera Camera;

        public TileMap(Vector2 position, TileSet tileSet, TmxMap tmxFormatData, TileMapCamera camera) : base(position)
        {
            _tileSet = tileSet;
            _tmxFormatData = tmxFormatData;

            MapWidth = tmxFormatData.Width;
            MapHeight = tmxFormatData.Height;

            Camera = camera;
        }

        public override void Draw(GameTime gameTime)
        {
            var firstSquare = new Vector2(
                Camera.Location.X / _tileSet.TileWidth,
                Camera.Location.Y / _tileSet.TileHeight);
            var firstX = (int) firstSquare.X;
            var firstY = (int) firstSquare.Y;

            var squareOffset = new Vector2(
                Camera.Location.X % _tileSet.TileWidth,
                Camera.Location.Y % _tileSet.TileHeight);
            var offsetX = (int) squareOffset.X;
            var offsetY = (int) squareOffset.Y;

            foreach (var layer in _tmxFormatData.Layers)
            {
                if (!layer.Visible) continue;

                for (int y = 0; y < Camera.SquaresDown; y++)
                {
                    for (int x = 0; x < Camera.SquaresAcross; x++)
                    {
                        var tile = layer.Tiles[(y + firstY) * _tmxFormatData.Height + (x + firstX)];
                        var gid = tile.Gid - 1;

                        if (gid > -1)
                        {
                            AXNA.SpriteBatch.Draw(
                                texture: _tileSet.TileSetTexture,
                                destinationRectangle:
                                    new Rectangle((x * _tileSet.TileWidth) - offsetX,
                                                  (y * _tileSet.TileHeight) - offsetY,
                                                  _tileSet.TileWidth, _tileSet.TileHeight),
                                sourceRectangle: _tileSet.GetSourceRectangle(gid),
                                color: Color.White,
                                rotation: 0.0f,
                                origin: Vector2.Zero,
                                effect: (tile.HorizontalFlip ? SpriteEffects.FlipHorizontally : SpriteEffects.None) |
                                        (tile.VerticalFlip ? SpriteEffects.FlipVertically : SpriteEffects.None),
                                depth: 1.0f);
                        }
                    }
                }
            }
        }
    }
}