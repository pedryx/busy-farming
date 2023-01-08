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
                .AddSystem(new PlantPlacementSystem(Game.SpriteBatch, Game.Camera, this))
                .AddSystem(new PlantSystem(this));
        }

        protected override void CreateEntities()
        {
            PlantUtils.FarmPlotTexture = Game.SpriteManager["plowed_soil"];
            PlantUtils.PlantsTexture = Game.SpriteManager["crops2"];
            PlantUtils.CalcFarmPosition(Game.Graphics);

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
                Inventory.Slots[i] = new SeedItem(PlantType.Types[i].PlantID)
                {
                    Quantity = 10,
                    Type = PlantType.Types[i],
                    Sprite = PlantUtils.CreatePlantSprite(
                        PlantType.Types[i],
                        PlantUtils.PlantStages - 1
                    ),
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
            CreateInventoryUI();
            CreateShopButton();
        }

        private void CreateInventoryUI()
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
                        SourceRectange = new Rectangle(96, 128, 96, 96),
                        Color = new Color(1.0f, 1.0f, 1.0f, 0.7f),
                    },
                },
                ClickedSprite = new Sprite()
                {
                    Texture = Game.SpriteManager["scrollsandblocks"],
                    SourceRectange = new Rectangle(0, 128, 96, 96),
                    Color = new Color(1.0f, 1.0f, 1.0f, 0.7f),
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
                Y = screenHeight * 0.99f - inventoryUI.Apperance.Size.Y,
            };

            UILayer.AddControl(inventoryUI);
        }

        private void CreateShopButton()
        {
            float screenWidth = Game.Graphics.PreferredBackBufferWidth;
            float screenHeight = Game.Graphics.PreferredBackBufferHeight;

            var button = new Button()
            {
                Apperance = new Apperance()
                {
                    Sprite = new Sprite()
                    {
                        Texture = Game.SpriteManager["scrollsandblocks"],
                        SourceRectange = new Rectangle(0, 64, 96, 32),
                    },
                },
                Font = Game.FontManager[new FontDescriptor()
                {
                    Name = "calibri",
                    FontHeight = 18,
                }],
                Text = "SHOP",
            };

            button.Apperance.Position = new Vector2()
            {
                X = screenWidth * 0.97f - button.Apperance.Size.X,
                Y = screenHeight * 0.03f,
            };

            button.Clicked += (sender, e) =>
            {
                System.Console.WriteLine("SHOP");
            };

            UILayer.AddControl(button);
        }
    }
}
