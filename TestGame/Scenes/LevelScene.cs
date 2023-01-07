using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;

using TestGame.Components;
using TestGame.Systems;
using TestGame.UI;

namespace TestGame.Scenes
{
    internal class LevelScene : Scene
    {
        protected override void CreateSystems()
        {
            Builder
                .AddSystem(new RenderSystem(Game.SpriteBatch, Game.Camera))
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

            PlantUtils.FarmPlotTexture = Game.SpriteManager["plowed_soil"];
            PlantUtils.AppendRow(farm);
            PlantUtils.AppendRow(farm);
            PlantUtils.AppendRow(farm);
        }

        protected override void CreateUI()
        {
            Button testButton = new Button()
            {
                Transform = new Transform()
                {
                    Position = new Vector2(100, 100),
                },
                Sprite = new Sprite()
                {
                    texture = Game.SpriteManager["scrollsandblocks"],
                    SourceRectange = new Rectangle(0, 64, 96, 32),
                },
            };
            testButton.Clicked += (sender, e) => {
                Console.WriteLine("test");
            };
            UILayer.AddControl(testButton);
        }
    }
}
