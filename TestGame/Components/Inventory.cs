using System.Collections.Generic;


namespace TestGame.Components
{
    internal class Inventory
    {
        public List<Item> Slots = new();
        public int Selected = -1;
        public int Money;

        public Item SelectedItem => Selected == -1 ? null : Slots[Selected];

        public int GetItemSlotOrFreeSlot<T>(int id)
            where T : Item
        {
            for (int i = 0; i < Slots.Count; i++)
            {
                if (Slots[i] is T && Slots[i].Quantity != 0 && Slots[i].ID == id)
                    return i;
            }

            return GetFreeSlot();
        }

        public int GetFreeSlot()
        {
            for (int i = 0; i < Slots.Count; i++)
            {
                if (Slots[i] == null || Slots[i].Quantity == 0)
                    return i;
            }

            return -1;
        }
    }
}
