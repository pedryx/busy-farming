using System;
using System.Collections.Generic;

using TestGame.Components;


namespace TestGame
{
    internal class PlantType
    {
        private static int lastPlantID;
        private static readonly List<PlantType> types = new()
        {
            new PlantType(00, 00, 09, 12, 08, 14, 018, "potatoe"),
            new PlantType(00, 06, 05, 08, 01, 01, 008, "carrot"),
            new PlantType(00, 09, 06, 07, 01, 01, 002, "beet"),
            new PlantType(00, 12, 09, 09, 10, 20, 026, "garlic"),
            new PlantType(00, 17, 06, 09, 02, 04, 151, "pepper"),
            new PlantType(00, 23, 07, 10, 02, 04, 550, "watermelon"),
            new PlantType(00, 27, 09, 12, 02, 05, 540, "pumpkin"),
            new PlantType(00, 31, 09, 12, 02, 04, 056, "corn"),
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
            Name = name;
        }

        public Plant CreatePlant() => new()
        {
            Type = this,
            GrowDuration = random.Next(MinGrowDuration, MaxGrowDuration + 1),
            Yield = random.Next(MinYield, MaxYield),
        };
        
    }
}
