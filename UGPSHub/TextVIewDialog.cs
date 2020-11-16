using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UGPSHub
{
    public partial class TextVIewDialog : Form
    {
        #region Properties

        public string ShowText
        {
            get { return txb.Text; }
            set { txb.Text = value; }

        }
        #endregion

        #region Constructor

        public TextVIewDialog()
        {
            InitializeComponent();
        }

        #endregion
    }
}
