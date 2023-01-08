using Microsoft.Xna.Framework;

using TestGame.Components;
using TestGame.Resources;
using TestGame.UI;


namespace TestGame.Scenes
{
    internal class ShopScene : Scene
    {
        private const int rowSize = 7;
        private const int columnSize = 4;
        private const float shopScale = 1.0f;

        private Inventory inventory;
        private Vector2 windowSize;

        public void SetInvetory(Inventory inventory) => this.inventory = inventory;

        public override void Initialize(LDGame game)
        {
            windowSize = new Vector2()
            {
                X = game.Graphics.PreferredBackBufferWidth,
                Y = game.Graphics.PreferredBackBufferHeight,
            };

            base.Initialize(game);
        }

        protected override void CreateUI()
        {
            CreateLeaveButton();
            CreateShoppingWindow();
        }

        private void CreateShoppingWindow()
        {
            var tileApperance = new Apperance()
            {
                Scale = new Vector2(shopScale),
                Sprite = new Sprite()
                {
                    Texture = Game.SpriteManager["scrollsandblocks"],
                    SourceRectange = new Rectangle(96, 128, 96, 96),
                    Color = new Color(1.0f, 1.0f, 1.0f, 0.7f),
                },
            };
            tileApperance.Position = new Vector2()
            {
                X = windowSize.X / 2 - (tileApperance.Size.X * rowSize) / 2,
                Y = windowSize.Y * 0.03f,
            };

            var font = Game.FontManager[new FontDescriptor()
            {
                Name = "calibri",
                FontHeight = 16,
            }];

            int typeID = 0;

            for (int y = 0; y < columnSize; y++)
            {
                for (int x = 0; x < rowSize; x++)
                {
                    // create button tile
                    var apperance = tileApperance.Clone();
                    apperance.Position += new Vector2(x, y) * apperance.Size;

                    var button = new Button()
                    {
                        Apperance = apperance,
                        Font = font,
                        Text = "",
                    };
                    UILayer.AddElement(button);

                    if (typeID >= PlantType.Types.Count)
                        continue;

                    // create image for tile
                    var type = PlantType.Types[typeID++];

                    var itemApperance = apperance.Clone();
                    itemApperance.Sprite = PlantUtils.CreatePlantSprite(type, PlantUtils.PlantStages - 1);
                    itemApperance.Scale = new Vector2((apperance.Size.Y * 0.8f) /
                        itemApperance.NotScaledSize.Y);
                    itemApperance.Center(apperance);

                    var image = new Image()
                    {
                        Apperance = itemApperance,
                    };
                    UILayer.AddElement(image);
                }
            }
        }

        private void CreateLeaveButton()
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
                Text = "LEAVE",
            };

            button.Apperance.Position = new Vector2()
            {
                X = windowSize.X * 0.97f - button.Apperance.Size.X,
                Y = windowSize.Y * 0.03f,
            };

            button.Clicked += (sender, e) => Game.PopScene();

            UILayer.AddElement(button);
        }

        public void SellItem(Item item)
        {
            inventory.Coins += item.Price;
            item.Quantity--;
        }
    }
}
