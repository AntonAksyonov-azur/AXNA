using System;
using Microsoft.Xna.Framework;

namespace AXNAEngine.com.axna.graphics
{
    public class Anim
    {
        public Point FirstFramePosition;

        public float FrameRate;
        public int FramesCount;

        public bool Loop;
        public String Name;

        public Anim(string name, Point firstFramePosition, int framesCount, float frameRate, bool loop)
        {
            Name = name;
            FirstFramePosition = firstFramePosition;
            FramesCount = framesCount;
            FrameRate = frameRate;
            Loop = loop;
        }
    }
}