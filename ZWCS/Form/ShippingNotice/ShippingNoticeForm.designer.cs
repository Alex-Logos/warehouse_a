namespace Com.ZimVie.Wcs.ZWCS
{
    partial class ShippingNoticeForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ShippingNoticeTrackingNumber_txt = new Com.ZimVie.Wcs.Framework.TextBoxCommon();
            this.TrackingNumber_lbl = new Com.ZimVie.Wcs.Framework.LabelCommon();
            this.ShippingNotice_dgv = new Com.ZimVie.Wcs.Framework.DataGridViewCommon();
            this.Exit_btn = new Com.ZimVie.Wcs.Framework.ButtonCommon();
            this.Import_btn = new Com.ZimVie.Wcs.Framework.ButtonCommon();
            this.SupplierNumberAndName_cmb = new Com.ZimVie.Wcs.Framework.ComboBoxCommon();
            this.SupplierNumberAndName_lbl = new Com.ZimVie.Wcs.Framework.LabelCommon();
            this.ShippingNoticeIssueDate_lbl = new Com.ZimVie.Wcs.Framework.LabelCommon();
            this.ShippingNoticeIssueDate_dtp = new Com.ZimVie.Wcs.Framework.DateTimePickerCommon();
            this.CreateWorkOrder_btn = new Com.ZimVie.Wcs.Framework.ButtonCommon();
            this.labelCommon2 = new Com.ZimVie.Wcs.Framework.LabelCommon();
            this.labelCommon1 = new Com.ZimVie.Wcs.Framework.LabelCommon();
            this.labelCommon3 = new Com.ZimVie.Wcs.Framework.LabelCommon();
            this.colShippingNoticeLineID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPurchaseOrderNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colInvoiceNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colItemNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSupplierItemNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLotNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLotQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLotExpirationDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.ShippingNotice_dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // ShippingNoticeTrackingNumber_txt
            // 
            this.ShippingNoticeTrackingNumber_txt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ShippingNoticeTrackingNumber_txt.ControlId = null;
            this.ShippingNoticeTrackingNumber_txt.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ShippingNoticeTrackingNumber_txt.InputType = Com.ZimVie.Wcs.Framework.TextBoxCommon.InputTypeList.All;
            this.ShippingNoticeTrackingNumber_txt.Location = new System.Drawing.Point(277, 166);
            this.ShippingNoticeTrackingNumber_txt.MaxLength = 16;
            this.ShippingNoticeTrackingNumber_txt.Name = "ShippingNoticeTrackingNumber_txt";
            this.ShippingNoticeTrackingNumber_txt.Size = new System.Drawing.Size(200, 21);
            this.ShippingNoticeTrackingNumber_txt.TabIndex = 1;
            // 
            // TrackingNumber_lbl
            // 
            this.TrackingNumber_lbl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TrackingNumber_lbl.AutoSize = true;
            this.TrackingNumber_lbl.ControlId = null;
            this.TrackingNumber_lbl.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TrackingNumber_lbl.Location = new System.Drawing.Point(71, 169);
            this.TrackingNumber_lbl.Name = "TrackingNumber_lbl";
            this.TrackingNumber_lbl.Size = new System.Drawing.Size(103, 15);
            this.TrackingNumber_lbl.TabIndex = 29;
            this.TrackingNumber_lbl.Text = "出荷通知追跡番号";
            // 
            // ShippingNotice_dgv
            // 
            this.ShippingNotice_dgv.AllowUserToAddRows = false;
            this.ShippingNotice_dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(232)))), ((int)(((byte)(180)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ShippingNotice_dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.ShippingNotice_dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ShippingNotice_dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colShippingNoticeLineID,
            this.colPurchaseOrderNumber,
            this.colInvoiceNumber,
            this.colItemNumber,
            this.colSupplierItemNumber,
            this.colLotNumber,
            this.colLotQuantity,
            this.colLotExpirationDate});
            this.ShippingNotice_dgv.ControlId = null;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ShippingNotice_dgv.DefaultCellStyle = dataGridViewCellStyle2;
            this.ShippingNotice_dgv.EnableHeadersVisualStyles = false;
            this.ShippingNotice_dgv.Location = new System.Drawing.Point(65, 267);
            this.ShippingNotice_dgv.Name = "ShippingNotice_dgv";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(232)))), ((int)(((byte)(180)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ShippingNotice_dgv.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.ShippingNotice_dgv.RowHeadersVisible = false;
            this.ShippingNotice_dgv.RowTemplate.Height = 21;
            this.ShippingNotice_dgv.Size = new System.Drawing.Size(903, 440);
            this.ShippingNotice_dgv.TabIndex = 3;
            // 
            // Exit_btn
            // 
            this.Exit_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Exit_btn.BackColor = System.Drawing.SystemColors.Control;
            this.Exit_btn.ControlId = null;
            this.Exit_btn.Font = new System.Drawing.Font("Arial", 9F);
            this.Exit_btn.Location = new System.Drawing.Point(906, 741);
            this.Exit_btn.Name = "Exit_btn";
            this.Exit_btn.Size = new System.Drawing.Size(80, 30);
            this.Exit_btn.TabIndex = 6;
            this.Exit_btn.Text = "閉じる";
            this.Exit_btn.UseVisualStyleBackColor = false;
            this.Exit_btn.Click += new System.EventHandler(this.Exit_btn_Click);
            // 
            // Import_btn
            // 
            this.Import_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Import_btn.BackColor = System.Drawing.SystemColors.Control;
            this.Import_btn.ControlId = null;
            this.Import_btn.Font = new System.Drawing.Font("Arial", 9F);
            this.Import_btn.Location = new System.Drawing.Point(596, 741);
            this.Import_btn.Name = "Import_btn";
            this.Import_btn.Size = new System.Drawing.Size(115, 30);
            this.Import_btn.TabIndex = 4;
            this.Import_btn.Text = "出荷通知取込";
            this.Import_btn.UseVisualStyleBackColor = false;
            this.Import_btn.Click += new System.EventHandler(this.Import_btn_Click);
            // 
            // SupplierNumberAndName_cmb
            // 
            this.SupplierNumberAndName_cmb.ControlId = null;
            this.SupplierNumberAndName_cmb.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SupplierNumberAndName_cmb.FormattingEnabled = true;
            this.SupplierNumberAndName_cmb.Location = new System.Drawing.Point(277, 122);
            this.SupplierNumberAndName_cmb.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SupplierNumberAndName_cmb.Name = "SupplierNumberAndName_cmb";
            this.SupplierNumberAndName_cmb.Size = new System.Drawing.Size(327, 23);
            this.SupplierNumberAndName_cmb.TabIndex = 0;
            // 
            // SupplierNumberAndName_lbl
            // 
            this.SupplierNumberAndName_lbl.AutoSize = true;
            this.SupplierNumberAndName_lbl.ControlId = null;
            this.SupplierNumberAndName_lbl.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SupplierNumberAndName_lbl.Location = new System.Drawing.Point(71, 125);
            this.SupplierNumberAndName_lbl.Name = "SupplierNumberAndName_lbl";
            this.SupplierNumberAndName_lbl.Size = new System.Drawing.Size(43, 15);
            this.SupplierNumberAndName_lbl.TabIndex = 86;
            this.SupplierNumberAndName_lbl.Text = "仕入先";
            // 
            // ShippingNoticeIssueDate_lbl
            // 
            this.ShippingNoticeIssueDate_lbl.AutoSize = true;
            this.ShippingNoticeIssueDate_lbl.ControlId = null;
            this.ShippingNoticeIssueDate_lbl.Font = new System.Drawing.Font("Arial", 9F);
            this.ShippingNoticeIssueDate_lbl.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ShippingNoticeIssueDate_lbl.Location = new System.Drawing.Point(71, 218);
            this.ShippingNoticeIssueDate_lbl.Name = "ShippingNoticeIssueDate_lbl";
            this.ShippingNoticeIssueDate_lbl.Size = new System.Drawing.Size(91, 15);
            this.ShippingNoticeIssueDate_lbl.TabIndex = 99;
            this.ShippingNoticeIssueDate_lbl.Text = "出荷通知発行日";
            // 
            // ShippingNoticeIssueDate_dtp
            // 
            this.ShippingNoticeIssueDate_dtp.BackColor = System.Drawing.SystemColors.Control;
            this.ShippingNoticeIssueDate_dtp.CalendarMonthBackground = System.Drawing.SystemColors.ControlLight;
            this.ShippingNoticeIssueDate_dtp.ControlId = null;
            this.ShippingNoticeIssueDate_dtp.CustomFormat = "yyyy-MM-dd";
            this.ShippingNoticeIssueDate_dtp.DisplayFormat = Com.ZimVie.Wcs.Framework.DateTimePickerCommon.DisplayFormatList.ShortDatePattern;
            this.ShippingNoticeIssueDate_dtp.Font = new System.Drawing.Font("Arial", 9.75F);
            this.ShippingNoticeIssueDate_dtp.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ShippingNoticeIssueDate_dtp.Location = new System.Drawing.Point(277, 212);
            this.ShippingNoticeIssueDate_dtp.Name = "ShippingNoticeIssueDate_dtp";
            this.ShippingNoticeIssueDate_dtp.Size = new System.Drawing.Size(119, 22);
            this.ShippingNoticeIssueDate_dtp.TabIndex = 2;
            // 
            // CreateWorkOrder_btn
            // 
            this.CreateWorkOrder_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CreateWorkOrder_btn.BackColor = System.Drawing.SystemColors.Control;
            this.CreateWorkOrder_btn.ControlId = null;
            this.CreateWorkOrder_btn.Font = new System.Drawing.Font("Arial", 9F);
            this.CreateWorkOrder_btn.Location = new System.Drawing.Point(742, 741);
            this.CreateWorkOrder_btn.Name = "CreateWorkOrder_btn";
            this.CreateWorkOrder_btn.Size = new System.Drawing.Size(115, 30);
            this.CreateWorkOrder_btn.TabIndex = 6;
            this.CreateWorkOrder_btn.Text = "指図書生成";
            this.CreateWorkOrder_btn.UseVisualStyleBackColor = false;
            this.CreateWorkOrder_btn.Click += new System.EventHandler(this.CreateWorkOrder_btn_Click);
            // 
            // labelCommon2
            // 
            this.labelCommon2.AutoSize = true;
            this.labelCommon2.ControlId = null;
            this.labelCommon2.Font = new System.Drawing.Font("Arial", 9F);
            this.labelCommon2.ForeColor = System.Drawing.Color.DarkRed;
            this.labelCommon2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelCommon2.Location = new System.Drawing.Point(498, 169);
            this.labelCommon2.Name = "labelCommon2";
            this.labelCommon2.Size = new System.Drawing.Size(27, 15);
            this.labelCommon2.TabIndex = 102;
            this.labelCommon2.Text = "(＊)";
            this.labelCommon2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelCommon1
            // 
            this.labelCommon1.AutoSize = true;
            this.labelCommon1.ControlId = null;
            this.labelCommon1.Font = new System.Drawing.Font("Arial", 9F);
            this.labelCommon1.ForeColor = System.Drawing.Color.DarkRed;
            this.labelCommon1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelCommon1.Location = new System.Drawing.Point(417, 218);
            this.labelCommon1.Name = "labelCommon1";
            this.labelCommon1.Size = new System.Drawing.Size(27, 15);
            this.labelCommon1.TabIndex = 103;
            this.labelCommon1.Text = "(＊)";
            this.labelCommon1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelCommon3
            // 
            this.labelCommon3.AutoSize = true;
            this.labelCommon3.ControlId = null;
            this.labelCommon3.Font = new System.Drawing.Font("Arial", 9F);
            this.labelCommon3.ForeColor = System.Drawing.Color.DarkRed;
            this.labelCommon3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelCommon3.Location = new System.Drawing.Point(626, 125);
            this.labelCommon3.Name = "labelCommon3";
            this.labelCommon3.Size = new System.Drawing.Size(27, 15);
            this.labelCommon3.TabIndex = 104;
            this.labelCommon3.Text = "(＊)";
            this.labelCommon3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // colShippingNoticeLineID
            // 
            this.colShippingNoticeLineID.DataPropertyName = "ShippingNoticeLineId";
            this.colShippingNoticeLineID.HeaderText = "Shipping Notice Line ID";
            this.colShippingNoticeLineID.Name = "colShippingNoticeLineID";
            this.colShippingNoticeLineID.Visible = false;
            // 
            // colPurchaseOrderNumber
            // 
            this.colPurchaseOrderNumber.DataPropertyName = "PurchaseOrderNumber";
            this.colPurchaseOrderNumber.FillWeight = 50.76142F;
            this.colPurchaseOrderNumber.HeaderText = "発注番号";
            this.colPurchaseOrderNumber.Name = "colPurchaseOrderNumber";
            this.colPurchaseOrderNumber.ReadOnly = true;
            // 
            // colInvoiceNumber
            // 
            this.colInvoiceNumber.DataPropertyName = "InvoiceNumber";
            this.colInvoiceNumber.FillWeight = 149.2386F;
            this.colInvoiceNumber.HeaderText = "納品書番号";
            this.colInvoiceNumber.Name = "colInvoiceNumber";
            this.colInvoiceNumber.ReadOnly = true;
            // 
            // colItemNumber
            // 
            this.colItemNumber.DataPropertyName = "ItemNumber";
            this.colItemNumber.HeaderText = "品目番号";
            this.colItemNumber.Name = "colItemNumber";
            this.colItemNumber.ReadOnly = true;
            // 
            // colSupplierItemNumber
            // 
            this.colSupplierItemNumber.DataPropertyName = "SupplierItemNumber";
            this.colSupplierItemNumber.HeaderText = "仕入先品目番号";
            this.colSupplierItemNumber.Name = "colSupplierItemNumber";
            this.colSupplierItemNumber.ReadOnly = true;
            this.colSupplierItemNumber.Visible = false;
            // 
            // colLotNumber
            // 
            this.colLotNumber.DataPropertyName = "LotNumber";
            this.colLotNumber.HeaderText = "ロット番号";
            this.colLotNumber.Name = "colLotNumber";
            this.colLotNumber.ReadOnly = true;
            // 
            // colLotQuantity
            // 
            this.colLotQuantity.DataPropertyName = "LotQuantity";
            this.colLotQuantity.HeaderText = "ロット数量";
            this.colLotQuantity.Name = "colLotQuantity";
            this.colLotQuantity.ReadOnly = true;
            // 
            // colLotExpirationDate
            // 
            this.colLotExpirationDate.DataPropertyName = "LotExpirationDate";
            this.colLotExpirationDate.HeaderText = "滅菌期限";
            this.colLotExpirationDate.Name = "colLotExpirationDate";
            this.colLotExpirationDate.ReadOnly = true;
            // 
            // ShippingNoticeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(1032, 807);
            this.Controls.Add(this.labelCommon3);
            this.Controls.Add(this.labelCommon1);
            this.Controls.Add(this.labelCommon2);
            this.Controls.Add(this.CreateWorkOrder_btn);
            this.Controls.Add(this.ShippingNoticeIssueDate_lbl);
            this.Controls.Add(this.ShippingNoticeIssueDate_dtp);
            this.Controls.Add(this.SupplierNumberAndName_cmb);
            this.Controls.Add(this.SupplierNumberAndName_lbl);
            this.Controls.Add(this.Import_btn);
            this.Controls.Add(this.Exit_btn);
            this.Controls.Add(this.ShippingNotice_dgv);
            this.Controls.Add(this.ShippingNoticeTrackingNumber_txt);
            this.Controls.Add(this.TrackingNumber_lbl);
            this.Name = "ShippingNoticeForm";
            this.Text = "出荷通知取込と指図書発行";
            this.TitleText = "出荷通知取込と指図書発行";
            this.Load += new System.EventHandler(this.ShippingNoticeImportAndWorkOrderCreationForm_Load);
            this.Controls.SetChildIndex(this.TrackingNumber_lbl, 0);
            this.Controls.SetChildIndex(this.ShippingNoticeTrackingNumber_txt, 0);
            this.Controls.SetChildIndex(this.ShippingNotice_dgv, 0);
            this.Controls.SetChildIndex(this.Exit_btn, 0);
            this.Controls.SetChildIndex(this.Import_btn, 0);
            this.Controls.SetChildIndex(this.SupplierNumberAndName_lbl, 0);
            this.Controls.SetChildIndex(this.SupplierNumberAndName_cmb, 0);
            this.Controls.SetChildIndex(this.ShippingNoticeIssueDate_dtp, 0);
            this.Controls.SetChildIndex(this.ShippingNoticeIssueDate_lbl, 0);
            this.Controls.SetChildIndex(this.CreateWorkOrder_btn, 0);
            this.Controls.SetChildIndex(this.labelCommon2, 0);
            this.Controls.SetChildIndex(this.labelCommon1, 0);
            this.Controls.SetChildIndex(this.labelCommon3, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ShippingNotice_dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public Framework.TextBoxCommon ShippingNoticeTrackingNumber_txt;
        private Framework.LabelCommon TrackingNumber_lbl;
        private Framework.DataGridViewCommon ShippingNotice_dgv;
        private Framework.ButtonCommon Exit_btn;
        private Framework.ButtonCommon Import_btn;
        private Framework.ComboBoxCommon SupplierNumberAndName_cmb;
        private Framework.LabelCommon SupplierNumberAndName_lbl;
        private Framework.LabelCommon ShippingNoticeIssueDate_lbl;
        private Framework.ButtonCommon CreateWorkOrder_btn;
        private Framework.DateTimePickerCommon ShippingNoticeIssueDate_dtp;
        private Framework.LabelCommon labelCommon2;
        private Framework.LabelCommon labelCommon1;
        private Framework.LabelCommon labelCommon3;
        private System.Windows.Forms.DataGridViewTextBoxColumn colShippingNoticeLineID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPurchaseOrderNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn colInvoiceNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn colItemNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSupplierItemNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLotNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLotQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLotExpirationDate;
    }
}
