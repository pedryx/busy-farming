using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TestGame.Components;
using TestGame.Scenes;


namespace TestGame.Systems
{
    internal class PlantPlacementSystem : EntityDrawSystem<Apperance, FarmPlot>
    {
        private readonly LevelScene scene;

        public PlantPlacementSystem(SpriteBatch spriteBatch, LevelScene scene) 
            : base(spriteBatch)
        {
            this.scene = scene;
        }

        public override void Draw(Apperance apperance, FarmPlot farmPlot, GameTime gameTime)
        {
            if (farmPlot.Occupied)
                return;

            var inventory = scene.Inventory;

            if (inventory.Selected == -1 || inventory.Slots[inventory.Selected] is not SeedItem)
                return;

            if (apperance.Rectangle.Contains(Input.MousePositionTransformed))
            {
                // render ghost plant
                var ghostApperance = apperance.Clone();
                ghostApperance.Scale = new Vector2((apperance.Scale.Y * 1.5f) /
                    ghostApperance.NotScaledSize.Y);
                ghostApperance.Center(apperance);
                ghostApperance.Sprite = inventory.Slots[inventory.Selected].Sprite;
                ghostApperance.Sprite.Color.A = 128;

                if (Input.LeftMouseClicked)
                {
                    // plant a seed
                    inventory.Slots[inventory.Selected].Quantity--;

                    var plantType = (inventory.Slots[inventory.Selected] as SeedItem).Type;
                    var plantApperance = ghostApperance.Clone();
                    plantApperance.Sprite = PlantUtils.CreatePlantSprite(plantType, 0);

                    var plantEntity = CreateEntity();
                    plantEntity.Attach(plantApperance);
                    plantEntity.Attach(plantType.CreatePlant());

                    farmPlot.Occupied = true;
                }
            }
        }
    }
}
