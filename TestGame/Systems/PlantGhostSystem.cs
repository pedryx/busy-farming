using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MonoGame.Extended.Entities;

using System;
using System.Collections.Generic;

using TestGame.Components;
using TestGame.Scenes;

namespace TestGame.Systems
{
    internal class PlantGhostSystem : EntityDrawSystem<PlantGhost>
    {
        private readonly Camera camera;
        private readonly LevelScene scene;

        private Vector2 position;
        private bool lastClicked = false;
        private bool currentClicked = false;

        public PlantGhostSystem(
            SpriteBatch spriteBatch,
            Camera camera,
            LevelScene scene
        )
            : base(spriteBatch)
        {
            this.camera = camera;
            this.scene = scene;
        }

        public override void PreDraw(GameTime gameTime)
        {
            var state = Mouse.GetState();
            position = Vector2.Transform(
                new Vector2(state.X, state.Y),
                Matrix.Invert(camera.GetTransform())

            );
            lastClicked = currentClicked;
            currentClicked = state.LeftButton == ButtonState.Pressed;

            SpriteBatch.Begin(transformMatrix: camera.GetTransform());
        }

        public override void PostDraw(GameTime gameTime)
        {
            SpriteBatch.End();
        }

        public override void Draw(PlantGhost plantGhost, GameTime gameTime)
        {
            var ghostSprite = plantGhost.Sprite;
            var sprites = scene.Farm.Get<Apperance>().Sprites;
            var farmPlots = scene.Farm.Get<FarmPlots>().plants;
            for (int y = 1; y < farmPlots.Count - 1; y++)
            {
                for (int x = 1; x < farmPlots[0].Count - 1; x++)
                {
                    var sprite = sprites[y * farmPlots[0].Count + x];
                    var rectangle = new Rectangle(sprite.Position.ToPoint(), sprite.Size.ToPoint());
                    if (rectangle.Contains(position) && farmPlots[y][x] == null)
                    {
                        ghostSprite.Scale = (sprite.Size.Y * 1.5f) / ghostSprite.NotScaledSize.Y;
                        ghostSprite.Position = sprite.Position + sprite.Size / 2 - ghostSprite.Size / 2;
                        plantGhost.Sprite.Draw(SpriteBatch);

                        if (lastClicked && !currentClicked)
                        {
                            var plant = plantGhost.Plant.Clone();
                            farmPlots[y][x] = plant;
                            plant.X = x;
                            plant.Y = y;

                            var plantEntity = scene.World.CreateEntity();
                            plantEntity.Attach(farmPlots[y][x]);
                            plantEntity.Attach(new Apperance()
                            {
                                Sprites = new List<Sprite>()
                                {
                                    PlantUtils.CreatePlantSprite(
                                        plant,
                                        0,
                                        ghostSprite.Position,
                                        ghostSprite.Scale
                                    ),
                                },
                            });

                            var inventory = scene.Farm.Get<Inventory>();
                            inventory.Slots[inventory.Selected].Count--;
                            if (inventory.Slots[inventory.Selected].Count == 0)
                            {
                                inventory.Slots[inventory.Selected].Plant = null;
                                inventory.Selected = -1;
                                scene.DestroyPlantGhost();
                            }
                        }
                        break;
                    }
                }
            }
        }
    }
}
