using System;
using System.Linq;
using Com.ZimVie.Wcs.Framework;
using Com.ZimVie.Wcs.ZWCS.Vo;
using Com.ZimVie.Wcs.ZWCS.Dao;
using System.Collections.Generic;

namespace Com.ZimVie.Wcs.ZWCS.Cbm
{
    /// <summary>
    /// CBM to create ZWCS item master update history
    /// </summary>
    public class CreateZwcsItemHistoryCbm : CbmController
    {
        /// <summary>
        /// Instantiate logger
        /// </summary>
        private static readonly CommonLogger logger = CommonLogger.GetInstance(typeof(CreateZwcsItemHistoryCbm));

        /// <summary>
        /// Instantiate DAO to read ZWCS item master's existing records for target items
        /// </summary>
        private readonly DataAccessObject readZwcsUpdatOrCreateTargetItemsDao = new ReadZwcsUpdatOrCreateTargetItemsDao();

        /// <summary>
        /// Instantiate DAO to create ZWCS item master update history
        /// </summary>
        private readonly DataAccessObject createZwcsItemHistoryDao = new CreateZwcsItemHistoryDao();

        /// <summary>
        /// 1. Read ZWCS item master's existing records for target items    
        /// 2. Create ZWCS item master update history
        /// </summary>
        /// <param name="trxContext"></param>
        /// <param name="vo"></param>
        /// <returns></returns>
        public ValueObject Execute(TransactionContext trxContext, ValueObject vo)
        {

            ValueObjectList<ItemMasterVo> inVo = vo as ValueObjectList<ItemMasterVo>;

            List<ItemMasterVo> kintoneItems = inVo?.GetList();

            if (kintoneItems == null || kintoneItems.Count <= 0)
            {
                var messageData = new MessageData("zwce00008", Properties.Resources.zwce00008, nameof(kintoneItems));
                logger.Error(messageData);
                throw new Framework.ApplicationException(messageData);
            }


            // 1. Read ZWCS item master's existing records for target items

            List<string> kintoneItemNumbers = kintoneItems.Select(i => i.ItemNumber).ToList();

            ItemNumbersVo queryVo = new ItemNumbersVo { ItemNumbers = kintoneItemNumbers };

            ValueObjectList<ItemMasterVo> returnedVo = readZwcsUpdatOrCreateTargetItemsDao.Execute(trxContext, queryVo) as ValueObjectList<ItemMasterVo>;

            List<ItemMasterVo> zwcsItems = returnedVo?.GetList();

            if (zwcsItems == null || zwcsItems.Count <= 0)
            {
                var messageData = new MessageData("zwci00005", Properties.Resources.zwci00005);
                logger.Info(messageData);

                return null;
            }


            // 2. Create ZWCS item master update history

            ResultVo result = createZwcsItemHistoryDao.Execute(trxContext, returnedVo) as ResultVo;

            if (result == null || result.AffectedCount <= 0 || result.AffectedCount != zwcsItems.Count)
            {
                var messageData = new MessageData("zwce00052", Properties.Resources.zwce00052);
                logger.Error(messageData);
                throw new Framework.ApplicationException(messageData);
            }


            List<string> zwcsUpdatedItemsNumbers = zwcsItems.Select(i => i.ItemNumber).ToList();

            return new ItemNumbersVo { ItemNumbers = zwcsUpdatedItemsNumbers };

        }

    }
}
