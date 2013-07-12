using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AXNAEngine.com.axna.graphics
{
    public class Spritemap : Image
    {
        protected float ElapsedTime = 0.0f;
        protected Dictionary<String, Anim> AnimationList = new Dictionary<string, Anim>();
        protected Anim PlayedAnim;
        public int CurrentFrame { get; private set; }

        public Spritemap(Texture2D source, int frameWidth, int frameHeight)
            : base(source)
        {
            _frameWidth = frameWidth;
            _frameHeight = frameHeight;
        }

        #region Source control

        public void AddAnimation(String name, Point firstFramePosition,
                                 int framesCount, int framesPerSecond = 30, bool loop = false)
        {
            AnimationList.Add(
                name,
                new Anim(name, firstFramePosition, framesCount, framesPerSecond, loop));
        }

        public void RemoveAnimation(String name)
        {
            if (AnimationList.ContainsKey(name))
            {
                AnimationList.Remove(name);
            }
            else
            {
                // Как-то вывести информацию об ошибке
                throw new Exception(String.Format("Анимация {0} не зарегистрирована (операция удаления)!", name));
            }
        }

        #endregion

        /// <summary>
        ///     Начинает воспроизведение анимации
        /// </summary>
        /// <param name="name">Имя анимации. Предварительно должно быть зарегистрировано в экземпляре Spritemap</param>
        /// <param name="forceFrame">Можно указать кадр, с которой анимация начнет воспроизводиться</param>
        public void PlayAnimation(String name, int forceFrame = 0)
        {
            if (AnimationList.ContainsKey(name))
            {
                IsActive = true;
                PlayedAnim = AnimationList[name];
                ElapsedTime = 0;
                CurrentFrame = forceFrame;
            }
            else
            {
                throw new Exception(String.Format("Анимация {0} не зарегистрирована (операция воспроизведения)!", name));
            }
        }


        public override void Update(GameTime gameTime)
        {
            if (!IsActive) return;

            ElapsedTime += AXNA.GetTimeIntervalValue(gameTime);

            if (ElapsedTime >= PlayedAnim.TargetTime)
            {
                //CurrentFrame = (CurrentFrame + 1) % PlayedAnim.framesPerSecond;
                CurrentFrame += 1;
                if (CurrentFrame == PlayedAnim.FramesCount)
                {
                    if (PlayedAnim.Loop)
                    {
                        CurrentFrame = 0;
                    }
                    else
                    {
                        IsActive = false;
                    }
                }

                ElapsedTime = 0.0f;
            }
        }

        public override void Render(SpriteBatch spriteBatch, Vector2 position)
        {
            if (!IsVisible) return;

            Rectangle positionRectangle =
                IsRelative
                    ? new Rectangle(
                          (int) (position.X + X + Origin.X),
                          (int) (position.Y + Y + Origin.Y),
                          _frameWidth, _frameHeight)
                    : new Rectangle(X, Y, _frameWidth, _frameHeight);

            spriteBatch.Draw(
                Texture,
                new Vector2(positionRectangle.X, positionRectangle.Y),
                GetCurrentFrameRectangle(),
                GetColor(),
                Angle,
                Origin,
                Scale,
                SpriteEffect,
                0);
        }

        public Anim GetCurrentAnimation()
        {
            return PlayedAnim;
        }

        private Rectangle GetCurrentFrameRectangle()
        {
            var rectangle = new Rectangle(
                PlayedAnim.FirstFramePosition.X + (_frameWidth * CurrentFrame),
                PlayedAnim.FirstFramePosition.Y,
                _frameWidth, _frameHeight);

            return rectangle;
        }

        public override void CenterOrigin()
        {
            Origin = new Vector2(_frameWidth / 2, _frameHeight / 2);
        }

        public bool IsAnimationFinished()
        {
            if (PlayedAnim != null)
            {
                return CurrentFrame == PlayedAnim.FramesPerSecond - 1;
            }

            return false;
        }

        //
        private readonly int _frameWidth;
        private readonly int _frameHeight;
        private float targetTime;
    }
}