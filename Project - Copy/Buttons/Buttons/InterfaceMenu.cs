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
        public TextButton[] buttons;
        Text[] texts;
        public Texture2D background;
        public bool menuOn;
        private Game1 game;

        public InterfaceMenu(TextButton[] buttonArray, Text[] t, Texture2D bg, Game1 game)
        {
            this.game = game;
            buttons = buttonArray;
            texts = t;
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

            game.spriteBatch.Begin();
            game.spriteBatch.Draw(background, new Rectangle(0, 0, game.width, game.height), Color.White);
            game.spriteBatch.End();

            for ( int i = 0; i < buttons.Length; i++){
                buttons[i].Clickable = true;
                buttons[i].Update();
                buttons[i].Draw();
            }
            game.spriteBatch.Begin();
            for (int i = 0; i < texts.Length; i++)
            {
                  game.spriteBatch.DrawString(texts[i].font, texts[i].textValue, texts[i].location, Color.White);
            }
            game.spriteBatch.End();
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