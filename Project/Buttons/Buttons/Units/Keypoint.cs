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
        public Rectangle sure;
        public List<Virus> list;
        public bool vers_g;
        public bool objectif;
        public Keypoint(Vector2 position, bool vers_g, bool objectif)
        {
            this.position = position;
            this.vers_g = vers_g;
            this.objectif = objectif;
            this.Coordinate = new Vector2((int)this.position.X / 64, (int)this.position.Y / 32);
            list = new List<Virus>();
            sure = new Rectangle((int)this.position.X - 2, (int)this.position.Y - 2, (int)this.position.X + 2, (int)this.position.Y + 2);
        }

        public void TheCamera(Vector2 L, Vector2 E)
        {
            this.position = this.position - L*E;
            this.Coordinate = new Vector2((int)(this.position.X - 16 + Camera.Location.X) / 64, (int)(this.position.Y + Camera.Location.Y) / 32);
            this.sure = new Rectangle((int)this.position.X - 4, (int)this.position.Y - 4, (int)this.position.X + 4, (int)this.position.Y + 4);
        }

        public void TheFullscreen(float w, float h)
        {
            this.position = new Vector2((int)this.position.X * w, (int)this.position.Y * h);
        }

        public void Check(Virus v)
        {
            if (list.Contains(v))
                list.Remove(v);
        }
    }
}
