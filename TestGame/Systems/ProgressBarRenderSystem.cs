using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TestGame.Components;

namespace TestGame.Systems
{
    internal class ProgressBarRenderSystem : EntityDrawSystem<Apperance, ProgressBar>
    {
        private readonly Camera camera;
        private readonly Texture2D texture;

        public ProgressBarRenderSystem(
            SpriteBatch spriteBatch,
            Camera camera, 
            GraphicsDevice device
        ) 
            : base(spriteBatch)
        {
            this.camera = camera;

            texture = new Texture2D(device, 1, 1);
            texture.SetData(new Color[] { Color.White });
        }

        public override void PreDraw(GameTime gameTime)
            => SpriteBatch.Begin(transformMatrix: camera.GetTransform());

        public override void PostDraw(GameTime gameTime)
            => SpriteBatch.End();

        public override void Draw(Apperance apperance, ProgressBar progressBar, GameTime gameTime)
        {
            float relativeWidth = 0.9f;
            float relativeHeight = 0.05f;

            var position = apperance.Position + new Vector2()
            {
                X = apperance.Size.X * (1.0f - relativeWidth),
                Y = apperance.Size.Y * (1.0f - relativeHeight),
            };
            Vector2 scale = apperance.Size * new Vector2(relativeWidth, relativeHeight);

            SpriteBatch.Draw(
                texture,
                position,
                null,
                Color.White,
                0,
                Vector2.Zero,
                scale,
                SpriteEffects.None,
                0
            );

            scale.X *= (progressBar.CurrentValue - progressBar.StartValue) /
                (progressBar.EndValue - progressBar.StartValue);
            SpriteBatch.Draw(
                texture,
                position,
                null,
                progressBar.Color,
                0,
                Vector2.Zero,
                scale,
                SpriteEffects.None,
                0
            );
        }
    }
}
