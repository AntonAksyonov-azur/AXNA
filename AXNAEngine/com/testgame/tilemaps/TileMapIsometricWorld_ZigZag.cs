using AXNAEngine.com.axna;
using AXNAEngine.com.axna.entity;
using AXNAEngine.com.axna.graphics;
using AXNAEngine.com.axna.managers;
using AXNAEngine.com.axna.tile.engine;
using AXNAEngine.com.axna.tile.engine.map;
using AXNAEngine.com.axna.worlds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TiledSharp;

namespace AXNAEngine.com.testgame.tilemaps
{
    public class TileMapIsometricWorld_ZigZag : World
    {
        private const int TileSize = 64;

        private AbstractTileMap _map;
        private float _tileMapScrollSpeed;
        private TileMapCamera _tileMapCamera;
        private Vector2 _oldMousePos;
        private Vector2 _oldCameraPos;
        private bool _isMouseDrag;
        private Point _lightPos;


        public TileMapIsometricWorld_ZigZag() : base("TileMapIsometricWorld_ZigZag")
        {
            ClearColor = Color.Black;
        }

        public override void OnInitialize()
        {
            var tmxFormatData = new TmxMap(
                string.Format(@"{0}/{1}", AXNA.Content.RootDirectory, @"Tilemaps/ZigZagMap.tmx"));

            var tileset = new TileSet(
                AXNA.Content.Load<Texture2D>(@"Textures/Tiles/part4_tileset"),
                64, 64,
                64, 32,
                32,
                32, 32);

            _tileMapCamera = new TileMapCamera(50, 50);
            _tileMapCamera.Location = new Vector2(690, 0);

            _map = new IsometricZigZagTmxMap(Vector2.Zero, tileset, tmxFormatData, _tileMapCamera);
            _lightPos = new Point(15, 20);
            //_map.SetupFogOfWar(_lightPos, 4, AXNA.Content.Load<Texture2D>(@"Textures/Tiles/FogOfWar/FogOfWarIsometric"));


            AddEntity(_map);

            _tileMapScrollSpeed = tileset.TileWidth;
        }

        public override void OnUpdate(GameTime gameTime)
        {
            MouseDrag();

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

            // Fog of war
            if (InputManager.IsKeyPressed(Keys.S))
            {
                _map.SetFogOfWarLightPosition(
                    new Point(_map.FogOfWarLightPosition.X, _map.FogOfWarLightPosition.Y + 1));
            }
            if (InputManager.IsKeyPressed(Keys.W))
            {
                _map.SetFogOfWarLightPosition(
                    new Point(_map.FogOfWarLightPosition.X, _map.FogOfWarLightPosition.Y - 1));
            }
            if (InputManager.IsKeyPressed(Keys.A))
            {
                _map.SetFogOfWarLightPosition(
                    new Point(_map.FogOfWarLightPosition.X - 1, _map.FogOfWarLightPosition.Y));
            }
            if (InputManager.IsKeyPressed(Keys.D))
            {
                _map.SetFogOfWarLightPosition(
                    new Point(_map.FogOfWarLightPosition.X + 1, _map.FogOfWarLightPosition.Y));
            }


            if (InputManager.IsKeyPressed(Keys.R))
            {
                (_map as IsometricZigZagTmxMap).FogRes += 0.1f;
            }
            if (InputManager.IsKeyPressed(Keys.F))
            {
                (_map as IsometricZigZagTmxMap).FogRes -= 0.1f;
            }

            base.OnUpdate(gameTime);
        }

        private void MouseDrag()
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
                var vectorBorder = new Vector2(
                    (_map.MapWidth - _tileMapCamera.SquaresAcross) * 64,
                    (_map.MapHeight - _tileMapCamera.SquaresDown) * 32);

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
        }
    }
}