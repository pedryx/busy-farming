using Microsoft.Xna.Framework.Graphics;


namespace TestGame.UI
{
    internal abstract class UIControl
    {
        public Sprite Sprite;

        public abstract void Update();
        public virtual void Draw(SpriteBatch spriteBatch)
            => Sprite.Draw(spriteBatch);
    }
}
