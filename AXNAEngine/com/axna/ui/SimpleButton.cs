using System;
using System.Collections.Generic;
using AXNAEngine.com.axna.entity;
using AXNAEngine.com.axna.graphics;
using AXNAEngine.com.axna.managers;
using Microsoft.Xna.Framework;

namespace AXNAEngine.com.axna.ui
{
    public class SimpleButton : BasicEntity
    {
        /// <summary>
        /// При значении false запрещает кнопке вызывать функцию по клику.
        /// Остальная функциональность продолжает работать
        /// </summary>
        public bool Clickable = true;

        private readonly Dictionary<SimpleButtonState, Graphic> _states =
            new Dictionary<SimpleButtonState, Graphic>();

        private SimpleButtonState _currentState = SimpleButtonState.Normal;
        private Action _actionOnClick;
        private Action _actionOnOver;
        private Action _actionOnExit;
        private bool _overFlag;

        public SimpleButton(Rectangle positionAndSize, Graphic normal, Graphic over, Graphic pressed) :
            base(new Vector2(positionAndSize.X, positionAndSize.Y))
        {
            _states.Add(SimpleButtonState.Normal, normal);
            _states.Add(SimpleButtonState.Over, over);
            _states.Add(SimpleButtonState.Pressed, pressed);

            Hitbox = new Rectangle(0, 0, positionAndSize.Width, positionAndSize.Height);
        }

        public void SetActions(Action onClick, Action onOver, Action onExit)
        {
            _actionOnClick = onClick;
            _actionOnOver = onOver;
            _actionOnExit = onExit;
        }

        public override void Draw(GameTime gameTime)
        {
            _states[_currentState].Render(AXNA.SpriteBatch, Position);

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            if (CollideMouse() && !InputManager.IsMouseLeftDown() && _currentState != SimpleButtonState.Over)
            {
                if (_actionOnOver != null)
                {
                    _actionOnOver.Invoke();
                }
                _currentState = SimpleButtonState.Over;
                _overFlag = true;
            }
            else if (CollideMouse() && InputManager.IsMouseLeftDown() && _currentState != SimpleButtonState.Pressed)
            {
                _currentState = SimpleButtonState.Pressed;
            }
            else if (!CollideMouse() && _currentState != SimpleButtonState.Normal)
            {
                if (_overFlag)
                {
                    _actionOnExit.Invoke();
                    _overFlag = false;
                }
                _currentState = SimpleButtonState.Normal;
            }


            if (IsMouseClick() && Clickable && _actionOnClick != null)
            {
                _actionOnClick.Invoke();
            }
        }

        //
        private enum SimpleButtonState
        {
            Normal = 0,
            Over = 1,
            Pressed = 2
        };
    }
}