namespace Com.ZimVie.Wcs.ZWCS
{
    partial class LabelPrintForManualInputForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LabelPrintForManualInputForm));
            this.ExpirationDate_lbl = new Com.ZimVie.Wcs.Framework.LabelCommon();
            this.ExpirationDate_dtp = new Com.ZimVie.Wcs.Framework.DateTimePickerCommon();
            this.LotNumber_txt = new Com.ZimVie.Wcs.Framework.TextBoxCommon();
            this.LotNumber_lbl = new Com.ZimVie.Wcs.Framework.LabelCommon();
            this.ItemNumber_txt = new Com.ZimVie.Wcs.Framework.TextBoxCommon();
            this.ItemNumber_lbl = new Com.ZimVie.Wcs.Framework.LabelCommon();
            this.ItemName_txt = new Com.ZimVie.Wcs.Framework.TextBoxCommon();
            this.ItemName_lbl = new Com.ZimVie.Wcs.Framework.LabelCommon();
            this.Exit_btn = new Com.ZimVie.Wcs.Framework.ButtonCommon();
            this.PrintLabel_btn = new Com.ZimVie.Wcs.Framework.ButtonCommon();
            this.LabelQuantity_txt = new Com.ZimVie.Wcs.Framework.TextBoxCommon();
            this.labelQuantity_lbl = new Com.ZimVie.Wcs.Framework.LabelCommon();
            this.Clear_btn = new Com.ZimVie.Wcs.Framework.ButtonCommon();
            this.LabelType_txt = new Com.ZimVie.Wcs.Framework.TextBoxCommon();
            this.labelCommon1 = new Com.ZimVie.Wcs.Framework.LabelCommon();
            this.SuspendLayout();
            // 
            // ExpirationDate_lbl
            // 
            this.ExpirationDate_lbl.AutoSize = true;
            this.ExpirationDate_lbl.ControlId = null;
            this.ExpirationDate_lbl.Font = new System.Drawing.Font("Arial", 9F);
            this.ExpirationDate_lbl.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ExpirationDate_lbl.Location = new System.Drawing.Point(28, 311);
            this.ExpirationDate_lbl.Name = "ExpirationDate_lbl";
            this.ExpirationDate_lbl.Size = new System.Drawing.Size(55, 15);
            this.ExpirationDate_lbl.TabIndex = 103;
            this.ExpirationDate_lbl.Text = "使用期限";
            // 
            // ExpirationDate_dtp
            // 
            this.ExpirationDate_dtp.BackColor = System.Drawing.SystemColors.Control;
            this.ExpirationDate_dtp.CalendarMonthBackground = System.Drawing.SystemColors.ControlLight;
            this.ExpirationDate_dtp.ControlId = null;
            this.ExpirationDate_dtp.CustomFormat = "yyyy-MM-dd";
            this.ExpirationDate_dtp.DisplayFormat = Com.ZimVie.Wcs.Framework.DateTimePickerCommon.DisplayFormatList.ShortDatePattern;
            this.ExpirationDate_dtp.Font = new System.Drawing.Font("Arial", 9.75F);
            this.ExpirationDate_dtp.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ExpirationDate_dtp.Location = new System.Drawing.Point(137, 305);
            this.ExpirationDate_dtp.Name = "ExpirationDate_dtp";
            this.ExpirationDate_dtp.Size = new System.Drawing.Size(185, 22);
            this.ExpirationDate_dtp.TabIndex = 2;
            // 
            // LotNumber_txt
            // 
            this.LotNumber_txt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.LotNumber_txt.ControlId = null;
            this.LotNumber_txt.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LotNumber_txt.InputType = Com.ZimVie.Wcs.Framework.TextBoxCommon.InputTypeList.All;
            this.LotNumber_txt.Location = new System.Drawing.Point(137, 265);
            this.LotNumber_txt.MaxLength = 20;
            this.LotNumber_txt.Name = "LotNumber_txt";
            this.LotNumber_txt.Size = new System.Drawing.Size(185, 21);
            this.LotNumber_txt.TabIndex = 1;
            // 
            // LotNumber_lbl
            // 
            this.LotNumber_lbl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LotNumber_lbl.AutoSize = true;
            this.LotNumber_lbl.ControlId = null;
            this.LotNumber_lbl.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LotNumber_lbl.Location = new System.Drawing.Point(28, 268);
            this.LotNumber_lbl.Name = "LotNumber_lbl";
            this.LotNumber_lbl.Size = new System.Drawing.Size(55, 15);
            this.LotNumber_lbl.TabIndex = 102;
            this.LotNumber_lbl.Text = "ロット番号";
            // 
            // ItemNumber_txt
            // 
            this.ItemNumber_txt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ItemNumber_txt.ControlId = null;
            this.ItemNumber_txt.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ItemNumber_txt.InputType = Com.ZimVie.Wcs.Framework.TextBoxCommon.InputTypeList.All;
            this.ItemNumber_txt.Location = new System.Drawing.Point(137, 124);
            this.ItemNumber_txt.MaxLength = 12;
            this.ItemNumber_txt.Name = "ItemNumber_txt";
            this.ItemNumber_txt.Size = new System.Drawing.Size(185, 21);
            this.ItemNumber_txt.TabIndex = 1;
            this.ItemNumber_txt.Leave += new System.EventHandler(this.ItemNumber_txt_Leave);
            // 
            // ItemNumber_lbl
            // 
            this.ItemNumber_lbl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ItemNumber_lbl.AutoSize = true;
            this.ItemNumber_lbl.ControlId = null;
            this.ItemNumber_lbl.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ItemNumber_lbl.Location = new System.Drawing.Point(28, 127);
            this.ItemNumber_lbl.Name = "ItemNumber_lbl";
            this.ItemNumber_lbl.Size = new System.Drawing.Size(55, 15);
            this.ItemNumber_lbl.TabIndex = 105;
            this.ItemNumber_lbl.Text = "製品番号";
            // 
            // ItemName_txt
            // 
            this.ItemName_txt.ControlId = null;
            this.ItemName_txt.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ItemName_txt.InputType = Com.ZimVie.Wcs.Framework.TextBoxCommon.InputTypeList.All;
            this.ItemName_txt.Location = new System.Drawing.Point(137, 162);
            this.ItemName_txt.MaxLength = 38;
            this.ItemName_txt.Multiline = true;
            this.ItemName_txt.Name = "ItemName_txt";
            this.ItemName_txt.ReadOnly = true;
            this.ItemName_txt.Size = new System.Drawing.Size(185, 44);
            this.ItemName_txt.TabIndex = 100;
            this.ItemName_txt.TabStop = false;
            // 
            // ItemName_lbl
            // 
            this.ItemName_lbl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ItemName_lbl.AutoSize = true;
            this.ItemName_lbl.ControlId = null;
            this.ItemName_lbl.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ItemName_lbl.Location = new System.Drawing.Point(28, 165);
            this.ItemName_lbl.Name = "ItemName_lbl";
            this.ItemName_lbl.Size = new System.Drawing.Size(43, 15);
            this.ItemName_lbl.TabIndex = 107;
            this.ItemName_lbl.Text = "製品名";
            // 
            // Exit_btn
            // 
            this.Exit_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Exit_btn.BackColor = System.Drawing.SystemColors.Control;
            this.Exit_btn.ControlId = null;
            this.Exit_btn.Font = new System.Drawing.Font("Arial", 9F);
            this.Exit_btn.Location = new System.Drawing.Point(265, 403);
            this.Exit_btn.Name = "Exit_btn";
            this.Exit_btn.Size = new System.Drawing.Size(80, 30);
            this.Exit_btn.TabIndex = 6;
            this.Exit_btn.Text = "閉じる";
            this.Exit_btn.UseVisualStyleBackColor = false;
            this.Exit_btn.Click += new System.EventHandler(this.Exit_btn_Click);
            // 
            // PrintLabel_btn
            // 
            this.PrintLabel_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.PrintLabel_btn.BackColor = System.Drawing.SystemColors.Control;
            this.PrintLabel_btn.ControlId = null;
            this.PrintLabel_btn.Font = new System.Drawing.Font("Arial", 9F);
            this.PrintLabel_btn.Location = new System.Drawing.Point(147, 403);
            this.PrintLabel_btn.Name = "PrintLabel_btn";
            this.PrintLabel_btn.Size = new System.Drawing.Size(96, 30);
            this.PrintLabel_btn.TabIndex = 5;
            this.PrintLabel_btn.Text = "ラベル発行";
            this.PrintLabel_btn.UseVisualStyleBackColor = false;
            this.PrintLabel_btn.Click += new System.EventHandler(this.PrintLabel_btn_Click);
            // 
            // LabelQuantity_txt
            // 
            this.LabelQuantity_txt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.LabelQuantity_txt.ControlId = null;
            this.LabelQuantity_txt.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelQuantity_txt.InputType = Com.ZimVie.Wcs.Framework.TextBoxCommon.InputTypeList.Numeric;
            this.LabelQuantity_txt.Location = new System.Drawing.Point(137, 343);
            this.LabelQuantity_txt.MaxLength = 2;
            this.LabelQuantity_txt.Name = "LabelQuantity_txt";
            this.LabelQuantity_txt.Size = new System.Drawing.Size(185, 21);
            this.LabelQuantity_txt.TabIndex = 3;
            // 
            // labelQuantity_lbl
            // 
            this.labelQuantity_lbl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelQuantity_lbl.AutoSize = true;
            this.labelQuantity_lbl.ControlId = null;
            this.labelQuantity_lbl.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelQuantity_lbl.Location = new System.Drawing.Point(28, 346);
            this.labelQuantity_lbl.Name = "labelQuantity_lbl";
            this.labelQuantity_lbl.Size = new System.Drawing.Size(31, 15);
            this.labelQuantity_lbl.TabIndex = 112;
            this.labelQuantity_lbl.Text = "枚数";
            // 
            // Clear_btn
            // 
            this.Clear_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Clear_btn.BackColor = System.Drawing.SystemColors.Control;
            this.Clear_btn.ControlId = null;
            this.Clear_btn.Font = new System.Drawing.Font("Arial", 9F);
            this.Clear_btn.Location = new System.Drawing.Point(41, 403);
            this.Clear_btn.Name = "Clear_btn";
            this.Clear_btn.Size = new System.Drawing.Size(83, 30);
            this.Clear_btn.TabIndex = 4;
            this.Clear_btn.Text = "クリア";
            this.Clear_btn.UseVisualStyleBackColor = false;
            this.Clear_btn.Click += new System.EventHandler(this.Clear_btn_Click);
            // 
            // LabelType_txt
            // 
            this.LabelType_txt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.LabelType_txt.ControlId = null;
            this.LabelType_txt.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelType_txt.InputType = Com.ZimVie.Wcs.Framework.TextBoxCommon.InputTypeList.All;
            this.LabelType_txt.Location = new System.Drawing.Point(137, 226);
            this.LabelType_txt.MaxLength = 8;
            this.LabelType_txt.Name = "LabelType_txt";
            this.LabelType_txt.ReadOnly = true;
            this.LabelType_txt.Size = new System.Drawing.Size(185, 21);
            this.LabelType_txt.TabIndex = 100;
            this.LabelType_txt.TabStop = false;
            // 
            // labelCommon1
            // 
            this.labelCommon1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelCommon1.AutoSize = true;
            this.labelCommon1.ControlId = null;
            this.labelCommon1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCommon1.Location = new System.Drawing.Point(28, 229);
            this.labelCommon1.Name = "labelCommon1";
            this.labelCommon1.Size = new System.Drawing.Size(59, 15);
            this.labelCommon1.TabIndex = 114;
            this.labelCommon1.Text = "ラベル種類";
            // 
            // LabelPrintForManualInputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 456);
            this.Controls.Add(this.LabelType_txt);
            this.Controls.Add(this.labelCommon1);
            this.Controls.Add(this.Clear_btn);
            this.Controls.Add(this.LabelQuantity_txt);
            this.Controls.Add(this.labelQuantity_lbl);
            this.Controls.Add(this.PrintLabel_btn);
            this.Controls.Add(this.Exit_btn);
            this.Controls.Add(this.ItemName_txt);
            this.Controls.Add(this.ItemName_lbl);
            this.Controls.Add(this.ItemNumber_txt);
            this.Controls.Add(this.ItemNumber_lbl);
            this.Controls.Add(this.ExpirationDate_lbl);
            this.Controls.Add(this.ExpirationDate_dtp);
            this.Controls.Add(this.LotNumber_txt);
            this.Controls.Add(this.LotNumber_lbl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LabelPrintForManualInputForm";
            this.Text = "ラベル手入力印刷";
            this.TitleText = "ラベル手入力印刷";
            this.Load += new System.EventHandler(this.LabelPrintForManualInputForm_Load);
            this.Controls.SetChildIndex(this.LotNumber_lbl, 0);
            this.Controls.SetChildIndex(this.LotNumber_txt, 0);
            this.Controls.SetChildIndex(this.ExpirationDate_dtp, 0);
            this.Controls.SetChildIndex(this.ExpirationDate_lbl, 0);
            this.Controls.SetChildIndex(this.ItemNumber_lbl, 0);
            this.Controls.SetChildIndex(this.ItemNumber_txt, 0);
            this.Controls.SetChildIndex(this.ItemName_lbl, 0);
            this.Controls.SetChildIndex(this.ItemName_txt, 0);
            this.Controls.SetChildIndex(this.Exit_btn, 0);
            this.Controls.SetChildIndex(this.PrintLabel_btn, 0);
            this.Controls.SetChildIndex(this.labelQuantity_lbl, 0);
            this.Controls.SetChildIndex(this.LabelQuantity_txt, 0);
            this.Controls.SetChildIndex(this.Clear_btn, 0);
            this.Controls.SetChildIndex(this.labelCommon1, 0);
            this.Controls.SetChildIndex(this.LabelType_txt, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Framework.LabelCommon ExpirationDate_lbl;
        private Framework.DateTimePickerCommon ExpirationDate_dtp;
        public Framework.TextBoxCommon LotNumber_txt;
        private Framework.LabelCommon LotNumber_lbl;
        public Framework.TextBoxCommon ItemNumber_txt;
        private Framework.LabelCommon ItemNumber_lbl;
        public Framework.TextBoxCommon ItemName_txt;
        private Framework.LabelCommon ItemName_lbl;
        private Framework.ButtonCommon Exit_btn;
        private Framework.ButtonCommon PrintLabel_btn;
        public Framework.TextBoxCommon LabelQuantity_txt;
        private Framework.LabelCommon labelQuantity_lbl;
        private Framework.ButtonCommon Clear_btn;
        public Framework.TextBoxCommon LabelType_txt;
        private Framework.LabelCommon labelCommon1;
    }
}

