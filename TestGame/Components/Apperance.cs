using Microsoft.Xna.Framework;

using System.Collections.Generic;


namespace TestGame.Components
{
    internal class Apperance
    {
        public List<Sprite> Sprites = new();
    }

    public class Sprite
    {
        public string TextureName;
        public Vector2 Offset;
        public Rectangle? SourceRectange;
        public Color Color = Color.White;
        public float Rotation = 0.0f;
        public float Scale = 1.0f;
    }
}
