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
        public int MapWidth = 100;//30
        public int MapHeight = 100;//60

        public TileMap()
        {
            for (int y = 0; y < MapHeight; y++)
            {
                MapRow thisRow = new MapRow();
                for (int x = 0; x < MapWidth; x++)
                {
                    thisRow.Columns.Add(new MapCell(1)); //Texture générale
                }
                Rows.Add(thisRow);
            }

            /*
              Ajoutez la texture voulu ici en indiquant les coordonnées et la texture voulu.
              Le PNG de la texture est un tableau commençant par 0 !
            */
            
            /*Rows[14].Columns[5].AddHeightTile(80);
            Rows[16].Columns[5].AddHeightTile(54);
            Rows[18].Columns[5].AddHeightTile(54);
            Rows[20].Columns[5].AddHeightTile(54);
            Rows[12].Columns[4].AddHeightTile(54);
            Rows[13].Columns[4].AddHeightTile(54);
            Rows[13].Columns[5].AddHeightTile(54);
            Rows[12].Columns[6].AddHeightTile(54);*/
        }
    }
}
