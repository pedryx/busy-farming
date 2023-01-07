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
        public float GrowDuration;
        public float CurrentGrow;

        public Plant(int column, int row)
        {
            SpriteColumn = column;
            SpriteRow = row;

            InventorySprite = PlantUtils.CreatePlantSprite(this, 3);
        }

        public Plant Clone()
        {
            var plant = (Plant)MemberwiseClone();
            plant.InventorySprite = InventorySprite.Clone();

            return plant;
        }
    }
}
