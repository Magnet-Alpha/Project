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



    class InterfaceMenu
    {
        private SpriteBatch spriteBatch;
        public TextButton[] buttons;
        Text[] texts;
        public Texture2D background;
        public bool menuOn;
        GraphicsDeviceManager graphics;

        public InterfaceMenu(TextButton[] buttonArray, Text[] t, Texture2D bg, SpriteBatch sBatch,GraphicsDeviceManager graphics)
        {
            this.graphics = graphics;
            buttons = buttonArray;
            texts = t;
            spriteBatch = sBatch;
            background = bg;
        }

        public bool MenuOn{

            get { return menuOn; }

            set
            {
                for(int i = 0; i < buttons.Length; i++){
                 buttons[i].Clickable = value;
                 buttons[i].clicked = false;
                }
                menuOn = value;
                Draw();
        }
        }
        public void Update()
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].Update();
            }
            
        }

        public void Draw()
        {

            if (!menuOn)
            {
                return;
            }

            spriteBatch.Begin();
            spriteBatch.Draw(background, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);
            spriteBatch.End();

            for ( int i = 0; i < buttons.Length; i++){
                buttons[i].Clickable = true;
                buttons[i].Update();
                buttons[i].Draw();
            }
            spriteBatch.Begin();
            for (int i = 0; i < texts.Length; i++)
            {
                  spriteBatch.DrawString(texts[i].font, texts[i].textValue, texts[i].location, Color.White);
            }
            spriteBatch.End();
        }

        public bool buttonWithIndexPressed(int n)
        {
            return buttons[n].clicked && buttons[n].takingAction;
        }

        private int buttonsTakingAction()
        {
            int n = 0;
            for (int i = 0; i < buttons.Length; i++)
                if(buttons[i].TakingAction())
                    n++;

            return n;
        }

        public void unload()
        {
            menuOn = false;
            for (int i = 0; i < buttons.Length; i++ )
            {
                buttons[i].Clickable = false;
            }
            Draw();
        }


    }


}