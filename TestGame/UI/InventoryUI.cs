﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;

using TestGame.Components;


namespace TestGame.UI
{
    internal delegate void InventoryClickEventHandler(object sender, InventoryClickEventArgs e);

    internal class InventoryClickEventArgs : EventArgs
    {
        public Inventory Inventory { get; private set; }
        public int Slot { get; private set; }

        public InventoryClickEventArgs(Inventory inventory, int slot)
        {
            Inventory = inventory;
            Slot = slot;
        }
    }

    internal class InventoryUI : UIElement
    {
        private readonly Inventory inventory;

        public SpriteFont Font;
        public Sprite ClickedSprite;

        public event InventoryClickEventHandler LeftClicked;
        public event InventoryClickEventHandler RightClicked;

        public InventoryUI(Inventory inventory)
        {
            this.inventory = inventory;
        }

        public override void Update(GameTime gameTime)
        {
            var apperance = Apperance.Clone();
            for (int i = 0; i < inventory.Slots.Count; i++)
            {
                if (inventory.Slots[i] != null && inventory.Slots[i].Quantity != 0)
                {
                    if (apperance.Rectangle.Contains(Input.MousePosition))
                    {
                        if (Input.LeftMouseClicked)
                            LeftClicked?.Invoke(this, new InventoryClickEventArgs(inventory, i));
                        else if (Input.RightMouseClicked)
                            RightClicked?.Invoke(this, new InventoryClickEventArgs(inventory, i));
                    }
                }
                apperance.Position.X += apperance.Size.X;
            }

            if (Input.RightMouseClicked)
                inventory.Selected = -1;
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var apperance = Apperance.Clone();
            var clickedSprite = ClickedSprite.Clone();
            var notClickedSprite = Apperance.Sprite.Clone();

            for (int i = 0; i < inventory.Slots.Count; ++i)
            {
                apperance.Sprite = inventory.Selected == i ? clickedSprite : notClickedSprite;
                apperance.Draw(spriteBatch);

                if (inventory.Slots[i] != null && inventory.Slots[i].Quantity != 0)
                {
                    // draw item icon
                    var itemApperance = apperance.Clone();
                    itemApperance.Sprite = inventory.Slots[i].Sprite;
                    float biggerSide = MathHelper.Max(
                        itemApperance.NotScaledSize.X,
                        itemApperance.NotScaledSize.Y
                    );
                    itemApperance.Scale = new Vector2(apperance.Size.X * 0.8f) / biggerSide;
                    itemApperance.Center(apperance);
                    itemApperance.Draw(spriteBatch);

                    // draw item quantity
                    string text = $"{inventory.Slots[i].Quantity}";
                    Vector2 position = apperance.Position;
                    position.X += (apperance.Size.X * 0.88f) - Font.MeasureString(text).X;
                    position.Y += apperance.Size.Y * 0.12f;
                    spriteBatch.DrawString(Font, text, position, Color.Black);
                }

                apperance.Position.X += apperance.Size.X;
            }
        }
    }
}
