/*
 * Copyright 2021 by Taku Fujii, All rights reserved.
 *
 *  Change Tracking
 *  2021/09/14 <Change NO.0001> Newly created by Takusuke Fujii(ZBD G.K.)
 */
using System;
using Com.ZimVie.Wcs.Framework;
using Com.ZimVie.Wcs.ZWCS.Vo;
using Com.ZimVie.Wcs.ZWCS.Dao;
using System.Collections.Generic;
using System.Linq;

namespace Com.ZimVie.Wcs.ZWCS.Cbm
{
    public class ValidateItemNumbersCbm : CbmController
    {
        /// <summary>
        /// Initialize CommonLogger
        /// </summary>
        private static readonly CommonLogger logger = CommonLogger.GetInstance(typeof(ValidateItemNumbersCbm));

        /// <summary>
        /// 
        /// </summary>
        private readonly DataAccessObject readItemMasterItemNumbersDao = new ReadItemMasterItemNumbersDao();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="trxContext"></param>
        /// <param name="vo"></param>
        /// <returns></returns>
        public ValueObject Execute(TransactionContext trxContext, ValueObject vo)
        {

            ItemNumbersVo inVo = (ItemNumbersVo)vo;

            List<string> queryItemNumbers = inVo?.ItemNumbers;

            if (queryItemNumbers == null || queryItemNumbers.Count == 0)
            {
                var messageData = new MessageData("", Properties.Resources.zwce00008, nameof(queryItemNumbers));
                logger.Error(messageData, new NullReferenceException());
                throw new Framework.ApplicationException(messageData, new NullReferenceException());
            }


            ItemNumbersVo outVo = readItemMasterItemNumbersDao.Execute(trxContext, inVo) as ItemNumbersVo;


            List<string> masterItemNumbers = outVo?.ItemNumbers;

            if (masterItemNumbers == null)
            {
                var messageData = new MessageData("zwce00058", Properties.Resources.zwce00058);
                logger.Error(messageData);
                throw new Framework.ApplicationException(messageData);
            }

            if (queryItemNumbers.Count != masterItemNumbers.Count)
            {
                List<string> wrongItemNumbers = masterItemNumbers.Where(i => !masterItemNumbers.Contains(i)).ToList();
                var messageData = new MessageData("zwce00059", Properties.Resources.zwce00059, string.Join(", ", wrongItemNumbers));
                logger.Error(messageData);
                throw new Framework.ApplicationException(messageData);
            }

            return new BooleanValueObject { BooleanValue = true };

        }
    }
}
