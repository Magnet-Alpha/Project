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
        public InterfaceMenu optionsMenu;
        SoundEffectInstance music;
        int gap;
        Game1 game;

        public MenuState(Game1 game)
        {
            this.game = game;
            LoadContent();
            Initialize();
            if(music == null)
                 music = game.Content.Load<SoundEffect>("music").CreateInstance();
            music.IsLooped = true;
            if(music.State != SoundState.Playing)
                music.Play();
            music.Volume = 0.5f;
            
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
            bool mainMenuOn;
            try { mainMenuOn = mainMenu.MenuOn; }
            catch { mainMenuOn = true; }

            background = game.Content.Load<Texture2D>("BG");

            font = game.Content.Load<SpriteFont>("Font");
            gap = (int)font.MeasureString("L").Y;
            //menu general
            
            continueButton = new TextButton(font, game, "Continue Game", new Vector2(40, game.height/2 - gap/2));

            newGameButton = new TextButton(font, game, "New Game", new Vector2(30, continueButton.top - gap));
            optionsButton = new TextButton(font, game, "Options", new Vector2(50, game.height/2 + gap/2));
            exitButton = new TextButton(font, game, "Exit", new Vector2(75, optionsButton.bottom + gap - (int) font.MeasureString("Exit").Y));
            
            mainMenu = new InterfaceMenu(new TextButton[] { newGameButton, continueButton, optionsButton, exitButton }, new Text[] { }, background, game);
            mainMenu.MenuOn = mainMenuOn;
            // menu options
            Text musicLevelText;
            musicLevelText.textValue = "Music :";
            musicLevelText.location = new Vector2(game.width/2 - (int)font.MeasureString(musicLevelText.textValue).X - 20, 50);
            musicLevelText.font = font;
            Text soundEffectText;
            soundEffectText.textValue = "Sound Effects :";
            soundEffectText.location = new Vector2(game.width/2 - (int)font.MeasureString(soundEffectText.textValue).X - 20, 10 + musicLevelText.location.Y
                                                                                            + (int)font.MeasureString(musicLevelText.textValue).Y);
            soundEffectText.font = font;
            Text languageText;
            languageText.textValue = "Language :";
            languageText.location = new Vector2(game.width/2 - (int)font.MeasureString(languageText.textValue).X - 20, 10 + soundEffectText.location.Y
                                                                                            + (int)font.MeasureString(languageText.textValue).Y);
            languageText.font = font;

            TextButton backToMainMenuButton = new TextButton(font, game, "Back", new Vector2(20, game.height - 80));

            Text fullScreenText = new Text("Full Screen : ", new Vector2(game.width/2 - (int)font.MeasureString("Full Screen :").X - 20, 10 + languageText.location.Y
                                                                                             + (int)font.MeasureString(languageText.textValue).Y), font);

            TextButton englishButton = new TextButton(font, game, "English", new Vector2(languageText.location.X + (int)font.MeasureString(languageText.textValue).X + font.MeasureString("English").X - 50, languageText.location.Y));
            TextButton frenchButton = new TextButton(font, game, "Francais", new Vector2(englishButton.right + 50, languageText.location.Y));

            TextButton toFS = new TextButton(font, game, "On", new Vector2((englishButton.left + englishButton.right) / 2, fullScreenText.location.Y));
            TextButton toNS = new TextButton(font, game, "Off", new Vector2((frenchButton.left + frenchButton.right) / 2, fullScreenText.location.Y));


            TextButton userNameButton = new TextButton(font, game, "Username :", new Vector2(game.width/2 - font.MeasureString("Username :").X - 20, fullScreenText.location.Y + font.MeasureString(fullScreenText.textValue).Y + 10));


            TextButton decreaseSoundEffect = new TextButton(font, game, "  -  ", new Vector2(soundEffectText.location.X + 200, soundEffectText.location.Y));
            TextButton increaseSoundEffect = new TextButton(font, game, "  +  ", new Vector2(soundEffectText.location.X + 500, soundEffectText.location.Y));
            TextButton decreaseMusicVolume = new TextButton(font, game, "  -  ", new Vector2(decreaseSoundEffect.textLocation.X, musicLevelText.location.Y));
            TextButton increaseMusicVolume = new TextButton(font, game, "  +  ", new Vector2(decreaseSoundEffect.textLocation.X + 300, musicLevelText.location.Y));

            optionsMenu = new InterfaceMenu(new TextButton[10] { backToMainMenuButton, decreaseSoundEffect, increaseSoundEffect, decreaseMusicVolume, increaseMusicVolume, englishButton, frenchButton, toFS, toNS, userNameButton },
                new Text[4] { musicLevelText, soundEffectText, languageText, fullScreenText }, background, game);
            optionsMenu.menuOn = !mainMenuOn;
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

            if (mainMenu.MenuOn)
                mainMenu.Update();
            if (optionsMenu.MenuOn)
                optionsMenu.Update();
            if (mainMenu.buttonWithIndexPressed(0)) // New Game
            {
                ChangeState(new GameState(game, music));
            }

            if (mainMenu.buttonWithIndexPressed(2)) // Options
            {
                //mainMenu.unload();
                mainMenu.menuOn = false;
                optionsMenu.MenuOn = true;
                optionsMenu.Draw();
            }


            // Sound Buttons
            if (optionsMenu.buttonWithIndexPressed(1))
            {
                
                while (Mouse.GetState().LeftButton == ButtonState.Pressed) { }
            }

            if (optionsMenu.buttonWithIndexPressed(2))
            {
                while (Mouse.GetState().LeftButton == ButtonState.Pressed) { }
            }

            if (optionsMenu.buttonWithIndexPressed(3) && music.Volume > 0)
            {
                try
                {
                    music.Volume -= 0.1f;
                }
                catch { music.Volume = 0; }
                while (Mouse.GetState().LeftButton == ButtonState.Pressed) { }
            }

            if (optionsMenu.buttonWithIndexPressed(4) && music.Volume + 0.1 <= 1)
            {
                music.Volume += 0.1f;
                while (Mouse.GetState().LeftButton == ButtonState.Pressed) { }
            }

            // full screen on
            if (optionsMenu.buttonWithIndexPressed(7))
            {
                game.graphics.PreferredBackBufferWidth = 480;
                game.graphics.PreferredBackBufferHeight = 800;
                game.graphics.IsFullScreen = true;
            }

            // Back
            if (optionsMenu.buttonWithIndexPressed(0))
            {
                optionsMenu.MenuOn = false;
                mainMenu.MenuOn = true;
                mainMenu.Draw();
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
            optionsMenu.Draw();
            if (optionsMenu.MenuOn)
            {
                game.spriteBatch.Begin();
                game.spriteBatch.DrawString(font, LevelString((int)(music.Volume*10)), new Vector2(optionsMenu.buttons[3].textLocation.X + 60, optionsMenu.buttons[3].textLocation.Y), Color.White);
                //game.spriteBatch.DrawString(font, LevelString(soundEffectVolume), new Vector2(optionsMenu.buttons[1].textLocation.X + 60, optionsMenu.buttons[1].textLocation.Y), Color.White);
                game.spriteBatch.End();

            }
            String copyright = "Copyright GeekHub@Epita 2018";
            game.spriteBatch.Begin();
            Vector2 vector = new Vector2(game.width - font.MeasureString(copyright).X - 10, game.height - font.MeasureString(copyright).Y);
            game.spriteBatch.DrawString(font, copyright, vector, Color.White);
            game.spriteBatch.End();

        }

        public void ChangeState(IState state)
        {
            mainMenu.menuOn = false;
            GameState newGame = new GameState(this.game, music);
            game.gameState = newGame;
            Draw(new GameTime());
        }


        public void Window_ClientSizeChanged(object sender, EventArgs e)
        {
            LoadContent();
        }

        static public string LevelString(int n)
        {
            string str = "";
            for (int i = 0; i < n; i++)
            {
                str += " |";
            }

            return str;
        }
       

    }
}
