using AXNAEngine.com.axna;
using AXNAEngine.com.axna.managers;
using AXNAEngine.com.axna.tile.engine;
using AXNAEngine.com.axna.tile.engine.map;
using AXNAEngine.com.axna.worlds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AXNAEngine.com.testgame
{
    public class TileMapHexWorld : World
    {
        private const int TileWidth = 33;
        private const int TileHeight = 27;

        private HexArrayTileMap _map;
        private float _tileMapScrollSpeed;
        private TileMapCamera _tileMapCamera;
        private Vector2 _oldMousePos;
        private Vector2 _oldCameraPos;
        private bool _isMouseDrag;

        public TileMapHexWorld() : base("TileMapHexWorld")
        {
        }

        private int[,] GetMapData()
        {
            var result = new int[10, 10];
            // Create Sample Map Data
            result[0, 3] = 3;
            result[0, 4] = 3;
            result[0, 5] = 1;
            result[0, 6] = 1;
            result[0, 7] = 1;

            result[1, 3] = 3;
            result[1, 4] = 1;
            result[1, 5] = 1;
            result[1, 6] = 1;
            result[1, 7] = 1;

            result[2, 2] = 3;
            result[2, 3] = 1;
            result[2, 4] = 1;
            result[2, 5] = 1;
            result[2, 6] = 1;
            result[2, 7] = 1;

            result[3, 2] = 3;
            result[3, 3] = 1;
            result[3, 4] = 1;
            result[3, 5] = 2;
            result[3, 6] = 2;
            result[3, 7] = 2;

            result[4, 2] = 3;
            result[4, 3] = 1;
            result[4, 4] = 1;
            result[4, 5] = 2;
            result[4, 6] = 2;
            result[4, 7] = 2;

            result[5, 2] = 3;
            result[5, 3] = 1;
            result[5, 4] = 1;
            result[5, 5] = 2;
            result[5, 6] = 2;
            result[5, 7] = 2;

            return result;
        }

        public override void OnInitialize()
        {
            var tileset = new TileSet(AXNA.Content.Load<Texture2D>(@"Textures/Tiles/part3_tileset"), TileWidth,
                TileHeight);
            _tileMapCamera = new TileMapCamera(10, 10);
            _map = new HexArrayTileMap(
                GetMapData(),
                Vector2.Zero,
                tileset, 10, 10,
                _tileMapCamera,
                52, 14, 26);
            AddEntity(_map);


            SetClearColor(Color.Black);
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
                var vectorBorder = new Vector2((_map.MapWidth - _tileMapCamera.SquaresAcross) * TileWidth,
                    (_map.MapHeight - _tileMapCamera.SquaresDown) * TileHeight);

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
                        (_map.MapWidth - _tileMapCamera.SquaresAcross) * TileWidth);
            }

            if (InputManager.IsKeyDown(Keys.Right))
            {
                _tileMapCamera.Location.X =
                    MathHelper.Clamp(
                        _tileMapCamera.Location.X + speed,
                        0,
                        (_map.MapWidth - _tileMapCamera.SquaresAcross) * TileWidth);
            }

            if (InputManager.IsKeyDown(Keys.Up))
            {
                _tileMapCamera.Location.Y =
                    MathHelper.Clamp(
                        _tileMapCamera.Location.Y - speed,
                        0,
                        (_map.MapHeight - _tileMapCamera.SquaresDown) * TileHeight);
            }

            if (InputManager.IsKeyDown(Keys.Down))
            {
                _tileMapCamera.Location.Y =
                    MathHelper.Clamp(
                        _tileMapCamera.Location.Y + speed,
                        0,
                        (_map.MapHeight - _tileMapCamera.SquaresDown) * TileHeight);
            }
            base.OnUpdate(gameTime);
        }
    }
}