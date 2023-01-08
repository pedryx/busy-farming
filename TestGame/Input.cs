using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace TestGame
{
    internal static class Input
    {
        private static bool lastLeftButtonState;
        private static bool lastRightButtonState;

        public static Vector2 MousePosition { get; private set; }
        public static Vector2 MousePositionTransformed { get; private set; }
        public static bool LeftMouseClicked { get; private set; }
        public static bool RightMouseClicked { get; private set; }

        public static void Update(Camera camera)
        {
            var state = Mouse.GetState();

            // left button
            bool currentLeftButtonState = state.LeftButton == ButtonState.Pressed;
            LeftMouseClicked = lastLeftButtonState && !currentLeftButtonState;
            lastLeftButtonState = currentLeftButtonState;

            // right button
            bool currentRightButtonState = state.RightButton == ButtonState.Pressed;
            RightMouseClicked = lastRightButtonState && !currentRightButtonState;
            lastRightButtonState = currentRightButtonState;

            // mouse position
            MousePosition = new Vector2(state.X, state.Y);

            // mouse position transformed
            MousePositionTransformed = Vector2.Transform(
                MousePosition,
                Matrix.Invert(camera.GetTransform())
            );
        }
    }
}
