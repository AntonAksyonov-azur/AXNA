﻿using AXNAEngine.com.axna;
using AXNAEngine.com.axna.managers;
using AXNAEngine.com.axna.tile.engine;
using AXNAEngine.com.axna.tile.engine.map;
using AXNAEngine.com.axna.worlds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TiledSharp;

namespace AXNAEngine.com.testgame.tilemaps
{
    public class TiledMapRenderTargetWorld : RenderTargetWorld
    {
        private Vector2 _oldMousePos;
        private Vector2 _oldCameraPos;
        private bool _isMouseDrag;
        private AbstractTileMap _map;

        public TiledMapRenderTargetWorld()
            : base("w123", new Point(6400, 3200), new Rectangle(0, 0, 800, 600))
        {
        }

        public override void OnInitialize()
        {
            var tmxFormatData = new TmxMap(
                               string.Format(@"{0}/{1}", AXNA.Content.RootDirectory, @"Tilemaps/Diamond.tmx"));
                // string.Format(@"{0}/{1}", AXNA.Content.RootDirectory, @"Tilemaps/ZigZagMap.tmx"));

            var tileset = new TileSet(
                AXNA.Content.Load<Texture2D>(@"Textures/Tiles/part4_tileset"),
                64, 64,
                64, 32,
                32,
                32, 32);

            var tileMapCamera = new TileMapCamera(50, 50);

            _map =
                new IsometricDiamondTmxMap(new Vector2(0, 0), tileset, tmxFormatData, tileMapCamera);
            //                new IsometricZigZagTmxMap(new Vector2(0, 0), tileset, tmxFormatData, _tileMapCamera);
            AddEntity(_map);

            base.OnInitialize();
        }

        public override void OnUpdate(GameTime gameTime)
        {
            if (InputManager.IsMouseLeftDown() && !_isMouseDrag)
            {
                _isMouseDrag = true;

                _oldCameraPos = new Vector2(CameraRectangle.X, CameraRectangle.Y);
                _oldMousePos = InputManager.MousePositionToVector2();
            }

            if (_isMouseDrag)
            {
                var newCameraPos = _oldCameraPos + _oldMousePos - InputManager.MousePositionToVector2();
                CameraRectangle.X = (int)newCameraPos.X;
                CameraRectangle.Y = (int)newCameraPos.Y;
            }

            if (InputManager.IsMouseLeftUp() && _isMouseDrag)
            {
                _isMouseDrag = false;
            }

            base.OnUpdate(gameTime);
        }
    }
}