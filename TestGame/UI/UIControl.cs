using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TestGame.UI
{
    internal abstract class UIControl
    {
        public Vector2 Position;
        public float Scale = 1.0f;

        public abstract void Update();
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
