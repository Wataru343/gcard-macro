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
            checkBoxUseBoost.Checked = Properties.Settings.Default.GroupUseBoost;
            checkBoxFirstAttackBoss.Checked = Properties.Settings.Default.GroupFirstAttackBoss;
            checkBoxRecieveReword.Checked = Properties.Settings.Default.GroupReceiveReword;
            checkBoxRecievePresent.Checked = Properties.Settings.Default.GroupReceivePresent;
            checkBoxAutojobLevelUp.Checked = Properties.Settings.Default.GroupAutoJobLevelUp;
            checkBoxOnlySearch.Checked = Properties.Settings.Default.GroupOnlySearch;
            checkBoxNoSearch.Checked = Properties.Settings.Default.GroupNoSearch;
            comboBoxFinalJob.SelectedIndex = Properties.Settings.Default.GroupFinalJob;

            checkBoxUseCombo30.Checked = Properties.Settings.Default.GroupUseCombo30;
            checkBoxCombo30Normal.Checked = Properties.Settings.Default.GroupCombo30Normal;
            checkBoxCombo30Mira.Checked = Properties.Settings.Default.GroupCombo30Mira;
            checkBoxCombo30Boost.Checked = Properties.Settings.Default.GroupCombo30Boost;
            checkBoxCombo30FirstAttack.Checked = Properties.Settings.Default.GroupCombo30FirstAttack;

            checkBoxUseAttack20.Checked = Properties.Settings.Default.GroupUseAttack20;
            checkBoxAttack20Normal.Checked = Properties.Settings.Default.GroupAttack20Normal;
            checkBoxAttack20Mira.Checked = Properties.Settings.Default.GroupAttack20Mira;
            checkBoxAttack20Boost.Checked = Properties.Settings.Default.GroupAttack20Boost;
            checkBoxAttack20RequiredRatio.Checked = Properties.Settings.Default.GroupAttack20RequiredRatio;

            checkBoxUseAttack10.Checked = Properties.Settings.Default.GroupUseAttack10;
            checkBoxAttack10Normal.Checked = Properties.Settings.Default.GroupAttack10Normal;
            checkBoxAttack10Boost.Checked = Properties.Settings.Default.GroupAttack10Boost;

            checkBoxUseBE1.Checked = Properties.Settings.Default.GroupUseBE1;
            checkBoxBE1Normal.Checked = Properties.Settings.Default.GroupBE1Normal;
            checkBoxBE1Mira.Checked = Properties.Settings.Default.GroupBE1Mira;
            checkBoxBE1RequiredRatio.Checked = Properties.Settings.Default.GroupBE1RequiredRatio;

            try
            {
                dateTimePickerTimeStart.Value = Properties.Settings.Default.GroupTimeStart;
                dateTimePickerTimeEnd.Value = Properties.Settings.Default.GroupTimeEnd;
            }
            catch
            {
                dateTimePickerTimeStart.Value = dateTimePickerTimeStart.MinDate;
                dateTimePickerTimeEnd.Value = dateTimePickerTimeEnd.MinDate;
            }

            checkBoxUseCombo30.CheckedChanged += ValueChanged;
            checkBoxUseAttack20.CheckedChanged += ValueChanged;
            checkBoxUseAttack10.CheckedChanged += ValueChanged;
            checkBoxUseBE1.CheckedChanged += ValueChanged;

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
                
                if(str != KeyGenerator.Hash.GenerateHash(UserName))
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

            Group?.KillThread();

#if !DEBUG
            if (Webdriver.IsChrome())
            {
                Webdriver.Close();
                Webdriver.CreateHtmlAgilityPackDriver();
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
                    BaseDamage = Utils.ToUlong(textBoxBaseDamage.Text),
                    EnemyCount = Utils.ToUlong(textBoxEnemyCount.Text),
                    PointDiff = Utils.ToUlong(textBoxPointDiff.Text),
                    Mode = (Event.AttackMode)comboBoxAttackMode.SelectedIndex,
                    ReceiveCount = comboBoxRecieve.SelectedIndex + 1,
                    UseBoost = checkBoxUseBoost.Checked,
                    FirstAttackBoss = checkBoxFirstAttackBoss.Checked,
                    ReceiveReword = checkBoxRecieveReword.Checked,
                    ReceivePresent = checkBoxRecievePresent.Checked,
                    AutojobLevelUp = checkBoxAutojobLevelUp.Checked,
                    OnlySearch = checkBoxOnlySearch.Checked,
                    NoSearch = checkBoxNoSearch.Checked,
                    FinalJob = comboBoxFinalJob.SelectedIndex,
                    StartTime = dateTimePickerTimeStart.Value,
                    EndTime = dateTimePickerTimeEnd.Value,
                    SampleCount = OptimizedWaitEnemyCount,
                    Combo30 = new Combo30Button()
                    {
                        Type = SpecialAttackButton.AttackType.Combo30,
                        Use = checkBoxUseCombo30.Checked,
                        Normal = checkBoxCombo30Normal.Checked,
                        Mira = checkBoxCombo30Mira.Checked,
                        FirstAttack = checkBoxCombo30FirstAttack.Checked,
                        Boost = checkBoxCombo30Boost.Checked
                    },
                    Attack20 = new Attack20Button()
                    {
                        Type = SpecialAttackButton.AttackType.Attack20,
                        Use = checkBoxUseAttack20.Checked,
                        Normal = checkBoxAttack20Normal.Checked,
                        Mira = checkBoxAttack20Mira.Checked,
                        Boost = checkBoxAttack20Boost.Checked,
                        RequiredRatio = checkBoxAttack20RequiredRatio.Checked
                    },
                    Attack10 = new Attack10Button()
                    {
                        Type = SpecialAttackButton.AttackType.Attack10,
                        Use = checkBoxUseAttack10.Checked,
                        Normal = checkBoxAttack10Normal.Checked,
                        Mira = false,
                        Boost = checkBoxAttack10Boost.Checked
                    },
                    BE1 = new BE1Button()
                    {
                        Type = SpecialAttackButton.AttackType.BE1,
                        Use = checkBoxUseBE1.Checked,
                        Normal = checkBoxBE1Normal.Checked,
                        Mira = checkBoxBE1Mira.Checked,
                        Boost = true,
                        RequiredRatio = checkBoxBE1RequiredRatio.Checked
                    }
                };

                Group.StateChanged += StateChanged;
                Group.MinicapChanged += MiniCapChanged;
                Group.Log += OnLog;
                Group.SpeedCounter += OnSpeedCount;

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
            Properties.Settings.Default.GroupBaseDamage = Utils.ToUlong(textBoxBaseDamage.Text);
            Properties.Settings.Default.GroupPointDiff = Utils.ToUlong(textBoxPointDiff.Text);
            Properties.Settings.Default.GroupEnemyCount = Utils.ToUlong(textBoxEnemyCount.Text);
            Properties.Settings.Default.GroupAttackMode = comboBoxAttackMode.SelectedIndex;
            Properties.Settings.Default.GroupReceiveCount = comboBoxRecieve.SelectedIndex;
            Properties.Settings.Default.GroupUseBoost = checkBoxUseBoost.Checked;
            Properties.Settings.Default.GroupFirstAttackBoss = checkBoxFirstAttackBoss.Checked;
            Properties.Settings.Default.GroupReceiveReword = checkBoxRecieveReword.Checked;
            Properties.Settings.Default.GroupReceivePresent = checkBoxRecievePresent.Checked;
            Properties.Settings.Default.GroupAutoJobLevelUp = checkBoxAutojobLevelUp.Checked;
            Properties.Settings.Default.GroupOnlySearch = checkBoxOnlySearch.Checked;
            Properties.Settings.Default.GroupNoSearch = checkBoxNoSearch.Checked;
            Properties.Settings.Default.GroupFinalJob = comboBoxFinalJob.SelectedIndex;
            Properties.Settings.Default.GroupTimeStart = dateTimePickerTimeStart.Value;
            Properties.Settings.Default.GroupTimeEnd = dateTimePickerTimeEnd.Value;
            Properties.Settings.Default.GroupUseCombo30 = checkBoxUseCombo30.Checked;
            Properties.Settings.Default.GroupCombo30Normal = checkBoxCombo30Normal.Checked;
            Properties.Settings.Default.GroupCombo30Mira = checkBoxCombo30Mira.Checked;
            Properties.Settings.Default.GroupCombo30Boost = checkBoxCombo30Boost.Checked;
            Properties.Settings.Default.GroupCombo30FirstAttack = checkBoxCombo30FirstAttack.Checked;
            Properties.Settings.Default.GroupUseAttack20 = checkBoxUseAttack20.Checked;
            Properties.Settings.Default.GroupAttack20Normal = checkBoxAttack20Normal.Checked;
            Properties.Settings.Default.GroupAttack20Mira = checkBoxAttack20Mira.Checked;
            Properties.Settings.Default.GroupAttack20Boost = checkBoxAttack20Boost.Checked;
            Properties.Settings.Default.GroupAttack20RequiredRatio = checkBoxAttack20RequiredRatio.Checked;
            Properties.Settings.Default.GroupUseAttack10 = checkBoxUseAttack10.Checked;
            Properties.Settings.Default.GroupAttack10Normal = checkBoxAttack10Normal.Checked;
            Properties.Settings.Default.GroupAttack10Boost = checkBoxAttack10Boost.Checked;
            Properties.Settings.Default.GroupUseBE1 = checkBoxUseBE1.Checked;
            Properties.Settings.Default.GroupBE1Normal = checkBoxBE1Normal.Checked;
            Properties.Settings.Default.GroupBE1Mira = checkBoxBE1Mira.Checked;
            Properties.Settings.Default.GroupBE1RequiredRatio = checkBoxBE1RequiredRatio.Checked;
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

        public void RecievePresent() => Group?.SendRecievePresentRequest();

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

        private void checkBoxUseCombo30_CheckedChanged(object sender, EventArgs e) => checkBoxCombo30Normal.Enabled = checkBoxCombo30Mira.Enabled = checkBoxCombo30Boost.Enabled = checkBoxCombo30FirstAttack.Enabled = (sender as CheckBox).Checked;

        private void checkBoxUseAttack20_CheckedChanged(object sender, EventArgs e) => checkBoxAttack20Normal.Enabled = checkBoxAttack20Mira.Enabled = checkBoxAttack20Boost.Enabled = checkBoxAttack20RequiredRatio.Enabled = (sender as CheckBox).Checked;

        private void checkBoxUseAttack10_CheckedChanged(object sender, EventArgs e) => checkBoxAttack10Normal.Enabled = checkBoxAttack10Boost.Enabled = (sender as CheckBox).Checked;

        private void checkBoxUseBE1_CheckedChanged(object sender, EventArgs e) => checkBoxBE1Normal.Enabled = checkBoxBE1Mira.Enabled = checkBoxBE1RequiredRatio.Enabled = (sender as CheckBox).Checked;

        private void PaintFrameTopLeftRight(object sender, PaintEventArgs e) => PaintFrame(e, new Point[] { new Point(0, (sender as Control).Height - 1), new Point(0, 0), new Point((sender as Control).Width - 1, 0), new Point((sender as Control).Width - 1, (sender as Control).Height - 1) });

        private void PaintFrameTopLeftRightBottom(object sender, PaintEventArgs e) => PaintFrame(e, new Point[] { new Point(0, (sender as Control).Height - 1), new Point(0, 0), new Point((sender as Control).Width - 1, 0), new Point((sender as Control).Width - 1, (sender as Control).Height - 1), new Point(0, (sender as Control).Height - 1) });

        private void PaintFrameTopRight(object sender, PaintEventArgs e) => PaintFrame(e, new Point[] { new Point(0, 0), new Point((sender as Control).Size.Width - 1, 0), new Point((sender as Control).Size.Width - 1, (sender as Control).Size.Height - 1) });

        private void PaintFrameTopRightBottom(object sender, PaintEventArgs e) => PaintFrame(e, new Point[] { new Point(0, 0), new Point((sender as Control).Size.Width - 1, 0), new Point((sender as Control).Size.Width - 1, (sender as Control).Size.Height - 1), new Point(0, (sender as Control).Size.Height - 1) });

        private void PaintFrame(PaintEventArgs e, Point[] pt) => e.Graphics.DrawLines(new Pen(Color.Black, 1), pt);

        private void timerRecievePresent_Tick(object sender, EventArgs e) => Group?.SendRecievePresentRequest();
    }
}
