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
        public bool cheated = false;

        public CheatForm(GameState gameState)
        {
            InitializeComponent();
            this.gameState = gameState;
            FormBorderStyle = FormBorderStyle.FixedDialog;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            string str = textBox1.Text;
           
            if (str == "upgrade base")
            {
                Base.level++;
                label1.Text = "Base has been upgraded to level " + Base.level;
                return;
            }
            if (str == "upgrade towers")
            {
                foreach (Tower T in gameState.To)
                {
                    T.Upgrade();
                }
                label1.Text = "All towers have been upgraded";
                return;
            }

            List<string> words = getWords(str);
            if (words.Count != 3)
            {
                label1.Text = "Unknown command " + str;
                return;
            }

            switch (words[0])
            {
                case "add" :
                    switch (words[1])
                    {
                        case "gold":
                            try
                            {
                                int x = Convert.ToInt32(words[2]);
                                gameState.gold += x;
                                label1.Text = "Added " + x + " gold";
                                cheated = true;
                                return;
                            }
                            catch
                            {
                                label1.Text = "Argument must be an integer";
                                return;
                            }
                           
                        case "income":
                            try
                            {
                                int x = Convert.ToInt32(words[2]);
                                gameState.income += x;
                                label1.Text = "Added " + x + " to income";
                                cheated = true;
                                return;
                            }
                            catch
                            {
                                label1.Text = "Argument must be an integer";
                                return;
                            }
                            
                        default:
                            label1.Text = "Unknown command: " + str;
                            return;
                    }
                case "set":
                    if (words[1] == "life")
                    {
                        try
                        {
                            int x = Convert.ToInt32(words[2]);
                            gameState.Life = x;
                            label1.Text = "Life set to " + x;
                            cheated = true;
                            return;
                        }
                        catch
                        {
                            label1.Text = "Argument must be an integer";
                            return;
                        }
                    }
                    else
                    {
                        label1.Text = "Unknown command: " + str;
                        return;
                    }
                default:
                    label1.Text = "Unknown command: " + str;
                    return;
            }

            
        }

        List<string> getWords(String str)
        {
            List<string> words = new List<string>();
            Console.WriteLine(str);
            string word = "";
            foreach (char c in str)
            {
                if (c == ' ')
                {
                    words.Add(word);
                    word = "";
                }
                else
                {
                    word += c;
                }
            }
            words.Add(word);
            foreach(string s in words)
            {
                Console.Write(s + ", ");
            }
            return words;
        }
    }
}
