using System.Collections.Generic;


namespace TestGame.Components
{
    internal class Inventory
    {
        public List<Item> Slots = new();
        public int Selected = -1;
    }
}
