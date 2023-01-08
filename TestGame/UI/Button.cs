using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;


namespace TestGame.UI
{
    internal class Button : UIControl
    {
        public event EventHandler Clicked;

        public SpriteFont Font;
        public string Text;

        public override void Update()
        {
            if (Input.LeftClickOn(Apperance))
                Clicked?.Invoke(this, EventArgs.Empty);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            Vector2 size = Font.MeasureString(Text);
            Vector2 position = Apperance.Position + Apperance.Size / 2 - size / 2;
            spriteBatch.DrawString(Font, Text, position, Color.Black);
        }
    }
}
