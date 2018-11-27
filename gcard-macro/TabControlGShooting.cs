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
    public partial class TabControlGShooting : UserControl
    {
        private GShooting GShooting { get; set; }
        public bool IsStart;
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

        public TabControlGShooting()
        {
            InitializeComponent();
            timerWatchWebdriver.Start();
            GShooting = null;
            IsStart = false;
            buttonStop.Enabled = false;

            textBoxURL.Text = Properties.Settings.Default.GShootingURL;
            textBoxBaseDamage.Text = Properties.Settings.Default.GShootingBaseDamage.ToString();
            textBoxEnemyCount.Text = Properties.Settings.Default.GShootingEnemyCount.ToString();
            comboBoxAttackMode.SelectedIndex = Properties.Settings.Default.GShootingAttackMode;
            comboBoxRecieve.SelectedIndex = Properties.Settings.Default.GShootingReceiveCount;
            checkBoxRecieveReword.Checked = Properties.Settings.Default.GShootingReceiveReword;
            checkBoxRecievePresent.Checked = Properties.Settings.Default.GShootingReceivePresent;
            checkBoxOnlySearch.Checked = Properties.Settings.Default.GShootingOnlySearch;
            checkBoxNoSearch.Checked = Properties.Settings.Default.GShootingNoSearch;
            checkBoxRequest.Checked = Properties.Settings.Default.GShootingRequest;

            try
            {
                dateTimePickerTimeStart.Value = Properties.Settings.Default.GShootingTimeStart;
                dateTimePickerTimeEnd.Value = Properties.Settings.Default.GShootingTimeEnd;
            }
            catch
            {
                dateTimePickerTimeStart.Value = dateTimePickerTimeStart.MinDate;
                dateTimePickerTimeEnd.Value = dateTimePickerTimeEnd.MinDate;
            }

            CurrentState = labelStateHome;

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

            GShooting?.KillThread();

#if !DEBUG
            //if (Webdriver.IsChrome())
            //{
            //    Webdriver.Close();
            //    Webdriver.CreatePhantomJS();
            //}
#endif

            if (Webdriver.IsOoen())
            {
                GShooting = new GShooting(Webdriver.Instance, textBoxURL.Text)
                {
                    WaitSearch = WaitSearch,
                    WaitBattle = WaitBattle,
                    WaitAttack = WaitAttack,
                    WaitReceive = WaitReceive,
                    WaitAccessBlock = WaitAccessBlock,
                    WaitMisc = WaitMisc,
                    BaseDamage = Utils.ToUlong(textBoxBaseDamage.Text),
                    EnemyCount = Utils.ToUlong(textBoxEnemyCount.Text),
                    Mode = (Event.AttackMode)comboBoxAttackMode.SelectedIndex,
                    ReceiveCount = comboBoxRecieve.SelectedIndex + 1,
                    ReceiveReword = checkBoxRecieveReword.Checked,
                    ReceivePresent = checkBoxRecievePresent.Checked,
                    OnlySearch = checkBoxOnlySearch.Checked,
                    NoSearch = checkBoxNoSearch.Checked,
                    Request = checkBoxRequest.Checked,
                    StartTime = dateTimePickerTimeStart.Value,
                    EndTime = dateTimePickerTimeEnd.Value,
                    SampleCount = OptimizedWaitEnemyCount
                };

                GShooting.StateChanged += StateChanged;
                GShooting.MinicapChanged += MiniCapChanged;
                GShooting.Log += OnLog;
                GShooting.SpeedCounter += OnSpeedCount;

                Log?.Invoke(this, "マクロ初期化完了");
            }
            else
            {
                MessageBox.Show("ブラウザが起動していません", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Log?.Invoke(this, "ブラウザが起動していません");

                return;
            }
            GShooting.CreateThread();

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

            GShooting?.KillThread();
            GShooting = null;

            BotActived?.Invoke(this, false);
        }

        private void timerWatchWebdriver_Tick(object sender, EventArgs e)
        {
            if (IsStart)
            {
                if (!Webdriver.IsOoen() || !GShooting.IsRun)
                {
                    IsStart = false;
                    GShooting?.KillThread();
                    GShooting = null;

                    buttonStop.PerformClick();

                    MessageBox.Show("マクロが停止しました", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    Log?.Invoke(this, "マクロが停止しました");
                }
            }
        }

        public void SaveSetting()
        {
            Properties.Settings.Default.GShootingURL = textBoxURL.Text;
            Properties.Settings.Default.GShootingBaseDamage = Utils.ToUlong(textBoxBaseDamage.Text);
            Properties.Settings.Default.GShootingEnemyCount = Utils.ToUlong(textBoxEnemyCount.Text);
            Properties.Settings.Default.GShootingAttackMode = comboBoxAttackMode.SelectedIndex;
            Properties.Settings.Default.GShootingReceiveCount = comboBoxRecieve.SelectedIndex;
            Properties.Settings.Default.GShootingReceiveReword = checkBoxRecieveReword.Checked;
            Properties.Settings.Default.GShootingReceivePresent = checkBoxRecievePresent.Checked;
            Properties.Settings.Default.GShootingOnlySearch = checkBoxOnlySearch.Checked;
            Properties.Settings.Default.GShootingNoSearch = checkBoxNoSearch.Checked;
            Properties.Settings.Default.GShootingNoSearch = checkBoxNoSearch.Checked;
            Properties.Settings.Default.GShootingRequest = checkBoxRequest.Checked;
            Properties.Settings.Default.GShootingTimeStart = dateTimePickerTimeStart.Value;
            Properties.Settings.Default.GShootingTimeEnd = dateTimePickerTimeEnd.Value;
            Properties.Settings.Default.Save();
        }


        private void textBoxBaseDamage_KeyPress(object sender, KeyPressEventArgs e) => e.Handled = Utils.ValidUlong(sender as TextBox, e);

        private void textBoxPointDiff_KeyPress(object sender, KeyPressEventArgs e) => e.Handled = Utils.ValidUlong(sender as TextBox, e);

        private void textBoxEnemyCount_KeyPress(object sender, KeyPressEventArgs e) => e.Handled = Utils.ValidUlong(sender as TextBox, e);

        private void textBoxEnemyCount_Validated(object sender, EventArgs e) => (sender as TextBox).Text = (sender as TextBox).Text == "" ? "0" : (sender as TextBox).Text;

        private void textBoxBaseDamage_Validated(object sender, EventArgs e) => (sender as TextBox).Text = (sender as TextBox).Text == "" ? "0" : (sender as TextBox).Text;

        private void MiniCapChanged(object sender, int count)
        {
            Invoke((MethodInvoker)delegate
            {
                labelMiniCap.Text = "ミニカプ：" + count.ToString() + "個";
            });
        }

        private void StateChanged(object sender, Event.State state)
        {
            try
            {
                Invoke((MethodInvoker)delegate
                {
                    CurrentState.BackColor = Color.White;

                    switch (state)
                    {
                        case Event.State.None:
                            break;
                        case Event.State.Home:
                            labelStateHome.BackColor = Color.Yellow;
                            CurrentState = labelStateHome;
                            break;
                        case Event.State.Battle:
                            labelStateBattle.BackColor = Color.Yellow;
                            CurrentState = labelStateBattle;
                            break;
                        case Event.State.EnemyList:
                            labelStateEnemyList.BackColor = Color.Yellow;
                            CurrentState = labelStateEnemyList;
                            break;
                        case Event.State.SearchFlash:
                            labelStateSearch.BackColor = Color.Yellow;
                            CurrentState = labelStateSearch;
                            break;
                        case Event.State.BattleFlash:
                            labelStateBattleFlash.BackColor = Color.Yellow;
                            CurrentState = labelStateBattleFlash;
                            break;
                        case Event.State.LevelUp:
                            labelStateLevelUp.BackColor = Color.Yellow;
                            CurrentState = labelStateLevelUp;
                            break;
                        case Event.State.Error:
                            labelStateError.BackColor = Color.Yellow;
                            CurrentState = labelStateError;
                            break;
                        case Event.State.Result:
                            labelStateResult.BackColor = Color.Yellow;
                            CurrentState = labelStateResult;
                            break;
                        case Event.State.Receive:
                            labelStateReceive.BackColor = Color.Yellow;
                            CurrentState = labelStateReceive;
                            break;
                        case Event.State.PresentList:
                            labelStatePresentList.BackColor = Color.Yellow;
                            CurrentState = labelStatePresentList;
                            break;
                        case Event.State.RequestComplete:
                            labelStateRequest.BackColor = Color.Yellow;
                            CurrentState = labelStateRequest;
                            break;
                        case Event.State.FightAlreadyFinished:
                            labelStateFightAlreadyFinished.BackColor = Color.Yellow;
                            CurrentState = labelStateFightAlreadyFinished;
                            break;
                        case Event.State.AccessBlock:
                            labelStateAccessBlock.BackColor = Color.Yellow;
                            CurrentState = labelStateAccessBlock;
                            break;
                        case Event.State.GetCard:
                            labelStateGetCard.BackColor = Color.Yellow;
                            CurrentState = labelStateGetCard;
                            break;
                        case Event.State.EventFinished:
                            labelStateEventFinished.BackColor = Color.Yellow;
                            CurrentState = labelStateEventFinished;
                            break;
                        case Event.State.Unknown:
                            labelStateUnknown.BackColor = Color.Yellow;
                            CurrentState = labelStateUnknown;
                            break;
                        default:
                            labelStateUnknown.BackColor = Color.Yellow;
                            CurrentState = labelStateUnknown;
                            break;
                    }
                });
            }
            catch { }
        }

        public void EnableRunButton(bool enabled) => buttonStart.Enabled = enabled;

        public void RecievePresent() => GShooting?.SendRecievePresentRequest();

        private void OnLog(object sender, string text)
        {
            Invoke((MethodInvoker)delegate
            {
                Log?.Invoke(sender, text);
            });
        }

        private void OnSpeedCount(object sender, int count)
        {
            Invoke((MethodInvoker)delegate
            {
                labelSpm.Text = "1分間の敵発見数：" + count.ToString() + "体";
            });
        }

        private void ValueChanged(object sender, EventArgs e) => SettingChanged?.Invoke(this, e);

        private void PaintFrameTopLeftRight(object sender, PaintEventArgs e) => PaintFrame(e, new Point[] { new Point(0, (sender as Control).Height - 1), new Point(0, 0), new Point((sender as Control).Width - 1, 0), new Point((sender as Control).Width - 1, (sender as Control).Height - 1) });

        private void PaintFrameTopLeftRightBottom(object sender, PaintEventArgs e) => PaintFrame(e, new Point[] { new Point(0, (sender as Control).Height - 1), new Point(0, 0), new Point((sender as Control).Width - 1, 0), new Point((sender as Control).Width - 1, (sender as Control).Height - 1), new Point(0, (sender as Control).Height - 1) });

        private void PaintFrameTopRight(object sender, PaintEventArgs e) => PaintFrame(e, new Point[] { new Point(0, 0), new Point((sender as Control).Size.Width - 1, 0), new Point((sender as Control).Size.Width - 1, (sender as Control).Size.Height - 1) });

        private void PaintFrameTopRightBottom(object sender, PaintEventArgs e) => PaintFrame(e, new Point[] { new Point(0, 0), new Point((sender as Control).Size.Width - 1, 0), new Point((sender as Control).Size.Width - 1, (sender as Control).Size.Height - 1), new Point(0, (sender as Control).Size.Height - 1) });

        private void PaintFrame(PaintEventArgs e, Point[] pt) => e.Graphics.DrawLines(new Pen(Color.Black, 1), pt);

        private void timerRecievePresent_Tick(object sender, EventArgs e)
        {
            GShooting?.SendRecievePresentRequest();
        }
    }
}
