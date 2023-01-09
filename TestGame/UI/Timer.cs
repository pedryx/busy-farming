using Microsoft.Xna.Framework;

using System;


namespace TestGame.UI
{
    internal class Timer : Label
    {
        private float reamingTime;

        public event EventHandler TimeUp;

        public Timer(float startTime)
        {
            reamingTime = startTime;
            Text = "00:00";
        }

        public override void Update(GameTime gameTime)
        {
            reamingTime -= (float)gameTime.ElapsedGameTime.TotalSeconds * LDGame.GameSpeed;
            if (reamingTime <= 0)
                TimeUp?.Invoke(this, new EventArgs());

            int minutes = (int)(reamingTime / 60);
            int seconds = (int)(reamingTime % 60);

            Text = minutes.ToString().PadLeft(2, '0') + ':' + seconds.ToString().PadLeft(2, '0');

            base.Update(gameTime);
        }
    }
}
