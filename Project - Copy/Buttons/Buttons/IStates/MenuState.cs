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
    public class MenuState : IState
    {
        public bool isActive = true;
        TextButton newGameButton;
        TextButton continueButton;
        TextButton optionsButton;
        TextButton exitButton;
        TextButton websiteButton;
        SpriteFont font;
        Texture2D background = Textures.background;
        InterfaceMenu mainMenu;
        int gap;
        Game1 game;
        List<Keypoint> keypoints = new List<Keypoint>();
        Virus test;
        Keypoint test3;
        Keypoint test4;
        Keypoint test5;
        Keypoint test6;
        Keypoint test7;
        Keypoint test8;
        List<Virus> virus = new List<Virus>();
        List<int> indexs = new List<int>();

        public MenuState(Game1 game)
        {
            this.game = game;
            LoadContent();
            Initialize();
            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        public void Initialize()
        {
            // TODO: Add your initialization logic here

            
            mainMenu.MenuOn = true;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        public void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures. 

            background = Textures.background;

            font = game.Content.Load<SpriteFont>("Font");
            gap = (int)font.MeasureString("L").Y;
            //menu general
            
            continueButton = new TextButton(font, game, Strings.stringForKey("Multiplayer"), new Vector2(game.width /20 , game.height/2 - gap/2));

            newGameButton = new TextButton(font, game, Strings.stringForKey("NewGame"), new Vector2(game.width / 27, continueButton.top - gap));
            optionsButton = new TextButton(font, game, Strings.stringForKey("Options"), new Vector2(game.width /16 , game.height/2 + gap/2));
            websiteButton = new TextButton(font, game, "GeekHub", new Vector2(game.width / 12, optionsButton.bottom + gap - (int)font.MeasureString("GeekHub").Y));
            exitButton = new TextButton(font, game, Strings.stringForKey("Exit"), new Vector2(game.width / 10, websiteButton.bottom + gap - (int) font.MeasureString("Exit").Y));
            

            mainMenu = new InterfaceMenu(new TextButton[] { newGameButton, continueButton, optionsButton, exitButton, websiteButton }, new Text[] { }, background, game);
            mainMenu.MenuOn = true;

            test = new Virus("b", 10, 10, 5, new Vector2(500, 100), 1, game.Content, game.spriteBatch, Etat.Alive);
            test3 = new Keypoint(new Vector2(500, 100), true, false);
            test4 = new Keypoint(new Vector2(500, 300), true, false);
            test5 = new Keypoint(new Vector2(700, 100), true, false);
            test6 = new Keypoint(new Vector2(700, 300), true, false);
            test7 = new Keypoint(new Vector2(500, 0), true, false);
            test8 = new Keypoint(new Vector2(700, 0), true, false);
            keypoints.Add(test3);
            keypoints.Add(test4);
            keypoints.Add(test5);
            keypoints.Add(test6);
            keypoints.Add(test7);
            keypoints.Add(test8);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                game.Exit();

            mainMenu.Update();
            MouseState ms = Mouse.GetState();
            if (mainMenu.buttonWithIndexPressed(0)) // New Game
            {
                ChangeState(new OSState(game));
            }

            if (mainMenu.buttonWithIndexPressed(1))
                game.gameState = new MultiplayerState2(game);


            if (mainMenu.buttonWithIndexPressed(2)) // Options
            {
                ChangeState(new OptionsState(game, this));
            }

            if (mainMenu.buttonWithIndexPressed(3)) // Exit
                game.Exit();
            if (mainMenu.buttonWithIndexPressed(4)) // Credits
            {
                ChangeState(new CreditState(game));
            }

            test.NewPosition(new Vector2(game.widthFactor, game.heightFactor));
            int life = 0;
            test.Turn(keypoints, ref life);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here


            mainMenu.Draw();
           
            /*String copyright = "Copyright GeekHub@Epita 2018";
            Vector2 vector = new Vector2(game.width - font.MeasureString(copyright).X - 10, game.height - font.MeasureString(copyright).Y);
            game.spriteBatch.DrawString(font, copyright, vector, Color.White);*/
            test.StateDraw(1, 1);

        }

        public void ChangeState(IState state)
        {
            game.gameState = state;
        }


        public void Window_ClientSizeChanged()
        {
            LoadContent();
            mainMenu.buttons[0].Location(game.width / 27, mainMenu.buttons[1].top - gap);
            mainMenu.buttons[1].Location(game.width / 20, game.height / 2 - gap / 2);
            mainMenu.buttons[2].Location(game.width / 16, game.height / 2 + gap / 2);
            mainMenu.buttons[3].Location(game.width / 11, mainMenu.buttons[2].bottom + gap - (int)font.MeasureString("Exit").Y);
        }

       

    }
}
