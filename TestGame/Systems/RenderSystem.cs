using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TestGame.Components;


namespace TestGame.Systems
{
    internal class RenderSystem : EntityDrawSystem<Apperance>
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

        public override void Draw(Apperance apperance, GameTime gameTime)
        {
            foreach (var sprite in apperance.Sprites)
            {
                sprite.Draw(SpriteBatch);
            }
        }
    }
}
