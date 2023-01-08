using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace TestGame.UI
{
    internal class Label : UIElement
    {
        public SpriteFont Font;
        public string Text;
        public Color Color = Color.Black;

        public Vector2 Size => Font.MeasureString(Text);

        public override void Draw(SpriteBatch spriteBatch)
            => spriteBatch.DrawString(Font, Text, Apperance.Position, Color);
    }
}
