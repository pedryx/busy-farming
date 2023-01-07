using Microsoft.Xna.Framework.Graphics;

using SpriteFontPlus;

using System;
using System.Collections.Generic;
using System.IO;


namespace TestGame.Resources
{
    internal class FontManager
    {
        private readonly Dictionary<FontDescriptor, SpriteFont> fonts = new();
        private readonly GraphicsDevice graphics;

        public FontManager(GraphicsDevice graphics)
        {
            this.graphics = graphics;
        }

        private void Load(string name, int height)
        {
            string path = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Fonts),
                $"{name}.ttf"
            );

            var descriptor = new FontDescriptor()
            {
                Name = name,
                FontHeight = height,
            };
            var font = TtfFontBaker.Bake(
                File.ReadAllBytes(path),
                height,
                1024,
                1024,
                new[] { CharacterRange.BasicLatin}
            ).CreateSpriteFont(graphics);

            fonts.Add(descriptor, font);
        }

        public SpriteFont this[FontDescriptor descriptor]
        {
            get
            {
                if (!fonts.ContainsKey(descriptor))
                    Load(descriptor.Name, descriptor.FontHeight);

                return fonts[descriptor];
            }
        }
    }

    internal struct FontDescriptor
    {
        public string Name;
        public int FontHeight;
    }
}
