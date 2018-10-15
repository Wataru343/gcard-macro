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
        private bool IsStart { get; set; }
        private Label CurrentState { get; set; }

        public double WaitSearch { get; set; }
        public double WaitBattle { get; set; }
        public double WaitAttack { get; set; }
        public double WaitReceive { get; set; }
        public double WaitAccessBlock { get; set; }
        public double WaitMisc { get; set; }
        public string UserName { get; set; }

        public delegate void BotActiveHandler(object sender, bool actived);
        public event BotActiveHandler BotActived;
        public delegate void LogHandler(object sender, string text);
        public event LogHandler Log;

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
            checkBoxRequest.Checked = Properties.Settings.Default.GShootingRequest;

            CurrentState = labelStateHome;
        }


        private void buttonStart_Click(object sender, EventArgs e)
        {
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

            if (!Uri.IsWellFormedUriString(textBoxURL.Text, UriKind.Absolute))
            {
                MessageBox.Show("URLが正しい形式ではありません", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            GShooting?.KillThread();

#if !DEBUG
            if (Webdriver.IsChrome())
            {
                Webdriver.Close();
                Webdriver.CreatePhantomJS();
            }
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
                    BaseDamage = Convert.ToUInt64(textBoxBaseDamage.Text),
                    EnemyCount = Convert.ToUInt64(textBoxEnemyCount.Text),
                    Mode = (Event.AttackMode)comboBoxAttackMode.SelectedIndex,
                    ReceiveCount = comboBoxRecieve.SelectedIndex + 1,
                    ReceiveReword = checkBoxRecieveReword.Checked,
                    ReceivePresent = checkBoxRecievePresent.Checked,
                    OnlySearch = checkBoxOnlySearch.Checked,
                    Request = checkBoxRequest.Checked
                };

                GShooting.StateChanged += StateChanged;
                GShooting.MinicapChanged += MiniCapChanged;
                GShooting.Log += OnLog;

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
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            buttonStart.Enabled = true;
            buttonStop.Enabled = false;

            IsStart = false;

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
            Properties.Settings.Default.GShootingBaseDamage = Convert.ToUInt64(textBoxBaseDamage.Text);
            Properties.Settings.Default.GShootingEnemyCount = Convert.ToUInt64(textBoxEnemyCount.Text);
            Properties.Settings.Default.GShootingAttackMode = comboBoxAttackMode.SelectedIndex;
            Properties.Settings.Default.GShootingReceiveCount = comboBoxRecieve.SelectedIndex;
            Properties.Settings.Default.GShootingReceiveReword = checkBoxRecieveReword.Checked;
            Properties.Settings.Default.GShootingReceivePresent = checkBoxRecievePresent.Checked;
            Properties.Settings.Default.GShootingOnlySearch = checkBoxOnlySearch.Checked;
            Properties.Settings.Default.GShootingRequest = checkBoxRequest.Checked;
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

        private void OnLog(object sender, string text)
        {
            Invoke((MethodInvoker)delegate
            {
                Log?.Invoke(sender, text);
            });
        }
    }
}
