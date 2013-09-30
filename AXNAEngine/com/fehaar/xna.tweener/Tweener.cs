using System;
using Microsoft.Xna.Framework;

namespace AXNAEngine.com.fehaar.xna.tweener
{
    public delegate float TweeningFunction(float timeElapsed, float start, float change, float duration);

    public class Tweener
    {
        public Tweener(float from, float to, float duration, TweeningFunction tweeningFunction)
        {
            From = from;
            Position = from;
            Change = to - from;
            TweeningFunction = tweeningFunction;
            Duration = duration;
        }

        public Tweener(float from, float to, TimeSpan duration, TweeningFunction tweeningFunction)
            : this(from, to, (float) duration.TotalSeconds, tweeningFunction)
        {
        }

        #region Properties

        public bool Running { get; protected set; }
        public float Position { get; protected set; }
        public float From { get; protected set; }
        public float Change { get; protected set; }
        public float Duration { get; protected set; }
        public float Elapsed { get; protected set; }
        protected TweeningFunction TweeningFunction;

        public delegate void EndHandler();

        public event EndHandler Ended;

        #endregion

        #region Methods

        public virtual void Update(GameTime gameTime)
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
            }
        }

        protected void OnEnd()
        {
            if (Ended != null)
            {
                Ended();
            }
        }

        public void Start()
        {
            Running = true;
        }

        public void Stop()
        {
            Running = false;
        }

        public void Reset()
        {
            Elapsed = 0.0f;
            From = Position;
            Start();
        }

        public void Reset(float to)
        {
            Change = to - Position;
            Reset();
        }

        public void Reverse()
        {
            Elapsed = 0.0f;
            Change = -Change + (From + Change - Position);
            From = Position;
            Start();
        }

        public override string ToString()
        {
            return String.Format("{0}.{1}. Tween {2} -> {3} in {4}s. Elapsed {5:##0.##}s",
                                 TweeningFunction.Method.DeclaringType != null
                                     ? TweeningFunction.Method.DeclaringType.Name
                                     : "null",
                                 TweeningFunction.Method.Name,
                                 From,
                                 From + Change,
                                 Duration,
                                 Elapsed);
        }

        #endregion
    }
}