﻿using Microsoft.Xna.Framework.Graphics;

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

        public void Update()
        {
            foreach (var control in controls)
            {
                if (control.Enabled)
                    control.Update();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            foreach (var control in controls)
            {
                if (control.Visible)
                    control.Draw(spriteBatch);
            }
            spriteBatch.End();
        }
    }
}
