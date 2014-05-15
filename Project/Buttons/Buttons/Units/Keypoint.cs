using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Buttons
{
    class Keypoint
    {
        public Vector2 position;
        public bool vers_g;
        public bool objectif;
        public Keypoint(Vector2 position, bool vers_g)
        {
            this.position = position;
            this.vers_g = vers_g;
            this.objectif = false;
        }

        public void TheCamera(Vector2 L)
        {
            this.position = this.position - L;
        }
    }

    class Objective : Keypoint
    {
        public Objective(Vector2 position)
            : base(position, false)
        {
            this.objectif = true;
        }
    }

    class Start
    {
        public Vector2 position;
        public Start(Vector2 position)
        {
            this.position = position;
        }
        public void TheCamera(Vector2 L)
        {
            this.position = this.position - L;
        }
    }
}
