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
    public partial class TabControlGTactics : UserControl
    {
        private GTactics GTactics { get; set; }
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

        public TabControlGTactics()
        {
            InitializeComponent();
            timerWatchWebdriver.Start();
            GTactics = null;
            IsStart = false;
            buttonStop.Enabled = false;

            Setting.GTactics.Load();

            textBoxURL.Text = Setting.GTactics.Url;
            textBoxBaseDamage.Text = Setting.GTactics.BaseDamage.ToString();
            textBoxEnemyCount.Text = Setting.GTactics.EnemyCount.ToString();
            comboBoxAttackMode.SelectedIndex = Setting.GTactics.AttackMode;
            comboBoxRecieve.SelectedIndex = Setting.GTactics.ReceiveCount;
            checkBoxRecieveReword.Checked = Setting.GTactics.ReceiveReword;
            checkBoxRecievePresent.Checked = Setting.GTactics.ReceivePresent;
            checkBoxOnlySearch.Checked = Setting.GTactics.OnlySearch;
            checkBoxNoSearch.Checked = Setting.GTactics.NoSearch;
            comboBoxShieldA1.SelectedIndex = Setting.GTactics.ShieldA1;
            comboBoxShieldA2.SelectedIndex = Setting.GTactics.ShieldA2;
            comboBoxShieldA3.SelectedIndex = Setting.GTactics.ShieldA3;
            comboBoxShieldB1.SelectedIndex = Setting.GTactics.ShieldB1;
            comboBoxShieldB2.SelectedIndex = Setting.GTactics.ShieldB2;
            comboBoxShieldB3.SelectedIndex = Setting.GTactics.ShieldB3;
            comboBoxShieldC1.SelectedIndex = Setting.GTactics.ShieldC1;
            comboBoxShieldC2.SelectedIndex = Setting.GTactics.ShieldC2;
            comboBoxShieldC3.SelectedIndex = Setting.GTactics.ShieldC3;
            comboBoxStrategicArea.SelectedIndex = Setting.GTactics.StrategicArea;
            checkBoxUseForce.Checked = Setting.GTactics.UseForce;
            checkBoxForceCharge.Checked = Setting.GTactics.ForceCharge;
            comboBoxForcePattern.SelectedIndex = Setting.GTactics.ForcePattern;
            comboBoxPriority.SelectedIndex = Setting.GTactics.Priority;
            textBoxPointDiff.Text = Setting.GTactics.PointDiff.ToString();
            checkBoxStandby.Checked = Setting.GTactics.Standby;
            textBoxWaitForce.Text = Setting.GTactics.WaitForce.ToString();
            checkBoxEnableRightArea.Checked = Setting.GTactics.EnableRightArea;
            checkBoxEnableCenterArea.Checked = Setting.GTactics.EnableCenterArea;
            checkBoxEnableLeftArea.Checked = Setting.GTactics.EnableLeftArea;
            checkBoxEnableStrategyArea.Checked = Setting.GTactics.EnableStrategicArea;
            textBoxSearchForce.Text = Setting.GTactics.SearchForce.ToString();
            checkBoxSearchForceLeft.Checked = Setting.GTactics.SearchForceLeft;
            checkBoxSearchForceCenter.Checked = Setting.GTactics.SearchForceCenter;
            checkBoxSearchForceLRight.Checked = Setting.GTactics.SearchForceRight;


            try
            {
                dateTimePickerTimeStart.Value = Setting.GTactics.TimeStart;
                dateTimePickerTimeEnd.Value = Setting.GTactics.TimeEnd;
            }
            catch
            {
                dateTimePickerTimeStart.Value = dateTimePickerTimeStart.MinDate;
                dateTimePickerTimeEnd.Value = dateTimePickerTimeEnd.MinDate;
            }

            checkBoxStandby.CheckedChanged += ValueChanged;
            checkBoxEnableLeftArea.CheckedChanged += ValueChanged;
            checkBoxEnableCenterArea.CheckedChanged += ValueChanged;
            checkBoxEnableRightArea.CheckedChanged += ValueChanged;
            checkBoxEnableStrategyArea.CheckedChanged += ValueChanged;

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

            GTactics?.KillThread();

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
                GTactics = EventManager.CreateGTactics(Webdriver.Instance);

                GTactics.StateChanged += StateChanged;
                GTactics.MinicapChanged += MiniCapChanged;
                GTactics.AreaChanged += AreaChanged;
                GTactics.Log += OnLog;
                GTactics.SpeedCounter += OnSpeedCount;

                Log?.Invoke(this, "マクロ初期化完了");
            }
            else
            {
                MessageBox.Show("ブラウザが起動していません", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Log?.Invoke(this, "ブラウザが起動していません");

                return;
            }
            GTactics.CreateThread();

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

            GTactics?.KillThread();
            GTactics = null;

            BotActived?.Invoke(this, false);
        }

        private void timerWatchWebdriver_Tick(object sender, EventArgs e)
        {
            if (IsStart)
            {
                if (!Webdriver.IsOoen() || !GTactics.IsRun)
                {
                    IsStart = false;

                    GTactics?.KillThread();
                    GTactics = null;

                    buttonStop.PerformClick();

                    MessageBox.Show("マクロが停止しました", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    Log?.Invoke(this, "マクロが停止しました");
                }
            }
        }

        public void SaveSetting()
        {
            SetSetting();
            Setting.GTactics.Save();
        }

        private void SetSetting()
        {
            Setting.GTactics.Url = textBoxURL.Text;
            Setting.GTactics.BaseDamage = Utils.ToUlong(textBoxBaseDamage.Text);
            Setting.GTactics.EnemyCount = Utils.ToUlong(textBoxEnemyCount.Text);
            Setting.GTactics.AttackMode = comboBoxAttackMode.SelectedIndex;
            Setting.GTactics.ReceiveCount = comboBoxRecieve.SelectedIndex;
            Setting.GTactics.ReceiveReword = checkBoxRecieveReword.Checked;
            Setting.GTactics.ReceivePresent = checkBoxRecievePresent.Checked;
            Setting.GTactics.OnlySearch = checkBoxOnlySearch.Checked;
            Setting.GTactics.NoSearch = checkBoxNoSearch.Checked;
            Setting.GTactics.ShieldA1 = comboBoxShieldA1.SelectedIndex;
            Setting.GTactics.ShieldA2 = comboBoxShieldA2.SelectedIndex;
            Setting.GTactics.ShieldA3 = comboBoxShieldA3.SelectedIndex;
            Setting.GTactics.ShieldB1 = comboBoxShieldB1.SelectedIndex;
            Setting.GTactics.ShieldB2 = comboBoxShieldB2.SelectedIndex;
            Setting.GTactics.ShieldB3 = comboBoxShieldB3.SelectedIndex;
            Setting.GTactics.ShieldC1 = comboBoxShieldC1.SelectedIndex;
            Setting.GTactics.ShieldC2 = comboBoxShieldC2.SelectedIndex;
            Setting.GTactics.ShieldC3 = comboBoxShieldC3.SelectedIndex;
            Setting.GTactics.StrategicArea = comboBoxStrategicArea.SelectedIndex;
            Setting.GTactics.Priority = comboBoxPriority.SelectedIndex;
            Setting.GTactics.UseForce = checkBoxUseForce.Checked;
            Setting.GTactics.ForceCharge = checkBoxForceCharge.Checked;
            Setting.GTactics.ForcePattern = comboBoxForcePattern.SelectedIndex;
            Setting.GTactics.PointDiff = Utils.ToUlong(textBoxPointDiff.Text);
            Setting.GTactics.Standby = checkBoxStandby.Checked;
            Setting.GTactics.WaitForce = Utils.ToDouble(textBoxWaitForce.Text);
            Setting.GTactics.TimeStart = dateTimePickerTimeStart.Value;
            Setting.GTactics.TimeEnd = dateTimePickerTimeEnd.Value;
            Setting.GTactics.EnableRightArea = checkBoxEnableRightArea.Checked;
            Setting.GTactics.EnableCenterArea = checkBoxEnableCenterArea.Checked;
            Setting.GTactics.EnableLeftArea = checkBoxEnableLeftArea.Checked;
            Setting.GTactics.EnableStrategicArea = checkBoxEnableStrategyArea.Checked;
            Setting.GTactics.SearchForce = Utils.ToUlong(textBoxSearchForce.Text);
            Setting.GTactics.SearchForceLeft = checkBoxSearchForceLeft.Checked;
            Setting.GTactics.SearchForceCenter = checkBoxSearchForceCenter.Checked;
            Setting.GTactics.SearchForceRight = checkBoxSearchForceLRight.Checked;
        }


        private void textBoxBaseDamage_KeyPress(object sender, KeyPressEventArgs e) => e.Handled = Utils.ValidUlong(sender as TextBox, e);

        private void textBoxPointDiff_KeyPress(object sender, KeyPressEventArgs e) => e.Handled = Utils.ValidUlong(sender as TextBox, e);

        private void textBoxEnemyCount_KeyPress(object sender, KeyPressEventArgs e) => e.Handled = Utils.ValidUlong(sender as TextBox, e);

        private void textBoxSearchForce_KeyPress(object sender, KeyPressEventArgs e) => e.Handled = Utils.ValidUlong(sender as TextBox, e);

        private void textBoxEnemyCount_Validated(object sender, EventArgs e) => (sender as TextBox).Text = (sender as TextBox).Text == "" ? "0" : (sender as TextBox).Text;

        private void textBoxBaseDamage_Validated(object sender, EventArgs e) => (sender as TextBox).Text = (sender as TextBox).Text == "" ? "0" : (sender as TextBox).Text;

        private void textBoxPointDiff_Validated(object sender, EventArgs e) => (sender as TextBox).Text = (sender as TextBox).Text == "" ? "0" : (sender as TextBox).Text;

        private void textBoxSearchForce_Validated(object sender, EventArgs e) => (sender as TextBox).Text = (sender as TextBox).Text == "" ? "0" : (sender as TextBox).Text;


        private void MiniCapChanged(object sender, int count)
        {
            Invoke((MethodInvoker)delegate
            {
                labelMiniCap.Text = "ミニカプ：" + count.ToString() + "個";
            });
        }

        private void StateChanged(object sender, Event.State state)
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
                    case Event.State.Interval:
                        labelStateInterval.BackColor = Color.Yellow;
                        CurrentState = labelStateInterval;
                        break;
                    case Event.State.Battle:
                        labelStateBattle.BackColor = Color.Yellow;
                        CurrentState = labelStateBattle;
                        break;
                    case Event.State.EnemyList:
                        labelStateEnemyList.BackColor = Color.Yellow;
                        CurrentState = labelStateEnemyList;
                        break;
                    case Event.State.GTacticsStrategicArea:
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
                        labelStateResult.BackColor = Color.Yellow;
                        CurrentState = labelStatePresentList;
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

        private void AreaChanged(object sender, string area)
        {
            Invoke((MethodInvoker)delegate
            {
                labelArea.Text = "現在エリア：" + area;
            });
        }

        public void EnableRunButton(bool enabled) => buttonStart.Enabled = enabled;

        public void RecievePresent() => GTactics?.SendRecievePresentRequest();

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
                labelSpm.Text = "1分間の探索回数：" + count.ToString() + "回";
            });
        }

        private void ValueChanged(object sender, EventArgs e) => SettingChanged?.Invoke(this, e);

        private void PaintFrameTopLeftRight(object sender, PaintEventArgs e) => PaintFrame(e, new Point[] { new Point(0, (sender as Control).Height - 1), new Point(0, 0), new Point((sender as Control).Width - 1, 0), new Point((sender as Control).Width - 1, (sender as Control).Height - 1) });

        private void PaintFrameTopLeftRightBottom(object sender, PaintEventArgs e) => PaintFrame(e, new Point[] { new Point(0, (sender as Control).Height - 1), new Point(0, 0), new Point((sender as Control).Width - 1, 0), new Point((sender as Control).Width - 1, (sender as Control).Height - 1), new Point(0, (sender as Control).Height - 1) });

        private void PaintFrameTopRight(object sender, PaintEventArgs e) => PaintFrame(e, new Point[] { new Point(0, 0), new Point((sender as Control).Size.Width - 1, 0), new Point((sender as Control).Size.Width - 1, (sender as Control).Size.Height - 1) });

        private void PaintFrameTopRightBottom(object sender, PaintEventArgs e) => PaintFrame(e, new Point[] { new Point(0, 0), new Point((sender as Control).Size.Width - 1, 0), new Point((sender as Control).Size.Width - 1, (sender as Control).Size.Height - 1), new Point(0, (sender as Control).Size.Height - 1) });

        private void PaintFrame(PaintEventArgs e, Point[] pt) => e.Graphics.DrawLines(new Pen(Color.Black, 1), pt);

        private void timerRecievePresent_Tick(object sender, EventArgs e) => GTactics?.SendRecievePresentRequest();

        private void checkBoxStandby_CheckedChanged(object sender, EventArgs e) => textBoxPointDiff.Enabled = labelPointDiff.Enabled = (sender as CheckBox).Checked;

        private void checkBoxEnableLeftArea_CheckedChanged(object sender, EventArgs e) => comboBoxShieldA1.Enabled = comboBoxShieldB1.Enabled = comboBoxShieldC1.Enabled = (sender as CheckBox).Checked;

        private void checkBoxEnableCenterArea_CheckedChanged(object sender, EventArgs e) => comboBoxShieldA2.Enabled = comboBoxShieldB2.Enabled = comboBoxShieldC2.Enabled = (sender as CheckBox).Checked;

        private void checkBoxEnableRightArea_CheckedChanged(object sender, EventArgs e) => comboBoxShieldA3.Enabled = comboBoxShieldB3.Enabled = comboBoxShieldC3.Enabled = (sender as CheckBox).Checked;

        private void checkBoxEnableStrategyArea_CheckedChanged(object sender, EventArgs e) => comboBoxStrategicArea.Enabled = (sender as CheckBox).Checked;
    }
}
