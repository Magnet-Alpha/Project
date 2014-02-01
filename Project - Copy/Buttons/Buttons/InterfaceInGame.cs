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


namespace ButtonImage
{
    class InterfaceInGame
    {
        private SpriteBatch spriteBatch;
        ImageButton[] buttons;
        //Texture2D[] imgs;
        public Texture2D background;
        public bool menuOn;

        public InterfaceInGame(ImageButton[] buttonArray,/* Texture2D[] im,*/ Texture2D bg, SpriteBatch sBatch)
        {
            buttons = buttonArray;
            //imgs = im;
            spriteBatch = sBatch;
            background = bg;
        }

        public bool MenuOn
        {

            get { return menuOn; }

            set
            {
                for (int i = 0; i < buttons.Length; i++)
                {
                    buttons[i].Clickable = value;
                    buttons[i].clicked = false;
                }
                menuOn = value;
                Draw(100, 100);
            }
        }
        public void Update()
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].Update();
            }

        }

        public void Draw(int screenwidth, int screenheight)
        {

            if (!menuOn)
            {
                return;
            }

            spriteBatch.Begin();
            spriteBatch.Draw(background, new Rectangle(13 * screenwidth / 16, 0, 2 * screenwidth / 8, screenheight), Color.White);
            spriteBatch.End();

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].Clickable = true;
                buttons[i].Update();
                buttons[i].Draw();
            }
            /*spriteBatch.Begin();
            for (int i = 0; i < imgs.Length; i++)
            {
                  spriteBatch.Draw(imgs[i], new Rectangle(imgs[i]., imgs[i].Width, Color.White);
            }
            spriteBatch.End();*/
        }

        public bool buttonWithIndexPressed(int n)
        {
            return buttons[n].clicked && buttons[n].takingAction;
        }

        private int buttonsTakingAction()
        {
            int n = 0;
            for (int i = 0; i < buttons.Length; i++)
                if (buttons[i].TakingAction())
                    n++;

            return n;
        }
    }
}

