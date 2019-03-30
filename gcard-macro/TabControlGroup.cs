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

            Setting.Group.Load();

            textBoxURL.Text = Setting.Group.Url;
            textBoxBaseDamage.Text = Setting.Group.BaseDamage.ToString();
            textBoxEnemyCount.Text = Setting.Group.EnemyCount.ToString();
            textBoxPointDiff.Text = Setting.Group.PointDiff.ToString();
            comboBoxAttackMode.SelectedIndex = Setting.Group.AttackMode;
            comboBoxRecieve.SelectedIndex = Setting.Group.ReceiveCount;
            checkBoxUseBoost.Checked = Setting.Group.UseBoost;
            checkBoxFirstAttackBoss.Checked = Setting.Group.FirstAttackBoss;
            checkBoxRecieveReword.Checked = Setting.Group.ReceiveReword;
            checkBoxRecievePresent.Checked = Setting.Group.ReceivePresent;
            checkBoxAutojobLevelUp.Checked = Setting.Group.AutoJobLevelUp;
            checkBoxOnlySearch.Checked = Setting.Group.OnlySearch;
            checkBoxNoSearch.Checked = Setting.Group.NoSearch;
            comboBoxFinalJob.SelectedIndex = Setting.Group.FinalJob;

            checkBoxUseCombo30.Checked = Setting.Group.UseCombo30;
            checkBoxCombo30Normal.Checked = Setting.Group.Combo30Normal;
            checkBoxCombo30Mira.Checked = Setting.Group.Combo30Mira;
            checkBoxCombo30Boost.Checked = Setting.Group.Combo30Boost;
            checkBoxCombo30FirstAttack.Checked = Setting.Group.Combo30FirstAttack;

            checkBoxUseAttack20.Checked = Setting.Group.UseAttack20;
            checkBoxAttack20Normal.Checked = Setting.Group.Attack20Normal;
            checkBoxAttack20Mira.Checked = Setting.Group.Attack20Mira;
            checkBoxAttack20Boost.Checked = Setting.Group.Attack20Boost;
            checkBoxAttack20RequiredRatio.Checked = Setting.Group.Attack20RequiredRatio;

            checkBoxUseAttack10.Checked = Setting.Group.UseAttack10;
            checkBoxAttack10Normal.Checked = Setting.Group.Attack10Normal;
            checkBoxAttack10Boost.Checked = Setting.Group.Attack10Boost;

            checkBoxUseBE1.Checked = Setting.Group.UseBE1;
            checkBoxBE1Normal.Checked = Setting.Group.BE1Normal;
            checkBoxBE1Mira.Checked = Setting.Group.BE1Mira;
            checkBoxBE1RequiredRatio.Checked = Setting.Group.BE1RequiredRatio;

            try
            {
                dateTimePickerTimeStart.Value = Setting.Group.TimeStart;
                dateTimePickerTimeEnd.Value = Setting.Group.TimeEnd;
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
                SetSetting();
                Group = EventManager.CreateGroup(Webdriver.Instance);

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
            SetSetting();
            Setting.Group.Save();
        }

        private void SetSetting()
        {
            Setting.Group.Url = textBoxURL.Text;
            Setting.Group.BaseDamage = Utils.ToUlong(textBoxBaseDamage.Text);
            Setting.Group.PointDiff = Utils.ToUlong(textBoxPointDiff.Text);
            Setting.Group.EnemyCount = Utils.ToUlong(textBoxEnemyCount.Text);
            Setting.Group.AttackMode = comboBoxAttackMode.SelectedIndex;
            Setting.Group.ReceiveCount = comboBoxRecieve.SelectedIndex;
            Setting.Group.UseBoost = checkBoxUseBoost.Checked;
            Setting.Group.FirstAttackBoss = checkBoxFirstAttackBoss.Checked;
            Setting.Group.ReceiveReword = checkBoxRecieveReword.Checked;
            Setting.Group.ReceivePresent = checkBoxRecievePresent.Checked;
            Setting.Group.AutoJobLevelUp = checkBoxAutojobLevelUp.Checked;
            Setting.Group.OnlySearch = checkBoxOnlySearch.Checked;
            Setting.Group.NoSearch = checkBoxNoSearch.Checked;
            Setting.Group.FinalJob = comboBoxFinalJob.SelectedIndex;
            Setting.Group.TimeStart = dateTimePickerTimeStart.Value;
            Setting.Group.TimeEnd = dateTimePickerTimeEnd.Value;
            Setting.Group.UseCombo30 = checkBoxUseCombo30.Checked;
            Setting.Group.Combo30Normal = checkBoxCombo30Normal.Checked;
            Setting.Group.Combo30Mira = checkBoxCombo30Mira.Checked;
            Setting.Group.Combo30Boost = checkBoxCombo30Boost.Checked;
            Setting.Group.Combo30FirstAttack = checkBoxCombo30FirstAttack.Checked;
            Setting.Group.UseAttack20 = checkBoxUseAttack20.Checked;
            Setting.Group.Attack20Normal = checkBoxAttack20Normal.Checked;
            Setting.Group.Attack20Mira = checkBoxAttack20Mira.Checked;
            Setting.Group.Attack20Boost = checkBoxAttack20Boost.Checked;
            Setting.Group.Attack20RequiredRatio = checkBoxAttack20RequiredRatio.Checked;
            Setting.Group.UseAttack10 = checkBoxUseAttack10.Checked;
            Setting.Group.Attack10Normal = checkBoxAttack10Normal.Checked;
            Setting.Group.Attack10Boost = checkBoxAttack10Boost.Checked;
            Setting.Group.UseBE1 = checkBoxUseBE1.Checked;
            Setting.Group.BE1Normal = checkBoxBE1Normal.Checked;
            Setting.Group.BE1Mira = checkBoxBE1Mira.Checked;
            Setting.Group.BE1RequiredRatio = checkBoxBE1RequiredRatio.Checked;
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
