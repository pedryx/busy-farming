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
            var inventory = scene.Inventory;

            if (!((inventory.SelectedItem is SeedItem || inventory.SelectedItem is WaterCan) &&
                inventory.SelectedItem.Quantity != 0))
            {
                return;
            }

            if (farmPlot.Occupied && inventory.SelectedItem is SeedItem)
                return;

            if (apperance.Rectangle.Contains(Input.MousePositionTransformed))
            {
                // render ghost plant
                var ghostApperance = apperance.Clone();

                if (inventory.SelectedItem is SeedItem)
                {
                    ghostApperance.Sprite = PlantUtils.CreatePlantSprite(
                        (inventory.Slots[inventory.Selected] as SeedItem).Type,
                        0
                    );
                }
                else
                {
                    ghostApperance.Sprite = inventory.SelectedItem.Sprite.Clone();
                }
                ghostApperance.Sprite.Color.A = 128;

                float biggerSide = MathHelper.Max(
                    ghostApperance.NotScaledSize.X,
                    ghostApperance.NotScaledSize.Y
                );
                float m = inventory.SelectedItem is SeedItem ? 1.4f : 0.9f;
                ghostApperance.Scale = new Vector2(apperance.Size.Y * m) / biggerSide;
                ghostApperance.Center(apperance);
                ghostApperance.Position.Y -= ghostApperance.Size.Y / 3;

                ghostApperance.Draw(SpriteBatch);

                if (Input.LeftMouseClicked)
                {
                    if (inventory.SelectedItem is SeedItem)
                    {
                        // plant a seed
                        var plantType = (inventory.Slots[inventory.Selected] as SeedItem).Type;
                        var plantApperance = ghostApperance.Clone();
                        plantApperance.Sprite = PlantUtils.CreatePlantSprite(plantType, 0);
                        plantApperance.Layer = plantApperance.Position.Y / 3000;

                        var plant = plantType.CreatePlant();
                        plant.FarmPlot = farmPlot;

                        var progressBar = new ProgressBar()
                        {
                            StartValue = 0,
                            EndValue = plant.MaxWater,
                            CurrentValue = plant.CurrentWater,
                            Color = Color.Blue,
                        };

                        var plantEntity = CreateEntity();
                        plantEntity.Attach(plantApperance);
                        plantEntity.Attach(plant);
                        plantEntity.Attach(progressBar);

                        farmPlot.Occupied = true;
                        inventory.SelectedItem.Quantity--;
                        if (inventory.SelectedItem.Quantity == 0)
                        {
                            inventory.Selected = -1;
                        }
                    }
                    else if (inventory.SelectedItem is WaterCan)
                    {
                        System.Console.WriteLine("Watering!");
                    }
                }
            }
        }
    }
}
