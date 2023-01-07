using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TestGame.Resources;
using TestGame.Scenes;


namespace TestGame
{
    internal class LDGame : Game
    {
        public static float GameSpeed = 1.0f;

        private readonly Color clearColor = new(47, 129, 54);
        private readonly GraphicsDeviceManager graphics;
        private Scene currentScene;

        public SpriteManager SpriteManager { get; private set; }
        public SpriteBatch SpriteBatch { get; private set; }
        public Camera Camera { get; private set; }

        public LDGame()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 800,
                PreferredBackBufferHeight = 600,
            };
            IsMouseVisible = true;
            Camera = new Camera(graphics);
        }

        public void Run<T>()
            where T : Scene, new()
        {
            currentScene = new T();
            Run();
        }

        public void SwitchScene<T>()
            where T : Scene, new()
        {
            currentScene = new T();
            currentScene.Initialize(this);
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            SpriteManager = new SpriteManager(GraphicsDevice);

            SpriteManager.LoadAll();

            currentScene.Initialize(this);

            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            Camera.Update();
            currentScene.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(clearColor);
            currentScene.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}
