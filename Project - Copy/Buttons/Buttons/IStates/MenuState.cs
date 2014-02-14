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
    public class MenuState : IState
    {
        public bool isActive = true;
        TextButton newGameButton;
        TextButton continueButton;
        TextButton optionsButton;
        TextButton exitButton;
        SpriteFont font;
        Texture2D background;
        InterfaceMenu mainMenu;
        int gap;
        Game1 game;

        public MenuState(Game1 game)
        {
            this.game = game;
            LoadContent();
            Initialize();
            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        public void Initialize()
        {
            // TODO: Add your initialization logic here

            
            mainMenu.MenuOn = true;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        public void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures. 

            background = game.Content.Load<Texture2D>("background");

            font = game.Content.Load<SpriteFont>("Font");
            gap = (int)font.MeasureString("L").Y;
            //menu general
            
            continueButton = new TextButton(font, game, "Multiplayer", new Vector2(40, game.height/2 - gap/2));

            newGameButton = new TextButton(font, game, "New Game", new Vector2(30, continueButton.top - gap));
            optionsButton = new TextButton(font, game, "Options", new Vector2(50, game.height/2 + gap/2));
            exitButton = new TextButton(font, game, "Exit", new Vector2(75, optionsButton.bottom + gap - (int) font.MeasureString("Exit").Y));
            
            mainMenu = new InterfaceMenu(new TextButton[] { newGameButton, continueButton, optionsButton, exitButton }, new Text[] { }, background, game);
            mainMenu.MenuOn = true;
            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                game.Exit();

            mainMenu.Update();

            if (mainMenu.buttonWithIndexPressed(0)) // New Game
            {
                ChangeState(new GameState(game));
            }

            if (mainMenu.buttonWithIndexPressed(1))
                game.gameState = new MultiplayerState(game);


            if (mainMenu.buttonWithIndexPressed(2)) // Options
            {
                ChangeState(new OptionsState(game, this));
            }

            if (mainMenu.buttonWithIndexPressed(3)) // Exit
                game.Exit();
            
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here


            mainMenu.Draw();
           
            String copyright = "Copyright GeekHub@Epita 2018";
            Vector2 vector = new Vector2(game.width - font.MeasureString(copyright).X - 10, game.height - font.MeasureString(copyright).Y);
            game.spriteBatch.DrawString(font, copyright, vector, Color.White);

        }

        public void ChangeState(IState state)
        {
            game.gameState = state;
        }


        public void Window_ClientSizeChanged()
        {
            LoadContent();
        }

       

    }
}
