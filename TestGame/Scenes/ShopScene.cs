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
        private const int upgradesPrice = 10;

        private const float wateringUpgradeStep = 0.1f;
        private const int wateringUpgradePrice = upgradesPrice;

        private const float plantOvergrownUpgradeStep = 0.1f;
        private const int plantOvergrownUpragePrice = upgradesPrice;

        private const float rainChanceUpgradeStep = 0.1f;
        private const float maxRainChangeUpgrade = 0.7f;
        private const int rainChanceUpgradePrice = upgradesPrice;

        private const int waterCanUpgradePrice = upgradesPrice;

        private const int maxInventorySlots = 10;
        private const int inventoryUpgradePrice = upgradesPrice;

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
                Scale = new Vector2(2.0f),
            };
            var font = Game.FontManager[new FontDescriptor()
            {
                Name = "calibri",
                FontHeight = 24,
            }];
            float yPosition = windowSize.Y * 0.7f;

            // water can upgrade
            buttons.Add(new Button()
            {
                Apperance = apperance,
                Font = font,
                Text = $"Water Can - {waterCanUpgradePrice}",
            });
            buttons.Last().Clicked += (sender, e) =>
            {
                if (inventory.Coins < waterCanUpgradePrice)
                    return;

                inventory.Coins -= waterCanUpgradePrice;
                inventory.MaxWater++;
            };

            // inventory uprade
            buttons.Add(new Button()
            {
                Apperance = apperance.Clone(),
                Font = font,
                Text = $"Inventory - {inventoryUpgradePrice}",
            });
            buttons.Last().Clicked += (sender, e) =>
            {
                if (inventory.Coins < inventoryUpgradePrice)
                    return;

                inventory.Coins -= inventoryUpgradePrice;
                inventory.Slots.Add(null);
                inventory.ui.Apperance.Position.X -= inventory.ui.Apperance.Size.X / 2;

                if (inventory.Slots.Count >= maxInventorySlots)
                {
                    (sender as Button).Enabled = false;
                    (sender as Button).Apperance.Sprite.Color = Color.Gray;
                }
            };

            // rain chance upgrade
            buttons.Add(new Button()
            {
                Apperance = apperance.Clone(),
                Font = font,
                Text = $"Rain Chance - {rainChanceUpgradePrice}",
            });
            buttons.Last().Clicked += (sender, e) =>
            {
                if (inventory.Coins < rainChanceUpgradePrice)
                    return;

                inventory.Coins -= rainChanceUpgradePrice;
                GlobalModifiers.RainChance += rainChanceUpgradeStep;

                if (GlobalModifiers.RainChance >= maxRainChangeUpgrade)
                {
                    (sender as Button).Enabled = false;
                    (sender as Button).Apperance.Sprite.Color = Color.Gray;
                }
            };

            // plant overgrown upgrade
            buttons.Add(new Button()
            {
                Apperance = apperance.Clone(),
                Font = font,
                Text = $"Overgrown - {plantOvergrownUpragePrice}",
            });
            buttons.Last().Clicked += (sender, e) =>
            {
                if (inventory.Coins < plantOvergrownUpragePrice)
                    return;

                inventory.Coins -= plantOvergrownUpragePrice;
                GlobalModifiers.PlantOvergrownModifier += plantOvergrownUpgradeStep;
            };

            // watering upgrade
            buttons.Add(new Button()
            {
                Apperance = apperance.Clone(),
                Font = font,
                Text = $"Watering - {wateringUpgradePrice}",
            });
            buttons.Last().Clicked += (sender, e) =>
            {
                if (inventory.Coins < wateringUpgradePrice)
                    return;

                inventory.Coins -= wateringUpgradePrice;
                GlobalModifiers.WaterDecreaseSpeed += wateringUpgradeStep;
            };

            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].Apperance.Position = new Vector2()
                {
                    X = (windowSize.X / (buttons.Count + 1)) * (i + 1) 
                        - buttons[i].Apperance.Size.X / 2,
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

        public void SellItem(Item item, int quantity)
        {
            inventory.Coins += item.Price * quantity;
            item.Quantity -= quantity;
        }
    }
}
