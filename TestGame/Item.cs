using TestGame.Components;


namespace TestGame
{
    internal abstract class Item
    {
        public int Quantity;
        public Sprite Sprite;
        public int ID;
        public int Price;

        public Item(int id)
        {
            ID = id;
        }
    }

    internal class SeedItem : Item
    {
        public PlantType Type;

        public SeedItem(int id) : base(id) { }
    }

    internal class ProductItem : Item
    {
        public ProductItem(int id) : base(id) { }
    }

    internal class WaterCan : Item
    {
        public WaterCan(int id) : base(id) { }
    }
}
