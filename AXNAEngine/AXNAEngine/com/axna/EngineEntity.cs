using AXNAEngine.com.axna.worlds;
using Microsoft.Xna.Framework;

namespace AXNAEngine.com.axna
{
    public class EngineEntity : DrawableGameComponent
    {
        public World ParentWorld;

        public EngineEntity() : base(AXNA.Game)
        {
        }
    }
}