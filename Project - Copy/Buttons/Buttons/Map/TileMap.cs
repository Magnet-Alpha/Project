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

            /* En fait sans éditeur de map, ça fait MASSSSSS lignes de codes pour générer la map xD */

            Rows[8].Columns[2].AddBaseTile(1);
            Rows[8].Columns[3].AddBaseTile(1);
            Rows[8].Columns[4].AddBaseTile(1);
            Rows[9].Columns[2].AddBaseTile(1);
            Rows[9].Columns[3].AddBaseTile(1);
            Rows[7].Columns[2].AddBaseTile(1);
            Rows[7].Columns[3].AddBaseTile(1);
            Rows[10].Columns[2].AddBaseTile(1);
            Rows[10].Columns[3].AddBaseTile(1);
            Rows[10].Columns[4].AddBaseTile(1);
            Rows[11].Columns[2].AddBaseTile(1);
            Rows[11].Columns[3].AddBaseTile(1);
            Rows[12].Columns[2].AddBaseTile(1);
            Rows[12].Columns[3].AddBaseTile(1);
            Rows[12].Columns[4].AddBaseTile(1);
            Rows[13].Columns[2].AddBaseTile(1);
            Rows[13].Columns[3].AddBaseTile(1);
            Rows[7].Columns[1].AddBaseTile(2);
            Rows[9].Columns[1].AddBaseTile(2);
            Rows[11].Columns[1].AddBaseTile(2);
            Rows[13].Columns[1].AddBaseTile(2);
            Rows[6].Columns[2].AddBaseTile(5);
            Rows[6].Columns[3].AddBaseTile(5);
            Rows[6].Columns[4].AddBaseTile(5);
            Rows[7].Columns[4].AddBaseTile(3);
            Rows[9].Columns[4].AddBaseTile(3);
            Rows[11].Columns[4].AddBaseTile(3);
            Rows[13].Columns[4].AddBaseTile(3);
            Rows[14].Columns[2].AddBaseTile(4);
            Rows[14].Columns[4].AddBaseTile(4);
            Rows[14].Columns[3].AddBaseTile(4);


        }
    }
}
