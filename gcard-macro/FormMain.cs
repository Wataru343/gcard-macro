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
using OpenQA.Selenium.Remote;

namespace gcard_macro
{
    public partial class FormMain : Form
    {
        private RemoteWebDriver driver_ { get; set; }
        private string UserName { get; set; }
        private string AppTitle { get; set; }
        
        public FormMain()
        {
            InitializeComponent();

            textBoxWaitSearch.Text = Properties.Settings.Default.WaitSearch.ToString();
            textBoxWaitBattle.Text = Properties.Settings.Default.WaitBattle.ToString();
            textBoxWaitAttack.Text = Properties.Settings.Default.WaitAttack.ToString();
            textBoxWaitReceive.Text = Properties.Settings.Default.WaitReceive.ToString();
            textBoxWaitAccessBlock.Text = Properties.Settings.Default.WaitAccessBlock.ToString();
            textBoxWaitMisc.Text = Properties.Settings.Default.WaitMisc.ToString();
            checkBoxAutoRun.Checked = Properties.Settings.Default.AutoRun;
            timerWatchBrowser.Start();

            tabControlRaid.BotActived += macroActivated;
            tabControlGroup.BotActived += macroActivated;
            tabControlGShooting.BotActived += macroActivated;
            tabControlPromotion.BotActived += macroActivated;
            tabControlGTactics.BotActived += macroActivated;

            tabControlRaid.Log += onLog;
            tabControlGroup.Log += onLog;
            tabControlGShooting.Log += onLog;
            tabControlPromotion.Log += onLog;
            tabControlGTactics.Log += onLog;


            AppTitle = "ガンダムカードコレクション自動化ツール Ver0.6.0β";
            this.Text = string.Format("{0} {1}", UserName, AppTitle);
        }

        private void buttonRunBrowser_Click(object sender, EventArgs e)
        {
            try
            {
                onLog(this, "ログイン中");

                Cursor.Current = Cursors.WaitCursor;
                driver_ = Webdriver.Instance;

                buttonRunBrowser.Enabled = false;
                buttonStopBrowser.Enabled = true;
                buttonRemoveCookie.Enabled = false;

                if (driver_.Url != "http://gcc.sp.mbga.jp/_gcard_my_room")
                {
                    MessageBox.Show("ログインに失敗しました", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    buttonStopBrowser.PerformClick();
                    onLog(this, "ログイン失敗");
                }

                onLog(this, "ログイン完了");

                try
                {
                    driver_.Navigate().GoToUrl(driver_.FindElementByXPath("//a[@class=\"profile\"]").GetAttribute("href"));
                    UserName = driver_.FindElementByXPath("//div[@class=\"name-and-rank\"]").Text.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)[0];
                    this.Text = string.Format("{0} - {1}", UserName, AppTitle);

                    onLog(this, string.Format("ユーザー名取得({0})", UserName));
                }
                catch
                {
                    onLog(this, "ユーザー名取得失敗");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Chromeの起動に失敗しました", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                buttonRunBrowser.Enabled = true;
                buttonStopBrowser.Enabled = false;
                onLog(this, "Chromeの起動に失敗しました");
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
            buttonRemoveCookie.Enabled = true;

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

                        try
                        {
                            Invoke((MethodInvoker)delegate
                            {
                                try
                                {
                                    buttonStopBrowser.PerformClick();
                                }
                                catch { }
                            });
                        }
                        catch { }
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


        private void textBoxWaitSearch_Validated(object sender, EventArgs e) => (sender as TextBox).Text = (sender as TextBox).Text == "" ? "0" : (sender as TextBox).Text;

        private void textBoxWaitBattle_Validated(object sender, EventArgs e) => (sender as TextBox).Text = (sender as TextBox).Text == "" ? "0" : (sender as TextBox).Text;

        private void textBoxWaitAttack_Validated(object sender, EventArgs e) => (sender as TextBox).Text = (sender as TextBox).Text == "" ? "0" : (sender as TextBox).Text;

        private void textBoxWaitReceive_Validated(object sender, EventArgs e) => (sender as TextBox).Text = (sender as TextBox).Text == "" ? "0" : (sender as TextBox).Text;

        private void textBoxWaitAccessBlock_Validated(object sender, EventArgs e) => (sender as TextBox).Text = (sender as TextBox).Text == "" ? "0" : (sender as TextBox).Text;

        private void textBoxWaitMisc_Validated(object sender, EventArgs e) => (sender as TextBox).Text = (sender as TextBox).Text == "" ? "0" : (sender as TextBox).Text;



        private void textBoxWaitSearch_TextChanged(object sender, EventArgs e)
        {
            double wait = 0;
            try
            {
                wait = Convert.ToDouble((sender as TextBox).Text);
            }
            catch { }

            tabControlRaid.WaitSearch = tabControlGroup.WaitSearch = tabControlGShooting.WaitSearch = tabControlPromotion.WaitSearch = tabControlGTactics.WaitSearch = wait;
        }

        private void textBoxWaitBattle_TextChanged(object sender, EventArgs e)
        {
            double wait = 0;
            try
            {
                wait = Convert.ToDouble((sender as TextBox).Text);
            }
            catch { }

            tabControlRaid.WaitBattle = tabControlGroup.WaitBattle = tabControlGShooting.WaitBattle = tabControlPromotion.WaitBattle = tabControlGTactics.WaitBattle = wait;
        }

        private void textBoxWaitAttack_TextChanged(object sender, EventArgs e)
        {
            double wait = 0;
            try
            {
                wait = Convert.ToDouble((sender as TextBox).Text);
            }
            catch { }

            tabControlRaid.WaitAttack = tabControlGroup.WaitAttack = tabControlGShooting.WaitAttack = tabControlPromotion.WaitAttack = tabControlGTactics.WaitAttack = wait;
        }

        private void textBoxWaitReceive_TextChanged(object sender, EventArgs e)
        {
            double wait = 0;
            try
            {
                wait = Convert.ToDouble((sender as TextBox).Text);
            }
            catch { }

            tabControlRaid.WaitReceive = tabControlGroup.WaitReceive = tabControlGShooting.WaitReceive = tabControlPromotion.WaitReceive = tabControlGTactics.WaitReceive = wait;
        }

        private void textBoxWaitAccessBlock_TextChanged(object sender, EventArgs e)
        {
            double wait = 0;
            try
            {
                wait = Convert.ToDouble((sender as TextBox).Text);
            }
            catch { }

            tabControlRaid.WaitAccessBlock = tabControlGroup.WaitAccessBlock = tabControlGShooting.WaitAccessBlock = tabControlPromotion.WaitAccessBlock = tabControlGTactics.WaitAccessBlock = wait;
        }

        private void textBoxWaitMisc_TextChanged(object sender, EventArgs e)
        {
            double wait = 0;
            try
            {
                wait = Convert.ToDouble((sender as TextBox).Text);
            }
            catch { }

            tabControlRaid.WaitMisc = tabControlGroup.WaitMisc = tabControlGShooting.WaitMisc = tabControlPromotion.WaitMisc = tabControlGTactics.WaitMisc = wait;
        }


        private void buttonSave_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.WaitSearch = Convert.ToDouble(textBoxWaitSearch.Text);
            Properties.Settings.Default.WaitBattle = Convert.ToDouble(textBoxWaitBattle.Text);
            Properties.Settings.Default.WaitAttack = Convert.ToDouble(textBoxWaitAttack.Text);
            Properties.Settings.Default.WaitReceive = Convert.ToDouble(textBoxWaitReceive.Text);
            Properties.Settings.Default.WaitAccessBlock = Convert.ToDouble(textBoxWaitAccessBlock.Text);
            Properties.Settings.Default.WaitMisc = Convert.ToDouble(textBoxWaitMisc.Text);
            Properties.Settings.Default.AutoRun = checkBoxAutoRun.Checked;
            Properties.Settings.Default.Save();

            tabControlRaid.SaveSetting();
            tabControlGroup.SaveSetting();
            tabControlGShooting.SaveSetting();
            tabControlPromotion.SaveSetting();
            tabControlGTactics.SaveSetting();
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                Webdriver.Close();
            }
            catch { }
        }

        private void FormMain_Shown(object sender, EventArgs e)
        {
            if (checkBoxAutoRun.Checked)
            {
                Task.Run(() =>
                {
                    System.Threading.Thread.Sleep(300);
                    Invoke((MethodInvoker)delegate
                    {
                        buttonRunBrowser.PerformClick();
                    });
                });
            }
        }

        private void macroActivated(object sender, bool activated)
        {
            var raid = tabControlRaid;
            var group = tabControlGroup;
            var gshooting = tabControlGShooting;
            var promotion = tabControlPromotion;
            var gtactics = tabControlGTactics;

            switch (sender)
            {
                case TabControlRaid tab: raid = null; break;
                case TabControlGroup tab: group = null; break;
                case TabControlGShooting tab: gshooting = null; break;
                case TabControlPromotion tab: promotion = null; break;
                case TabControlGTactics tab: gtactics = null; break;
                default: return;
            }

            raid?.EnableRunButton(!activated);
            group?.EnableRunButton(!activated);
            gshooting?.EnableRunButton(!activated);
            promotion?.EnableRunButton(!activated);
            gtactics?.EnableRunButton(!activated);
        }

        private void onLog(object sender, string text)
        {
            
            textBoxLog.AppendText(string.Format("{0}: {1}{2}", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff"), text, Environment.NewLine));
        }

        private void buttonRemoveCookie_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Cookieを削除しますか？", "", MessageBoxButtons.YesNo, MessageBoxIcon.None) == DialogResult.No)
                return;

            string path = Path.Combine(System.Environment.CurrentDirectory, "userdata");
            if (Directory.Exists(path))
            {
                try
                {
                    Directory.Delete(path, true);
                    MessageBox.Show("Cookieを削除しました", "", MessageBoxButtons.OK, MessageBoxIcon.None);
                }
                catch
                {
                    MessageBox.Show("Cookieの削除に失敗しました", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
