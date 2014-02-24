using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Buttons
{
    public static class Textures
    {
        public static Texture2D background;
        public static Texture2D virus;

        public static void Load(ContentManager content)
        {
            background = content.Load<Texture2D>("sprites//background//background");
            virus = content.Load<Texture2D>("sprites//virus//virus-sprite1");
        }

    }
}
