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
    public partial class CheatForm : Form
    {
        GameState gameState;

        public CheatForm(GameState gameState)
        {
            InitializeComponent();
            this.gameState = gameState;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string str = textBox1.Text;
            if (str == "upgrade base")
            {
                Base.level++;
                return;
            }
            if (str == "upgrade towers")
            {
                foreach (Tower T in gameState.To)
                {
                    T.Upgrade();
                }
                return;
            }
            if (str.Length > 8 && str.Substring(0, 8) == "set life ")
            {
                int x;
                try
                {
                    x = Convert.ToInt16(str.Substring(9));
                    gameState.Life = x;
                    label1.Text = "Life set to " + x;
                }
                catch
                {
                    label1.Text = "Invalid argument: argument must be an integer";
                }
                return;
            }
            if (str.Length > 9 && str.Substring(0, 9) == "add gold ")
            {
                int x;
                try
                {
                    x = Convert.ToInt16(str.Substring(10));
                    gameState.gold += x;
                    label1.Text = "Added " + x + " gold";
                }
                catch
                {
                    label1.Text = "Invalid argument: argument must be an integer";
                }
                return;
            }
            if (str.Length > 11 && str.Substring(0, 10) == "add income ")
            {
                int x;
                try
                {
                    x = Convert.ToInt16(str.Substring(11));
                    gameState.income += x;
                    label1.Text = "Added " + x + " to income";
                }
                catch
                {
                    label1.Text = "Invalid argument: argument must be an integer";
                }
                return;
            }

            label1.Text = "Unknown command " + str;
        }
    }
}
