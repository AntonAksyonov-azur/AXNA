using AXNAEngine.com.axna;
using AXNAEngine.com.axna.managers;
using AXNAEngine.com.axna.tile.engine;
using AXNAEngine.com.axna.worlds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TiledSharp;

namespace AXNAEngine.com.testgame
{
    public class TileMapIsometricWorld : World
    {
        private const int TileSize = 48;

        private TileMap _map;
        private float _tileMapScrollSpeed;
        private TileMapCamera _tileMapCamera;
        private Vector2 _oldMousePos;
        private Vector2 _oldCameraPos;
        private bool _isMouseDrag;

        public TileMapIsometricWorld() : base("TileMapIsometricWorld")
        {
        }

        public override void OnInitialize()
        {
            var tmxFormatData = new TmxMap(string.Format(@"{0}/{1}", AXNA.Content.RootDirectory, @"Tilemaps/ExampleMap.tmx"));
            var tileset = new TileSet(AXNA.Content.Load<Texture2D>(@"Textures/Tiles/part2_tileset"), TileSize, TileSize);
            _tileMapCamera = new TileMapCamera(20, 20);
            _map = new TileMap(Vector2.Zero, tileset, tmxFormatData, _tileMapCamera);
            AddEntity(_map);

            _tileMapScrollSpeed = tileset.TileWidth * 3;
        }

        public override void OnUpdate(GameTime gameTime)
        {
           if (InputManager.IsMouseLeftDown() && !_isMouseDrag)
           {
               _isMouseDrag = true;

               _oldCameraPos = _tileMapCamera.Location;
               _oldMousePos = InputManager.GetMousePositionToVector2();
           }

            if (_isMouseDrag)
            {
                var newCameraPos = _oldCameraPos + _oldMousePos - InputManager.GetMousePositionToVector2();
                var vectorZero = Vector2.Zero;
                var vectorBorder = new Vector2((_map.MapWidth - _tileMapCamera.SquaresAcross) * TileSize,
                                               (_map.MapHeight - _tileMapCamera.SquaresDown) * TileSize);

                Vector2.Clamp(
                    ref newCameraPos,
                    ref vectorZero, 
                    ref vectorBorder, 
                    out _tileMapCamera.Location);
            }

            if (InputManager.IsMouseLeftUp() && _isMouseDrag)
            {
                _isMouseDrag = false;
            }


            var speed = _tileMapScrollSpeed * AXNA.GetTimeIntervalValue(gameTime);

            if (InputManager.IsKeyDown(Keys.Left))
            {
                _tileMapCamera.Location.X =
                    MathHelper.Clamp(
                        _tileMapCamera.Location.X - speed,
                        0,
                        (_map.MapWidth - _tileMapCamera.SquaresAcross) * TileSize);
            }

            if (InputManager.IsKeyDown(Keys.Right))
            {
                _tileMapCamera.Location.X =
                    MathHelper.Clamp(
                        _tileMapCamera.Location.X + speed,
                        0,
                        (_map.MapWidth - _tileMapCamera.SquaresAcross) * TileSize);
            }

            if (InputManager.IsKeyDown(Keys.Up))
            {
                _tileMapCamera.Location.Y =
                    MathHelper.Clamp(
                        _tileMapCamera.Location.Y - speed,
                        0,
                        (_map.MapHeight - _tileMapCamera.SquaresDown) * TileSize);
            }

            if (InputManager.IsKeyDown(Keys.Down))
            {
                _tileMapCamera.Location.Y =
                    MathHelper.Clamp(
                        _tileMapCamera.Location.Y + speed,
                        0,
                        (_map.MapHeight - _tileMapCamera.SquaresDown) * TileSize);
            }
            base.OnUpdate(gameTime);
        }
    }
}