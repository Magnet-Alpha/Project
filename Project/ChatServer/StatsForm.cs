using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Lidgren.Network;


namespace Server
{
    public partial class StatsForm : Form
    {
        NetServer server;

        public StatsForm(NetServer server)
        {
            InitializeComponent();
            this.server = server;
            label1.Text = "a\nb\nc";
        }

        private void StatsForm_Load(object sender, EventArgs e)
        {

        }
    }
}
