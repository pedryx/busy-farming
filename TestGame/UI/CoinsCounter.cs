using TestGame.Components;


namespace TestGame.UI
{
    internal class CoinsCounter : Label
    {
        private readonly Inventory inventory;

        public CoinsCounter(Inventory inventory)
        {
            this.inventory = inventory;
        }

        public override void Update()
        {
            Text = $"Coins: {inventory.Coins}";
        }
    }
}
