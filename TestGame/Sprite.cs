using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace TestGame
{
    internal class Sprite
    {
        public Texture2D Texture;
        public Vector2 Position;
        public Rectangle? SourceRectange;
        public Color Color = Color.White;
        public float Rotation = 0.0f;
        public float Scale = 1.0f;

        public Vector2 Size => Scale * NotScaledSize;

        public Vector2 NotScaledSize => new()
        {
            X = SourceRectange.HasValue ? SourceRectange.Value.Width : Texture.Width,
            Y = SourceRectange.HasValue ? SourceRectange.Value.Height : Texture.Height,
        };

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                Texture,
                Position,
                SourceRectange,
                Color,
                Rotation,
                Vector2.Zero,
                Scale,
                SpriteEffects.None,
                0
            );
        }
    }
}
