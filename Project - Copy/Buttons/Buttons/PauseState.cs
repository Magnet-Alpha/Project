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
        InterfaceMenu optionsMenu;
        SoundEffectInstance music;
        KeyboardState oldKs;

        public PauseState(GameState gameState, Game1 game, SoundEffectInstance music)
        {
            this.music = music;
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
                optionsMenu.MenuOn = true;
            }
            if (pauseMenu.buttonWithIndexPressed(2))
            {
                pauseMenu.MenuOn = false;
                ChangeState(new MenuState(game));
            }
            if (optionsMenu.buttonWithIndexPressed(0))
            {
                optionsMenu.MenuOn = false;
                pauseMenu.MenuOn = true;
            }
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

            if (optionsMenu.buttonWithIndexPressed(4))
            {
                try
                {
                    music.Volume += 0.1f;
                }
                catch { music.Volume = 1; }
                while (Mouse.GetState().LeftButton == ButtonState.Pressed) { }
            }
            oldKs = ks;
            pauseMenu.Update();
            optionsMenu.Update();
        }
        public void Draw(GameTime gameTime)
        {
            pauseMenu.Draw();
            optionsMenu.Draw();
            if (optionsMenu.MenuOn)
            {
                game.spriteBatch.Begin();
                game.spriteBatch.DrawString(font, MenuState.LevelString((int)(music.Volume*10)), new Vector2(optionsMenu.buttons[3].textLocation.X + 60, optionsMenu.buttons[3].textLocation.Y), Color.White);
                //game.spriteBatch.DrawString(font, LevelString(soundEffectVolume), new Vector2(optionsMenu.buttons[1].textLocation.X + 60, optionsMenu.buttons[1].textLocation.Y), Color.White);
                game.spriteBatch.End();

            }
        }
        public void Initialize()
        {
            background = game.Content.Load<Texture2D>("BG");
            Text musicLevelText;
            musicLevelText.textValue = "Music :";
            musicLevelText.location = new Vector2(game.width / 2 - (int)font.MeasureString(musicLevelText.textValue).X - 20, 50);
            musicLevelText.font = font;
            Text soundEffectText;
            soundEffectText.textValue = "Sound Effects :";
            soundEffectText.location = new Vector2(game.width / 2 - (int)font.MeasureString(soundEffectText.textValue).X - 20, 10 + musicLevelText.location.Y
                                                                                            + (int)font.MeasureString(musicLevelText.textValue).Y);
            soundEffectText.font = font;
            Text languageText;
            languageText.textValue = "Language :";
            languageText.location = new Vector2(game.width / 2 - (int)font.MeasureString(languageText.textValue).X - 20, 10 + soundEffectText.location.Y
                                                                                            + (int)font.MeasureString(languageText.textValue).Y);
            languageText.font = font;

            TextButton backToMainMenuButton = new TextButton(font, game, "Back", new Vector2(20, game.height - 80));

            Text fullScreenText = new Text("Full Screen : ", new Vector2(game.width / 2 - (int)font.MeasureString("Full Screen :").X - 20, 10 + languageText.location.Y
                                                                                             + (int)font.MeasureString(languageText.textValue).Y), font);

            TextButton englishButton = new TextButton(font, game, "English", new Vector2(languageText.location.X + (int)font.MeasureString(languageText.textValue).X + font.MeasureString("English").X - 50, languageText.location.Y));
            TextButton frenchButton = new TextButton(font, game, "Francais", new Vector2(englishButton.right + 50, languageText.location.Y));

            TextButton toFS = new TextButton(font, game, "On", new Vector2((englishButton.left + englishButton.right) / 2, fullScreenText.location.Y));
            TextButton toNS = new TextButton(font, game, "Off", new Vector2((frenchButton.left + frenchButton.right) / 2, fullScreenText.location.Y));


            TextButton userNameButton = new TextButton(font, game, "Username :", new Vector2(game.width / 2 - font.MeasureString("Username :").X - 20, fullScreenText.location.Y + font.MeasureString(fullScreenText.textValue).Y + 10));


            TextButton decreaseSoundEffect = new TextButton(font, game, "  -  ", new Vector2(soundEffectText.location.X + 200, soundEffectText.location.Y));
            TextButton increaseSoundEffect = new TextButton(font, game, "  +  ", new Vector2(soundEffectText.location.X + 500, soundEffectText.location.Y));
            TextButton decreaseMusicVolume = new TextButton(font, game, "  -  ", new Vector2(decreaseSoundEffect.textLocation.X, musicLevelText.location.Y));
            TextButton increaseMusicVolume = new TextButton(font, game, "  +  ", new Vector2(decreaseSoundEffect.textLocation.X + 300, musicLevelText.location.Y));

            optionsMenu = new InterfaceMenu(new TextButton[10] { backToMainMenuButton, decreaseSoundEffect, increaseSoundEffect, decreaseMusicVolume, increaseMusicVolume, englishButton, frenchButton, toFS, toNS, userNameButton },
                new Text[4] { musicLevelText, soundEffectText, languageText, fullScreenText }, background, game);
        }


        public void LoadContent()
        {
            
            TextButton returnGameButton = new TextButton(font, game, "Resume game", new Vector2(game.width / 2 - font.MeasureString("Resume game").X/2, 200));
            TextButton optionsButton = new TextButton(font, game, "Options", new Vector2(game.width / 2 - font.MeasureString("Options").X / 2, returnGameButton.textLocation.Y + font.MeasureString("Options").Y + 20));
            TextButton returnMainMenuButton = new TextButton(font, game, "Return to main menu", new Vector2(game.width / 2 - font.MeasureString("Return to main menu").X / 2, optionsButton.textLocation.Y + font.MeasureString("Return to main menu").Y + 20));
            pauseMenu = new InterfaceMenu(new TextButton[3]{returnGameButton, optionsButton, returnMainMenuButton}, new Text[1]{ new Text("Pause Menu", new Vector2(game.width/2 - font.MeasureString("Pause Menu").X/2, 50), font)}, background, game);
            pauseMenu.MenuOn = true;
        }
        public void ChangeState(IState state) {
            
            game.gameState = state;    
        }
    }
}
