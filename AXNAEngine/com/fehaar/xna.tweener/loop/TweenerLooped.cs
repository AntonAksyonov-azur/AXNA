using System;
using Microsoft.Xna.Framework;

namespace AXNAEngine.com.fehaar.xna.tweener.loop
{
    public class TweenerLooped : Tweener
    {
        private TweenerLoopType _loopType;

        private delegate void LoopHandler();

        private event LoopHandler _loop;
        private float _to;

        #region Constructor

        public TweenerLooped(
            float @from,
            float to,
            float duration,
            TweeningFunction tweeningFunction,
            TweenerLoopType loopType = TweenerLoopType.None) : base(@from, to, duration, tweeningFunction)
        {
            _loopType = loopType;
            _loop = OnLoop;
            _to = to;
        }

        public TweenerLooped(
            float @from,
            float to,
            TimeSpan duration,
            TweeningFunction tweeningFunction,
            TweenerLoopType loopType = TweenerLoopType.None)
            : base(@from, to, duration, tweeningFunction)
        {
            _loopType = loopType;
            _loop = OnLoop;
            _to = to;
        }

        #endregion

        private void OnLoop()
        {
            switch (_loopType)
            {
                case TweenerLoopType.None:
                    // Nothing
                    break;
                case TweenerLoopType.Loop:
                    // Full restart
                    Reset();
                    break;
                case TweenerLoopType.LoopRestart:
                    Restart();
                    break;
                case TweenerLoopType.PingPong:
                    Reverse();
                    break;
            }
        }

        protected void Restart()
        {
            Elapsed = 0.0f;
            Position = From;
            Start();
        }

        //
        public override void Update(GameTime gameTime)
        {
            if (!Running || (Elapsed >= Duration))
            {
                return;
            }
            Position = TweeningFunction(Elapsed, From, Change, Duration);
            Elapsed += (float) gameTime.ElapsedGameTime.TotalSeconds;
            if (Elapsed >= Duration)
            {
                Elapsed = Duration;
                Position = From + Change;
                OnEnd();
                _loop();
            }
        }
    }
}