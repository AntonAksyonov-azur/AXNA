using AXNAEngine.com.axna.entity;
using AXNAEngine.com.axna.graphics;
using Microsoft.Xna.Framework;

namespace AXNAEngine.com.axna.ui
{
    public class TextLabel : GraphicEntity
    {
        private readonly TextString _text;

        public TextLabel(TextString text, float x, float y)
            : base(text, x, y)
        {
            _text = text;
        }

        public void UpdateTextParameter(string text, Color color)
        {
            _text.Text = text;
            _text.OverColor = color;
        }
    }
}