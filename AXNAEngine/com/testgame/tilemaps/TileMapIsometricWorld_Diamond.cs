using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AXNAEngine.com.axna;
using AXNAEngine.com.axna.managers;
using AXNAEngine.com.axna.tile.engine;
using AXNAEngine.com.axna.tile.engine.map;
using AXNAEngine.com.axna.worlds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TiledSharp;

namespace AXNAEngine.com.testgame.tilemaps
{
    public class TileMapIsometricWorld_Diamond : World
    {
        private TileMapCamera _tileMapCamera;
        private AbstractTileMap _map;
        private Point _lightPos;
        private int _tileMapScrollSpeed;

        // MouseDrag
        private bool _isMouseDrag;
        private Vector2 _oldCameraPos;
        private Vector2 _oldMousePos;

        public TileMapIsometricWorld_Diamond() : base("Isometric_diamond")
        {
            ClearColor = Color.Black;
        }

        public override void OnInitialize()
        {
            var tmxFormatData = new TmxMap(
                string.Format(@"{0}/{1}", AXNA.Content.RootDirectory, @"Tilemaps/Diamond.tmx"));

            var tileset = new TileSet(
                AXNA.Content.Load<Texture2D>(@"Textures/Tiles/part4_tileset"),
                64, 64,
                64, 32,
                32,
                32, 32);

            var cameraPosition = new Vector2(0, 0);
            var mapPosition = new Vector2(500, 0);

            _tileMapCamera = new TileMapCamera(50, 50) { Location = cameraPosition };

            _map = new IsometricDiamondTmxMap(mapPosition, tileset, tmxFormatData, _tileMapCamera);
            _lightPos = new Point(15, 20);
            //_map.SetupFogOfWar(_lightPos, 4, AXNA.Content.Load<Texture2D>(@"Textures/Tiles/FogOfWar/FogOfWarIsometric"));

            AddEntity(_map);

            _tileMapScrollSpeed = tileset.TileWidth;
        }

        public override void OnUpdate(GameTime gameTime)
        {
            MouseDrag();

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
