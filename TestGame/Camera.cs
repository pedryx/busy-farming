using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TestGame.Components;


namespace TestGame
{
    internal class Camera
    {
        private readonly GraphicsDeviceManager graphics;

        public Vector2 Position { get; set; }

        public Transform Target { get; set; }

        public Camera(GraphicsDeviceManager graphics)
        {
            this.graphics = graphics;
        }

        public Matrix GetTransform()
        {
            return Matrix.CreateTranslation(
                graphics.PreferredBackBufferWidth / 2 - Position.X,
                graphics.PreferredBackBufferHeight / 2 - Position.Y,
                0
            );
        }

        public void Update()
        {
            if (Target != null)
                Position = Target.Position;
        }
    }
}
