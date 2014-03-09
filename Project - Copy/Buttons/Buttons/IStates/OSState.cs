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
    class OSState : IState
    {

        Game1 game;
        ImageButton apple;
        ImageButton windows;
        SpriteFont font;
        OS os = OS.Null;

        public enum OS
        {
            Mac,
            Windows,
            Null
        }
        public OSState(Game1 game)
        {
            this.game = game;
            Texture2D appleLogo = game.Content.Load<Texture2D>("Sprites\\os\\apple");
            apple = new ImageButton(appleLogo, new Rectangle(game.width / 3, 100, 50, 50), game);
            windows = new ImageButton(game.Content.Load<Texture2D>("Sprites\\os\\windows"), new Rectangle(2 * game.width / 3, 100, 50, 50), game);
            font = game.Content.Load<SpriteFont>("font");
        }

        public void Update(GameTime gameTime)
        {
            windows.Update();
            apple.Update();
            if (windows.takingAction)
                ChangeState(new GameState(game));

            if (os == OS.Mac && Keyboard.GetState().IsKeyDown(Keys.Enter))
                ChangeState(new GameState(game));

            
            if (apple.takingAction)
                os = OS.Mac;
            
        }
        public void Draw(GameTime gameTime)
        {
            game.spriteBatch.Draw(Textures.background, new Rectangle(0, 0, game.width, game.height), Color.White);
            if (os == OS.Null)
            {
                game.spriteBatch.DrawString(font, "Choose your OS", new Vector2(game.width / 2 - font.MeasureString("Choose your OS").X / 2, 50), Color.White);
                apple.Draw();
                windows.Draw();
            }
            if (os == OS.Mac)
            {
                string str = "All viruses eliminated, you won the game.\nJust kidding, starting the game with Windows.";
                game.spriteBatch.DrawString(font, str, new Vector2(game.width/2 - font.MeasureString(str).X/2, game.height/2 - font.MeasureString(str).Y/2),Color.White);
            }



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
