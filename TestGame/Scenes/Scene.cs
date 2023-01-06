using Microsoft.Xna.Framework;

using MonoGame.Extended.Entities;


namespace TestGame.Scenes
{
    internal abstract class Scene
    {
        protected WorldBuilder Builder { get; private set; } = new();
        protected World World { get; private set; }
        protected LDGame Game { get; private set; }

        protected virtual void CreateEntities() { }
        protected virtual void CreateSystems() { }

        public void Initialize(LDGame game)
        {
            Game = game;
            CreateSystems();
            World = Builder.Build();
            Builder = null;
            CreateEntities();
        }

        public void Update(GameTime gameTime)
        {
            World.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            World.Draw(gameTime);
        }
    }
}
