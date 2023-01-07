using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using MonoGame.Extended.Entities.Systems;


namespace TestGame.Systems
{
    internal class CameraControlSystem : UpdateSystem
    {
        private const float cameraMoveSpeed = 1.0f;

        private readonly Camera camera;

        public CameraControlSystem(Camera camera)
        {
            this.camera = camera;
        }

        public override void Update(GameTime gameTime)
        {
            Vector2 movement = Vector2.Zero;

            var state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.W))
                movement += new Vector2(0, -1);
            if (state.IsKeyDown(Keys.A))
                movement += new Vector2(-1, 0);
            if (state.IsKeyDown(Keys.S))
                movement += new Vector2(0, 1);
            if (state.IsKeyDown(Keys.D))
                movement += new Vector2(1, 0);

            camera.Position += movement 
                * cameraMoveSpeed 
                * LDGame.GameSpeed
                * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        }
    }
}
