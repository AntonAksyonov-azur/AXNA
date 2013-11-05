using System.Collections.Generic;
using AXNAEngine.com.axna;
using AXNAEngine.com.axna.managers;
using AXNAEngine.com.axna.tile.engine;
using AXNAEngine.com.axna.tile.engine.map;
using AXNAEngine.com.axna.utility;
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
        private const int TileStepX = 52;
        private const int TileStepY = 14;
        private const int OddRowX = 26;

        private HexArrayTileMap _map;
        private float _tileMapScrollSpeed;
        private TileMapCamera _tileMapCamera;
        private Vector2 _oldMousePos;
        private Vector2 _oldCameraPos;
        private bool _isMouseDrag;

        public TileMapHexWorld() : base("TileMapHexWorld")
        {
        }

        public override void OnInitialize()
        {
            int mapWidth = 100;
            int mapHeight = 100;

            var tileset = new TileSet(
                AXNA.Content.Load<Texture2D>(@"Textures/Tiles/part3_tileset"),
                TileWidth, TileHeight,
                TileStepX, TileStepY,
                OddRowX,
                -14, -14);

            _tileMapCamera = new TileMapCamera(10, 10);

            _map = new HexArrayTileMap(
                LandscapeGenerator.GenerateLandscape(mapWidth, mapHeight, new List<int> {1, 2, 3, 4},
                    LandscapeGenerator.GenerateNESW(), 25, 300),
                mapWidth, mapHeight,
                Vector2.Zero,
                tileset,
                _tileMapCamera);
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
                _oldMousePos = InputManager.MousePositionToVector2();
            }

            if (_isMouseDrag)
            {
                var newCameraPos = _oldCameraPos + _oldMousePos - InputManager.MousePositionToVector2();
                var vectorZero = Vector2.Zero;
                var vectorBorder = new Vector2((_map.MapWidth - _tileMapCamera.SquaresAcross) * TileStepX,
                    (_map.MapHeight - _tileMapCamera.SquaresDown) * TileStepY);

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