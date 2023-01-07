using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using System;


namespace TestGame.UI
{
    internal class Button : UIControl
    {
        private bool lastPressed = false;

        public event EventHandler Clicked;

        public Vector2 Position => Transform.Position + Sprite.Offset;

        public override void Update()
        {
            var state = Mouse.GetState();
            bool current = state.LeftButton == ButtonState.Pressed;

            if (!current && lastPressed)
            {
                float x = Transform.Position.X + Sprite.Offset.X;
                float y = Transform.Position.Y + Sprite.Offset.Y;
                var mousePosition = state.Position;

                if (
                    mousePosition.X > x &&
                    mousePosition.Y > y &&
                    mousePosition.X < x + Sprite.Width &&
                    mousePosition.Y < y + Sprite.Height
                )
                {
                    Clicked?.Invoke(this, new EventArgs());
                }
            }

            lastPressed = current;
        }
    }
}
