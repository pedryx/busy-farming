using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;

using TestGame.Components;
using TestGame.Resources;


namespace TestGame.Systems
{
    internal class RenderSystem : EntityDrawSystem
    {
        private readonly SpriteBatch spriteBatch;
        private readonly SpriteManager spriteManager;
        private readonly Camera camera;

        private ComponentMapper<Transform> transformMapper;
        private ComponentMapper<Apperance> apperanceMapper;

        public RenderSystem(
            SpriteBatch spriteBatch,
            SpriteManager spriteManager,
            Camera camera
        )
            : base(Aspect.All(typeof(Transform), typeof(Apperance)))
        {
            this.spriteBatch = spriteBatch;
            this.spriteManager = spriteManager;
            this.camera = camera;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            transformMapper = mapperService.GetMapper<Transform>();
            apperanceMapper = mapperService.GetMapper<Apperance>();
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(transformMatrix: camera.GetTransform());

            foreach (var entity in ActiveEntities)
            {
                var transform = transformMapper.Get(entity);
                var apperance = apperanceMapper.Get(entity);

                foreach (var sprite in apperance.Sprites)
                {
                    var texture = spriteManager[sprite.TextureName];
                    var origin = sprite.SourceRectange == null ?
                        new Vector2(texture.Width / 2, texture.Height / 2) :
                        new Vector2(
                            sprite.SourceRectange.Value.Width / 2,
                            sprite.SourceRectange.Value.Height / 2
                        );

                    spriteBatch.Draw(
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

            spriteBatch.End();
        }
    }
}
