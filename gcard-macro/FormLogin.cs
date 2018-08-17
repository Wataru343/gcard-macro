using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace gcard_macro
{
    public partial class FormLogin : Form
    {
        public ChromeDriver driver;

        public FormLogin()
        {
            InitializeComponent();
            Cursor.Current = Cursors.WaitCursor;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (driver != null && driver.Url == "http://gcc.sp.mbga.jp/_gcard_my_room")
                {
                    this.Close();
                }
            }
            catch
            {
                this.Close();
            }
        }

        private void FormLogin_FormClosed(object sender, FormClosedEventArgs e) => Cursor.Current = Cursors.Default;
    }
}
