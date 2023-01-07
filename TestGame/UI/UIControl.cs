using Microsoft.Xna.Framework.Graphics;

namespace TestGame.UI
{
    internal abstract class UIControl
    {
        public abstract void Update();
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
