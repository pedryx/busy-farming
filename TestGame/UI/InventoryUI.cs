using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;

using TestGame.Components;
using TestGame.Scenes;

namespace TestGame.UI
{
    internal class InventoryUI : UIControl
    {
        private readonly Inventory inventory;
        private readonly LevelScene scene;

        private bool lastMousePressed = false;
        private bool lastEscapePressed = false;

        public SpriteFont Font { get; set; }

        public float Width => inventory.Slots.Count * Sprite.Size.X;

        public float Height => Sprite.Size.Y;

        public InventoryUI(Inventory inventory, LevelScene scene)
        {
            this.inventory = inventory;
            this.scene = scene;
        }

        public override void Update()
        {
            var mouseState = Mouse.GetState();
            bool currentMouse = mouseState.LeftButton == ButtonState.Pressed;
            if (!currentMouse && lastMousePressed)
            {
                var mousePosition = mouseState.Position;

                if (
                    mousePosition.X > Sprite.Position.X &&
                    mousePosition.Y > Sprite.Position.Y &&
                    mousePosition.X < Sprite.Position.X + Width &&
                    mousePosition.Y < Sprite.Position.Y + Height
                )
                {
                    inventory.Selected = (int)((mousePosition.X - Sprite.Position.X) / Sprite.Size.X);
                    scene.CreatePlantGhost(inventory.Slots[inventory.Selected]);
                }
            }

            var keyboardState = Keyboard.GetState();
            bool currentEscapeKey = keyboardState.IsKeyDown(Keys.Escape);
            if (!lastEscapePressed && currentEscapeKey)
            {
                inventory.Selected = -1;
                scene.DestroyPlantGhost();
            }

            lastMousePressed = currentMouse;
            lastEscapePressed = currentEscapeKey;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            float offsetX = Sprite.Position.X;
            for (int i = 0; i < inventory.Slots.Count; ++i)
            {
                Sprite.Position.X = offsetX + i * Sprite.Size.X;
                if (inventory.Selected == i)
                {
                    var sourceRectangle = Sprite.SourceRectange.Value;
                    sourceRectangle.X -= sourceRectangle.Width;
                    Sprite.SourceRectange = sourceRectangle;
                    Sprite.Draw(spriteBatch);
                    sourceRectangle.X += sourceRectangle.Width;
                    Sprite.SourceRectange = sourceRectangle;
                }
                else
                {
                    Sprite.Draw(spriteBatch);
                }

                if (inventory.Slots[i] != null)
                {
                    var plantSprite = inventory.Slots[i].InventorySprite;
                    plantSprite.Scale = (Sprite.Size.Y * 0.8f) / plantSprite.NotScaledSize.Y;
                    plantSprite.Position = Sprite.Position + Sprite.Size / 2 - plantSprite.Size / 2;

                    plantSprite.Draw(spriteBatch);
                }
            }
            Sprite.Position.X = offsetX;
        }
    }
}
