namespace gcard_macro
{
    partial class FormMain
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

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.buttonRunBrowser = new System.Windows.Forms.Button();
            this.buttonStopBrowser = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageRaid = new System.Windows.Forms.TabPage();
            this.tabControlRaid = new gcard_macro.TabControlRaid();
            this.tabPageGroup = new System.Windows.Forms.TabPage();
            this.tabControlGroup = new gcard_macro.TabControlGroup();
            this.tabPageGShooting = new System.Windows.Forms.TabPage();
            this.tabControlGShooting = new gcard_macro.TabControlGShooting();
            this.tabPagePromotion = new System.Windows.Forms.TabPage();
            this.tabControlPromotion = new gcard_macro.TabControlPromotion();
            this.tabPageGTactics = new System.Windows.Forms.TabPage();
            this.tabControlGTactics = new gcard_macro.TabControlGTactics();
            this.timerWatchBrowser = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonRemoveCookie = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.textBoxLog = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxWaitReceive = new System.Windows.Forms.TextBox();
            this.textBoxWaitAttack = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxWaitBattle = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxWaitSearch = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxWaitAccessBlock = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxWaitContinueSearch = new System.Windows.Forms.TextBox();
            this.textBoxWaitMisc = new System.Windows.Forms.TextBox();
            this.checkBoxAutoRun = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.labelOptimizedWait1 = new System.Windows.Forms.Label();
            this.labelOptimizedWait2 = new System.Windows.Forms.Label();
            this.checkBoxOptimizedWait = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.tabControl1.SuspendLayout();
            this.tabPageRaid.SuspendLayout();
            this.tabPageGroup.SuspendLayout();
            this.tabPageGShooting.SuspendLayout();
            this.tabPagePromotion.SuspendLayout();
            this.tabPageGTactics.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown)).BeginInit();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonRunBrowser
            // 
            this.buttonRunBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRunBrowser.Location = new System.Drawing.Point(3, 3);
            this.buttonRunBrowser.Name = "buttonRunBrowser";
            this.buttonRunBrowser.Size = new System.Drawing.Size(167, 43);
            this.buttonRunBrowser.TabIndex = 0;
            this.buttonRunBrowser.Text = "ログイン";
            this.buttonRunBrowser.UseVisualStyleBackColor = true;
            this.buttonRunBrowser.Click += new System.EventHandler(this.buttonRunBrowser_Click);
            // 
            // buttonStopBrowser
            // 
            this.buttonStopBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStopBrowser.Location = new System.Drawing.Point(176, 3);
            this.buttonStopBrowser.Name = "buttonStopBrowser";
            this.buttonStopBrowser.Size = new System.Drawing.Size(167, 43);
            this.buttonStopBrowser.TabIndex = 1;
            this.buttonStopBrowser.Text = "ログアウト";
            this.buttonStopBrowser.UseVisualStyleBackColor = true;
            this.buttonStopBrowser.Click += new System.EventHandler(this.buttonStopBrowser_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPageRaid);
            this.tabControl1.Controls.Add(this.tabPageGroup);
            this.tabControl1.Controls.Add(this.tabPageGShooting);
            this.tabControl1.Controls.Add(this.tabPagePromotion);
            this.tabControl1.Controls.Add(this.tabPageGTactics);
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(674, 755);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPageRaid
            // 
            this.tabPageRaid.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageRaid.Controls.Add(this.tabControlRaid);
            this.tabPageRaid.Location = new System.Drawing.Point(4, 22);
            this.tabPageRaid.Name = "tabPageRaid";
            this.tabPageRaid.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageRaid.Size = new System.Drawing.Size(626, 729);
            this.tabPageRaid.TabIndex = 1;
            this.tabPageRaid.Text = "レイド";
            // 
            // tabControlRaid
            // 
            this.tabControlRaid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlRaid.Location = new System.Drawing.Point(3, 3);
            this.tabControlRaid.Name = "tabControlRaid";
            this.tabControlRaid.OptimizedWaitEnemyCount = ((uint)(0u));
            this.tabControlRaid.Size = new System.Drawing.Size(620, 723);
            this.tabControlRaid.TabIndex = 0;
            this.tabControlRaid.UserName = null;
            this.tabControlRaid.WaitAccessBlock = 0D;
            this.tabControlRaid.WaitAttack = 0D;
            this.tabControlRaid.WaitBattle = 0D;
            this.tabControlRaid.WaitContinueSearch = 0D;
            this.tabControlRaid.WaitMisc = 0D;
            this.tabControlRaid.WaitReceive = 0D;
            this.tabControlRaid.WaitSearch = 0D;
            // 
            // tabPageGroup
            // 
            this.tabPageGroup.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageGroup.Controls.Add(this.tabControlGroup);
            this.tabPageGroup.Location = new System.Drawing.Point(4, 22);
            this.tabPageGroup.Name = "tabPageGroup";
            this.tabPageGroup.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageGroup.Size = new System.Drawing.Size(666, 729);
            this.tabPageGroup.TabIndex = 2;
            this.tabPageGroup.Text = "部隊戦";
            // 
            // tabControlGroup
            // 
            this.tabControlGroup.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tabControlGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlGroup.Location = new System.Drawing.Point(3, 3);
            this.tabControlGroup.Name = "tabControlGroup";
            this.tabControlGroup.OptimizedWaitEnemyCount = ((uint)(0u));
            this.tabControlGroup.Size = new System.Drawing.Size(660, 723);
            this.tabControlGroup.TabIndex = 0;
            this.tabControlGroup.UserName = null;
            this.tabControlGroup.WaitAccessBlock = 0D;
            this.tabControlGroup.WaitAttack = 0D;
            this.tabControlGroup.WaitBattle = 0D;
            this.tabControlGroup.WaitContinueSearch = 0D;
            this.tabControlGroup.WaitMisc = 0D;
            this.tabControlGroup.WaitReceive = 0D;
            this.tabControlGroup.WaitSearch = 0D;
            // 
            // tabPageGShooting
            // 
            this.tabPageGShooting.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageGShooting.Controls.Add(this.tabControlGShooting);
            this.tabPageGShooting.Location = new System.Drawing.Point(4, 22);
            this.tabPageGShooting.Name = "tabPageGShooting";
            this.tabPageGShooting.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageGShooting.Size = new System.Drawing.Size(626, 729);
            this.tabPageGShooting.TabIndex = 0;
            this.tabPageGShooting.Text = "G-Shooting";
            // 
            // tabControlGShooting
            // 
            this.tabControlGShooting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlGShooting.Location = new System.Drawing.Point(3, 3);
            this.tabControlGShooting.Name = "tabControlGShooting";
            this.tabControlGShooting.OptimizedWaitEnemyCount = ((uint)(0u));
            this.tabControlGShooting.Size = new System.Drawing.Size(620, 723);
            this.tabControlGShooting.TabIndex = 0;
            this.tabControlGShooting.UserName = null;
            this.tabControlGShooting.WaitAccessBlock = 0D;
            this.tabControlGShooting.WaitAttack = 0D;
            this.tabControlGShooting.WaitBattle = 0D;
            this.tabControlGShooting.WaitContinueSearch = 0D;
            this.tabControlGShooting.WaitMisc = 0D;
            this.tabControlGShooting.WaitReceive = 0D;
            this.tabControlGShooting.WaitSearch = 0D;
            // 
            // tabPagePromotion
            // 
            this.tabPagePromotion.BackColor = System.Drawing.SystemColors.Control;
            this.tabPagePromotion.Controls.Add(this.tabControlPromotion);
            this.tabPagePromotion.Location = new System.Drawing.Point(4, 22);
            this.tabPagePromotion.Name = "tabPagePromotion";
            this.tabPagePromotion.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePromotion.Size = new System.Drawing.Size(626, 729);
            this.tabPagePromotion.TabIndex = 3;
            this.tabPagePromotion.Text = "昇格戦";
            // 
            // tabControlPromotion
            // 
            this.tabControlPromotion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPromotion.Location = new System.Drawing.Point(3, 3);
            this.tabControlPromotion.Name = "tabControlPromotion";
            this.tabControlPromotion.Size = new System.Drawing.Size(620, 723);
            this.tabControlPromotion.TabIndex = 0;
            this.tabControlPromotion.UserName = null;
            this.tabControlPromotion.WaitAccessBlock = 0D;
            this.tabControlPromotion.WaitAttack = 0D;
            this.tabControlPromotion.WaitBattle = 0D;
            this.tabControlPromotion.WaitContinueSearch = 0D;
            this.tabControlPromotion.WaitMisc = 0D;
            this.tabControlPromotion.WaitReceive = 0D;
            this.tabControlPromotion.WaitSearch = 0D;
            // 
            // tabPageGTactics
            // 
            this.tabPageGTactics.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageGTactics.Controls.Add(this.tabControlGTactics);
            this.tabPageGTactics.Location = new System.Drawing.Point(4, 22);
            this.tabPageGTactics.Name = "tabPageGTactics";
            this.tabPageGTactics.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageGTactics.Size = new System.Drawing.Size(626, 729);
            this.tabPageGTactics.TabIndex = 4;
            this.tabPageGTactics.Text = "G-Tactics";
            // 
            // tabControlGTactics
            // 
            this.tabControlGTactics.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlGTactics.Location = new System.Drawing.Point(3, 3);
            this.tabControlGTactics.Name = "tabControlGTactics";
            this.tabControlGTactics.OptimizedWaitEnemyCount = ((uint)(0u));
            this.tabControlGTactics.Size = new System.Drawing.Size(620, 723);
            this.tabControlGTactics.TabIndex = 0;
            this.tabControlGTactics.UserName = null;
            this.tabControlGTactics.WaitAccessBlock = 0D;
            this.tabControlGTactics.WaitAttack = 0D;
            this.tabControlGTactics.WaitBattle = 0D;
            this.tabControlGTactics.WaitContinueSearch = 0D;
            this.tabControlGTactics.WaitMisc = 0D;
            this.tabControlGTactics.WaitReceive = 0D;
            this.tabControlGTactics.WaitSearch = 0D;
            // 
            // timerWatchBrowser
            // 
            this.timerWatchBrowser.Tick += new System.EventHandler(this.timerWatchBrowser_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel5);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(683, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(358, 755);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "共通設定";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.buttonRunBrowser, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonRemoveCookie, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.buttonStopBrowser, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonSave, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 636);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(346, 98);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // buttonRemoveCookie
            // 
            this.buttonRemoveCookie.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRemoveCookie.Location = new System.Drawing.Point(176, 52);
            this.buttonRemoveCookie.Name = "buttonRemoveCookie";
            this.buttonRemoveCookie.Size = new System.Drawing.Size(167, 43);
            this.buttonRemoveCookie.TabIndex = 3;
            this.buttonRemoveCookie.Text = "Cookie削除";
            this.buttonRemoveCookie.UseVisualStyleBackColor = true;
            this.buttonRemoveCookie.Click += new System.EventHandler(this.buttonRemoveCookie_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.Location = new System.Drawing.Point(3, 52);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(167, 43);
            this.buttonSave.TabIndex = 2;
            this.buttonSave.Text = "設定保存";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // textBoxLog
            // 
            this.textBoxLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxLog.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxLog.Location = new System.Drawing.Point(3, 278);
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.ReadOnly = true;
            this.textBoxLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxLog.Size = new System.Drawing.Size(346, 352);
            this.textBoxLog.TabIndex = 6;
            this.textBoxLog.WordWrap = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tableLayoutPanel2);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(346, 195);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Wait(秒)";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 38.0828F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 61.91721F));
            this.tableLayoutPanel2.Controls.Add(this.textBoxWaitMisc, 1, 6);
            this.tableLayoutPanel2.Controls.Add(this.textBoxWaitContinueSearch, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this.label9, 0, 6);
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.textBoxWaitAccessBlock, 1, 5);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.label8, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.textBoxWaitSearch, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label5, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.textBoxWaitBattle, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.label6, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.textBoxWaitAttack, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.textBoxWaitReceive, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.label7, 0, 3);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 15);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 7;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(340, 177);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 81);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 12);
            this.label7.TabIndex = 6;
            this.label7.Text = "報酬受け取り前";
            // 
            // textBoxWaitReceive
            // 
            this.textBoxWaitReceive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxWaitReceive.Location = new System.Drawing.Point(132, 78);
            this.textBoxWaitReceive.Name = "textBoxWaitReceive";
            this.textBoxWaitReceive.Size = new System.Drawing.Size(205, 19);
            this.textBoxWaitReceive.TabIndex = 7;
            this.textBoxWaitReceive.TextChanged += new System.EventHandler(this.textBoxWaitReceive_TextChanged);
            this.textBoxWaitReceive.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxWaitReceive_KeyPress);
            this.textBoxWaitReceive.Validated += new System.EventHandler(this.textBoxWaitReceive_Validated);
            // 
            // textBoxWaitAttack
            // 
            this.textBoxWaitAttack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxWaitAttack.Location = new System.Drawing.Point(132, 53);
            this.textBoxWaitAttack.Name = "textBoxWaitAttack";
            this.textBoxWaitAttack.Size = new System.Drawing.Size(205, 19);
            this.textBoxWaitAttack.TabIndex = 5;
            this.textBoxWaitAttack.TextChanged += new System.EventHandler(this.textBoxWaitAttack_TextChanged);
            this.textBoxWaitAttack.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxWaitAttack_KeyPress);
            this.textBoxWaitAttack.Validated += new System.EventHandler(this.textBoxWaitAttack_Validated);
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 56);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 4;
            this.label6.Text = "攻撃前";
            // 
            // textBoxWaitBattle
            // 
            this.textBoxWaitBattle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxWaitBattle.Location = new System.Drawing.Point(132, 28);
            this.textBoxWaitBattle.Name = "textBoxWaitBattle";
            this.textBoxWaitBattle.Size = new System.Drawing.Size(205, 19);
            this.textBoxWaitBattle.TabIndex = 3;
            this.textBoxWaitBattle.TextChanged += new System.EventHandler(this.textBoxWaitBattle_TextChanged);
            this.textBoxWaitBattle.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxWaitBattle_KeyPress);
            this.textBoxWaitBattle.Validated += new System.EventHandler(this.textBoxWaitBattle_Validated);
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "戦闘前";
            // 
            // textBoxWaitSearch
            // 
            this.textBoxWaitSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxWaitSearch.Location = new System.Drawing.Point(132, 3);
            this.textBoxWaitSearch.Name = "textBoxWaitSearch";
            this.textBoxWaitSearch.Size = new System.Drawing.Size(205, 19);
            this.textBoxWaitSearch.TabIndex = 1;
            this.textBoxWaitSearch.TextChanged += new System.EventHandler(this.textBoxWaitSearch_TextChanged);
            this.textBoxWaitSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxWaitSearch_KeyPress);
            this.textBoxWaitSearch.Validated += new System.EventHandler(this.textBoxWaitSearch_Validated);
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 131);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 12);
            this.label8.TabIndex = 10;
            this.label8.Text = "アクセス制限中";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 106);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "探索続行時";
            // 
            // textBoxWaitAccessBlock
            // 
            this.textBoxWaitAccessBlock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxWaitAccessBlock.Location = new System.Drawing.Point(132, 128);
            this.textBoxWaitAccessBlock.Name = "textBoxWaitAccessBlock";
            this.textBoxWaitAccessBlock.Size = new System.Drawing.Size(205, 19);
            this.textBoxWaitAccessBlock.TabIndex = 11;
            this.textBoxWaitAccessBlock.TextChanged += new System.EventHandler(this.textBoxWaitAccessBlock_TextChanged);
            this.textBoxWaitAccessBlock.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxWaitAccessBlock_KeyPress);
            this.textBoxWaitAccessBlock.Validated += new System.EventHandler(this.textBoxWaitAccessBlock_Validated);
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "探索前";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 156);
            this.label9.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(123, 12);
            this.label9.TabIndex = 12;
            this.label9.Text = "その他";
            // 
            // textBoxWaitContinueSearch
            // 
            this.textBoxWaitContinueSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxWaitContinueSearch.Location = new System.Drawing.Point(132, 103);
            this.textBoxWaitContinueSearch.Name = "textBoxWaitContinueSearch";
            this.textBoxWaitContinueSearch.Size = new System.Drawing.Size(205, 19);
            this.textBoxWaitContinueSearch.TabIndex = 9;
            this.textBoxWaitContinueSearch.TextChanged += new System.EventHandler(this.textBoxWaitContinueSearch_TextChanged);
            this.textBoxWaitContinueSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxWaitContinueSearch_KeyPress);
            this.textBoxWaitContinueSearch.Validated += new System.EventHandler(this.textBoxWaitContinueSearch_Validated);
            // 
            // textBoxWaitMisc
            // 
            this.textBoxWaitMisc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxWaitMisc.Location = new System.Drawing.Point(132, 153);
            this.textBoxWaitMisc.Name = "textBoxWaitMisc";
            this.textBoxWaitMisc.Size = new System.Drawing.Size(205, 19);
            this.textBoxWaitMisc.TabIndex = 13;
            this.textBoxWaitMisc.TextChanged += new System.EventHandler(this.textBoxWaitMisc_TextChanged);
            this.textBoxWaitMisc.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxWaitMisc_KeyPress);
            this.textBoxWaitMisc.Validated += new System.EventHandler(this.textBoxWaitMisc_Validated);
            // 
            // checkBoxAutoRun
            // 
            this.checkBoxAutoRun.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.checkBoxAutoRun.AutoSize = true;
            this.checkBoxAutoRun.Location = new System.Drawing.Point(3, 256);
            this.checkBoxAutoRun.Name = "checkBoxAutoRun";
            this.checkBoxAutoRun.Size = new System.Drawing.Size(84, 16);
            this.checkBoxAutoRun.TabIndex = 5;
            this.checkBoxAutoRun.Text = "自動ログイン";
            this.checkBoxAutoRun.UseVisualStyleBackColor = true;
            this.checkBoxAutoRun.CheckedChanged += new System.EventHandler(this.onSettingChanged);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel3.ColumnCount = 4;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.Controls.Add(this.labelOptimizedWait2, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.labelOptimizedWait1, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.numericUpDown, 2, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 226);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(346, 24);
            this.tableLayoutPanel3.TabIndex = 2;
            // 
            // numericUpDown
            // 
            this.numericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDown.Location = new System.Drawing.Point(132, 3);
            this.numericUpDown.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.numericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown.Name = "numericUpDown";
            this.numericUpDown.Size = new System.Drawing.Size(39, 19);
            this.numericUpDown.TabIndex = 3;
            this.numericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // labelOptimizedWait1
            // 
            this.labelOptimizedWait1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelOptimizedWait1.AutoSize = true;
            this.labelOptimizedWait1.Location = new System.Drawing.Point(23, 6);
            this.labelOptimizedWait1.Name = "labelOptimizedWait1";
            this.labelOptimizedWait1.Size = new System.Drawing.Size(103, 12);
            this.labelOptimizedWait1.TabIndex = 2;
            this.labelOptimizedWait1.Text = "1分間の敵発見数が";
            // 
            // labelOptimizedWait2
            // 
            this.labelOptimizedWait2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelOptimizedWait2.AutoSize = true;
            this.labelOptimizedWait2.Location = new System.Drawing.Point(177, 6);
            this.labelOptimizedWait2.Name = "labelOptimizedWait2";
            this.labelOptimizedWait2.Size = new System.Drawing.Size(113, 12);
            this.labelOptimizedWait2.TabIndex = 4;
            this.labelOptimizedWait2.Text = "体になるように調整する";
            // 
            // checkBoxOptimizedWait
            // 
            this.checkBoxOptimizedWait.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.checkBoxOptimizedWait.AutoSize = true;
            this.checkBoxOptimizedWait.Checked = true;
            this.checkBoxOptimizedWait.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxOptimizedWait.Location = new System.Drawing.Point(3, 204);
            this.checkBoxOptimizedWait.Name = "checkBoxOptimizedWait";
            this.checkBoxOptimizedWait.Size = new System.Drawing.Size(148, 16);
            this.checkBoxOptimizedWait.TabIndex = 1;
            this.checkBoxOptimizedWait.Text = "待機時間を自動調整する";
            this.checkBoxOptimizedWait.UseVisualStyleBackColor = true;
            this.checkBoxOptimizedWait.CheckedChanged += new System.EventHandler(this.checkBoxOptimizedWait_CheckedChanged);
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel1, 0, 5);
            this.tableLayoutPanel5.Controls.Add(this.textBoxLog, 0, 4);
            this.tableLayoutPanel5.Controls.Add(this.checkBoxOptimizedWait, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel3, 0, 2);
            this.tableLayoutPanel5.Controls.Add(this.checkBoxAutoRun, 0, 3);
            this.tableLayoutPanel5.Controls.Add(this.groupBox2, 0, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 15);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 6;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.Size = new System.Drawing.Size(352, 737);
            this.tableLayoutPanel5.TabIndex = 4;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 364F));
            this.tableLayoutPanel4.Controls.Add(this.tabControl1, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.groupBox1, 1, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1044, 761);
            this.tableLayoutPanel4.TabIndex = 2;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1044, 761);
            this.Controls.Add(this.tableLayoutPanel4);
            this.Name = "FormMain";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
            this.Shown += new System.EventHandler(this.FormMain_Shown);
            this.tabControl1.ResumeLayout(false);
            this.tabPageRaid.ResumeLayout(false);
            this.tabPageGroup.ResumeLayout(false);
            this.tabPageGShooting.ResumeLayout(false);
            this.tabPagePromotion.ResumeLayout(false);
            this.tabPageGTactics.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown)).EndInit();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonRunBrowser;
        private System.Windows.Forms.Button buttonStopBrowser;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageRaid;
        private TabControlRaid tabControlRaid;
        private System.Windows.Forms.Timer timerWatchBrowser;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.TabPage tabPageGroup;
        private TabControlGroup tabControlGroup;
        private System.Windows.Forms.TabPage tabPageGShooting;
        private TabControlGShooting tabControlGShooting;
        private System.Windows.Forms.TabPage tabPagePromotion;
        private TabControlPromotion tabControlPromotion;
        private System.Windows.Forms.TabPage tabPageGTactics;
        private TabControlGTactics tabControlGTactics;
        private System.Windows.Forms.Button buttonRemoveCookie;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox textBoxLog;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.CheckBox checkBoxOptimizedWait;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label labelOptimizedWait2;
        private System.Windows.Forms.Label labelOptimizedWait1;
        private System.Windows.Forms.NumericUpDown numericUpDown;
        private System.Windows.Forms.CheckBox checkBoxAutoRun;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TextBox textBoxWaitMisc;
        private System.Windows.Forms.TextBox textBoxWaitContinueSearch;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxWaitAccessBlock;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxWaitSearch;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxWaitBattle;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxWaitAttack;
        private System.Windows.Forms.TextBox textBoxWaitReceive;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
    }
}

