using System;

using TestGame.Components;


namespace TestGame
{
    internal class PlantType
    {
        private readonly Random random = new();

        public Plant PlantPrototype { get; private set; }
        public int MinGrowDuration { get; private set; }
        public int MaxGrowDuration { get; private set; }

        public PlantType() { }
        public PlantType(int spriteRow, int spriteColumn, int minGrowDuration, int maxGrowDuration)
        {
            PlantPrototype = new Plant(spriteColumn, spriteRow);
            MinGrowDuration = minGrowDuration;
            MaxGrowDuration = maxGrowDuration;
        }

        public Plant CreatePlant()
        {
            Plant plant = PlantPrototype.Clone();
            plant.GrowDuration = random.Next(MinGrowDuration, MaxGrowDuration + 1);
            return plant;
        }

        public static PlantType Potatoe = new(0, 0, 9, 12);
        public static PlantType Carrots = new(0, 6, 5, 8);
        public static PlantType Beets = new(0, 9, 6, 7);
        public static PlantType Garlic = new(0, 12, 9, 9);
        public static PlantType Pepper = new(0, 17, 6, 9);
        public static PlantType WaterMelon = new(0, 23, 7, 10);
        public static PlantType Pumpkin = new(0, 27, 9, 12);
        public static PlantType Corn = new(0, 31, 9, 12);
    }
}
