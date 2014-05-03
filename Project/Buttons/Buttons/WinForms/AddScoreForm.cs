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
    public partial class AddScoreForm : Form
    {
        Game1 game;
        public int score;
        public AddScoreForm(Game1 game)
        {
            InitializeComponent();
            this.game = game;
            Text = "You'll Catch a Virus - " + Strings.stringForKey("SaveHighScore");
            
            this.Shown += new EventHandler(shown);
        }

        void shown(object sender, EventArgs e)
        {
            Console.WriteLine(score);
        }

        public void ShowWithScore(int score)
        {
            this.score = score;
            label2.Text = Strings.stringForKey("YouScored") + " " + score + " points";
            if (Strings.Language == Language.French)
            {
                button1.Text = "Sauvegarder";
                button2.Text = "Annuler";
            }
            if(!IsDisposed && score > 0)
                Show();

        }

        private void button1_Click(object sender, EventArgs e) // save
        {
            game.settings.addScore(new HighScore(textBox1.Text, score));
            game.settings.saveSettings();
            Close();
            new HighScores(game).Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
