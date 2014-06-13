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
    public class InterfaceInGame
    {
        private CustomSpriteBatch spriteBatch;
        public ImageButton[] buttons;
        public TextButton[] Tbuttons;
        private Game1 game;
        public Text[] texts;
        public Texture2D background;
        public bool menuOn;
        public bool TmenuOn;

        public InterfaceInGame(ImageButton[] buttonArray, Game1 game, Text[] t, Texture2D bg, CustomSpriteBatch sBatch)
        {
            this.game = game;
            buttons = buttonArray;
            texts = t;
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
            else
            {
                game.spriteBatch.Draw(background, new Rectangle(game.width - 800 / 12, 0, 800 / 12, game.height - game.height / 10), Color.White);
                game.spriteBatch.Draw(background, new Rectangle(0, game.height - 120, game.width, 120), Color.White);
            }
            for (int i = 0; i < buttons.Length; i++)
            {
                    buttons[i].Clickable = true;
                    buttons[i].Update();
                    buttons[i].Draw();
            }
            for (int i = 0; i < texts.Length; i++)
            {
                game.spriteBatch.DrawString(texts[i].font, texts[i].textValue, texts[i].location, Color.White);
            }
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

        public InterfaceInGame(TextButton[] buttonArray, Game1 game, Text[] t, Texture2D bg, CustomSpriteBatch sBatch)
        {
            this.game = game;
            Tbuttons = buttonArray;
            texts = t;
            spriteBatch = sBatch;
            background = bg;
        }

        public bool TMenuOn
        {

            get { return TmenuOn; }

            set
            {
                for (int i = 0; i < Tbuttons.Length; i++)
                {
                    Tbuttons[i].Clickable = value;
                    Tbuttons[i].clicked = false;
                }
                TmenuOn = value;
                Draw();
            }
        }
        public void TUpdate()
        {
            for (int i = 0; i < Tbuttons.Length; i++)
            {
                Tbuttons[i].Update();
            }

        }

        public void TDraw()
        {

            if (!TmenuOn)
            {
                return;
            }
            else
            {
                game.spriteBatch.Draw(background, new Rectangle(game.width - 800 / 12, 0, 800 / 12, game.height - game.height / 10), Color.White);
                game.spriteBatch.Draw(background, new Rectangle(0, game.height - 120, game.width, 120), Color.White);
            }
            for (int i = 0; i < Tbuttons.Length; i++)
            {
                Tbuttons[i].Clickable = true;
                Tbuttons[i].Update();
                Tbuttons[i].Draw();
            }
            for (int i = 0; i < texts.Length; i++)
            {
                game.spriteBatch.DrawString(texts[i].font, texts[i].textValue, texts[i].location, Color.White);
            }
        }

        public bool TbuttonWithIndexPressed(int n)
        {
            return Tbuttons[n].clicked && Tbuttons[n].takingAction;
        }

        private int TbuttonsTakingAction()
        {
            int n = 0;
            for (int i = 0; i < Tbuttons.Length; i++)
                if (Tbuttons[i].TakingAction())
                    n++;

            return n;
        }
    }
}
