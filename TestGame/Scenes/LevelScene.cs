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
        private const int InitialInventorySize = 10;

        private Inventory inventory;
        private Entity plantGhost;

        public Entity Farm { get; private set; }

        protected override void CreateSystems()
        {
            Builder
                .AddSystem(new RenderSystem(Game.SpriteBatch, Game.Camera))
                .AddSystem(new CameraControlSystem(Game.Camera))
                .AddSystem(new PlantGhostSystem(Game.SpriteBatch, Game.Camera, this))
                .AddSystem(new PlantSystem());
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
            PlantUtils.AppendFarmRow(Farm);
            PlantUtils.AppendFarmRow(Farm);
            PlantUtils.AppendFarmRow(Farm);
            PlantUtils.AppendFarmRow(Farm);
            PlantUtils.AppendFarmRow(Farm);
            PlantUtils.AppendFarmRow(Farm);
            PlantUtils.AppendFarmRow(Farm);
            PlantUtils.AppendFarmRow(Farm);

            inventory = new();
            for (int i = 0; i < InitialInventorySize; i++)
                inventory.Slots.Add(new InventorySlot());
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
                    Name = "calibri",
                    FontHeight = 32,
                }],
            };
            inventoryUI.Sprite.Position = new Vector2()
            {
                X = screenWidth / 2 - inventoryUI.Width / 2,
                Y = screenHeight - inventoryUI.Height,
            };
            UILayer.AddControl(inventoryUI);

            inventory.Slots[1].Plant = PlantType.Potatoe.CreatePlant();
            inventory.Slots[1].Count = 10;

            inventory.Slots[2].Plant = PlantType.Carrots.CreatePlant();
            inventory.Slots[2].Count = 10;

            inventory.Slots[4].Plant = PlantType.Beets.CreatePlant();
            inventory.Slots[4].Count = 10;

            inventory.Slots[5].Plant = PlantType.Garlic.CreatePlant();
            inventory.Slots[5].Count = 10;

            inventory.Slots[6].Plant = PlantType.Pepper.CreatePlant();
            inventory.Slots[6].Count = 10;

            inventory.Slots[7].Plant = PlantType.WaterMelon.CreatePlant();
            inventory.Slots[7].Count = 10;

            inventory.Slots[8].Plant = PlantType.Pumpkin.CreatePlant();
            inventory.Slots[8].Count = 10;

            inventory.Slots[9].Plant = PlantType.Corn.CreatePlant();
            inventory.Slots[9].Count = 10;
        }
    }
}
