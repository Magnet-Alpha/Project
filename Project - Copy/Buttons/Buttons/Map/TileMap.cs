using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Buttons
{
    class MapRow
    {
        public List<MapCell> Columns = new List<MapCell>();
    }

    class TileMap // Class pour gérer le contenu de la map
    {
        public List<MapRow> Rows = new List<MapRow>();
        // Taille de la map
        public int MapWidth;
        public int MapHeight;

        public TileMap(int width, int height)
        {
            MapWidth = 100;
            MapHeight = 130;
            for (int y = 0; y < MapHeight; y++)
            {
                MapRow thisRow = new MapRow();
                for (int x = 0; x < MapWidth; x++)
                {
                    thisRow.Columns.Add(new MapCell(0)); //Texture générale
                }
                Rows.Add(thisRow);
            }

            /*
              Ajoutez la texture voulu ici en indiquant les coordonnées et la texture voulu.
              Le PNG de la texture est un tableau commençant par 0 !
            */

            Rows[2].Columns[0].AddBaseTile(1);
            Rows[1].Columns[0].AddBaseTile(10);
            Rows[1].Columns[1].AddBaseTile(11);
            Rows[4].Columns[0].AddBaseTile(12);
            Rows[3].Columns[0].AddBaseTile(13);
        }
    }
}
