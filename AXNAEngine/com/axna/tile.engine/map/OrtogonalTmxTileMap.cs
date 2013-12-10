using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TiledSharp;

namespace AXNAEngine.com.axna.tile.engine.map
{
    public class OrtogonalTmxTileMap : AbstractTileMap
    {
        private readonly TmxMap _tmxFormatData;

        public OrtogonalTmxTileMap(Vector2 position, TileSet tileSet, TmxMap tmxFormatData, TileMapCamera camera)
            : base(position, tileSet, tmxFormatData.Width, tmxFormatData.Height, camera)
        {
            _tmxFormatData = tmxFormatData;
        }

        public override void DrawMap(GameTime gameTime)
        {
            var firstSquare = new Vector2(
                Camera.Location.X / TileSet.TileWidth,
                Camera.Location.Y / TileSet.TileHeight);
            var firstX = (int) firstSquare.X;
            var firstY = (int) firstSquare.Y;

            var squareOffset = new Vector2(
                Camera.Location.X % TileSet.TileWidth,
                Camera.Location.Y % TileSet.TileHeight);

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
                                texture: TileSet.TileSetTexture,
                                destinationRectangle:
                                    new Rectangle((int) ((x * TileSet.TileWidth) - squareOffset.X),
                                        (int) ((y * TileSet.TileHeight) - squareOffset.Y),
                                        TileSet.TileWidth, TileSet.TileHeight),
                                sourceRectangle: TileSet.GetSourceRectangle(gid),
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

        public float FogRes = 1.0f;

        public override void DrawFogOfWar(GameTime gameTime)
        {
            var firstSquare = new Vector2(
                Camera.Location.X / TileSet.TileWidth,
                Camera.Location.Y / TileSet.TileHeight);
            var firstX = (int) firstSquare.X;
            var firstY = (int) firstSquare.Y;

            var squareOffset = new Vector2(
                Camera.Location.X % TileSet.TileWidth,
                Camera.Location.Y % TileSet.TileHeight);

            var rS = FogOfWarLightRadius * FogOfWarLightRadius;

            FogRes = MathHelper.Clamp(FogRes, 0.1f, 1.0f);

            for (float y = 0; y < Camera.SquaresDown; y += FogRes)
            {
                for (float x = 0; x < Camera.SquaresAcross; x += FogRes)
                {
                    var tileMapCoordinateY = y + firstY;
                    var tileMapCoordinateX = x + firstX;
                    var rValue = Math.Pow(tileMapCoordinateX - FogOfWarLightPosition.X, 2) +
                                 Math.Pow(tileMapCoordinateY - FogOfWarLightPosition.Y, 2);

                    if (rValue >= rS)
                    {
                        AXNA.SpriteBatch.Draw(
                            FogOfWarTexture,
                            new Vector2(
                                (x * TileSet.TileWidth - squareOffset.X),
                                (y * TileSet.TileHeight - squareOffset.Y)),
                            new Rectangle(0, 0, FogOfWarTexture.Width, FogOfWarTexture.Height), 
                            new Color(1.0f, 1.0f, 1.0f, 1.0f - (float) (1.0f * (rS / rValue))),
                            0.0f,
                            new Vector2(FogOfWarTexture.Width * FogRes / 2, FogOfWarTexture.Height * FogRes / 2),
                            FogRes,
                            SpriteEffects.None,
                            1.0f);

                        /*
                        AXNA.SpriteBatch.Draw(
                            texture: FogOfWarTexture,
                            position:
                                new Vector2(
                                    (x * TileSet.TileWidth - squareOffset.X),
                                    (y * TileSet.TileHeight - squareOffset.Y)),
                            color: new Color(1.0f, 1.0f, 1.0f, 1.0f - (float) (1.0f * (rS / rValue))));
                            //color: new Color(1.0f, 1.0f, 1.0f, 1.0f));
                         */
                    }
                }
            }
        }
    }
}