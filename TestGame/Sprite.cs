using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace TestGame
{
    internal class Sprite
    {
        public Texture2D Texture;
        public Rectangle? SourceRectange;
        public Color Color = Color.White;

        public Sprite Clone()
            => (Sprite)MemberwiseClone();
    }
}
