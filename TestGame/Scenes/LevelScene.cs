using Microsoft.Xna.Framework;

using MonoGame.Extended.Entities;

using System;
using System.Collections.Generic;

using TestGame.Components;
using TestGame.Resources;
using TestGame.Systems;
using TestGame.UI;


namespace TestGame.Scenes
{
    internal class LevelScene : Scene
    {
        private const int InitialInventorySize = 4;

        private Inventory inventory;
        private Entity plantGhost;

        public Entity Farm { get; private set; }

        protected override void CreateSystems()
        {
            Builder
                .AddSystem(new RenderSystem(Game.SpriteBatch, Game.Camera))
                .AddSystem(new CameraControlSystem(Game.Camera))
                .AddSystem(new PlantGhostSystem(Game.SpriteBatch, Game.Camera, this));
        }

        protected override void CreateEntities()
        {
            PlantUtils.FarmPlotTexture = Game.SpriteManager["plowed_soil"];
            PlantUtils.PlantsTexture = Game.SpriteManager["crops2"];

            CreateFarm();
        }

        private void CreateFarm()
        {
            Farm = World.CreateEntity();
            Farm.Attach(new Inventory());
            Farm.Attach(new Apperance());
            Farm.Attach(new FarmPlots());

            PlantUtils.AppendFarmRow(Farm);
            PlantUtils.AppendFarmRow(Farm);
            PlantUtils.AppendFarmRow(Farm);

            inventory = new();
            for (int i = 0; i < InitialInventorySize; i++)
                inventory.Slots.Add(null);
            Farm.Attach(inventory);
        }

        public void CreatePlantGhost(Plant plant)
        {
            DestroyPlantGhost();
            plantGhost = World.CreateEntity();
            var sprite = PlantUtils.CreatePlantSprite(plant, 0);
            sprite.Color.A = 127;
            plantGhost.Attach(new PlantGhost()
            {
                Sprite = sprite,
                Plant = plant,
            });
        }

        public void DestroyPlantGhost()
        {
            if (plantGhost != null)
            {
                World.DestroyEntity(plantGhost);
                plantGhost = null;
            }
        }

        protected override void CreateUI()
        {
            float screenWidth = Game.Graphics.PreferredBackBufferWidth;
            float screenHeight = Game.Graphics.PreferredBackBufferHeight;

            InventoryUI inventoryUI = new(inventory, this)
            {
                Sprite = new Sprite()
                {
                    Texture = Game.SpriteManager["scrollsandblocks"],
                    SourceRectange = new Rectangle(96, 224 - 96, 96, 96),
                    Scale = 1.0f,
                    Color = new Color(1, 1, 1, 0.7f),
                },
                Font = Game.FontManager[new FontDescriptor()
                {
                    Name = "arial",
                    FontHeight = 32,
                }],
            };
            inventoryUI.Sprite.Position = new Vector2()
            {
                X = screenWidth / 2 - inventoryUI.Width / 2,
                Y = screenHeight - inventoryUI.Height,
            };
            UILayer.AddControl(inventoryUI);

            inventory.Slots[1] = Plant.RussetPotatoe;
            inventory.Slots[2] = Plant.RandomCorn;

            /*Button testButton = new()
            {
                Transform = new Transform()
                {
                    Position = new Vector2(100, 100),
                },
                Sprite = new Sprite()
                {
                    texture = Game.SpriteManager["scrollsandblocks"],
                    SourceRectange = new Rectangle(0, 64, 96, 32),
                },
            };
            testButton.Clicked += (sender, e) => {
                Console.WriteLine("test");
            };
            UILayer.AddControl(testButton);*/
        }
    }
}
