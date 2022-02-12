using System;
using Com.ZimVie.Wcs.Framework;
using Com.ZimVie.Wcs.ZWCS.Cbm;
using Com.ZimVie.Wcs.ZWCS.Vo;
using System.Collections.Generic;
using System.Linq;

namespace Com.ZimVie.Wcs.ZWCS
{
    public partial class LabelPrintForManualInputForm : Framework.FormCommon
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
        /// Instantiate CBM to read item master's label related fields
        /// </summary>
        private readonly CbmController readItemMasterLabelFieldsForSingleItemCbm = new ReadItemMasterLabelFieldsForSingleItemCbm();

        /// <summary>
        /// Instantiate CBM to print label
        /// </summary>
        private readonly CbmController printLabelForManualInputCbm = new PrintLabelForManualInputCbm();

        /// <summary>
        /// Declare class variable for target item's label information 
        /// </summary>
        private ItemMasterLabelFieldsVo labelInfo = null;

        /// <summary>
        /// Define Label type Enum
        /// </summary>
        private enum LabelTypeEnum
        {
            ラベル不要 = 0,
            製品ラベル = 1,
            物流ラベル = 2
        }

        /// <summary>
        /// 
        /// </summary>
        public LabelPrintForManualInputForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LabelPrintForManualInputForm_Load(object sender, EventArgs e)
        {
            ExpirationDate_dtp.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemNumber_txt_Leave(object sender, EventArgs e)
        {
            string itemNumber = ItemNumber_txt.Text.Trim();

            if (string.IsNullOrWhiteSpace(itemNumber))
            {
                var messageData = new MessageData("zwce00008", Properties.Resources.zwce00008, nameof(itemNumber));
                logger.Error(messageData);
                popUpMessage.ApplicationError(messageData, this.Text);
                return;
            }

            // Initialize form class variable
            labelInfo = null;

            // Read item master label fields
            ItemMasterLabelFieldsQueryForSingleItemVo inVo = new ItemMasterLabelFieldsQueryForSingleItemVo();
            inVo.ItemNumber = itemNumber;

            try
            {
                labelInfo = DefaultCbmInvoker.Invoke(readItemMasterLabelFieldsForSingleItemCbm, inVo) as ItemMasterLabelFieldsVo;
            }
            catch (Framework.ApplicationException exception)
            {
                popUpMessage.ApplicationError(exception.GetMessageData(), this.Text);
                logger.Error(exception.GetMessageData());

                ClearUiItemRelatedFields();
                return;
            }

            if (labelInfo == null)
            {
                var messageData = new MessageData("zwce00026", Properties.Resources.zwce00026, itemNumber);
                logger.Error(messageData);
                popUpMessage.ApplicationError(messageData, this.Text);

                ClearUiItemRelatedFields();
                return;
            }

            if (labelInfo.LabelType == 0)
            {
                var messageData = new MessageData("zwce00034", Properties.Resources.zwce00034, itemNumber);
                logger.Error(messageData);
                popUpMessage.ApplicationError(messageData, this.Text);

                ClearUiItemRelatedFields();
                return;
            }


            // Set values on UI

            ItemName_txt.Text = labelInfo.ProductName;

            LabelType_txt.Text = (Enum.GetNames(typeof(LabelTypeEnum)))[labelInfo.LabelType];
        
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrintLabel_btn_Click(object sender, EventArgs e)
        {
            // Validate values on UI 

            bool uiValudationOk = ValidateUiFields();

            if (!uiValudationOk)
            {
                return;
            }

            // Send print instruction to label printer

            PrintLabelForManualInputVo inVo = new PrintLabelForManualInputVo();
            inVo.ItemNumber = ItemName_txt.Text.Trim();
            inVo.LotNumber = LotNumber_txt.Text.Trim();
            inVo.LabelQuantity = int.Parse(LabelQuantity_txt.Text.Trim());
            inVo.LabelFieldsVo = labelInfo;

            if (!string.IsNullOrWhiteSpace(ExpirationDate_dtp.Text))
            {
                inVo.ExpirationDate = ExpirationDate_dtp.Value;
            }


            ResultVo printResult = null;            
            try
            {
                printResult = DefaultCbmInvoker.Invoke(printLabelForManualInputCbm, inVo) as ResultVo;
            }
            catch (Framework.ApplicationException exception)
            {
                popUpMessage.ApplicationError(exception.GetMessageData(), this.Text);
                logger.Error(exception.GetMessageData());
                return;
            }

            if (printResult == null || printResult.AffectedCount <= 0)
            {
                MessageData messageData = new MessageData("zwce00029", Properties.Resources.zwce00029, nameof(printLabelForManualInputCbm));
                logger.Error(messageData);
                popUpMessage.ApplicationError(messageData, this.Text);
                return;
            }


            MessageData messageSuccess = new MessageData("zwce00035", Properties.Resources.zwce00035);
            logger.Info(messageSuccess);
            popUpMessage.Information(messageSuccess, this.Text);

            ClearAllUiFields();

            return;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Clear_btn_Click(object sender, EventArgs e)
        {
            ClearAllUiFields();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exit_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private void ClearUiItemRelatedFields()
        {
            ItemNumber_txt.Clear();

            ItemName_txt.Clear();

            LabelType_txt.Clear();

            labelInfo = null;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private void ClearAllUiFields()
        {
            ItemNumber_txt.Clear();

            ItemName_txt.Clear();

            LabelType_txt.Clear();

            LotNumber_txt.Clear();

            ExpirationDate_dtp.Clear();

            LabelQuantity_txt.Clear();

            labelInfo = null;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool ValidateUiFields()
        {
            if (labelInfo == null || string.IsNullOrWhiteSpace(labelInfo.ItemNumber) || !labelInfo.ItemNumber.Equals(ItemNumber_txt.Text.Trim()))
            {
                var messageData = new MessageData("zwce00008", Properties.Resources.zwce00008, nameof(ItemNumber_txt));
                logger.Warn(messageData);
                popUpMessage.Warning(messageData, this.Text);

                return false;
            }

            if (string.IsNullOrWhiteSpace(LotNumber_txt.Text))
            {
                var messageData = new MessageData("zwce00008", Properties.Resources.zwce00008, nameof(LotNumber_txt));
                logger.Warn(messageData);
                popUpMessage.Warning(messageData, this.Text);

                return false;
            }

            int.TryParse(LabelQuantity_txt.Text.Trim(), out int labelQuantity);

            if (labelQuantity == 0)
            {
                var messageData = new MessageData("zwce00008", Properties.Resources.zwce00008, nameof(LabelQuantity_txt));
                logger.Warn(messageData);
                popUpMessage.Warning(messageData, this.Text);

                return false;
            }

            return true;

        }

    }
}
