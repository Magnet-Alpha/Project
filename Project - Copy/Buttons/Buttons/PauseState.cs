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
    public class PauseState : IState
    {
        Game1 game;
        GameState gameState;
        Texture2D background;
        InterfaceMenu pauseMenu;
        SpriteFont font;
        KeyboardState oldKs;
        


        public PauseState(GameState gameState, Game1 game)
        {
            this.game = game;
            this.gameState = gameState;
            font = game.Content.Load<SpriteFont>("Font");
            Initialize();
            LoadContent();
            oldKs = Keyboard.GetState();
        }
        public void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            if ((ks.IsKeyDown(Keys.Escape) && !oldKs.IsKeyDown(Keys.Escape) || pauseMenu.buttonWithIndexPressed(0)))
            {
                ChangeState(gameState);
                gameState.status = GameStateStatus.InGame;
            }
            if (pauseMenu.buttonWithIndexPressed(1))
            {
                pauseMenu.MenuOn = false;
                ChangeState(new OptionsState(game, this));
            }
            if (pauseMenu.buttonWithIndexPressed(2))
            {
                pauseMenu.MenuOn = false;
                ChangeState(new MenuState(game));
            }
           
            oldKs = ks;
            pauseMenu.Update();

        }
        public void Draw(GameTime gameTime)
        {
            pauseMenu.Draw();

           
        }
        public void Initialize()
        {
            background = game.Content.Load<Texture2D>("BG");

            TextButton returnGameButton = new TextButton(font, game, "Resume game", new Vector2(game.width / 2 - font.MeasureString("Resume game").X / 2, 200));
            TextButton optionsButton = new TextButton(font, game, "Options", new Vector2(game.width / 2 - font.MeasureString("Options").X / 2, returnGameButton.textLocation.Y + font.MeasureString("Options").Y + 20));
            TextButton returnMainMenuButton = new TextButton(font, game, "Return to main menu", new Vector2(game.width / 2 - font.MeasureString("Return to main menu").X / 2, optionsButton.textLocation.Y + font.MeasureString("Return to main menu").Y + 20));
            pauseMenu = new InterfaceMenu(new TextButton[3] { returnGameButton, optionsButton, returnMainMenuButton }, new Text[1] { new Text("Pause Menu", new Vector2(game.width / 2 - font.MeasureString("Pause Menu").X / 2, 50), font) }, background, game);

            pauseMenu.MenuOn = true;




        }


        public void LoadContent()
        {
            
           
        }
        public void Window_ClientSizeChanged(object sender, EventArgs e)
        {
            Initialize();
        }

        public void ChangeState(IState state) {
            state.LoadContent();
            game.gameState = state;    
        }
    }
}
