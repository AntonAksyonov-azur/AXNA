using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TiledSharp;

namespace AXNAEngine.com.axna.tile.engine.map
{
    public class IsometricZigZagTmxMap : AbstractTileMap
    {
        protected readonly TmxMap TmxFormatData;
        private const int CameraFogOfWarBonus = 3;

        public IsometricZigZagTmxMap(Vector2 position, TileSet tileSet, TmxMap tmxFormatData, TileMapCamera camera)
            : base(position, tileSet, tmxFormatData.Width, tmxFormatData.Height, camera)
        {
            TmxFormatData = tmxFormatData;
        }

        public override void DrawMap(GameTime gameTime)
        {
            var cameraBorders = DetermineCameraBorders();
            var squareOffset = DetermineOffset();

            foreach (var layer in TmxFormatData.Layers)
            {
                if (!layer.Visible) continue;

                for (int y = 0; y < Camera.SquaresDown; y++)
                {
                    for (int x = 0; x < Camera.SquaresAcross; x++)
                    {
                        var tile =
                            layer.Tiles[
                                (y + (int) cameraBorders.Y) * TmxFormatData.Height +
                                (x + (int) cameraBorders.X)];
                        var gid = tile.Gid - 1;

                        if (gid > -1)
                        {
                            var offsetX = tile.Y % 2 != 0 ? TileSet.TileWidth / 2 : 0;

                            var destPoint = new Vector2(
                                (float) Math.Ceiling((x * TileSet.TileStepX) + offsetX - squareOffset.X + Position.X),
                                (float)
                                    Math.Ceiling((y * (float) TileSet.TileStepY / 2) - squareOffset.Y / 2 + Position.Y));

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
                        }
                    }
                }
            }
        }

        public float FogRes = 0.5f;

        public override void DrawFogOfWar(GameTime gameTime)
        {
            var cameraBorders = DetermineCameraBorders();
            var squareOffset = DetermineOffset();
            var rS = FogOfWarLightRadius * FogOfWarLightRadius;

            FogRes = MathHelper.Clamp(FogRes, 0.1f, 1.0f);

            for (float y = -CameraFogOfWarBonus; y < Camera.SquaresDown + CameraFogOfWarBonus; y += FogRes)
            {
                for (float x = -CameraFogOfWarBonus; x < Camera.SquaresAcross + CameraFogOfWarBonus; x += FogRes)
                {
                    var tileMapCoordinateY = y + (int) cameraBorders.Y;
                    var tileMapCoordinateX = x + (int) cameraBorders.X;
                    var rValue = Math.Pow(tileMapCoordinateX - FogOfWarLightPosition.X, 2) +
                                 Math.Pow(tileMapCoordinateY - FogOfWarLightPosition.Y, 2);

                    if (rValue >= rS)
                    {
                        var offsetX = tileMapCoordinateY % 2 != 0 ? TileSet.TileWidth / 2 : 0;
                        var destPoint = new Vector2(
                            (float) Math.Ceiling((x * TileSet.TileStepX) + offsetX - squareOffset.X + Position.X),
                            (float) Math.Ceiling((y * TileSet.TileStepY / 2) - squareOffset.Y / 2 + Position.Y));

                        AXNA.SpriteBatch.Draw(
                            texture: FogOfWarTexture,
                            position: destPoint,
                            color: new Color(1.0f, 1.0f, 1.0f,
                                1.0f - (float) (1.0f * (rS / rValue))));
                        /*
                        var t = 255.0f * (rS / rValue);
                        
                        AXNA.SpriteBatch.DrawString(
                            AXNA.Content.Load<SpriteFont>(@"Base/Arial"), string.Format("{0:f0}", t),
                            destPoint + new Vector2(16, 36), Color.Red);
                         */
                    }
                }
            }
        }
    }
}