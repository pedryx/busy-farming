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

        public float Width => SourceRectange.HasValue ?
            SourceRectange.Value.Width :
            texture.Width;

        public float Height => SourceRectange.HasValue ?
            SourceRectange.Value.Height :
            texture.Height;

        public void Draw(SpriteBatch spriteBatch, Transform transform)
        {
            var origin = SourceRectange == null ?
            new Vector2(texture.Width / 2, texture.Height / 2) :
            new Vector2(
                SourceRectange.Value.Width / 2,
                SourceRectange.Value.Height / 2
            );

            spriteBatch.Draw(
                texture,
                transform.Position + Offset,
                SourceRectange,
                Color,
                transform.Rotation + Rotation,
                origin,
                transform.Scale * Scale,
                SpriteEffects.None,
                0
            );
        }
    }
}
