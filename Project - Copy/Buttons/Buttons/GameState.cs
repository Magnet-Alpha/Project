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
    public class GameState : IState
    {
        Game1 game;
        TileMap myMap;
        int squaresAcross = 17;        
        int squaresDown = 37;
        int baseOffsetX = -32;
        int baseOffsetY = -64;
        float heightRowDepthMod = 0.00001f;


        public GameState(Game1 game)
        {
            this.game = game;
            Initialize();
            LoadContent();
        }


        public void Initialize()
        {
        }


        public void LoadContent()
        {
            Tile.TileSetTexture = game.Content.Load<Texture2D>(@"texture1");
            myMap = new TileMap(game.width, game.height);
        }

        public void Update(GameTime gameTime)
        {

            //--------------------Gestion de la cam�ra-------------------
            //Modifier la coordonn�e "4" pour acc�l�rer ou decc�l�rer la vitesse de d�placement de la cam�ra
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Left))
            {
                Camera.Location.X = MathHelper.Clamp(Camera.Location.X - 4, 0, 
                    (myMap.MapWidth - squaresAcross) * Tile.TileStepX);
            }

            if (ks.IsKeyDown(Keys.Right))
            {
                Camera.Location.X = MathHelper.Clamp(Camera.Location.X + 4, 0, 
                    (myMap.MapWidth - squaresAcross) * Tile.TileStepX);
            }

            if (ks.IsKeyDown(Keys.Up))
            {
                Camera.Location.Y = MathHelper.Clamp(Camera.Location.Y - 4, 0, 
                    (myMap.MapHeight - squaresDown) * Tile.TileStepY);
            }

            if (ks.IsKeyDown(Keys.Down))
            {
                Camera.Location.Y = MathHelper.Clamp(Camera.Location.Y + 4, 0, 
                    (myMap.MapHeight - squaresDown) * Tile.TileStepY);
            }

            // Gestion de redimentionnement
            if (myMap.MapWidth != game.width || myMap.MapHeight != game.height)
            {
                myMap = new TileMap(game.width, game.height);
                Draw(gameTime);
            }

            //-----------------------------------------------------------------
        }

      
        public void Draw(GameTime gameTime)
        {
            
            //--------------------Affichage des textures--------------------

            game.spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            Vector2 firstSquare = new Vector2(Camera.Location.X / Tile.TileStepX, Camera.Location.Y / Tile.TileStepY);
            int firstX = (int)firstSquare.X;
            int firstY = (int)firstSquare.Y;

            Vector2 squareOffset = new Vector2(Camera.Location.X % Tile.TileStepX, Camera.Location.Y % Tile.TileStepY);
            int offsetX = (int)squareOffset.X;
            int offsetY = (int)squareOffset.Y;
            
            float maxdepth = ((myMap.MapWidth + 1) * ((myMap.MapHeight + 1) * Tile.TileWidth)) / 10;
            float depthOffset;

            // D�but de la boucle de boucle pour r�cup�rer les coordonn�es
            for (int y = 0; y < squaresDown; y++)
            {
                int rowOffset = 0;
                if ((firstY + y) % 2 == 1)
                    rowOffset = Tile.OddRowXOffset;

                for (int x = 0; x < squaresAcross; x++)
                {
                    int mapx = (firstX + x);
                    int mapy = (firstY + y);
                    depthOffset = 0.7f - ((mapx + (mapy * Tile.TileWidth)) / maxdepth);

                    // Boucle de la 1�re couche de texture
                    foreach (int tileID in myMap.Rows[mapy].Columns[mapx].BaseTiles)
                    {
                        game.spriteBatch.Draw(

                            Tile.TileSetTexture,
                            new Rectangle(
                                (x * Tile.TileStepX) - offsetX + rowOffset + baseOffsetX,
                                (y * Tile.TileStepY) - offsetY + baseOffsetY,
                                Tile.TileWidth, Tile.TileHeight),
                            Tile.GetSourceRectangle(tileID),
                            Color.White,
                            0.0f,
                            Vector2.Zero,
                            SpriteEffects.None,
                            1.0f);
                    }

                    int heightRow = 0;

                    // Boucle de la 2�me couche de texture
                    foreach (int tileID in myMap.Rows[mapy].Columns[mapx].HeightTiles)
                    {
                        game.spriteBatch.Draw(
                            Tile.TileSetTexture,
                            new Rectangle(
                                (x * Tile.TileStepX) - offsetX + rowOffset + baseOffsetX,
                                (y * Tile.TileStepY) - offsetY + baseOffsetY - (heightRow * Tile.HeightTileOffset),
                                Tile.TileWidth, Tile.TileHeight),
                            Tile.GetSourceRectangle(tileID),
                            Color.White,
                            0.0f,
                            Vector2.Zero,
                            SpriteEffects.None,
                            depthOffset - ((float)heightRow * heightRowDepthMod));
                        heightRow++;
                    }

                    // Boucle de la 3�me couche de texture
                    foreach (int tileID in myMap.Rows[y + firstY].Columns[x + firstX].TopperTiles)
                    {
                        game.spriteBatch.Draw(
                            Tile.TileSetTexture,
                            new Rectangle(
                                (x * Tile.TileStepX) - offsetX + rowOffset + baseOffsetX,
                                (y * Tile.TileStepY) - offsetY + baseOffsetY - (heightRow * Tile.HeightTileOffset),
                                Tile.TileWidth, Tile.TileHeight),
                            Tile.GetSourceRectangle(tileID),
                            Color.White,
                            0.0f,
                            Vector2.Zero,
                            SpriteEffects.None,
                            depthOffset - ((float)heightRow * heightRowDepthMod));
                    }
                }
            }

            game.spriteBatch.End();

            //---------------------------------------------------------------------------------------

        }
        public void ChangeGameState(IState state)
        {
        }
    }
    


}