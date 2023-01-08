using Microsoft.Xna.Framework;

using TestGame.Components;


namespace TestGame.Systems
{
    internal class WeedSpawnSystem : EntityUpdateSystem<Apperance, Weed>
    {
        private const float weedSpawnTime = 5.0f;

        private readonly Sprite weedSprite;

        private float weedSpawnEllapsed;

        public WeedSpawnSystem(Sprite weedSprite)
        {
            this.weedSprite = weedSprite;
        }

        public override void PreUpdate(GameTime gameTime)
        {
            weedSpawnEllapsed += (float)gameTime.ElapsedGameTime.TotalSeconds * LDGame.GameSpeed;
            if (weedSpawnEllapsed >= weedSpawnTime)
            {
                weedSpawnEllapsed -= weedSpawnTime;

                var spotEntity = PlantUtils.GetFreePlantSpot();
                if (spotEntity == null)
                    return;

                var tileApperance = spotEntity.Get<Apperance>();
                var farmPlot = spotEntity.Get<FarmPlot>();
                farmPlot.Occupied = true;

                var apperance = tileApperance.Clone();
                apperance.Sprite = weedSprite;
                float biggerSide = MathHelper.Max(apperance.NotScaledSize.X, apperance.NotScaledSize.Y);
                apperance.Scale = new Vector2(tileApperance.Size.X / biggerSide);

                var entity = CreateEntity();
                entity.Attach(new Weed()
                {
                    FarmPlot = farmPlot,
                });
                entity.Attach(apperance);
            }
        }

        public override void Update(Apperance apperance, Weed weed, GameTime gameTime)
        {
            if (Input.LeftClickOn(apperance))
            {
                DestroyEntity(EntityID);
                weed.FarmPlot.Occupied = false;
                weed.FarmPlot = null;
            }
        }
    }
}
