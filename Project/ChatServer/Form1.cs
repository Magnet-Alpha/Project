using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;

namespace Server
{
	public partial class Form1 : Form
	{
        public bool formShown = false;
        public MyTextBox richTextBox1;
		public Form1()
		{
			InitializeComponent();
            Text = "You'll Catch A Virus Server";
            FormClosed += new FormClosedEventHandler(formClosed);
            richTextBox1 = new MyTextBox();
            richTextBox1.Location = new System.Drawing.Point(12, 12);
            richTextBox1.Width = 570;
            richTextBox1.Height = 309;
            richTextBox1.Enabled = false;
            Controls.Add(richTextBox1);
            
		}

        void formClosed(object sender, FormClosedEventArgs e)
        {
            Program.formClosed();
        }

		private void button1_Click(object sender, EventArgs e)
		{
			if (button1.Text == "Start")
			{
				Program.StartServer();
				button1.Text = "Shut down";
			}
			else
			{
                Program.formClosed();
				Program.Shutdown();
				button1.Text = "Start";
                
			}
		}

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            new StatsForm(this).ShowForm();
            formShown = true;
        }
	}
}
