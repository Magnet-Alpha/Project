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
        GameState gameState;
        public int score;
        public AddScoreForm(GameState gameState)
        {
            InitializeComponent();
            this.gameState = gameState;
            this.game = gameState.game;
            Text = "You'll Catch a Virus - " + Strings.stringForKey("SaveHighScore");
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
        }


        protected override CreateParams CreateParams
        {
            get
            {
                const int CS_NOCLOSE = 0x200;

                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_NOCLOSE;
                return cp;
            }
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
            if (game.settings.Fullscreen)
            {
                var form = (Form)Form.FromHandle(game.Window.Handle);
                form.WindowState = FormWindowState.Minimized
                    ;
            }
            if(!IsDisposed && score > 0)
                Show();

        }

        private void button1_Click(object sender, EventArgs e) // save
        {
            game.settings.addScore(new HighScore(textBox1.Text + (gameState.cheatForm.cheated ? " <Cheated>" : ""), score));
            game.settings.saveHighScores();
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
