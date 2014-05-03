using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Server
{
    public class MyTextBox : RichTextBox
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            SolidBrush drawBrush = new SolidBrush(ForeColor);
            // Draw string to screen.
            e.Graphics.DrawString(Text, Font, drawBrush, 0f, 0f);
        }

        public MyTextBox()//constructor
        {
            // This call is required by the Windows.Forms Form Designer.
            
            this.SetStyle(ControlStyles.UserPaint, true);

            // TODO: Add any initialization after the InitForm call
        }
    }
}
