namespace gcard_macro
{
    partial class TabControlGTactics
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
            this.checkBoxStandby = new System.Windows.Forms.CheckBox();
            this.label19 = new System.Windows.Forms.Label();
            this.textBoxPointDiff = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.comboBoxPriority = new System.Windows.Forms.ComboBox();
            this.checkBoxForceCharge = new System.Windows.Forms.CheckBox();
            this.checkBoxUseForce = new System.Windows.Forms.CheckBox();
            this.label17 = new System.Windows.Forms.Label();
            this.comboBoxForcePattern = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label16 = new System.Windows.Forms.Label();
            this.comboBoxStrategicArea = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.comboBoxShieldC3 = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.comboBoxShieldC2 = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.comboBoxShieldC1 = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.comboBoxShieldB3 = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.comboBoxShieldB2 = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.comboBoxShieldB1 = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.comboBoxShieldA3 = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBoxShieldA2 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxShieldA1 = new System.Windows.Forms.ComboBox();
            this.checkBoxRecievePresent = new System.Windows.Forms.CheckBox();
            this.checkBoxOnlySearch = new System.Windows.Forms.CheckBox();
            this.checkBoxRecieveReword = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxEnemyCount = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBoxAttackMode = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxRecieve = new System.Windows.Forms.ComboBox();
            this.textBoxBaseDamage = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.labelStateHome = new System.Windows.Forms.Label();
            this.labelStateSearch = new System.Windows.Forms.Label();
            this.labelStateEnemyList = new System.Windows.Forms.Label();
            this.labelStateBattle = new System.Windows.Forms.Label();
            this.labelStateBattleFlash = new System.Windows.Forms.Label();
            this.labelStateResult = new System.Windows.Forms.Label();
            this.labelStateReceive = new System.Windows.Forms.Label();
            this.labelStatePresentList = new System.Windows.Forms.Label();
            this.labelStateError = new System.Windows.Forms.Label();
            this.labelStateFightAlreadyFinished = new System.Windows.Forms.Label();
            this.labelStateAccessBlock = new System.Windows.Forms.Label();
            this.labelStateEventFinished = new System.Windows.Forms.Label();
            this.labelStateUnknown = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.labelStateInterval = new System.Windows.Forms.Label();
            this.labelStateGetCard = new System.Windows.Forms.Label();
            this.labelStateLevelUp = new System.Windows.Forms.Label();
            this.labelMiniCap = new System.Windows.Forms.Label();
            this.labelArea = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.textBoxWaitForce = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.dateTimePickerTimeEnd = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerTimeStart = new System.Windows.Forms.DateTimePicker();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonStart
            // 
            this.buttonStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStart.Location = new System.Drawing.Point(291, 643);
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
            this.buttonStop.Location = new System.Drawing.Point(378, 643);
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
            this.textBoxURL.Size = new System.Drawing.Size(517, 19);
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
            this.groupBox1.Controls.Add(this.label23);
            this.groupBox1.Controls.Add(this.label24);
            this.groupBox1.Controls.Add(this.dateTimePickerTimeEnd);
            this.groupBox1.Controls.Add(this.dateTimePickerTimeStart);
            this.groupBox1.Controls.Add(this.label21);
            this.groupBox1.Controls.Add(this.checkBoxStandby);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.textBoxWaitForce);
            this.groupBox1.Controls.Add(this.textBoxPointDiff);
            this.groupBox1.Controls.Add(this.label20);
            this.groupBox1.Controls.Add(this.label22);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.comboBoxPriority);
            this.groupBox1.Controls.Add(this.checkBoxForceCharge);
            this.groupBox1.Controls.Add(this.checkBoxUseForce);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.comboBoxForcePattern);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.checkBoxRecievePresent);
            this.groupBox1.Controls.Add(this.checkBoxOnlySearch);
            this.groupBox1.Controls.Add(this.checkBoxRecieveReword);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.textBoxEnemyCount);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.comboBoxAttackMode);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.comboBoxRecieve);
            this.groupBox1.Controls.Add(this.textBoxBaseDamage);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(19, 41);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(434, 596);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "オプション";
            // 
            // checkBoxStandby
            // 
            this.checkBoxStandby.AutoSize = true;
            this.checkBoxStandby.Location = new System.Drawing.Point(14, 379);
            this.checkBoxStandby.Name = "checkBoxStandby";
            this.checkBoxStandby.Size = new System.Drawing.Size(299, 16);
            this.checkBoxStandby.TabIndex = 38;
            this.checkBoxStandby.Text = "目標エリアレベル、敵部隊との点数差を満たしたら待機する";
            this.checkBoxStandby.UseVisualStyleBackColor = true;
            // 
            // label19
            // 
            this.label19.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(365, 353);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(63, 12);
            this.label19.TabIndex = 37;
            this.label19.Text = "以上で待機";
            // 
            // textBoxPointDiff
            // 
            this.textBoxPointDiff.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPointDiff.Location = new System.Drawing.Point(14, 350);
            this.textBoxPointDiff.Name = "textBoxPointDiff";
            this.textBoxPointDiff.Size = new System.Drawing.Size(345, 19);
            this.textBoxPointDiff.TabIndex = 36;
            this.textBoxPointDiff.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxPointDiff_KeyPress);
            this.textBoxPointDiff.Validated += new System.EventHandler(this.textBoxPointDiff_Validated);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(14, 330);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(95, 12);
            this.label20.TabIndex = 35;
            this.label20.Text = "敵部隊との点数差";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(14, 540);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(99, 12);
            this.label18.TabIndex = 34;
            this.label18.Text = "戦略拠点の優先度";
            // 
            // comboBoxPriority
            // 
            this.comboBoxPriority.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxPriority.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPriority.FormattingEnabled = true;
            this.comboBoxPriority.Items.AddRange(new object[] {
            "戦略拠点を優先しない",
            "戦略拠点を優先する",
            "戦略拠点のみ"});
            this.comboBoxPriority.Location = new System.Drawing.Point(14, 558);
            this.comboBoxPriority.Name = "comboBoxPriority";
            this.comboBoxPriority.Size = new System.Drawing.Size(210, 20);
            this.comboBoxPriority.TabIndex = 31;
            // 
            // checkBoxForceCharge
            // 
            this.checkBoxForceCharge.AutoSize = true;
            this.checkBoxForceCharge.Location = new System.Drawing.Point(14, 428);
            this.checkBoxForceCharge.Name = "checkBoxForceCharge";
            this.checkBoxForceCharge.Size = new System.Drawing.Size(148, 16);
            this.checkBoxForceCharge.TabIndex = 33;
            this.checkBoxForceCharge.Text = "フォースチャージを使用する";
            this.checkBoxForceCharge.UseVisualStyleBackColor = true;
            // 
            // checkBoxUseForce
            // 
            this.checkBoxUseForce.AutoSize = true;
            this.checkBoxUseForce.Location = new System.Drawing.Point(14, 404);
            this.checkBoxUseForce.Name = "checkBoxUseForce";
            this.checkBoxUseForce.Size = new System.Drawing.Size(111, 16);
            this.checkBoxUseForce.TabIndex = 32;
            this.checkBoxUseForce.Text = "フォースを使用する";
            this.checkBoxUseForce.UseVisualStyleBackColor = true;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(14, 492);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(159, 12);
            this.label17.TabIndex = 31;
            this.label17.Text = "戦略拠点のフォース使用パターン";
            // 
            // comboBoxForcePattern
            // 
            this.comboBoxForcePattern.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxForcePattern.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxForcePattern.FormattingEnabled = true;
            this.comboBoxForcePattern.Items.AddRange(new object[] {
            "攻撃回数を最適化",
            "強弱弱弱",
            "弱のみ"});
            this.comboBoxForcePattern.Location = new System.Drawing.Point(14, 512);
            this.comboBoxForcePattern.Name = "comboBoxForcePattern";
            this.comboBoxForcePattern.Size = new System.Drawing.Size(210, 20);
            this.comboBoxForcePattern.TabIndex = 30;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Controls.Add(this.comboBoxStrategicArea);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.comboBoxShieldC3);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.comboBoxShieldC2);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.comboBoxShieldC1);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.comboBoxShieldB3);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.comboBoxShieldB2);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.comboBoxShieldB1);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.comboBoxShieldA3);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.comboBoxShieldA2);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.comboBoxShieldA1);
            this.groupBox3.Location = new System.Drawing.Point(230, 444);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(198, 141);
            this.groupBox3.TabIndex = 29;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "目標エリアレベル";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(138, 58);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(53, 12);
            this.label16.TabIndex = 19;
            this.label16.Text = "戦略拠点";
            // 
            // comboBoxStrategicArea
            // 
            this.comboBoxStrategicArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStrategicArea.FormattingEnabled = true;
            this.comboBoxStrategicArea.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3"});
            this.comboBoxStrategicArea.Location = new System.Drawing.Point(138, 73);
            this.comboBoxStrategicArea.Name = "comboBoxStrategicArea";
            this.comboBoxStrategicArea.Size = new System.Drawing.Size(34, 20);
            this.comboBoxStrategicArea.TabIndex = 18;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(94, 99);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(19, 12);
            this.label13.TabIndex = 17;
            this.label13.Text = "C3";
            // 
            // comboBoxShieldC3
            // 
            this.comboBoxShieldC3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxShieldC3.FormattingEnabled = true;
            this.comboBoxShieldC3.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3"});
            this.comboBoxShieldC3.Location = new System.Drawing.Point(94, 114);
            this.comboBoxShieldC3.Name = "comboBoxShieldC3";
            this.comboBoxShieldC3.Size = new System.Drawing.Size(34, 20);
            this.comboBoxShieldC3.TabIndex = 16;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(50, 99);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(19, 12);
            this.label14.TabIndex = 15;
            this.label14.Text = "C2";
            // 
            // comboBoxShieldC2
            // 
            this.comboBoxShieldC2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxShieldC2.FormattingEnabled = true;
            this.comboBoxShieldC2.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3"});
            this.comboBoxShieldC2.Location = new System.Drawing.Point(50, 114);
            this.comboBoxShieldC2.Name = "comboBoxShieldC2";
            this.comboBoxShieldC2.Size = new System.Drawing.Size(34, 20);
            this.comboBoxShieldC2.TabIndex = 14;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 99);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(19, 12);
            this.label15.TabIndex = 13;
            this.label15.Text = "C1";
            // 
            // comboBoxShieldC1
            // 
            this.comboBoxShieldC1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxShieldC1.FormattingEnabled = true;
            this.comboBoxShieldC1.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3"});
            this.comboBoxShieldC1.Location = new System.Drawing.Point(6, 114);
            this.comboBoxShieldC1.Name = "comboBoxShieldC1";
            this.comboBoxShieldC1.Size = new System.Drawing.Size(34, 20);
            this.comboBoxShieldC1.TabIndex = 12;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(94, 58);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(19, 12);
            this.label10.TabIndex = 11;
            this.label10.Text = "B3";
            // 
            // comboBoxShieldB3
            // 
            this.comboBoxShieldB3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxShieldB3.FormattingEnabled = true;
            this.comboBoxShieldB3.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3"});
            this.comboBoxShieldB3.Location = new System.Drawing.Point(94, 73);
            this.comboBoxShieldB3.Name = "comboBoxShieldB3";
            this.comboBoxShieldB3.Size = new System.Drawing.Size(34, 20);
            this.comboBoxShieldB3.TabIndex = 10;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(50, 58);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(19, 12);
            this.label11.TabIndex = 9;
            this.label11.Text = "B2";
            // 
            // comboBoxShieldB2
            // 
            this.comboBoxShieldB2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxShieldB2.FormattingEnabled = true;
            this.comboBoxShieldB2.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3"});
            this.comboBoxShieldB2.Location = new System.Drawing.Point(50, 73);
            this.comboBoxShieldB2.Name = "comboBoxShieldB2";
            this.comboBoxShieldB2.Size = new System.Drawing.Size(34, 20);
            this.comboBoxShieldB2.TabIndex = 8;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 58);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(19, 12);
            this.label12.TabIndex = 7;
            this.label12.Text = "B1";
            // 
            // comboBoxShieldB1
            // 
            this.comboBoxShieldB1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxShieldB1.FormattingEnabled = true;
            this.comboBoxShieldB1.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3"});
            this.comboBoxShieldB1.Location = new System.Drawing.Point(6, 73);
            this.comboBoxShieldB1.Name = "comboBoxShieldB1";
            this.comboBoxShieldB1.Size = new System.Drawing.Size(34, 20);
            this.comboBoxShieldB1.TabIndex = 6;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(94, 17);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(19, 12);
            this.label9.TabIndex = 5;
            this.label9.Text = "A3";
            // 
            // comboBoxShieldA3
            // 
            this.comboBoxShieldA3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxShieldA3.FormattingEnabled = true;
            this.comboBoxShieldA3.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3"});
            this.comboBoxShieldA3.Location = new System.Drawing.Point(94, 32);
            this.comboBoxShieldA3.Name = "comboBoxShieldA3";
            this.comboBoxShieldA3.Size = new System.Drawing.Size(34, 20);
            this.comboBoxShieldA3.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(50, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(19, 12);
            this.label6.TabIndex = 3;
            this.label6.Text = "A2";
            // 
            // comboBoxShieldA2
            // 
            this.comboBoxShieldA2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxShieldA2.FormattingEnabled = true;
            this.comboBoxShieldA2.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3"});
            this.comboBoxShieldA2.Location = new System.Drawing.Point(50, 32);
            this.comboBoxShieldA2.Name = "comboBoxShieldA2";
            this.comboBoxShieldA2.Size = new System.Drawing.Size(34, 20);
            this.comboBoxShieldA2.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(19, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "A1";
            // 
            // comboBoxShieldA1
            // 
            this.comboBoxShieldA1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxShieldA1.FormattingEnabled = true;
            this.comboBoxShieldA1.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3"});
            this.comboBoxShieldA1.Location = new System.Drawing.Point(6, 32);
            this.comboBoxShieldA1.Name = "comboBoxShieldA1";
            this.comboBoxShieldA1.Size = new System.Drawing.Size(34, 20);
            this.comboBoxShieldA1.TabIndex = 0;
            // 
            // checkBoxRecievePresent
            // 
            this.checkBoxRecievePresent.AutoSize = true;
            this.checkBoxRecievePresent.Location = new System.Drawing.Point(14, 255);
            this.checkBoxRecievePresent.Name = "checkBoxRecievePresent";
            this.checkBoxRecievePresent.Size = new System.Drawing.Size(121, 16);
            this.checkBoxRecievePresent.TabIndex = 28;
            this.checkBoxRecievePresent.Text = "プレゼントを受け取る";
            this.checkBoxRecievePresent.UseVisualStyleBackColor = true;
            // 
            // checkBoxOnlySearch
            // 
            this.checkBoxOnlySearch.AutoSize = true;
            this.checkBoxOnlySearch.Location = new System.Drawing.Point(14, 207);
            this.checkBoxOnlySearch.Name = "checkBoxOnlySearch";
            this.checkBoxOnlySearch.Size = new System.Drawing.Size(69, 16);
            this.checkBoxOnlySearch.TabIndex = 26;
            this.checkBoxOnlySearch.Text = "探索のみ";
            this.checkBoxOnlySearch.UseVisualStyleBackColor = true;
            // 
            // checkBoxRecieveReword
            // 
            this.checkBoxRecieveReword.AutoSize = true;
            this.checkBoxRecieveReword.Location = new System.Drawing.Point(14, 231);
            this.checkBoxRecieveReword.Name = "checkBoxRecieveReword";
            this.checkBoxRecieveReword.Size = new System.Drawing.Size(100, 16);
            this.checkBoxRecieveReword.TabIndex = 24;
            this.checkBoxRecieveReword.Text = "報酬を受け取る";
            this.checkBoxRecieveReword.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(334, 39);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(94, 12);
            this.label8.TabIndex = 23;
            this.label8.Text = "体以下で探索する";
            // 
            // textBoxEnemyCount
            // 
            this.textBoxEnemyCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxEnemyCount.Location = new System.Drawing.Point(14, 37);
            this.textBoxEnemyCount.Name = "textBoxEnemyCount";
            this.textBoxEnemyCount.Size = new System.Drawing.Size(312, 19);
            this.textBoxEnemyCount.TabIndex = 22;
            this.textBoxEnemyCount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxEnemyCount_KeyPress);
            this.textBoxEnemyCount.Validated += new System.EventHandler(this.textBoxEnemyCount_Validated);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 19);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 12);
            this.label7.TabIndex = 21;
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
            this.comboBoxAttackMode.Location = new System.Drawing.Point(14, 133);
            this.comboBoxAttackMode.Name = "comboBoxAttackMode";
            this.comboBoxAttackMode.Size = new System.Drawing.Size(414, 20);
            this.comboBoxAttackMode.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 114);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 12);
            this.label4.TabIndex = 14;
            this.label4.Text = "攻撃モード";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 279);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(141, 12);
            this.label3.TabIndex = 13;
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
            this.comboBoxRecieve.Location = new System.Drawing.Point(14, 299);
            this.comboBoxRecieve.Name = "comboBoxRecieve";
            this.comboBoxRecieve.Size = new System.Drawing.Size(414, 20);
            this.comboBoxRecieve.TabIndex = 12;
            // 
            // textBoxBaseDamage
            // 
            this.textBoxBaseDamage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxBaseDamage.Location = new System.Drawing.Point(14, 180);
            this.textBoxBaseDamage.Name = "textBoxBaseDamage";
            this.textBoxBaseDamage.Size = new System.Drawing.Size(414, 19);
            this.textBoxBaseDamage.TabIndex = 9;
            this.textBoxBaseDamage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxBaseDamage_KeyPress);
            this.textBoxBaseDamage.Validated += new System.EventHandler(this.textBoxBaseDamage_Validated);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 161);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "BEx1使用時のダメージ";
            // 
            // labelStateHome
            // 
            this.labelStateHome.BackColor = System.Drawing.Color.White;
            this.labelStateHome.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelStateHome.Location = new System.Drawing.Point(12, 19);
            this.labelStateHome.Name = "labelStateHome";
            this.labelStateHome.Size = new System.Drawing.Size(85, 46);
            this.labelStateHome.TabIndex = 7;
            this.labelStateHome.Text = "ホーム";
            this.labelStateHome.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelStateSearch
            // 
            this.labelStateSearch.BackColor = System.Drawing.Color.White;
            this.labelStateSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelStateSearch.Location = new System.Drawing.Point(12, 62);
            this.labelStateSearch.Name = "labelStateSearch";
            this.labelStateSearch.Size = new System.Drawing.Size(85, 46);
            this.labelStateSearch.TabIndex = 9;
            this.labelStateSearch.Text = "探索";
            this.labelStateSearch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelStateEnemyList
            // 
            this.labelStateEnemyList.BackColor = System.Drawing.Color.White;
            this.labelStateEnemyList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelStateEnemyList.Location = new System.Drawing.Point(95, 62);
            this.labelStateEnemyList.Name = "labelStateEnemyList";
            this.labelStateEnemyList.Size = new System.Drawing.Size(85, 46);
            this.labelStateEnemyList.TabIndex = 10;
            this.labelStateEnemyList.Text = "敵一覧";
            this.labelStateEnemyList.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelStateBattle
            // 
            this.labelStateBattle.BackColor = System.Drawing.Color.White;
            this.labelStateBattle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelStateBattle.Location = new System.Drawing.Point(12, 105);
            this.labelStateBattle.Name = "labelStateBattle";
            this.labelStateBattle.Size = new System.Drawing.Size(85, 46);
            this.labelStateBattle.TabIndex = 11;
            this.labelStateBattle.Text = "戦闘画面";
            this.labelStateBattle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelStateBattleFlash
            // 
            this.labelStateBattleFlash.BackColor = System.Drawing.Color.White;
            this.labelStateBattleFlash.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelStateBattleFlash.Location = new System.Drawing.Point(95, 105);
            this.labelStateBattleFlash.Name = "labelStateBattleFlash";
            this.labelStateBattleFlash.Size = new System.Drawing.Size(85, 46);
            this.labelStateBattleFlash.TabIndex = 12;
            this.labelStateBattleFlash.Text = "戦闘";
            this.labelStateBattleFlash.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelStateResult
            // 
            this.labelStateResult.BackColor = System.Drawing.Color.White;
            this.labelStateResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelStateResult.Location = new System.Drawing.Point(12, 149);
            this.labelStateResult.Name = "labelStateResult";
            this.labelStateResult.Size = new System.Drawing.Size(85, 46);
            this.labelStateResult.TabIndex = 13;
            this.labelStateResult.Text = "リザルト";
            this.labelStateResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelStateReceive
            // 
            this.labelStateReceive.BackColor = System.Drawing.Color.White;
            this.labelStateReceive.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelStateReceive.Location = new System.Drawing.Point(96, 149);
            this.labelStateReceive.Name = "labelStateReceive";
            this.labelStateReceive.Size = new System.Drawing.Size(85, 46);
            this.labelStateReceive.TabIndex = 14;
            this.labelStateReceive.Text = "報酬受取";
            this.labelStateReceive.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelStatePresentList
            // 
            this.labelStatePresentList.BackColor = System.Drawing.Color.White;
            this.labelStatePresentList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelStatePresentList.Location = new System.Drawing.Point(12, 194);
            this.labelStatePresentList.Name = "labelStatePresentList";
            this.labelStatePresentList.Size = new System.Drawing.Size(85, 46);
            this.labelStatePresentList.TabIndex = 15;
            this.labelStatePresentList.Text = "プレゼント一覧";
            this.labelStatePresentList.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelStateError
            // 
            this.labelStateError.BackColor = System.Drawing.Color.White;
            this.labelStateError.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelStateError.Location = new System.Drawing.Point(95, 239);
            this.labelStateError.Name = "labelStateError";
            this.labelStateError.Size = new System.Drawing.Size(86, 46);
            this.labelStateError.TabIndex = 18;
            this.labelStateError.Text = "不正な画面遷移";
            this.labelStateError.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelStateFightAlreadyFinished
            // 
            this.labelStateFightAlreadyFinished.BackColor = System.Drawing.Color.White;
            this.labelStateFightAlreadyFinished.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelStateFightAlreadyFinished.Location = new System.Drawing.Point(12, 284);
            this.labelStateFightAlreadyFinished.Name = "labelStateFightAlreadyFinished";
            this.labelStateFightAlreadyFinished.Size = new System.Drawing.Size(85, 46);
            this.labelStateFightAlreadyFinished.TabIndex = 19;
            this.labelStateFightAlreadyFinished.Text = "既に戦闘は終了しています";
            this.labelStateFightAlreadyFinished.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelStateAccessBlock
            // 
            this.labelStateAccessBlock.BackColor = System.Drawing.Color.White;
            this.labelStateAccessBlock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelStateAccessBlock.Location = new System.Drawing.Point(96, 284);
            this.labelStateAccessBlock.Name = "labelStateAccessBlock";
            this.labelStateAccessBlock.Size = new System.Drawing.Size(85, 46);
            this.labelStateAccessBlock.TabIndex = 20;
            this.labelStateAccessBlock.Text = "アクセス制限";
            this.labelStateAccessBlock.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelStateEventFinished
            // 
            this.labelStateEventFinished.BackColor = System.Drawing.Color.White;
            this.labelStateEventFinished.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelStateEventFinished.Location = new System.Drawing.Point(12, 329);
            this.labelStateEventFinished.Name = "labelStateEventFinished";
            this.labelStateEventFinished.Size = new System.Drawing.Size(85, 46);
            this.labelStateEventFinished.TabIndex = 21;
            this.labelStateEventFinished.Text = "イベント終了";
            this.labelStateEventFinished.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelStateUnknown
            // 
            this.labelStateUnknown.BackColor = System.Drawing.Color.White;
            this.labelStateUnknown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelStateUnknown.Location = new System.Drawing.Point(96, 329);
            this.labelStateUnknown.Name = "labelStateUnknown";
            this.labelStateUnknown.Size = new System.Drawing.Size(85, 46);
            this.labelStateUnknown.TabIndex = 22;
            this.labelStateUnknown.Text = "不明な画面";
            this.labelStateUnknown.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.labelStateInterval);
            this.groupBox2.Controls.Add(this.labelStateGetCard);
            this.groupBox2.Controls.Add(this.labelStateHome);
            this.groupBox2.Controls.Add(this.labelStateLevelUp);
            this.groupBox2.Controls.Add(this.labelStateUnknown);
            this.groupBox2.Controls.Add(this.labelStateEventFinished);
            this.groupBox2.Controls.Add(this.labelStateSearch);
            this.groupBox2.Controls.Add(this.labelStateAccessBlock);
            this.groupBox2.Controls.Add(this.labelStateEnemyList);
            this.groupBox2.Controls.Add(this.labelStateFightAlreadyFinished);
            this.groupBox2.Controls.Add(this.labelStateBattle);
            this.groupBox2.Controls.Add(this.labelStateError);
            this.groupBox2.Controls.Add(this.labelStateBattleFlash);
            this.groupBox2.Controls.Add(this.labelStateResult);
            this.groupBox2.Controls.Add(this.labelStateReceive);
            this.groupBox2.Controls.Add(this.labelStatePresentList);
            this.groupBox2.Location = new System.Drawing.Point(464, 99);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(190, 584);
            this.groupBox2.TabIndex = 23;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "画面遷移";
            // 
            // labelStateInterval
            // 
            this.labelStateInterval.BackColor = System.Drawing.Color.White;
            this.labelStateInterval.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelStateInterval.Location = new System.Drawing.Point(96, 19);
            this.labelStateInterval.Name = "labelStateInterval";
            this.labelStateInterval.Size = new System.Drawing.Size(85, 46);
            this.labelStateInterval.TabIndex = 25;
            this.labelStateInterval.Text = "インターバル";
            this.labelStateInterval.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelStateGetCard
            // 
            this.labelStateGetCard.BackColor = System.Drawing.Color.White;
            this.labelStateGetCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelStateGetCard.Location = new System.Drawing.Point(12, 239);
            this.labelStateGetCard.Name = "labelStateGetCard";
            this.labelStateGetCard.Size = new System.Drawing.Size(85, 46);
            this.labelStateGetCard.TabIndex = 24;
            this.labelStateGetCard.Text = "カード入手";
            this.labelStateGetCard.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelStateLevelUp
            // 
            this.labelStateLevelUp.BackColor = System.Drawing.Color.White;
            this.labelStateLevelUp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelStateLevelUp.Location = new System.Drawing.Point(96, 194);
            this.labelStateLevelUp.Name = "labelStateLevelUp";
            this.labelStateLevelUp.Size = new System.Drawing.Size(85, 46);
            this.labelStateLevelUp.TabIndex = 23;
            this.labelStateLevelUp.Text = "レベルアップ";
            this.labelStateLevelUp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelMiniCap
            // 
            this.labelMiniCap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelMiniCap.AutoSize = true;
            this.labelMiniCap.Location = new System.Drawing.Point(462, 50);
            this.labelMiniCap.Name = "labelMiniCap";
            this.labelMiniCap.Size = new System.Drawing.Size(45, 12);
            this.labelMiniCap.TabIndex = 24;
            this.labelMiniCap.Text = "ミニカプ：";
            // 
            // labelArea
            // 
            this.labelArea.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelArea.AutoSize = true;
            this.labelArea.Location = new System.Drawing.Point(462, 74);
            this.labelArea.Name = "labelArea";
            this.labelArea.Size = new System.Drawing.Size(60, 12);
            this.labelArea.TabIndex = 25;
            this.labelArea.Text = "現在エリア：";
            // 
            // label21
            // 
            this.label21.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(207, 469);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(17, 12);
            this.label21.TabIndex = 41;
            this.label21.Text = "秒";
            // 
            // textBoxWaitForce
            // 
            this.textBoxWaitForce.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxWaitForce.Location = new System.Drawing.Point(14, 466);
            this.textBoxWaitForce.Name = "textBoxWaitForce";
            this.textBoxWaitForce.Size = new System.Drawing.Size(187, 19);
            this.textBoxWaitForce.TabIndex = 40;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(14, 450);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(166, 12);
            this.label22.TabIndex = 39;
            this.label22.Text = "戦略拠点のフォース使用前のWait";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(103, 87);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(17, 12);
            this.label23.TabIndex = 45;
            this.label23.Text = "～";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(14, 64);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(53, 12);
            this.label24.TabIndex = 44;
            this.label24.Text = "稼働時間";
            // 
            // dateTimePickerTimeEnd
            // 
            this.dateTimePickerTimeEnd.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePickerTimeEnd.Location = new System.Drawing.Point(127, 84);
            this.dateTimePickerTimeEnd.Name = "dateTimePickerTimeEnd";
            this.dateTimePickerTimeEnd.ShowUpDown = true;
            this.dateTimePickerTimeEnd.Size = new System.Drawing.Size(78, 19);
            this.dateTimePickerTimeEnd.TabIndex = 43;
            // 
            // dateTimePickerTimeStart
            // 
            this.dateTimePickerTimeStart.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePickerTimeStart.Location = new System.Drawing.Point(14, 84);
            this.dateTimePickerTimeStart.Name = "dateTimePickerTimeStart";
            this.dateTimePickerTimeStart.ShowUpDown = true;
            this.dateTimePickerTimeStart.Size = new System.Drawing.Size(78, 19);
            this.dateTimePickerTimeStart.TabIndex = 42;
            // 
            // TabControlGTactics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelArea);
            this.Controls.Add(this.labelMiniCap);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxURL);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonStart);
            this.Name = "TabControlGTactics";
            this.Size = new System.Drawing.Size(671, 702);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
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
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxRecieve;
        private System.Windows.Forms.ComboBox comboBoxAttackMode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxEnemyCount;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label labelStateHome;
        private System.Windows.Forms.Label labelStateSearch;
        private System.Windows.Forms.Label labelStateEnemyList;
        private System.Windows.Forms.Label labelStateBattle;
        private System.Windows.Forms.Label labelStateBattleFlash;
        private System.Windows.Forms.Label labelStateResult;
        private System.Windows.Forms.Label labelStateReceive;
        private System.Windows.Forms.Label labelStatePresentList;
        private System.Windows.Forms.Label labelStateError;
        private System.Windows.Forms.Label labelStateFightAlreadyFinished;
        private System.Windows.Forms.Label labelStateAccessBlock;
        private System.Windows.Forms.Label labelStateEventFinished;
        private System.Windows.Forms.Label labelStateUnknown;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label labelMiniCap;
        private System.Windows.Forms.Label labelStateGetCard;
        private System.Windows.Forms.Label labelStateLevelUp;
        private System.Windows.Forms.CheckBox checkBoxRecieveReword;
        private System.Windows.Forms.CheckBox checkBoxOnlySearch;
        private System.Windows.Forms.Label labelStateInterval;
        private System.Windows.Forms.CheckBox checkBoxRecievePresent;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboBoxShieldA3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBoxShieldA2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxShieldA1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox comboBoxShieldC3;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox comboBoxShieldC2;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox comboBoxShieldC1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox comboBoxShieldB3;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox comboBoxShieldB2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox comboBoxShieldB1;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox comboBoxStrategicArea;
        private System.Windows.Forms.CheckBox checkBoxForceCharge;
        private System.Windows.Forms.CheckBox checkBoxUseForce;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox comboBoxForcePattern;
        private System.Windows.Forms.Label labelArea;
        private System.Windows.Forms.ComboBox comboBoxPriority;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox textBoxPointDiff;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.CheckBox checkBoxStandby;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox textBoxWaitForce;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.DateTimePicker dateTimePickerTimeEnd;
        private System.Windows.Forms.DateTimePicker dateTimePickerTimeStart;
    }
}
