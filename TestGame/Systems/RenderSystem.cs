using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TestGame.Components;


namespace TestGame.Systems
{
    internal class RenderSystem : EntityDrawSystem<Apperance>
    {
        private readonly Camera camera;

        private bool staticDraw;

        public RenderSystem(SpriteBatch spriteBatch, Camera camera)
            : base(spriteBatch)
        {
            this.camera = camera;
        }

        public override void Draw(GameTime gameTime)
        {
            // non static render
            staticDraw = false;
            SpriteBatch.Begin(transformMatrix: camera.GetTransform());
            base.Draw(gameTime);
            SpriteBatch.End();

            // static render
            staticDraw = true;
            SpriteBatch.Begin();
            base.Draw(gameTime);
            SpriteBatch.End();
        }


        public override void Draw(Apperance apperance, GameTime gameTime)
        {
            if (apperance.Static != staticDraw)
                return;

            apperance.Draw(SpriteBatch);
        }
    }
}
