﻿using System;
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
    class MenuState : IState
    {
        TextButton newGameButton;
        TextButton continueButton;
        TextButton optionsButton;
        TextButton exitButton;
        SpriteFont font;
        Texture2D background;
        InterfaceMenu mainMenu;
        InterfaceMenu optionsMenu;
        IState myState;
        int soundEffectVolume;
        int musicVolume;
        SoundEffectInstance music;
        int gap;
        float width;
        float height;
        Game1 game;

        public MenuState(Game1 game)
        {
            
            game.Content.RootDirectory = "Content";
            /*
            this.graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            this.graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;*/
            this.graphics.PreferredBackBufferWidth = 800;
            this.graphics.PreferredBackBufferHeight = 460;
            width = 800;
            height = 460;

           // this.graphics.IsFullScreen = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            background = game.Content.Load<Texture2D>("BG");
            soundEffectVolume = 5;
            musicVolume = 5;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures. 
            
            music = game.Content.Load<SoundEffect>("music").CreateInstance();
            music.IsLooped = true;
            music.Play();
            music.Volume = 0.5f;


            Texture2D texture;
            texture = game.Content.Load<Texture2D>("SmileyWalk");
            font = game.Content.Load<SpriteFont>("Font");
            gap = (int)font.MeasureString("L").Y;
            //menu general
            
            continueButton = new TextButton(font, spriteBatch, "Continue Game", new Vector2(40, height/2 - gap/2));

            newGameButton = new TextButton(font, spriteBatch, "New Game", new Vector2(30, continueButton.top - gap));
            optionsButton = new TextButton(font, spriteBatch, "Options", new Vector2(50, height/2 + gap/2));
            exitButton = new TextButton(font, spriteBatch, "Exit", new Vector2(75, optionsButton.bottom + gap - (int) font.MeasureString("Exit").Y));
            mainMenu = new InterfaceMenu(new TextButton[] { newGameButton, continueButton, optionsButton, exitButton }, new Text[] { }, background, spriteBatch, this.graphics);


            mainMenu.menuOn = true;
            // menu options
            Text musicLevelText;
            musicLevelText.textValue = "Music :";
            musicLevelText.location = new Vector2(width/2 - (int)font.MeasureString(musicLevelText.textValue).X - 20, 50);
            musicLevelText.font = font;
            Text soundEffectText;
            soundEffectText.textValue = "Sound Effects :";
            soundEffectText.location = new Vector2(width/2 - (int)font.MeasureString(soundEffectText.textValue).X - 20, 10 + musicLevelText.location.Y
                                                                                            + (int)font.MeasureString(musicLevelText.textValue).Y);
            soundEffectText.font = font;
            Text languageText;
            languageText.textValue = "Language :";
            languageText.location = new Vector2(width/2 - (int)font.MeasureString(languageText.textValue).X - 20, 10 + soundEffectText.location.Y
                                                                                            + (int)font.MeasureString(languageText.textValue).Y);
            languageText.font = font;

            TextButton backToMainMenuButton = new TextButton(font, spriteBatch, "Back", new Vector2(20, height - 80));

            Text fullScreenText = new Text("Full Screen : ", new Vector2(width/2 - (int)font.MeasureString("Full Screen :").X - 20, 10 + languageText.location.Y
                                                                                             + (int)font.MeasureString(languageText.textValue).Y), font);

            TextButton englishButton = new TextButton(font, spriteBatch, "English", new Vector2(languageText.location.X + (int)font.MeasureString(languageText.textValue).X + font.MeasureString("English").X - 50, languageText.location.Y));
            TextButton frenchButton = new TextButton(font, spriteBatch, "Francais", new Vector2(englishButton.right + 50, languageText.location.Y));

            TextButton toFS = new TextButton(font, spriteBatch, "On", new Vector2((englishButton.left + englishButton.right) / 2, fullScreenText.location.Y));
            TextButton toNS = new TextButton(font, spriteBatch, "Off", new Vector2((frenchButton.left + frenchButton.right) / 2, fullScreenText.location.Y));


            TextButton userNameButton = new TextButton(font, spriteBatch, "Username :", new Vector2(width/2 - font.MeasureString("Username :").X - 20, fullScreenText.location.Y + font.MeasureString(fullScreenText.textValue).Y + 10));


            TextButton decreaseSoundEffect = new TextButton(font, spriteBatch, "  -  ", new Vector2(soundEffectText.location.X + 200, soundEffectText.location.Y));
            TextButton increaseSoundEffect = new TextButton(font, spriteBatch, "  +  ", new Vector2(soundEffectText.location.X + 500, soundEffectText.location.Y));
            TextButton decreaseMusicVolume = new TextButton(font, spriteBatch, "  -  ", new Vector2(decreaseSoundEffect.textLocation.X, musicLevelText.location.Y));
            TextButton increaseMusicVolume = new TextButton(font, spriteBatch, "  +  ", new Vector2(decreaseSoundEffect.textLocation.X + 300, musicLevelText.location.Y));

            optionsMenu = new InterfaceMenu(new TextButton[10] { backToMainMenuButton, decreaseSoundEffect, increaseSoundEffect, decreaseMusicVolume, increaseMusicVolume, englishButton, frenchButton, toFS, toNS, userNameButton },
                new Text[4] { musicLevelText, soundEffectText, languageText, fullScreenText }, background, spriteBatch, this.graphics);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                game.Exit();

            width = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            height = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

            if (mainMenu.MenuOn)
                mainMenu.Update();
            if (optionsMenu.MenuOn)
                optionsMenu.Update();
            if (mainMenu.buttonWithIndexPressed(0)) // New Game
            {
                mainMenu.menuOn = false;
                
                Text loadingText;
                loadingText.font = font;
                loadingText.textValue = "Loading...";
                loadingText.location = new Vector2(50, 50);
                Screen loadingScreen = new Screen(background, new Text[1] { loadingText }, spriteBatch);
                loadingScreen.IsOn = true;
                loadingScreen.Draw();
            }

            if (mainMenu.buttonWithIndexPressed(2)) // Options
            {
                //mainMenu.unload();
                mainMenu.menuOn = false;
                optionsMenu.MenuOn = true;
                optionsMenu.Draw();
            }


            // Sound Buttons
            if (optionsMenu.buttonWithIndexPressed(1) && soundEffectVolume > 0)
            {
                soundEffectVolume--;
                while (Mouse.GetState().LeftButton == ButtonState.Pressed) { }
            }

            if (optionsMenu.buttonWithIndexPressed(2) && soundEffectVolume < 10)
            {
                soundEffectVolume++;
                while (Mouse.GetState().LeftButton == ButtonState.Pressed) { }
            }

            if (optionsMenu.buttonWithIndexPressed(3) && music.Volume > 0)
            {
                music.Volume -= 0.1f;
                while (Mouse.GetState().LeftButton == ButtonState.Pressed) { }
            }

            if (optionsMenu.buttonWithIndexPressed(4) && music.Volume + 0.1 <= 1)
            {
                music.Volume += 0.1f;
                while (Mouse.GetState().LeftButton == ButtonState.Pressed) { }
            }

            // full screen on
            /*if (optionsMenu.buttonWithIndexPressed(7))
            {
                this.graphics.IsFullScreen = true;
            }*/

            // Back
            if (optionsMenu.buttonWithIndexPressed(0))
            {
                optionsMenu.MenuOn = false;
                mainMenu.MenuOn = true;
                mainMenu.Draw();
            }

            if (mainMenu.buttonWithIndexPressed(3)) // Exit
                Exit();

            Draw(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here


            mainMenu.Draw();
            optionsMenu.Draw();
            if (optionsMenu.MenuOn)
            {
                spriteBatch.Begin();
                spriteBatch.DrawString(font, LevelString((int)(music.Volume*10)), new Vector2(optionsMenu.buttons[3].textLocation.X + 60, optionsMenu.buttons[3].textLocation.Y), Color.White);
                spriteBatch.DrawString(font, LevelString(soundEffectVolume), new Vector2(optionsMenu.buttons[1].textLocation.X + 60, optionsMenu.buttons[1].textLocation.Y), Color.White);
                spriteBatch.End();

            }
            String copyright = "Copyright GeekHub@Epita 2018";
            spriteBatch.Begin();
            Vector2 vector = new Vector2(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - font.MeasureString(copyright).X - 10, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - font.MeasureString(copyright).Y);
            spriteBatch.DrawString(font, copyright, vector, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }


        private string LevelString(int n)
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
