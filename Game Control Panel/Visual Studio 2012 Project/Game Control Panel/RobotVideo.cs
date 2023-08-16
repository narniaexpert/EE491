using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Game_Control_Panel
{
    public partial class RobotVideo : Form
    {
        public RobotVideo(string URL)
        {
            InitializeComponent();
            this.webBrowser1.Url = new System.Uri(URL, System.UriKind.Absolute);
        }
    }
}
