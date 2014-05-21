using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Buttons
{
    static class Wave
    {
        public static int level = 0;
        public static List<Virus> viruses = new List<Virus>();

        public static void Fillwave(int cout, Start start, CustomSpriteBatch sb, ContentManager content)
        {
            level += 1;
            int number = (level + 25) / 5;
            int x = 0;
            int limite;
            if (level < 5)
                limite = 1;
            else if (level < 10)
                limite = 2;
            else
                limite = 3;
            Random a = new Random();
            while (x < number)
            {
                Virus v;
                int o = a.Next(0, limite);
                if (o == 0)
                    v = new Virus1(cout, start.position, content, sb);
                else if (o == 1)
                    v = new Virus2(cout, start.position, content, sb);
                else
                    v = new Virus3(cout, start.position, content, sb);
                viruses.Add(v);
                x++;
            }
        }

        public static void Send(ref List<Virus> virus, Start start)
        {
            if (viruses.Count > 0)
            {
                viruses[0].Position = start.position;
                virus.Add(viruses[0]);
                viruses.RemoveAt(0);
            }
        }
    }
}
