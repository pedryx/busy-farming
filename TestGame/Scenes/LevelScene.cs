using Microsoft.Xna.Framework;

using MonoGame.Extended.Entities;

using TestGame.Components;
using TestGame.Resources;
using TestGame.Systems;
using TestGame.UI;


namespace TestGame.Scenes
{
    internal class LevelScene : Scene
    {
        private const int InitialInventorySize = 10;
        private const int InitialFarmColumns = 10;

        public Inventory Inventory { get; private set; }

        protected override void CreateSystems()
        {
            Builder
                .AddSystem(new RenderSystem(Game.SpriteBatch, Game.Camera))
                .AddSystem(new PlantPlacementSystem(Game.SpriteBatch, this))
                .AddSystem(new PlantSystem());
        }

        protected override void CreateEntities()
        {
            PlantUtils.FarmPlotTexture = Game.SpriteManager["plowed_soil"];
            PlantUtils.PlantsTexture = Game.SpriteManager["crops2"];

            CreateFarm();
            CreateInventory();
        }

        private void CreateInventory()
        {
            Inventory = new Inventory();
            for (int i = 0; i < InitialInventorySize; i++)
                Inventory.Slots.Add(null);

            var inventoryEntity = World.CreateEntity();
            inventoryEntity.Attach(Inventory);

            for (int i = 0; i < PlantType.Types.Count; i++)
            {
                Inventory.Slots[i] = new SeedItem()
                {
                    Quantity = 10,
                    Type = PlantType.Types[i],
                    Sprite = PlantType.Types[i].Sprite
                };
                Inventory.Slots[i].Quantity = 10;
            }
        }

        private void CreateFarm()
        {
            for (int i = 0; i < InitialFarmColumns; i++)
                PlantUtils.CreateFarmColumn(World);
        }

        protected override void CreateUI()
        {
            float screenWidth = Game.Graphics.PreferredBackBufferWidth;
            float screenHeight = Game.Graphics.PreferredBackBufferHeight;

            // create UI
            InventoryUI inventoryUI = new(Inventory)
            {
                Apperance = new Apperance()
                {
                    Sprite = new Sprite()
                    {
                        Texture = Game.SpriteManager["scrollsandblocks"],
                        SourceRectange = new Rectangle(96, 224 - 96, 96, 96),
                        Color = new Color(1.0f, 1.0f, 1.0f, 0.7f),
                    },
                },
                Font = Game.FontManager[new FontDescriptor()
                {
                    Name = "calibri",
                    FontHeight = 32,
                }],
            };

            // center
            inventoryUI.Apperance.Position = new Vector2()
            {
                X = screenWidth / 2 - (inventoryUI.Apperance.Size.X * InitialInventorySize) / 2,
                Y = screenHeight - inventoryUI.Apperance.Size.Y,
            };

            UILayer.AddControl(inventoryUI);
        }
    }
}
