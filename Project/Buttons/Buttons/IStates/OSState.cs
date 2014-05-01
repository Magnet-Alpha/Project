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
        MouseState oldMs;
        int n = 0;
        KeyboardState oldKS;
        string[] macStrings = { Strings.stringForKey("MacStory1").Replace("\\n","\n"), Strings.stringForKey("MacStory2").Replace("\\n","\n") };
        string windowsStory = Strings.stringForKey("WindowsStory").Replace("\\n","\n");

        TextButton nextButton;
        

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
            nextButton = new TextButton(font, game, Strings.stringForKey("Next"), new Vector2(game.width - 100, game.height - 80));
            nextButton.Clickable = false;
            nextButton.color = Color.Black;


        }

        public void Update(GameTime gameTime)
        {
            windows.Update();
            apple.Update();

            KeyboardState ks = Keyboard.GetState();
            MouseState ms = Mouse.GetState();

            nextButton.Update();
            if (os != OS.Null)
            {
                nextButton.Clickable = true;

                if (nextButton.takingAction && oldMs.LeftButton == ButtonState.Released)
                {

                    
                    
                    if (os == OS.Windows && n == 1)
                        ChangeState(new GameState(game));

                    if (os == OS.Windows)
                        n++;

                    if (os == OS.Mac && n == 1)
                        ChangeState(new GameState(game));

                    if (os == OS.Mac && n < 2)
                        n++;

                }
            }


            if (windows.takingAction)
                os = OS.Windows;

            if (os == OS.Windows && n == 1 && ks.IsKeyDown(Keys.Enter))
                ChangeState(new GameState(game));

            if (os == OS.Windows && ks.IsKeyDown(Keys.Enter))
                n++;

            if (os == OS.Mac && n == 1 && ks.IsKeyDown(Keys.Enter) && oldKS.IsKeyUp(Keys.Enter))
                ChangeState(new GameState(game));

            if ((os == OS.Mac && n < 2 && ks.IsKeyDown(Keys.Enter) && oldKS.IsKeyUp(Keys.Enter)))
                n++;

            if (apple.takingAction)
                os = OS.Mac;

            if (ks.IsKeyDown(Keys.Escape))
                ChangeState(new MenuState(game));


            oldKS = ks;
            oldMs = ms;


        }
        public void Draw(GameTime gameTime)
        {
            game.spriteBatch.Draw(game.Content.Load<Texture2D>("white"), new Rectangle(0, 0, game.width, game.height), Color.White);
            nextButton.Draw();

            if (os != OS.Null)
            {
                apple.Clickable = false;
                windows.Clickable = false;
                nextButton.Clickable = true;
            }
            else
            {
                game.spriteBatch.Draw(Textures.background, new Rectangle(0, 0, game.width, game.height), Color.White);
            }

            if (os == OS.Null)
            {
                game.spriteBatch.DrawString(font, Strings.stringForKey("ChooseOS"), new Vector2(game.width / 2 - font.MeasureString("Choose your OS").X / 2, 50), Color.White);
                apple.Draw();
                windows.Draw();
            }
            if (os == OS.Mac && n < 2)
            {
                string str = macStrings[n];
                game.spriteBatch.DrawString(font, str, new Vector2(game.width / 2 - font.MeasureString(str).X / 2, game.height / 2 - font.MeasureString(str).Y / 2), Color.Black);
            }
            if (os == OS.Windows)
                game.spriteBatch.DrawString(font, windowsStory, new Vector2(game.width / 2 - font.MeasureString(windowsStory).X / 2, game.height / 2 - font.MeasureString(windowsStory).Y / 2), Color.Black);


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
