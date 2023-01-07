using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MonoGame.Extended.Entities;

using System.Collections.Generic;

using TestGame.Components;


namespace TestGame
{
    internal static class PlantUtils
    {
        private const int rowSize = 20;
        private const float tileScale = 1.5f;
        private const int tileSize = 32;
        private const int borderSize = 32;
        private const int yOffset = 64;
        private const int plantTileWidth = 32;
        private const int plantTileHeight = 64;

        public const int PlantStages = 4;

        public static Texture2D FarmPlotTexture;
        public static Texture2D PlantsTexture;

        public static Rectangle GetFarmRectangle(Entity farm)
        {
            var apperance = farm.Get<Apperance>();
            var farmPlots = farm.Get<FarmPlots>();

            return new Rectangle()
            {
                X = (int)apperance.Sprites[0].Position.X,
                Y = (int)apperance.Sprites[0].Position.Y,
                Width = (int)(apperance.Sprites[0].Size.X * rowSize),
                Height = (int)(apperance.Sprites[0].Size.Y * farmPlots.plants.Count),
            };
        }

        public static void AppendFarmRow(Entity farm)
        {
            var apperance = farm.Get<Apperance>();
            var farmPlots = farm.Get<FarmPlots>();

            // change border of previous row
            if (farmPlots.plants.Count != 0)
            {
                for (int i = (farmPlots.plants.Count - 1) * rowSize; i < apperance.Sprites.Count; i++)
                {
                    var source = apperance.Sprites[i].SourceRectange.Value;
                    source.Y -= tileSize;
                    apperance.Sprites[i].SourceRectange = source;
                }
            }

            // create sprites for new row
            int y = farmPlots.plants.Count;
            for (int x = 0; x < rowSize; x++)
            {
                int xSource = borderSize;
                int ySource = yOffset + borderSize + tileSize;

                if (x == 0)
                    xSource -= borderSize;
                if (y == 0)
                    ySource -= tileSize;
                if (x == rowSize - 1)
                    xSource += borderSize;

                var sprite = new Sprite()
                {
                    Texture = FarmPlotTexture,
                    Position = new Vector2(x * tileSize * tileScale, y * tileSize * tileScale),
                    SourceRectange = new Rectangle(
                        xSource,
                        ySource,
                        tileSize,
                        tileSize
                    ),
                    Scale = tileScale,
                };
                apperance.Sprites.Add(sprite);
            }

            // create farm plots for new row
            farmPlots.plants.Add(new List<Plant>());
            for (int x = 0; x < rowSize; x++)
            {
                farmPlots.plants[y].Add(null);
            }
        }

        public static Sprite CreatePlantSprite(Plant plant, int stage)
            => CreatePlantSprite(plant, stage, Vector2.Zero, 1.0f);

        public static Sprite CreatePlantSprite(Plant plant, int stage, Vector2 position, float scale)
            => new()
            {
                Texture = PlantsTexture,
                SourceRectange = new Rectangle()
                {
                    X = plant.SpriteColumn * plantTileWidth,
                    Y = plant.SpriteRow * plantTileHeight * PlantStages + stage * plantTileHeight,
                    Width = plantTileWidth,
                    Height = plantTileHeight,
                },
                Position = position,
                Scale = scale,
            };

        public static void SetGrowStage(Plant plant, Sprite sprite, int stage)
        {
            var sourceRectangle = sprite.SourceRectange.Value;
            sourceRectangle.Y = plant.SpriteRow * plantTileHeight * PlantStages
                + stage * plantTileHeight;
            sprite.SourceRectange = sourceRectangle;
        }
    }
}
