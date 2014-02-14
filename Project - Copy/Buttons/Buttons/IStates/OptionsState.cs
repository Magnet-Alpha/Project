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
        Texture2D background;
        IState previousState;

        public OptionsState(Game1 game, IState backState)
        {
            this.game = game;
            previousState = backState;
            LoadContent();
        }

        public void LoadContent()
        {

            font = game.Content.Load<SpriteFont>("Font");

            background = game.Content.Load<Texture2D>("BG");

            // menu options
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
            TextButton increaseSoundEffect = new TextButton(font, game, "  +  ", new Vector2(font.MeasureString(" | | | | | | | | | | ").X + decreaseSoundEffect.textLocation.X + 50, soundEffectText.location.Y));
            TextButton decreaseMusicVolume = new TextButton(font, game, "  -  ", new Vector2(decreaseSoundEffect.textLocation.X, musicLevelText.location.Y));
            TextButton increaseMusicVolume = new TextButton(font, game, "  +  ", new Vector2(font.MeasureString(" | | | | | | | | | | ").X + decreaseSoundEffect.textLocation.X + 50, musicLevelText.location.Y));

            optionsMenu = new InterfaceMenu(new TextButton[10] { backToMainMenuButton, decreaseSoundEffect, increaseSoundEffect, decreaseMusicVolume, increaseMusicVolume, englishButton, frenchButton, toFS, toNS, userNameButton },
                new Text[4] { musicLevelText, soundEffectText, languageText, fullScreenText }, background, game);
            optionsMenu.menuOn = true;
        }






        public void Update(GameTime gameTime)
        {
            // Sound Buttons
            if (optionsMenu.buttonWithIndexPressed(1))
            {

                while (Mouse.GetState().LeftButton == ButtonState.Pressed) { }
            }

            if (optionsMenu.buttonWithIndexPressed(2))
            {
                while (Mouse.GetState().LeftButton == ButtonState.Pressed) { }
            }

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

            if (optionsMenu.buttonWithIndexPressed(4) && game.music.Volume + 0.1 <= 1)
            {
                game.music.Volume += 0.1f;
                game.settings.musicVolume = game.music.Volume;
                while (Mouse.GetState().LeftButton == ButtonState.Pressed) { }
            }

            // full screen on
            if (optionsMenu.buttonWithIndexPressed(7) && !game.graphics.IsFullScreen)
            {
                game.settings.fullScreen = true;
                game.graphics.IsFullScreen = true;
                game.graphics.ApplyChanges();
            }
            //fullscreen off
            if (optionsMenu.buttonWithIndexPressed(8))
            {
                game.settings.fullScreen = false;
                game.graphics.IsFullScreen = false;
                game.graphics.ApplyChanges();
            }
            // Back
            if (optionsMenu.buttonWithIndexPressed(0))
            {
                ChangeState(previousState);
            }
            game.settings.writeSettings();
        }
        public void Draw(GameTime gameTime)
        {
            optionsMenu.Draw();

            game.spriteBatch.DrawString(font, LevelString((int)(game.music.Volume * 10)), new Vector2(optionsMenu.buttons[3].textLocation.X + 60, optionsMenu.buttons[3].textLocation.Y), Color.White);
            //game.spriteBatch.DrawString(font, LevelString(soundEffectVolume), new Vector2(optionsMenu.buttons[1].textLocation.X + 60, optionsMenu.buttons[1].textLocation.Y), Color.White);


            String copyright = "Copyright GeekHub@Epita 2018";
            Vector2 vector = new Vector2(game.width - font.MeasureString(copyright).X - 10, game.height - font.MeasureString(copyright).Y);
            game.spriteBatch.DrawString(font, copyright, vector, Color.White);
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
