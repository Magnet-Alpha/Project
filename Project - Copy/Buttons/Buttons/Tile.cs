using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Buttons
{
    static class Tile
    {
        static public Texture2D TileSetTexture;
        static public int TileWidth = 64; //Pixel d'une case
        static public int TileHeight = 64;
        static public int TileStepX = 64;
        static public int TileStepY = 16;
        static public int OddRowXOffset = 32;
        static public int HeightTileOffset = 32;

        static public Vector2 originPoint = new Vector2(19, 39);

        //Passage de la 2D à la 2D Iso (Inclinaison de chaque case)
        static public Rectangle GetSourceRectangle(int tileIndex)
        {
            int tileY = tileIndex / (TileSetTexture.Width / TileWidth);
            int tileX = tileIndex % (TileSetTexture.Width / TileWidth);

            return new Rectangle(tileX * TileWidth, tileY * TileHeight, TileWidth, TileHeight);
        }
    }
}
