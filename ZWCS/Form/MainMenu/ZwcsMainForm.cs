using System;
using Com.ZimVie.Wcs.Framework;
using Com.ZimVie.Wcs.ZWCS.Cbm;
using Com.ZimVie.Wcs.ZWCS.Vo;
using System.Collections.Generic;
using System.Linq;

namespace Com.ZimVie.Wcs.ZWCS
{
    public partial class ZwcsMainForm
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
        /// Instantiate CBM to synchronize ZWCS item master to Kintone item master
        /// </summary>
        private readonly CbmController synchronizeItemMasterBetweenKintoneAndZwcsCbm = new SynchronizeItemMasterBetweenKintoneAndZwcsCbm();


        /// <summary>
        /// constructor
        /// </summary>
        public ZwcsMainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Main form loading
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            BptsSubMenu_gpb.Visible = false;
            MasterSubMenu_gpb.Visible = false;
        }

        /// <summary>
        /// Button click event to visible [Master maintenance menus]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Master_btn_Click(object sender, EventArgs e)
        {
            BptsSubMenu_gpb.Visible = false;
            MasterSubMenu_gpb.Visible = true;
        }

        /// <summary>
        /// Button click event to visible [BPTS menus]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Bpts_btn_Click(object sender, EventArgs e)
        {
            MasterSubMenu_gpb.Visible = false;
            BptsSubMenu_gpb.Visible = true;
        }

        /// <summary>
        /// Button click event to close the form
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImportShippingNoticeInternal_btn_Click(object sender, EventArgs e)
        {
            ShippingNoticeForm shippingNoticeForm = new ShippingNoticeForm();
            shippingNoticeForm.Show();
        }


        private void LabelPrintForManualInput_btn_Click(object sender, EventArgs e)
        {
            LabelPrintForManualInputForm labelPrintForManualInputForm = new LabelPrintForManualInputForm();
            labelPrintForManualInputForm.Show();
        }

        private void WorkVarification_btn_Click(object sender, EventArgs e)
        {
            WorkVarificationForm workVarificationForm = new WorkVarificationForm();
            workVarificationForm.Show();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemMasterSync_btn_Click(object sender, EventArgs e)
        {
            // Check updates on Kintone item master and reflect them to ZWCS item master if any

            using (new CursorWait())
            {

                ItemMasterUpdateOrCreateVo syncedItems = null;
                try
                {
                    syncedItems = DefaultCbmInvoker.Invoke(synchronizeItemMasterBetweenKintoneAndZwcsCbm, null) as ItemMasterUpdateOrCreateVo;
                }
                catch (Framework.ApplicationException exception)
                {
                    logger.Error(exception.GetMessageData());
                    popUpMessage.ApplicationError(exception.GetMessageData(), this.Text);
                    return;
                }

                List<ItemMasterVo> updatedItems = syncedItems?.UpdateItems;
                List<ItemMasterVo> createdItems = syncedItems?.CreateItems;

                bool updatedItemExists = updatedItems != null && updatedItems.Count > 0;
                bool createdItemExists = createdItems != null && createdItems.Count > 0;

                MessageData masterSyncMessage = null;

                if (updatedItemExists || createdItemExists)
                {
                    string updateItemDisplay = !updatedItemExists ? "なし" : string.Join(", ", updatedItems.Select(i => i.ItemNumber));
                    string createdItemDisplay = !createdItemExists ? "なし" : string.Join(", ", createdItems.Select(i => i.ItemNumber));
                    masterSyncMessage = new MessageData("zwci00006", Properties.Resources.zwci00006, updateItemDisplay, createdItemDisplay);
                }
                else
                {
                    masterSyncMessage = new MessageData("zwci00003", Properties.Resources.zwci00003);
                }

                logger.Info(masterSyncMessage);
                popUpMessage.Information(masterSyncMessage, this.Text);

            }
        }
    }
}
