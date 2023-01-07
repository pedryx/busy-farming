using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TestGame.Components;


namespace TestGame.UI
{
    internal abstract class UIControl
    {
        public Transform Transform;
        public Sprite Sprite;

        public abstract void Update();
        public virtual void Draw(SpriteBatch spriteBatch)
            => Sprite.Draw(spriteBatch, Transform);
    }
}
