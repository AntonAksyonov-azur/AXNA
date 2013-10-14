using Microsoft.Xna.Framework;

namespace AXNAEngine.com.axna.tile.engine
{
    public class TileMapCamera
    {
        public Vector2 Location = Vector2.Zero;
        public int SquaresAcross;
        public int SquaresDown;

        public TileMapCamera(int squaresAcross = 10, int squaresDown = 10)
        {
            SquaresAcross = squaresAcross;
            SquaresDown = squaresDown;
        }
    }
}