using Microsoft.Xna.Framework;

using MonoGame.Extended.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TestGame.Components;

namespace TestGame
{
    internal static class PlantUtils
    {
        private const int rowSize = 10;
        private const float tileScale = 2.5f;
        private const int tileSize = 32;
        private const int borderSize = 32;
        private const int yOffset = 64;

        public static void AppendRow(Entity farm)
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
                    TextureName = "plowed_soil",
                    Offset = new Vector2(x * tileSize * tileScale, y * tileSize * tileScale),
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
    }
}
