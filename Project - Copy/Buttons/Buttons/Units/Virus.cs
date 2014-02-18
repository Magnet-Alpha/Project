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
        protected double Speed { get; set; }
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
            imgs.Add(content.Load<Texture2D>("Sprites\\virus\\virus"));
            imgs.Add(content.Load<Texture2D>("TestSprites\\test attack 1" + this.Name));
            imgs.Add(content.Load<Texture2D>("TestSprites\\test dead 1" + this.Name));
        }
        public void NewPosition(Vector2 E)
        {
            this.Position = this.Position + this.moving * (float)this.Speed * E;
        }
        public void NewSpeed(GameTime gametime)
        {
            this.Speed = this.Speed - 10 * (float)gametime.ElapsedGameTime.TotalMinutes;
        }
        public void Turn(List<Keypoint> keypoints, List<Unit> virus, ref List<int>indexs)
        {
            foreach (Keypoint k in keypoints)
            {
                if (this.Position == k.position)
                {
                    if (k.objectif)
                    {
                        this.etat = Etat.Dead;
                        this.moving = new Vector2(0, 0);
                        indexs.Add(virus.IndexOf(this));
                    }
                    if (k.vers_g)
                    {
                        float z = this.moving.X;
                        this.moving.X = this.moving.Y;
                        this.moving.Y = -z;
                    }
                    else
                    {
                        float z = this.moving.X;
                        this.moving.X = -this.moving.Y;
                        this.moving.Y = z;
                    }
                }
            }
        }

        public override void StateDrawing(float w, float h)
        {
            unitbatch.Draw(imgs[img], new Rectangle((int)this.Position.X, (int)this.Position.Y, 32 * (int)w , 32 * (int)h), new Rectangle(0, 0, 132, 133), Color.White, 0, new Vector2(0, 0), SpriteEffects.None, 0);
        }
    }
}
