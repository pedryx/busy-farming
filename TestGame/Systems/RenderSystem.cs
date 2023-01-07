using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TestGame.Components;


namespace TestGame.Systems
{
    internal class RenderSystem : EntityDrawSystem<Transform, Apperance>
    {
        private readonly Camera camera;

        public RenderSystem(SpriteBatch spriteBatch, Camera camera)
            : base(spriteBatch)
        {
            this.camera = camera;
        }

        public override void PreDraw(GameTime gameTime)
            => SpriteBatch.Begin(transformMatrix: camera.GetTransform());

        public override void PostDraw(GameTime gameTime)
            => SpriteBatch.End();

        public override void Draw(Transform transform, Apperance apperance, GameTime gameTime)
        {
            foreach (var sprite in apperance.Sprites)
            {
                var origin = sprite.SourceRectange == null ?
                    new Vector2(sprite.texture.Width / 2, sprite.texture.Height / 2) :
                    new Vector2(
                        sprite.SourceRectange.Value.Width / 2,
                        sprite.SourceRectange.Value.Height / 2
                    );

                SpriteBatch.Draw(
                    sprite.texture,
                    transform.Position + sprite.Offset,
                    sprite.SourceRectange,
                    sprite.Color,
                    transform.Rotation + sprite.Rotation,
                    origin,
                    transform.Scale * sprite.Scale,
                    SpriteEffects.None,
                    0
                );
            }
        }
    }
}
