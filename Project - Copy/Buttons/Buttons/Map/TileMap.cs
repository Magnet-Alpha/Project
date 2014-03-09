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
            MapWidth = 30;
            MapHeight = 60;

            Random resist = new Random();
            for (int y = 0; y < MapHeight; y++)
            {
                MapRow thisRow = new MapRow();
                for (int x = 0; x < MapWidth; x++)
                {
                    int randomResist = resist.Next(0, 300);

                    switch (randomResist)
                    {
                        case 7:
                            thisRow.Columns.Add(new MapCell(10));
                            break;
                        case 42:
                            thisRow.Columns.Add(new MapCell(11));
                            break;
                        case 169:
                            thisRow.Columns.Add(new MapCell(12));
                            break;
                        case 299:
                            thisRow.Columns.Add(new MapCell(13));
                            break;
                        default:
                            thisRow.Columns.Add(new MapCell(0)); //Texture générale
                            break;
                    }
                }
                Rows.Add(thisRow);
        }

            /*
              Ajoutez la texture voulu ici en indiquant les coordonnées et la texture voulu.
              Le PNG de la texture est un tableau commençant par 0 !
            */

            /* En fait sans éditeur de map, ça fait MASSSSSS lignes de codes pour générer la map xD */



            Rows[5].Columns[1].AddBaseTile(20);
            Rows[6].Columns[1].AddBaseTile(20);
            Rows[5].Columns[4].AddBaseTile(20);
            Rows[6].Columns[5].AddBaseTile(20);
            Rows[14].Columns[1].AddBaseTile(20);
            Rows[15].Columns[1].AddBaseTile(20);
            Rows[15].Columns[4].AddBaseTile(20);
            Rows[14].Columns[5].AddBaseTile(20);

            Rows[5].Columns[12].AddBaseTile(20);
            Rows[6].Columns[12].AddBaseTile(20);
            Rows[5].Columns[15].AddBaseTile(20);
            Rows[6].Columns[16].AddBaseTile(20);
            Rows[14].Columns[12].AddBaseTile(20);
            Rows[15].Columns[12].AddBaseTile(20);
            Rows[15].Columns[15].AddBaseTile(20);
            Rows[14].Columns[16].AddBaseTile(20);

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

            Rows[14].Columns[3].AddBaseTile(1);
            Rows[15].Columns[3].AddBaseTile(3);
            Rows[16].Columns[3].AddBaseTile(1);
            Rows[17].Columns[3].AddBaseTile(3);
            Rows[18].Columns[3].AddBaseTile(1);
            Rows[19].Columns[3].AddBaseTile(3);
            Rows[20].Columns[3].AddBaseTile(1);
            Rows[21].Columns[3].AddBaseTile(3);
            Rows[22].Columns[3].AddBaseTile(1);
            Rows[23].Columns[3].AddBaseTile(3);
            Rows[24].Columns[3].AddBaseTile(1);
            Rows[25].Columns[3].AddBaseTile(3);
            Rows[26].Columns[3].AddBaseTile(1);
            Rows[27].Columns[3].AddBaseTile(3);
            Rows[28].Columns[3].AddBaseTile(1);
            Rows[29].Columns[3].AddBaseTile(3);
            Rows[30].Columns[3].AddBaseTile(1);
            Rows[31].Columns[3].AddBaseTile(3);
            Rows[32].Columns[3].AddBaseTile(1);
            Rows[33].Columns[3].AddBaseTile(3);
            Rows[34].Columns[3].AddBaseTile(1);
            Rows[35].Columns[3].AddBaseTile(1);
            Rows[36].Columns[3].AddBaseTile(1);
            Rows[37].Columns[3].AddBaseTile(1);
            Rows[38].Columns[3].AddBaseTile(4);
            Rows[15].Columns[2].AddBaseTile(2);
            Rows[17].Columns[2].AddBaseTile(2);
            Rows[19].Columns[2].AddBaseTile(2);
            Rows[21].Columns[2].AddBaseTile(2);
            Rows[23].Columns[2].AddBaseTile(2);
            Rows[25].Columns[2].AddBaseTile(2);
            Rows[27].Columns[2].AddBaseTile(2);
            Rows[29].Columns[2].AddBaseTile(2);
            Rows[31].Columns[2].AddBaseTile(2);
            Rows[33].Columns[2].AddBaseTile(2);
            Rows[35].Columns[2].AddBaseTile(2);
            Rows[37].Columns[2].AddBaseTile(2);

            Rows[37].Columns[4].AddBaseTile(1);
            Rows[37].Columns[5].AddBaseTile(1);
            Rows[37].Columns[6].AddBaseTile(1);
            Rows[37].Columns[7].AddBaseTile(1);
            Rows[37].Columns[8].AddBaseTile(1);
            Rows[37].Columns[9].AddBaseTile(1);
            Rows[37].Columns[10].AddBaseTile(1);
            Rows[37].Columns[11].AddBaseTile(1);
            Rows[37].Columns[12].AddBaseTile(1);
            Rows[37].Columns[13].AddBaseTile(3);
            Rows[36].Columns[4].AddBaseTile(1);
            Rows[36].Columns[5].AddBaseTile(1);
            Rows[36].Columns[6].AddBaseTile(1);
            Rows[36].Columns[7].AddBaseTile(1);
            Rows[36].Columns[8].AddBaseTile(1);
            Rows[36].Columns[9].AddBaseTile(1);
            Rows[36].Columns[10].AddBaseTile(1);
            Rows[36].Columns[11].AddBaseTile(1);
            Rows[36].Columns[12].AddBaseTile(1);
            Rows[36].Columns[13].AddBaseTile(1);
            Rows[35].Columns[4].AddBaseTile(1);
            Rows[35].Columns[5].AddBaseTile(1);
            Rows[35].Columns[6].AddBaseTile(1);
            Rows[35].Columns[7].AddBaseTile(1);
            Rows[35].Columns[8].AddBaseTile(1);
            Rows[35].Columns[9].AddBaseTile(1);
            Rows[35].Columns[10].AddBaseTile(1);
            Rows[35].Columns[11].AddBaseTile(1);
            Rows[35].Columns[12].AddBaseTile(1);
            Rows[35].Columns[13].AddBaseTile(3);
            Rows[34].Columns[4].AddBaseTile(5);
            Rows[34].Columns[5].AddBaseTile(5);
            Rows[34].Columns[6].AddBaseTile(5);
            Rows[34].Columns[7].AddBaseTile(5);
            Rows[34].Columns[8].AddBaseTile(5);
            Rows[34].Columns[9].AddBaseTile(5);
            Rows[34].Columns[10].AddBaseTile(5);
            Rows[34].Columns[11].AddBaseTile(5);
            Rows[34].Columns[12].AddBaseTile(5);
            Rows[34].Columns[13].AddBaseTile(1);
            Rows[38].Columns[4].AddBaseTile(4);
            Rows[38].Columns[5].AddBaseTile(4);
            Rows[38].Columns[6].AddBaseTile(4);
            Rows[38].Columns[7].AddBaseTile(4);
            Rows[38].Columns[8].AddBaseTile(4);
            Rows[38].Columns[9].AddBaseTile(4);
            Rows[38].Columns[10].AddBaseTile(4);
            Rows[38].Columns[11].AddBaseTile(4);
            Rows[38].Columns[12].AddBaseTile(4);
            Rows[38].Columns[13].AddBaseTile(4);

            Rows[33].Columns[13].AddBaseTile(3);
            Rows[32].Columns[13].AddBaseTile(1);
            Rows[31].Columns[13].AddBaseTile(3);
            Rows[30].Columns[13].AddBaseTile(1);
            Rows[29].Columns[13].AddBaseTile(3);
            Rows[28].Columns[13].AddBaseTile(1);
            Rows[27].Columns[13].AddBaseTile(3);
            Rows[26].Columns[13].AddBaseTile(1);
            Rows[25].Columns[13].AddBaseTile(3);
            Rows[24].Columns[13].AddBaseTile(1);
            Rows[23].Columns[13].AddBaseTile(3);
            Rows[22].Columns[13].AddBaseTile(5);
            Rows[33].Columns[12].AddBaseTile(2);
            Rows[31].Columns[12].AddBaseTile(2);
            Rows[29].Columns[12].AddBaseTile(2);
            Rows[27].Columns[12].AddBaseTile(2);
            Rows[25].Columns[12].AddBaseTile(1);
            Rows[23].Columns[12].AddBaseTile(1);

            Rows[23].Columns[11].AddBaseTile(1);
            Rows[23].Columns[10].AddBaseTile(1);
            Rows[23].Columns[9].AddBaseTile(1);
            Rows[23].Columns[8].AddBaseTile(1);
            Rows[23].Columns[7].AddBaseTile(2);
            Rows[24].Columns[12].AddBaseTile(1);
            Rows[24].Columns[11].AddBaseTile(1);
            Rows[24].Columns[10].AddBaseTile(1);
            Rows[24].Columns[9].AddBaseTile(1);
            Rows[24].Columns[8].AddBaseTile(1);
            Rows[25].Columns[11].AddBaseTile(1);
            Rows[25].Columns[10].AddBaseTile(1);
            Rows[25].Columns[9].AddBaseTile(1);
            Rows[25].Columns[8].AddBaseTile(1);
            Rows[25].Columns[7].AddBaseTile(2);
            Rows[26].Columns[12].AddBaseTile(4);
            Rows[26].Columns[11].AddBaseTile(4);
            Rows[26].Columns[10].AddBaseTile(4);
            Rows[26].Columns[9].AddBaseTile(4);
            Rows[26].Columns[8].AddBaseTile(4);
            Rows[22].Columns[12].AddBaseTile(5);
            Rows[22].Columns[11].AddBaseTile(5);
            Rows[22].Columns[10].AddBaseTile(5);
            Rows[22].Columns[9].AddBaseTile(5);
            Rows[22].Columns[8].AddBaseTile(1);

            Rows[21].Columns[8].AddBaseTile(3);
            Rows[20].Columns[8].AddBaseTile(1);
            Rows[19].Columns[8].AddBaseTile(3);
            Rows[18].Columns[8].AddBaseTile(1);
            Rows[17].Columns[8].AddBaseTile(3);
            Rows[16].Columns[8].AddBaseTile(1);
            Rows[15].Columns[8].AddBaseTile(3);
            Rows[14].Columns[8].AddBaseTile(1);
            Rows[13].Columns[8].AddBaseTile(3);
            Rows[12].Columns[8].AddBaseTile(1);
            Rows[11].Columns[8].AddBaseTile(1);
            Rows[10].Columns[8].AddBaseTile(1);
            Rows[21].Columns[7].AddBaseTile(2);
            Rows[19].Columns[7].AddBaseTile(2);
            Rows[17].Columns[7].AddBaseTile(2);
            Rows[15].Columns[7].AddBaseTile(2);
            Rows[13].Columns[7].AddBaseTile(2);
            Rows[11].Columns[7].AddBaseTile(2);

            Rows[9].Columns[7].AddBaseTile(2);
            Rows[8].Columns[8].AddBaseTile(5);
            Rows[8].Columns[9].AddBaseTile(5);
            Rows[8].Columns[10].AddBaseTile(5);
            Rows[8].Columns[11].AddBaseTile(5);
            Rows[8].Columns[12].AddBaseTile(5);
            Rows[8].Columns[13].AddBaseTile(5);
            Rows[9].Columns[8].AddBaseTile(1);
            Rows[9].Columns[9].AddBaseTile(1);
            Rows[9].Columns[10].AddBaseTile(1);
            Rows[9].Columns[11].AddBaseTile(1);
            Rows[9].Columns[12].AddBaseTile(1);
            Rows[9].Columns[13].AddBaseTile(1);
            Rows[10].Columns[9].AddBaseTile(1);
            Rows[10].Columns[10].AddBaseTile(1);
            Rows[10].Columns[11].AddBaseTile(1);
            Rows[10].Columns[12].AddBaseTile(1);
            Rows[10].Columns[13].AddBaseTile(1);
            Rows[11].Columns[9].AddBaseTile(1);
            Rows[11].Columns[10].AddBaseTile(1);
            Rows[11].Columns[11].AddBaseTile(1);
            Rows[11].Columns[12].AddBaseTile(1);
            Rows[11].Columns[9].AddBaseTile(1);
            Rows[12].Columns[9].AddBaseTile(4);
            Rows[12].Columns[10].AddBaseTile(4);
            Rows[12].Columns[11].AddBaseTile(4);
            Rows[12].Columns[12].AddBaseTile(4);
            Rows[12].Columns[13].AddBaseTile(4);

            Rows[8].Columns[13].AddBaseTile(1);
            Rows[8].Columns[14].AddBaseTile(1);
            Rows[8].Columns[15].AddBaseTile(1);
            Rows[9].Columns[13].AddBaseTile(1);
            Rows[9].Columns[14].AddBaseTile(1);
            Rows[7].Columns[13].AddBaseTile(1);
            Rows[7].Columns[14].AddBaseTile(1);
            Rows[10].Columns[13].AddBaseTile(1);
            Rows[10].Columns[14].AddBaseTile(1);
            Rows[10].Columns[15].AddBaseTile(1);
            Rows[11].Columns[13].AddBaseTile(1);
            Rows[11].Columns[14].AddBaseTile(1);
            Rows[12].Columns[13].AddBaseTile(1);
            Rows[12].Columns[14].AddBaseTile(1);
            Rows[12].Columns[15].AddBaseTile(1);
            Rows[13].Columns[13].AddBaseTile(1);
            Rows[13].Columns[14].AddBaseTile(1);
            Rows[7].Columns[12].AddBaseTile(2);
            Rows[9].Columns[12].AddBaseTile(1);
            Rows[11].Columns[12].AddBaseTile(1);
            Rows[13].Columns[12].AddBaseTile(2);
            Rows[6].Columns[13].AddBaseTile(5);
            Rows[6].Columns[14].AddBaseTile(5);
            Rows[6].Columns[15].AddBaseTile(5);
            Rows[7].Columns[15].AddBaseTile(3);
            Rows[9].Columns[15].AddBaseTile(3);
            Rows[11].Columns[15].AddBaseTile(3);
            Rows[13].Columns[15].AddBaseTile(3);
            Rows[14].Columns[13].AddBaseTile(4);
            Rows[14].Columns[15].AddBaseTile(4);
            Rows[14].Columns[14].AddBaseTile(4);


        }
    }
}
