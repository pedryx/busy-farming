using Microsoft.Xna.Framework.Graphics;

using TestGame.Components;


namespace TestGame.UI
{
    internal class InventoryUI : UIControl
    {
        private readonly Inventory inventory;

        public float Width => inventory.Slots.Count * Sprite.Width;

        public float Height => Sprite.Height;

        public InventoryUI(Inventory inventory)
        {
            this.inventory = inventory;
        }

        public override void Update()
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            float offsetX = Sprite.Offset.X;
            for (int i = 0; i < inventory.Slots.Count; ++i)
            {
                Sprite.Offset.X = offsetX + i * Sprite.Width;
                Sprite.Draw(spriteBatch, Transform);
            }
            Sprite.Offset.X = offsetX;
        }
    }
}
