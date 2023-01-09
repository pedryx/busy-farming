using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MonoGame.Extended.Entities.Systems;

using System;
using System.Collections.Generic;

using TestGame.Components;


namespace TestGame.Systems
{
    internal class WeatherSystem : DrawSystem
    {
        private readonly Apperance heatWave;
        private readonly Color heatWaveColor = new(100, 0, 0, 1);

        private const int rainDropCount = 1_000;
        private const float minRainDropSpeed = 500.0f;
        private const float maxRainDropSpeed = 1000.0f;
        private readonly Vector2 rainDropSize = new(2, 6);

        private readonly Random random = new();
        private readonly Texture2D texture;
        private readonly List<RainDrop> rainDrops = new();
        private readonly Vector2 windowSize;
        private readonly SpriteBatch spriteBatch;

        private readonly float nextWeatherEvent = 30;
        private readonly float weatherEventDuration = 10;
        private float ellapsed;
        private WeatherEvent activeEvent;

        public WeatherSystem(LDGame game)
        {
            spriteBatch = game.SpriteBatch;

            texture = new Texture2D(game.GraphicsDevice, 1, 1);
            texture.SetData(new Color[] { Color.White });

            windowSize = new Vector2(
                game.Graphics.PreferredBackBufferWidth,
                game.Graphics.PreferredBackBufferHeight
            );

            heatWave = new Apperance()
            {
                Scale = windowSize,
                Sprite = new Sprite()
                {
                    Color = heatWaveColor,
                    Texture = texture,
                },
            };

            InvokeEvent();
        }

        public override void Draw(GameTime gameTime)
        {
            // update event
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds 
                * LDGame.GameSpeed;
            ellapsed += deltaTime;

            if (activeEvent == WeatherEvent.None && ellapsed >= nextWeatherEvent)
            {
                ellapsed -= nextWeatherEvent;

                var events = Enum.GetValues<WeatherEvent>();
                activeEvent = events[random.Next(1, events.Length)];
                InvokeEvent();
            }
            else if (activeEvent != WeatherEvent.None && ellapsed >= weatherEventDuration)
            {
                ellapsed -= weatherEventDuration;
                activeEvent = WeatherEvent.None;
                GlobalModifiers.WaterDecreaseSpeed = 1.0f;
            }

            // draw rain
            spriteBatch.Begin();
            for (int i = rainDrops.Count - 1; i >= 0; i--)
            {
                rainDrops[i].apperance.Position.Y += rainDrops[i].Speed * deltaTime;
                if (rainDrops[i].apperance.Position.Y > windowSize.Y)
                    rainDrops.RemoveAt(i);
                else
                    rainDrops[i].apperance.Draw(spriteBatch);
            }

            // update event
            switch (activeEvent)
            {
                case WeatherEvent.None:
                    break;
                case WeatherEvent.Rain:
                    UpdateRain();
                    break;
                case WeatherEvent.HeatWave:
                    DrawHeatWave();
                    break;
            }
            spriteBatch.End();
        }

        private void InvokeEvent()
        {
            switch (activeEvent)
            {
                case WeatherEvent.Rain:
                    GlobalModifiers.WaterDecreaseSpeed = 0.0f;
                    break;
                case WeatherEvent.HeatWave:
                    GlobalModifiers.WaterDecreaseSpeed *= 2;
                    break;
            }
        }

        private void UpdateRain()
        {
            int toSpawn = rainDropCount - rainDrops.Count;
            for (int i = 0; i < toSpawn; i++)
            {
                rainDrops.Add(new RainDrop()
                {
                    Speed = random.Next((int)minRainDropSpeed, (int)maxRainDropSpeed)
                        + random.NextSingle(),
                    apperance = new Apperance()
                    {
                        Position = new Vector2()
                        {
                            X = random.Next((int)windowSize.X),
                            Y = -random.Next((int)rainDropSize.Y, (int)windowSize.Y)
                        },
                        Scale = rainDropSize,
                        Sprite = new Sprite()
                        {
                            Color = Color.Aqua,
                            Texture = texture,
                        },
                    },
                });
            }
        }

        private void DrawHeatWave()
            => heatWave.Draw(spriteBatch);

        private enum WeatherEvent
        {
            None,
            Rain,
            HeatWave,
        }

        private class RainDrop
        {
            public float Speed;
            public Apperance apperance;
        }
    }
}
