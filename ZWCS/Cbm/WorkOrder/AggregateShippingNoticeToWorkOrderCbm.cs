using System;
using System.Linq;
using Com.ZimVie.Wcs.Framework;
using Com.ZimVie.Wcs.ZWCS.Vo;
using Com.ZimVie.Wcs.ZWCS.Dao;
using System.Collections.Generic;

namespace Com.ZimVie.Wcs.ZWCS.Cbm
{
    /// <summary>
    /// CBM to create work order header and create work order lines by aggregating shipping notice
    /// </summary>
    public class AggregateShippingNoticeToWorkOrderCbm : CbmController
    {
        /// <summary>
        /// Instantiate CommonLogger
        /// </summary>
        private static readonly CommonLogger logger = CommonLogger.GetInstance(typeof(AggregateShippingNoticeToWorkOrderCbm));

        /// <summary>
        /// Instantiate CBM to aggregate shipping notice into work order header by attached document number
        /// </summary>
        private readonly CbmController aggregateShippingNoticeToWorkOrderHeaderCbm = new AggregateShippingNoticeToWorkOrderHeaderCbm();

        /// <summary>
        /// Instantiate CBM to aggregate shipping notice into work order line by item and lot number
        /// </summary>
        private readonly CbmController aggregateShippingNoticeToWorkOrderLineCbm = new AggregateShippingNoticeToWorkOrderLineCbm();


        /// <summary>
        /// Create work order header and create work order lines by aggregating shipping notice
        /// 1. Aggregate shipping notice into work order header by attached document number
        /// 2. Aggregate shipping notice into work order line by item and lot number
        /// 4. Align work order header and line into hierarchical structure
        /// </summary>
        /// <param name="trxContext"></param>
        /// <param name="vo"></param>
        /// <returns></returns>
        public ValueObject Execute(TransactionContext trxContext, ValueObject vo)
        {
            ShippingNoticeVo shippingNoticeInVo = vo as ShippingNoticeVo;
            ShippingNoticeHeaderVo noticeHeader = shippingNoticeInVo?.Header;
            List<ShippingNoticeLineVo> noticeLines = shippingNoticeInVo?.Line;

            if (shippingNoticeInVo == null || noticeHeader == null || noticeLines == null || noticeLines.Count <= 0)
            {
                var messageData = new MessageData("zwce00008", Properties.Resources.zwce00008, nameof(AggregateShippingNoticeToWorkOrderCbm));
                logger.Error(messageData);
                throw new Framework.ApplicationException(messageData);
            }


            // 1. Aggregate shipping notice into work order header by attached document number
            WorkOrderHeaderCreationVo workOrderInParams = new WorkOrderHeaderCreationVo();
            workOrderInParams.ShippingNoticeId = noticeHeader.ShippingNoticeId;
            workOrderInParams.ShippingNoticeTrackingNumberr = noticeHeader.ShippingNoticeTrackingNumber;

            ValueObjectList<WorkOrderHeaderVo> headerOutVo = aggregateShippingNoticeToWorkOrderHeaderCbm.Execute(trxContext, workOrderInParams) as ValueObjectList<WorkOrderHeaderVo>;

            List<WorkOrderHeaderVo> workOrderHeaders = headerOutVo?.GetList();

            if (workOrderHeaders == null || workOrderHeaders.Count <= 0)
            {
                var messageData = new MessageData("zwce00011", Properties.Resources.zwce00011, nameof(aggregateShippingNoticeToWorkOrderHeaderCbm));
                logger.Error(messageData);
                throw new Framework.ApplicationException(messageData);
            }


            // 2. Aggregate shipping notice into work order line by item and lot number

            WorkOrderLineCreationVo workOrderLineCreationVo = new WorkOrderLineCreationVo();
            workOrderLineCreationVo.ShippingNoticeId = noticeHeader.ShippingNoticeId;
            workOrderLineCreationVo.WorkOrderHeaders = workOrderHeaders;

            ValueObjectList<WorkOrderLineVo> workOrdersWithLine = aggregateShippingNoticeToWorkOrderLineCbm.Execute(trxContext, workOrderLineCreationVo) as ValueObjectList<WorkOrderLineVo>;

            List<WorkOrderLineVo> workOrderLines = workOrdersWithLine?.GetList();

            if (workOrderLines == null || workOrderLines.Count <= 0)
            {
                var messageData = new MessageData("zwce00012", Properties.Resources.zwce00012, nameof(aggregateShippingNoticeToWorkOrderLineCbm));
                logger.Error(messageData);
                throw new Framework.ApplicationException(messageData);
            }


            // 4. Align work order header and line into hierarchical structure

            List<WorkOrderVo> workOrdersInHierarchy = new List<WorkOrderVo>();

            List<Tuple<int, int>> workOrderIdSubNumberPairs = workOrderLines.
                Select(l => new Tuple<int, int>(l.WorkOrderId, l.WorkOrderSubNumber)).Distinct().OrderBy(t => t).ToList();

            foreach (Tuple<int, int> key in workOrderIdSubNumberPairs)
            {
                WorkOrderVo order = new WorkOrderVo();

                order.Header = workOrderHeaders.First(h => h.WorkOrderId == key.Item1);

                List<WorkOrderLineVo> lines = workOrderLines.Where(l => l.WorkOrderId == key.Item1 && l.WorkOrderSubNumber == key.Item2 ).ToList();

                order.Lines = lines;

                workOrdersInHierarchy.Add(order);
            }

            ValueObjectList<WorkOrderVo> workOrderOutVo = new ValueObjectList<WorkOrderVo>();
            workOrderOutVo.SetNewList(workOrdersInHierarchy);


            return workOrderOutVo;

        }
    }
}
