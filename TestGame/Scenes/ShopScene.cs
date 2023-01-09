using Microsoft.Xna.Framework;

using System.Collections.Generic;
using System.Linq;

using TestGame.Components;
using TestGame.Resources;
using TestGame.UI;


namespace TestGame.Scenes
{
    internal class ShopScene : Scene
    {
        private const int waterCanUpgradePrice = 100;
        private const int inventoryUpgradePrice = 100;

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
            CreateUpgradeButtons();
        }

        private void CreateUpgradeButtons()
        {
            var buttons = new List<Button>();
            var apperance = new Apperance()
            {
                Sprite = new Sprite()
                {
                    Texture = Game.SpriteManager["scrollsandblocks"],
                    SourceRectange = new Rectangle(0, 64, 96, 32),
                },
                Scale = new Vector2(2.5f),
            };
            var font = Game.FontManager[new FontDescriptor()
            {
                Name = "calibri",
                FontHeight = 32,
            }];
            float yPosition = windowSize.Y * 0.7f;

            buttons.Add(new Button()
            {
                Apperance = apperance,
                Font = font,
                Text = $"Water Can - {waterCanUpgradePrice}",
            });
            buttons.Last().Clicked += (sender, e) =>
            {
                System.Console.WriteLine("water can upgrade");
            };

            buttons.Add(new Button()
            {
                Apperance = apperance.Clone(),
                Font = font,
                Text = $"Inventory - {inventoryUpgradePrice}",
            });
            buttons.Last().Clicked += (sender, e) =>
            {
                System.Console.WriteLine("inventory upgrade");
            };

            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].Apperance.Position = new Vector2()
                {
                    X = (windowSize.X / (buttons.Count + 1)) * (i + 1) - buttons[i].Apperance.Size.X / 2,
                    Y = yPosition,
                };
                UILayer.AddElement(buttons[i]);
            }
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
                FontHeight = 20,
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

                    // create image for tile item
                    var type = PlantType.Types[typeID++];

                    var itemApperance = apperance.Clone();
                    itemApperance.Sprite = PlantUtils.CreatePlantSprite(type, PlantUtils.PlantStages - 1);
                    itemApperance.Scale = new Vector2((apperance.Size.Y * 0.5f) /
                        itemApperance.NotScaledSize.Y);
                    itemApperance.Center(apperance);
                    itemApperance.Position.Y -= apperance.Size.Y * 0.1f;

                    var image = new Image()
                    {
                        Apperance = itemApperance,
                    };
                    UILayer.AddElement(image);

                    // create price label
                    string text = $"{type.Price / 2}";
                    var size = font.MeasureString(text);
                    var position = apperance.Position + new Vector2()
                    {
                        X = apperance.Size.X / 2 - size.X / 2,
                        Y = apperance.Size.Y * 0.9f - size.Y,
                    };

                    var label = new Label()
                    {
                        Apperance = new Apperance()
                        {
                            Position = position,
                        },
                        Font = font,
                        Text = text,
                    };
                    UILayer.AddElement(label);

                    // buy option
                    button.Clicked += (sender, e) =>
                    {
                        if (inventory.Coins < type.Price / 2)
                            return;
                        inventory.Coins -= type.Price / 2;

                        int slot = inventory.GetItemSlotOrFreeSlot<SeedItem>(type.PlantID);
                        if (slot == -1)
                            return;

                        if (inventory.Slots[slot] == null || inventory.Slots[slot].Quantity == 0)
                        {
                            inventory.Slots[slot] = new SeedItem(type.PlantID)
                            {
                                Quantity = 1,
                                Sprite = PlantUtils.CreatePlantSprite(type, PlantUtils.PlantStages - 1),
                                Price = type.Price / 2,
                                Type = type,
                            };
                        }
                        else
                        {
                            inventory.Slots[slot].Quantity++;
                        }
                    };
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
