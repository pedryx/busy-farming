using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TestGame.Components;
using TestGame.Scenes;


namespace TestGame.Systems
{
    internal class PlantPlacementSystem : EntityDrawSystem<Apperance, FarmPlot>
    {
        private readonly LevelScene scene;
        private readonly Camera camera;

        public PlantPlacementSystem(SpriteBatch spriteBatch, Camera camera, LevelScene scene) 
            : base(spriteBatch)
        {
            this.scene = scene;
            this.camera = camera;
        }

        public override void PreDraw(GameTime gameTime)
            => SpriteBatch.Begin(transformMatrix: camera.GetTransform());

        public override void PostDraw(GameTime gameTime)
            => SpriteBatch.End();

        public override void Draw(Apperance apperance, FarmPlot farmPlot, GameTime gameTime)
        {
            if (farmPlot.Occupied)
                return;

            var inventory = scene.Inventory;

            if (inventory.SelectedItem is not SeedItem || inventory.SelectedItem.Quantity == 0)
                return;

            if (apperance.Rectangle.Contains(Input.MousePositionTransformed))
            {
                // render ghost plant
                var ghostApperance = apperance.Clone();

                ghostApperance.Sprite = PlantUtils.CreatePlantSprite(
                    (inventory.Slots[inventory.Selected] as SeedItem).Type,
                    0
                );
                ghostApperance.Sprite.Color.A = 128;

                ghostApperance.Scale = new Vector2(ghostApperance.NotScaledSize.Y
                    / (apperance.Size.Y * 1.4f));
                ghostApperance.Center(apperance);
                ghostApperance.Position.Y -= ghostApperance.Size.Y / 3;

                ghostApperance.Draw(SpriteBatch);

                if (Input.LeftMouseClicked)
                {
                    // plant a seed
                    var plantType = (inventory.Slots[inventory.Selected] as SeedItem).Type;
                    var plantApperance = ghostApperance.Clone();
                    plantApperance.Sprite = PlantUtils.CreatePlantSprite(plantType, 0);
                    plantApperance.Layer = plantApperance.Position.Y / 3000;

                    var plant = plantType.CreatePlant();
                    plant.FarmPlot = farmPlot;

                    var plantEntity = CreateEntity();
                    plantEntity.Attach(plantApperance);
                    plantEntity.Attach(plant);

                    farmPlot.Occupied = true;
                    inventory.SelectedItem.Quantity--;
                    if (inventory.SelectedItem.Quantity == 0)
                    {
                        inventory.Selected = -1;
                    }
                }
            }
        }
    }
}
