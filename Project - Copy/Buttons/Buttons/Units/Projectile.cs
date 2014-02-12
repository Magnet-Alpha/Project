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
    class Projectile
    {
        private Vector2 direction;
        private Vector2 position;
        private SpriteBatch sb;
        private GameTime origin;
        private GameTime lifespan;

        public Projectile(Vector2 direction, SpriteBatch sb, GameTime origin, GameTime lifespan)
        {
            this.sb = sb;
            this.direction = direction;
            this.origin = origin;
            this.lifespan = lifespan;
        }

        public void NewPosition()
        {
            this.position = this.position + this.direction;
        }

        public void TheCamera(Vector2 L)
        {
            this.position = this.position - L;
        }

        public void Destruction(GameTime time)
        {
            if (time.TotalGameTime.Milliseconds - this.origin.TotalGameTime.Milliseconds > this.lifespan.TotalGameTime.Milliseconds)
            {

            }
        }
    }
}
