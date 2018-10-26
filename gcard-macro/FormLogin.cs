using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenQA.Selenium;

using OpenQA.Selenium.Interactions;

namespace gcard_macro
{
    public partial class FormLogin : Form
    {
        public IWebDriver driver;
        private bool GettingCookie { get; set; }

        public FormLogin()
        {
            InitializeComponent();
            GettingCookie = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (GettingCookie)
            {
                this.Close();
            }

            try
            {
                IWebElement elm = driver.FindElement(By.ClassName("swf"));

                Actions action = new Actions(driver);
                action.MoveToElement(elm, (int)(elm.Size.Width / 2.6), elm.Size.Height / 6 * 5).Click().Build().Perform();
            }
            catch { }

            try
            {
                if (driver != null && driver.Url == "http://gcc.sp.mbga.jp/_gcard_my_room")
                {
                    this.label1.Text = "Cookie取得中";
                    GettingCookie = true;
                }
            }
            catch
            {
                this.label1.Text = "Cookie取得中";
                GettingCookie = true;
            }
        }

        private void FormLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            //cookieを確認するまで待機
            Func<bool> checkLogin = () =>
            {
                string sql = "select host_key,name from cookies where host_key='.sp.mbga.jp'";
                try
                {
                    using (SQLiteConnection con = new SQLiteConnection("Data Source=./userdata/Default/Cookies2;"))
                    {
                        con.Open();
                        using (SQLiteCommand com = new SQLiteCommand(sql, con))
                        using (SQLiteDataReader reader = com.ExecuteReader())
                        {
                            while (reader.Read() == true)
                            {
                                if ((reader["name"] as string).IndexOf("SP_LOGIN_SESSION") >= 0)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
                catch { }

                return false;
            };


            do
            {
                System.Threading.Thread.Sleep(500);
                try
                {
                    File.Copy("./userdata/Default/Cookies", "./userdata/Default/Cookies2", true);
                }
                catch { }
            } while (!checkLogin());

            try
            {
                File.Delete("./userdata/Default/Cookies2");
            }
            catch { }
        }

        private void FormLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.label1.Text = "Cookie取得中";
        }
    }
}
