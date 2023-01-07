using Microsoft.Xna.Framework.Graphics;

using TestGame.Components;


namespace TestGame.UI
{
    internal abstract class UIControl
    {
        public Transform Transform;

        public abstract void Update();
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
