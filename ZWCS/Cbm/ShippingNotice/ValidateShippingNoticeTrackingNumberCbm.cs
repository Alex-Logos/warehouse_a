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

namespace Com.ZimVie.Wcs.ZWCS.Cbm
{
    public class ValidateShippingNoticeTrackingNumberCbm : CbmController
    {
        /// <summary>
        /// Initialize CommonLogger
        /// </summary>
        private static readonly CommonLogger logger = CommonLogger.GetInstance(typeof(ValidateShippingNoticeTrackingNumberCbm));

        /// <summary>
        /// 
        /// </summary>
        private readonly DataAccessObject readShippingNoticeTrackingNumberDao = new ReadShippingNoticeTrackingNumberDao();

        public ValueObject Execute(TransactionContext trxContext, ValueObject vo)
        {

            var inVo = (ShippingNoticeTrackingNumberVo)vo; 

            if (string.IsNullOrEmpty(inVo.ShippingNoticeTrackingNumber))
            {
                var messageData = new MessageData("", Properties.Resources.zwce00008, nameof(inVo.ShippingNoticeTrackingNumber));
                logger.Error(messageData, new NullReferenceException());

                throw new Framework.ApplicationException(messageData, new NullReferenceException());
            }


            var outVo = readShippingNoticeTrackingNumberDao.Execute(trxContext, inVo) as ShippingNoticeTrackingNumberVo;

            if (outVo != null)
            {
                var messageData = new MessageData("zwce00002", Properties.Resources.zwce00002, 
                    outVo.ShippingNoticeTrackingNumber, 
                    outVo.RegistrationUserCode, 
                    outVo.RegistrationDateTime.ToString(UserData.GetUserData().DateTimeFormat),
                    outVo.SupplierNumber + ": " + outVo.SupplierName);

                logger.Info(messageData);
                throw new Framework.ApplicationException(messageData);
            }

            return new BooleanValueObject { BooleanValue = true };
        }
    }
}
