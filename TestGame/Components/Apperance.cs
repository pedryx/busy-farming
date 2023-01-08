using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace TestGame.Components
{
    internal class Apperance
    {
        public Vector2 Position;
        public Vector2 Scale = Vector2.One;
        public Sprite Sprite;
        public bool Static = false;
        public float Layer = 0;

        public Vector2 NotScaledSize
        {
            get
            {
                if (Sprite.SourceRectange == null)
                {
                    return new Vector2(Sprite.Texture.Width, Sprite.Texture.Height);
                }
                else
                {
                    return new Vector2()
                    {
                        X = Sprite.SourceRectange.Value.Width,
                        Y = Sprite.SourceRectange.Value.Height,
                    };
                }
            }
        }

        public Vector2 Size => Scale * NotScaledSize;

        public Rectangle Rectangle => new(Position.ToPoint(), Size.ToPoint());

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                Sprite.Texture,
                Position,
                Sprite.SourceRectange,
                Sprite.Color,
                0,
                Vector2.Zero,
                Scale,
                SpriteEffects.None,
                Layer
            );
        }

        public Apperance Clone()
        {
            var clone = (Apperance)MemberwiseClone();
            clone.Sprite = Sprite.Clone();

            return clone;
        }

        public void Center(Apperance other)
        {
            Position = other.Position + other.Size / 2 - Size / 2;
        }
    }
}
