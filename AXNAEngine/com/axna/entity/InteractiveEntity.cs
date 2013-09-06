using System.Collections;
using AXNAEngine.com.axna.graphics;
using AXNAEngine.com.axna.managers;
using Microsoft.Xna.Framework;

namespace AXNAEngine.com.axna.entity
{
    public class InteractiveEntity : GameEntity
    {
        public InteractiveEntity(Graphic graphic, float x, float y) : base(graphic, x, y)
        {
        }

        public bool CollideMouse()
        {
            Rectangle thisRectange = GetEntityRectangle();
            Point mousePosition = InputManager.GetMousePositionToPoint();

            return thisRectange.Contains(mousePosition.X, mousePosition.Y);
        }

        public bool IsMouseClick()
        {
            return CollideMouse() && InputManager.IsMouseLeftClick();
        }
    }
}
