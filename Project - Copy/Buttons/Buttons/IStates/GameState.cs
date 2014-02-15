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
        ImageButton firstbut;
        ImageButton secondbut;
        InterfaceInGame Interface;
        private int screenHeight;
        private int screenWidth;
        SpriteFont font;

        public GameState(Game1 game)
        {
            myMap = new TileMap(100,100);
            squaresAcross = game.width / 64 + 1;
            squaresDown = game.height / 16 + 3;
            this.game = game;
            Initialize();
            LoadContent();
            status = GameStateStatus.InGame;
            oldKs = Keyboard.GetState();
            screenHeight = game.Window.ClientBounds.Height;
            screenWidth = game.Window.ClientBounds.Width;
        }


        public void Initialize()
        {
        }


        public void LoadContent()
        {
            //Menu Interface and image buttons
            font = game.Content.Load<SpriteFont>("Font");
            
            int gold = 0;
            Text goldText;
            goldText.textValue = "Gold : " + gold;
            goldText.location = new Vector2(1 * game.width / 16, 1);
            goldText.font = font;

            int income = 0;
            Text incomeText;
            incomeText.textValue = "Income : " + income;
            incomeText.location = new Vector2(5 * game.width / 16, 1);
            incomeText.font = font;

            int life = 0;
            Text lifeText;
            lifeText.textValue = "Life : " + life;
            lifeText.location = new Vector2(10 * game.width / 16, 1);
            lifeText.font = font;

            Texture2D textureimg;
            textureimg = game.Content.Load<Texture2D>("W3");
            Texture2D textureimg2;
            textureimg2 = game.Content.Load<Texture2D>("DrainYellow");
            Texture2D background;
            background = game.Content.Load<Texture2D>("testbackground");
            firstbut = new ImageButton(game.spriteBatch, textureimg, new Vector2((13 * game.width / 16) + 7, 1), game);
            secondbut = new ImageButton(game.spriteBatch, textureimg2, new Vector2(7 + firstbut.right, 1), game);
            Interface = new InterfaceInGame(new ImageButton[] { firstbut, secondbut }, game, new Text[3] { goldText, incomeText, lifeText }, background, game.spriteBatch);
            Interface.menuOn = true;
            //Interface.Draw();

            //things about the map ^^
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

            //--------------------Gestion de la cam�ra-------------------
            //Modifier la coordonn�e "4" pour acc�l�rer ou decc�l�rer la vitesse de d�placement de la cam�ra
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
                     (myMap.MapWidth - squaresAcross) * Tile.TileStepX);
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
            
            
            
            
            Interface.Update();


        }

      
        public void Draw(GameTime gameTime)
        {
            //Interface.Draw();
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
            Interface.Draw();

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
                Interface.Draw();
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
                        //Interface.Draw();
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
                        //Interface.Draw();
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
                        //Interface.Draw();
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
            squaresAcross = game.width /64 + 3;
            squaresDown = game.height / 16 + 3;
            
        }

        public void ChangeState(IState state)
        {
            game.gameState = state;
            
        }
    }
}
