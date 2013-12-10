using System;
using AXNAEngine.com.axna;
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
    public class TileMapOrthogonalWorld : World
    {
        private OrtogonalTmxTileMap _map;
        private TileMapCamera _tileMapCamera;
        private Vector2 _oldMousePos;
        private Vector2 _oldCameraPos;
        private bool _isMouseDrag;

        public TileMapOrthogonalWorld() : base("OrthographicTileMap")
        {
        }

        public override void OnInitialize()
        {
            _tileMapCamera = new TileMapCamera(10, 10) {Location = new Vector2(0, 0)};
            _map = new OrtogonalTmxTileMap(
                Vector2.Zero,
                new TileSet(AXNA.Content.Load<Texture2D>(@"Textures\Tiles\part2_tileset"), 48, 48),
                new TmxMap(String.Format(@"{0}/{1}", AXNA.Content.RootDirectory, @"Tilemaps/ExampleMap.tmx")),
                _tileMapCamera);

            _map.SetupFogOfWar(new Point(5, 5), 2,
                AXNA.Content.Load<Texture2D>(@"Textures\Tiles\FogOfWar\fogofwar_black"));

            AddEntity(_map);
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

            if (InputManager.IsKeyPressed(Keys.Down))
            {
                _map.FogRes -= 0.1f;
            }
            if (InputManager.IsKeyPressed(Keys.Up))
            {
                _map.FogRes += 0.1f;
            }

            base.OnUpdate(gameTime);

            AXNA.Game.Window.Title = String.Format("CameraX: {0}, CameraY {1}", _tileMapCamera.Location.X,
                _tileMapCamera.Location.Y);
        }
    }
}