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
        private Vector2 futpos;
        private int speed;
        private int attack;
        private int life;
        private CustomSpriteBatch sb;
        private List<Texture2D> imgs = new List<Texture2D>();
        private Virus target;
        public bool isalive;

        public Projectile(Vector2 position, CustomSpriteBatch sb, Vector2 futpos, ContentManager content, Virus target, int speed, int attack)
        {
            this.position = new Vector2(position.X - 16 * (float)Math.Cos(Math.Atan2(direction.Y, direction.X)), position.Y - 8 * (float)Math.Sin(Math.Atan2(direction.Y, direction.X)));
            this.sb = sb;
            this.futpos = futpos;
            this.direction = new Vector2((futpos.X - position.X), (futpos.Y - position.Y));
            this.target = target;
            this.speed = speed;
            this.attack = attack;
            imgs.Add(content.Load<Texture2D>("Sprites\\tower\\projectile"));
            this.isalive = true;
            this.life = 0;
        }

        public void NewPosition()
        {
            this.position = this.position + this.direction * ((float)this.speed / 30);
            this.life++;
        }

        public void TheCamera(Vector2 L)
        {
            this.position = this.position - L;
        }

        public void Destruction()
        {
            if (this.life == 30)
            {
                target.Hp = target.Hp - this.attack;
                this.isalive = false;
            }
        }

        public void Draw(float w, float h)
        {
            sb.Draw(imgs[0], new Rectangle((int)this.position.X, (int)this.position.Y, 32 * (int)w, 32 * (int)h), new Rectangle(0, 18, 292, 195), Color.White, (float)Math.Atan2(direction.Y, direction.X) - 3*(float)Math.PI/4, new Vector2(0, 0), SpriteEffects.None, 0);
        }
    }
}
