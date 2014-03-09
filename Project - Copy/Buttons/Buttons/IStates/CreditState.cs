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
    class CreditState : IState
    {
        Game1 game;
        SpriteFont font;
        TextButton back;
        TextButton website;

        public CreditState(Game1 game)
        {
            this.game = game;
            font = game.Content.Load<SpriteFont>("font");
            back = new TextButton(font, game, "Back", new Vector2(20, game.height - 80));
            website = new TextButton(font, game, "Website", new Vector2(game.width / 2 - (int)font.MeasureString("back").X / 2, 230));
        }

        public void Update(GameTime gameTime)
        {
            back.Update();
            website.Update();
            if (back.takingAction)
                ChangeState(new MenuState(game));
            if (website.takingAction)
            {
                System.Diagnostics.Process.Start("http://geekhub.tr.gg");
                game.Exit();
            }
        }
        public void Draw(GameTime gameTime)
        {
            game.spriteBatch.Draw(Textures.background, new Rectangle(0, 0, game.width, game.height), Color.White);
            game.spriteBatch.DrawString(font, "Moray Baruh", new Vector2( game.width / 2 - (int)font.MeasureString("Moray Baruh").X / 2, 30), Color.White);
            game.spriteBatch.DrawString(font, "Guillaume \"Magnet\" Antin", new Vector2(game.width / 2 - (int)font.MeasureString("Guillaume \"Magnet\" Antin").X / 2, 80), Color.White);
            game.spriteBatch.DrawString(font, "Jimmy \"Kojima\" Ha", new Vector2(game.width / 2 - (int)font.MeasureString("Jimmy \"Kojima\" Ha").X / 2, 120), Color.White);
            game.spriteBatch.DrawString(font, "Nicolas \"Toncar\" Carton", new Vector2(game.width / 2 - (int)font.MeasureString("Nicolas \"Toncar\" Carton").X / 2, 170), Color.White);
            back.Draw();
            website.Draw();
        }
        public void Initialize() { }
        public void LoadContent() { }
        public void ChangeState(IState state)
        {
            game.gameState = state;
        }
        public void Window_ClientSizeChanged() { }
    }
}
