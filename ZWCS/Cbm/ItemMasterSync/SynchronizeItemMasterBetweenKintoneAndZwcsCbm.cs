using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using Com.ZimVie.Wcs.Framework;
using Com.ZimVie.Wcs.ZWCS.Vo;
using System.Linq;

namespace Com.ZimVie.Wcs.ZWCS.Cbm
{
    /// <summary>
    /// CBM to synchronize Kintone item master and ZWCS item master
    /// </summary>
    class SynchronizeItemMasterBetweenKintoneAndZwcsCbm : CbmController
    {
        /// <summary>
        /// Instantiate logger
        /// </summary>
        private static readonly CommonLogger logger = CommonLogger.GetInstance(typeof(SynchronizeItemMasterBetweenKintoneAndZwcsCbm));

        /// <summary>
        /// Instantiate CBM to read updated or newly created items on Kintone item master using ZWCS item master max registration date time
        /// </summary>
        private readonly CbmController readKintoneUpdatedOrCreatedItemsCbm = new ReadKintoneUpdatedOrCreatedItemsCbm();

        /// <summary>
        /// Instantiate CBM to create ZWCS item master history
        /// </summary>
        private readonly CbmController createZwcsItemHistoryCbm = new CreateZwcsItemHistoryCbm();

        /// <summary>
        /// Instantiate CBM to update or create ZWCS item master
        /// </summary>
        private readonly CbmController updateOrCreateZwcsItemsCbm = new UpdateOrCreateZwcsItemsCbm();


        /// <summary>
        /// Synchronize Kintone item master and ZWCS item master
        /// </summary>
        /// <param name="trxContext"></param>
        /// <param name="vo"></param>
        /// <returns></returns>
        public ValueObject Execute(TransactionContext trxContext, ValueObject vo)
        {
            if (vo != null)
            {
                var messageData = new MessageData("zwce00050", Properties.Resources.zwce00050, this.ToString());
                logger.Error(messageData);

                throw new Framework.ApplicationException(messageData);
            }


            // Read updated or newly created items on Kintone item master

            ValueObjectList<ItemMasterVo> kintoneVo = readKintoneUpdatedOrCreatedItemsCbm.Execute(trxContext, null) as ValueObjectList<ItemMasterVo>;

            List<ItemMasterVo> kintoneItems = kintoneVo?.GetList();

            if (kintoneItems == null || kintoneItems.Count <= 0)
            {
                var messageData = new MessageData("zwci00003", Properties.Resources.zwci00003);
                logger.Info(messageData);

                return new ItemMasterUpdateOrCreateVo();
            }


            // Create ZWCS item master history and return the items existing in ZWCS master

            ItemNumbersVo zwcsVo = createZwcsItemHistoryCbm.Execute(trxContext, kintoneVo) as ItemNumbersVo;

            List<string> zwcsExistingItemNumbers = zwcsVo?.ItemNumbers;

            bool kintoneItemsNotFoundInZwcsItemMaster = zwcsExistingItemNumbers == null || zwcsExistingItemNumbers.Count <= 0;


            // Update or create ZWCS item master

            ItemMasterUpdateOrCreateVo updateOrCreateItemVo = new ItemMasterUpdateOrCreateVo();

            if (kintoneItemsNotFoundInZwcsItemMaster)
            {
                updateOrCreateItemVo.UpdateItems = null;
                updateOrCreateItemVo.CreateItems = kintoneItems;
            }
            else
            {
                updateOrCreateItemVo.UpdateItems = kintoneItems.Where(i => zwcsExistingItemNumbers.Contains(i.ItemNumber)).ToList();
                updateOrCreateItemVo.CreateItems = kintoneItems.Where(i => !zwcsExistingItemNumbers.Contains(i.ItemNumber)).ToList();
            }

            ResultVo result = updateOrCreateZwcsItemsCbm.Execute(trxContext, updateOrCreateItemVo) as ResultVo;

            if (result == null || result.AffectedCount <= 0)
            {
                var messageData = new MessageData("zwce00049", Properties.Resources.zwce00049, nameof(updateOrCreateZwcsItemsCbm));
                logger.Error(messageData);

                throw new Framework.ApplicationException(messageData);
            }


            return updateOrCreateItemVo;

        }

    }
}
