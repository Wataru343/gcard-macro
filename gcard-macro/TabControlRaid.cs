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

        public double WaitSearch { get; set; }
        public double WaitBattle { get; set; }
        public double WaitAttack { get; set; }
        public double WaitReceive { get; set; }
        public double WaitAccessBlock { get; set; }
        public double WaitMisc { get; set; }

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
        }


        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (!Uri.IsWellFormedUriString(textBoxURL.Text, UriKind.Absolute))
            {
                MessageBox.Show("URLが正しい形式ではありません", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(Raid != null)
                Raid.KillThread();


            if (Webdriver.IsOoen())
            {
                Raid = new Raid(Webdriver.Instance, textBoxURL.Text);
                Raid.WaitSearch = WaitSearch;
                Raid.WaitBattle = WaitBattle;
                Raid.WaitAttack = WaitAttack;
                Raid.WaitReceive = WaitReceive;
                Raid.WaitAccessBlock = WaitAccessBlock;
                Raid.WaitMisc = WaitMisc;
                Raid.JoinAssault = checkBoxJoinAssault.Checked;
                Raid.UseAssaultBE = checkBoxUseAssaultBE.Checked;
                Raid.Request = checkBoxRequest.Checked;
                Raid.BaseDamage = Convert.ToUInt64(textBoxBaseDamage.Text);
            }
            else
            {
                MessageBox.Show("ブラウザが起動していません", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Raid.CreateThread();

            buttonStart.Enabled = false;
            buttonStop.Enabled = true;
            IsStart = true;
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            buttonStart.Enabled = true;
            buttonStop.Enabled = false;

            IsStart = false;

            if(Raid != null)
                Raid.KillThread();
            Raid = null;            
        }

        private void timerWatchWebdriver_Tick(object sender, EventArgs e)
        {
            if (IsStart)
            {
                if (!Webdriver.IsOoen() || !Raid.IsRun)
                {
                    IsStart = false;
                    if (Raid != null)
                        Raid.KillThread();
                    Raid = null;

                    buttonStop.PerformClick();

                    MessageBox.Show("マクロが停止しました", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        public void SaveSetting()
        {
            Properties.Settings.Default.RaidURL = textBoxURL.Text;
            Properties.Settings.Default.RaidJoinAssault = checkBoxJoinAssault.Checked;
            Properties.Settings.Default.RaidUseAssaultBE = checkBoxUseAssaultBE.Checked;
            Properties.Settings.Default.RaidRequest = checkBoxRequest.Checked;
            Properties.Settings.Default.RaidBaseDamage = Convert.ToUInt64(textBoxBaseDamage.Text);
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
    }
}
//http://gcc.sp.mbga.jp/_gcard_event299
                                       