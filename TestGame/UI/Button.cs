using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;


namespace TestGame.UI
{
    internal class Button : UIControl
    {
        private bool lastPressed = false;

        public Sprite Sprite { get; set; }

        public event EventHandler Clicked;

        public Vector2 Position => Transform.Position + Sprite.Offset;

        public float Width => Sprite.SourceRectange.HasValue ?
            Sprite.SourceRectange.Value.Width :
            Sprite.texture.Width;

        public float Height => Sprite.SourceRectange.HasValue ?
            Sprite.SourceRectange.Value.Height :
            Sprite.texture.Height;

        public override void Update()
        {
            var state = Mouse.GetState();
            bool current = state.LeftButton == ButtonState.Pressed;

            if (!current && lastPressed)
            {
                var position = state.Position;
                if (
                    position.X > Position.X &&
                    position.Y > Position.Y &&
                    position.X < Position.X + Width &&
                    position.Y < Position.Y + Height
                )
                {
                    Clicked?.Invoke(this, new EventArgs());
                }
            }

            lastPressed = current;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                Sprite.texture,
                Transform.Position + Sprite.Offset,
                Sprite.SourceRectange,
                Sprite.Color,
                Transform.Rotation + Sprite.Rotation,
                Vector2.Zero,
                Transform.Scale * Sprite.Scale,
                SpriteEffects.None,
                0
            );
        }
    }
}
