using System;
using System.Linq;
using Com.ZimVie.Wcs.Framework;
using Com.ZimVie.Wcs.ZWCS.Vo;
using Com.ZimVie.Wcs.ZWCS.Dao;
using System.Collections.Generic;

namespace Com.ZimVie.Wcs.ZWCS.Cbm
{
    /// <summary>
    /// CBM to generate label value objects
    /// </summary>
    public class ReadItemMasterLabelFieldsForSingleItemCbm : CbmController
    {
        /// <summary>
        /// Instantiate logger
        /// </summary>
        private static readonly CommonLogger logger = CommonLogger.GetInstance(typeof(ReadItemMasterLabelFieldsForSingleItemCbm));

        /// <summary>
        /// Instantiate DAO to read item master's label related fields
        /// </summary>
        private readonly DataAccessObject readItemMasterLabelFieldsForSingleItemDao = new ReadItemMasterLabelFieldsForSingleItemDao();

        /// <summary>
        /// 1. Read item master label related fileds        
        /// 2. Generate label value objects
        /// </summary>
        /// <param name="trxContext"></param>
        /// <param name="vo"></param>
        /// <returns></returns>
        public ValueObject Execute(TransactionContext trxContext, ValueObject vo)
        {

            ItemMasterLabelFieldsQueryForSingleItemVo inVo = vo as ItemMasterLabelFieldsQueryForSingleItemVo;

            if (inVo == null || string.IsNullOrWhiteSpace(inVo.ItemNumber))
            {
                var messageData = new MessageData("zwce00008", Properties.Resources.zwce00008, nameof(inVo.ItemNumber));
                logger.Error(messageData);
                throw new Framework.ApplicationException(messageData);
            }


            ItemMasterLabelFieldsVo labelInfo = readItemMasterLabelFieldsForSingleItemDao.Execute(trxContext, inVo) as ItemMasterLabelFieldsVo;

            if (labelInfo == null)
            {
                var messageData = new MessageData("zwce00026", Properties.Resources.zwce00026, inVo.ItemNumber);
                logger.Error(messageData);
                throw new Framework.ApplicationException(messageData);
            }


            return labelInfo;

        }

    }
}
