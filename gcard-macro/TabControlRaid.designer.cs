namespace gcard_macro
{
    partial class TabControlRaid
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.textBoxURL = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.timerWatchWebdriver = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxBaseDamage = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxEnemyCount = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBoxAttackMode = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxRecieve = new System.Windows.Forms.ComboBox();
            this.checkBoxUseAssaultBE = new System.Windows.Forms.CheckBox();
            this.checkBoxRequest = new System.Windows.Forms.CheckBox();
            this.checkBoxJoinAssault = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonStart
            // 
            this.buttonStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStart.Location = new System.Drawing.Point(370, 447);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(81, 42);
            this.buttonStart.TabIndex = 6;
            this.buttonStart.Text = "開始";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStop.Location = new System.Drawing.Point(457, 447);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(75, 42);
            this.buttonStop.TabIndex = 0;
            this.buttonStop.Text = "停止";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // textBoxURL
            // 
            this.textBoxURL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxURL.Location = new System.Drawing.Point(136, 13);
            this.textBoxURL.Name = "textBoxURL";
            this.textBoxURL.Size = new System.Drawing.Size(396, 19);
            this.textBoxURL.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "イベントのホームのURL";
            // 
            // timerWatchWebdriver
            // 
            this.timerWatchWebdriver.Tick += new System.EventHandler(this.timerWatchWebdriver_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.textBoxEnemyCount);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.comboBoxAttackMode);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.comboBoxRecieve);
            this.groupBox1.Controls.Add(this.textBoxBaseDamage);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.checkBoxJoinAssault);
            this.groupBox1.Controls.Add(this.checkBoxRequest);
            this.groupBox1.Controls.Add(this.checkBoxUseAssaultBE);
            this.groupBox1.Location = new System.Drawing.Point(19, 41);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(506, 400);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "オプション";
            // 
            // textBoxBaseDamage
            // 
            this.textBoxBaseDamage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxBaseDamage.Location = new System.Drawing.Point(10, 34);
            this.textBoxBaseDamage.Name = "textBoxBaseDamage";
            this.textBoxBaseDamage.Size = new System.Drawing.Size(477, 19);
            this.textBoxBaseDamage.TabIndex = 11;
            this.textBoxBaseDamage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxBaseDamage_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "BEx1使用時のダメージ";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(393, 85);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(94, 12);
            this.label8.TabIndex = 30;
            this.label8.Text = "体以下で探索する";
            // 
            // textBoxEnemyCount
            // 
            this.textBoxEnemyCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxEnemyCount.Location = new System.Drawing.Point(10, 79);
            this.textBoxEnemyCount.Name = "textBoxEnemyCount";
            this.textBoxEnemyCount.Size = new System.Drawing.Size(377, 19);
            this.textBoxEnemyCount.TabIndex = 29;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 60);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 12);
            this.label7.TabIndex = 28;
            this.label7.Text = "探索する条件";
            // 
            // comboBoxAttackMode
            // 
            this.comboBoxAttackMode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxAttackMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAttackMode.FormattingEnabled = true;
            this.comboBoxAttackMode.Items.AddRange(new object[] {
            "無制限に攻撃する",
            "コンボのみ狙う",
            "一発だけ攻撃する"});
            this.comboBoxAttackMode.Location = new System.Drawing.Point(10, 124);
            this.comboBoxAttackMode.Name = "comboBoxAttackMode";
            this.comboBoxAttackMode.Size = new System.Drawing.Size(479, 20);
            this.comboBoxAttackMode.TabIndex = 27;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 151);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 12);
            this.label4.TabIndex = 26;
            this.label4.Text = "攻撃モード";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(141, 12);
            this.label3.TabIndex = 25;
            this.label3.Text = "報酬をまとめて受け取る個数";
            // 
            // comboBoxRecieve
            // 
            this.comboBoxRecieve.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxRecieve.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxRecieve.FormattingEnabled = true;
            this.comboBoxRecieve.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.comboBoxRecieve.Location = new System.Drawing.Point(10, 170);
            this.comboBoxRecieve.Name = "comboBoxRecieve";
            this.comboBoxRecieve.Size = new System.Drawing.Size(479, 20);
            this.comboBoxRecieve.TabIndex = 24;
            // 
            // checkBoxUseAssaultBE
            // 
            this.checkBoxUseAssaultBE.AutoSize = true;
            this.checkBoxUseAssaultBE.Location = new System.Drawing.Point(10, 220);
            this.checkBoxUseAssaultBE.Name = "checkBoxUseAssaultBE";
            this.checkBoxUseAssaultBE.Size = new System.Drawing.Size(132, 16);
            this.checkBoxUseAssaultBE.TabIndex = 1;
            this.checkBoxUseAssaultBE.Text = "強襲専用BEのみ使用";
            this.checkBoxUseAssaultBE.UseVisualStyleBackColor = true;
            this.checkBoxUseAssaultBE.CheckedChanged += new System.EventHandler(this.checkBoxUseAssaultBE_CheckedChanged);
            // 
            // checkBoxRequest
            // 
            this.checkBoxRequest.AutoSize = true;
            this.checkBoxRequest.Location = new System.Drawing.Point(10, 243);
            this.checkBoxRequest.Name = "checkBoxRequest";
            this.checkBoxRequest.Size = new System.Drawing.Size(103, 16);
            this.checkBoxRequest.TabIndex = 2;
            this.checkBoxRequest.Text = "応援依頼を出す";
            this.checkBoxRequest.UseVisualStyleBackColor = true;
            this.checkBoxRequest.CheckedChanged += new System.EventHandler(this.checkBoxRequest_CheckedChanged);
            // 
            // checkBoxJoinAssault
            // 
            this.checkBoxJoinAssault.AutoSize = true;
            this.checkBoxJoinAssault.Location = new System.Drawing.Point(10, 197);
            this.checkBoxJoinAssault.Name = "checkBoxJoinAssault";
            this.checkBoxJoinAssault.Size = new System.Drawing.Size(124, 16);
            this.checkBoxJoinAssault.TabIndex = 0;
            this.checkBoxJoinAssault.Text = "強襲作戦に参加する";
            this.checkBoxJoinAssault.UseVisualStyleBackColor = true;
            this.checkBoxJoinAssault.CheckedChanged += new System.EventHandler(this.checkBoxJoinAssault_CheckedChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(10, 266);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(142, 16);
            this.checkBox1.TabIndex = 31;
            this.checkBox1.Text = "MVPを狙う(レアボスのみ)";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // TabControlRaid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxURL);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonStart);
            this.Name = "TabControlRaid";
            this.Size = new System.Drawing.Size(550, 503);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.TextBox textBoxURL;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timerWatchWebdriver;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxBaseDamage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxEnemyCount;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBoxAttackMode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxRecieve;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBoxJoinAssault;
        private System.Windows.Forms.CheckBox checkBoxRequest;
        private System.Windows.Forms.CheckBox checkBoxUseAssaultBE;
    }
}
