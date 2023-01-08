using Microsoft.Xna.Framework;

using TestGame.Components;
using TestGame.Scenes;


namespace TestGame.Systems
{
    internal class PlantSystem : EntityUpdateSystem<Plant, Apperance>
    {
        private readonly LevelScene scene;

        public PlantSystem(LevelScene scene)
        {
            this.scene = scene;
        }

        public override void Update(Plant plant, Apperance apperance, GameTime gameTime)
        {
            var inventory = scene.Inventory;

            plant.CurrentGrow += (float)gameTime.ElapsedGameTime.TotalSeconds * LDGame.GameSpeed;
            int stage = (int)((plant.CurrentGrow / plant.GrowDuration) * (PlantUtils.PlantStages - 1));
            apperance.Sprite = PlantUtils.CreatePlantSprite(plant.Type, stage);

            if (plant.CurrentGrow >= plant.GrowDuration)
            {
                plant.CurrentGrow = plant.GrowDuration;

                if (!Input.LeftClickOn(apperance) || inventory.Selected != -1)
                    return;

                DestroyEntity(EntityID);
                plant.FarmPlot.Occupied = false;
                plant.FarmPlot = null;

                int slot = inventory.GetItemSlotOrFreeSlot<ProductItem>(plant.Type.PlantID);
                if (slot == -1)
                    return;

                if (inventory.Slots[slot] == null || inventory.Slots[slot].Quantity == 0)
                {
                    inventory.Slots[slot] = new ProductItem(plant.Type.PlantID)
                    {
                        Quantity = plant.Yield,
                        Sprite = plant.Type.ProductSprite,
                    };
                }
                else
                {
                    inventory.Slots[slot].Quantity += plant.Yield;
                }
            }
        }
    }
}
