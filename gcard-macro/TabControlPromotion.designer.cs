namespace gcard_macro
{
    partial class TabControlPromotion
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
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dateTimePickerTimeEnd = new System.Windows.Forms.DateTimePicker();
            this.textBoxSallyCount = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePickerTimeStart = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxWatchRank = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBoxAttackMode = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.labelStateHome = new System.Windows.Forms.Label();
            this.labelStateEnemyList = new System.Windows.Forms.Label();
            this.labelStateBattle = new System.Windows.Forms.Label();
            this.labelStateBattleFlash = new System.Windows.Forms.Label();
            this.labelStateResult = new System.Windows.Forms.Label();
            this.labelStateError = new System.Windows.Forms.Label();
            this.labelStateAccessBlock = new System.Windows.Forms.Label();
            this.labelStateEventFinished = new System.Windows.Forms.Label();
            this.labelStateUnknown = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.labelStateSallyConfirmation = new System.Windows.Forms.Label();
            this.labelStateWithdrawalCompletion = new System.Windows.Forms.Label();
            this.labelStateWithdrawalConfirmation = new System.Windows.Forms.Label();
            this.labelMiniCap = new System.Windows.Forms.Label();
            this.labelSallyCount = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonStart
            // 
            this.buttonStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStart.Location = new System.Drawing.Point(363, 477);
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
            this.buttonStop.Location = new System.Drawing.Point(450, 477);
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
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.dateTimePickerTimeEnd);
            this.groupBox1.Controls.Add(this.textBoxSallyCount);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dateTimePickerTimeStart);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.textBoxWatchRank);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.comboBoxAttackMode);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(19, 41);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(506, 421);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "オプション";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(103, 198);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 12);
            this.label5.TabIndex = 30;
            this.label5.Text = "～";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 173);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 29;
            this.label3.Text = "稼働時間";
            // 
            // dateTimePickerTimeEnd
            // 
            this.dateTimePickerTimeEnd.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePickerTimeEnd.Location = new System.Drawing.Point(127, 195);
            this.dateTimePickerTimeEnd.Name = "dateTimePickerTimeEnd";
            this.dateTimePickerTimeEnd.ShowUpDown = true;
            this.dateTimePickerTimeEnd.Size = new System.Drawing.Size(78, 19);
            this.dateTimePickerTimeEnd.TabIndex = 28;
            // 
            // textBoxSallyCount
            // 
            this.textBoxSallyCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSallyCount.Location = new System.Drawing.Point(14, 144);
            this.textBoxSallyCount.Name = "textBoxSallyCount";
            this.textBoxSallyCount.Size = new System.Drawing.Size(486, 19);
            this.textBoxSallyCount.TabIndex = 27;
            this.textBoxSallyCount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxSallyCount_KeyPress);
            this.textBoxSallyCount.Leave += new System.EventHandler(this.textBoxSallyCount_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 122);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 12);
            this.label2.TabIndex = 26;
            this.label2.Text = "出撃回数(0で無制限)";
            // 
            // dateTimePickerTimeStart
            // 
            this.dateTimePickerTimeStart.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePickerTimeStart.Location = new System.Drawing.Point(14, 195);
            this.dateTimePickerTimeStart.Name = "dateTimePickerTimeStart";
            this.dateTimePickerTimeStart.ShowUpDown = true;
            this.dateTimePickerTimeStart.Size = new System.Drawing.Size(78, 19);
            this.dateTimePickerTimeStart.TabIndex = 25;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(406, 41);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(75, 12);
            this.label8.TabIndex = 23;
            this.label8.Text = "位以下で出撃";
            // 
            // textBoxWatchRank
            // 
            this.textBoxWatchRank.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxWatchRank.Location = new System.Drawing.Point(14, 41);
            this.textBoxWatchRank.Name = "textBoxWatchRank";
            this.textBoxWatchRank.Size = new System.Drawing.Size(384, 19);
            this.textBoxWatchRank.TabIndex = 22;
            this.textBoxWatchRank.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxWatchRank_KeyPress);
            this.textBoxWatchRank.Leave += new System.EventHandler(this.textBoxWatchRank_Leave);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 19);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 12);
            this.label7.TabIndex = 21;
            this.label7.Text = "ランキング監視";
            // 
            // comboBoxAttackMode
            // 
            this.comboBoxAttackMode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxAttackMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAttackMode.FormattingEnabled = true;
            this.comboBoxAttackMode.Items.AddRange(new object[] {
            "攻撃力が低い敵を攻撃(撤退無し)",
            "攻撃力が低い敵を攻撃(HP20%以下で撤退)",
            "PTが高い敵を攻撃(撤退無し)",
            "PTが高い敵を攻撃(HP20%以下で撤退)",
            "攻撃力÷MS数が低い敵を攻撃(撤退無し)",
            "攻撃力÷MS数が低い敵を攻撃(HP20%以下で撤退)"});
            this.comboBoxAttackMode.Location = new System.Drawing.Point(14, 92);
            this.comboBoxAttackMode.Name = "comboBoxAttackMode";
            this.comboBoxAttackMode.Size = new System.Drawing.Size(486, 20);
            this.comboBoxAttackMode.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 12);
            this.label4.TabIndex = 14;
            this.label4.Text = "攻撃モード";
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
            // labelStateEnemyList
            // 
            this.labelStateEnemyList.BackColor = System.Drawing.Color.White;
            this.labelStateEnemyList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelStateEnemyList.Location = new System.Drawing.Point(96, 19);
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
            this.labelStateBattle.Location = new System.Drawing.Point(12, 64);
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
            this.labelStateBattleFlash.Location = new System.Drawing.Point(96, 64);
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
            this.labelStateResult.Location = new System.Drawing.Point(12, 109);
            this.labelStateResult.Name = "labelStateResult";
            this.labelStateResult.Size = new System.Drawing.Size(85, 46);
            this.labelStateResult.TabIndex = 13;
            this.labelStateResult.Text = "リザルト";
            this.labelStateResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelStateError
            // 
            this.labelStateError.BackColor = System.Drawing.Color.White;
            this.labelStateError.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelStateError.Location = new System.Drawing.Point(96, 109);
            this.labelStateError.Name = "labelStateError";
            this.labelStateError.Size = new System.Drawing.Size(86, 46);
            this.labelStateError.TabIndex = 18;
            this.labelStateError.Text = "不正な画面遷移";
            this.labelStateError.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelStateAccessBlock
            // 
            this.labelStateAccessBlock.BackColor = System.Drawing.Color.White;
            this.labelStateAccessBlock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelStateAccessBlock.Location = new System.Drawing.Point(12, 243);
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
            this.labelStateEventFinished.Location = new System.Drawing.Point(96, 198);
            this.labelStateEventFinished.Name = "labelStateEventFinished";
            this.labelStateEventFinished.Size = new System.Drawing.Size(86, 46);
            this.labelStateEventFinished.TabIndex = 21;
            this.labelStateEventFinished.Text = "イベント終了";
            this.labelStateEventFinished.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelStateUnknown
            // 
            this.labelStateUnknown.BackColor = System.Drawing.Color.White;
            this.labelStateUnknown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelStateUnknown.Location = new System.Drawing.Point(96, 243);
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
            this.groupBox2.Controls.Add(this.labelStateSallyConfirmation);
            this.groupBox2.Controls.Add(this.labelStateWithdrawalCompletion);
            this.groupBox2.Controls.Add(this.labelStateWithdrawalConfirmation);
            this.groupBox2.Controls.Add(this.labelStateHome);
            this.groupBox2.Controls.Add(this.labelStateUnknown);
            this.groupBox2.Controls.Add(this.labelStateEventFinished);
            this.groupBox2.Controls.Add(this.labelStateAccessBlock);
            this.groupBox2.Controls.Add(this.labelStateEnemyList);
            this.groupBox2.Controls.Add(this.labelStateBattle);
            this.groupBox2.Controls.Add(this.labelStateError);
            this.groupBox2.Controls.Add(this.labelStateBattleFlash);
            this.groupBox2.Controls.Add(this.labelStateResult);
            this.groupBox2.Location = new System.Drawing.Point(536, 97);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(190, 422);
            this.groupBox2.TabIndex = 23;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "画面遷移";
            // 
            // labelStateSallyConfirmation
            // 
            this.labelStateSallyConfirmation.BackColor = System.Drawing.Color.White;
            this.labelStateSallyConfirmation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelStateSallyConfirmation.Location = new System.Drawing.Point(12, 153);
            this.labelStateSallyConfirmation.Name = "labelStateSallyConfirmation";
            this.labelStateSallyConfirmation.Size = new System.Drawing.Size(85, 46);
            this.labelStateSallyConfirmation.TabIndex = 25;
            this.labelStateSallyConfirmation.Text = "出撃確認";
            this.labelStateSallyConfirmation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelStateWithdrawalCompletion
            // 
            this.labelStateWithdrawalCompletion.BackColor = System.Drawing.Color.White;
            this.labelStateWithdrawalCompletion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelStateWithdrawalCompletion.Location = new System.Drawing.Point(12, 198);
            this.labelStateWithdrawalCompletion.Name = "labelStateWithdrawalCompletion";
            this.labelStateWithdrawalCompletion.Size = new System.Drawing.Size(85, 46);
            this.labelStateWithdrawalCompletion.TabIndex = 24;
            this.labelStateWithdrawalCompletion.Text = "撤退完了";
            this.labelStateWithdrawalCompletion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelStateWithdrawalConfirmation
            // 
            this.labelStateWithdrawalConfirmation.BackColor = System.Drawing.Color.White;
            this.labelStateWithdrawalConfirmation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelStateWithdrawalConfirmation.Location = new System.Drawing.Point(96, 153);
            this.labelStateWithdrawalConfirmation.Name = "labelStateWithdrawalConfirmation";
            this.labelStateWithdrawalConfirmation.Size = new System.Drawing.Size(86, 46);
            this.labelStateWithdrawalConfirmation.TabIndex = 23;
            this.labelStateWithdrawalConfirmation.Text = "撤退確認";
            this.labelStateWithdrawalConfirmation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            // labelSallyCount
            // 
            this.labelSallyCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSallyCount.AutoSize = true;
            this.labelSallyCount.Location = new System.Drawing.Point(534, 73);
            this.labelSallyCount.Name = "labelSallyCount";
            this.labelSallyCount.Size = new System.Drawing.Size(79, 12);
            this.labelSallyCount.TabIndex = 25;
            this.labelSallyCount.Text = "残り出撃回数：";
            // 
            // TabControlPromotion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelSallyCount);
            this.Controls.Add(this.labelMiniCap);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxURL);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonStart);
            this.Name = "TabControlPromotion";
            this.Size = new System.Drawing.Size(743, 536);
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
        private System.Windows.Forms.ComboBox comboBoxAttackMode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxWatchRank;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label labelStateHome;
        private System.Windows.Forms.Label labelStateEnemyList;
        private System.Windows.Forms.Label labelStateBattle;
        private System.Windows.Forms.Label labelStateBattleFlash;
        private System.Windows.Forms.Label labelStateResult;
        private System.Windows.Forms.Label labelStateError;
        private System.Windows.Forms.Label labelStateAccessBlock;
        private System.Windows.Forms.Label labelStateEventFinished;
        private System.Windows.Forms.Label labelStateUnknown;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label labelMiniCap;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateTimePickerTimeEnd;
        private System.Windows.Forms.TextBox textBoxSallyCount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimePickerTimeStart;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelSallyCount;
        private System.Windows.Forms.Label labelStateWithdrawalConfirmation;
        private System.Windows.Forms.Label labelStateWithdrawalCompletion;
        private System.Windows.Forms.Label labelStateSallyConfirmation;
    }
}
