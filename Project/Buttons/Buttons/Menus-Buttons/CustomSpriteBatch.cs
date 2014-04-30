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
    public class CustomSpriteBatch
    {
        public SpriteBatch spriteBatch;

        public CustomSpriteBatch(GraphicsDevice gd)
        {
            spriteBatch = new SpriteBatch(gd);
        }

        public void Draw(Texture2D texture, Rectangle rectangle, Color color)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texture, rectangle, color);
            spriteBatch.End();
        }

        public void DrawString(SpriteFont font, String str, Vector2 vector,Color color)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(font, str, vector, color);
            spriteBatch.End();
        }
       public void Draw (Texture2D texture, Rectangle rectangle, Nullable<Rectangle> rec, Color color, Single single1, Vector2 vector, SpriteEffects spriteEffects, Single single2)
     {
         spriteBatch.Begin();
         spriteBatch.Draw(texture, rectangle, rec, color, single1, vector, spriteEffects, single2);
         spriteBatch.End();
     }

    }
}
