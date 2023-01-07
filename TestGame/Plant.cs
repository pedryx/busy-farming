namespace TestGame
{
    internal class Plant
    {
        /// <summary>
        /// Column in plants sprite sheet.
        /// </summary>
        public int Column { get; private set; }
        /// <summary>
        /// Row in plants sprite sheet.
        /// </summary>
        public int Row { get; private set; }
        /// <summary>
        /// Planting time multiplier.
        /// </summary>
        public float PlantMultiplier { get; private set; }
        /// <summary>
        /// Harvesting time multiplier.
        /// </summary>
        public float HarvestMultiplier { get; private set; }
        /// <summary>
        /// Safe overgrow duration multiplier.
        /// </summary>
        public float OvergrowMultiplier { get; private set; }
        /// <summary>
        /// watering duration multiplier.
        /// </summary>
        public float WateringMultiplier { get; private set; }
        /// <summary>
        /// No watering duration multiplier.
        /// </summary>
        public float NoWateringMultiplier { get; private set; }
        /// <summary>
        /// Number of days to grow.
        /// </summary>
        public float GrowTime { get; private set; }
        /// <summary>
        /// Base price for plant product (product obtained when plant is harvested).
        /// </summary>
        public float BasePrice { get; private set; }

        /// <summary>
        /// Current grow progress (in days).
        /// </summary>
        public float GrowProgress;
        /// <summary>
        /// Duration for which is plant without watering (in days).
        /// </summary>
        public float NoWateringDuration;

        public Sprite InventorySprite;

        public Plant(
            int column,
            int row,
            float plantMultiplier,
            float harvestMultiplier,
            float overgrowMultiplier,
            float wateringMultiplier,
            float noWateringDuration,
            float basePrice,
            float growTime
        )
        {
            Column = column;
            Row = row;
            PlantMultiplier = plantMultiplier;
            HarvestMultiplier = harvestMultiplier;
            OvergrowMultiplier = overgrowMultiplier;
            WateringMultiplier = wateringMultiplier;
            NoWateringMultiplier = noWateringDuration;
            BasePrice = basePrice;
            GrowTime = growTime;

            InventorySprite = PlantUtils.CreateInvetoryPlantSprite(this);
        }

        public static Plant RussetPotatoe = new(0, 0, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 4.0f);
        public static Plant RandomCorn = new(31, 0, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 4.0f);
    }
}
