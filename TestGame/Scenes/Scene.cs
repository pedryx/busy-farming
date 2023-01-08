using Microsoft.Xna.Framework;

using MonoGame.Extended.Entities;

using TestGame.UI;


namespace TestGame.Scenes
{
    internal abstract class Scene
    {
        protected UILayer UILayer { get; private set; } = new();
        protected WorldBuilder Builder { get; private set; } = new();
        protected LDGame Game { get; private set; }

        public World World { get; private set; }

        protected virtual void CreateEntities() { }
        protected virtual void CreateSystems() { }

        protected virtual void CreateUI() { }

        public virtual void Initialize(LDGame game)
        {
            Game = game;
            CreateSystems();
            World = Builder.Build();
            Builder = null;
            CreateEntities();
            CreateUI();
        }

        public void Update(GameTime gameTime)
        {
            World.Update(gameTime);
            UILayer.Update();
        }

        public void Draw(GameTime gameTime)
        {
            World.Draw(gameTime);
            UILayer.Draw(Game.SpriteBatch);
        }
    }
}
