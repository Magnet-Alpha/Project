using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Buttons
{
    class ImageButton : Button
    {
        private Texture2D img;
        private CustomSpriteBatch spriteBatch;
        private MouseState mouse;
        private MouseState oldMouse;
        public bool takingAction = false;
        public bool clicked = false;
        public Rectangle ImgRectangle;
        public Rectangle ImgLocation;
        private Game1 game;
        bool clickable = true;

        public ImageButton(CustomSpriteBatch sBatch, Texture2D image, Rectangle imgLoc, Game1 game)
        {
            spriteBatch = sBatch;
            img = image;
            this.game = game;
            ImgLocation = imgLoc;
            ImgRectangle = new Rectangle((int)ImgLocation.X, (int)ImgLocation.Y, (int)img.Height, (int)img.Width);
        }

        public ImageButton(Texture2D img, Rectangle imgLoc, Game1 game)
        {
            spriteBatch = game.spriteBatch;
            this.img = img;
            this.game = game;
            ImgLocation = imgLoc;
            ImgRectangle = imgLoc;
        }

        public Texture2D Img
        {
            get { return img; }
            set
            {
                img = value;
                ImgLocation = new Rectangle();
            }
        }
        public bool Clickable
        {
            get { return clickable; }
            set
            {
                clickable = value;
                Draw();
            }
        }
        public bool TakingAction()
        {
            return takingAction;

        }
        public int bottom
        {
            get
            {
                return ImgRectangle.Height + ImgRectangle.Y;
            }
        }

        public int top
        {
            get { return ImgRectangle.Y; }
        }
        public int right
        {
            get { return ImgRectangle.X + ImgRectangle.Width; }
        }
        public int left
        {
            get { return ImgRectangle.X; }
        }


        public void Location(int x, int y)
        {
            ImgRectangle.X = x;
            ImgRectangle.Y = y;
        }

        public void Update()
        {
            if (!clickable)
                return;

            mouse = Mouse.GetState();

            takingAction = false;

            if (mouse.LeftButton == ButtonState.Pressed)
            {
                if (ImgRectangle.Contains(new Point(mouse.X, mouse.Y)))
                {
                    clicked = true;
                    takingAction = true;
                }
                else
                {
                    clicked = false;
                }

            }

            if (mouse.LeftButton == ButtonState.Released && oldMouse.LeftButton == ButtonState.Pressed && clicked)
            {
                takingAction = true;
            }


            oldMouse = mouse;
        }

        public void Draw()
        {
            if (clickable)
            {
                DrawButton(ImgRectangle);
            }
        }
        private void DrawButton(Rectangle boundaries)
        {
            float xScale = (boundaries.Width);
            float yScale = (boundaries.Height);
            float scale = Math.Min(xScale, yScale);

            //Put the image on location we want
            Vector2 position = new Vector2();
            position.X = ((boundaries.Width / 2) + boundaries.X);
            position.Y = ((boundaries.Height / 2) + boundaries.Y);

            // A bunch of settings where we just want to use reasonable defaults.
            float rotation = 0.0f;
            Vector2 spriteOrigin = new Vector2(0, 0);
            float spriteLayer = 0.0f; // all the way in the front
            SpriteEffects spriteEffects = new SpriteEffects();

            Color color;

            if (clicked && mouse.LeftButton == ButtonState.Pressed)
            {
                color = Color.Silver;
            }
            else
            {
                color = Color.White;
            }
            
            game.spriteBatch.Draw(img, ImgRectangle, null, color, rotation, spriteOrigin, spriteEffects, spriteLayer);
        }
        
        public void TheFullscreen(int w, int h)
        {
            this.ImgLocation = new Rectangle(this.ImgLocation.X * w, this.ImgLocation.Y * h, img.Width, img.Height);
        }
    }
}