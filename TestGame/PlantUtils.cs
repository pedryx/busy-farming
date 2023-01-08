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
        private const int sourceYOffset = 64;
        private const int plantTileWidth = 32;
        private const int plantTileHeight = 64;

        public const int PlantStages = 4;

        private static readonly Entity[] lastRow = new Entity[rowSize];

        private static Vector2 farmPosition;

        public static Texture2D FarmPlotTexture;
        public static Texture2D PlantsTexture;

        public static int FarmRows { get; private set; }

        public static void CalcFarmPosition(GraphicsDeviceManager graphics)
        {
            float size = tileSize * tileScale;
            float width = size * rowSize;

            farmPosition.X = graphics.PreferredBackBufferWidth / 2 - width / 2;
            farmPosition.Y = graphics.PreferredBackBufferHeight * 0.05f;
        }

        public static void CreateFarmColumn(World world)
        {
            if (FarmRows != 0)
            {
                // change border for previous row and attach farm plot components
                for (int i = 0; i < rowSize; i++)
                {
                    var entity = lastRow[i];

                    // change border
                    var apperance = entity.Get<Apperance>();
                    var sourceRectangle = apperance.Sprite.SourceRectange.Value;
                    sourceRectangle.Y -= tileSize;
                    apperance.Sprite.SourceRectange = sourceRectangle;

                    // attach farm plot component
                    if (i != 0 && i != rowSize - 1)
                    {
                        entity.Attach(new FarmPlot());
                    }
                }
            }

            // create new row
            for (int i = 0; i < rowSize; i++)
            {
                var entity = world.CreateEntity();

                // create apperance
                Rectangle sourceRectangle = new()
                {
                    X = borderSize,
                    Y = sourceYOffset + borderSize + tileSize,
                    Width = tileSize,
                    Height = tileSize,
                };

                if (i == 0)
                    sourceRectangle.X -= borderSize;
                if (i == rowSize - 1)
                    sourceRectangle.X += tileSize;
                if (FarmRows == 0)
                    sourceRectangle.Y -= borderSize;

                entity.Attach(new Apperance()
                {
                    Sprite = new Sprite()
                    {
                        Texture = FarmPlotTexture,
                        SourceRectange = sourceRectangle,
                    },
                    Position = new Vector2()
                    {
                        X = i * tileSize * tileScale,
                        Y = FarmRows * tileSize * tileScale,
                    } + farmPosition,
                    Scale = new Vector2(tileScale),
                });

                lastRow[i] = entity;
            }

            FarmRows++;
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
