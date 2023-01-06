using Microsoft.Xna.Framework;

using TestGame.Components;
using TestGame.Systems;


namespace TestGame.Scenes
{
    internal class LevelScene : Scene
    {
        protected override void CreateSystems()
        {
            Builder
                .AddSystem(new RenderSystem(
                    Game.SpriteBatch,
                    Game.SpriteManager,
                    Game.Camera
                 ));
        }

        protected override void CreateEntities()
        {
            var farmer = World.CreateEntity();
            farmer.Attach(new Transform()
            {
                Position = new Vector2(100, 100),
            });
            farmer.Attach(new Apperance()
            {
                Sprites = new()
                {
                    new Sprite()
                    {
                        TextureName = "body_male_walk",
                        SourceRectange = new Rectangle(0, 64 * 2, 64, 64),
                        Scale = 2.0f,
                    }
                },
            });

            Game.Camera.Target = farmer.Get<Transform>();
        }
    }
}
