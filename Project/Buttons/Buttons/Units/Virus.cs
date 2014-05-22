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
        public int cout;
        private Texture2D lifebar;
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
        public Virus(string name, int hp, int attack, int cooldown, int cout , Vector2 position, double speed, ContentManager content, CustomSpriteBatch sb, Etat e) : base(name, hp, attack, cooldown, position, content, sb, e)
        {
            this.Speed = speed;
            dir = 3;
            x = 0;
            this.Center = new Vector2(this.Position.X + 16, this.Position.Y + 16);
            imgs.Add(content.Load<Texture2D>("Sprites\\virus\\virus-sprite" + name));
            lifebar = content.Load<Texture2D>("Sprites\\virus\\lifebar");
            this.cout = cout;
        }
        public void NewPosition(Vector2 E)
        {
            this.Position = this.Position + this.moving * (float)this.Speed * new Vector2((int)E.X, (int)E.Y);
            this.Center = new Vector2(this.Position.X + 16, this.Position.Y + 16);
        }
        public void Death(ref int gold, List<Keypoint> k, ref int score)
        {
            if (this.Hp <= 0)
            {
                this.State = Etat.Dead;
                gold += 2;
                score += 5;
            }
        }
        public void Turn(List<Keypoint> keypoints, ref int life)
        {
            foreach (Keypoint k in keypoints)
            {
                if (k.position == this.Position)
                {
                    if (k.objectif)
                    {
                        this.State = Etat.Dead;
                        this.moving = new Vector2(0, 0);
                        life -= 1;
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
            unitbatch.Draw(imgs[img], new Rectangle((int)this.Position.X, (int)this.Position.Y, 32 * (int)w , 32 * (int)h), new Rectangle(((int)x/30) * 32 + 2 , y*32, 32, 32), Color.White, 0, new Vector2(0, 0), SpriteEffects.None, 0);
        }

        public void HUDDraw(float w, float h)
        {
            unitbatch.Draw(lifebar, new Rectangle((int)this.Position.X, (int)this.Position.Y + 36, 32 * (int)w, 4 * (int)h), new Rectangle((int)(8 - 8 * (float)this.Hp / this.maxhp) * 16, 0, 16, 4), Color.White, 0, new Vector2(0, 0), SpriteEffects.None, 0);
        }

        public override void fuckingcamera(Vector2 L, Vector2 E)
        {
            base.fuckingcamera(L, E);
        }
    }

    class Virus1 : Virus
    {
        public Virus1(int cout, Vector2 Position, ContentManager content, CustomSpriteBatch sb)
            : base("1", 100, 10, 5, cout, Position, 1, content, sb, Etat.Alive)
        {
        }
    }

    class Virus2 : Virus
    {
        public Virus2(int cout, Vector2 Position, ContentManager content, CustomSpriteBatch sb)
            : base("1", 200, 5, 5, cout, Position, 0.5, content, sb, Etat.Alive)
        {
        }
    }
    class Virus3 : Virus
    {
        public Virus3(int cout, Vector2 Position, ContentManager content, CustomSpriteBatch sb)
                : base("1", 50, 10, 5, cout, Position, 2, content, sb, Etat.Alive)
            {
            }
    }
}
