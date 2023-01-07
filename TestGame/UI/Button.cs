using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;


namespace TestGame.UI
{
    internal class Button : UIControl
    {
        private bool lastPressed = false;

        public Texture2D Texture { get; set; }
        public float Width => 96 * Scale;
        public float Height => 32 * Scale;

        public event EventHandler Clicked;

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
                Texture,
                Position,
                new Rectangle(0, 64, 96, 32),
                Color.White,
                0.0f,
                Vector2.Zero,
                Scale,
                SpriteEffects.None,
                0
            );
        }
    }
}
