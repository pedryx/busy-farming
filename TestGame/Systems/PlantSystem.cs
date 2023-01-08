using Microsoft.Xna.Framework;

using TestGame.Components;
using TestGame.Scenes;


namespace TestGame.Systems
{
    internal class PlantSystem : EntityUpdateSystem<Plant, Apperance, ProgressBar>
    {
        private readonly LevelScene scene;

        public static bool PlantDecay = true;

        public PlantSystem(LevelScene scene)
        {
            this.scene = scene;
        }

        public override void Update(Plant plant, Apperance apperance, ProgressBar progressBar, GameTime gameTime)
        {
            var inventory = scene.Inventory;
            float ellaped = (float)gameTime.ElapsedGameTime.TotalSeconds * LDGame.GameSpeed;

            // update water
            if (!plant.Decayed && plant.CurrentGrow < plant.GrowDuration)
            {
                plant.CurrentWater -= ellaped;
                progressBar.CurrentValue = plant.CurrentWater;
                if (plant.CurrentWater <= 0)
                {
                    plant.Decayed = true;
                    apperance.Sprite.Color = Color.Black;
                }
            }

            // update grow
            if (!plant.Decayed && plant.MaxWater / plant.CurrentWater >= 0.5f)
            {
                plant.CurrentGrow += ellaped;
                int stage = MathHelper.Min(
                    (int)((plant.CurrentGrow / plant.GrowDuration) * (PlantUtils.PlantStages - 1)),
                    PlantUtils.PlantStages - 1
                );
                apperance.Sprite = PlantUtils.CreatePlantSprite(plant.Type, stage);
            }

            // decay
            if (plant.CurrentGrow >= plant.GrowDuration + plant.Type.MaxOvergrow)
            {
                if (PlantDecay)
                {
                    plant.Decayed = true;
                    apperance.Sprite.Color = Color.Black;
                }
            }

            // when clicked
            if (Input.LeftClickOn(apperance))
            {
                // destroy decay plant
                if (plant.Decayed)
                {
                    DestroyPlant(plant);
                    return;
                }

                // harvest plant
                if (plant.CurrentGrow >= plant.GrowDuration)
                {
                    DestroyPlant(plant);

                    int slot = inventory.GetItemSlotOrFreeSlot<ProductItem>(plant.Type.PlantID);
                    if (slot == -1)
                        return;

                    if (inventory.Slots[slot] == null || inventory.Slots[slot].Quantity == 0)
                    {
                        inventory.Slots[slot] = new ProductItem(plant.Type.PlantID)
                        {
                            Quantity = plant.Yield,
                            Sprite = plant.Type.ProductSprite,
                            Price = plant.Type.Price,
                        };
                    }
                    else
                    {
                        inventory.Slots[slot].Quantity += plant.Yield;
                    }
                    return;
                }

                // water plant
                if (inventory.SelectedItem is WaterCan && inventory.CurrentWater > 0)
                {
                    plant.CurrentWater = plant.MaxWater;
                    inventory.CurrentWater--;
                    return;
                }
            }
        }

        private void DestroyPlant(Plant plant)
        {
            DestroyEntity(EntityID);
            plant.FarmPlot.Occupied = false;
            plant.FarmPlot = null;
        }

    }
}
