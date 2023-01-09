using Microsoft.Xna.Framework;

using TestGame.Components;


namespace TestGame.UI
{
    internal class MiscCounter : Label
    {
        private readonly Inventory inventory;

        public MiscCounter(Inventory inventory)
        {
            this.inventory = inventory;
        }

        public override void Update(GameTime gameTime)
        {
            Text = $"Coins: {inventory.Coins}\nWater: {inventory.CurrentWater}/{inventory.MaxWater}";
        }
    }
}
