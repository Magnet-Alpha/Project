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
        public Vector2 Coordinate { get; set; }
        public bool vers_g;
        public bool objectif;
        public Keypoint(Vector2 position, bool vers_g, bool objectif)
        {
            this.position = position;
            this.vers_g = vers_g;
            this.objectif = objectif;
            this.Coordinate = new Vector2((int)this.position.X / 64, (int)this.position.Y / 32);
        }

        public void TheCamera(Vector2 L, Vector2 E)
        {
            this.position = this.position - L*E;
            this.Coordinate = new Vector2((int)this.position.X / 64, (int)this.position.Y / 32);
        }

        public void TheFullscreen(float w, float h)
        {
            this.position = new Vector2(this.position.X * w, this.position.Y * h);
        }
    }
}
