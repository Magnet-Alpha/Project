using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace Buttons
{
    public partial class GetIPForm : Form
    {
        public IPAddress ip;
        MultiplayerState3 multiState;
        public bool shown = true;

        public GetIPForm(MultiplayerState3 multiState)
        {
            InitializeComponent();

            Location = new Point(multiState.game.Window.ClientBounds.X, multiState.game.Window.ClientBounds.Y);

            ownServer.Text = Strings.stringForKey("OwnServer");
            connectServer.Text = Strings.stringForKey("NotOwnServer");
            ipField.Enabled = false;
            this.multiState = multiState;
            ownServer.Checked = true;
            Text = Strings.stringForKey("ConnectToServer");


            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            ControlBox = false;

            ipField.KeyDown += new KeyEventHandler(keyDown);
            KeyDown += new KeyEventHandler(keyDown);
            button2.Text = Strings.stringForKey("Cancel");
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

        public void showForm()
        {
            if(multiState.game.settings.Fullscreen)
            {
                Form form = (Form)Form.FromHandle(multiState.game.Window.Handle);
                form.WindowState = FormWindowState.Minimized;
                
            }
                Show();
                BringToFront();
        }

        private string GetLocalIP()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    return ip.ToString();
            }
            return "127.0.0.1";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ownServer.Checked)
            {
                ip = IPAddress.Parse(GetLocalIP());
                CloseForm();
            }
            else
            {
                try
                {
                    ip = IPAddress.Parse(ipField.Text);
                    CloseForm();
                }
                catch
                {
                    MessageBox.Show("Invalid IP address");
                }
            }

        }

        void CloseForm()
        {
            if (multiState.game.settings.Fullscreen)
            {
                var form = (Form)Form.FromHandle(multiState.game.Window.Handle);
                form.WindowState = FormWindowState.Normal;
                 
            }
            Console.WriteLine("Form closing");
            multiState.formClosed();
            shown = false;
            this.Close();

        }

        private void connectServer_CheckedChanged(object sender, EventArgs e)
        {
            ipField.Enabled = true;
        }

        private void ownServer_CheckedChanged(object sender, EventArgs e)
        {
            ipField.Enabled = false;
        }

        void keyDown(object sender,KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return || e.KeyCode == Keys.Enter)
                button1_Click(sender, e);
        }

        private void GetIPForm_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            multiState.ChangeState(new MenuState(multiState.game));
            Close();
        }
    }
}
