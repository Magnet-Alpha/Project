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
    class Screen
    {
        public Texture2D background;
        public Text[] texts;
        SpriteBatch spriteBatch;
        public bool isOn;

        public Screen (Texture2D bg, Text[] t, SpriteBatch sBatch)
        {
            background = bg;
            texts = t;
            spriteBatch = sBatch;
            isOn = false;
        }

        public bool IsOn 
        {
            get { return isOn; }
            set
            {
                isOn = value;
                Draw();
            }
        }

        public void Draw()
        {
            if (!isOn)
                return;
            spriteBatch.Begin();
            spriteBatch.Draw(background, new Rectangle(0, 0, 800, 480), Color.White);
            for (int i = 0; i < texts.Length; i++)
            {
                spriteBatch.DrawString(texts[i].font, texts[i].textValue, texts[i].location, Color.White);
            }
            spriteBatch.End();
        }

    }
}
