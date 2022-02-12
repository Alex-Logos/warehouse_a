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
    public partial class WorkVarificationForm : FormCommonZwcs
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
        /// Instantiate CBM to read item number from JAN number
        /// </summary>
        private readonly CbmController readItemNumberAndLotNumberFromProductLabelBarcodeCbm = new ReadItemNumberAndLotNumberFromProductLabelBarcodeCbm();

        /// <summary>
        /// Instantiate CBM to varify imtem number between order and actual goods
        /// </summary>
        private readonly CbmController varifyItemNumberBetweenWorkOrderAndActualGoodsCbm = new VarifyItemNumberBetweenWorkOrderAndActualGoodsCbm();

        /// <summary>
        /// 
        /// </summary>
        public WorkVarificationForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitializeUiObjects()
        {
            WorkOrderNumber_txt.Text = string.Empty;
            WorkOrderLineNumber_txt.Text = string.Empty;
            PrintedBarcode_txt.Text = string.Empty;
            LabelBarcode_txt.Text = string.Empty;
            ItemNumber_txt.Text = string.Empty;
            LotNumber_txt.Text = string.Empty;
            Quantity_txt.Text = string.Empty;
            ItemNumberOnGoods_txt.Text = string.Empty;
            LotNumberOnGoods_txt.Text = string.Empty;
            ScannedGoodsItemLot_dgv.DataSource = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WorkOrderNumber_txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Tab)
            {
                return;
            }

            string workOrderNumberAndLineNumberAndQuantity = WorkOrderNumber_txt.Text;

            if (string.IsNullOrWhiteSpace(workOrderNumberAndLineNumberAndQuantity))
            {
                return;
            }

            if (workOrderNumberAndLineNumberAndQuantity.Length != 25)
            {
                var messageData = new MessageData("zwce00038", Properties.Resources.zwce00038, workOrderNumberAndLineNumberAndQuantity);
                logger.Error(messageData);
                popUpMessage.ApplicationError(messageData, this.Text);

                WorkOrderNumber_txt.Clear();
                return;
            }

            string workOrderNumber = workOrderNumberAndLineNumberAndQuantity.Substring(0, 16);

            string lineNumber = workOrderNumberAndLineNumberAndQuantity.Substring(16, 3);

            string quantity = workOrderNumberAndLineNumberAndQuantity.Substring(19, 6);


            WorkOrderNumber_txt.Text = workOrderNumber;

            WorkOrderLineNumber_txt.Text = lineNumber.TrimStart(new char[] { '0' });

            Quantity_txt.Text = quantity.TrimStart(new char[] { '0' });


            PrintedBarcode_txt.Focus();
        }


        /// <summary>
        /// Read item number by jan number from item master then reflect item number and lot number onto UI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        
        private void PrintedBarcode_txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Tab)
            {
                return;
            }


            ProductLabelBarcodeVo inVo = new ProductLabelBarcodeVo();
            inVo.ProductLabelBarcode = PrintedBarcode_txt.Text;

            VarificationItemLotVo outVo = null;
            try
            {
                outVo = DefaultCbmInvoker.Invoke(readItemNumberAndLotNumberFromProductLabelBarcodeCbm, inVo) as VarificationItemLotVo;
            }
            catch (Framework.ApplicationException exception)
            {
                logger.Error(exception.GetMessageData());
                popUpMessage.ApplicationError(exception.GetMessageData(), this.Text);

                PrintedBarcode_txt.Clear();
                return;
            }

            if (outVo == null || string.IsNullOrWhiteSpace(outVo.ItemNumber) || string.IsNullOrWhiteSpace(outVo.LotNumber))
            {
                var messageData = new MessageData("zwce00042", Properties.Resources.zwce00042, inVo.ProductLabelBarcode);
                logger.Error(messageData);
                popUpMessage.ApplicationError(messageData, this.Text);

                PrintedBarcode_txt.Clear();
                return;
            }


            ItemNumber_txt.Text = outVo.ItemNumber;

            LotNumber_txt.Text = outVo.LotNumber;


            LabelBarcode_txt.Focus();
        }


        private void LabelBarcode_txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Tab)
            {
                return;
            }

            string labelBarcode = LabelBarcode_txt.Text;

            string printedBarcode = PrintedBarcode_txt.Text;

            if (string.IsNullOrEmpty(labelBarcode) || string.IsNullOrWhiteSpace(printedBarcode))
            {
                var messageData = new MessageData("zwce00008", Properties.Resources.zwce00008, nameof(labelBarcode) + " or " + nameof(printedBarcode));
                logger.Error(messageData);
                popUpMessage.ApplicationError(messageData, this.Text);
                return;
            }

            if (!labelBarcode.Equals(printedBarcode))
            {
                var messageData = new MessageData("zwce00043", Properties.Resources.zwce00043, printedBarcode, labelBarcode);
                logger.Error(messageData);
                popUpMessage.ApplicationError(messageData, this.Text);

                LabelBarcode_txt.Clear();
                return;
            }


            ItemNumberOnGoods_txt.Focus();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        
        private void ItemNumberOnGoods_txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Tab)
            {
                return;
            }

            VarifyItemNumber();
        }


        private bool VarifyItemNumber()
        {

            VarificationItemQueryVo inVo = new VarificationItemQueryVo();
            inVo.ItemNumberOnOrder = ItemNumber_txt.Text;
            inVo.ItemNumberOnGoods = ItemNumberOnGoods_txt.Text;

            BooleanValueObject outVo = null;
            try
            {
                outVo = DefaultCbmInvoker.Invoke(varifyItemNumberBetweenWorkOrderAndActualGoodsCbm, inVo) as BooleanValueObject;
            }
            catch (Framework.ApplicationException exception)
            {
                logger.Error(exception.GetMessageData());
                popUpMessage.ApplicationError(exception.GetMessageData(), this.Text);

                ItemNumberOnGoods_txt.Clear();
                return false;
            }

            if (!outVo.BooleanValue)
            {
                ItemNumberOnGoods_txt.Clear();
                return false;
            }


            LotNumberOnGoods_txt.Focus();
            return true;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void LotNumberOnGoods_txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Tab)
            {
                return;
            }

            // Varify item again to make sure not only lot but item also matches at the same time before reflecting the pair into data grid view
            bool itemVarified = VarifyItemNumber();

            if (!itemVarified)
            {
                return;
            }


            string lotNumberOnOrder = LotNumber_txt.Text;

            string lotNumberOnGoods = LotNumberOnGoods_txt.Text;

            if (string.IsNullOrEmpty(lotNumberOnOrder) || string.IsNullOrWhiteSpace(lotNumberOnGoods))
            {
                var messageData = new MessageData("zwce00008", Properties.Resources.zwce00008, nameof(lotNumberOnOrder) + " or " + nameof(lotNumberOnGoods));
                logger.Error(messageData);
                popUpMessage.ApplicationError(messageData, this.Text);

                ItemNumberOnGoods_txt.Clear();
                LotNumberOnGoods_txt.Clear();
                ItemNumberOnGoods_txt.Focus();

                return;
            }

            if (!lotNumberOnGoods.Contains(lotNumberOnOrder))
            {
                var messageData = new MessageData("zwce00045", Properties.Resources.zwce00045, lotNumberOnOrder, lotNumberOnGoods);
                logger.Error(messageData);
                popUpMessage.ApplicationError(messageData, this.Text);

                ItemNumberOnGoods_txt.Clear();
                LotNumberOnGoods_txt.Clear();
                ItemNumberOnGoods_txt.Focus();

                return;
            }


            // Display varified item number and lot number onto the data grid view

            string itemNumberOnOrder = ItemNumber_txt.Text;

            string itemNumberOnGoods = ItemNumberOnGoods_txt.Text;

            int.TryParse(Quantity_txt.Text, out int targetQuantity);

            if (targetQuantity <= 0)
            {
                var messageData = new MessageData("zwce00046", Properties.Resources.zwce00046, Quantity_txt.Text);
                logger.Error(messageData);
                popUpMessage.ApplicationError(messageData, this.Text);

                ItemNumberOnGoods_txt.Clear();
                LotNumberOnGoods_txt.Clear();
                ItemNumberOnGoods_txt.Focus();

                return;
            }


            bool quantityReachedTraget = DisplayItemNumberAndLotNumberOnDateGridViewAndJudgeQuantity(itemNumberOnOrder, lotNumberOnOrder, targetQuantity, e);

            if (!quantityReachedTraget)
            {
                ItemNumberOnGoods_txt.Clear();
                LotNumberOnGoods_txt.Clear();
                ItemNumberOnGoods_txt.Focus();

                return;
            }


            var checkMessage = new MessageData("zwci00002", Properties.Resources.zwci00002, targetQuantity.ToString());

            DialogResult dialogResult = popUpMessage.ConfirmationYesNo(checkMessage, Text);

            if (dialogResult == DialogResult.Yes)
            {
                var messageData = new MessageData("zwci00001", Properties.Resources.zwci00001, targetQuantity.ToString(), WorkOrderNumber_txt.Text, WorkOrderLineNumber_txt.Text);
                logger.Info(messageData);
                popUpMessage.Information(messageData, this.Text);
            }
            else
            {
                var messageData = new MessageData("zwce00047", Properties.Resources.zwce00047);
                logger.Error(messageData);
                popUpMessage.ApplicationError(messageData, this.Text);
            }

            InitializeUiObjects();
            WorkOrderNumber_txt.Focus();

        }


        private bool DisplayItemNumberAndLotNumberOnDateGridViewAndJudgeQuantity(string itemNumberOnOrder, string lotNumberOnOrder, int targetQuantity, KeyEventArgs e)
        {
            List<VarificationGridViewItemLotVo> scannedItemLotList = ((BindingSource)ScannedGoodsItemLot_dgv.DataSource)?.List as List<VarificationGridViewItemLotVo>;

            if (scannedItemLotList == null)
            {
                scannedItemLotList = new List<VarificationGridViewItemLotVo>();
            }

            int currentSerial = scannedItemLotList.Count == 0 ? 0 : scannedItemLotList.Last().ScanSerial;

            VarificationGridViewItemLotVo newLine = new VarificationGridViewItemLotVo();
            newLine.ScanSerial = currentSerial + 1;
            newLine.ItemNumber = itemNumberOnOrder;
            newLine.LotNumber = lotNumberOnOrder;

            scannedItemLotList.Add(newLine);

            ScannedGoodsItemLot_dgv.DataSource = new BindingSource(scannedItemLotList, null);


            int scannedQuantity = scannedItemLotList.Count;

            ScannedGoodsItemLot_dgv.FirstDisplayedScrollingRowIndex = scannedQuantity - 1;


            if (scannedQuantity == targetQuantity)
            {
                return true;
            }
            else 
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearAll_btn_Click(object sender, EventArgs e)
        {
            InitializeUiObjects();

            WorkOrderNumber_txt.Focus();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Close_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void WorkOrderNumber_txt_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Keys.Tab)
            {
                e.IsInputKey = true;
            }
        }

        private void PrintedBarcode_txt_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Keys.Tab)
            {
                e.IsInputKey = true;
            }
        }

        private void LabelBarcode_txt_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Keys.Tab)
            {
                e.IsInputKey = true;
            }
        }

        private void ItemNumberOnGoods_txt_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Keys.Tab)
            {
                e.IsInputKey = true;
            }
        }

        private void LotNumberOnGoods_txt_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Keys.Tab)
            {
                e.IsInputKey = true;
            }
        }

    }
}
