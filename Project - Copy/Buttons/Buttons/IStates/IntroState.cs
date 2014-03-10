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
    class IntroState : IState
    {
        Game1 game;
        Video video;
        VideoPlayer player;
        Texture2D videoTexture;

        public IntroState(Game1 game)
        {
            this.game = game;
            game.drawMouse = false;
            video = game.Content.Load<Video>("intro");
            player = new VideoPlayer();
            player.Volume = game.settings.SoundEffectVolume;
            player.Play(video);
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            if (player.State == MediaState.Stopped || ks.IsKeyDown(Keys.Enter) || ks.IsKeyDown(Keys.Escape) || Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                game.drawMouse = true;
                ChangeState(new MenuState(game));
            }
        }
        public void Draw(GameTime gameTime)
        {

            if (player.State == MediaState.Playing)
            {
                videoTexture = player.GetTexture();
                game.spriteBatch.Draw(videoTexture, new Rectangle(0,0,game.width, game.height), Color.White);
            }
        
        }
        public void Initialize() { }
        public void LoadContent() { }
        public void ChangeState(IState state)
        {
            player.Stop();
            game.gameState = state;
        }
        public void Window_ClientSizeChanged() { }
    }
}
