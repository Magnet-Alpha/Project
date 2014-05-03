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
        Form1 mainForm;
        
        public StatsForm(Form1 form)
        {
            InitializeComponent();
            FormClosed += new FormClosedEventHandler(formClosed);
            mainForm = form;
        }

        void formClosed(object sender, FormClosedEventArgs e)
        {
            mainForm.formShown = false;
        }

        public void ShowForm()
        {
            NetPeerStatistics stats = Program.s_server.Statistics;
            label1.Text = "";
            label1.Text += "Received: " + stats.ReceivedBytes + " B\n";
            label1.Text += "                " + stats.ReceivedPackets + " packets\n";
            label1.Text += "                " + stats.ReceivedMessages + " messages\n";
            label1.Text += "Sent: " + stats.SentBytes + " B\n";
            label1.Text += "          " + stats.SentPackets + " packets\n";
            label1.Text += "          " + stats.SentMessages + " messages";
            Show();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            NetPeerStatistics stats = Program.s_server.Statistics;
            label1.Text = "";
            label1.Text += "Received: " + stats.ReceivedBytes + " B\n";
            label1.Text += "                " + stats.ReceivedPackets + " packets\n";
            label1.Text += "                " + stats.ReceivedMessages + " messages\n";
            label1.Text += "Sent: " + stats.SentBytes + " B\n";
            label1.Text += "          " + stats.SentPackets + " packets\n";
            label1.Text += "          " + stats.SentMessages + " messages";
        }
    }
}
