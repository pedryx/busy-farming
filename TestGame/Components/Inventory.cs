using System.Collections.Generic;


namespace TestGame.Components
{
    internal class Inventory
    {
        public List<Plant> Slots = new();
        public int Selected = -1;
    }
}
