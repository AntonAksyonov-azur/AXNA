using AXNAEngine.com.axna.worlds;
using Microsoft.Xna.Framework;

namespace AXNAEngine.com.axna.entity
{
    public class EngineEntity : DrawableGameComponent
    {
        public World ParentWorld;
        public Vector2 Position;

        public bool IsActive = true;
        public bool IsVisible = true;

        public EngineEntity(Vector2 position) : base(AXNA.Game)
        {
            Position = position;
        }
    }
}