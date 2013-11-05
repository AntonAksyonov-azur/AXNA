using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TiledSharp;

namespace AXNAEngine.com.axna.tile.engine.map
{
    public class IsometricZigZagTmxMap : AbstractTileMap
    {
        private readonly TmxMap _tmxFormatData;

        public IsometricZigZagTmxMap(Vector2 position, TileSet tileSet, TmxMap tmxFormatData, TileMapCamera camera)
            : base(position, tileSet, tmxFormatData.Width, tmxFormatData.Height, camera)
        {
            _tmxFormatData = tmxFormatData;
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
                            var offsetX = tile.Y % 2 != 0 ? TileSet.TileWidth / 2 : 0;
                            var destPoint = new Vector2(
                                (x * TileSet.TileStepX) + offsetX - squareOffset.X + Position.X,
                                (y * (float) TileSet.TileStepY / 2) - squareOffset.Y / 2 + Position.Y);

                            AXNA.SpriteBatch.Draw(
                                texture: TileSet.TileSetTexture,
                                position: destPoint,
                                sourceRectangle: TileSet.GetSourceRectangle(gid),
                                color: Color.White,
                                rotation: 0.0f,
                                origin: Vector2.Zero,
                                effect: (tile.HorizontalFlip ? SpriteEffects.FlipHorizontally : SpriteEffects.None) |
                                        (tile.VerticalFlip ? SpriteEffects.FlipVertically : SpriteEffects.None),
                                depth: 1.0f,
                                scale: 1.0f);
                            /*
                            AXNA.SpriteBatch.DrawRectangle(destPoint, new Vector2(64, 64),
                                new Color(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255)));
                             */
                        }
                    }
                }
            }
        }
    }
}