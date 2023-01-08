using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System.Collections.Generic;

using TestGame.Resources;
using TestGame.Scenes;


namespace TestGame
{
    internal class LDGame : Game
    {
        public static float GameSpeed = 1.0f;

        private readonly Color clearColor = new(47, 129, 54);
        private readonly Stack<Scene> sceneStack = new();

        public Scene CurrentScene => sceneStack.Peek();
        public GraphicsDeviceManager Graphics { get; private set; }
        public SpriteManager SpriteManager { get; private set; }
        public FontManager FontManager { get; private set; }
        public SpriteBatch SpriteBatch { get; private set; }
        public Camera Camera { get; private set; } = new();

        public LDGame()
        {
            Graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 1280,
                PreferredBackBufferHeight = 720,
            };
            IsMouseVisible = true;
        }

        public void Run<T>()
            where T : Scene, new()
        {
            sceneStack.Push(new T());
            Run();
        }

        public void PushScene<T>()
            where T : Scene, new()
        {
            var scene = new T();
            scene.Initialize(this);
            PushScene(scene);
        }

        public void PushScene(Scene scene)
            => sceneStack.Push(scene);

        public void PopScene() => sceneStack.Pop();

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            SpriteManager = new SpriteManager(GraphicsDevice);
            FontManager = new FontManager(GraphicsDevice);

            SpriteManager.LoadAll();

            sceneStack.Peek().Initialize(this);
            PlantType.PrepareTextures(this);

            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            Input.Update(Camera);
            sceneStack.Peek().Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            Graphics.GraphicsDevice.Clear(clearColor);
            sceneStack.Peek().Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}
