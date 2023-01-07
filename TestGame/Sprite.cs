using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace TestGame
{
    public class Sprite
    {
        public Texture2D texture;
        public Vector2 Offset;
        public Rectangle? SourceRectange;
        public Color Color = Color.White;
        public float Rotation = 0.0f;
        public float Scale = 1.0f;
    }
}
