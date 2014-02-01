using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ButtonImage
{
    class ImageButton
    {
        private Texture2D img;
        private SpriteBatch spriteBatch;
        private MouseState mouse;
        private MouseState oldMouse;
        public bool takingAction = false;
        public bool clicked = false;
        Rectangle ImgRectangle;
        Vector2 ImgLocation;
        bool clickable = true;

        public ImageButton(SpriteBatch sBatch, Texture2D image, Vector2 textLoc)
        {
            spriteBatch = sBatch;
            img = image;

            ImgLocation = textLoc;
            ImgRectangle = new Rectangle((int)ImgLocation.X, (int)ImgLocation.Y, (int)img.Height, (int)img.Width);
        }

        public Texture2D Img
        {
            get { return img; }
            set
            {
                img = value;
                ImgLocation = new Vector2();
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
            ImgLocation.X = x;
            ImgLocation.Y = y;
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
                    Console.WriteLine("Clicked ");
                    takingAction = true;
                    DrawButton(ImgRectangle);
                }
                else
                {
                    clicked = false;
                }

            }

            if (mouse.LeftButton == ButtonState.Released && oldMouse.LeftButton == ButtonState.Pressed && clicked)
            {
                Console.WriteLine("Action!");
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
            spriteBatch.Begin();

            spriteBatch.Draw(img, ImgRectangle, null, color, rotation, spriteOrigin, spriteEffects, spriteLayer);

            spriteBatch.End();

        }
    }
}