namespace Com.ZimVie.Wcs.ZWCS
{
    partial class WorkVarificationForm
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
            this.PrintedBarcode_txt = new Com.ZimVie.Wcs.Framework.TextBoxCommon();
            this.PrintedBarcode_lbl = new Com.ZimVie.Wcs.Framework.LabelCommon();
            this.ScannedGoodsItemLot_dgv = new Com.ZimVie.Wcs.Framework.DataGridViewCommon();
            this.colScanSerialNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colItemNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLotNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Close_btn = new Com.ZimVie.Wcs.Framework.ButtonCommon();
            this.ClearAll_btn = new Com.ZimVie.Wcs.Framework.ButtonCommon();
            this.labelCommon2 = new Com.ZimVie.Wcs.Framework.LabelCommon();
            this.LabelBarcode_txt = new Com.ZimVie.Wcs.Framework.TextBoxCommon();
            this.LabelBarcode_lbl = new Com.ZimVie.Wcs.Framework.LabelCommon();
            this.ItemNumber_txt = new Com.ZimVie.Wcs.Framework.TextBoxCommon();
            this.ItemNumber_lbl = new Com.ZimVie.Wcs.Framework.LabelCommon();
            this.LotNumber_txt = new Com.ZimVie.Wcs.Framework.TextBoxCommon();
            this.LotNumber_lbl = new Com.ZimVie.Wcs.Framework.LabelCommon();
            this.Quantity_txt = new Com.ZimVie.Wcs.Framework.TextBoxCommon();
            this.Quantity_lbl = new Com.ZimVie.Wcs.Framework.LabelCommon();
            this.labelCommon6 = new Com.ZimVie.Wcs.Framework.LabelCommon();
            this.WorkOrderNumber_txt = new Com.ZimVie.Wcs.Framework.TextBoxCommon();
            this.WorkOrderNumber_lbl = new Com.ZimVie.Wcs.Framework.LabelCommon();
            this.WorkOrderLineNumber_txt = new Com.ZimVie.Wcs.Framework.TextBoxCommon();
            this.WorkOrderLineNumber_lbl = new Com.ZimVie.Wcs.Framework.LabelCommon();
            this.LotNumberOnGoods_txt = new Com.ZimVie.Wcs.Framework.TextBoxCommon();
            this.LotNumberOnGoods_lbl = new Com.ZimVie.Wcs.Framework.LabelCommon();
            this.ItemNumberOnGoods_txt = new Com.ZimVie.Wcs.Framework.TextBoxCommon();
            this.ItemNumberOnGoods_lbl = new Com.ZimVie.Wcs.Framework.LabelCommon();
            ((System.ComponentModel.ISupportInitialize)(this.ScannedGoodsItemLot_dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // PrintedBarcode_txt
            // 
            this.PrintedBarcode_txt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PrintedBarcode_txt.ControlId = null;
            this.PrintedBarcode_txt.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PrintedBarcode_txt.InputType = Com.ZimVie.Wcs.Framework.TextBoxCommon.InputTypeList.All;
            this.PrintedBarcode_txt.Location = new System.Drawing.Point(193, 158);
            this.PrintedBarcode_txt.MaxLength = 38;
            this.PrintedBarcode_txt.Name = "PrintedBarcode_txt";
            this.PrintedBarcode_txt.Size = new System.Drawing.Size(259, 21);
            this.PrintedBarcode_txt.TabIndex = 1;
            this.PrintedBarcode_txt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PrintedBarcode_txt_KeyDown);
            this.PrintedBarcode_txt.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.PrintedBarcode_txt_PreviewKeyDown);
            // 
            // PrintedBarcode_lbl
            // 
            this.PrintedBarcode_lbl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PrintedBarcode_lbl.AutoSize = true;
            this.PrintedBarcode_lbl.ControlId = null;
            this.PrintedBarcode_lbl.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PrintedBarcode_lbl.Location = new System.Drawing.Point(71, 161);
            this.PrintedBarcode_lbl.Name = "PrintedBarcode_lbl";
            this.PrintedBarcode_lbl.Size = new System.Drawing.Size(90, 15);
            this.PrintedBarcode_lbl.TabIndex = 29;
            this.PrintedBarcode_lbl.Text = "バーコード（印字）";
            // 
            // ScannedGoodsItemLot_dgv
            // 
            this.ScannedGoodsItemLot_dgv.AllowUserToAddRows = false;
            this.ScannedGoodsItemLot_dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(232)))), ((int)(((byte)(180)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ScannedGoodsItemLot_dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.ScannedGoodsItemLot_dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ScannedGoodsItemLot_dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colScanSerialNumber,
            this.colItemNumber,
            this.colLotNumber});
            this.ScannedGoodsItemLot_dgv.ControlId = null;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ScannedGoodsItemLot_dgv.DefaultCellStyle = dataGridViewCellStyle2;
            this.ScannedGoodsItemLot_dgv.EnableHeadersVisualStyles = false;
            this.ScannedGoodsItemLot_dgv.Location = new System.Drawing.Point(65, 361);
            this.ScannedGoodsItemLot_dgv.Name = "ScannedGoodsItemLot_dgv";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(232)))), ((int)(((byte)(180)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ScannedGoodsItemLot_dgv.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.ScannedGoodsItemLot_dgv.RowHeadersVisible = false;
            this.ScannedGoodsItemLot_dgv.RowTemplate.Height = 21;
            this.ScannedGoodsItemLot_dgv.Size = new System.Drawing.Size(630, 336);
            this.ScannedGoodsItemLot_dgv.TabIndex = 6;
            // 
            // colScanSerialNumber
            // 
            this.colScanSerialNumber.DataPropertyName = "ScanSerial";
            this.colScanSerialNumber.FillWeight = 149.2386F;
            this.colScanSerialNumber.HeaderText = "スキャン連番";
            this.colScanSerialNumber.Name = "colScanSerialNumber";
            this.colScanSerialNumber.ReadOnly = true;
            // 
            // colItemNumber
            // 
            this.colItemNumber.DataPropertyName = "ItemNumber";
            this.colItemNumber.FillWeight = 200F;
            this.colItemNumber.HeaderText = "品目番号";
            this.colItemNumber.Name = "colItemNumber";
            this.colItemNumber.ReadOnly = true;
            this.colItemNumber.Width = 200;
            // 
            // colLotNumber
            // 
            this.colLotNumber.DataPropertyName = "LotNumber";
            this.colLotNumber.FillWeight = 200F;
            this.colLotNumber.HeaderText = "ロット番号";
            this.colLotNumber.Name = "colLotNumber";
            this.colLotNumber.ReadOnly = true;
            this.colLotNumber.Width = 200;
            // 
            // Close_btn
            // 
            this.Close_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Close_btn.BackColor = System.Drawing.SystemColors.Control;
            this.Close_btn.ControlId = null;
            this.Close_btn.Font = new System.Drawing.Font("Arial", 9F);
            this.Close_btn.Location = new System.Drawing.Point(628, 719);
            this.Close_btn.Name = "Close_btn";
            this.Close_btn.Size = new System.Drawing.Size(80, 30);
            this.Close_btn.TabIndex = 8;
            this.Close_btn.Text = "閉じる";
            this.Close_btn.UseVisualStyleBackColor = false;
            this.Close_btn.Click += new System.EventHandler(this.Close_btn_Click);
            // 
            // ClearAll_btn
            // 
            this.ClearAll_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ClearAll_btn.BackColor = System.Drawing.SystemColors.Control;
            this.ClearAll_btn.ControlId = null;
            this.ClearAll_btn.Font = new System.Drawing.Font("Arial", 9F);
            this.ClearAll_btn.Location = new System.Drawing.Point(506, 719);
            this.ClearAll_btn.Name = "ClearAll_btn";
            this.ClearAll_btn.Size = new System.Drawing.Size(92, 30);
            this.ClearAll_btn.TabIndex = 7;
            this.ClearAll_btn.Text = "全てクリア";
            this.ClearAll_btn.UseVisualStyleBackColor = false;
            this.ClearAll_btn.Click += new System.EventHandler(this.ClearAll_btn_Click);
            // 
            // labelCommon2
            // 
            this.labelCommon2.AutoSize = true;
            this.labelCommon2.ControlId = null;
            this.labelCommon2.Font = new System.Drawing.Font("Arial", 9F);
            this.labelCommon2.ForeColor = System.Drawing.Color.DarkRed;
            this.labelCommon2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelCommon2.Location = new System.Drawing.Point(459, 160);
            this.labelCommon2.Name = "labelCommon2";
            this.labelCommon2.Size = new System.Drawing.Size(27, 15);
            this.labelCommon2.TabIndex = 102;
            this.labelCommon2.Text = "(＊)";
            this.labelCommon2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LabelBarcode_txt
            // 
            this.LabelBarcode_txt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.LabelBarcode_txt.ControlId = null;
            this.LabelBarcode_txt.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelBarcode_txt.InputType = Com.ZimVie.Wcs.Framework.TextBoxCommon.InputTypeList.All;
            this.LabelBarcode_txt.Location = new System.Drawing.Point(193, 195);
            this.LabelBarcode_txt.MaxLength = 38;
            this.LabelBarcode_txt.Name = "LabelBarcode_txt";
            this.LabelBarcode_txt.Size = new System.Drawing.Size(259, 21);
            this.LabelBarcode_txt.TabIndex = 2;
            this.LabelBarcode_txt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LabelBarcode_txt_KeyDown);
            this.LabelBarcode_txt.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.LabelBarcode_txt_PreviewKeyDown);
            // 
            // LabelBarcode_lbl
            // 
            this.LabelBarcode_lbl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelBarcode_lbl.AutoSize = true;
            this.LabelBarcode_lbl.ControlId = null;
            this.LabelBarcode_lbl.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelBarcode_lbl.Location = new System.Drawing.Point(71, 198);
            this.LabelBarcode_lbl.Name = "LabelBarcode_lbl";
            this.LabelBarcode_lbl.Size = new System.Drawing.Size(94, 15);
            this.LabelBarcode_lbl.TabIndex = 104;
            this.LabelBarcode_lbl.Text = "バーコード（ラベル）";
            // 
            // ItemNumber_txt
            // 
            this.ItemNumber_txt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ItemNumber_txt.ControlId = null;
            this.ItemNumber_txt.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ItemNumber_txt.InputType = Com.ZimVie.Wcs.Framework.TextBoxCommon.InputTypeList.All;
            this.ItemNumber_txt.Location = new System.Drawing.Point(193, 232);
            this.ItemNumber_txt.MaxLength = 12;
            this.ItemNumber_txt.Name = "ItemNumber_txt";
            this.ItemNumber_txt.ReadOnly = true;
            this.ItemNumber_txt.Size = new System.Drawing.Size(186, 21);
            this.ItemNumber_txt.TabIndex = 100;
            this.ItemNumber_txt.TabStop = false;
            // 
            // ItemNumber_lbl
            // 
            this.ItemNumber_lbl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ItemNumber_lbl.AutoSize = true;
            this.ItemNumber_lbl.ControlId = null;
            this.ItemNumber_lbl.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ItemNumber_lbl.Location = new System.Drawing.Point(71, 235);
            this.ItemNumber_lbl.Name = "ItemNumber_lbl";
            this.ItemNumber_lbl.Size = new System.Drawing.Size(91, 15);
            this.ItemNumber_lbl.TabIndex = 106;
            this.ItemNumber_lbl.Text = "製品番号（印字）";
            // 
            // LotNumber_txt
            // 
            this.LotNumber_txt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.LotNumber_txt.ControlId = null;
            this.LotNumber_txt.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LotNumber_txt.InputType = Com.ZimVie.Wcs.Framework.TextBoxCommon.InputTypeList.All;
            this.LotNumber_txt.Location = new System.Drawing.Point(543, 229);
            this.LotNumber_txt.MaxLength = 12;
            this.LotNumber_txt.Name = "LotNumber_txt";
            this.LotNumber_txt.ReadOnly = true;
            this.LotNumber_txt.Size = new System.Drawing.Size(112, 21);
            this.LotNumber_txt.TabIndex = 100;
            this.LotNumber_txt.TabStop = false;
            // 
            // LotNumber_lbl
            // 
            this.LotNumber_lbl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LotNumber_lbl.AutoSize = true;
            this.LotNumber_lbl.ControlId = null;
            this.LotNumber_lbl.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LotNumber_lbl.Location = new System.Drawing.Point(465, 232);
            this.LotNumber_lbl.Name = "LotNumber_lbl";
            this.LotNumber_lbl.Size = new System.Drawing.Size(67, 15);
            this.LotNumber_lbl.TabIndex = 108;
            this.LotNumber_lbl.Text = "ロット（印字）";
            // 
            // Quantity_txt
            // 
            this.Quantity_txt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Quantity_txt.ControlId = null;
            this.Quantity_txt.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Quantity_txt.InputType = Com.ZimVie.Wcs.Framework.TextBoxCommon.InputTypeList.All;
            this.Quantity_txt.Location = new System.Drawing.Point(193, 269);
            this.Quantity_txt.MaxLength = 6;
            this.Quantity_txt.Name = "Quantity_txt";
            this.Quantity_txt.ReadOnly = true;
            this.Quantity_txt.Size = new System.Drawing.Size(111, 21);
            this.Quantity_txt.TabIndex = 100;
            this.Quantity_txt.TabStop = false;
            // 
            // Quantity_lbl
            // 
            this.Quantity_lbl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Quantity_lbl.AutoSize = true;
            this.Quantity_lbl.ControlId = null;
            this.Quantity_lbl.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Quantity_lbl.Location = new System.Drawing.Point(71, 272);
            this.Quantity_lbl.Name = "Quantity_lbl";
            this.Quantity_lbl.Size = new System.Drawing.Size(91, 15);
            this.Quantity_lbl.TabIndex = 110;
            this.Quantity_lbl.Text = "製品数量（印字）";
            // 
            // labelCommon6
            // 
            this.labelCommon6.AutoSize = true;
            this.labelCommon6.ControlId = null;
            this.labelCommon6.Font = new System.Drawing.Font("Arial", 9F);
            this.labelCommon6.ForeColor = System.Drawing.Color.DarkRed;
            this.labelCommon6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelCommon6.Location = new System.Drawing.Point(459, 198);
            this.labelCommon6.Name = "labelCommon6";
            this.labelCommon6.Size = new System.Drawing.Size(27, 15);
            this.labelCommon6.TabIndex = 111;
            this.labelCommon6.Text = "(＊)";
            this.labelCommon6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // WorkOrderNumber_txt
            // 
            this.WorkOrderNumber_txt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.WorkOrderNumber_txt.ControlId = null;
            this.WorkOrderNumber_txt.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WorkOrderNumber_txt.InputType = Com.ZimVie.Wcs.Framework.TextBoxCommon.InputTypeList.All;
            this.WorkOrderNumber_txt.Location = new System.Drawing.Point(193, 124);
            this.WorkOrderNumber_txt.MaxLength = 25;
            this.WorkOrderNumber_txt.Name = "WorkOrderNumber_txt";
            this.WorkOrderNumber_txt.Size = new System.Drawing.Size(186, 21);
            this.WorkOrderNumber_txt.TabIndex = 0;
            this.WorkOrderNumber_txt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.WorkOrderNumber_txt_KeyDown);
            this.WorkOrderNumber_txt.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.WorkOrderNumber_txt_PreviewKeyDown);
            // 
            // WorkOrderNumber_lbl
            // 
            this.WorkOrderNumber_lbl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.WorkOrderNumber_lbl.AutoSize = true;
            this.WorkOrderNumber_lbl.ControlId = null;
            this.WorkOrderNumber_lbl.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WorkOrderNumber_lbl.Location = new System.Drawing.Point(71, 127);
            this.WorkOrderNumber_lbl.Name = "WorkOrderNumber_lbl";
            this.WorkOrderNumber_lbl.Size = new System.Drawing.Size(55, 15);
            this.WorkOrderNumber_lbl.TabIndex = 114;
            this.WorkOrderNumber_lbl.Text = "指図番号";
            // 
            // WorkOrderLineNumber_txt
            // 
            this.WorkOrderLineNumber_txt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.WorkOrderLineNumber_txt.ControlId = null;
            this.WorkOrderLineNumber_txt.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WorkOrderLineNumber_txt.InputType = Com.ZimVie.Wcs.Framework.TextBoxCommon.InputTypeList.All;
            this.WorkOrderLineNumber_txt.Location = new System.Drawing.Point(543, 121);
            this.WorkOrderLineNumber_txt.MaxLength = 3;
            this.WorkOrderLineNumber_txt.Name = "WorkOrderLineNumber_txt";
            this.WorkOrderLineNumber_txt.ReadOnly = true;
            this.WorkOrderLineNumber_txt.Size = new System.Drawing.Size(112, 21);
            this.WorkOrderLineNumber_txt.TabIndex = 100;
            this.WorkOrderLineNumber_txt.TabStop = false;
            // 
            // WorkOrderLineNumber_lbl
            // 
            this.WorkOrderLineNumber_lbl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.WorkOrderLineNumber_lbl.AutoSize = true;
            this.WorkOrderLineNumber_lbl.ControlId = null;
            this.WorkOrderLineNumber_lbl.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WorkOrderLineNumber_lbl.Location = new System.Drawing.Point(465, 124);
            this.WorkOrderLineNumber_lbl.Name = "WorkOrderLineNumber_lbl";
            this.WorkOrderLineNumber_lbl.Size = new System.Drawing.Size(55, 15);
            this.WorkOrderLineNumber_lbl.TabIndex = 116;
            this.WorkOrderLineNumber_lbl.Text = "明細番号";
            // 
            // LotNumberOnGoods_txt
            // 
            this.LotNumberOnGoods_txt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.LotNumberOnGoods_txt.ControlId = null;
            this.LotNumberOnGoods_txt.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LotNumberOnGoods_txt.InputType = Com.ZimVie.Wcs.Framework.TextBoxCommon.InputTypeList.All;
            this.LotNumberOnGoods_txt.Location = new System.Drawing.Point(543, 318);
            this.LotNumberOnGoods_txt.MaxLength = 40;
            this.LotNumberOnGoods_txt.Name = "LotNumberOnGoods_txt";
            this.LotNumberOnGoods_txt.Size = new System.Drawing.Size(112, 21);
            this.LotNumberOnGoods_txt.TabIndex = 4;
            this.LotNumberOnGoods_txt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LotNumberOnGoods_txt_KeyDown);
            this.LotNumberOnGoods_txt.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.LotNumberOnGoods_txt_PreviewKeyDown);
            // 
            // LotNumberOnGoods_lbl
            // 
            this.LotNumberOnGoods_lbl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LotNumberOnGoods_lbl.AutoSize = true;
            this.LotNumberOnGoods_lbl.ControlId = null;
            this.LotNumberOnGoods_lbl.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LotNumberOnGoods_lbl.Location = new System.Drawing.Point(465, 321);
            this.LotNumberOnGoods_lbl.Name = "LotNumberOnGoods_lbl";
            this.LotNumberOnGoods_lbl.Size = new System.Drawing.Size(67, 15);
            this.LotNumberOnGoods_lbl.TabIndex = 120;
            this.LotNumberOnGoods_lbl.Text = "ロット（現物）";
            // 
            // ItemNumberOnGoods_txt
            // 
            this.ItemNumberOnGoods_txt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ItemNumberOnGoods_txt.ControlId = null;
            this.ItemNumberOnGoods_txt.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ItemNumberOnGoods_txt.InputType = Com.ZimVie.Wcs.Framework.TextBoxCommon.InputTypeList.All;
            this.ItemNumberOnGoods_txt.Location = new System.Drawing.Point(193, 321);
            this.ItemNumberOnGoods_txt.MaxLength = 40;
            this.ItemNumberOnGoods_txt.Name = "ItemNumberOnGoods_txt";
            this.ItemNumberOnGoods_txt.Size = new System.Drawing.Size(186, 21);
            this.ItemNumberOnGoods_txt.TabIndex = 3;
            this.ItemNumberOnGoods_txt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ItemNumberOnGoods_txt_KeyDown);
            this.ItemNumberOnGoods_txt.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.ItemNumberOnGoods_txt_PreviewKeyDown);
            // 
            // ItemNumberOnGoods_lbl
            // 
            this.ItemNumberOnGoods_lbl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ItemNumberOnGoods_lbl.AutoSize = true;
            this.ItemNumberOnGoods_lbl.ControlId = null;
            this.ItemNumberOnGoods_lbl.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ItemNumberOnGoods_lbl.Location = new System.Drawing.Point(71, 324);
            this.ItemNumberOnGoods_lbl.Name = "ItemNumberOnGoods_lbl";
            this.ItemNumberOnGoods_lbl.Size = new System.Drawing.Size(91, 15);
            this.ItemNumberOnGoods_lbl.TabIndex = 119;
            this.ItemNumberOnGoods_lbl.Text = "製品番号（現物）";
            // 
            // WorkVarificationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(754, 774);
            this.Controls.Add(this.LotNumberOnGoods_txt);
            this.Controls.Add(this.LotNumberOnGoods_lbl);
            this.Controls.Add(this.ItemNumberOnGoods_txt);
            this.Controls.Add(this.ItemNumberOnGoods_lbl);
            this.Controls.Add(this.WorkOrderLineNumber_txt);
            this.Controls.Add(this.WorkOrderLineNumber_lbl);
            this.Controls.Add(this.WorkOrderNumber_txt);
            this.Controls.Add(this.WorkOrderNumber_lbl);
            this.Controls.Add(this.labelCommon6);
            this.Controls.Add(this.Quantity_txt);
            this.Controls.Add(this.Quantity_lbl);
            this.Controls.Add(this.LotNumber_txt);
            this.Controls.Add(this.LotNumber_lbl);
            this.Controls.Add(this.ItemNumber_txt);
            this.Controls.Add(this.ItemNumber_lbl);
            this.Controls.Add(this.LabelBarcode_txt);
            this.Controls.Add(this.LabelBarcode_lbl);
            this.Controls.Add(this.labelCommon2);
            this.Controls.Add(this.ClearAll_btn);
            this.Controls.Add(this.Close_btn);
            this.Controls.Add(this.ScannedGoodsItemLot_dgv);
            this.Controls.Add(this.PrintedBarcode_txt);
            this.Controls.Add(this.PrintedBarcode_lbl);
            this.Name = "WorkVarificationForm";
            this.Text = "ラベリング照合";
            this.TitleText = "ラベリング照合";
            this.Controls.SetChildIndex(this.PrintedBarcode_lbl, 0);
            this.Controls.SetChildIndex(this.PrintedBarcode_txt, 0);
            this.Controls.SetChildIndex(this.ScannedGoodsItemLot_dgv, 0);
            this.Controls.SetChildIndex(this.Close_btn, 0);
            this.Controls.SetChildIndex(this.ClearAll_btn, 0);
            this.Controls.SetChildIndex(this.labelCommon2, 0);
            this.Controls.SetChildIndex(this.LabelBarcode_lbl, 0);
            this.Controls.SetChildIndex(this.LabelBarcode_txt, 0);
            this.Controls.SetChildIndex(this.ItemNumber_lbl, 0);
            this.Controls.SetChildIndex(this.ItemNumber_txt, 0);
            this.Controls.SetChildIndex(this.LotNumber_lbl, 0);
            this.Controls.SetChildIndex(this.LotNumber_txt, 0);
            this.Controls.SetChildIndex(this.Quantity_lbl, 0);
            this.Controls.SetChildIndex(this.Quantity_txt, 0);
            this.Controls.SetChildIndex(this.labelCommon6, 0);
            this.Controls.SetChildIndex(this.WorkOrderNumber_lbl, 0);
            this.Controls.SetChildIndex(this.WorkOrderNumber_txt, 0);
            this.Controls.SetChildIndex(this.WorkOrderLineNumber_lbl, 0);
            this.Controls.SetChildIndex(this.WorkOrderLineNumber_txt, 0);
            this.Controls.SetChildIndex(this.ItemNumberOnGoods_lbl, 0);
            this.Controls.SetChildIndex(this.ItemNumberOnGoods_txt, 0);
            this.Controls.SetChildIndex(this.LotNumberOnGoods_lbl, 0);
            this.Controls.SetChildIndex(this.LotNumberOnGoods_txt, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ScannedGoodsItemLot_dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public Framework.TextBoxCommon PrintedBarcode_txt;
        private Framework.LabelCommon PrintedBarcode_lbl;
        private Framework.DataGridViewCommon ScannedGoodsItemLot_dgv;
        private Framework.ButtonCommon Close_btn;
        private Framework.ButtonCommon ClearAll_btn;
        private Framework.LabelCommon labelCommon2;
        public Framework.TextBoxCommon LabelBarcode_txt;
        private Framework.LabelCommon LabelBarcode_lbl;
        public Framework.TextBoxCommon ItemNumber_txt;
        private Framework.LabelCommon ItemNumber_lbl;
        public Framework.TextBoxCommon LotNumber_txt;
        private Framework.LabelCommon LotNumber_lbl;
        public Framework.TextBoxCommon Quantity_txt;
        private Framework.LabelCommon Quantity_lbl;
        private Framework.LabelCommon labelCommon6;
        public Framework.TextBoxCommon WorkOrderNumber_txt;
        private Framework.LabelCommon WorkOrderNumber_lbl;
        public Framework.TextBoxCommon WorkOrderLineNumber_txt;
        private Framework.LabelCommon WorkOrderLineNumber_lbl;
        public Framework.TextBoxCommon LotNumberOnGoods_txt;
        private Framework.LabelCommon LotNumberOnGoods_lbl;
        public Framework.TextBoxCommon ItemNumberOnGoods_txt;
        private Framework.LabelCommon ItemNumberOnGoods_lbl;
        private System.Windows.Forms.DataGridViewTextBoxColumn colScanSerialNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn colItemNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLotNumber;
    }
}
