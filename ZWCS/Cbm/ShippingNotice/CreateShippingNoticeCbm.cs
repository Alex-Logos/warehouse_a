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
    /// <summary>
    /// CBM to check duplicate of shipping notice traking number, create shipping notice header, and create shipping notice lines
    /// </summary>
    public class CreateShippingNoticeCbm : CbmController
    {
        /// <summary>
        /// Initialize logger
        /// </summary>
        private static readonly CommonLogger logger = CommonLogger.GetInstance(typeof(CreateShippingNoticeCbm));

        /// <summary>
        /// Instantiate CBM to validate shipping notice item numbers with item master
        /// </summary>
        private readonly CbmController validateItemNumbersCbm = new ValidateItemNumbersCbm();

        /// <summary>
        /// Initialize CBM to check duplicate of shipping notice traking number
        /// </summary>
        private readonly CbmController validateShippingNoticeTrackingNumberCbm = new ValidateShippingNoticeTrackingNumberCbm();

        /// <summary>
        /// Initialize DAO to create shipping notice header
        /// </summary>
        private readonly DataAccessObject createShippingNoticeHeaderDao = new CreateShippingNoticeHeaderDao();

        /// <summary>
        /// Initialize DAO to create shipping notice line
        /// </summary>
        private readonly DataAccessObject createShippingNoticeLineDao = new CreateShippingNoticeLineDao();


        /// <summary>
        /// Check duplicate of shipping notice traking number, create shipping notice header, and create shipping notice lines
        /// </summary>
        /// <param name="trxContext"></param>
        /// <param name="vo"></param>
        /// <returns></returns>
        public ValueObject Execute(TransactionContext trxContext, ValueObject vo)
        {
            var inVo = vo as ShippingNoticeVo;


            // Validate shipping notice item numbers with item master 

            List<ShippingNoticeLineVo> lines = inVo?.Line;

            if (lines == null || lines.Count == 0)
            {
                var messageData = new MessageData("", Properties.Resources.zwce00008, nameof(lines));
                logger.Error(messageData);
                throw new Framework.ApplicationException(messageData);
            }

            List<string> items = lines.Select(l => l.ItemNumber).Distinct().ToList();

            ItemNumbersVo itemsInVo = new ItemNumbersVo();
            itemsInVo.ItemNumbers = items;

            var itemsResult = validateItemNumbersCbm.Execute(trxContext, itemsInVo) as BooleanValueObject;

            if (!itemsResult.BooleanValue)
            {
                var messageData = new MessageData("zwce00060", Properties.Resources.zwce00060);
                logger.Error(messageData);
                throw new Framework.ApplicationException(messageData);
            }


            // Check duplicate of shipping notice traking number

            ShippingNoticeHeaderVo headerInVo = inVo?.Header;

            if (headerInVo == null)
            {
                var messageData = new MessageData("", Properties.Resources.zwce00008, nameof(headerInVo));
                logger.Error(messageData);
                throw new Framework.ApplicationException(messageData);
            }

            var checkInVo = new ShippingNoticeTrackingNumberVo();
            checkInVo.ShippingNoticeTrackingNumber = headerInVo.ShippingNoticeTrackingNumber;

            var checkOutVo = validateShippingNoticeTrackingNumberCbm.Execute(trxContext, checkInVo) as BooleanValueObject;

            if (!checkOutVo.BooleanValue)
            {
                var messageData = new MessageData("zwce00006", Properties.Resources.zwce00006, nameof(validateShippingNoticeTrackingNumberCbm));
                logger.Error(messageData);
                throw new Framework.ApplicationException(messageData);
            }


            // Create shipping notice header

            var headerOutVo = createShippingNoticeHeaderDao.Execute(trxContext, headerInVo) as ResultVo;

            if (headerOutVo == null || headerOutVo.ResultId <= 0)
            {
                var messageData = new MessageData("zwce00006", Properties.Resources.zwce00006, nameof(createShippingNoticeHeaderDao)); 
                logger.Error(messageData);
                throw new Framework.ApplicationException(messageData);
            }


            // Assign shipping notice ID to inVo

            headerInVo.ShippingNoticeId = headerOutVo.ResultId;

            lines.ForEach(l => l.ShippingNoticeId = headerOutVo.ResultId);


            // Create shipping notice line

            var lineInVo = new ValueObjectList<ShippingNoticeLineVo>();
            lineInVo.SetNewList(lines);

            var lineOutVo = createShippingNoticeLineDao.Execute(trxContext, lineInVo) as ResultVo;

            if (lineOutVo == null || lineOutVo.AffectedCount <= 0)
            {
                var messageData = new MessageData("zwce00006", Properties.Resources.zwce00006, nameof(createShippingNoticeLineDao)); 
                logger.Error(messageData);
                throw new Framework.ApplicationException(messageData);
            }


            return inVo;

        }
    }
}
