using System;
using System.Collections.Generic;

using TestGame.Components;


namespace TestGame
{
    internal class PlantType
    {
        private const float maxPlantWater = 8.0f;

        private static int lastPlantID;
        private static readonly List<PlantType> types = new()
        {
            new PlantType(00, 00, 09, 12, 2, 4, 18, 10, "potatoe"),
            new PlantType(00, 06, 05, 08, 1, 1, 08, 11, "carrot"),
            new PlantType(00, 09, 06, 07, 1, 1, 02, 15, "beet"),
            new PlantType(00, 12, 09, 09, 2, 4, 26, 08, "garlic"),
            new PlantType(00, 17, 06, 09, 2, 4, 40, 06, "pepper"),
            new PlantType(00, 23, 08, 10, 1, 1, 90, 03, "watermelon"),
            new PlantType(00, 27, 07, 12, 1, 1, 80, 03, "pumpkin"),
            new PlantType(00, 31, 09, 12, 2, 4, 10, 07, "corn"),
        };
        public static IReadOnlyList<PlantType> Types => types;
        public static PlantType GetType(string name)
        {
            foreach (var type in types)
            {
                if (type.Name == name)
                    return type;
            }

            return null;
        }

        public static void PrepareTextures(LDGame game)
        {
            foreach (var type in types)
            {
                type.ProductSprite = new Sprite()
                {
                    Texture = game.SpriteManager[type.Name],
                };
            }
        }

        private readonly Random random = new();

        public int SpriteRow { get; private set; }
        public int SpriteColumn { get; private set; }
        public int MinGrowDuration { get; private set; }
        public int MaxGrowDuration { get; private set; }
        public int MinYield { get; private set; }
        public int MaxYield { get; private set; }
        public int MaxOvergrow { get; private set; }
        public int Price { get; private set; }
        public string Name { get; private set; }
        public Sprite ProductSprite { get; private set; }
        public int PlantID { get; private set; } = lastPlantID++;

        public PlantType(
            int spriteRow,
            int spriteColumn,
            int minGrowDuration,
            int maxGrowDuration,
            int minYield,
            int maxYield,
            int price,
            int maxOvergrow,
            string name
        )
        {
            SpriteRow = spriteRow;
            SpriteColumn = spriteColumn;
            MinGrowDuration = minGrowDuration;
            MaxGrowDuration = maxGrowDuration;
            MinYield = minYield;
            MaxYield = maxYield;
            Price = price;
            MaxOvergrow = maxOvergrow;
            Name = name;
        }

        public Plant CreatePlant() => new()
        {
            Type = this,
            GrowDuration = random.Next(MinGrowDuration, MaxGrowDuration + 1),
            Yield = random.Next(MinYield, MaxYield),
            MaxWater = maxPlantWater,
            CurrentWater = maxPlantWater,
        };
        
    }
}
