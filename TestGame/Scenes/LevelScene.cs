using Microsoft.Xna.Framework;

using System.Collections.Generic;

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
                 ))
                .AddSystem(new CameraControlSystem(Game.Camera));
        }

        protected override void CreateEntities()
        {
            CreateFarm();
        }

        private void CreateFarm()
        {
            var farm = World.CreateEntity();
            farm.Attach(new Transform());
            farm.Attach(new Inventory());
            farm.Attach(new Apperance());
            farm.Attach(new FarmPlots());

            PlantUtils.AppendRow(farm);
            PlantUtils.AppendRow(farm);
            PlantUtils.AppendRow(farm);
        }

        protected override void CreateUI()
        {

        }
    }
}
