using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Buttons
{
    public partial class HighScores : Form
    {
        List<HighScore> scores;
        Game1 game;

        public HighScores(Game1 game)
        {

            InitializeComponent();
            this.game = game;
            scores = game.settings.Scores;
            foreach(HighScore hs in scores)
            {
                listBox1.Items.Add(hs.name + " " + hs.score);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
