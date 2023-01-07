using Microsoft.Xna.Framework;

using System.Collections.Generic;

using TestGame.Components;
using TestGame.Systems;


namespace TestGame.Scenes
{
    internal class LevelScene : Scene
    {
        protected override void CreateSystems()
        {
            Builder
                .AddSystem(new RenderSystem(
                    Game.SpriteBatch,
                    Game.SpriteManager,
                    Game.Camera
                 ))
                .AddSystem(new CameraControlSystem(Game.Camera));
        }

        protected override void CreateEntities()
        {
            CreateFarm(1, 10);
        }

        private void CreateFarm(int rows, int rowSize)
        {
            var farm = World.CreateEntity();
            farm.Attach(new Transform());
            farm.Attach(new Inventory());

            float scale = 2.5f;
            int tileSize = 32;
            int borderSize = 32;
            int yOffset = 64;
            int width = rowSize + 2;
            int height = rows + 2;
            var apperance = new Apperance();
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    int xSource = borderSize;
                    int ySource = yOffset + borderSize;

                    if (x == 0)
                        xSource -= borderSize;
                    if (y == 0)
                        ySource -= borderSize;
                    if (x == width - 1)
                        xSource += borderSize;
                    if (y == height - 1)
                        ySource += borderSize;

                    var sprite = new Sprite()
                    {
                        TextureName = "plowed_soil",
                        Offset = new Vector2(x * tileSize * scale, y * tileSize * scale),
                        SourceRectange = new Rectangle(
                            xSource,
                            ySource,
                            tileSize,
                            tileSize
                        ),
                        Scale = scale,
                    };
                    apperance.Sprites.Add(sprite);

                }
            }
            farm.Attach(apperance);

            var farmPlots = new FarmPlots();
            for (int y = 0; y < rows; y++)
            {
                farmPlots.plants.Add(new List<Plant>());
                for (int x = 0; x < rowSize; x++)
                {
                    farmPlots.plants[y].Add(null);
                }
            }
            farm.Attach(farmPlots);
        }
    }
}
