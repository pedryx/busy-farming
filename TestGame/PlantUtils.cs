using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MonoGame.Extended.Entities;

using System.Collections.Generic;

using TestGame.Components;


namespace TestGame
{
    internal static class PlantUtils
    {
        private const int columnSize = 20;
        private const float tileScale = 1.5f;
        private const int tileSize = 32;
        private const int borderSize = 32;
        private const int yOffset = 64;
        private const int plantTileWidth = 32;
        private const int plantTileHeight = 64;

        public const int PlantStages = 4;

        private static readonly Vector2 farmPosition = new(-540, -260);
        private static readonly Entity[] lastColumn = new Entity[columnSize];

        public static Texture2D FarmPlotTexture;
        public static Texture2D PlantsTexture;

        public static int FarmColumns { get; private set; }

        public static void CreateFarmColumn(World world)
        {
            if (FarmColumns != 0)
            {
                // change border for previous column and attach farm plot components
                for (int i = 0; i < columnSize; i++)
                {
                    var entity = lastColumn[i];

                    // change border
                    var apperance = entity.Get<Apperance>();
                    var sourceRectangle = apperance.Sprite.SourceRectange.Value;
                    sourceRectangle.X -= tileSize;
                    apperance.Sprite.SourceRectange = sourceRectangle;

                    // attach farm plot component
                    if (i != 0 && i != columnSize - 1)
                    {
                        entity.Attach(new FarmPlot());
                    }
                }
            }

            // create new column
            for (int i = 0; i < columnSize; i++)
            {
                var entity = world.CreateEntity();

                // create apperance
                Rectangle sourceRectangle = new()
                {
                    X = borderSize + tileSize,
                    Y = yOffset + borderSize,
                    Width = tileSize,
                    Height = tileSize,
                };

                if (i == 0)
                    sourceRectangle.Y -= borderSize;
                if (i == columnSize - 1)
                    sourceRectangle.Y += borderSize;
                if (FarmColumns == 0)
                    sourceRectangle.X -= borderSize;

                entity.Attach(new Apperance()
                {
                    Sprite = new Sprite()
                    {
                        Texture = FarmPlotTexture,
                        SourceRectange = sourceRectangle,
                    },
                    Position = new Vector2()
                    {
                        X = FarmColumns * tileSize * tileScale,
                        Y = i * tileSize * tileScale,
                    } + farmPosition,
                    Scale = new Vector2(tileScale),
                });

                lastColumn[i] = entity;
            }
        }

        public static Sprite CreatePlantSprite(PlantType plant, int stage) => new()
        {
            Texture = PlantsTexture,
            SourceRectange = new Rectangle()
            {
                X = plant.SpriteColumn * plantTileWidth,
                Y = plant.SpriteRow * plantTileHeight * PlantStages + stage * plantTileHeight,
                Width = plantTileWidth,
                Height = plantTileHeight,
            },
        };
    }
}
