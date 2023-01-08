#define SCREENSHOT

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

        private Vector2 windowSize;
        private InventoryUI inventoryUI;
        private CoinsCounter moneyCounter;

        private readonly ShopScene shopScene = new();

        public Inventory Inventory { get; private set; }

        public override void Initialize(LDGame game)
        {
            windowSize = new Vector2()
            {
                X = game.Graphics.PreferredBackBufferWidth,
                Y = game.Graphics.PreferredBackBufferHeight,
            };

            base.Initialize(game);
            shopScene.ShareUI(inventoryUI);
            shopScene.ShareUI(moneyCounter);
            shopScene.SetInvetory(Inventory);
            shopScene.Initialize(game);
        }

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

#if SCREENSHOT
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
                    Price = PlantType.Types[i].Price / 2,
                };
                Inventory.Slots[i].Quantity = 10;
            }
            PlantSystem.PlantDecay = false;
#else
            var carrotType = PlantType.GetType("carrot");
            Inventory.Slots[0] = new SeedItem(carrotType.PlantID)
            {
                Quantity = 4,
                Type = carrotType,
                Sprite = PlantUtils.CreatePlantSprite(carrotType, PlantUtils.PlantStages - 1),
                Price = carrotType.Price / 2,
            };
            Inventory.Slots[0].Quantity = 4;
#endif
        }

        private void CreateFarm()
        {
            for (int i = 0; i < InitialFarmColumns; i++)
                PlantUtils.CreateFarmColumn(World);
        }

        protected override void CreateUI()
        {
            CreateInventoryUI();
            CreateMoneyCounter();
            CreateShopButton();
            CreateLake();
        }

        private void CreateLake()
        {
            var lake = new Button()
            {
                Apperance = new Apperance()
                {
                    Sprite = new Sprite()
                    {
                        Texture = Game.SpriteManager["terrain_atlas"],
                        SourceRectange = new Rectangle(192, 352, 96, 96),
                    },
                    Scale = new Vector2(3),
                },
            };

            lake.Apperance.Position = new Vector2()
            {
                X = -lake.Apperance.Size.X * 0.4f,
                Y = windowSize.Y / 2 - lake.Apperance.Size.Y / 2,
            };

            UILayer.AddElement(lake);
        }

        private void CreateInventoryUI()
        {
            // create UI
            inventoryUI = new(Inventory)
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

            inventoryUI.Apperance.Position = new Vector2()
            {
                X = windowSize.X / 2 - (inventoryUI.Apperance.Size.X * InitialInventorySize) / 2,
                Y = windowSize.Y * 0.99f - inventoryUI.Apperance.Size.Y,
            };

            inventoryUI.Clicked += (sender, e) =>
            {
                if (Game.CurrentScene is LevelScene)
                {
                    e.Inventory.Selected = e.Slot;
                }
                else if (Game.CurrentScene is ShopScene)
                {
                    shopScene.SellItem(e.Inventory.Slots[e.Slot]);
                }
            };

            UILayer.AddElement(inventoryUI);
        }

        private void CreateMoneyCounter()
        {
            moneyCounter = new CoinsCounter(Inventory)
            {
                Apperance = new Apperance(),
                Font = Game.FontManager[new FontDescriptor()
                {
                    Name = "calibri",
                    FontHeight = 32,
                }],
                Text = "Coins: 0000"
            };

            moneyCounter.Apperance.Position = new Vector2()
            {
                X = windowSize.X * 0.01f,
                Y = windowSize.Y * 0.95f - moneyCounter.Size.Y,
            };

            UILayer.AddElement(moneyCounter);
        }

        private void CreateShopButton()
        {
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
                X = windowSize.X * 0.98f - button.Apperance.Size.X,
                Y = windowSize.Y * 0.03f,
            };

            button.Clicked += (sender, e) =>
            {
                Inventory.Selected = -1;
                Game.PushScene(shopScene);
            };

            UILayer.AddElement(button);
        }
    }
}
