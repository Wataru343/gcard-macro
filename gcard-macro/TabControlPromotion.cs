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
    public partial class TabControlPromotion : UserControl
    {
        private Promotion Promotion { get; set; }
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

        public TabControlPromotion()
        {
            InitializeComponent();
            timerWatchWebdriver.Start();
            Promotion = null;
            IsStart = false;
            buttonStop.Enabled = false;

            textBoxURL.Text = Properties.Settings.Default.PromotionURL;
            textBoxWatchRank.Text = Properties.Settings.Default.PromotionWatchRank.ToString();
            comboBoxAttackMode.SelectedIndex = Properties.Settings.Default.PromotionAttackMode;
            textBoxSallyCount.Text = Properties.Settings.Default.PromotionSallyCount.ToString();

            try
            {
                dateTimePickerSallyTimeStart.Value = Properties.Settings.Default.PromotionTimeStart;
                dateTimePickerSallyTimeEnd.Value = Properties.Settings.Default.PromotionTimeEnd;
            }
            catch { }
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

            Promotion?.KillThread();

#if !DEBUG
            if (Webdriver.IsChrome())
            {
                Webdriver.Close();
                Webdriver.CreateHtmlAgilityPackDriver();
            }
#endif

            if (Webdriver.IsOoen())
            {
                Promotion = new Promotion(Webdriver.Instance, textBoxURL.Text)
                {
                    WaitSearch = WaitSearch,
                    WaitBattle = WaitBattle,
                    WaitAttack = WaitAttack,
                    WaitReceive = WaitReceive,
                    WaitAccessBlock = WaitAccessBlock,
                    WaitMisc = WaitMisc,
                    Mode = (Event.AttackMode)comboBoxAttackMode.SelectedIndex + 3,
                    WatchRank = Convert.ToInt32(textBoxWatchRank.Text),
                    SallyCount = Convert.ToInt32(textBoxSallyCount.Text),
                    SallyUnlimited = Convert.ToInt32(textBoxSallyCount.Text) == 0 ? true : false,
                    SallyStart = dateTimePickerSallyTimeStart.Value,
                    SallyEnd = dateTimePickerSallyTimeEnd.Value
                };

                Promotion.StateChanged += StateChanged;
                Promotion.MinicapChanged += MiniCapChanged;
                Promotion.SallyCountChanged += SallyCountChanged;
                Promotion.Log += OnLog;

                Log?.Invoke(this, "マクロ初期化完了");
            }
            else
            {
                MessageBox.Show("ブラウザが起動していません", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Log?.Invoke(this, "ブラウザが起動していません");

                return;
            }
            Promotion.CreateThread();

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

            Promotion?.KillThread();
            Promotion = null;

            BotActived?.Invoke(this, false);
        }

        private void timerWatchWebdriver_Tick(object sender, EventArgs e)
        {
            if (IsStart)
            {
                if (!Webdriver.IsOoen() || !Promotion.IsRun)
                {
                    IsStart = false;

                    Promotion?.KillThread();
                    Promotion = null;

                    buttonStop.PerformClick();

                    MessageBox.Show("マクロが停止しました", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    Log?.Invoke(this, "マクロが停止しました");
                }
            }
        }

        public void SaveSetting()
        {
            Properties.Settings.Default.PromotionURL = textBoxURL.Text;
            Properties.Settings.Default.PromotionWatchRank = Convert.ToInt32(textBoxWatchRank.Text);
            Properties.Settings.Default.PromotionAttackMode = comboBoxAttackMode.SelectedIndex;
            Properties.Settings.Default.PromotionSallyCount = Convert.ToInt32(textBoxSallyCount.Text);
            Properties.Settings.Default.PromotionTimeStart = dateTimePickerSallyTimeStart.Value;
            Properties.Settings.Default.PromotionTimeEnd = dateTimePickerSallyTimeEnd.Value;
            Properties.Settings.Default.Save();
        }


        private void textBoxBaseDamage_KeyPress(object sender, KeyPressEventArgs e) => e.Handled = Utils.ValidUlong(sender as TextBox, e);


        private void textBoxWatchRank_KeyPress(object sender, KeyPressEventArgs e) => e.Handled = Utils.ValidUlong(sender as TextBox, e);

        private void textBoxWatchRank_Leave(object sender, EventArgs e) => (sender as TextBox).Text = (sender as TextBox).Text == "" ? "0" : (sender as TextBox).Text;

        private void textBoxSallyCount_KeyPress(object sender, KeyPressEventArgs e) => e.Handled = Utils.ValidUlong(sender as TextBox, e);

        private void textBoxSallyCount_Leave(object sender, EventArgs e) => (sender as TextBox).Text = (sender as TextBox).Text == "" ? "0" : (sender as TextBox).Text;

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
                        case Event.State.BattleFlash:
                            labelStateBattleFlash.BackColor = Color.Yellow;
                            CurrentState = labelStateBattleFlash;
                            break;
                        case Event.State.Error:
                            labelStateError.BackColor = Color.Yellow;
                            CurrentState = labelStateError;
                            break;
                        case Event.State.Result:
                            labelStateResult.BackColor = Color.Yellow;
                            CurrentState = labelStateResult;
                            break;
                        case Event.State.AccessBlock:
                            labelStateAccessBlock.BackColor = Color.Yellow;
                            CurrentState = labelStateAccessBlock;
                            break;
                        case Event.State.EventFinished:
                            labelStateEventFinished.BackColor = Color.Yellow;
                            CurrentState = labelStateEventFinished;
                            break;
                        case Event.State.Unknown:
                            labelStateUnknown.BackColor = Color.Yellow;
                            CurrentState = labelStateUnknown;
                            break;
                        case Event.State.PromotionWithdrawalConfirmation:
                            labelStateWithdrawalConfirmation.BackColor = Color.Yellow;
                            CurrentState = labelStateWithdrawalConfirmation;
                            break;
                        case Event.State.PromotionWithdrawalCompletion:
                            labelStateWithdrawalCompletion.BackColor = Color.Yellow;
                            CurrentState = labelStateWithdrawalCompletion;
                            break;
                        case Event.State.PromotionSallyConfirmation:
                            labelStateSallyConfirmation.BackColor = Color.Yellow;
                            CurrentState = labelStateSallyConfirmation;
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


        private void SallyCountChanged(object sender, int count)
        {
            Invoke((MethodInvoker)delegate
            {
                labelSallyCount.Text = "残り出撃回数：" + count.ToString() + "回";
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
                                       