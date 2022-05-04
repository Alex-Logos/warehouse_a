using System;
using System.Data;
using System.IO;
using System.Data.OleDb;
using System.Windows.Forms;
using Com.ZimVie.Wcs.Framework;
using Com.ZimVie.Wcs.ZWCS.Vo;
using Com.ZimVie.Wcs.ZWCS.Cbm;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

namespace Com.ZimVie.Wcs.ZWCS
{
    public partial class ShippingNoticeForm : FormCommonZwcs
    {
        /// <summary>
        /// Instantiate message controller
        /// </summary>
        private readonly PopUpMessageController popUpMessage = new PopUpMessageController();

        /// <summary>
        /// Instantiate logger
        /// </summary>
        private static readonly CommonLogger logger = CommonLogger.GetInstance(typeof(ShippingNoticeForm));

        /// <summary>
        /// Instantiate CBM to create shipping notice and create work order
        /// </summary>
        private readonly CbmController createShippingNoticeAndWorkOrderCbm = new CreateShippingNoticeAndWorkOrderCbm();

        /// <summary>
        /// Instantiate CBM to generate excel file by copying template file then reflect the values into the file for each work order
        /// </summary>
        private readonly CbmController outputMultipleWorkOrderFilesCbm = new OutputMultipleWorkOrderFilesCbm();

        /// <summary>
        /// Instantiate CBM to generate label objects then send print instruction to label printer
        /// </summary>
        private readonly CbmController printLabelsForWorkOrderLinesCbm = new PrintLabelsForWorkOrderLinesCbm();

        /// <summary>
        /// 
        /// </summary>
        public ShippingNoticeForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShippingNoticeImportAndWorkOrderCreationForm_Load(object sender, EventArgs e)
        {
            InitializeUiObjects();
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitializeUiObjects()
        {
            ShippingNoticeTrackingNumber_txt.Text = string.Empty;

            ShippingNoticeIssueDate_dtp.Clear();

            ShippingNotice_dgv.Rows.Clear();
            ShippingNotice_dgv.Refresh();

            LoadSupplierComboBox();
        }

        /// <summary>
        /// 
        /// </summary>
        private void LoadSupplierComboBox()
        {
            var inVo = new SupplierVo();

            ValueObjectList<SupplierVo> outVo = null;
            try
            {
                outVo = DefaultCbmInvoker.Invoke(new ReadSupplierCbm(), inVo) as ValueObjectList<SupplierVo>;
            }
            catch (Framework.ApplicationException exception)
            {
                popUpMessage.ApplicationError(exception.GetMessageData(), Text);
                logger.Error(exception.GetMessageData());
                return;
            }

            List<SupplierVo> suppliers = outVo.GetList();
            SupplierNumberAndName_cmb.DataSource = suppliers;

            SupplierVo supplier = new SupplierVo();
            SupplierNumberAndName_cmb.DisplayMember = nameof(supplier.SupplierNumberAndName);
            SupplierNumberAndName_cmb.ValueMember = nameof(supplier.SupplierNumber);
            SupplierNumberAndName_cmb.SelectedIndex = -1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Validate_btn_Click(object sender, EventArgs e)
        {
            bool uiValudationOk = ValidateUiHeaderItems();

            if (!uiValudationOk)
            {
                CreateWorkOrder_btn.Enabled = false;
                return;
            }

            var inVo = new ShippingNoticeTrackingNumberVo();
            inVo.ShippingNoticeTrackingNumber = ShippingNoticeTrackingNumber_txt.Text.Trim();

            BooleanValueObject outVo = null;
            try
            {
                outVo = DefaultCbmInvoker.Invoke(new ValidateShippingNoticeTrackingNumberCbm(), inVo) as BooleanValueObject;
            }
            catch (Framework.ApplicationException exception)
            {
                popUpMessage.ApplicationError(exception.GetMessageData(), this.Text);
                logger.Error(exception.GetMessageData());

                CreateWorkOrder_btn.Enabled = false;
                return;
            }

            if (outVo.BooleanValue)
            {
                var messageData = new MessageData("zwce00007", Properties.Resources.zwce00007);
                logger.Info(messageData);
                popUpMessage.Information(messageData, this.Text);
            }

            CreateWorkOrder_btn.Enabled = outVo.BooleanValue;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool ValidateUiHeaderItems()
        {

            SupplierVo supplier = SupplierNumberAndName_cmb.SelectedItem as SupplierVo;

            if (supplier == null)
            {
                var messageData = new MessageData("zwce00004", Properties.Resources.zwce00004);
                logger.Warn(messageData);
                popUpMessage.Warning(messageData, this.Text);

                return false;
            }

            string trackingNumber = ShippingNoticeTrackingNumber_txt.Text.Trim();

            if (string.IsNullOrWhiteSpace(trackingNumber))
            {
                var messageData = new MessageData("zwce00001", Properties.Resources.zwce00001);
                logger.Warn(messageData);
                popUpMessage.Warning(messageData, this.Text);

                return false;
            }

            if (ShippingNoticeIssueDate_dtp.CustomFormat == " ")
            {
                var messageData = new MessageData("zwce00005", Properties.Resources.zwce00005);
                logger.Warn(messageData);
                popUpMessage.Warning(messageData, this.Text);

                return false;                      
            }

            var bindingSource = ShippingNotice_dgv.DataSource as BindingSource;
            var gridViewLines = bindingSource.List as List<ShippingNoticeLineVo>;

            if (gridViewLines == null || gridViewLines.Count <= 0)
            {
                var messageData = new MessageData("zwce00003", Properties.Resources.zwce00003);
                logger.Info(messageData);
                popUpMessage.Information(messageData, this.Text);

                return false;
            }


            return true;

        }

        /// <summary>
        /// 1. Validate values on UI selected or input by user
        /// 2. Create shipping notice and work orders in database
        /// 3. Generate excel files by copying template file then reflect the values for each work orders
        /// 4. Display excel file generation result message to user, then initialize UI objects for processing next shipping notice 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateWorkOrder_btn_Click(object sender, EventArgs e)
        {

            // 1. Validate values on UI selected or input by user 

            bool uiValudationOk = ValidateUiHeaderItems();

            if (!uiValudationOk)
            {
                return;
            }


            // 2. Create shipping notice and work orders in database

            var supplier = SupplierNumberAndName_cmb.SelectedItem as SupplierVo;

            var header = new ShippingNoticeHeaderVo();
            header.SupplierNumber = supplier.SupplierNumber;
            header.SourceTypeInventory = supplier.SourceTypeInventory;
            header.ShippingNoticeTrackingNumber = ShippingNoticeTrackingNumber_txt.Text.Trim();
            header.ShippingNoticeIssueDate = ShippingNoticeIssueDate_dtp.Value;

            var bindingSource = ShippingNotice_dgv.DataSource as BindingSource;
            var lines = bindingSource.List as List<ShippingNoticeLineVo>;

            var inVo = new ShippingNoticeVo();
            inVo.Header = header;
            inVo.Line = lines;

            ValueObjectList<WorkOrderVo> workOrderOutVo = null;
            try
            {
                workOrderOutVo = DefaultCbmInvoker.Invoke(createShippingNoticeAndWorkOrderCbm, inVo) as ValueObjectList<WorkOrderVo>;
            }
            catch (Framework.ApplicationException exception)
            {
                logger.Error(exception.GetMessageData());
                popUpMessage.ApplicationError(exception.GetMessageData(), this.Text);
                return;
            }

            List<WorkOrderVo> workOrders = workOrderOutVo?.GetList();

            if (workOrders == null || workOrders.Count <= 0)
            {
                var messageData = new MessageData("zwce00012", Properties.Resources.zwce00012, nameof(createShippingNoticeAndWorkOrderCbm));
                logger.Error(messageData);
                popUpMessage.ApplicationError(messageData, this.Text);
                return;
            }


            // 3. Generate excel file by copying template file then reflect the values in the file for each work order

            ValueObjectList<WorkOrderOutputVo> fileInfo = null;
            try
            {
                fileInfo = DefaultCbmInvoker.Invoke(outputMultipleWorkOrderFilesCbm, workOrderOutVo) as ValueObjectList<WorkOrderOutputVo>;
            }
            catch (Framework.ApplicationException exception)
            {
                popUpMessage.ApplicationError(exception.GetMessageData(), this.Text);
                logger.Error(exception.GetMessageData());
                return;
            }

            List<WorkOrderOutputVo> files = fileInfo?.GetList();

            if (files == null || files.Count <= 0)
            {
                var messageData = new MessageData("zwce00024", Properties.Resources.zwce00024, nameof(outputMultipleWorkOrderFilesCbm));
                logger.Error(messageData);
                popUpMessage.ApplicationError(messageData, this.Text);
                return;
            }


            // 4. Display excel file generation result message to user, then initialize UI objects for processing next shipping notice 

            string fileCount = files.Count.ToString();
            string directory = files.First().Directory;
            string fileNames = string.Join(Environment.NewLine, files.Select(f => f.FileName));

            var successMessage = new MessageData("zwce00025", Properties.Resources.zwce00025, fileCount, directory, fileNames);
            logger.Info(successMessage);
            popUpMessage.Information(successMessage, this.Text);


            // 5. Generate label objects then send print instruction to label printer

            PrintLabelsResultVo printResult = null;
            try
            {
                printResult = DefaultCbmInvoker.Invoke(printLabelsForWorkOrderLinesCbm, workOrderOutVo) as PrintLabelsResultVo;
            }
            catch (Framework.ApplicationException exception)
            {
                popUpMessage.ApplicationError(exception.GetMessageData(), this.Text);
                logger.Error(exception.GetMessageData());
                return;
            }

            if (printResult == null)
            {
                var messageData = new MessageData("zwce00029", Properties.Resources.zwce00029, nameof(printLabelsForWorkOrderLinesCbm));
                logger.Error(messageData);
                popUpMessage.ApplicationError(messageData, this.Text);
                return;
            }


            // 6. Display label print result message to user 

            List<string> productLabelWorkOrders = printResult?.ProductLabelWorkOrders;
            List<string> logisticsLabelWorkOrders = printResult?.InternalLogisticsLabelWorkOrders;

            string productLabelOrderList = 
                productLabelWorkOrders == null || productLabelWorkOrders.Count <= 0 ? "なし" : string.Join(Environment.NewLine, productLabelWorkOrders);

            string logisticsLabelOrderList =
                logisticsLabelWorkOrders == null || logisticsLabelWorkOrders.Count <= 0 ? "なし" : string.Join(Environment.NewLine, logisticsLabelWorkOrders);

            var printResultMessage = new MessageData("zwce00030", Properties.Resources.zwce00030,
                Environment.NewLine,
                printResult.ProductLabelSetCount.ToString(), printResult.ProductLabelQuantityTotal.ToString(),
                printResult.InternalLogisticsLabelSetCount.ToString(), printResult.InternalLogisticsLabelQuantityTotal.ToString(),
                productLabelOrderList, logisticsLabelOrderList);

            logger.Info(printResultMessage);
            popUpMessage.Information(printResultMessage, this.Text);


            // 7. Initialize UI objecst for processing next shipping notice

            InitializeUiObjects();

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Import_btn_Click(object sender, EventArgs e)
        {
            ShippingNotice_dgv.Rows.Clear();
            ShippingNotice_dgv.Refresh();

            var fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";

            if (fileDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }

            string filePath = fileDialog.FileName;

            if (string.IsNullOrWhiteSpace(filePath))
            {
                var messageData = new MessageData("tpce00012", Properties.Resources.zwce00056);
                logger.Info(messageData);
                popUpMessage.Information(messageData, Text);
                return;
            }

            string fileExt = Path.GetExtension(filePath).ToLower();

            if (!fileExt.Equals(".xls") && !fileExt.Equals(".xlsx"))
            {
                var messageData = new MessageData("tpce00012", Properties.Resources.zwce00057);
                logger.Info(messageData);
                popUpMessage.Information(messageData, Text);
                return;
            }

            try
            {
                List<ShippingNoticeLineVo> voList = ReadShippingNoticeFromExcelFile(filePath);
                ShippingNotice_dgv.DataSource = new BindingSource(voList, null);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="FileName"></param>
        /// <param name="Header"></param>
        /// <returns></returns>
        private string GenerateConnectionString(string FileName)
        {
            OleDbConnectionStringBuilder Builder = new OleDbConnectionStringBuilder();
            if (System.IO.Path.GetExtension(FileName).ToUpper() == ".XLS")
            {
                Builder.Provider = "Microsoft.Jet.OLEDB.4.0";
                Builder.Add("Extended Properties", "Excel 8.0;IMEX=1;HDR=YES");
            }
            else
            {
                Builder.Provider = "Microsoft.ACE.OLEDB.12.0";
                Builder.Add("Extended Properties", "Excel 12.0;IMEX=1;HDR=YES");
            }

            Builder.DataSource = FileName;

            return Builder.ConnectionString;

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="FileName"></param>
        /// <returns></returns>
        private List<ShippingNoticeLineVo> ReadShippingNoticeFromExcelFile(string FileName)
        {

            using (OleDbConnection cn = new OleDbConnection 
                { ConnectionString = GenerateConnectionString(FileName) })
            {
                
                cn.Open();

                DataTable dtSchema = cn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                string sheetName = dtSchema.Rows[0].Field<string>("TABLE_NAME");


                using (OleDbCommand cmd = new OleDbCommand {
                    CommandText = "SELECT [PO Number], [Invoice Number], [Item Number], [Lot Number], [Shipped Quantity], [Lot Expiration date] FROM [" + sheetName + "]",
                    Connection = cn })
                {
                    OleDbDataReader dr = cmd.ExecuteReader();

                    List<ShippingNoticeLineVo> outList = LoadValueObjects(dr);

                    return outList;
                }
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataReader"></param>
        /// <returns></returns>
        private List<ShippingNoticeLineVo> LoadValueObjects(OleDbDataReader dataReader)
        {
            List<ShippingNoticeLineVo> outList = new List<ShippingNoticeLineVo>();

            while (dataReader.Read())
            {
                ShippingNoticeLineVo vo = new ShippingNoticeLineVo();

                vo.PurchaseOrderNumber = Convert.ToString(dataReader["PO Number"]);

                vo.InvoiceNumber = Convert.ToString(dataReader["Invoice Number"]);

                vo.ItemNumber = Convert.ToString(dataReader["Item Number"]);

                vo.LotNumber = Convert.ToString(dataReader["Lot Number"]);


                // Rows with null value to be disregarded
                object shippedQuantity = dataReader["Shipped Quantity"];

                if (shippedQuantity == DBNull.Value)
                {
                    continue;
                }

                vo.LotQuantity = Convert.ToInt32(shippedQuantity);


                object expirationDate = dataReader["Lot Expiration date"];

                DateTime? expirationDateDateTime = expirationDate as DateTime?;

                string expirationDateString = expirationDate as string;

                if (expirationDateDateTime != null)
                {
                    vo.LotExpirationDate = (DateTime)expirationDateDateTime;
                }
                else if (expirationDateString != null)
                {
                    bool dateParseSuccess = DateTime.TryParseExact(expirationDateString, "d-M-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime expiration);

                    bool intParseSuccess = int.TryParse(expirationDateString, out int oaDate);

                    if (dateParseSuccess)
                    {
                        vo.LotExpirationDate = expiration;
                    }
                    else if (intParseSuccess)
                    {
                        vo.LotExpirationDate = DateTime.FromOADate(oaDate);
                    }
                }

                outList.Add(vo);
            }
            dataReader.Close();

            return outList;
        }

        private void Exit_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
