using AXNAEngine.com.axna.graphics;

namespace AXNAEngine.com.axna.ui
{
    public class TextLabel : GameEntity
    {
        private readonly TextString _text;

        public TextLabel(TextString text, float x, float y, int width = 0, int height = 0)
            : base(text, x, y)
        {
            _text = text;
        }

        public void UpdateTextParameter(string text)
        {
            _text.Text = text;
        }
    }
}