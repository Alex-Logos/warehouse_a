

using System;
using Com.ZimVie.Wcs.Framework;
using Com.ZimVie.Wcs.ZWCS.Vo;
using Com.ZimVie.Wcs.ZWCS.Dao;
using System.Collections.Generic;
using System.Linq;

namespace Com.ZimVie.Wcs.ZWCS.Cbm
{
    /// <summary>
    /// CMB to create shipping notice and create work order
    /// </summary>
    public class CreateShippingNoticeAndWorkOrderCbm : CbmController
    {
        /// <summary>
        /// Instantiate logger
        /// </summary>
        private static readonly CommonLogger logger = CommonLogger.GetInstance(typeof(CreateShippingNoticeAndWorkOrderCbm));

        /// <summary>
        /// Instantiate CBM to create shipping notice header and create shipping notice lines
        /// </summary>
        private readonly CbmController createShippingNoticeCbm = new CreateShippingNoticeCbm();

        /// <summary>
        /// Instantiate CBM to create work order header and work order lines
        /// </summary>
        private readonly CbmController aggregateShippingNoticeToWorkOrderCbm = new AggregateShippingNoticeToWorkOrderCbm();

        /// <summary>
        /// Create shipping notice and create work order
        /// </summary>
        /// <param name="trxContext"></param>
        /// <param name="vo"></param>
        /// <returns></returns>
        public ValueObject Execute(TransactionContext trxContext, ValueObject vo)
        {
            ShippingNoticeVo shippingNoticeInVo = vo as ShippingNoticeVo;
            

            // Create shipping notice header and create shipping notice lines
            ShippingNoticeVo shippingNoticeOutVo = DefaultCbmInvoker.Invoke(createShippingNoticeCbm, shippingNoticeInVo) as ShippingNoticeVo;

            ShippingNoticeHeaderVo header = shippingNoticeOutVo.Header;

            List<ShippingNoticeLineVo> lines = shippingNoticeOutVo.Line;

            if (shippingNoticeOutVo == null || header == null || lines == null || lines.Count <= 0)
            {
                var messageData = new MessageData("zwce00009", Properties.Resources.zwce00009);
                logger.Error(messageData);
                throw new Framework.ApplicationException(messageData);
            }


            // Create shipping notice and create work order
            ValueObjectList<WorkOrderVo> workOrders = aggregateShippingNoticeToWorkOrderCbm.Execute(trxContext, shippingNoticeOutVo) as ValueObjectList<WorkOrderVo>;

            if (workOrders == null || workOrders.GetCurrentList() == null || workOrders.GetCurrentList().Count <= 0)
            {
                var messageData = new MessageData("zwce00010", Properties.Resources.zwce00010);
                logger.Error(messageData);
                throw new Framework.ApplicationException(messageData);
            }

            return workOrders;
        }
    }
}
