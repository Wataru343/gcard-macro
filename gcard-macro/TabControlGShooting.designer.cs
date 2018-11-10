namespace gcard_macro
{
    partial class TabControlGShooting
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.checkBoxRequest = new System.Windows.Forms.CheckBox();
            this.checkBoxRecievePresent = new System.Windows.Forms.CheckBox();
            this.comboBoxRecieve = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBoxNoSearch = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.dateTimePickerTimeStart = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerTimeEnd = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.checkBoxRecieveReword = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.textBoxEnemyCount = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.checkBoxOnlySearch = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxAttackMode = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxBaseDamage = new System.Windows.Forms.TextBox();
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
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.labelStateGetCard = new System.Windows.Forms.Label();
            this.labelStateRequest = new System.Windows.Forms.Label();
            this.labelStateLevelUp = new System.Windows.Forms.Label();
            this.labelMiniCap = new System.Windows.Forms.Label();
            this.labelSpm = new System.Windows.Forms.Label();
            this.tableLayoutPanel10 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel10.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonStart
            // 
            this.buttonStart.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonStart.Location = new System.Drawing.Point(58, 7);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(81, 42);
            this.buttonStart.TabIndex = 3;
            this.buttonStart.Text = "開始";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonStop.Location = new System.Drawing.Point(145, 7);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(75, 42);
            this.buttonStop.TabIndex = 4;
            this.buttonStop.Text = "停止";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // textBoxURL
            // 
            this.textBoxURL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxURL.Location = new System.Drawing.Point(122, 3);
            this.textBoxURL.Name = "textBoxURL";
            this.textBoxURL.Size = new System.Drawing.Size(308, 19);
            this.textBoxURL.TabIndex = 1;
            this.textBoxURL.TextChanged += new System.EventHandler(this.ValueChanged);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
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
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(223, 371);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "オプション";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoScroll = true;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.checkBoxRequest, 0, 14);
            this.tableLayoutPanel1.Controls.Add(this.checkBoxRecievePresent, 0, 11);
            this.tableLayoutPanel1.Controls.Add(this.comboBoxRecieve, 0, 13);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 12);
            this.tableLayoutPanel1.Controls.Add(this.checkBoxNoSearch, 0, 9);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.checkBoxRecieveReword, 0, 10);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.checkBoxOnlySearch, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.label11, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.comboBoxAttackMode, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.textBoxBaseDamage, 0, 7);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 15);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 15;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(217, 353);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // checkBoxRequest
            // 
            this.checkBoxRequest.AutoSize = true;
            this.checkBoxRequest.Location = new System.Drawing.Point(3, 329);
            this.checkBoxRequest.Name = "checkBoxRequest";
            this.checkBoxRequest.Size = new System.Drawing.Size(103, 16);
            this.checkBoxRequest.TabIndex = 17;
            this.checkBoxRequest.Text = "応援依頼を出す";
            this.checkBoxRequest.UseVisualStyleBackColor = true;
            this.checkBoxRequest.CheckedChanged += new System.EventHandler(this.ValueChanged);
            // 
            // checkBoxRecievePresent
            // 
            this.checkBoxRecievePresent.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.checkBoxRecievePresent.AutoSize = true;
            this.checkBoxRecievePresent.Location = new System.Drawing.Point(3, 261);
            this.checkBoxRecievePresent.Name = "checkBoxRecievePresent";
            this.checkBoxRecievePresent.Size = new System.Drawing.Size(121, 16);
            this.checkBoxRecievePresent.TabIndex = 14;
            this.checkBoxRecievePresent.Text = "プレゼントを受け取る";
            this.checkBoxRecievePresent.UseVisualStyleBackColor = true;
            this.checkBoxRecievePresent.CheckedChanged += new System.EventHandler(this.ValueChanged);
            // 
            // comboBoxRecieve
            // 
            this.comboBoxRecieve.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
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
            this.comboBoxRecieve.Location = new System.Drawing.Point(3, 303);
            this.comboBoxRecieve.Name = "comboBoxRecieve";
            this.comboBoxRecieve.Size = new System.Drawing.Size(211, 20);
            this.comboBoxRecieve.TabIndex = 16;
            this.comboBoxRecieve.SelectedIndexChanged += new System.EventHandler(this.ValueChanged);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 284);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(141, 12);
            this.label3.TabIndex = 15;
            this.label3.Text = "報酬をまとめて受け取る個数";
            // 
            // checkBoxNoSearch
            // 
            this.checkBoxNoSearch.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.checkBoxNoSearch.AutoSize = true;
            this.checkBoxNoSearch.Location = new System.Drawing.Point(3, 217);
            this.checkBoxNoSearch.Name = "checkBoxNoSearch";
            this.checkBoxNoSearch.Size = new System.Drawing.Size(77, 16);
            this.checkBoxNoSearch.TabIndex = 12;
            this.checkBoxNoSearch.Text = "探索しない";
            this.checkBoxNoSearch.UseVisualStyleBackColor = true;
            this.checkBoxNoSearch.CheckedChanged += new System.EventHandler(this.ValueChanged);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 88F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 93F));
            this.tableLayoutPanel3.Controls.Add(this.dateTimePickerTimeStart, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.dateTimePickerTimeEnd, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.label10, 1, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 73);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(211, 25);
            this.tableLayoutPanel3.TabIndex = 9;
            // 
            // dateTimePickerTimeStart
            // 
            this.dateTimePickerTimeStart.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dateTimePickerTimeStart.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePickerTimeStart.Location = new System.Drawing.Point(5, 3);
            this.dateTimePickerTimeStart.Name = "dateTimePickerTimeStart";
            this.dateTimePickerTimeStart.ShowUpDown = true;
            this.dateTimePickerTimeStart.Size = new System.Drawing.Size(78, 19);
            this.dateTimePickerTimeStart.TabIndex = 4;
            this.dateTimePickerTimeStart.ValueChanged += new System.EventHandler(this.ValueChanged);
            // 
            // dateTimePickerTimeEnd
            // 
            this.dateTimePickerTimeEnd.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.dateTimePickerTimeEnd.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePickerTimeEnd.Location = new System.Drawing.Point(121, 3);
            this.dateTimePickerTimeEnd.Name = "dateTimePickerTimeEnd";
            this.dateTimePickerTimeEnd.ShowUpDown = true;
            this.dateTimePickerTimeEnd.Size = new System.Drawing.Size(78, 19);
            this.dateTimePickerTimeEnd.TabIndex = 6;
            this.dateTimePickerTimeEnd.ValueChanged += new System.EventHandler(this.ValueChanged);
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(94, 6);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(17, 12);
            this.label10.TabIndex = 5;
            this.label10.Text = "～";
            // 
            // checkBoxRecieveReword
            // 
            this.checkBoxRecieveReword.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.checkBoxRecieveReword.AutoSize = true;
            this.checkBoxRecieveReword.Location = new System.Drawing.Point(3, 239);
            this.checkBoxRecieveReword.Name = "checkBoxRecieveReword";
            this.checkBoxRecieveReword.Size = new System.Drawing.Size(100, 16);
            this.checkBoxRecieveReword.TabIndex = 13;
            this.checkBoxRecieveReword.Text = "報酬を受け取る";
            this.checkBoxRecieveReword.UseVisualStyleBackColor = true;
            this.checkBoxRecieveReword.CheckedChanged += new System.EventHandler(this.ValueChanged);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 102F));
            this.tableLayoutPanel2.Controls.Add(this.textBoxEnemyCount, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label8, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 23);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(211, 24);
            this.tableLayoutPanel2.TabIndex = 9;
            // 
            // textBoxEnemyCount
            // 
            this.textBoxEnemyCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxEnemyCount.Location = new System.Drawing.Point(3, 3);
            this.textBoxEnemyCount.Name = "textBoxEnemyCount";
            this.textBoxEnemyCount.Size = new System.Drawing.Size(103, 19);
            this.textBoxEnemyCount.TabIndex = 1;
            this.textBoxEnemyCount.TextChanged += new System.EventHandler(this.ValueChanged);
            this.textBoxEnemyCount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxEnemyCount_KeyPress);
            this.textBoxEnemyCount.Validated += new System.EventHandler(this.textBoxEnemyCount_Validated);
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(114, 6);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(94, 12);
            this.label8.TabIndex = 2;
            this.label8.Text = "体以下で探索する";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "探索する条件";
            // 
            // checkBoxOnlySearch
            // 
            this.checkBoxOnlySearch.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.checkBoxOnlySearch.AutoSize = true;
            this.checkBoxOnlySearch.Location = new System.Drawing.Point(3, 195);
            this.checkBoxOnlySearch.Name = "checkBoxOnlySearch";
            this.checkBoxOnlySearch.Size = new System.Drawing.Size(98, 16);
            this.checkBoxOnlySearch.TabIndex = 11;
            this.checkBoxOnlySearch.Text = "敵を攻撃しない";
            this.checkBoxOnlySearch.UseVisualStyleBackColor = true;
            this.checkBoxOnlySearch.CheckedChanged += new System.EventHandler(this.ValueChanged);
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 54);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 3;
            this.label11.Text = "稼働時間";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "攻撃モード";
            // 
            // comboBoxAttackMode
            // 
            this.comboBoxAttackMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxAttackMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAttackMode.FormattingEnabled = true;
            this.comboBoxAttackMode.Items.AddRange(new object[] {
            "無制限に攻撃する",
            "コンボのみ狙う",
            "一発だけ攻撃する"});
            this.comboBoxAttackMode.Location = new System.Drawing.Point(3, 124);
            this.comboBoxAttackMode.Name = "comboBoxAttackMode";
            this.comboBoxAttackMode.Size = new System.Drawing.Size(211, 20);
            this.comboBoxAttackMode.TabIndex = 8;
            this.comboBoxAttackMode.SelectedIndexChanged += new System.EventHandler(this.ValueChanged);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 151);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "BEx1使用時のダメージ";
            // 
            // textBoxBaseDamage
            // 
            this.textBoxBaseDamage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxBaseDamage.Location = new System.Drawing.Point(3, 170);
            this.textBoxBaseDamage.Name = "textBoxBaseDamage";
            this.textBoxBaseDamage.Size = new System.Drawing.Size(211, 19);
            this.textBoxBaseDamage.TabIndex = 10;
            this.textBoxBaseDamage.TextChanged += new System.EventHandler(this.ValueChanged);
            this.textBoxBaseDamage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxBaseDamage_KeyPress);
            this.textBoxBaseDamage.Validated += new System.EventHandler(this.textBoxBaseDamage_Validated);
            // 
            // labelStateHome
            // 
            this.labelStateHome.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelStateHome.BackColor = System.Drawing.Color.White;
            this.labelStateHome.Location = new System.Drawing.Point(0, 0);
            this.labelStateHome.Margin = new System.Windows.Forms.Padding(0);
            this.labelStateHome.Name = "labelStateHome";
            this.labelStateHome.Size = new System.Drawing.Size(90, 46);
            this.labelStateHome.TabIndex = 0;
            this.labelStateHome.Text = "ホーム";
            this.labelStateHome.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelStateHome.Paint += new System.Windows.Forms.PaintEventHandler(this.PaintFrameTopLeftRight);
            // 
            // labelStateSearch
            // 
            this.labelStateSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelStateSearch.BackColor = System.Drawing.Color.White;
            this.labelStateSearch.Location = new System.Drawing.Point(90, 0);
            this.labelStateSearch.Margin = new System.Windows.Forms.Padding(0);
            this.labelStateSearch.Name = "labelStateSearch";
            this.labelStateSearch.Size = new System.Drawing.Size(90, 46);
            this.labelStateSearch.TabIndex = 1;
            this.labelStateSearch.Text = "探索";
            this.labelStateSearch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelStateSearch.Paint += new System.Windows.Forms.PaintEventHandler(this.PaintFrameTopRight);
            // 
            // labelStateEnemyList
            // 
            this.labelStateEnemyList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelStateEnemyList.BackColor = System.Drawing.Color.White;
            this.labelStateEnemyList.Location = new System.Drawing.Point(0, 46);
            this.labelStateEnemyList.Margin = new System.Windows.Forms.Padding(0);
            this.labelStateEnemyList.Name = "labelStateEnemyList";
            this.labelStateEnemyList.Size = new System.Drawing.Size(90, 46);
            this.labelStateEnemyList.TabIndex = 2;
            this.labelStateEnemyList.Text = "敵一覧";
            this.labelStateEnemyList.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelStateEnemyList.Paint += new System.Windows.Forms.PaintEventHandler(this.PaintFrameTopLeftRight);
            // 
            // labelStateBattle
            // 
            this.labelStateBattle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelStateBattle.BackColor = System.Drawing.Color.White;
            this.labelStateBattle.Location = new System.Drawing.Point(90, 46);
            this.labelStateBattle.Margin = new System.Windows.Forms.Padding(0);
            this.labelStateBattle.Name = "labelStateBattle";
            this.labelStateBattle.Size = new System.Drawing.Size(90, 46);
            this.labelStateBattle.TabIndex = 3;
            this.labelStateBattle.Text = "戦闘画面";
            this.labelStateBattle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelStateBattle.Paint += new System.Windows.Forms.PaintEventHandler(this.PaintFrameTopRight);
            // 
            // labelStateBattleFlash
            // 
            this.labelStateBattleFlash.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelStateBattleFlash.BackColor = System.Drawing.Color.White;
            this.labelStateBattleFlash.Location = new System.Drawing.Point(0, 92);
            this.labelStateBattleFlash.Margin = new System.Windows.Forms.Padding(0);
            this.labelStateBattleFlash.Name = "labelStateBattleFlash";
            this.labelStateBattleFlash.Size = new System.Drawing.Size(90, 46);
            this.labelStateBattleFlash.TabIndex = 4;
            this.labelStateBattleFlash.Text = "戦闘";
            this.labelStateBattleFlash.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelStateBattleFlash.Paint += new System.Windows.Forms.PaintEventHandler(this.PaintFrameTopLeftRight);
            // 
            // labelStateResult
            // 
            this.labelStateResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelStateResult.BackColor = System.Drawing.Color.White;
            this.labelStateResult.Location = new System.Drawing.Point(90, 92);
            this.labelStateResult.Margin = new System.Windows.Forms.Padding(0);
            this.labelStateResult.Name = "labelStateResult";
            this.labelStateResult.Size = new System.Drawing.Size(90, 46);
            this.labelStateResult.TabIndex = 5;
            this.labelStateResult.Text = "リザルト";
            this.labelStateResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelStateResult.Paint += new System.Windows.Forms.PaintEventHandler(this.PaintFrameTopRight);
            // 
            // labelStateReceive
            // 
            this.labelStateReceive.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelStateReceive.BackColor = System.Drawing.Color.White;
            this.labelStateReceive.Location = new System.Drawing.Point(0, 138);
            this.labelStateReceive.Margin = new System.Windows.Forms.Padding(0);
            this.labelStateReceive.Name = "labelStateReceive";
            this.labelStateReceive.Size = new System.Drawing.Size(90, 46);
            this.labelStateReceive.TabIndex = 6;
            this.labelStateReceive.Text = "報酬受取";
            this.labelStateReceive.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelStateReceive.Paint += new System.Windows.Forms.PaintEventHandler(this.PaintFrameTopLeftRight);
            // 
            // labelStatePresentList
            // 
            this.labelStatePresentList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelStatePresentList.BackColor = System.Drawing.Color.White;
            this.labelStatePresentList.Location = new System.Drawing.Point(90, 138);
            this.labelStatePresentList.Margin = new System.Windows.Forms.Padding(0);
            this.labelStatePresentList.Name = "labelStatePresentList";
            this.labelStatePresentList.Size = new System.Drawing.Size(90, 46);
            this.labelStatePresentList.TabIndex = 7;
            this.labelStatePresentList.Text = "プレゼント一覧";
            this.labelStatePresentList.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelStatePresentList.Paint += new System.Windows.Forms.PaintEventHandler(this.PaintFrameTopRight);
            // 
            // labelStateError
            // 
            this.labelStateError.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelStateError.BackColor = System.Drawing.Color.White;
            this.labelStateError.Location = new System.Drawing.Point(90, 230);
            this.labelStateError.Margin = new System.Windows.Forms.Padding(0);
            this.labelStateError.Name = "labelStateError";
            this.labelStateError.Size = new System.Drawing.Size(90, 46);
            this.labelStateError.TabIndex = 11;
            this.labelStateError.Text = "不正な画面遷移";
            this.labelStateError.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelStateError.Paint += new System.Windows.Forms.PaintEventHandler(this.PaintFrameTopRight);
            // 
            // labelStateFightAlreadyFinished
            // 
            this.labelStateFightAlreadyFinished.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelStateFightAlreadyFinished.BackColor = System.Drawing.Color.White;
            this.labelStateFightAlreadyFinished.Location = new System.Drawing.Point(0, 276);
            this.labelStateFightAlreadyFinished.Margin = new System.Windows.Forms.Padding(0);
            this.labelStateFightAlreadyFinished.Name = "labelStateFightAlreadyFinished";
            this.labelStateFightAlreadyFinished.Size = new System.Drawing.Size(90, 46);
            this.labelStateFightAlreadyFinished.TabIndex = 12;
            this.labelStateFightAlreadyFinished.Text = "既に戦闘は終了しています";
            this.labelStateFightAlreadyFinished.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelStateFightAlreadyFinished.Paint += new System.Windows.Forms.PaintEventHandler(this.PaintFrameTopLeftRight);
            // 
            // labelStateAccessBlock
            // 
            this.labelStateAccessBlock.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelStateAccessBlock.BackColor = System.Drawing.Color.White;
            this.labelStateAccessBlock.Location = new System.Drawing.Point(90, 276);
            this.labelStateAccessBlock.Margin = new System.Windows.Forms.Padding(0);
            this.labelStateAccessBlock.Name = "labelStateAccessBlock";
            this.labelStateAccessBlock.Size = new System.Drawing.Size(90, 46);
            this.labelStateAccessBlock.TabIndex = 13;
            this.labelStateAccessBlock.Text = "アクセス制限";
            this.labelStateAccessBlock.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelStateAccessBlock.Paint += new System.Windows.Forms.PaintEventHandler(this.PaintFrameTopRight);
            // 
            // labelStateEventFinished
            // 
            this.labelStateEventFinished.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelStateEventFinished.BackColor = System.Drawing.Color.White;
            this.labelStateEventFinished.Location = new System.Drawing.Point(0, 322);
            this.labelStateEventFinished.Margin = new System.Windows.Forms.Padding(0);
            this.labelStateEventFinished.Name = "labelStateEventFinished";
            this.labelStateEventFinished.Size = new System.Drawing.Size(90, 46);
            this.labelStateEventFinished.TabIndex = 14;
            this.labelStateEventFinished.Text = "イベント終了";
            this.labelStateEventFinished.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelStateEventFinished.Paint += new System.Windows.Forms.PaintEventHandler(this.PaintFrameTopLeftRightBottom);
            // 
            // labelStateUnknown
            // 
            this.labelStateUnknown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelStateUnknown.BackColor = System.Drawing.Color.White;
            this.labelStateUnknown.Location = new System.Drawing.Point(90, 322);
            this.labelStateUnknown.Margin = new System.Windows.Forms.Padding(0);
            this.labelStateUnknown.Name = "labelStateUnknown";
            this.labelStateUnknown.Size = new System.Drawing.Size(90, 46);
            this.labelStateUnknown.TabIndex = 15;
            this.labelStateUnknown.Text = "不明な画面";
            this.labelStateUnknown.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelStateUnknown.Paint += new System.Windows.Forms.PaintEventHandler(this.PaintFrameTopRightBottom);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.tableLayoutPanel5);
            this.groupBox2.Location = new System.Drawing.Point(3, 43);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(186, 393);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "画面遷移";
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.AutoScroll = true;
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Controls.Add(this.labelStateUnknown, 1, 7);
            this.tableLayoutPanel5.Controls.Add(this.labelStateGetCard, 0, 5);
            this.tableLayoutPanel5.Controls.Add(this.labelStateEventFinished, 0, 7);
            this.tableLayoutPanel5.Controls.Add(this.labelStateRequest, 0, 4);
            this.tableLayoutPanel5.Controls.Add(this.labelStateAccessBlock, 1, 6);
            this.tableLayoutPanel5.Controls.Add(this.labelStateLevelUp, 1, 4);
            this.tableLayoutPanel5.Controls.Add(this.labelStateFightAlreadyFinished, 0, 6);
            this.tableLayoutPanel5.Controls.Add(this.labelStateHome, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.labelStateError, 1, 5);
            this.tableLayoutPanel5.Controls.Add(this.labelStateSearch, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.labelStateEnemyList, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.labelStateBattle, 1, 1);
            this.tableLayoutPanel5.Controls.Add(this.labelStateBattleFlash, 0, 2);
            this.tableLayoutPanel5.Controls.Add(this.labelStateResult, 1, 2);
            this.tableLayoutPanel5.Controls.Add(this.labelStateReceive, 0, 3);
            this.tableLayoutPanel5.Controls.Add(this.labelStatePresentList, 1, 3);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 15);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 8;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(180, 375);
            this.tableLayoutPanel5.TabIndex = 0;
            // 
            // labelStateGetCard
            // 
            this.labelStateGetCard.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelStateGetCard.BackColor = System.Drawing.Color.White;
            this.labelStateGetCard.Location = new System.Drawing.Point(0, 230);
            this.labelStateGetCard.Margin = new System.Windows.Forms.Padding(0);
            this.labelStateGetCard.Name = "labelStateGetCard";
            this.labelStateGetCard.Size = new System.Drawing.Size(90, 46);
            this.labelStateGetCard.TabIndex = 10;
            this.labelStateGetCard.Text = "カード入手";
            this.labelStateGetCard.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelStateGetCard.Paint += new System.Windows.Forms.PaintEventHandler(this.PaintFrameTopLeftRight);
            // 
            // labelStateRequest
            // 
            this.labelStateRequest.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelStateRequest.BackColor = System.Drawing.Color.White;
            this.labelStateRequest.Location = new System.Drawing.Point(0, 184);
            this.labelStateRequest.Margin = new System.Windows.Forms.Padding(0);
            this.labelStateRequest.Name = "labelStateRequest";
            this.labelStateRequest.Size = new System.Drawing.Size(90, 46);
            this.labelStateRequest.TabIndex = 8;
            this.labelStateRequest.Text = "応援依頼完了";
            this.labelStateRequest.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelStateRequest.Paint += new System.Windows.Forms.PaintEventHandler(this.PaintFrameTopLeftRight);
            // 
            // labelStateLevelUp
            // 
            this.labelStateLevelUp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelStateLevelUp.BackColor = System.Drawing.Color.White;
            this.labelStateLevelUp.Location = new System.Drawing.Point(90, 184);
            this.labelStateLevelUp.Margin = new System.Windows.Forms.Padding(0);
            this.labelStateLevelUp.Name = "labelStateLevelUp";
            this.labelStateLevelUp.Size = new System.Drawing.Size(90, 46);
            this.labelStateLevelUp.TabIndex = 9;
            this.labelStateLevelUp.Text = "レベルアップ";
            this.labelStateLevelUp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelStateLevelUp.Paint += new System.Windows.Forms.PaintEventHandler(this.PaintFrameTopRight);
            // 
            // labelMiniCap
            // 
            this.labelMiniCap.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelMiniCap.AutoSize = true;
            this.labelMiniCap.Location = new System.Drawing.Point(3, 4);
            this.labelMiniCap.Name = "labelMiniCap";
            this.labelMiniCap.Size = new System.Drawing.Size(45, 12);
            this.labelMiniCap.TabIndex = 5;
            this.labelMiniCap.Text = "ミニカプ：";
            // 
            // labelSpm
            // 
            this.labelSpm.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelSpm.AutoSize = true;
            this.labelSpm.Location = new System.Drawing.Point(3, 24);
            this.labelSpm.Name = "labelSpm";
            this.labelSpm.Size = new System.Drawing.Size(99, 12);
            this.labelSpm.TabIndex = 6;
            this.labelSpm.Text = "1分間の敵発見数：";
            // 
            // tableLayoutPanel10
            // 
            this.tableLayoutPanel10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel10.ColumnCount = 3;
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel10.Controls.Add(this.buttonStart, 1, 0);
            this.tableLayoutPanel10.Controls.Add(this.buttonStop, 2, 0);
            this.tableLayoutPanel10.Location = new System.Drawing.Point(3, 380);
            this.tableLayoutPanel10.Name = "tableLayoutPanel10";
            this.tableLayoutPanel10.RowCount = 1;
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel10.Size = new System.Drawing.Size(223, 56);
            this.tableLayoutPanel10.TabIndex = 12;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel10, 0, 1);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.Size = new System.Drawing.Size(229, 439);
            this.tableLayoutPanel4.TabIndex = 13;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel6.ColumnCount = 1;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Controls.Add(this.labelMiniCap, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.labelSpm, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.groupBox2, 0, 2);
            this.tableLayoutPanel6.Location = new System.Drawing.Point(238, 3);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 3;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(192, 439);
            this.tableLayoutPanel6.TabIndex = 14;
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel8.ColumnCount = 2;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 198F));
            this.tableLayoutPanel8.Controls.Add(this.tableLayoutPanel4, 0, 0);
            this.tableLayoutPanel8.Controls.Add(this.tableLayoutPanel6, 1, 0);
            this.tableLayoutPanel8.Location = new System.Drawing.Point(3, 33);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 1;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(433, 445);
            this.tableLayoutPanel8.TabIndex = 16;
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.ColumnCount = 1;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.Controls.Add(this.tableLayoutPanel7, 0, 0);
            this.tableLayoutPanel9.Controls.Add(this.tableLayoutPanel8, 0, 1);
            this.tableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel9.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 2;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(439, 481);
            this.tableLayoutPanel9.TabIndex = 18;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel7.ColumnCount = 2;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.textBoxURL, 1, 0);
            this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 1;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(433, 24);
            this.tableLayoutPanel7.TabIndex = 21;
            // 
            // TabControlGShooting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel9);
            this.Name = "TabControlGShooting";
            this.Size = new System.Drawing.Size(439, 481);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel10.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel9.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            this.ResumeLayout(false);

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
        private System.Windows.Forms.CheckBox checkBoxRequest;
        private System.Windows.Forms.Label labelStateRequest;
        private System.Windows.Forms.CheckBox checkBoxRecievePresent;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DateTimePicker dateTimePickerTimeEnd;
        private System.Windows.Forms.DateTimePicker dateTimePickerTimeStart;
        private System.Windows.Forms.Label labelSpm;
        private System.Windows.Forms.CheckBox checkBoxNoSearch;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel10;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
    }
}
