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
        int squaresAcross = 17; //17        
        int squaresDown = 37; //original : 37 max : 60
        int baseOffsetX = -32;
        int baseOffsetY = -64;
        float heightRowDepthMod = 0.00001f;
        public GameStateStatus status;
        KeyboardState oldKs;
        List<Unit> virus = new List<Unit>();                                              //List of viruses on the map
        List<Unit> tower = new List<Unit>();                                              //List of towers on the map
        List<Keypoint> keypoints = new List<Keypoint>();                                    //List of keypoints on the map
        Virus test;                                                                         //All those are tests
        Tower test2;
        Keypoint test3;
        Keypoint test4;
        Keypoint test5;
        Vector2 v = new Vector2(0, 0);
        Vector2 v2 = new Vector2(200, 100);
        Vector2 ancientL;                                                                   //Memorizing Camera position before moving it
        Vector2 difL;                                                                       //Memorizing difference between camera position when moving it
        List<int> indexs = new List<int>();


        public GameState(Game1 game)
        {
            myMap = new TileMap(game.width / 64, game.height / 32);
            squaresAcross = myMap.MapWidth;
            squaresDown = myMap.MapHeight;
            this.game = game;
            Initialize();
            LoadContent();
            status = GameStateStatus.InGame;
            oldKs = Keyboard.GetState();
        }


        public void Initialize()
        {
        }


        public void LoadContent()
        {
            Tile.TileSetTexture = game.Content.Load<Texture2D>(@"TestSprites\\texture1");
            test = new Virus("b", 10, 10, 5, v, 1, game.Content, game.spriteBatch, Etat.Alive);
            test2 = new Tower("a", 10, 10, 5, v2, 100, game.Content, game.spriteBatch, Etat.Alive);
            test3 = new Keypoint(new Vector2(200, 0), false, false);
            test4 = new Keypoint(new Vector2(200, 400), true, false);
            test5 = new Keypoint(new Vector2(500, 400), true, true);
            virus.Add(test);
            tower.Add(test2);
            keypoints.Add(test3);
            keypoints.Add(test4);
            keypoints.Add(test5);
            DrawMap();
        }


        int abs(int a)
        {
            if (a < 0)
                return -a;
            return a; 
        }

        public void Update(GameTime gameTime)
        {
            if (status == GameStateStatus.Pause)
                return;

            //--------------------Gestion de la caméra-------------------
            //Modifier la coordonnée "4" pour accélérer ou deccélérer la vitesse de déplacement de la caméra
            KeyboardState ks = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();
            ancientL = Camera.Location;
            if (ks.IsKeyDown(Keys.Left) || mouse.X < 50)
            {
                Camera.Location.X = MathHelper.Clamp(Camera.Location.X - 4, 0, 
                    (myMap.MapWidth - squaresAcross) * Tile.TileStepX);
                difL = Camera.Location - ancientL;
            }

            if (ks.IsKeyDown(Keys.Right) || mouse.X > game.width - 50)
            {
                Camera.Location.X = MathHelper.Clamp(Camera.Location.X + 4, 0, 
                     (myMap.MapWidth - squaresAcross + game.width*64) * Tile.TileStepX);
                difL = Camera.Location - ancientL;
            }

            if (ks.IsKeyDown(Keys.Up) || mouse.Y < 50)
            {
                Camera.Location.Y = MathHelper.Clamp(Camera.Location.Y - 4, 0, 
                    (myMap.MapHeight - squaresDown) * Tile.TileStepY);
                difL = Camera.Location - ancientL;
            }

            if (ks.IsKeyDown(Keys.Down) || mouse.Y > game.height - 50)
            {
                Camera.Location.Y = MathHelper.Clamp(Camera.Location.Y + 4, 0, 
                    (myMap.MapHeight - squaresDown) * Tile.TileStepY);
                difL = Camera.Location - ancientL;
            }
            //-----------------------------------------------------------------

            //--------------------Gestion Pause -------------------------------
            if (ks.IsKeyDown(Keys.Escape) && !oldKs.IsKeyDown(Keys.Escape)) 
            {
                status = GameStateStatus.Pause;
                ChangeState(new PauseState(this, game));
                
            }
            oldKs = ks;
            foreach (Keypoint k in keypoints)
            {
                k.TheCamera(difL);                                                              //Correcting Camera location problems
            }
            foreach (Virus v in virus)
            {
                v.fuckingcamera(difL);                                                          //Correcting Camera location problems
                v.NewPosition();                                                                //Virus moving
                v.Turn(keypoints, virus, ref indexs);                                           //Virus turning and dying at objective
            }
            foreach (int i in indexs)
            {
                virus.RemoveAt(i);                                                              //Delete dead viruses
            }
            indexs.Clear();
            // TODO: Add your update logic here
            foreach (Tower t in tower)
            {
                t.fuckingcamera(difL);                                                          //Correcting Camera location problems
                t.Stating(virus);                                                               //Detecting viruses
            }
            difL = new Vector2(0,0);
               


        }

      
        public void Draw(GameTime gameTime)
        {
            if (status == GameStateStatus.Pause)
                return;

            DrawMap();
            
            foreach (Virus v in virus)
            {
                v.StateDraw();                                                                  //Draw all active viruses
            }
            foreach (Tower t in tower)
            {
                t.StateDraw();                                                                  //Draw all active towers
            }


            //---------------------------------------------------------------------------------------

        }

        void DrawMap()
        {
            //--------------------Affichage des textures--------------------


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

                    // Boucle de la 2ème couche de texture
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

                    // Boucle de la 3ème couche de texture
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
        }

        public void Window_ClientSizeChanged()
        {
            /*
            myMap.MapWidth = game.width;
            myMap.MapHeight = game.height;
            squaresAcross = myMap.MapWidth;
            squaresDown = myMap.MapHeight;
             */
        }

        public void ChangeState(IState state)
        {
            game.gameState = state;
            
        }
    }
}
