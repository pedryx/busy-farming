using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TestGame.Components;
using TestGame.Resources;


namespace TestGame.Systems
{
    internal class RenderSystem : EntityDrawSystem<Transform, Apperance>
    {
        private readonly SpriteManager spriteManager;
        private readonly Camera camera;

        public RenderSystem(
            SpriteBatch spriteBatch,
            SpriteManager spriteManager,
            Camera camera
        )
            : base(spriteBatch)
        {
            this.spriteManager = spriteManager;
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
                var texture = spriteManager[sprite.TextureName];
                var origin = sprite.SourceRectange == null ?
                    new Vector2(texture.Width / 2, texture.Height / 2) :
                    new Vector2(
                        sprite.SourceRectange.Value.Width / 2,
                        sprite.SourceRectange.Value.Height / 2
                    );

                SpriteBatch.Draw(
                    texture,
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
