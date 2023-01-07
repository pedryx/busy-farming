using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using System;


namespace TestGame.UI
{
    internal class Button : UIControl
    {
        private bool lastPressed = false;

        public event EventHandler Clicked;

        public Vector2 Position => Sprite.Position;

        public override void Update()
        {
            var state = Mouse.GetState();
            bool current = state.LeftButton == ButtonState.Pressed;

            if (!current && lastPressed)
            {
                float x = Sprite.Position.X;
                float y = Sprite.Position.Y;
                var mousePosition = state.Position;

                if (
                    mousePosition.X > x &&
                    mousePosition.Y > y &&
                    mousePosition.X < x + Sprite.Size.X &&
                    mousePosition.Y < y + Sprite.Size.Y
                )
                {
                    Clicked?.Invoke(this, new EventArgs());
                }
            }

            lastPressed = current;
        }
    }
}
