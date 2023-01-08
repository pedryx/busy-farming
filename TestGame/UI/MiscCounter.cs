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

        public override void Update()
        {
            Text = $"Coins: {inventory.Coins}\nWater: {inventory.CurrentWater}/{inventory.MaxWater}";
        }
    }
}
