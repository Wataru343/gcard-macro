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
    public partial class TabControlRaid : UserControl
    {
        private Raid Raid { get; set; }
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

        public TabControlRaid()
        {
            InitializeComponent();
            timerWatchWebdriver.Start();
            Raid = null;
            IsStart = false;
            buttonStop.Enabled = false;

            textBoxURL.Text = Properties.Settings.Default.RaidURL;
            checkBoxJoinAssault.Checked = Properties.Settings.Default.RaidJoinAssault;
            checkBoxUseAssaultBE.Checked = Properties.Settings.Default.RaidUseAssaultBE;
            checkBoxRequest.Checked = Properties.Settings.Default.RaidRequest;
            textBoxBaseDamage.Text = Properties.Settings.Default.RaidBaseDamage.ToString();
            textBoxEnemyCount.Text = Properties.Settings.Default.RaidEnemyCount.ToString();
            comboBoxAttackMode.SelectedIndex = Properties.Settings.Default.RaidAttackMode;
            comboBoxRecieve.SelectedIndex = Properties.Settings.Default.RaidReceiveCount;
            checkBoxOnlySearch.Checked = Properties.Settings.Default.RaidOnlySearch;
            checkBoxRecieveReword.Checked = Properties.Settings.Default.RaidReceiveReword;
            checkBoxRecievePresent.Checked = Properties.Settings.Default.RaidReceivePresent;
            checkBoxAimMVP.Checked = Properties.Settings.Default.RaidAimMVP;
            checkBoxOnlyAttackAssultBoss.Checked = Properties.Settings.Default.RaidOnlyAttackAssultBoss;
            textBoxWaitRecieveAssult.Text = Properties.Settings.Default.RaidWaitRecieveAssult.ToString();
            textBoxWaitAtackBattleShip.Text = Properties.Settings.Default.RaidWaitAtackBattleShip.ToString();

            try
            {
                dateTimePickerTimeStart.Value = Properties.Settings.Default.RaidTimeStart;
                dateTimePickerTimeEnd.Value = Properties.Settings.Default.RaidTimeEnd;
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

            Raid?.KillThread();

#if !DEBUG
            if (Webdriver.IsChrome())
            {
                Webdriver.Close();
                Webdriver.CreateHtmlAgilityPackDriver();
            }
#endif

            if (Webdriver.IsOoen())
            {
                Raid = new Raid(Webdriver.Instance, textBoxURL.Text)
                {
                    WaitSearch = WaitSearch,
                    WaitBattle = WaitBattle,
                    WaitAttack = WaitAttack,
                    WaitReceive = WaitReceive,
                    WaitContinueSearch = WaitContinueSearch,
                    WaitAccessBlock = WaitAccessBlock,
                    WaitMisc = WaitMisc,
                    JoinAssault = checkBoxJoinAssault.Checked,
                    UseAssaultBE = checkBoxUseAssaultBE.Checked,
                    Request = checkBoxRequest.Checked,
                    BaseDamage = Utils.ToUlong(textBoxBaseDamage.Text),
                    EnemyCount = Utils.ToUlong(textBoxEnemyCount.Text),
                    Mode = (Event.AttackMode)comboBoxAttackMode.SelectedIndex,
                    ReceiveCount = comboBoxRecieve.SelectedIndex + 1,
                    OnlySearch = checkBoxOnlySearch.Checked,
                    ReceiveReword = checkBoxRecieveReword.Checked,
                    ReceivePresent = checkBoxRecievePresent.Checked,
                    AimMVP = checkBoxAimMVP.Checked,
                    OnlyAttackAssultBoss = checkBoxOnlyAttackAssultBoss.Checked,
                    WaitRecieveAssult = Utils.ToDouble(textBoxWaitRecieveAssult.Text),
                    StartTime = dateTimePickerTimeStart.Value,
                    EndTime = dateTimePickerTimeEnd.Value
                };

                Raid.StateChanged += StateChanged;
                Raid.MinicapChanged += MiniCapChanged;
                Raid.Log += OnLog;

                Log?.Invoke(this, "マクロ初期化完了");
            }
            else
            {
                MessageBox.Show("ブラウザが起動していません", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Log?.Invoke(this, "ブラウザが起動していません");

                return;
            }
            Raid.CreateThread();

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

            Raid?.KillThread();
            Raid = null;

            BotActived?.Invoke(this, false);
        }

        private void timerWatchWebdriver_Tick(object sender, EventArgs e)
        {
            if (IsStart)
            {
                if (!Webdriver.IsOoen() || !Raid.IsRun)
                {
                    IsStart = false;

                    Raid?.KillThread();
                    Raid = null;

                    buttonStop.PerformClick();

                    MessageBox.Show("マクロが停止しました", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    Log?.Invoke(this, "マクロが停止しました");
                }
            }
        }


        public void SaveSetting()
        {
            Properties.Settings.Default.RaidURL = textBoxURL.Text;
            Properties.Settings.Default.RaidJoinAssault = checkBoxJoinAssault.Checked;
            Properties.Settings.Default.RaidUseAssaultBE = checkBoxUseAssaultBE.Checked;
            Properties.Settings.Default.RaidRequest = checkBoxRequest.Checked;
            Properties.Settings.Default.RaidBaseDamage = Utils.ToUlong(textBoxBaseDamage.Text);
            Properties.Settings.Default.RaidEnemyCount = Utils.ToUlong(textBoxEnemyCount.Text);
            Properties.Settings.Default.RaidAttackMode = comboBoxAttackMode.SelectedIndex;
            Properties.Settings.Default.RaidReceiveCount = comboBoxRecieve.SelectedIndex;
            Properties.Settings.Default.RaidOnlySearch = checkBoxOnlySearch.Checked;
            Properties.Settings.Default.RaidReceiveReword = checkBoxRecieveReword.Checked;
            Properties.Settings.Default.RaidReceivePresent = checkBoxRecievePresent.Checked;
            Properties.Settings.Default.RaidAimMVP = checkBoxAimMVP.Checked;
            Properties.Settings.Default.RaidOnlyAttackAssultBoss = checkBoxOnlyAttackAssultBoss.Checked;
            Properties.Settings.Default.RaidWaitRecieveAssult = Utils.ToDouble(textBoxWaitRecieveAssult.Text);
            Properties.Settings.Default.RaidWaitAtackBattleShip = Utils.ToDouble(textBoxWaitAtackBattleShip.Text);
            Properties.Settings.Default.RaidTimeStart = dateTimePickerTimeStart.Value;
            Properties.Settings.Default.RaidTimeEnd = dateTimePickerTimeEnd.Value;
            Properties.Settings.Default.Save();
        }


        private void checkBoxJoinAssault_CheckedChanged(object sender, EventArgs e)
        {
            if (Raid != null) Raid.JoinAssault = (sender as CheckBox).Checked;
        }

        private void checkBoxUseAssaultBE_CheckedChanged(object sender, EventArgs e)
        {
            if (Raid != null) Raid.UseAssaultBE = (sender as CheckBox).Checked;
        }

        private void checkBoxRequest_CheckedChanged(object sender, EventArgs e)
        {
            if (Raid != null) Raid.Request = (sender as CheckBox).Checked;
        }

        private void textBoxBaseDamage_KeyPress(object sender, KeyPressEventArgs e) => e.Handled = Utils.ValidUlong(sender as TextBox, e);

        private void textBoxEnemyCount_KeyPress(object sender, KeyPressEventArgs e) => e.Handled = Utils.ValidUlong(sender as TextBox, e);

        private void textBoxWaitRecieveAssult_KeyPress(object sender, KeyPressEventArgs e) => e.Handled = Utils.ValidDouble(sender as TextBox, e);

        private void textBoxBaseDamage_Validated(object sender, EventArgs e) => (sender as TextBox).Text = (sender as TextBox).Text == "" ? "0" : (sender as TextBox).Text;

        private void textBoxEnemyCount_Validated(object sender, EventArgs e) => (sender as TextBox).Text = (sender as TextBox).Text == "" ? "0" : (sender as TextBox).Text;

        private void textBoxWaitRecieveAssult_Validated(object sender, EventArgs e) => (sender as TextBox).Text = (sender as TextBox).Text == "" ? "0" : (sender as TextBox).Text;


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
                        labelStatePresentList.BackColor = Color.Yellow;
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
                    case Event.State.AssaultOperationHome:
                        labelStateAssaultOperationHome.BackColor = Color.Yellow;
                        CurrentState = labelStateAssaultOperationHome;
                        break;
                    case Event.State.BattleAssaultOperation:
                        labelStateBattleAssaultOperation.BackColor = Color.Yellow;
                        CurrentState = labelStateBattleAssaultOperation;
                        break;
                    case Event.State.AssaultOperationRequest:
                        labelStateAssaultOperationRequest.BackColor = Color.Yellow;
                        CurrentState = labelStateAssaultOperationRequest;
                        break;
                    case Event.State.AssaultOperationRequestSubmit:
                        labelStateAssaultOperationRequestSubmit.BackColor = Color.Yellow;
                        CurrentState = labelStateAssaultOperationRequestSubmit;
                        break;
                    case Event.State.AssaultOperationRequestComplete:
                        labelStateAssaultOperationRequestComplete.BackColor = Color.Yellow;
                        CurrentState = labelStateAssaultOperationRequestComplete;
                        break;
                    case Event.State.AssaultOperationWin:
                        labelStateAssaultOperationWin.BackColor = Color.Yellow;
                        CurrentState = labelStateAssaultOperationWin;
                        break;
                    case Event.State.AssaultOperationFaildRequestJoin:
                        labelStateAssaultOperationRequestFaild.BackColor = Color.Yellow;
                        CurrentState = labelStateAssaultOperationRequestFaild;
                        break;
                    default:
                        labelStateUnknown.BackColor = Color.Yellow;
                        CurrentState = labelStateUnknown;
                        break;
                }
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
//http://gcc.sp.mbga.jp/_gcard_event299
                                       