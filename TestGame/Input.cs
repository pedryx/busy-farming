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
            if (lastLeftButtonState && !currentLeftButtonState)
                LeftMouseClicked = true;
            lastLeftButtonState = currentLeftButtonState;

            // right button
            bool currentRightButtonState = state.LeftButton == ButtonState.Pressed;
            if (lastRightButtonState && !currentRightButtonState)
                RightMouseClicked = true;
            lastRightButtonState = currentRightButtonState;

            // mouse position
            MousePosition = new Vector2(state.X, state.Y);

            // transformed mouse position
            MousePositionTransformed = Vector2.Transform(
                MousePosition,
                Matrix.Invert(camera.GetTransform())
            );
        }
    }
}
