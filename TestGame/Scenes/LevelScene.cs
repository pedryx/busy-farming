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
        private const float time = 2 * 60;
        private const int initialInventorySize = 3;
        private const int initialFarmColumns = 9;

        private Vector2 windowSize;
        private InventoryUI inventoryUI;
        private MiscCounter miscCounter;

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
            shopScene.ShareUI(miscCounter);
            shopScene.SetInvetory(Inventory);
            shopScene.Initialize(game);
        }

        protected override void CreateSystems()
        {
            var weedSprite = new Sprite()
            {
                Texture = Game.SpriteManager["tallgrass"],
                SourceRectange = new Rectangle(0, 160, 32, 32)
            };

            Builder
                .AddSystem(new RenderSystem(Game.SpriteBatch, Game.Camera))
                .AddSystem(new PlantPlacementSystem(Game.SpriteBatch, Game.Camera, this))
                .AddSystem(new PlantSystem(this))
                .AddSystem(new ProgressBarRenderSystem(Game))
                .AddSystem(new WeedSpawnSystem(weedSprite))
                .AddSystem(new WeatherSystem(Game));
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
            for (int i = 0; i < initialInventorySize; i++)
                Inventory.Slots.Add(null);
            Inventory.MaxWater = 1;
            Inventory.CurrentWater = 1;

            var inventoryEntity = World.CreateEntity();
            inventoryEntity.Attach(Inventory);

            Inventory.Slots[0] = new WaterCan(-1)
            {
                Quantity = 1,
                Sprite = new Sprite()
                {
                    Texture = Game.SpriteManager["water_can"],
                },
            };

            var carrotType = PlantType.GetType("carrot");
            Inventory.Slots[1] = new SeedItem(carrotType.PlantID)
            {
                Quantity = 4,
                Type = carrotType,
                Sprite = PlantUtils.CreatePlantSprite(carrotType, PlantUtils.PlantStages - 1),
                Price = carrotType.Price / 2,
            };
            Inventory.Slots[1].Quantity = 4;
        }

        private void CreateFarm()
        {
            for (int i = 0; i < initialFarmColumns; i++)
                PlantUtils.CreateFarmColumn(World);
        }

        protected override void CreateUI()
        {
            CreateInventoryUI();
            CreateMiscCounter();
            CreateShopButton();
            CreateLake();
            CreateTimer();
        }

        private void CreateTimer()
        {
            var timer = new Timer(time)
            {
                Apperance = new Apperance(),
                Font = Game.FontManager[new FontDescriptor()
                {
                    Name = "calibri",
                    FontHeight = 48,
                }],
            };

            timer.Apperance.Position = new Vector2()
            {
                X = windowSize.X / 2 - timer.Size.X / 2,
                Y = windowSize.Y * 0.015f,
            };

            timer.TimeUp += (sender, e) =>
            {
                Game.PushScene(new GameOverScene(Inventory.Coins));
                Game.CurrentScene.Initialize(Game);
            };

            UILayer.AddElement(timer);
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

            lake.Clicked += (sender, e) =>
            {
                if (Inventory.SelectedItem is WaterCan)
                    Inventory.CurrentWater = Inventory.MaxWater;
            };

            UILayer.AddElement(lake);
        }

        private void CreateInventoryUI()
        {
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
                X = windowSize.X / 2 - (inventoryUI.Apperance.Size.X * initialInventorySize) / 2,
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

            Inventory.ui = inventoryUI;
            UILayer.AddElement(inventoryUI);
        }

        private void CreateMiscCounter()
        {
            miscCounter = new MiscCounter(Inventory)
            {
                Apperance = new Apperance(),
                Font = Game.FontManager[new FontDescriptor()
                {
                    Name = "calibri",
                    FontHeight = 28,
                }],
                Text = "Coins: 0000\nWater: 0000/0000",
            };

            miscCounter.Apperance.Position = new Vector2()
            {
                X = windowSize.X * 0.01f,
                Y = windowSize.Y * 0.95f - miscCounter.Size.Y,
            };

            UILayer.AddElement(miscCounter);
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
