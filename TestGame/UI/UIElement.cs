using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TestGame.Components;


namespace TestGame.UI
{
    internal abstract class UIElement
    {
        public Apperance Apperance;

        public bool Enabled = true;
        public bool Visible = true;

        public virtual void Update(GameTime gameTime) { }
        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
            => Apperance.Draw(spriteBatch);
    }
}
