using Microsoft.Xna.Framework.Graphics;

namespace AXNAEngine.com.axna.graphics.background
{
    public class ParallaxTexture
    {
        public Texture2D Texture;

        private float _x;
        public float X
        {
            get { return _x; }
            set
            {
                _x = value;
                if (_x < -Texture.Width)
                {
                    // Именно так, потому что нужны правила целочисленного деления
                    // Если обойтись формулой _x = _x - Texture.Width * _x / Texture.Width получим 0
                    float a = _x / Texture.Width;
                    float b = Texture.Width * a;

                    _x = _x - b;
                }
                if (_x > Texture.Width)
                {
                    float a = _x / Texture.Width;
                    float b = Texture.Width * a;

                    _x = _x - b;
                }
            }
        }

        public float Y;
        public float ParallaxSpeed;
        public bool IsStretchVertical;

        public ParallaxTexture(Texture2D texture, bool isStretchVertical = false, float parallaxSpeed = 1.0f, int x = 0, int y = 0)
        {
            Texture = texture;
            X = x;
            Y = y;
            ParallaxSpeed = parallaxSpeed;
            IsStretchVertical = isStretchVertical;
        }
    }
}
