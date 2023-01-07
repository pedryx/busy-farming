using Microsoft.Xna.Framework;

using System;

using TestGame.Components;
using TestGame.Systems;
using TestGame.UI;


namespace TestGame.Scenes
{
    internal class LevelScene : Scene
    {
        private const int InitialInventorySize = 4;

        private Inventory inventory;

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

            inventory = new();
            for (int i = 0; i < InitialInventorySize; i++)
                inventory.Slots.Add(new InventorySlot());
            farm.Attach(inventory);
        }

        protected override void CreateUI()
        {
            float screenWidth = Game.Graphics.PreferredBackBufferWidth;
            float screenHeight = Game.Graphics.PreferredBackBufferHeight;

            InventoryUI inventoryUI = new(inventory)
            {
                Transform = new Transform(),
                Sprite = new Sprite()
                {
                    texture = Game.SpriteManager["scrollsandblocks"],
                    SourceRectange = new Rectangle(0, 224, 96, 96),
                    Scale = 0.5f,
                },
            };
            inventoryUI.Transform.Position = new Vector2()
            {
                X = screenWidth / 2 - inventoryUI.Width / 2,
                Y = screenHeight - inventoryUI.Height,
            };
            UILayer.AddControl(inventoryUI);

            Button testButton = new()
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
