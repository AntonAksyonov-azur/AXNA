using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace AXNAEngine.com.axna.managers
{
    public class InputManager
    {
        private static KeyboardState _keyState;
        private static KeyboardState _keyOldState;
        private static MouseState _mouseState;
        private static MouseState _mouseOldState;

        public static void Update(GameTime gameTime)
        {
            _keyOldState = _keyState;
            _mouseOldState = _mouseState;

            _keyState = Keyboard.GetState();
            _mouseState = Mouse.GetState();
        }

        public static bool IsKeyDown(Keys key)
        {
            return _keyState.IsKeyDown(key);
        }

        public static bool IsKeyPressed(Keys key)
        {
            return IsKeyDown(key) && _keyOldState.IsKeyUp(key);
        }

        public static bool IsKeyUp(Keys key)
        {
            return !IsKeyDown(key) && _keyOldState.IsKeyDown(key);
        }

        public static bool IsMouseLeftDown()
        {
            return _mouseState.LeftButton == ButtonState.Pressed;
        }

        public static bool IsMouseLeftUp()
        {
            return _mouseState.LeftButton == ButtonState.Released;
        }

        public static bool IsMouseLeftClick()
        {
            return IsMouseLeftDown() && _mouseOldState.LeftButton == ButtonState.Released;
        }

        public static bool IsMouseRightDown()
        {
            return _mouseState.RightButton == ButtonState.Pressed;
        }

        public static bool IsMouseRightClick()
        {
            return IsMouseRightDown() && _mouseOldState.RightButton == ButtonState.Released;
        }

        public static bool IsMouseMiddleDown()
        {
            return _mouseState.MiddleButton == ButtonState.Pressed;
        }

        public static bool IsMouseMiddleClick()
        {
            return IsMouseMiddleDown() && _mouseOldState.MiddleButton == ButtonState.Released;
        }

        public static bool IsMouseWheelUp()
        {
            return _mouseState.ScrollWheelValue > _mouseOldState.ScrollWheelValue;
        }

        public static bool IsMouseWheelDown()
        {
            return _mouseState.ScrollWheelValue < _mouseOldState.ScrollWheelValue;
        }

        public static Vector2 MousePositionToVector2()
        {
            return new Vector2(_mouseState.X, _mouseState.Y);
        }

        public static Point MousePositionToPoint()
        {
            return new Point(_mouseState.X, _mouseState.Y);
        }

        public static int GetMouseX()
        {
            return _mouseState.X;
        }

        public static int GetMouseY()
        {
            return _mouseState.Y;
        }
    }
}