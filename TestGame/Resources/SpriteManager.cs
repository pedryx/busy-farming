using Microsoft.Xna.Framework.Graphics;


namespace TestGame.Resources
{
    internal class SpriteManager : ResourcesManager<Texture2D>
    {
        private readonly GraphicsDevice device;

        public SpriteManager(GraphicsDevice device)
            : base("png", "Content/Textures") 
        {
            this.device = device;
        }

        public override Texture2D Load(string path)
        {
            var texture = Texture2D.FromFile(device, path);
            texture.Name = path;

            return texture;
        }
    }
}
