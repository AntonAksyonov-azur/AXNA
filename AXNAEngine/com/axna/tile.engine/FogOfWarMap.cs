using AXNAEngine.com.axna.entity;
using AXNAEngine.com.axna.tile.engine.map;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AXNAEngine.com.axna.tile.engine
{
    public class FogOfWarMap : EngineEntity
    {
        public AbstractTileMap TileMap { get; private set; }
        private Vector2 _fogOfWarLightPosition;
        private float _fogOfWarLightRadius;
        private Texture2D _fogOfWarTexture;

        public FogOfWarMap(Vector2 position, AbstractTileMap tileMap, Vector2 fogOfWarLightPosition,
            float fogOfWarLightRadius,
            Texture2D fogOfWarTexture) : base(position)
        {
            TileMap = tileMap;
            TileMap.Position += Position;

            _fogOfWarLightPosition = fogOfWarLightPosition;
            _fogOfWarLightRadius = fogOfWarLightRadius;
            _fogOfWarTexture = fogOfWarTexture;
        }

        public override void Draw(GameTime gameTime)
        {
            TileMap.Draw(gameTime);

            for (int y = 0; y < TileMap.Camera.SquaresDown; y++)
            {
                for (int x = 0; x < TileMap.Camera.SquaresAcross; x++)
                {
                    AXNA.SpriteBatch.Draw(
                        _fogOfWarTexture,
                        new Rectangle(
                            (x * TileMap.TileSet.TileWidth),
                            (y * TileMap.TileSet.TileHeight),
                            TileMap.TileSet.TileWidth, TileMap.TileSet.TileHeight),
                        Color.White);
                }
            }
        }
    }
}