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
    public class OptionsState : IState
    {

        InterfaceMenu optionsMenu;
        Game1 game;
        SpriteFont font;
        Texture2D background = Textures.background;
        IState previousState;

        public OptionsState(Game1 game, IState backState)
        {
            this.game = game;
            previousState = backState;
            LoadContent();
        }

        public void LoadContent()
        {

            float wfact = 800 / game.width;
            float hfact = 600 / game.height;


            font = game.Content.Load<SpriteFont>("Font");

            // menu options
            Text musicLevelText;
            musicLevelText.textValue = Strings.stringForKey("Music");
            musicLevelText.location = new Vector2(game.width / 2 - (int)font.MeasureString(musicLevelText.textValue).X - 20, 50);
            musicLevelText.font = font;
            Text soundEffectText;
            soundEffectText.textValue = Strings.stringForKey("SoundEffects");
            soundEffectText.location = new Vector2(game.width / 2 - (int)font.MeasureString(soundEffectText.textValue).X - 20, 10 + musicLevelText.location.Y
                                                                                            + (int)font.MeasureString(musicLevelText.textValue).Y);
            soundEffectText.font = font;
            Text languageText;
            languageText.textValue = Strings.stringForKey("Language");
            languageText.location = new Vector2(game.width / 2 - (int)font.MeasureString(languageText.textValue).X - 20, 10 + soundEffectText.location.Y
                                                                                            + (int)font.MeasureString(languageText.textValue).Y);
            languageText.font = font;

            TextButton backToMainMenuButton = new TextButton(font, game, Strings.stringForKey("Back"), new Vector2(20, game.height - 80));

            Text fullScreenText = new Text(Strings.stringForKey("Fullscreen"), new Vector2(game.width / 2 - (int)font.MeasureString("Full Screen :").X - 20, 10 + languageText.location.Y
                                                                                             + (int)font.MeasureString(languageText.textValue).Y), font);

            


            TextButton userNameButton = new TextButton(font, game, "", new Vector2(game.width / 2 - font.MeasureString("Username :").X - 20, fullScreenText.location.Y + font.MeasureString(fullScreenText.textValue).Y + 10));


            TextButton decreaseSoundEffect = new TextButton(font, game, "  -  ", new Vector2(soundEffectText.location.X + 200, soundEffectText.location.Y));
            TextButton increaseSoundEffect = new TextButton(font, game, "  +  ", new Vector2(font.MeasureString(" | | | | | | | | | | ").X + decreaseSoundEffect.textLocation.X + 50, soundEffectText.location.Y));
            TextButton decreaseMusicVolume = new TextButton(font, game, "  -  ", new Vector2(decreaseSoundEffect.textLocation.X, musicLevelText.location.Y));
            TextButton increaseMusicVolume = new TextButton(font, game, "  +  ", new Vector2(font.MeasureString(" | | | | | | | | | | ").X + decreaseSoundEffect.textLocation.X + 50, musicLevelText.location.Y));


            TextButton englishButton = new TextButton(font, game, Strings.stringForKey("English"), new Vector2((increaseMusicVolume.right + decreaseMusicVolume.left)/2 - font.MeasureString("English").X - 50, languageText.location.Y));
            TextButton frenchButton = new TextButton(font, game, Strings.stringForKey("French"), new Vector2(englishButton.right + 100, languageText.location.Y));

            TextButton toFS = new TextButton(font, game, Strings.stringForKey("On"), new Vector2((englishButton.left + englishButton.right) / 2 - font.MeasureString("On").X/2, fullScreenText.location.Y));
            TextButton toNS = new TextButton(font, game, Strings.stringForKey("Off"), new Vector2((frenchButton.left + frenchButton.right) / 2 - font.MeasureString("Off").X/2, fullScreenText.location.Y));


            optionsMenu = new InterfaceMenu(new TextButton[10] { backToMainMenuButton, decreaseSoundEffect, increaseSoundEffect, decreaseMusicVolume, increaseMusicVolume, englishButton, frenchButton, toFS, toNS, userNameButton },
                new Text[4] { musicLevelText, soundEffectText, languageText, fullScreenText }, background, game);
            optionsMenu.menuOn = true;

        }






        public void Update(GameTime gameTime)
        {

            optionsMenu.Update();
            // Sound Buttons
            // effects -
            if (optionsMenu.buttonWithIndexPressed(1))
            {
                try
                {
                    game.settings.SoundEffectVolume -= 0.1f;
                }
                catch { game.settings.SoundEffectVolume = 0; }

                while (Mouse.GetState().LeftButton == ButtonState.Pressed) { }
            }
            //effects +
            if (optionsMenu.buttonWithIndexPressed(2))
            {
                try
                {
                    game.settings.SoundEffectVolume += 0.1f;
                }
                catch { game.settings.SoundEffectVolume = 1; }

                while (Mouse.GetState().LeftButton == ButtonState.Pressed) { }
            }
            // music -
            if (optionsMenu.buttonWithIndexPressed(3) && game.music.Volume > 0)
            {
                try
                {
                    game.music.Volume -= 0.1f;
                }
                catch { game.music.Volume = 0; }
                game.settings.musicVolume = game.music.Volume;
                while (Mouse.GetState().LeftButton == ButtonState.Pressed) { }
            }
            // music +
            if (optionsMenu.buttonWithIndexPressed(4) && game.music.Volume + 0.1 <= 1)
            {
                game.music.Volume += 0.1f;
                game.settings.musicVolume = game.music.Volume;
                while (Mouse.GetState().LeftButton == ButtonState.Pressed) { }
            }
            // laguange : english
            if (optionsMenu.buttonWithIndexPressed(5))
            {
                Strings.Language = Language.English;
                game.settings.language = Language.English;
                LoadContent();
                while (Mouse.GetState().LeftButton == ButtonState.Pressed) { }
            }
            if (optionsMenu.buttonWithIndexPressed(6))
            {
                Strings.Language = Language.French;
                game.settings.language = Language.French;
                LoadContent();
                while (Mouse.GetState().LeftButton == ButtonState.Pressed) { }
            }

            // full screen on
            if (optionsMenu.buttonWithIndexPressed(7) && !game.graphics.IsFullScreen)
            {
                game.settings.fullScreen = true;
                /*game.graphics.IsFullScreen = true;
                game.graphics.PreferredBackBufferWidth = game.graphics.GraphicsDevice.DisplayMode.Width;
                game.graphics.PreferredBackBufferHeight = game.graphics.GraphicsDevice.DisplayMode.Height; 
                game.graphics.ApplyChanges();
                game.width = game.graphics.PreferredBackBufferWidth;
                game.height = game.graphics.PreferredBackBufferHeight;
                previousState.Window_ClientSizeChanged();
                game.Window_ClientSizeChanged(null, null);*/
                game.graphics.ToggleFullScreen();
            }
            //fullscreen off
            if (optionsMenu.buttonWithIndexPressed(8) && game.graphics.IsFullScreen)
            {
                game.settings.fullScreen = false;
                /*game.graphics.IsFullScreen = false;
                game.graphics.PreferredBackBufferWidth = 800;
                game.graphics.PreferredBackBufferHeight = 600;
                game.graphics.ApplyChanges();
                game.width = game.graphics.PreferredBackBufferWidth;
                game.height = game.graphics.PreferredBackBufferHeight;
                previousState.Window_ClientSizeChanged();
                game.Window_ClientSizeChanged(null, null);*/
                game.graphics.ToggleFullScreen();
               
            }
            // Back
            if (optionsMenu.buttonWithIndexPressed(0))
            {
                ChangeState(previousState);
            }
            game.settings.saveSettings();
        }
        public void Draw(GameTime gameTime)
        {
            optionsMenu.Draw();
            for (int i = 0; i < optionsMenu.buttons.Length; i++)
                optionsMenu.buttons[i].Draw();


            game.spriteBatch.DrawString(font, LevelString((int)(game.music.Volume * 10)), new Vector2(optionsMenu.buttons[3].textLocation.X + 60, optionsMenu.buttons[3].textLocation.Y), Color.White);
            game.spriteBatch.DrawString(font, LevelString((int)(game.settings.SoundEffectVolume * 10)), new Vector2(optionsMenu.buttons[1].textLocation.X + 60, optionsMenu.buttons[1].textLocation.Y), Color.White);
            

            
        }
        public void Initialize() { }

        public void ChangeState(IState state)
        {
            optionsMenu.MenuOn = false;
            state.LoadContent();
            game.gameState = state;
        }

        public void Window_ClientSizeChanged()
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
