using TestGame.Components;


namespace TestGame
{
    internal class Item
    {
        public int Quantity;
        public Sprite Sprite;
    }

    internal class SeedItem : Item
    {
        public PlantType Type;
    }
}
