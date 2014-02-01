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

        public PauseState(GameState gameState, Game1 game)
        {
            this.game = game;
            this.gameState = gameState;
            Initialize();
            LoadContent();
        }
        public void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Escape))
            {
                ChangeState(gameState);
                gameState.status = GameStateStatus.InGame;
            }
        }
        public void Draw(GameTime gameTime)
        {
            game.spriteBatch.Begin();
            game.spriteBatch.Draw(background, new Rectangle(0, 0, game.width, game.height), Color.White);
            game.spriteBatch.End();
        }
        public void Initialize() {
            
        }
        public void LoadContent() {
            background = game.Content.Load<Texture2D>("BG");
        }
        public void ChangeState(IState state) {
            
            game.gameState = state;    
        }
    }
}
