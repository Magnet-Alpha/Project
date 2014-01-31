﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Buttons
{
    class MapCell
    {
        public List<int> BaseTiles = new List<int>();
        public List<int> HeightTiles = new List<int>();
        public List<int> TopperTiles = new List<int>();

        public int TileID
        {
            get { return BaseTiles.Count > 0 ? BaseTiles[0] : 0; }
            set
            {
                if (BaseTiles.Count > 0)
                    BaseTiles[0] = value;
                else
                    AddBaseTile(value);
            }
        }

        public void AddBaseTile(int tileID) //Première couche de texture (sol)
        {
            BaseTiles.Add(tileID);
        }

        public void AddHeightTile(int tileID) // Deuxième couche de texture (élément sur le sol)
        {
            HeightTiles.Add(tileID);
        }

        public void AddTopperTile(int tileID) // Troisième couche de texture (Décor traversable)
        {
            TopperTiles.Add(tileID);
        }

        public MapCell(int tileID)
        {
            TileID = tileID;
        }
    }
}
