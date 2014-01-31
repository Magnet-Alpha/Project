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
    public class GameLoop : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        TileMap myMap = new TileMap();
        int squaresAcross = 17;        
        int squaresDown = 37;
        int baseOffsetX = -32;
        int baseOffsetY = -64;
        float heightRowDepthMod = 0.00001f;


        public GameLoop()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }


        protected override void Initialize()
        {
            base.Initialize();
            IsMouseVisible = true;
        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Tile.TileSetTexture = Content.Load<Texture2D>(@"texture1");
        }


        protected override void UnloadContent()
        {
            //VIDE
        }

        protected override void Update(GameTime gameTime)
        {

            //--------------------Gestion de la caméra-------------------
            //Modifier la coordonnée "4" pour accélérer ou deccélérer la vitesse de déplacement de la caméra
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
            //-----------------------------------------------------------------

            base.Update(gameTime);
        }

      
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            
            //--------------------Affichage des textures--------------------

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            Vector2 firstSquare = new Vector2(Camera.Location.X / Tile.TileStepX, Camera.Location.Y / Tile.TileStepY);
            int firstX = (int)firstSquare.X;
            int firstY = (int)firstSquare.Y;

            Vector2 squareOffset = new Vector2(Camera.Location.X % Tile.TileStepX, Camera.Location.Y % Tile.TileStepY);
            int offsetX = (int)squareOffset.X;
            int offsetY = (int)squareOffset.Y;

            float maxdepth = ((myMap.MapWidth + 1) * ((myMap.MapHeight + 1) * Tile.TileWidth)) / 10;
            float depthOffset;

            // Début de la boucle de boucle pour récupérer les coordonnées
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

                    // Boucle de la 1ère couche de texture
                    foreach (int tileID in myMap.Rows[mapy].Columns[mapx].BaseTiles)
                    {
                        spriteBatch.Draw(

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

                    // Boucle de la 2ème couche de texture
                    foreach (int tileID in myMap.Rows[mapy].Columns[mapx].HeightTiles)
                    {
                        spriteBatch.Draw(
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

                    // Boucle de la 3ème couche de texture
                    foreach (int tileID in myMap.Rows[y + firstY].Columns[x + firstX].TopperTiles)
                    {
                        spriteBatch.Draw(
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

            spriteBatch.End();

            //---------------------------------------------------------------------------------------

            base.Draw(gameTime);
        }
    }
}
