using TestGame.Components;


namespace TestGame.UI
{
    internal class MoneyCounter : Label
    {
        private readonly Inventory inventory;

        public MoneyCounter(Inventory inventory)
        {
            this.inventory = inventory;
        }

        public override void Update()
        {
            Text = $"Money: {inventory.Money}";
        }
    }
}
