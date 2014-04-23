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
        List<Virus> virus = new List<Virus>();                                              //List of viruses on the map
        List<Unit> tower = new List<Unit>();                                              //List of towers on the map
        Tower[,] towers = new Tower[30, 60];
        List<Keypoint> keypoints = new List<Keypoint>();                                    //List of keypoints on the map
        List<Projectile> projs = new List<Projectile>();
        Virus test;                                                                         //All those are tests
        Keypoint test3;
        Keypoint test4;
        Keypoint test5;
        Keypoint test6;
        Keypoint test7;
        Keypoint test8;
        Coordonnees test9;
        Coordonnees test10;
        Coordonnees test11;
        Coordonnees test12;
        Coordonnees test13;
        Coordonnees test14;
        Coordonnees test15;
        Coordonnees test16;
        Vector2 v = new Vector2(0, 0);
        Vector2 v2 = new Vector2(200, 100);
        Vector2 ancientL;                                                                   //Memorizing Camera position before moving it
        Vector2 difL;                                                                       //Memorizing difference between camera position when moving it
        List<int> indexs = new List<int>();
        List<int> indexP = new List<int>();
        ImageButton firstbut;
        ImageButton secondbut;
        ImageButton backmenu;
        public int gold;
        public int income;
        public int life;
        InterfaceInGame Interface;
        private int screenHeight;
        private int screenWidth;
        SpriteFont font;
        MouseState oldMouse = Mouse.GetState();
        bool choosing;
        TType choice;
        
        public GameState(Game1 game)
        {
            myMap = new TileMap(100,100);
            squaresAcross = game.width / 256 + 12;
            squaresDown = game.height / 32 + 20;
            
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
            
            gold = 100;
            Text goldText;
            goldText.textValue = "Gold : " + gold;
            goldText.location = new Vector2(game.width / 15, game.height - 110);
            goldText.font = font;

            income = 10;
            Text incomeText;
            incomeText.textValue = "Income : " + income;
            incomeText.location = new Vector2(game.width / 15, game.height - 70);
            incomeText.font = font;

            life = 25;
            Text lifeText;
            lifeText.textValue = "Life : " + life;
            lifeText.location = new Vector2(game.width / 15, game.height - 30);
            lifeText.font = font;

            Texture2D textureimg;
            textureimg = game.Content.Load<Texture2D>("W3");
            Texture2D textureimg2;
            textureimg2 = game.Content.Load<Texture2D>("DrainYellow");
            Texture2D background;
            background = game.Content.Load<Texture2D>("testbackground");
            Texture2D menu;
            menu = game.Content.Load<Texture2D>("menu");

            firstbut = new ImageButton(game.spriteBatch, textureimg, new Rectangle(game.width - 800 / 12 + 12, game.width / 60, game.width / 30, game.width / 30), game);
            secondbut = new ImageButton(game.spriteBatch, textureimg2, new Rectangle(game.width - 800 / 12 + 12, firstbut.bottom + game.width / 60, game.width / 30, game.width / 30), game);
            backmenu = new ImageButton(game.spriteBatch, menu, new Rectangle(firstbut.left, game.height - menu.Height, game.width / 30, game.width / 30), game);
            Interface = new InterfaceInGame(new ImageButton[] { firstbut, secondbut, backmenu}, game, new Text[3] { goldText, incomeText, lifeText }, background, game.spriteBatch);
            Interface.menuOn = true;

            //things about the map ^^
            Tile.TileSetTexture = game.Content.Load<Texture2D>(@"sprites//map//maptexture");
            test3 = new Keypoint(new Vector2(176 - Camera.Location.X, 542 - Camera.Location.Y), true, false);
            test4 = new Keypoint(new Vector2(816 - Camera.Location.X, 542 - Camera.Location.Y), true, false);
            test5 = new Keypoint(new Vector2(816 - Camera.Location.X, 350 - Camera.Location.Y), true, false);
            test6 = new Keypoint(new Vector2(496 - Camera.Location.X, 350 - Camera.Location.Y), false, false);
            test7 = new Keypoint(new Vector2(496 - Camera.Location.X, 126 - Camera.Location.Y), false, false);
            test8 = new Keypoint(new Vector2(880 - Camera.Location.X, 126 - Camera.Location.Y), false, true);
            keypoints.Add(test3);
            keypoints.Add(test4);
            keypoints.Add(test5);
            keypoints.Add(test6);
            keypoints.Add(test7);
            keypoints.Add(test8);
            choosing = false;
            test9 = new Coordonnees(3, 9, 6, 13);
            test10 = new Coordonnees(5, 7, 14, 37);
            test11 = new Coordonnees(8, 27, 34, 37);
            test12 = new Coordonnees(25, 27, 22, 37);
            test13 = new Coordonnees(15, 24, 22, 25);
            test14 = new Coordonnees(15, 17, 8, 21);
            test15 = new Coordonnees(18, 24, 8, 11);
            test16 = new Coordonnees(25, 31, 6, 13);
            test9.Fill(ref towers);
            test10.Fill(ref towers);
            test11.Fill(ref towers);
            test12.Fill(ref towers);
            test13.Fill(ref towers);
            test14.Fill(ref towers);
            test15.Fill(ref towers);
            test16.Fill(ref towers);
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
            if (ks.IsKeyDown(Keys.Left) || mouse.X < 20 )
            {
                Camera.Location.X = MathHelper.Clamp(Camera.Location.X - 4, 0, 
                    (myMap.MapWidth - squaresAcross) * Tile.TileStepX);
                difL = Camera.Location - ancientL;
            }

            if (ks.IsKeyDown(Keys.Right) || (mouse.X > game.width - 20))
            {
                Camera.Location.X = MathHelper.Clamp(Camera.Location.X + 4, 0, 
                     (myMap.MapWidth - squaresAcross) * Tile.TileStepX);
                difL = Camera.Location - ancientL;
            }

            if (ks.IsKeyDown(Keys.Up) || mouse.Y < 30)
            {
                Camera.Location.Y = MathHelper.Clamp(Camera.Location.Y - 4, 0, 
                    (myMap.MapHeight - squaresDown) * Tile.TileStepY);
                difL = Camera.Location - ancientL;
            }

            if (ks.IsKeyDown(Keys.Down) || mouse.Y > game.height - 20)
            {
                Camera.Location.Y = MathHelper.Clamp(Camera.Location.Y + 4, 0, 
                    (myMap.MapHeight - squaresDown) * Tile.TileStepY);
                difL = Camera.Location - ancientL;
            }



            //-----------------------------------------------------------------

            //-------------------- Gestion Boutons InGame ---------------------

            Interface.Update();

            if (oldMouse.LeftButton == ButtonState.Released && Interface.buttonWithIndexPressed(0))
            {
                test = new Virus("b", 100, 10, 5, new Vector2( 176 -Camera.Location.X * game.widthFactor + difL.X, 126 -Camera.Location.Y * game.heightFactor + difL.Y), 1, game.Content, game.spriteBatch, Etat.Alive);
                virus.Add(test);
            }
            if (Interface.buttonWithIndexPressed(1))
            {
                choosing = true;
                choice.name = "b";
                choice.attack = 10;
                choice.cooldown = 20;
                choice.range = 200;
            }
            //-----------------------------------------------------------------

            //--------------------Gestion Pause -------------------------------
            if (ks.IsKeyDown(Keys.Escape) && !oldKs.IsKeyDown(Keys.Escape) || Interface.buttonWithIndexPressed(2)) 
            {
                status = GameStateStatus.Pause;
                choosing = false;
                ChangeState(new PauseState(this, game));
                
            }
            oldKs = ks;


            foreach (Keypoint k in keypoints)
            {
                k.TheCamera(difL, new Vector2(game.widthFactor, game.heightFactor));                                                              //Correcting Camera location problems
            }
            foreach (Virus v in virus)
            {
                v.fuckingcamera(difL, new Vector2(game.widthFactor, game.heightFactor));                                                          //Correcting Camera location problems
                v.NewPosition(new Vector2(game.widthFactor, game.heightFactor));                                                                //Virus moving
                v.Turn(keypoints);                                           //Virus turning and dying at objective
                v.Death();
                if (v.Hp <= 0)
                    gold = gold + 2;
            }
            int m = 0;
            while (m < virus.Count)
            {
                if (virus[m].State == Etat.Dead)
                    virus.RemoveAt(m);
                else
                    m++;
            }
            foreach (Projectile p in projs)
            {
                p.TheCamera(difL);
                p.NewPosition();
                p.Destruction();
            }
            int n = 0;
            while(n < projs.Count)
            {
                if (!projs[n].isalive)
                    projs.RemoveAt(n);
                else
                    n++;
            }
            // TODO: Add your update logic here
            foreach (Tower t in tower)
            {
                t.fuckingcamera(difL, new Vector2(game.widthFactor, game.heightFactor));                                                          //Correcting Camera location problems
                t.Stating(virus);                                                               //Detecting viruses
                t.Attacking(ref projs);
            }
            difL = new Vector2(0,0);



            if (mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Released && choosing && mouse.X < game.width - game.width / 12 && mouse.Y > 0 && mouse.X > 0 && mouse.Y < game.height - game.height / 5)
            {
                float zx = (mouse.X + Camera.Location.X) % (64 * game.widthFactor);
                float zy = (mouse.Y + Camera.Location.Y - 16) % (32 * game.heightFactor);
                int x = (int)((mouse.X + Camera.Location.X) / (64 * game.widthFactor));
                int y = (int)((mouse.Y + Camera.Location.Y + 16) / (32 * game.heightFactor));
                if (towers[x, 2*y] == null && TheMap(zx, zy))
                {
                    Tower create = new Tower(choice.name, choice.attack, choice.attack, choice.cooldown, 
                        new Vector2(x * (64 * game.widthFactor) + 16 * game.widthFactor - Camera.Location.X * game.widthFactor, 
                        y * (32 * game.heightFactor) - 56 * game.heightFactor - Camera.Location.Y * game.heightFactor),
                        choice.range, 
                        game.Content, 
                        game.spriteBatch, 
                        Etat.Alive);
                    tower.Add(create);
                    towers[x, 2*y] = create;
                }
                else
                {
                    int pr = TheMap2(zx, zy);
                    switch (pr)
                    {
                        case 1 :
                            break;
                        case 2:
                            x++;
                            break;
                        case 3:
                            x++;
                            y++;
                            break;
                        default:
                            y++;
                            break;
                    }
                    if (y >= 1 && towers[x, 2 * y -1] == null)
                    {
                        Tower create = new Tower(choice.name, choice.attack, choice.attack, choice.cooldown, new Vector2(x * (64 * game.widthFactor) - 16 * game.widthFactor - Camera.Location.X * game.widthFactor, y * (32 * game.heightFactor) -72 * game.heightFactor - Camera.Location.Y * game.heightFactor), choice.range, game.Content, game.spriteBatch, Etat.Alive);
                        tower.Add(create);
                        towers[x, 2 * y - 1] = create;
                        gold = gold - 10;
                    }
                }
            }

            if (mouse.RightButton == ButtonState.Pressed)
                choosing = false;



            oldMouse = mouse;
            LoadContent();

        }

      
        public void Draw(GameTime gameTime)
        {
            if (status == GameStateStatus.Pause)
                return;
            try
            {
                DrawMap();
            }
            catch {
                squaresAcross = myMap.MapWidth;
                squaresDown = myMap.MapHeight;
            }
            foreach (Virus v in virus)
            {
                v.StateDraw(game.widthFactor, game.heightFactor);                                                                  //Draw all active viruses
            }
            foreach (Projectile p in projs)
            {
                p.Draw(game.widthFactor, game.heightFactor);
            }
            int x = 0;
            while (x < towers.GetLength(0))
            {
                int y = 0;
                while (y < towers.GetLength(1))
                {
                    if (towers[x, y] != null && towers[x,y].exist)
                        towers[x, y].StateDraw(game.widthFactor, game.heightFactor);
                    y++;
                }
                x++;
            }
            
            
            Interface.Draw();                  // Affichage de l'interface par dessus la map et les virus et les tours

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

            // D�but de la boucle de boucle pour r�cup�rer les coordonn�es
            int i = 0;
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
                                ((int)((x * Tile.TileStepX - offsetX + rowOffset + baseOffsetX) * game.widthFactor)),
                                ((int)((y * Tile.TileStepY - offsetY + baseOffsetY) * game.heightFactor)),
                                (int) (Tile.TileWidth * game.widthFactor), (int)(Tile.TileHeight * game.heightFactor)),
                            Tile.GetSourceRectangle(tileID),
                            Color.White,
                            0.0f,
                            Vector2.Zero,
                            SpriteEffects.None,
                            1.0f);
                        i++;
                    }
                    
                    /*int heightRow = 0;
                    
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
                    }*/
                }
            }
            //Console.WriteLine(i);
        }

        public void Window_ClientSizeChanged()
        {
            LoadContent();

            foreach (Virus v in virus)
            {
                v.TheFullscreen(game.widthFactor, game.heightFactor);                                                                  //Adapt Fullscreen
            }
            foreach (Tower t in tower)
            {
                t.TheFullscreen(game.widthFactor, game.heightFactor);                                                                  //Adapt Fullscreen
            }
            foreach (Keypoint k in keypoints)
            {
                k.TheFullscreen(game.widthFactor, game.heightFactor);
            }

        }

        public void ChangeState(IState state)
        {
            game.gameState = state;
            
        }

        public bool TheMap(float x, float y)
        {
            if (x < 32 && y < 16)
            {
                if (-(x) + 16 <= y)
                    return true;
                else
                    return false;
            }
            else if (x >= 32 && y < 16)
            {
                if ((x - 32) <= y)
                    return true;
                else
                    return false;
            }
            else if (x < 32 && y <= 16)
            {
                if (x >= y - 16)
                    return true;
                else
                    return false;
            }
            else if (x >= 32 && y >= 16)
            {
                if (-((x - 32)) + 16 <= y - 16)
                    return true;
                else
                    return false;
            }
            else
                return true;
        }

        public int TheMap2(float x, float y)
        {
            if (x < 32 && y < 16)
                return 1;
            else if (x >= 32 && y < 16)
                return 2;
            else if (x >= 32 && y >= 16)
                return 3;
            else
                return 4;
        }
    }
}
