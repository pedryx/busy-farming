namespace TestGame.Components
{
    internal class Plant
    {
        /// <summary>
        /// Column in plants sprite sheet.
        /// </summary>
        public int SpriteColumn { get; private set; }
        /// <summary>
        /// Row in plants sprite sheet.
        /// </summary>
        public int SpriteRow { get; private set; }
        public Sprite InventorySprite { get; private set; }

        public int X;
        public int Y;

        public Plant(int column, int row)
        {
            SpriteColumn = column;
            SpriteRow = row;

            InventorySprite = PlantUtils.CreatePlantSprite(this, 3);
        }

        public static Plant RussetPotatoe = new(0, 0);
        public static Plant RandomCorn = new(31, 0);

        public Plant Clone()
        {
            var plant = (Plant)MemberwiseClone();
            plant.InventorySprite = InventorySprite.Clone();

            return plant;
        }
    }
}
