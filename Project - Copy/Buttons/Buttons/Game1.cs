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
    /// <summary>
    /// This is the main type for your game//
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public GraphicsDeviceManager graphics;
        public CustomSpriteBatch spriteBatch;
        public IState gameState;
        public SoundEffectInstance music;
        public int height;
        public int width;
        public UserSetting settings;
        public float widthFactor;
        public float heightFactor;
        public Texture2D cursor;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
            
            this.Window.Title = "You'll Catch A Virus";
            music = Content.Load<SoundEffect>("music").CreateInstance();
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 460; 
            music.IsLooped = true;
            settings = new UserSetting();
            Console.WriteLine(settings.musicVolume + " " + settings.SoundEffectVolume);
            graphics.IsFullScreen = settings.fullScreen;
            graphics.ApplyChanges();
            widthFactor = GraphicsDevice.PresentationParameters.BackBufferWidth/800;
            heightFactor = GraphicsDevice.PresentationParameters.BackBufferHeight/460;
        }


        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            IsMouseVisible = false;
            SoundEffect.MasterVolume = 1;
            
            height = GraphicsDevice.PresentationParameters.BackBufferHeight;
            width = GraphicsDevice.PresentationParameters.BackBufferWidth;
            music.Play();
            Window.ClientSizeChanged += new EventHandler<EventArgs>(Window_ClientSizeChanged);

            music.Volume = settings.musicVolume;
            Textures.Load(Content);
            gameState = new IntroState(this);

            cursor = Content.Load<Texture2D>("sprites\\cursor");

            

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures. 
            spriteBatch = new CustomSpriteBatch(GraphicsDevice);

            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            gameState.Update(gameTime);

            Draw(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void draw()
        {
            Draw(new GameTime());
        }

        protected override void Draw(GameTime gameTime)
        {
            MouseState mouse = Mouse.GetState();
           
            gameState.Draw(gameTime);
            spriteBatch.Draw(cursor, new Rectangle(mouse.X, mouse.Y, cursor.Width/5, cursor.Height/5), Color.White);

            base.Draw(gameTime);
        }

        public void Window_ClientSizeChanged(object sender, EventArgs e)
        {
           

            height = GraphicsDevice.PresentationParameters.BackBufferHeight;
            width = GraphicsDevice.PresentationParameters.BackBufferWidth;
            widthFactor = width / 800f;
            heightFactor = height / 460f;
            gameState.Window_ClientSizeChanged();
            //Console.WriteLine(widthFactor.ToString() + "x" + heightFactor.ToString() + "   " + width + "x" + height);
        }

    }
}
