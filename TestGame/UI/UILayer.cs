using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System.Collections.Generic;


namespace TestGame.UI
{
    internal class UILayer
    {
        private readonly List<UIElement> controls = new();

        public void AddElement(UIElement control)
        {
            controls.Add(control);
        }

        public void Update(GameTime gameTime)
        {
            foreach (var control in controls)
            {
                if (control.Enabled)
                    control.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin();
            foreach (var control in controls)
            {
                control.Draw(spriteBatch, gameTime);
            }
            spriteBatch.End();
        }
    }
}
