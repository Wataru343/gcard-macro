using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gcard_macro
{
    public partial class TabControlShootingRange : UserControl
    {
        private ShootingRange ShootingRange { get; set; }
        public bool IsStart { get; set; }
        private Label CurrentState { get; set; }

        public double WaitSearch { get; set; }
        public double WaitBattle { get; set; }
        public double WaitAttack { get; set; }
        public double WaitReceive { get; set; }
        public double WaitContinueSearch { get; set; }
        public double WaitAccessBlock { get; set; }
        public double WaitMisc { get; set; }
        public string UserName { get; set; }
        public uint OptimizedWaitEnemyCount { get; set; }
        public TimeSpan CycleRecieveTime { get; set; }

        public delegate void BotActiveHandler(object sender, bool actived);
        public event BotActiveHandler BotActived;
        public event Event.LogHandler Log;
        public delegate void SettingChangedHandler(object sender, EventArgs e);
        public event SettingChangedHandler SettingChanged;

        public TabControlShootingRange()
        {
            InitializeComponent();
            timerWatchWebdriver.Start();
            ShootingRange = null;
            IsStart = false;
            buttonStop.Enabled = false;

            textBoxURL.Text = Properties.Settings.Default.ShootingRangeURL;
            textBoxThresholdFocusShot.Text = Properties.Settings.Default.ShootingRangeThresholdFocusShot.ToString();
            checkBoxUseFocusShotDuringFever.Checked = Properties.Settings.Default.ShootingRangeUseFocusShotDuringFever;
            checkBoxUseFeverTip.Checked = Properties.Settings.Default.ShootingRangeUseFeverTip;
            checkBoxAutoStop.Checked = Properties.Settings.Default.ShootingRangeAutoStop;

            CycleRecieveTime = TimeSpan.FromHours(1);
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
#if !DEBUG
            //シリアルキーチェック
            if (Properties.Settings.Default.AccessKey != KeyGenerator.Hash.GenerateHash(UserName))
            {

                string str = Microsoft.VisualBasic.Interaction.InputBox("", "シリアルキーを入力してください", "", -1, -1);

                if (str != KeyGenerator.Hash.GenerateHash(UserName))
                {
                    MessageBox.Show("シリアルキーが正しくありません", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    buttonStop.PerformClick();
                    return;
                }

                Properties.Settings.Default.AccessKey = str;
                Properties.Settings.Default.Save();
            }
#endif

            if (!Uri.IsWellFormedUriString(textBoxURL.Text, UriKind.Absolute))
            {
                MessageBox.Show("URLが正しい形式ではありません", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ShootingRange?.KillThread();

#if !DEBUG
            if (Webdriver.IsChrome())
            {
                Webdriver.Close();
                Webdriver.CreateHtmlAgilityPackDriver();
            }
#endif

            if (Webdriver.IsOoen())
            {
                SetSetting();
                ShootingRange = EventManager.CreateShootingRange(Webdriver.Instance);

                ShootingRange.Log += OnLog;
                ShootingRange.AutoStopped += OnAutoStop;

                Log?.Invoke(this, "マクロ初期化完了");
            }
            else
            {
                MessageBox.Show("ブラウザが起動していません", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Log?.Invoke(this, "ブラウザが起動していません");

                return;
            }
            ShootingRange.CreateThread();

            buttonStart.Enabled = false;
            buttonStop.Enabled = true;
            IsStart = true;
            BotActived?.Invoke(this, true);

            if ((int)CycleRecieveTime.TotalMilliseconds > 0)
            {
                timerRecievePresent.Interval = (int)CycleRecieveTime.TotalMilliseconds;
                timerRecievePresent.Start();
            }
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            buttonStart.Enabled = true;
            buttonStop.Enabled = false;

            IsStart = false;

            timerWatchWebdriver.Stop();
            timerRecievePresent.Stop();

            ShootingRange?.KillThread();
            ShootingRange = null;

            BotActived?.Invoke(this, false);
        }

        private void timerWatchWebdriver_Tick(object sender, EventArgs e)
        {
            if (IsStart)
            {
                if (!Webdriver.IsOoen() || !ShootingRange.IsRun)
                {
                    IsStart = false;
                    ShootingRange?.KillThread();
                    ShootingRange = null;

                    buttonStop.PerformClick();

                    MessageBox.Show("マクロが停止しました", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    Log?.Invoke(this, "マクロが停止しました");
                }
            }
        }

        public void SaveSetting()
        {
            SetSetting();
            Properties.Settings.Default.Save();
        }

        private void SetSetting()
        {
            Setting.ShootingRange.Url = textBoxURL.Text;
            Setting.ShootingRange.ThresholdFocusShot = Utils.ToUInt(textBoxThresholdFocusShot.Text);
            Setting.ShootingRange.UseFocusShotDuringFever = checkBoxUseFocusShotDuringFever.Checked;
            Setting.ShootingRange.UseFeverTip = checkBoxUseFeverTip.Checked;
            Setting.ShootingRange.AutoStop = checkBoxAutoStop.Checked;
        }

        private void textBoxThresholdFocusShot_KeyPress(object sender, KeyPressEventArgs e) => e.Handled = Utils.ValidUlong(sender as TextBox, e);

        private void textBoxThresholdFocusShot_Validated(object sender, EventArgs e) => (sender as TextBox).Text = (sender as TextBox).Text == "" ? "0" : (sender as TextBox).Text;

        public void EnableRunButton(bool enabled) => buttonStart.Enabled = enabled;

        public void RecievePresent() => ShootingRange?.SendRecievePresentRequest();

        private void OnLog(object sender, string text)
        {
            Invoke((MethodInvoker)delegate
            {
                Log?.Invoke(sender, text);
            });
        }

        private void OnAutoStop(object sender)
        {
            Invoke((MethodInvoker)delegate
            {
                buttonStop.PerformClick();
                Invoke((MethodInvoker)delegate
                {
                    MessageBox.Show("射撃を終了しました", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                });
            });
        }

        private void ValueChanged(object sender, EventArgs e) => SettingChanged?.Invoke(this, e);

        private void timerRecievePresent_Tick(object sender, EventArgs e) => ShootingRange?.SendRecievePresentRequest();
    }
}
