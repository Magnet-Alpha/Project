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
using System.Runtime.InteropServices;
using System.Windows.Forms;

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
        public SoundEffectInstance music2;
        int Height;
        int Width;
        public UserSetting settings;
        public float widthFactor;
        public float heightFactor;
        public Texture2D cursor;
        public bool drawMouse = true;


        public bool enableClose = false;

        public int width
        {
            get { return Width; }
            set
            {
                float withFactor = graphics.PreferredBackBufferWidth / value / 10;
                graphics.PreferredBackBufferWidth = (int) (graphics.PreferredBackBufferWidth* widthFactor);
                Console.WriteLine("Width set to" + graphics.PreferredBackBufferWidth);
                Width = value;
            }
        }
        public int height
        {
            get { return Height; }
            set
            {
                float withFactor = graphics.PreferredBackBufferHeight / value / 10;
                graphics.PreferredBackBufferHeight = (int)(graphics.PreferredBackBufferHeight * widthFactor);
                Console.WriteLine("Height set to" + graphics.PreferredBackBufferHeight);
                Height = value;
            }
        }

        public Game1()
        {

           /* IntPtr hSystemMenu = GetSystemMenu(this.Window.Handle, false);
            EnableMenuItem(hSystemMenu, SC_CLOSE, (uint)(MF_ENABLED | (false ? MF_ENABLED : MF_GRAYED)));*/

            graphics = new GraphicsDeviceManager(this);
            
            Content.RootDirectory = "Content";
            
            this.Window.Title = "You'll Catch A Virus";
            music = Content.Load<SoundEffect>("music").CreateInstance();
            music2 = Content.Load<SoundEffect>("game2").CreateInstance();
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            music.IsLooped = true;
            music2.IsLooped = true;
            settings = new UserSetting(this);
            
            
            
            Strings.Language = settings.language;

            if (settings.Fullscreen)
                graphics.ToggleFullScreen();

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

            graphics.IsFullScreen = settings.Fullscreen;
            graphics.ApplyChanges();
            widthFactor = GraphicsDevice.PresentationParameters.BackBufferWidth / 800;
            heightFactor = GraphicsDevice.PresentationParameters.BackBufferHeight / 460;

            height = GraphicsDevice.PresentationParameters.BackBufferHeight;
            width = GraphicsDevice.PresentationParameters.BackBufferWidth;
            

            music.Volume = settings.musicVolume;
            music2.Volume = settings.musicVolume;
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



            Form f = Form.FromHandle(Window.Handle) as Form;
            if (f != null)
            {
                f.FormClosing += f_FormClosing;
            } 

            
        }
        void f_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!enableClose)
                e.Cancel = true;
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

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                this.Exit();
            /*if (this.IsActive)
            {
                MouseState ms = Mouse.GetState();
                if (ms.X < 0)
                    Mouse.SetPosition(10, ms.Y);
                if (ms.X > width)
                    Mouse.SetPosition(width - 10, ms.Y);
                if (ms.Y < 0)
                    Mouse.SetPosition(ms.X, 10);
                if (ms.Y > height)
                    Mouse.SetPosition(ms.X, height - 10);
            }*/
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
            if(drawMouse)
                spriteBatch.Draw(cursor, new Rectangle(mouse.X, mouse.Y, cursor.Width/5, cursor.Height/5), Color.White);

            base.Draw(gameTime);
        }

        

    }
}
