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
    class Tower : Unit
    {
        protected Virus target;                                 //Target of the tower
        public double Range { get; set; }                    //Range of attack of the tower
        public double BRange;
        public int BAttack;
        public int BCooldown;
        public int BCost;
        public int level;
        private double p2;
        public bool exist;
        public ContentManager c;
        public Rectangle Hitbox { get; set; }
        public int cout;
        public Point P;
        SoundEffectInstance thro;
        Game1 game;
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
        public Tower(string name, int hp, int attack, int cooldown, int cout, Vector2 position, Point P, double range, ContentManager content, CustomSpriteBatch sb, Etat e, Game1 game) 
            : base(name, hp, attack, cooldown, position, content, sb, e)
        {
            this.Range = range;
            this.p2 = Math.Pow(range, 2);
            this.c = content;
            this.Center = new Vector2(this.Position.X + 16, this.Position.Y + 56);
            imgs.Add(content.Load<Texture2D>("Sprites\\tower\\tour2"));
            imgs.Add(content.Load<Texture2D>("Sprites\\tower\\tourattack2"));
            imgs.Add(content.Load<Texture2D>("TestSprites\\test dead 1" + this.Name));
            this.exist = true;
            this.Hitbox = new Rectangle((int)position.X, (int)position.Y, 32, 64);
            this.cout = cout;
            this.P = P;
            thro = content.Load<SoundEffect>("Sounds\\SFX shoot").CreateInstance();
            this.game = game;
            this.level = 1;
            this.BRange = range;
            this.BAttack = attack;
            this.BCooldown = cooldown;
            this.BCost = cout;
        }

        public Tower()
        {
            this.exist = false;
        }

        public void Attacking(ref List<Projectile> projs)
        {
            if (this.State == Etat.Attack & this.Cooldown <= 0)
            {
                this.Cooldown = this.basecooldown;
                Projectile P = new Projectile(new Vector2 (this.Center.X, this.Center.Y - 48), this.unitbatch, target.Center + 30 * target.moving * (float)target.Speed, this.c, this.target, 1, this.Attack);
                projs.Add(P);
                thro.Volume = game.settings.SoundEffectVolume;
                thro.Play();
            }
            else
            {
                Cooldown--;
            }
        }

        public void Stating(List<Virus> virus)
        {
            if (this.Hp <= 0)
                this.etat = Etat.Dead;
            if (virus.Count == 0)
                this.etat = Etat.Alive;
            foreach (Virus unite in virus)
            {
                if (Math.Pow((unite.Center.X - this.Center.X), 2) + 4 * Math.Pow((-(unite.Center.Y - this.Center.Y)), 2) <= p2 && (this.State == Etat.Alive || this.State == Etat.Attack) && unite.State != Etat.Dead)
                {
                    this.target = unite;
                    unite.State = Etat.Attacked;
                    this.etat = Etat.Attack;
                    break;
                }
                else
                    this.etat = Etat.Alive;
            }
        }

        public override void fuckingcamera(Vector2 L, Vector2 E)
        {
            base.fuckingcamera(L, E);
            this.Center = new Vector2(this.Position.X + 16, this.Position.Y + 56);
            this.Hitbox = new Rectangle((int)this.Position.X, (int)this.Position.Y, 32, 64);
        }

        public void Upgrade()
        {
            if (this.level <= 5)
            {
                this.level++;
                this.Range = this.BRange * (float)(this.level + 1) / 2;
                this.Attack = (int)(this.BAttack * (float)(this.level + 1) / 2);
                this.basecooldown = (int)(this.BCooldown / (float)(this.level + 19) * 20);
                this.cout = (int)(this.BCost * (float)(this.level + 3) / 4);
            }
        }
    } ///Vector2.Distance(this.Position, unite.Position) <= Range
    ///Math.Pow((unite.Position.X - this.Position.X), 2) + 4 * Math.Pow((-(unite.Position.Y - this.Position.Y)), 2) <= p2 
}
