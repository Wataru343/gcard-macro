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

            textBoxURL.Text = Properties.Settings.Default.GTacticsURL;
            textBoxBaseDamage.Text = Properties.Settings.Default.GTacticsBaseDamage.ToString();
            textBoxEnemyCount.Text = Properties.Settings.Default.GTacticsEnemyCount.ToString();
            comboBoxAttackMode.SelectedIndex = Properties.Settings.Default.GTacticsAttackMode;
            comboBoxRecieve.SelectedIndex = Properties.Settings.Default.GTacticsReceiveCount;
            checkBoxRecieveReword.Checked = Properties.Settings.Default.GTacticsReceiveReword;
            checkBoxRecievePresent.Checked = Properties.Settings.Default.GTacticsReceivePresent;
            checkBoxOnlySearch.Checked = Properties.Settings.Default.GTacticsOnlySearch;
            comboBoxShieldA1.SelectedIndex = Properties.Settings.Default.GTacticsShieldA1;
            comboBoxShieldA2.SelectedIndex = Properties.Settings.Default.GTacticsShieldA2;
            comboBoxShieldA3.SelectedIndex = Properties.Settings.Default.GTacticsShieldA3;
            comboBoxShieldB1.SelectedIndex = Properties.Settings.Default.GTacticsShieldB1;
            comboBoxShieldB2.SelectedIndex = Properties.Settings.Default.GTacticsShieldB2;
            comboBoxShieldB3.SelectedIndex = Properties.Settings.Default.GTacticsShieldB3;
            comboBoxShieldC1.SelectedIndex = Properties.Settings.Default.GTacticsShieldC1;
            comboBoxShieldC2.SelectedIndex = Properties.Settings.Default.GTacticsShieldC2;
            comboBoxShieldC3.SelectedIndex = Properties.Settings.Default.GTacticsShieldC3;
            comboBoxStrategicArea.SelectedIndex = Properties.Settings.Default.GTacticsStrategicArea;
            checkBoxUseForce.Checked = Properties.Settings.Default.GTacticsUseForce;
            checkBoxForceCharge.Checked = Properties.Settings.Default.GTacticsForceCharge;
            comboBoxForcePattern.SelectedIndex = Properties.Settings.Default.GTacticsForcePattern;
            comboBoxPriority.SelectedIndex = Properties.Settings.Default.GTacticsPriority;
            textBoxPointDiff.Text = Properties.Settings.Default.GTacticsPointDiff.ToString();
            checkBoxStandby.Checked = Properties.Settings.Default.GTacticsStandby;
            textBoxWaitForce.Text = Properties.Settings.Default.GTacticsWaitForce.ToString();
            checkBoxEnableRightArea.Checked = Properties.Settings.Default.GTacticsEnableRightArea;
            checkBoxEnableCenterArea.Checked = Properties.Settings.Default.GTacticsEnableCenterArea;
            checkBoxEnableLeftArea.Checked = Properties.Settings.Default.GTacticsEnableLeftArea;
            textBoxSearchForce.Text = Properties.Settings.Default.GTacticsSearchForce.ToString();
            checkBoxSearchForceLeft.Checked = Properties.Settings.Default.GTacticsSearchForceLeft;
            checkBoxSearchForceCenter.Checked = Properties.Settings.Default.GTacticsSearchForceCenter;
            checkBoxSearchForceLRight.Checked = Properties.Settings.Default.GTacticsSearchForceRight;

            try
            {
                dateTimePickerTimeStart.Value = Properties.Settings.Default.GTacticsTimeStart;
                dateTimePickerTimeEnd.Value = Properties.Settings.Default.GTacticsTimeEnd;
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
                GTactics = new GTactics(Webdriver.Instance, textBoxURL.Text)
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
                    Shield = new List<GTactics.Area>() {
                        new GTactics.Area() { Level = comboBoxStrategicArea.SelectedIndex, Enable = checkBoxEnableStrategyArea.Checked },
                        new GTactics.Area() { Level = comboBoxShieldC3.SelectedIndex, Enable = checkBoxEnableRightArea.Checked },
                        new GTactics.Area() { Level = comboBoxShieldC2.SelectedIndex, Enable = checkBoxEnableCenterArea.Checked },
                        new GTactics.Area() { Level = comboBoxShieldC1.SelectedIndex, Enable = checkBoxEnableLeftArea.Checked },
                        new GTactics.Area() { Level = comboBoxShieldB3.SelectedIndex, Enable = checkBoxEnableRightArea.Checked },
                        new GTactics.Area() { Level = comboBoxShieldB2.SelectedIndex, Enable = checkBoxEnableCenterArea.Checked },
                        new GTactics.Area() { Level = comboBoxShieldB1.SelectedIndex, Enable = checkBoxEnableLeftArea.Checked },
                        new GTactics.Area() { Level = comboBoxShieldA3.SelectedIndex, Enable = checkBoxEnableRightArea.Checked },
                        new GTactics.Area() { Level = comboBoxShieldA2.SelectedIndex, Enable = checkBoxEnableCenterArea.Checked },
                        new GTactics.Area() { Level = comboBoxShieldA1.SelectedIndex, Enable = checkBoxEnableLeftArea.Checked },
                    },
                    Priority = (GTactics.AreaPriority)comboBoxPriority.SelectedIndex,
                    UseForce = checkBoxUseForce.Checked,
                    ForceCharge = checkBoxForceCharge.Checked,
                    StrategyAreaForcePattern = (GTactics.ForcePattern)comboBoxForcePattern.SelectedIndex,
                    PointDiff = Utils.ToLong(textBoxPointDiff.Text),
                    Standby = checkBoxStandby.Checked,
                    WaitForce = Utils.ToDouble(textBoxWaitForce.Text),
                    StartTime = dateTimePickerTimeStart.Value,
                    EndTime = dateTimePickerTimeEnd.Value,
                    SampleCount = OptimizedWaitEnemyCount,
                    SearchForceEnemyCount = Utils.ToUlong(textBoxSearchForce.Text),
                    SearchForcePlace = new List<bool>()
                    {
                        checkBoxSearchForceLeft.Checked,
                        checkBoxSearchForceCenter.Checked,
                        checkBoxSearchForceLRight.Checked,
                    }
                };

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
            Properties.Settings.Default.GTacticsURL = textBoxURL.Text;
            Properties.Settings.Default.GTacticsBaseDamage = Utils.ToUlong(textBoxBaseDamage.Text);
            Properties.Settings.Default.GTacticsEnemyCount = Utils.ToUlong(textBoxEnemyCount.Text);
            Properties.Settings.Default.GTacticsAttackMode = comboBoxAttackMode.SelectedIndex;
            Properties.Settings.Default.GTacticsReceiveCount = comboBoxRecieve.SelectedIndex;
            Properties.Settings.Default.GTacticsReceiveReword = checkBoxRecieveReword.Checked;
            Properties.Settings.Default.GTacticsReceivePresent = checkBoxRecievePresent.Checked;
            Properties.Settings.Default.GTacticsOnlySearch = checkBoxOnlySearch.Checked;
            Properties.Settings.Default.GTacticsShieldA1 = comboBoxShieldA1.SelectedIndex;
            Properties.Settings.Default.GTacticsShieldA2 = comboBoxShieldA2.SelectedIndex;
            Properties.Settings.Default.GTacticsShieldA3 = comboBoxShieldA3.SelectedIndex;
            Properties.Settings.Default.GTacticsShieldB1 = comboBoxShieldB1.SelectedIndex;
            Properties.Settings.Default.GTacticsShieldB2 = comboBoxShieldB2.SelectedIndex;
            Properties.Settings.Default.GTacticsShieldB3 = comboBoxShieldB3.SelectedIndex;
            Properties.Settings.Default.GTacticsShieldC1 = comboBoxShieldC1.SelectedIndex;
            Properties.Settings.Default.GTacticsShieldC2 = comboBoxShieldC2.SelectedIndex;
            Properties.Settings.Default.GTacticsShieldC3 = comboBoxShieldC3.SelectedIndex;
            Properties.Settings.Default.GTacticsStrategicArea = comboBoxStrategicArea.SelectedIndex;
            Properties.Settings.Default.GTacticsPriority = comboBoxPriority.SelectedIndex;
            Properties.Settings.Default.GTacticsUseForce = checkBoxUseForce.Checked;
            Properties.Settings.Default.GTacticsForceCharge = checkBoxForceCharge.Checked;
            Properties.Settings.Default.GTacticsForcePattern = comboBoxForcePattern.SelectedIndex;
            Properties.Settings.Default.GTacticsPointDiff = Utils.ToUlong(textBoxPointDiff.Text);
            Properties.Settings.Default.GTacticsStandby = checkBoxStandby.Checked;
            Properties.Settings.Default.GTacticsWaitForce = Utils.ToDouble(textBoxWaitForce.Text);
            Properties.Settings.Default.GTacticsTimeStart = dateTimePickerTimeStart.Value;
            Properties.Settings.Default.GTacticsTimeEnd = dateTimePickerTimeEnd.Value;
            Properties.Settings.Default.GTacticsEnableRightArea = checkBoxEnableRightArea.Checked;
            Properties.Settings.Default.GTacticsEnableCenterArea = checkBoxEnableCenterArea.Checked;
            Properties.Settings.Default.GTacticsEnableLeftArea = checkBoxEnableLeftArea.Checked;
            Properties.Settings.Default.GTacticsSearchForce = Utils.ToUlong(textBoxSearchForce.Text);
            Properties.Settings.Default.GTacticsSearchForceLeft = checkBoxSearchForceLeft.Checked;
            Properties.Settings.Default.GTacticsSearchForceCenter = checkBoxSearchForceCenter.Checked;
            Properties.Settings.Default.GTacticsSearchForceRight = checkBoxSearchForceLRight.Checked;
            Properties.Settings.Default.Save();
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
