using System;
using Microsoft.Xna.Framework;

namespace AXNAEngine.com.axna.graphics
{
    public class Anim
    {
        public Point FirstFramePosition { get; private set; }

        public int FramesCount { get; private set; }
        public float FramesPerSecond { get; private set; }
        public float TargetTime { get; private set; }

        public bool Loop { get; private set; }
        public String Name { get; private set; }

        public Anim(String name, Point firstFramePosition, int framesCount, float framesPerSecond, bool loop)
        {
            Name = name;

            FramesCount = framesCount;
            FirstFramePosition = firstFramePosition;
            FramesPerSecond = framesPerSecond;
            
            TargetTime = 1.0f / FramesPerSecond;
            
            Loop = loop;
        }

    }
}