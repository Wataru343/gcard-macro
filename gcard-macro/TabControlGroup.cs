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
    public partial class TabControlGroup : UserControl
    {
        private Group Group { get; set; }
        private volatile bool IsStart;
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

        public TabControlGroup()
        {
            InitializeComponent();
            timerWatchWebdriver.Start();
            Group = null;
            IsStart = false;
            buttonStop.Enabled = false;

            textBoxURL.Text = Properties.Settings.Default.GroupURL;
            textBoxBaseDamage.Text = Properties.Settings.Default.GroupBaseDamage.ToString();
            textBoxEnemyCount.Text = Properties.Settings.Default.GroupEnemyCount.ToString();
            textBoxPointDiff.Text = Properties.Settings.Default.GroupPointDiff.ToString();
            comboBoxAttackMode.SelectedIndex = Properties.Settings.Default.GroupAttackMode;
            comboBoxRecieve.SelectedIndex = Properties.Settings.Default.GroupReceiveCount;
            checkBoxUseAttack10.Checked = Properties.Settings.Default.GroupUseAttack10;
            checkBoxUseAttack20.Checked = Properties.Settings.Default.GroupUseAttack20;
            checkBoxUseBoost.Checked = Properties.Settings.Default.GroupUseBoost;
            checkBoxFirstAttackBoss.Checked = Properties.Settings.Default.GroupFirstAttackBoss;
            checkBoxRecieveReword.Checked = Properties.Settings.Default.GroupReceiveReword;
            checkBoxRecievePresent.Checked = Properties.Settings.Default.GroupReceivePresent;
            checkBoxAutojobLevelUp.Checked = Properties.Settings.Default.GroupAutoJobLevelUp;
            checkBoxOnlySearch.Checked = Properties.Settings.Default.GroupOnlySearch;
            comboBoxFinalJob.SelectedIndex = Properties.Settings.Default.GroupFinalJob;

            CurrentState = labelStateHome;
        }


        private void buttonStart_Click(object sender, EventArgs e)
        {
            //シリアルキーチェック
            if(Properties.Settings.Default.AccessKey != KeyGenerator.Hash.GenerateHash(UserName))
            {

                string str = Microsoft.VisualBasic.Interaction.InputBox("", "シリアルキーを入力してください", "", -1, -1);
                
                if(str != KeyGenerator.Hash.GenerateHash(UserName))
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

            Group?.KillThread();

#if !DEBUG
            if (Webdriver.IsChrome())
            {
                Webdriver.Close();
                Webdriver.CreatePhantomJS();
            }
#endif

            if (Webdriver.IsOoen())
            {
                Group = new Group(Webdriver.Instance, textBoxURL.Text)
                {
                    WaitSearch = WaitSearch,
                    WaitBattle = WaitBattle,
                    WaitAttack = WaitAttack,
                    WaitReceive = WaitReceive,
                    WaitAccessBlock = WaitAccessBlock,
                    WaitMisc = WaitMisc,
                    BaseDamage = Convert.ToUInt64(textBoxBaseDamage.Text),
                    EnemyCount = Convert.ToUInt64(textBoxEnemyCount.Text),
                    PointDiff = Convert.ToUInt64(textBoxPointDiff.Text),
                    Mode = (Event.AttackMode)comboBoxAttackMode.SelectedIndex,
                    ReceiveCount = comboBoxRecieve.SelectedIndex + 1,
                    UseAttack10 = checkBoxUseAttack10.Checked,
                    UseAttack20 = checkBoxUseAttack20.Checked,
                    UseBoost = checkBoxUseBoost.Checked,
                    FirstAttackBoss = checkBoxFirstAttackBoss.Checked,
                    ReceiveReword = checkBoxRecieveReword.Checked,
                    ReceivePresent = checkBoxRecievePresent.Checked,
                    AutojobLevelUp = checkBoxAutojobLevelUp.Checked,
                    OnlySearch = checkBoxOnlySearch.Checked,
                    FinalJob = comboBoxFinalJob.SelectedIndex
                };

                Group.StateChanged += StateChanged;
                Group.MinicapChanged += MiniCapChanged;
                Group.Log += OnLog;

                Log?.Invoke(this, "マクロ初期化完了");
            }
            else
            {
                MessageBox.Show("ブラウザが起動していません", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Log?.Invoke(this, "ブラウザが起動していません");

                return;
            }
            Group.CreateThread();

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

            Group?.KillThread();
            Group = null;

            BotActived?.Invoke(this, false);
        }

        private void timerWatchWebdriver_Tick(object sender, EventArgs e)
        {
            if (IsStart)
            {
                if (!Webdriver.IsOoen() || !Group.IsRun)
                {
                    IsStart = false;
                    Group?.KillThread();
                    Group = null;

                    buttonStop.PerformClick();

                    MessageBox.Show("マクロが停止しました", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    Log?.Invoke(this, "マクロが停止しました");
                }
            }
        }
        
        public void SaveSetting()
        {
            Properties.Settings.Default.GroupURL = textBoxURL.Text;
            Properties.Settings.Default.GroupBaseDamage = Convert.ToUInt64(textBoxBaseDamage.Text);
            Properties.Settings.Default.GroupPointDiff = Convert.ToUInt64(textBoxPointDiff.Text);
            Properties.Settings.Default.GroupEnemyCount = Convert.ToUInt64(textBoxEnemyCount.Text);
            Properties.Settings.Default.GroupAttackMode = comboBoxAttackMode.SelectedIndex;
            Properties.Settings.Default.GroupReceiveCount = comboBoxRecieve.SelectedIndex;
            Properties.Settings.Default.GroupUseAttack10 = checkBoxUseAttack10.Checked;
            Properties.Settings.Default.GroupUseAttack20 = checkBoxUseAttack20.Checked;
            Properties.Settings.Default.GroupUseBoost = checkBoxUseBoost.Checked;
            Properties.Settings.Default.GroupFirstAttackBoss = checkBoxFirstAttackBoss.Checked;
            Properties.Settings.Default.GroupReceiveReword = checkBoxRecieveReword.Checked;
            Properties.Settings.Default.GroupReceivePresent = checkBoxRecievePresent.Checked;
            Properties.Settings.Default.GroupAutoJobLevelUp = checkBoxAutojobLevelUp.Checked;
            Properties.Settings.Default.GroupOnlySearch = checkBoxOnlySearch.Checked;
            Properties.Settings.Default.GroupFinalJob = comboBoxFinalJob.SelectedIndex;
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
                        case Event.State.Interval:
                            labelStateInterval.BackColor = Color.Yellow;
                            CurrentState = labelStateInterval;
                            break;
                        case Event.State.GroupSelectJobs:
                            labelStateSelectJobs.BackColor = Color.Yellow;
                            CurrentState = labelStateSelectJobs;
                            break;
                        case Event.State.GroupUseBoost:
                            labelStateUseBoost.BackColor = Color.Yellow;
                            CurrentState = labelStateUseBoost;
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxRecievePresent_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxOnlySearch_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxAutojobLevelUp_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxRecieveReword_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxUseBoost_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxFirstAttackBoss_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBoxPointDiff_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void comboBoxRecieve_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxUseAttack20_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
//http://gcc.sp.mbga.jp/_gcard_event299
                                       