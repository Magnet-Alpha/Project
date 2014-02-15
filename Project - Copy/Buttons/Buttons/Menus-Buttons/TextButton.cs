using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Buttons
{
    public class TextButton : Button
    {
        private SpriteFont font;
        private string text;
        private Game1 game;
        private MouseState mouse;
        private MouseState oldMouse;
        public bool takingAction = false;
        public bool clicked = false;
        Rectangle textRectangle;
        public Vector2 textLocation;
        bool clickable = true;
        SoundEffectInstance click;
        SoundEffectInstance mouseOver;


        public TextButton(SpriteFont font, Game1 game, string t, Vector2 textLoc)
        {
            this.font = font;
            this.game = game;
            text = t;

            textLocation = textLoc;
            textRectangle = new Rectangle((int)textLocation.X,(int) textLocation.Y, (int) font.MeasureString(text).X, (int) font.MeasureString(text).Y);
            click = game.Content.Load<SoundEffect>("Sounds\\click").CreateInstance();
            mouseOver = game.Content.Load<SoundEffect>("Sounds\\mouseover").CreateInstance();
            click.Volume = game.settings.SoundEffectVolume;
            mouseOver.Volume = game.settings.SoundEffectVolume;
        }

        public string Text
        {
            get { return text; }
            set
            {
                text = value;
                Vector2 size = font.MeasureString(text);
                textLocation = new Vector2();
            }
        }
        public bool Clickable
        {
            get { return clickable; }
            set
            {
                clickable = value;
            }
        }
        public bool TakingAction()
        {
            return takingAction;
           
        }
        public int bottom
        {
             get
             {
                return textRectangle.Height + textRectangle.Y;
             }
        }

        public int top
        {
            get { return textRectangle.Y;  }
        }
        public int right
        {
            get { return textRectangle.X + textRectangle.Width; }
        }
        public int left 
        {
            get { return textRectangle.X; }
        }


        public void Location(int x, int y)
        {
            textLocation.X = x;
            textLocation.Y = y;
        }

        public void Update()
        {
            if (!clickable)
                return;

            click.Volume = game.settings.SoundEffectVolume;
            mouseOver.Volume = game.settings.SoundEffectVolume;

            mouse = Mouse.GetState();

            takingAction = false;
            if (mouse.LeftButton == ButtonState.Pressed)
            {
                if (textRectangle.Contains(new Point(mouse.X, mouse.Y)))
                {
                    clicked = true;
                    //Console.WriteLine("Clicked "+text);
                    takingAction = true;

                    click.Play();
                } 
                else
                {
                    clicked = false;
                }

            }

            if (mouse.LeftButton == ButtonState.Released && oldMouse.LeftButton == ButtonState.Pressed && clicked)
            {
                //Console.WriteLine("Action!");
                takingAction = true;
                
            }
            

            oldMouse = mouse;
        }

        public void Draw()
        {
            if (clickable)
            {
                DrawButton(textRectangle);
            }
        }
        private void DrawButton(Rectangle boundaries)
        {
            Vector2 size = font.MeasureString(text);
            float xScale = (boundaries.Width / size.X);
            float yScale = (boundaries.Height / size.Y);
            // Taking the smaller scaling value will result in the text always fitting in the boundaires.
            float scale = Math.Min(xScale, yScale);

            // Figure out the location to absolutely-center it in the boundaries rectangle.
            int strWidth = (int)Math.Round(size.X * scale);
            int strHeight = (int)Math.Round(size.Y * scale);
            Vector2 position = new Vector2();
            position.X = (((boundaries.Width - strWidth) / 2) + boundaries.X);
            position.Y = (((boundaries.Height - strHeight) / 2) + boundaries.Y);

            // A bunch of settings where we just want to use reasonable defaults.
            Vector2 spriteOrigin = new Vector2(0, 0);

            // Draw the string to the sprite batch!

            Color color;

            if (!textRectangle.Contains(new Point(mouse.X, mouse.Y)))
            {
                color = Color.White;
            }
            else
            {
                color = Color.Chocolate;
                if(!textRectangle.Contains(new Point(oldMouse.X, oldMouse.Y)))
                     mouseOver.Play();
            }
            game.spriteBatch.DrawString(font, text, position, color);
            
        }
    }
}
