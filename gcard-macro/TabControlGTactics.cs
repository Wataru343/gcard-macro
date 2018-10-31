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
        private bool IsStart { get; set; }
        private Label CurrentState { get; set; }

        public double WaitSearch { get; set; }
        public double WaitBattle { get; set; }
        public double WaitAttack { get; set; }
        public double WaitReceive { get; set; }
        public double WaitContinueSearch { get; set; }
        public double WaitAccessBlock { get; set; }
        public double WaitMisc { get; set; }
        public string UserName { get; set; }

        public delegate void BotActiveHandler(object sender, bool actived);
        public event BotActiveHandler BotActived;
        public delegate void LogHandler(object sender, string text);
        public event LogHandler Log;

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

            CurrentState = labelStateHome;
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
                    WaitContinueSearch = WaitContinueSearch,
                    WaitAccessBlock = WaitAccessBlock,
                    WaitMisc = WaitMisc,
                    BaseDamage = Utils.ToUlong(textBoxBaseDamage.Text),
                    EnemyCount = Utils.ToUlong(textBoxEnemyCount.Text),
                    Mode = (Event.AttackMode)comboBoxAttackMode.SelectedIndex,
                    ReceiveCount = comboBoxRecieve.SelectedIndex + 1,
                    ReceiveReword = checkBoxRecieveReword.Checked,
                    ReceivePresent = checkBoxRecievePresent.Checked,
                    OnlySearch = checkBoxOnlySearch.Checked,
                    Shield = new Dictionary<string, int> {
                        { "A1", comboBoxShieldA1.SelectedIndex },
                        { "A2", comboBoxShieldA2.SelectedIndex },
                        { "A3", comboBoxShieldA3.SelectedIndex },
                        { "B1", comboBoxShieldB1.SelectedIndex },
                        { "B2", comboBoxShieldB2.SelectedIndex },
                        { "B3", comboBoxShieldB3.SelectedIndex },
                        { "C1", comboBoxShieldC1.SelectedIndex },
                        { "C2", comboBoxShieldC2.SelectedIndex },
                        { "C3", comboBoxShieldC3.SelectedIndex },
                        { "戦略拠点", comboBoxStrategicArea.SelectedIndex },
                    },
                    Priority = (GTactics.AreaPriority)comboBoxPriority.SelectedIndex,
                    UseForce = checkBoxUseForce.Checked,
                    ForceCharge = checkBoxForceCharge.Checked,
                    StrategyAreaForcePattern = (GTactics.ForcePattern)comboBoxForcePattern.SelectedIndex,
                    PointDiff = Utils.ToLong(textBoxPointDiff.Text),
                    Standby = checkBoxStandby.Checked,
                    WaitForce = Utils.ToDouble(textBoxWaitForce.Text),
                    StartTime = dateTimePickerTimeStart.Value,
                    EndTime = dateTimePickerTimeEnd.Value
                };

                GTactics.StateChanged += StateChanged;
                GTactics.MinicapChanged += MiniCapChanged;
                GTactics.AreaChanged += AreaChanged;
                GTactics.Log += OnLog;

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
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            buttonStart.Enabled = true;
            buttonStop.Enabled = false;

            IsStart = false;


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
            Properties.Settings.Default.Save();
        }


        private void textBoxBaseDamage_KeyPress(object sender, KeyPressEventArgs e) => e.Handled = Utils.ValidUlong(sender as TextBox, e);

        private void textBoxPointDiff_KeyPress(object sender, KeyPressEventArgs e) => e.Handled = Utils.ValidUlong(sender as TextBox, e);

        private void textBoxEnemyCount_KeyPress(object sender, KeyPressEventArgs e) => e.Handled = Utils.ValidUlong(sender as TextBox, e);

        private void textBoxEnemyCount_Validated(object sender, EventArgs e) => (sender as TextBox).Text = (sender as TextBox).Text == "" ? "0" : (sender as TextBox).Text;

        private void textBoxBaseDamage_Validated(object sender, EventArgs e) => (sender as TextBox).Text = (sender as TextBox).Text == "" ? "0" : (sender as TextBox).Text;

        private void textBoxPointDiff_Validated(object sender, EventArgs e) => (sender as TextBox).Text = (sender as TextBox).Text == "" ? "0" : (sender as TextBox).Text;



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

        private void OnLog(object sender, string text)
        {
            Invoke((MethodInvoker)delegate
            {
                Log?.Invoke(sender, text);
            });
        }
    }
}
