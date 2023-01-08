using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TestGame.Components;


namespace TestGame.UI
{
    internal class InventoryUI : UIControl
    {
        private readonly Inventory inventory;

        public SpriteFont Font;
        public Sprite ClickedSprite;

        public InventoryUI(Inventory inventory)
        {
            this.inventory = inventory;
        }

        public override void Update()
        {
            var apperance = Apperance.Clone();
            for (int i = 0; i < inventory.Slots.Count; i++)
            {
                if (apperance.Rectangle.Contains(Input.MousePosition) && Input.LeftMouseClicked)
                {
                    if (inventory.Slots[i] != null && inventory.Slots[i].Quantity != 0)
                        inventory.Selected = i;
                }
                apperance.Position.X += apperance.Size.X;
            }

            if (Input.RightMouseClicked)
            {
                inventory.Selected = -1;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
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
                    itemApperance.Scale = new Vector2((apperance.Size.Y * 0.8f) /
                        itemApperance.NotScaledSize.Y);
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
