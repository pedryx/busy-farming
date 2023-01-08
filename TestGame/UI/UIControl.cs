using Microsoft.Xna.Framework.Graphics;

using TestGame.Components;


namespace TestGame.UI
{
    internal abstract class UIControl
    {
        public Apperance Apperance;

        public abstract void Update();
        public virtual void Draw(SpriteBatch spriteBatch)
            => Apperance.Draw(spriteBatch);
    }
}
