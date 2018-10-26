namespace gcard_macro
{
    partial class TabControlGroup
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
            this.comboBoxFinalJob = new System.Windows.Forms.ComboBox();
            this.checkBoxRecievePresent = new System.Windows.Forms.CheckBox();
            this.checkBoxOnlySearch = new System.Windows.Forms.CheckBox();
            this.checkBoxAutojobLevelUp = new System.Windows.Forms.CheckBox();
            this.checkBoxRecieveReword = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxEnemyCount = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.checkBoxUseBoost = new System.Windows.Forms.CheckBox();
            this.checkBoxFirstAttackBoss = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxPointDiff = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxAttackMode = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxRecieve = new System.Windows.Forms.ComboBox();
            this.checkBoxUseAttack20 = new System.Windows.Forms.CheckBox();
            this.checkBoxUseAttack10 = new System.Windows.Forms.CheckBox();
            this.textBoxBaseDamage = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.labelStateHome = new System.Windows.Forms.Label();
            this.labelStateInterval = new System.Windows.Forms.Label();
            this.labelStateSearch = new System.Windows.Forms.Label();
            this.labelStateEnemyList = new System.Windows.Forms.Label();
            this.labelStateBattle = new System.Windows.Forms.Label();
            this.labelStateBattleFlash = new System.Windows.Forms.Label();
            this.labelStateResult = new System.Windows.Forms.Label();
            this.labelStateReceive = new System.Windows.Forms.Label();
            this.labelStatePresentList = new System.Windows.Forms.Label();
            this.labelStateSelectJobs = new System.Windows.Forms.Label();
            this.labelStateUseBoost = new System.Windows.Forms.Label();
            this.labelStateError = new System.Windows.Forms.Label();
            this.labelStateFightAlreadyFinished = new System.Windows.Forms.Label();
            this.labelStateAccessBlock = new System.Windows.Forms.Label();
            this.labelStateEventFinished = new System.Windows.Forms.Label();
            this.labelStateUnknown = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.labelStateGetCard = new System.Windows.Forms.Label();
            this.labelStateLevelUp = new System.Windows.Forms.Label();
            this.labelMiniCap = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonStart
            // 
            this.buttonStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStart.Location = new System.Drawing.Point(363, 526);
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
            this.buttonStop.Location = new System.Drawing.Point(450, 526);
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
            this.groupBox1.Controls.Add(this.comboBoxFinalJob);
            this.groupBox1.Controls.Add(this.checkBoxRecievePresent);
            this.groupBox1.Controls.Add(this.checkBoxOnlySearch);
            this.groupBox1.Controls.Add(this.checkBoxAutojobLevelUp);
            this.groupBox1.Controls.Add(this.checkBoxRecieveReword);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.textBoxEnemyCount);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.checkBoxUseBoost);
            this.groupBox1.Controls.Add(this.checkBoxFirstAttackBoss);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.textBoxPointDiff);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.comboBoxAttackMode);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.comboBoxRecieve);
            this.groupBox1.Controls.Add(this.checkBoxUseAttack20);
            this.groupBox1.Controls.Add(this.checkBoxUseAttack10);
            this.groupBox1.Controls.Add(this.textBoxBaseDamage);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(19, 41);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(506, 470);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "オプション";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(14, 259);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(54, 12);
            this.label9.TabIndex = 29;
            this.label9.Text = "最終ジョブ";
            // 
            // comboBoxFinalJob
            // 
            this.comboBoxFinalJob.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxFinalJob.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFinalJob.FormattingEnabled = true;
            this.comboBoxFinalJob.Items.AddRange(new object[] {
            "アタッカー",
            "コマンダー",
            "シーカー",
            "スーパーエース",
            "スーパーサポーター"});
            this.comboBoxFinalJob.Location = new System.Drawing.Point(14, 277);
            this.comboBoxFinalJob.Name = "comboBoxFinalJob";
            this.comboBoxFinalJob.Size = new System.Drawing.Size(486, 20);
            this.comboBoxFinalJob.TabIndex = 28;
            // 
            // checkBoxRecievePresent
            // 
            this.checkBoxRecievePresent.AutoSize = true;
            this.checkBoxRecievePresent.Location = new System.Drawing.Point(14, 347);
            this.checkBoxRecievePresent.Name = "checkBoxRecievePresent";
            this.checkBoxRecievePresent.Size = new System.Drawing.Size(121, 16);
            this.checkBoxRecievePresent.TabIndex = 27;
            this.checkBoxRecievePresent.Text = "プレゼントを受け取る";
            this.checkBoxRecievePresent.UseVisualStyleBackColor = true;
            // 
            // checkBoxOnlySearch
            // 
            this.checkBoxOnlySearch.AutoSize = true;
            this.checkBoxOnlySearch.Location = new System.Drawing.Point(14, 303);
            this.checkBoxOnlySearch.Name = "checkBoxOnlySearch";
            this.checkBoxOnlySearch.Size = new System.Drawing.Size(69, 16);
            this.checkBoxOnlySearch.TabIndex = 26;
            this.checkBoxOnlySearch.Text = "探索のみ";
            this.checkBoxOnlySearch.UseVisualStyleBackColor = true;
            // 
            // checkBoxAutojobLevelUp
            // 
            this.checkBoxAutojobLevelUp.AutoSize = true;
            this.checkBoxAutojobLevelUp.Location = new System.Drawing.Point(14, 237);
            this.checkBoxAutojobLevelUp.Name = "checkBoxAutojobLevelUp";
            this.checkBoxAutojobLevelUp.Size = new System.Drawing.Size(124, 16);
            this.checkBoxAutojobLevelUp.TabIndex = 25;
            this.checkBoxAutojobLevelUp.Text = "自動ジョブレベル上げ";
            this.checkBoxAutojobLevelUp.UseVisualStyleBackColor = true;
            // 
            // checkBoxRecieveReword
            // 
            this.checkBoxRecieveReword.AutoSize = true;
            this.checkBoxRecieveReword.Location = new System.Drawing.Point(14, 325);
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
            this.label8.Location = new System.Drawing.Point(406, 41);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(94, 12);
            this.label8.TabIndex = 23;
            this.label8.Text = "体以下で探索する";
            // 
            // textBoxEnemyCount
            // 
            this.textBoxEnemyCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxEnemyCount.Location = new System.Drawing.Point(16, 37);
            this.textBoxEnemyCount.Name = "textBoxEnemyCount";
            this.textBoxEnemyCount.Size = new System.Drawing.Size(384, 19);
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
            // checkBoxUseBoost
            // 
            this.checkBoxUseBoost.AutoSize = true;
            this.checkBoxUseBoost.Location = new System.Drawing.Point(14, 215);
            this.checkBoxUseBoost.Name = "checkBoxUseBoost";
            this.checkBoxUseBoost.Size = new System.Drawing.Size(112, 16);
            this.checkBoxUseBoost.TabIndex = 20;
            this.checkBoxUseBoost.Text = "ブーストを使用する";
            this.checkBoxUseBoost.UseVisualStyleBackColor = true;
            // 
            // checkBoxFirstAttackBoss
            // 
            this.checkBoxFirstAttackBoss.AutoSize = true;
            this.checkBoxFirstAttackBoss.Location = new System.Drawing.Point(14, 193);
            this.checkBoxFirstAttackBoss.Name = "checkBoxFirstAttackBoss";
            this.checkBoxFirstAttackBoss.Size = new System.Drawing.Size(180, 16);
            this.checkBoxFirstAttackBoss.TabIndex = 19;
            this.checkBoxFirstAttackBoss.Text = "メモリアルボスを優先して攻撃する";
            this.checkBoxFirstAttackBoss.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(437, 434);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 12);
            this.label6.TabIndex = 18;
            this.label6.Text = "以上で待機";
            // 
            // textBoxPointDiff
            // 
            this.textBoxPointDiff.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPointDiff.Location = new System.Drawing.Point(14, 431);
            this.textBoxPointDiff.Name = "textBoxPointDiff";
            this.textBoxPointDiff.Size = new System.Drawing.Size(417, 19);
            this.textBoxPointDiff.TabIndex = 17;
            this.textBoxPointDiff.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxPointDiff_KeyPress);
            this.textBoxPointDiff.Validated += new System.EventHandler(this.textBoxPointDiff_Validated);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 413);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 12);
            this.label5.TabIndex = 16;
            this.label5.Text = "敵部隊との点数差";
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
            this.comboBoxAttackMode.Location = new System.Drawing.Point(14, 80);
            this.comboBoxAttackMode.Name = "comboBoxAttackMode";
            this.comboBoxAttackMode.Size = new System.Drawing.Size(486, 20);
            this.comboBoxAttackMode.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 12);
            this.label4.TabIndex = 14;
            this.label4.Text = "攻撃モード";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 369);
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
            this.comboBoxRecieve.Location = new System.Drawing.Point(14, 387);
            this.comboBoxRecieve.Name = "comboBoxRecieve";
            this.comboBoxRecieve.Size = new System.Drawing.Size(486, 20);
            this.comboBoxRecieve.TabIndex = 12;
            // 
            // checkBoxUseAttack20
            // 
            this.checkBoxUseAttack20.AutoSize = true;
            this.checkBoxUseAttack20.Location = new System.Drawing.Point(14, 171);
            this.checkBoxUseAttack20.Name = "checkBoxUseAttack20";
            this.checkBoxUseAttack20.Size = new System.Drawing.Size(396, 16);
            this.checkBoxUseAttack20.TabIndex = 11;
            this.checkBoxUseAttack20.Text = "BEx3 20倍攻撃を使用する(敵部隊のミラージュコロイドが発動しているときのみ)";
            this.checkBoxUseAttack20.UseVisualStyleBackColor = true;
            // 
            // checkBoxUseAttack10
            // 
            this.checkBoxUseAttack10.AutoSize = true;
            this.checkBoxUseAttack10.Location = new System.Drawing.Point(14, 149);
            this.checkBoxUseAttack10.Name = "checkBoxUseAttack10";
            this.checkBoxUseAttack10.Size = new System.Drawing.Size(155, 16);
            this.checkBoxUseAttack10.TabIndex = 10;
            this.checkBoxUseAttack10.Text = "BEx5 10倍攻撃を使用する";
            this.checkBoxUseAttack10.UseVisualStyleBackColor = true;
            // 
            // textBoxBaseDamage
            // 
            this.textBoxBaseDamage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxBaseDamage.Location = new System.Drawing.Point(14, 124);
            this.textBoxBaseDamage.Name = "textBoxBaseDamage";
            this.textBoxBaseDamage.Size = new System.Drawing.Size(486, 19);
            this.textBoxBaseDamage.TabIndex = 9;
            this.textBoxBaseDamage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxBaseDamage_KeyPress);
            this.textBoxBaseDamage.Validated += new System.EventHandler(this.textBoxBaseDamage_Validated);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 106);
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
            // labelStateInterval
            // 
            this.labelStateInterval.BackColor = System.Drawing.Color.White;
            this.labelStateInterval.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelStateInterval.Location = new System.Drawing.Point(96, 19);
            this.labelStateInterval.Name = "labelStateInterval";
            this.labelStateInterval.Size = new System.Drawing.Size(86, 46);
            this.labelStateInterval.TabIndex = 8;
            this.labelStateInterval.Text = "インターバル";
            this.labelStateInterval.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelStateSearch
            // 
            this.labelStateSearch.BackColor = System.Drawing.Color.White;
            this.labelStateSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelStateSearch.Location = new System.Drawing.Point(12, 64);
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
            this.labelStateEnemyList.Location = new System.Drawing.Point(96, 64);
            this.labelStateEnemyList.Name = "labelStateEnemyList";
            this.labelStateEnemyList.Size = new System.Drawing.Size(86, 46);
            this.labelStateEnemyList.TabIndex = 10;
            this.labelStateEnemyList.Text = "敵一覧";
            this.labelStateEnemyList.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelStateBattle
            // 
            this.labelStateBattle.BackColor = System.Drawing.Color.White;
            this.labelStateBattle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelStateBattle.Location = new System.Drawing.Point(12, 109);
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
            this.labelStateBattleFlash.Location = new System.Drawing.Point(96, 109);
            this.labelStateBattleFlash.Name = "labelStateBattleFlash";
            this.labelStateBattleFlash.Size = new System.Drawing.Size(86, 46);
            this.labelStateBattleFlash.TabIndex = 12;
            this.labelStateBattleFlash.Text = "戦闘";
            this.labelStateBattleFlash.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelStateResult
            // 
            this.labelStateResult.BackColor = System.Drawing.Color.White;
            this.labelStateResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelStateResult.Location = new System.Drawing.Point(12, 154);
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
            this.labelStateReceive.Location = new System.Drawing.Point(96, 154);
            this.labelStateReceive.Name = "labelStateReceive";
            this.labelStateReceive.Size = new System.Drawing.Size(86, 46);
            this.labelStateReceive.TabIndex = 14;
            this.labelStateReceive.Text = "報酬受取";
            this.labelStateReceive.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelStatePresentList
            // 
            this.labelStatePresentList.BackColor = System.Drawing.Color.White;
            this.labelStatePresentList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelStatePresentList.Location = new System.Drawing.Point(12, 199);
            this.labelStatePresentList.Name = "labelStatePresentList";
            this.labelStatePresentList.Size = new System.Drawing.Size(85, 46);
            this.labelStatePresentList.TabIndex = 15;
            this.labelStatePresentList.Text = "プレゼント一覧";
            this.labelStatePresentList.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelStateSelectJobs
            // 
            this.labelStateSelectJobs.BackColor = System.Drawing.Color.White;
            this.labelStateSelectJobs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelStateSelectJobs.Location = new System.Drawing.Point(96, 199);
            this.labelStateSelectJobs.Name = "labelStateSelectJobs";
            this.labelStateSelectJobs.Size = new System.Drawing.Size(86, 46);
            this.labelStateSelectJobs.TabIndex = 16;
            this.labelStateSelectJobs.Text = "ジョブ選択";
            this.labelStateSelectJobs.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelStateUseBoost
            // 
            this.labelStateUseBoost.BackColor = System.Drawing.Color.White;
            this.labelStateUseBoost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelStateUseBoost.Location = new System.Drawing.Point(12, 289);
            this.labelStateUseBoost.Name = "labelStateUseBoost";
            this.labelStateUseBoost.Size = new System.Drawing.Size(85, 46);
            this.labelStateUseBoost.TabIndex = 17;
            this.labelStateUseBoost.Text = "BOOST使用";
            this.labelStateUseBoost.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelStateError
            // 
            this.labelStateError.BackColor = System.Drawing.Color.White;
            this.labelStateError.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelStateError.Location = new System.Drawing.Point(96, 289);
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
            this.labelStateFightAlreadyFinished.Location = new System.Drawing.Point(12, 334);
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
            this.labelStateAccessBlock.Location = new System.Drawing.Point(96, 334);
            this.labelStateAccessBlock.Name = "labelStateAccessBlock";
            this.labelStateAccessBlock.Size = new System.Drawing.Size(86, 46);
            this.labelStateAccessBlock.TabIndex = 20;
            this.labelStateAccessBlock.Text = "アクセス制限";
            this.labelStateAccessBlock.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelStateEventFinished
            // 
            this.labelStateEventFinished.BackColor = System.Drawing.Color.White;
            this.labelStateEventFinished.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelStateEventFinished.Location = new System.Drawing.Point(12, 379);
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
            this.labelStateUnknown.Location = new System.Drawing.Point(96, 379);
            this.labelStateUnknown.Name = "labelStateUnknown";
            this.labelStateUnknown.Size = new System.Drawing.Size(86, 46);
            this.labelStateUnknown.TabIndex = 22;
            this.labelStateUnknown.Text = "不明な画面";
            this.labelStateUnknown.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.labelStateGetCard);
            this.groupBox2.Controls.Add(this.labelStateHome);
            this.groupBox2.Controls.Add(this.labelStateLevelUp);
            this.groupBox2.Controls.Add(this.labelStateUnknown);
            this.groupBox2.Controls.Add(this.labelStateInterval);
            this.groupBox2.Controls.Add(this.labelStateEventFinished);
            this.groupBox2.Controls.Add(this.labelStateSearch);
            this.groupBox2.Controls.Add(this.labelStateAccessBlock);
            this.groupBox2.Controls.Add(this.labelStateEnemyList);
            this.groupBox2.Controls.Add(this.labelStateFightAlreadyFinished);
            this.groupBox2.Controls.Add(this.labelStateBattle);
            this.groupBox2.Controls.Add(this.labelStateError);
            this.groupBox2.Controls.Add(this.labelStateBattleFlash);
            this.groupBox2.Controls.Add(this.labelStateUseBoost);
            this.groupBox2.Controls.Add(this.labelStateResult);
            this.groupBox2.Controls.Add(this.labelStateSelectJobs);
            this.groupBox2.Controls.Add(this.labelStateReceive);
            this.groupBox2.Controls.Add(this.labelStatePresentList);
            this.groupBox2.Location = new System.Drawing.Point(536, 75);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(190, 493);
            this.groupBox2.TabIndex = 23;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "画面遷移";
            // 
            // labelStateGetCard
            // 
            this.labelStateGetCard.BackColor = System.Drawing.Color.White;
            this.labelStateGetCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelStateGetCard.Location = new System.Drawing.Point(96, 244);
            this.labelStateGetCard.Name = "labelStateGetCard";
            this.labelStateGetCard.Size = new System.Drawing.Size(86, 46);
            this.labelStateGetCard.TabIndex = 24;
            this.labelStateGetCard.Text = "カード入手";
            this.labelStateGetCard.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelStateLevelUp
            // 
            this.labelStateLevelUp.BackColor = System.Drawing.Color.White;
            this.labelStateLevelUp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelStateLevelUp.Location = new System.Drawing.Point(12, 244);
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
            this.labelMiniCap.Location = new System.Drawing.Point(534, 50);
            this.labelMiniCap.Name = "labelMiniCap";
            this.labelMiniCap.Size = new System.Drawing.Size(45, 12);
            this.labelMiniCap.TabIndex = 24;
            this.labelMiniCap.Text = "ミニカプ：";
            // 
            // TabControlGroup
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
            this.Name = "TabControlGroup";
            this.Size = new System.Drawing.Size(743, 585);
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
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxRecieve;
        private System.Windows.Forms.ComboBox comboBoxAttackMode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxEnemyCount;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label labelStateHome;
        private System.Windows.Forms.Label labelStateInterval;
        private System.Windows.Forms.Label labelStateSearch;
        private System.Windows.Forms.Label labelStateEnemyList;
        private System.Windows.Forms.Label labelStateBattle;
        private System.Windows.Forms.Label labelStateBattleFlash;
        private System.Windows.Forms.Label labelStateResult;
        private System.Windows.Forms.Label labelStateReceive;
        private System.Windows.Forms.Label labelStatePresentList;
        private System.Windows.Forms.Label labelStateSelectJobs;
        private System.Windows.Forms.Label labelStateUseBoost;
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
        private System.Windows.Forms.CheckBox checkBoxAutojobLevelUp;
        private System.Windows.Forms.CheckBox checkBoxUseBoost;
        private System.Windows.Forms.CheckBox checkBoxFirstAttackBoss;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxPointDiff;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox checkBoxUseAttack20;
        private System.Windows.Forms.CheckBox checkBoxUseAttack10;
        private System.Windows.Forms.CheckBox checkBoxRecievePresent;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboBoxFinalJob;
    }
}
