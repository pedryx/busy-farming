using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TestGame.Components;


namespace TestGame
{
    internal class Camera
    {
        public Vector2 Position { get; set; }

        public Matrix GetTransform()
        {
            return Matrix.CreateTranslation(
                Position.X,
                Position.Y,
                0
            );
        }
    }
}
