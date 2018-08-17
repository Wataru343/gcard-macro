using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace gcard_macro
{
    public partial class FormMain : Form
    {
        private ChromeDriver driver_;
        
        public FormMain()
        {
            InitializeComponent();

            textBoxWaitSearch.Text = Properties.Settings.Default.WaitSearch.ToString();
            textBoxWaitBattle.Text = Properties.Settings.Default.WaitBattle.ToString();
            textBoxWaitAttack.Text = Properties.Settings.Default.WaitAttack.ToString();
            textBoxWaitReceive.Text = Properties.Settings.Default.WaitReceive.ToString();
            textBoxWaitAccessBlock.Text = Properties.Settings.Default.WaitAccessBlock.ToString();
            textBoxWaitMisc.Text = Properties.Settings.Default.WaitMisc.ToString();

            timerWatchBrowser.Start();
        }

        private void buttonRunBrowser_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                driver_ = Webdriver.Instance;

                buttonRunBrowser.Enabled = false;
                buttonStopBrowser.Enabled = true;


                if (driver_.Url != "http://gcc.sp.mbga.jp/_gcard_my_room")
                {
                    MessageBox.Show("ログインに失敗しました", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    buttonStopBrowser.PerformClick();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Chromeの起動に失敗しました", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }


        private void buttonStopBrowser_Click(object sender, EventArgs e)
        {
            try
            {
                Webdriver.Close();
            }
            catch { }

            buttonRunBrowser.Enabled = true;
            buttonStopBrowser.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void timerWatchBrowser_Tick(object sender, EventArgs e)
        {
            if(driver_ != null)
            {
                Task.Run(() =>
                {
                    try
                    {
                        string aa = driver_.PageSource.ToString();
                    }
                    catch
                    {
                        driver_ = null;
                        Webdriver.Close();
                        Invoke((MethodInvoker)delegate
                        {
                            buttonStopBrowser.PerformClick();
                        });
                    }
                });
            }
        }

        private void textBoxWaitSearch_KeyPress(object sender, KeyPressEventArgs e) => e.Handled = Utils.ValidDouble(sender as TextBox, e);
        private void textBoxWaitBattle_KeyPress(object sender, KeyPressEventArgs e) => e.Handled = Utils.ValidDouble(sender as TextBox, e);
        private void textBoxWaitAttack_KeyPress(object sender, KeyPressEventArgs e) => e.Handled = Utils.ValidDouble(sender as TextBox, e);
        private void textBoxWaitReceive_KeyPress(object sender, KeyPressEventArgs e) => e.Handled = Utils.ValidDouble(sender as TextBox, e);
        private void textBoxWaitAccessBlock_KeyPress(object sender, KeyPressEventArgs e) => e.Handled = Utils.ValidDouble(sender as TextBox, e);
        private void textBoxWaitMisc_KeyPress(object sender, KeyPressEventArgs e) => e.Handled = Utils.ValidDouble(sender as TextBox, e);


        private void textBoxWaitSearch_TextChanged(object sender, EventArgs e)
        {
            tabControlRaid.WaitSearch = Convert.ToDouble(textBoxWaitSearch.Text);
            tabControlGroup.WaitSearch = Convert.ToDouble(textBoxWaitSearch.Text);
        }

        private void textBoxWaitBattle_TextChanged(object sender, EventArgs e)
        {
            tabControlRaid.WaitBattle = Convert.ToDouble(textBoxWaitBattle.Text);
            tabControlGroup.WaitBattle = Convert.ToDouble(textBoxWaitBattle.Text);
        }

        private void textBoxWaitAttack_TextChanged(object sender, EventArgs e)
        {
            tabControlRaid.WaitAttack = Convert.ToDouble(textBoxWaitAttack.Text);
            tabControlGroup.WaitAttack = Convert.ToDouble(textBoxWaitAttack.Text);
        }

        private void textBoxWaitReceive_TextChanged(object sender, EventArgs e)
        {
            tabControlRaid.WaitReceive = Convert.ToDouble(textBoxWaitReceive.Text);
            tabControlGroup.WaitReceive = Convert.ToDouble(textBoxWaitReceive.Text);
        }

        private void textBoxWaitAccessBlock_TextChanged(object sender, EventArgs e)
        {
            tabControlRaid.WaitAccessBlock = Convert.ToDouble(textBoxWaitAccessBlock.Text);
            tabControlGroup.WaitAccessBlock = Convert.ToDouble(textBoxWaitAccessBlock.Text);
        }

        private void textBoxWaitMisc_TextChanged(object sender, EventArgs e)
        {
            tabControlRaid.WaitMisc = Convert.ToDouble(textBoxWaitMisc.Text);
            tabControlGroup.WaitMisc = Convert.ToDouble(textBoxWaitMisc.Text);
        }


        private void buttonSave_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.WaitSearch = Convert.ToDouble(textBoxWaitSearch.Text);
            Properties.Settings.Default.WaitBattle = Convert.ToDouble(textBoxWaitBattle.Text);
            Properties.Settings.Default.WaitAttack = Convert.ToDouble(textBoxWaitAttack.Text);
            Properties.Settings.Default.WaitReceive = Convert.ToDouble(textBoxWaitReceive.Text);
            Properties.Settings.Default.WaitAccessBlock = Convert.ToDouble(textBoxWaitAccessBlock.Text);
            Properties.Settings.Default.WaitMisc = Convert.ToDouble(textBoxWaitMisc.Text);
            Properties.Settings.Default.Save();

            tabControlRaid.SaveSetting();
            tabControlGroup.SaveSetting();
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                Webdriver.Close();
            }
            catch { }
        }
    }
}
