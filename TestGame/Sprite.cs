using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TestGame.Components;

namespace TestGame
{
    internal class Sprite
    {
        public Texture2D texture;
        public Vector2 Offset;
        public Rectangle? SourceRectange;
        public Color Color = Color.White;
        public float Rotation = 0.0f;
        public float Scale = 1.0f;

        public float Width => Scale * (SourceRectange.HasValue ?
            SourceRectange.Value.Width :
            texture.Width);

        public float Height => Scale * (SourceRectange.HasValue ?
            SourceRectange.Value.Height :
            texture.Height);

        public void Draw(SpriteBatch spriteBatch, Transform transform)
        {
            spriteBatch.Draw(
                texture,
                transform.Position + Offset,
                SourceRectange,
                Color,
                transform.Rotation + Rotation,
                Vector2.Zero,
                transform.Scale * Scale,
                SpriteEffects.None,
                0
            );
        }
    }
}
