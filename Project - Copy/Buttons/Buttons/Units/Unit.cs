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
    public enum Etat
    {
        Alive,
        Attack,
        Dead,
        Attacked,
        Moving
    }

    class Unit                                             //In development, DO NOT EDIT WITHOUT PERMISSION
    {
       
        protected CustomSpriteBatch unitbatch;                    //The SpriteBatch
        public string Name { get; set; }                    //Name of the unit (type of unit)
        protected int maxhp;                                //Maximum hp, initialized at the creation of the object, cannot be changed
        public int Hp { get; set; }                         //Actual hp
        public int Attack { get; set; }                     //Unit attack
        protected int basecooldown;                         //base cooldown
        public int Cooldown { get; set; }                   //Actual unit cooldown before attacking again
        public Vector2 Position { get; set; }               //Actual position of the unit
        public Vector2 Center { get; set; }                 //Correction of position
        public Vector2 moving;                              //Actual direction of the unit
        protected Etat etat;
        protected List<Texture2D> imgs = new List<Texture2D>();
        protected int img;
        public virtual Etat State                           //State of the unit (Walking/Attacking/Dead)
        { 
            get
            {
                return this.etat;
            }

            set
            {
                if (this.Hp <= 0)
                    this.etat = Etat.Dead;
                else
                    this.etat = Etat.Alive;
            }
        
        }

        public Unit(string name, int hp, int attack, int cooldown, Vector2 position, ContentManager content, CustomSpriteBatch sb, Etat e)       //Creation of an unit, taking his name, his max hp and his attack. Unit initial position will be his spawn point
        {
            this.Name = name;
            this.maxhp = hp;
            this.Hp = hp;
            this.Attack = attack;
            this.basecooldown = cooldown;
            this.Cooldown = cooldown;
            this.Position = position;
            this.State = Etat.Alive;
            this.moving = new Vector2(0,1);
            this.unitbatch = sb;
            this.etat = e;
        }

        public Unit()
        {
        }

        public void StateDraw(float w, float h)
        {
            if (this.State == Etat.Alive)
                img = 0;
            else if (this.State == Etat.Attack)
                img = 1;
            else if (this.State == Etat.Dead)
                img = 2;
            StateDrawing(w, h);
        }
        public virtual void StateDrawing(float w, float h)
        {
            unitbatch.Draw(imgs[img], new Rectangle((int)this.Position.X, (int)this.Position.Y, 32 * (int)w, 64 * (int)h), Color.White);
        }
        public void fuckingcamera(Vector2 L, Vector2 E)
        {
            this.Position = this.Position - L * E;
        }

        public void TheFullscreen(float w, float h)
        {
            this.Position = new Vector2(this.Position.X * w, this.Position.Y * h);
        }
    }
}
