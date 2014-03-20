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
    class Virus : Unit
    {
        public double Speed { get; set; }
        private int dir;
        private int x;
        public override Etat State
        {
            get
            {
                return this.etat;
            }
            set
            {
                this.etat = value;
            }
        }
        public Virus(string name, int hp, int attack, int cooldown, Vector2 position, double speed, ContentManager content, CustomSpriteBatch sb, Etat e) : base(name, hp, attack, cooldown, position, content, sb, e)
        {
            this.Speed = speed;
            dir = 3;
            x = 0;
            imgs.Add(content.Load<Texture2D>("Sprites\\virus\\virus"));
            imgs.Add(content.Load<Texture2D>("TestSprites\\test attack 1" + this.Name));
            imgs.Add(content.Load<Texture2D>("TestSprites\\test dead 1" + this.Name));
        }
        public void NewPosition(Vector2 E)
        {
            this.Position = this.Position + this.moving * (float)this.Speed * E;
        }
        public void Death()
        {
            if (this.Hp <= 0)
            {
                this.State = Etat.Dead;
            }
        }
        public void Turn(List<Keypoint> keypoints)
        {
            foreach (Keypoint k in keypoints)
            {
                if (this.Position == k.position)
                {
                    if (k.objectif)
                    {
                        this.Hp = 0;
                        this.moving = new Vector2(0, 0);
                    }
                    if (k.vers_g)
                    {
                        float z = this.moving.X;
                        this.moving.X = this.moving.Y;
                        this.moving.Y = -z;
                        dir = (dir + 1) % 4;
                    }
                    else
                    {
                        float z = this.moving.X;
                        this.moving.X = -this.moving.Y;
                        this.moving.Y = z;
                        dir = dir - 1;
                        if (dir == -1)
                            dir = 3;
                    }
                }
            }
        }

        public override void StateDrawing(float w, float h)
        {
            int y;
            x = (x + 1) % 60;
            switch (dir)
            {
                case 0: y = 3;
                    break;
                case 1: y = 1;
                    break;
                case 2: y = 2;
                    break;
                default: y = 0;
                    break;
            }
            unitbatch.Draw(imgs[img], new Rectangle((int)this.Position.X, (int)this.Position.Y, 32 * (int)w , 32 * (int)h), new Rectangle(((int)x/30) * 132 , y*133, 132, 133), Color.White, 0, new Vector2(0, 0), SpriteEffects.None, 0);
        }
    }
}
