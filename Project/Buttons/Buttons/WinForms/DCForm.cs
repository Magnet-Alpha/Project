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
    public partial class DCForm : Form
    {
        string reason;
        MultiplayerState3 multiState;
        GrowLabel label1;

        public DCForm(string reason, MultiplayerState3 multiState)
        {
            InitializeComponent();
            this.reason = reason;
            this.multiState = multiState;
            label1 = new GrowLabel();
            label1.Text = reason;
            label1.Location = new Point(13, 13);
            ControlBox = false;
            label1.Width = Width - 26;
            Controls.Add(label1);
            this.Name = "Disconnected";
            button1.Text = Strings.stringForKey("Retry");
            button2.Text = Strings.stringForKey("ChangeServer");
            button3.Text = Strings.stringForKey("MainMenu");
            Text = Strings.stringForKey("Disconnected");
            if (multiState.game.settings.Fullscreen)
            {
                var form = (Form)Form.FromHandle(multiState.game.Window.Handle);
                form.WindowState = FormWindowState.Minimized;

            }
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

        private void button1_Click(object sender, EventArgs e) // retry connection
        {
            multiState.Connect(multiState.IPform.ip.ToString(), multiState.port);
            if (multiState.game.settings.Fullscreen)
            {
                var form = (Form)Form.FromHandle(multiState.game.Window.Handle);
                form.WindowState = FormWindowState.Maximized;

            }
            Close();
        }

        private void button2_Click(object sender, EventArgs e) //change server
        {
            new GetIPForm(multiState).Show();
            if (multiState.game.settings.Fullscreen)
            {
                var form = (Form)Form.FromHandle(multiState.game.Window.Handle);
                form.WindowState = FormWindowState.Maximized;

            }
            Close();
        }

        private void button3_Click(object sender, EventArgs e) // back to main menu
        {
            multiState.ChangeState(new MenuState(multiState.game));
            if (multiState.game.settings.Fullscreen)
            {
                var form = (Form)Form.FromHandle(multiState.game.Window.Handle);
                form.WindowState = FormWindowState.Maximized;

            }
            Close();
        }

    }

    public class GrowLabel : Label
    {
        private bool mGrowing;
        public GrowLabel()
        {
            this.AutoSize = false;
        }
        private void resizeLabel()
        {
            if (mGrowing) return;
            try
            {
                mGrowing = true;
                Size sz = new Size(this.Width, Int32.MaxValue);
                sz = TextRenderer.MeasureText(this.Text, this.Font, sz, TextFormatFlags.WordBreak);
                this.Height = sz.Height;
            }
            finally
            {
                mGrowing = false;
            }
        }
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            resizeLabel();
        }
        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            resizeLabel();
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            resizeLabel();
        }
    }





}
