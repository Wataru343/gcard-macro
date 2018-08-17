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
            this.timerWatchBrowser = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBoxWaitMisc = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxWaitAccessBlock = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxWaitReceive = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxWaitAttack = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxWaitBattle = new System.Windows.Forms.TextBox();
            this.textBoxWaitSearch = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonSave = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPageRaid.SuspendLayout();
            this.tabPageGroup.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonRunBrowser
            // 
            this.buttonRunBrowser.Location = new System.Drawing.Point(12, 12);
            this.buttonRunBrowser.Name = "buttonRunBrowser";
            this.buttonRunBrowser.Size = new System.Drawing.Size(107, 57);
            this.buttonRunBrowser.TabIndex = 0;
            this.buttonRunBrowser.Text = "Chrome起動";
            this.buttonRunBrowser.UseVisualStyleBackColor = true;
            this.buttonRunBrowser.Click += new System.EventHandler(this.buttonRunBrowser_Click);
            // 
            // buttonStopBrowser
            // 
            this.buttonStopBrowser.Location = new System.Drawing.Point(125, 12);
            this.buttonStopBrowser.Name = "buttonStopBrowser";
            this.buttonStopBrowser.Size = new System.Drawing.Size(107, 57);
            this.buttonStopBrowser.TabIndex = 1;
            this.buttonStopBrowser.Text = "Chrome停止";
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
            this.tabControl1.Location = new System.Drawing.Point(12, 321);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(946, 562);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPageRaid
            // 
            this.tabPageRaid.Controls.Add(this.tabControlRaid);
            this.tabPageRaid.Location = new System.Drawing.Point(4, 22);
            this.tabPageRaid.Name = "tabPageRaid";
            this.tabPageRaid.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageRaid.Size = new System.Drawing.Size(938, 536);
            this.tabPageRaid.TabIndex = 1;
            this.tabPageRaid.Text = "レイド";
            this.tabPageRaid.UseVisualStyleBackColor = true;
            // 
            // tabControlRaid
            // 
            this.tabControlRaid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlRaid.Location = new System.Drawing.Point(3, 6);
            this.tabControlRaid.Name = "tabControlRaid";
            this.tabControlRaid.Size = new System.Drawing.Size(926, 432);
            this.tabControlRaid.TabIndex = 0;
            this.tabControlRaid.WaitAccessBlock = 0D;
            this.tabControlRaid.WaitAttack = 0D;
            this.tabControlRaid.WaitBattle = 0D;
            this.tabControlRaid.WaitMisc = 0D;
            this.tabControlRaid.WaitReceive = 0D;
            this.tabControlRaid.WaitSearch = 0D;
            // 
            // tabPageGroup
            // 
            this.tabPageGroup.Controls.Add(this.tabControlGroup);
            this.tabPageGroup.Location = new System.Drawing.Point(4, 22);
            this.tabPageGroup.Name = "tabPageGroup";
            this.tabPageGroup.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageGroup.Size = new System.Drawing.Size(938, 536);
            this.tabPageGroup.TabIndex = 2;
            this.tabPageGroup.Text = "部隊戦";
            this.tabPageGroup.UseVisualStyleBackColor = true;
            // 
            // tabControlGroup
            // 
            this.tabControlGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlGroup.Location = new System.Drawing.Point(0, 4);
            this.tabControlGroup.Name = "tabControlGroup";
            this.tabControlGroup.Size = new System.Drawing.Size(938, 532);
            this.tabControlGroup.TabIndex = 0;
            this.tabControlGroup.WaitAccessBlock = 0D;
            this.tabControlGroup.WaitAttack = 0D;
            this.tabControlGroup.WaitBattle = 0D;
            this.tabControlGroup.WaitMisc = 0D;
            this.tabControlGroup.WaitReceive = 0D;
            this.tabControlGroup.WaitSearch = 0D;
            // 
            // timerWatchBrowser
            // 
            this.timerWatchBrowser.Tick += new System.EventHandler(this.timerWatchBrowser_Tick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Location = new System.Drawing.Point(16, 76);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(935, 239);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "共通設定";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.textBoxWaitMisc);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.textBoxWaitAccessBlock);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.textBoxWaitReceive);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.textBoxWaitAttack);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.textBoxWaitBattle);
            this.groupBox2.Controls.Add(this.textBoxWaitSearch);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(16, 18);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(904, 174);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Wait(秒)";
            // 
            // textBoxWaitMisc
            // 
            this.textBoxWaitMisc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxWaitMisc.Location = new System.Drawing.Point(118, 141);
            this.textBoxWaitMisc.Name = "textBoxWaitMisc";
            this.textBoxWaitMisc.Size = new System.Drawing.Size(779, 19);
            this.textBoxWaitMisc.TabIndex = 11;
            this.textBoxWaitMisc.TextChanged += new System.EventHandler(this.textBoxWaitMisc_TextChanged);
            this.textBoxWaitMisc.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxWaitMisc_KeyPress);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 144);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(36, 12);
            this.label9.TabIndex = 10;
            this.label9.Text = "その他";
            // 
            // textBoxWaitAccessBlock
            // 
            this.textBoxWaitAccessBlock.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxWaitAccessBlock.Location = new System.Drawing.Point(118, 116);
            this.textBoxWaitAccessBlock.Name = "textBoxWaitAccessBlock";
            this.textBoxWaitAccessBlock.Size = new System.Drawing.Size(780, 19);
            this.textBoxWaitAccessBlock.TabIndex = 9;
            this.textBoxWaitAccessBlock.TextChanged += new System.EventHandler(this.textBoxWaitAccessBlock_TextChanged);
            this.textBoxWaitAccessBlock.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxWaitAccessBlock_KeyPress);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 119);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 12);
            this.label8.TabIndex = 8;
            this.label8.Text = "アクセス制限中";
            // 
            // textBoxWaitReceive
            // 
            this.textBoxWaitReceive.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxWaitReceive.Location = new System.Drawing.Point(118, 91);
            this.textBoxWaitReceive.Name = "textBoxWaitReceive";
            this.textBoxWaitReceive.Size = new System.Drawing.Size(780, 19);
            this.textBoxWaitReceive.TabIndex = 7;
            this.textBoxWaitReceive.TextChanged += new System.EventHandler(this.textBoxWaitReceive_TextChanged);
            this.textBoxWaitReceive.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxWaitReceive_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 94);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 12);
            this.label7.TabIndex = 6;
            this.label7.Text = "報酬受け取り前";
            // 
            // textBoxWaitAttack
            // 
            this.textBoxWaitAttack.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxWaitAttack.Location = new System.Drawing.Point(118, 66);
            this.textBoxWaitAttack.Name = "textBoxWaitAttack";
            this.textBoxWaitAttack.Size = new System.Drawing.Size(780, 19);
            this.textBoxWaitAttack.TabIndex = 5;
            this.textBoxWaitAttack.TextChanged += new System.EventHandler(this.textBoxWaitAttack_TextChanged);
            this.textBoxWaitAttack.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxWaitAttack_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 69);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 4;
            this.label6.Text = "攻撃前";
            // 
            // textBoxWaitBattle
            // 
            this.textBoxWaitBattle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxWaitBattle.Location = new System.Drawing.Point(118, 41);
            this.textBoxWaitBattle.Name = "textBoxWaitBattle";
            this.textBoxWaitBattle.Size = new System.Drawing.Size(780, 19);
            this.textBoxWaitBattle.TabIndex = 3;
            this.textBoxWaitBattle.TextChanged += new System.EventHandler(this.textBoxWaitBattle_TextChanged);
            this.textBoxWaitBattle.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxWaitBattle_KeyPress);
            // 
            // textBoxWaitSearch
            // 
            this.textBoxWaitSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxWaitSearch.Location = new System.Drawing.Point(118, 16);
            this.textBoxWaitSearch.Name = "textBoxWaitSearch";
            this.textBoxWaitSearch.Size = new System.Drawing.Size(780, 19);
            this.textBoxWaitSearch.TabIndex = 1;
            this.textBoxWaitSearch.TextChanged += new System.EventHandler(this.textBoxWaitSearch_TextChanged);
            this.textBoxWaitSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxWaitSearch_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "戦闘前";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "探索前";
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(238, 12);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(107, 57);
            this.buttonSave.TabIndex = 6;
            this.buttonSave.Text = "設定保存";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(970, 895);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.buttonStopBrowser);
            this.Controls.Add(this.buttonRunBrowser);
            this.Name = "FormMain";
            this.Text = "ガンダムカードコレクション自動化ツール Ver0.2.0β";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
            this.tabControl1.ResumeLayout(false);
            this.tabPageRaid.ResumeLayout(false);
            this.tabPageGroup.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonRunBrowser;
        private System.Windows.Forms.Button buttonStopBrowser;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageRaid;
        private TabControlRaid tabControlRaid;
        private System.Windows.Forms.Timer timerWatchBrowser;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBoxWaitMisc;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxWaitAccessBlock;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxWaitReceive;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxWaitAttack;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxWaitBattle;
        private System.Windows.Forms.TextBox textBoxWaitSearch;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.TabPage tabPageGroup;
        private TabControlGroup tabControlGroup;
    }
}

