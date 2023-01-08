using Microsoft.Xna.Framework;

using TestGame.Components;
using TestGame.Resources;
using TestGame.UI;


namespace TestGame.Scenes
{
    internal class ShopScene : Scene
    {
        private Inventory inventory;

        public void SetInvetory(Inventory inventory) => this.inventory = inventory;

        protected override void CreateUI()
        {
            CreateLeaveButton();
        }

        private void CreateLeaveButton()
        {
            float screenWidth = Game.Graphics.PreferredBackBufferWidth;
            float screenHeight = Game.Graphics.PreferredBackBufferHeight;

            var button = new Button()
            {
                Apperance = new Apperance()
                {
                    Sprite = new Sprite()
                    {
                        Texture = Game.SpriteManager["scrollsandblocks"],
                        SourceRectange = new Rectangle(0, 64, 96, 32),
                    },
                },
                Font = Game.FontManager[new FontDescriptor()
                {
                    Name = "calibri",
                    FontHeight = 18,
                }],
                Text = "LEAVE",
            };

            button.Apperance.Position = new Vector2()
            {
                X = screenWidth * 0.97f - button.Apperance.Size.X,
                Y = screenHeight * 0.03f,
            };

            button.Clicked += (sender, e) => Game.PopScene();

            UILayer.AddElement(button);
        }

        public void SellItem(Item item)
        {
            
        }
    }
}
