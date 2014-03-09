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
    class InterfaceInGame
    {
        private CustomSpriteBatch spriteBatch;
        public ImageButton[] buttons;
        private Game1 game;
        public Text[] texts;
        public Texture2D background;
        public bool menuOn;

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
                game.spriteBatch.Draw(background, new Rectangle(game.width - game.width / 12, 0, game.width / 12, game.height - game.height / 5), Color.White);
                game.spriteBatch.Draw(background, new Rectangle(0, game.height - game.height / 5, game.width, game.height / 5), Color.White);
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
    }
}
