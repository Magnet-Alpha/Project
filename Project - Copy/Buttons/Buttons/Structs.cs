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
    public struct Text
    {
        public string textValue;
        public Vector2 location;
        public SpriteFont font;
        public Text(string textValue, Vector2 location, SpriteFont font)
        {
            this.textValue = textValue;
            this.location = location;
            this.font = font;
        }

    }

    struct Keypoint
    {
        public Vector2 position;
        public bool vers_g;
        public bool objectif;
        public Keypoint(Vector2 position, bool vers_g, bool objectif)
        {
            this.position = position;
            this.vers_g = vers_g;
            this.objectif = objectif;
        }
    }

    public enum GameState
    {
        Menu,
        InGame,
        Pause,
    }

 }