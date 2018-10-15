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
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxWaitAtackBattleShip = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxWaitRecieveAssult = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.checkBoxOnlyAttackAssultBoss = new System.Windows.Forms.CheckBox();
            this.checkBoxRecievePresent = new System.Windows.Forms.CheckBox();
            this.checkBoxOnlySearch = new System.Windows.Forms.CheckBox();
            this.checkBoxRecieveReword = new System.Windows.Forms.CheckBox();
            this.checkBoxAimMVP = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxEnemyCount = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBoxRecieve = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxAttackMode = new System.Windows.Forms.ComboBox();
            this.textBoxBaseDamage = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBoxJoinAssault = new System.Windows.Forms.CheckBox();
            this.checkBoxRequest = new System.Windows.Forms.CheckBox();
            this.checkBoxUseAssaultBE = new System.Windows.Forms.CheckBox();
            this.labelMiniCap = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.labelStateAssaultOperationWin = new System.Windows.Forms.Label();
            this.labelStateAssaultOperationRequestComplete = new System.Windows.Forms.Label();
            this.labelStateAssaultOperationRequestSubmit = new System.Windows.Forms.Label();
            this.labelStateGetCard = new System.Windows.Forms.Label();
            this.labelStateHome = new System.Windows.Forms.Label();
            this.labelStateLevelUp = new System.Windows.Forms.Label();
            this.labelStateUnknown = new System.Windows.Forms.Label();
            this.labelStateAssaultOperationHome = new System.Windows.Forms.Label();
            this.labelStateEventFinished = new System.Windows.Forms.Label();
            this.labelStateSearch = new System.Windows.Forms.Label();
            this.labelStateAccessBlock = new System.Windows.Forms.Label();
            this.labelStateEnemyList = new System.Windows.Forms.Label();
            this.labelStateFightAlreadyFinished = new System.Windows.Forms.Label();
            this.labelStateBattle = new System.Windows.Forms.Label();
            this.labelStateError = new System.Windows.Forms.Label();
            this.labelStateBattleFlash = new System.Windows.Forms.Label();
            this.labelStateAssaultOperationRequest = new System.Windows.Forms.Label();
            this.labelStateResult = new System.Windows.Forms.Label();
            this.labelStateBattleAssaultOperation = new System.Windows.Forms.Label();
            this.labelStateReceive = new System.Windows.Forms.Label();
            this.labelStatePresentList = new System.Windows.Forms.Label();
            this.labelStateAssaultOperationRequestFaild = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonStart
            // 
            this.buttonStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStart.Location = new System.Drawing.Point(363, 562);
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
            this.buttonStop.Location = new System.Drawing.Point(450, 562);
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
            this.textBoxURL.Size = new System.Drawing.Size(589, 19);
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
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.textBoxWaitAtackBattleShip);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.textBoxWaitRecieveAssult);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.checkBoxOnlyAttackAssultBoss);
            this.groupBox1.Controls.Add(this.checkBoxRecievePresent);
            this.groupBox1.Controls.Add(this.checkBoxOnlySearch);
            this.groupBox1.Controls.Add(this.checkBoxRecieveReword);
            this.groupBox1.Controls.Add(this.checkBoxAimMVP);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.textBoxEnemyCount);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.comboBoxRecieve);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.comboBoxAttackMode);
            this.groupBox1.Controls.Add(this.textBoxBaseDamage);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.checkBoxJoinAssault);
            this.groupBox1.Controls.Add(this.checkBoxRequest);
            this.groupBox1.Controls.Add(this.checkBoxUseAssaultBE);
            this.groupBox1.Location = new System.Drawing.Point(19, 41);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(506, 506);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "オプション";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(470, 399);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(17, 12);
            this.label9.TabIndex = 41;
            this.label9.Text = "秒";
            // 
            // textBoxWaitAtackBattleShip
            // 
            this.textBoxWaitAtackBattleShip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxWaitAtackBattleShip.Location = new System.Drawing.Point(10, 394);
            this.textBoxWaitAtackBattleShip.Name = "textBoxWaitAtackBattleShip";
            this.textBoxWaitAtackBattleShip.Size = new System.Drawing.Size(454, 19);
            this.textBoxWaitAtackBattleShip.TabIndex = 40;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(10, 376);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(155, 12);
            this.label10.TabIndex = 39;
            this.label10.Text = "強襲作戦の戦艦攻撃時のWait";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(470, 440);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 12);
            this.label5.TabIndex = 38;
            this.label5.Text = "秒";
            // 
            // textBoxWaitRecieveAssult
            // 
            this.textBoxWaitRecieveAssult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxWaitRecieveAssult.Location = new System.Drawing.Point(10, 437);
            this.textBoxWaitRecieveAssult.Name = "textBoxWaitRecieveAssult";
            this.textBoxWaitRecieveAssult.Size = new System.Drawing.Size(454, 19);
            this.textBoxWaitRecieveAssult.TabIndex = 37;
            this.textBoxWaitRecieveAssult.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxWaitRecieveAssult_KeyPress);
            this.textBoxWaitRecieveAssult.Validated += new System.EventHandler(this.textBoxWaitRecieveAssult_Validated);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 419);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(173, 12);
            this.label6.TabIndex = 36;
            this.label6.Text = "強襲作戦の報酬受け取り時のWait";
            // 
            // checkBoxOnlyAttackAssultBoss
            // 
            this.checkBoxOnlyAttackAssultBoss.AutoSize = true;
            this.checkBoxOnlyAttackAssultBoss.Location = new System.Drawing.Point(10, 354);
            this.checkBoxOnlyAttackAssultBoss.Name = "checkBoxOnlyAttackAssultBoss";
            this.checkBoxOnlyAttackAssultBoss.Size = new System.Drawing.Size(199, 16);
            this.checkBoxOnlyAttackAssultBoss.TabIndex = 35;
            this.checkBoxOnlyAttackAssultBoss.Text = "強襲作戦中は上のボスのみ攻撃する";
            this.checkBoxOnlyAttackAssultBoss.UseVisualStyleBackColor = true;
            // 
            // checkBoxRecievePresent
            // 
            this.checkBoxRecievePresent.AutoSize = true;
            this.checkBoxRecievePresent.Location = new System.Drawing.Point(10, 197);
            this.checkBoxRecievePresent.Name = "checkBoxRecievePresent";
            this.checkBoxRecievePresent.Size = new System.Drawing.Size(121, 16);
            this.checkBoxRecievePresent.TabIndex = 34;
            this.checkBoxRecievePresent.Text = "プレゼントを受け取る";
            this.checkBoxRecievePresent.UseVisualStyleBackColor = true;
            // 
            // checkBoxOnlySearch
            // 
            this.checkBoxOnlySearch.AutoSize = true;
            this.checkBoxOnlySearch.Location = new System.Drawing.Point(10, 151);
            this.checkBoxOnlySearch.Name = "checkBoxOnlySearch";
            this.checkBoxOnlySearch.Size = new System.Drawing.Size(69, 16);
            this.checkBoxOnlySearch.TabIndex = 33;
            this.checkBoxOnlySearch.Text = "探索のみ";
            this.checkBoxOnlySearch.UseVisualStyleBackColor = true;
            // 
            // checkBoxRecieveReword
            // 
            this.checkBoxRecieveReword.AutoSize = true;
            this.checkBoxRecieveReword.Location = new System.Drawing.Point(10, 174);
            this.checkBoxRecieveReword.Name = "checkBoxRecieveReword";
            this.checkBoxRecieveReword.Size = new System.Drawing.Size(100, 16);
            this.checkBoxRecieveReword.TabIndex = 32;
            this.checkBoxRecieveReword.Text = "報酬を受け取る";
            this.checkBoxRecieveReword.UseVisualStyleBackColor = true;
            // 
            // checkBoxAimMVP
            // 
            this.checkBoxAimMVP.AutoSize = true;
            this.checkBoxAimMVP.Location = new System.Drawing.Point(10, 288);
            this.checkBoxAimMVP.Name = "checkBoxAimMVP";
            this.checkBoxAimMVP.Size = new System.Drawing.Size(142, 16);
            this.checkBoxAimMVP.TabIndex = 31;
            this.checkBoxAimMVP.Text = "MVPを狙う(レアボスのみ)";
            this.checkBoxAimMVP.UseVisualStyleBackColor = true;
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
            this.textBoxEnemyCount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxEnemyCount_KeyPress);
            this.textBoxEnemyCount.Validated += new System.EventHandler(this.textBoxEnemyCount_Validated);
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
            this.comboBoxRecieve.Location = new System.Drawing.Point(10, 239);
            this.comboBoxRecieve.Name = "comboBoxRecieve";
            this.comboBoxRecieve.Size = new System.Drawing.Size(479, 20);
            this.comboBoxRecieve.TabIndex = 27;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 12);
            this.label4.TabIndex = 26;
            this.label4.Text = "攻撃モード";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 220);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(141, 12);
            this.label3.TabIndex = 25;
            this.label3.Text = "報酬をまとめて受け取る個数";
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
            this.comboBoxAttackMode.TabIndex = 24;
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
            this.textBoxBaseDamage.Validated += new System.EventHandler(this.textBoxBaseDamage_Validated);
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
            // checkBoxJoinAssault
            // 
            this.checkBoxJoinAssault.AutoSize = true;
            this.checkBoxJoinAssault.Location = new System.Drawing.Point(10, 310);
            this.checkBoxJoinAssault.Name = "checkBoxJoinAssault";
            this.checkBoxJoinAssault.Size = new System.Drawing.Size(124, 16);
            this.checkBoxJoinAssault.TabIndex = 0;
            this.checkBoxJoinAssault.Text = "強襲作戦に参加する";
            this.checkBoxJoinAssault.UseVisualStyleBackColor = true;
            this.checkBoxJoinAssault.CheckedChanged += new System.EventHandler(this.checkBoxJoinAssault_CheckedChanged);
            // 
            // checkBoxRequest
            // 
            this.checkBoxRequest.AutoSize = true;
            this.checkBoxRequest.Location = new System.Drawing.Point(10, 265);
            this.checkBoxRequest.Name = "checkBoxRequest";
            this.checkBoxRequest.Size = new System.Drawing.Size(103, 16);
            this.checkBoxRequest.TabIndex = 2;
            this.checkBoxRequest.Text = "応援依頼を出す";
            this.checkBoxRequest.UseVisualStyleBackColor = true;
            this.checkBoxRequest.CheckedChanged += new System.EventHandler(this.checkBoxRequest_CheckedChanged);
            // 
            // checkBoxUseAssaultBE
            // 
            this.checkBoxUseAssaultBE.AutoSize = true;
            this.checkBoxUseAssaultBE.Location = new System.Drawing.Point(10, 332);
            this.checkBoxUseAssaultBE.Name = "checkBoxUseAssaultBE";
            this.checkBoxUseAssaultBE.Size = new System.Drawing.Size(132, 16);
            this.checkBoxUseAssaultBE.TabIndex = 1;
            this.checkBoxUseAssaultBE.Text = "強襲専用BEのみ使用";
            this.checkBoxUseAssaultBE.UseVisualStyleBackColor = true;
            this.checkBoxUseAssaultBE.CheckedChanged += new System.EventHandler(this.checkBoxUseAssaultBE_CheckedChanged);
            // 
            // labelMiniCap
            // 
            this.labelMiniCap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelMiniCap.AutoSize = true;
            this.labelMiniCap.Location = new System.Drawing.Point(534, 50);
            this.labelMiniCap.Name = "labelMiniCap";
            this.labelMiniCap.Size = new System.Drawing.Size(45, 12);
            this.labelMiniCap.TabIndex = 26;
            this.labelMiniCap.Text = "ミニカプ：";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.labelStateLevelUp);
            this.groupBox2.Controls.Add(this.labelStateAssaultOperationRequestFaild);
            this.groupBox2.Controls.Add(this.labelStateAssaultOperationWin);
            this.groupBox2.Controls.Add(this.labelStateAssaultOperationRequestComplete);
            this.groupBox2.Controls.Add(this.labelStateAssaultOperationRequestSubmit);
            this.groupBox2.Controls.Add(this.labelStateGetCard);
            this.groupBox2.Controls.Add(this.labelStateHome);
            this.groupBox2.Controls.Add(this.labelStateUnknown);
            this.groupBox2.Controls.Add(this.labelStateAssaultOperationHome);
            this.groupBox2.Controls.Add(this.labelStateEventFinished);
            this.groupBox2.Controls.Add(this.labelStateSearch);
            this.groupBox2.Controls.Add(this.labelStateAccessBlock);
            this.groupBox2.Controls.Add(this.labelStateEnemyList);
            this.groupBox2.Controls.Add(this.labelStateFightAlreadyFinished);
            this.groupBox2.Controls.Add(this.labelStateBattle);
            this.groupBox2.Controls.Add(this.labelStateError);
            this.groupBox2.Controls.Add(this.labelStateBattleFlash);
            this.groupBox2.Controls.Add(this.labelStateAssaultOperationRequest);
            this.groupBox2.Controls.Add(this.labelStateResult);
            this.groupBox2.Controls.Add(this.labelStateBattleAssaultOperation);
            this.groupBox2.Controls.Add(this.labelStateReceive);
            this.groupBox2.Controls.Add(this.labelStatePresentList);
            this.groupBox2.Location = new System.Drawing.Point(536, 75);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(190, 529);
            this.groupBox2.TabIndex = 25;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "画面遷移";
            // 
            // labelStateAssaultOperationWin
            // 
            this.labelStateAssaultOperationWin.BackColor = System.Drawing.Color.White;
            this.labelStateAssaultOperationWin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelStateAssaultOperationWin.Location = new System.Drawing.Point(96, 285);
            this.labelStateAssaultOperationWin.Name = "labelStateAssaultOperationWin";
            this.labelStateAssaultOperationWin.Size = new System.Drawing.Size(85, 46);
            this.labelStateAssaultOperationWin.TabIndex = 27;
            this.labelStateAssaultOperationWin.Text = "強襲作戦終了";
            this.labelStateAssaultOperationWin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelStateAssaultOperationRequestComplete
            // 
            this.labelStateAssaultOperationRequestComplete.BackColor = System.Drawing.Color.White;
            this.labelStateAssaultOperationRequestComplete.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelStateAssaultOperationRequestComplete.Location = new System.Drawing.Point(12, 285);
            this.labelStateAssaultOperationRequestComplete.Name = "labelStateAssaultOperationRequestComplete";
            this.labelStateAssaultOperationRequestComplete.Size = new System.Drawing.Size(85, 46);
            this.labelStateAssaultOperationRequestComplete.TabIndex = 26;
            this.labelStateAssaultOperationRequestComplete.Text = "強襲作戦参加完了";
            this.labelStateAssaultOperationRequestComplete.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelStateAssaultOperationRequestSubmit
            // 
            this.labelStateAssaultOperationRequestSubmit.BackColor = System.Drawing.Color.White;
            this.labelStateAssaultOperationRequestSubmit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelStateAssaultOperationRequestSubmit.Location = new System.Drawing.Point(96, 244);
            this.labelStateAssaultOperationRequestSubmit.Name = "labelStateAssaultOperationRequestSubmit";
            this.labelStateAssaultOperationRequestSubmit.Size = new System.Drawing.Size(85, 46);
            this.labelStateAssaultOperationRequestSubmit.TabIndex = 25;
            this.labelStateAssaultOperationRequestSubmit.Text = "強襲作戦参加";
            this.labelStateAssaultOperationRequestSubmit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelStateGetCard
            // 
            this.labelStateGetCard.BackColor = System.Drawing.Color.White;
            this.labelStateGetCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelStateGetCard.Location = new System.Drawing.Point(12, 372);
            this.labelStateGetCard.Name = "labelStateGetCard";
            this.labelStateGetCard.Size = new System.Drawing.Size(85, 45);
            this.labelStateGetCard.TabIndex = 24;
            this.labelStateGetCard.Text = "カード入手";
            this.labelStateGetCard.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            // labelStateLevelUp
            // 
            this.labelStateLevelUp.BackColor = System.Drawing.Color.White;
            this.labelStateLevelUp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelStateLevelUp.Location = new System.Drawing.Point(96, 327);
            this.labelStateLevelUp.Name = "labelStateLevelUp";
            this.labelStateLevelUp.Size = new System.Drawing.Size(85, 46);
            this.labelStateLevelUp.TabIndex = 23;
            this.labelStateLevelUp.Text = "レベルアップ";
            this.labelStateLevelUp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelStateUnknown
            // 
            this.labelStateUnknown.BackColor = System.Drawing.Color.White;
            this.labelStateUnknown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelStateUnknown.Location = new System.Drawing.Point(96, 459);
            this.labelStateUnknown.Name = "labelStateUnknown";
            this.labelStateUnknown.Size = new System.Drawing.Size(85, 46);
            this.labelStateUnknown.TabIndex = 22;
            this.labelStateUnknown.Text = "不明な画面";
            this.labelStateUnknown.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelStateAssaultOperationHome
            // 
            this.labelStateAssaultOperationHome.BackColor = System.Drawing.Color.White;
            this.labelStateAssaultOperationHome.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelStateAssaultOperationHome.Location = new System.Drawing.Point(12, 199);
            this.labelStateAssaultOperationHome.Name = "labelStateAssaultOperationHome";
            this.labelStateAssaultOperationHome.Size = new System.Drawing.Size(85, 46);
            this.labelStateAssaultOperationHome.TabIndex = 8;
            this.labelStateAssaultOperationHome.Text = "強襲作戦ホーム";
            this.labelStateAssaultOperationHome.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelStateEventFinished
            // 
            this.labelStateEventFinished.BackColor = System.Drawing.Color.White;
            this.labelStateEventFinished.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelStateEventFinished.Location = new System.Drawing.Point(12, 459);
            this.labelStateEventFinished.Name = "labelStateEventFinished";
            this.labelStateEventFinished.Size = new System.Drawing.Size(85, 46);
            this.labelStateEventFinished.TabIndex = 21;
            this.labelStateEventFinished.Text = "イベント終了";
            this.labelStateEventFinished.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelStateSearch
            // 
            this.labelStateSearch.BackColor = System.Drawing.Color.White;
            this.labelStateSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelStateSearch.Location = new System.Drawing.Point(96, 19);
            this.labelStateSearch.Name = "labelStateSearch";
            this.labelStateSearch.Size = new System.Drawing.Size(85, 46);
            this.labelStateSearch.TabIndex = 9;
            this.labelStateSearch.Text = "探索";
            this.labelStateSearch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelStateAccessBlock
            // 
            this.labelStateAccessBlock.BackColor = System.Drawing.Color.White;
            this.labelStateAccessBlock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelStateAccessBlock.Location = new System.Drawing.Point(96, 416);
            this.labelStateAccessBlock.Name = "labelStateAccessBlock";
            this.labelStateAccessBlock.Size = new System.Drawing.Size(85, 46);
            this.labelStateAccessBlock.TabIndex = 20;
            this.labelStateAccessBlock.Text = "アクセス制限";
            this.labelStateAccessBlock.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelStateEnemyList
            // 
            this.labelStateEnemyList.BackColor = System.Drawing.Color.White;
            this.labelStateEnemyList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelStateEnemyList.Location = new System.Drawing.Point(12, 64);
            this.labelStateEnemyList.Name = "labelStateEnemyList";
            this.labelStateEnemyList.Size = new System.Drawing.Size(85, 46);
            this.labelStateEnemyList.TabIndex = 10;
            this.labelStateEnemyList.Text = "敵一覧";
            this.labelStateEnemyList.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelStateFightAlreadyFinished
            // 
            this.labelStateFightAlreadyFinished.BackColor = System.Drawing.Color.White;
            this.labelStateFightAlreadyFinished.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelStateFightAlreadyFinished.Location = new System.Drawing.Point(12, 414);
            this.labelStateFightAlreadyFinished.Name = "labelStateFightAlreadyFinished";
            this.labelStateFightAlreadyFinished.Size = new System.Drawing.Size(85, 46);
            this.labelStateFightAlreadyFinished.TabIndex = 19;
            this.labelStateFightAlreadyFinished.Text = "既に戦闘は終了しています";
            this.labelStateFightAlreadyFinished.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelStateBattle
            // 
            this.labelStateBattle.BackColor = System.Drawing.Color.White;
            this.labelStateBattle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelStateBattle.Location = new System.Drawing.Point(96, 63);
            this.labelStateBattle.Name = "labelStateBattle";
            this.labelStateBattle.Size = new System.Drawing.Size(85, 46);
            this.labelStateBattle.TabIndex = 11;
            this.labelStateBattle.Text = "戦闘画面";
            this.labelStateBattle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelStateError
            // 
            this.labelStateError.BackColor = System.Drawing.Color.White;
            this.labelStateError.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelStateError.Location = new System.Drawing.Point(96, 371);
            this.labelStateError.Name = "labelStateError";
            this.labelStateError.Size = new System.Drawing.Size(85, 46);
            this.labelStateError.TabIndex = 18;
            this.labelStateError.Text = "不正な画面遷移";
            this.labelStateError.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelStateBattleFlash
            // 
            this.labelStateBattleFlash.BackColor = System.Drawing.Color.White;
            this.labelStateBattleFlash.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelStateBattleFlash.Location = new System.Drawing.Point(12, 109);
            this.labelStateBattleFlash.Name = "labelStateBattleFlash";
            this.labelStateBattleFlash.Size = new System.Drawing.Size(85, 46);
            this.labelStateBattleFlash.TabIndex = 12;
            this.labelStateBattleFlash.Text = "戦闘";
            this.labelStateBattleFlash.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelStateAssaultOperationRequest
            // 
            this.labelStateAssaultOperationRequest.BackColor = System.Drawing.Color.White;
            this.labelStateAssaultOperationRequest.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelStateAssaultOperationRequest.Location = new System.Drawing.Point(12, 244);
            this.labelStateAssaultOperationRequest.Name = "labelStateAssaultOperationRequest";
            this.labelStateAssaultOperationRequest.Size = new System.Drawing.Size(85, 46);
            this.labelStateAssaultOperationRequest.TabIndex = 17;
            this.labelStateAssaultOperationRequest.Text = "強襲作戦参加依頼";
            this.labelStateAssaultOperationRequest.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelStateResult
            // 
            this.labelStateResult.BackColor = System.Drawing.Color.White;
            this.labelStateResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelStateResult.Location = new System.Drawing.Point(96, 108);
            this.labelStateResult.Name = "labelStateResult";
            this.labelStateResult.Size = new System.Drawing.Size(85, 47);
            this.labelStateResult.TabIndex = 13;
            this.labelStateResult.Text = "リザルト";
            this.labelStateResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelStateBattleAssaultOperation
            // 
            this.labelStateBattleAssaultOperation.BackColor = System.Drawing.Color.White;
            this.labelStateBattleAssaultOperation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelStateBattleAssaultOperation.Location = new System.Drawing.Point(96, 199);
            this.labelStateBattleAssaultOperation.Name = "labelStateBattleAssaultOperation";
            this.labelStateBattleAssaultOperation.Size = new System.Drawing.Size(85, 46);
            this.labelStateBattleAssaultOperation.TabIndex = 16;
            this.labelStateBattleAssaultOperation.Text = "強襲作戦戦闘画面";
            this.labelStateBattleAssaultOperation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelStateReceive
            // 
            this.labelStateReceive.BackColor = System.Drawing.Color.White;
            this.labelStateReceive.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelStateReceive.Location = new System.Drawing.Point(12, 154);
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
            this.labelStatePresentList.Location = new System.Drawing.Point(96, 154);
            this.labelStatePresentList.Name = "labelStatePresentList";
            this.labelStatePresentList.Size = new System.Drawing.Size(85, 46);
            this.labelStatePresentList.TabIndex = 15;
            this.labelStatePresentList.Text = "プレゼント一覧";
            this.labelStatePresentList.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelStateAssaultOperationRequestFaild
            // 
            this.labelStateAssaultOperationRequestFaild.BackColor = System.Drawing.Color.White;
            this.labelStateAssaultOperationRequestFaild.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelStateAssaultOperationRequestFaild.Location = new System.Drawing.Point(12, 327);
            this.labelStateAssaultOperationRequestFaild.Name = "labelStateAssaultOperationRequestFaild";
            this.labelStateAssaultOperationRequestFaild.Size = new System.Drawing.Size(85, 46);
            this.labelStateAssaultOperationRequestFaild.TabIndex = 28;
            this.labelStateAssaultOperationRequestFaild.Text = "強襲作戦エラー";
            this.labelStateAssaultOperationRequestFaild.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TabControlRaid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelMiniCap);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxURL);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonStart);
            this.Name = "TabControlRaid";
            this.Size = new System.Drawing.Size(743, 621);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxEnemyCount;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBoxRecieve;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxAttackMode;
        private System.Windows.Forms.CheckBox checkBoxAimMVP;
        private System.Windows.Forms.CheckBox checkBoxJoinAssault;
        private System.Windows.Forms.CheckBox checkBoxRequest;
        private System.Windows.Forms.CheckBox checkBoxUseAssaultBE;
        private System.Windows.Forms.Label labelMiniCap;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label labelStateGetCard;
        private System.Windows.Forms.Label labelStateHome;
        private System.Windows.Forms.Label labelStateLevelUp;
        private System.Windows.Forms.Label labelStateUnknown;
        private System.Windows.Forms.Label labelStateAssaultOperationHome;
        private System.Windows.Forms.Label labelStateEventFinished;
        private System.Windows.Forms.Label labelStateSearch;
        private System.Windows.Forms.Label labelStateAccessBlock;
        private System.Windows.Forms.Label labelStateEnemyList;
        private System.Windows.Forms.Label labelStateFightAlreadyFinished;
        private System.Windows.Forms.Label labelStateBattle;
        private System.Windows.Forms.Label labelStateError;
        private System.Windows.Forms.Label labelStateBattleFlash;
        private System.Windows.Forms.Label labelStateAssaultOperationRequest;
        private System.Windows.Forms.Label labelStateResult;
        private System.Windows.Forms.Label labelStateBattleAssaultOperation;
        private System.Windows.Forms.Label labelStateReceive;
        private System.Windows.Forms.Label labelStatePresentList;
        private System.Windows.Forms.CheckBox checkBoxRecievePresent;
        private System.Windows.Forms.CheckBox checkBoxOnlySearch;
        private System.Windows.Forms.CheckBox checkBoxRecieveReword;
        private System.Windows.Forms.Label labelStateAssaultOperationRequestSubmit;
        private System.Windows.Forms.Label labelStateAssaultOperationRequestComplete;
        private System.Windows.Forms.Label labelStateAssaultOperationWin;
        private System.Windows.Forms.CheckBox checkBoxOnlyAttackAssultBoss;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxWaitRecieveAssult;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxWaitAtackBattleShip;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label labelStateAssaultOperationRequestFaild;
    }
}
