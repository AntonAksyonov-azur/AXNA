using AXNAEngine.com.axna.utility;
using Microsoft.Xna.Framework;

namespace AXNAEngine.com.axna.configuration
{
    public class ParticleEngineConfiguration : IConfigurationObject
    {
        public bool IsOneShot;
        public int EmitInterval;
        public int MinEmission;
        public int MaxEmission;
        public int MinEnergy;
        public int MaxEnergy;
        public Vector2 LocalVelocity;
        public Vector2 RandomVelocity;

        public Vector2 ScaleSpeed;

        public float AngleSpeed;
        public float RandomMinAngle;
        public float RandomMaxAngle;

        public float FadeSpeed;

        public void InitDefault()
        {
            IsOneShot = false;
            EmitInterval = 1000;
            MinEmission = 10;
            MaxEmission = 30;
            MinEnergy = 3000;
            MaxEnergy = 3000;
            LocalVelocity = Vector2.Zero;
            RandomVelocity = Vector2.Zero;

            ScaleSpeed =  Vector2.Zero;

            AngleSpeed = 0;
            RandomMinAngle = 0;
            RandomMaxAngle = 0;

            FadeSpeed = 0;
        }
    }
}