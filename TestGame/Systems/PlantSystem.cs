using Microsoft.Xna.Framework;

using TestGame.Components;


namespace TestGame.Systems
{
    internal class PlantSystem : EntityUpdateSystem<Plant, Apperance>
    {
        public override void Update(Plant plant, Apperance apperance, GameTime gameTime)
        {
            plant.CurrentGrow += (float)gameTime.ElapsedGameTime.TotalSeconds * LDGame.GameSpeed;
            int stage = (int)((plant.CurrentGrow / plant.GrowDuration) * (PlantUtils.PlantStages - 1));
            PlantUtils.SetGrowStage(plant, apperance.Sprites[0], stage);

            if (plant.CurrentGrow >= plant.GrowDuration)
            {
                plant.CurrentGrow = plant.GrowDuration;
            }
        }
    }
}
