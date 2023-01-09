using TestGame.Components;
using TestGame.Resources;
using TestGame.UI;


namespace TestGame.Scenes
{
    internal class GameOverScene : Scene
    {
        private readonly int coins;

        public GameOverScene(int coins)
        {
            this.coins = coins;
        }

        protected override void CreateUI()
        {
            string text = $"Scored Coins: {coins}\n" +
                $"Plants Planted: {GlobalStatistics.PlantsPlanted}\n" +
                $"Plants Harvested: {GlobalStatistics.PlantsHarvested}";

            var label = new Label()
            {
                Apperance = new Apperance(),
                Font = Game.FontManager[new FontDescriptor()
                {
                    Name = "calibri",
                    FontHeight = 128,
                }],
                Text = text,
            };

            label.Apperance.Position = Game.WindowSize / 2 - label.Size / 2;

            UILayer.AddElement(label);
        }
    }
}
