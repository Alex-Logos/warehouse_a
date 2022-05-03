using System;
using System.Linq;
using Com.ZimVie.Wcs.Framework;
using Com.ZimVie.Wcs.ZWCS.Vo;
using Com.ZimVie.Wcs.ZWCS.Dao;
using System.Collections.Generic;

namespace Com.ZimVie.Wcs.ZWCS.Cbm
{
    /// <summary>
    /// CBM to aggregate shipping notice into work order line by attached document number, item, and lot, then create work order lines
    /// </summary>
    public class AggregateShippingNoticeToWorkOrderLineCbm : CbmController
    {
        /// <summary>
        /// Instantiate CommonLogger
        /// </summary>
        private static readonly CommonLogger logger = CommonLogger.GetInstance(typeof(AggregateShippingNoticeToWorkOrderHeaderCbm));

        /// <summary>
        /// Instantiate DAO to aggregate shipping notice into work order line by attached document number, item, and lot
        /// </summary>
        private readonly DataAccessObject aggregateShippingNoticeToWorkOrderLineDao = new AggregateShippingNoticeToWorkOrderLineDao();

        /// <summary>
        /// Instantiate DAO to create work order lines
        /// </summary>
        private readonly DataAccessObject createWorkOrderLineDao = new CreateWorkOrderLineDao();

        /// <summary>
        /// 1. Aggregate shipping notice into work order line by attached document number, item, and lot
        /// 2. Assign work order id, serial within work order, and page withing work order
        /// 3. Create work order lines in dateabase then reflect order ids for returning work order headers
        /// </summary>
        /// <param name="trxContext"></param>
        /// <param name="vo"></param>
        /// <returns></returns>
        public ValueObject Execute(TransactionContext trxContext, ValueObject vo)
        {
            WorkOrderLineCreationVo orderLineInVo = vo as WorkOrderLineCreationVo;
            List<WorkOrderHeaderVo> headers = orderLineInVo?.WorkOrderHeaders;

            if (orderLineInVo == null || orderLineInVo.ShippingNoticeId == 0 || headers == null || headers.Count <= 0)
            {
                var messageData = new MessageData("zwce00008", Properties.Resources.zwce00008, nameof(AggregateShippingNoticeToWorkOrderLineCbm));
                logger.Error(messageData);
                throw new Framework.ApplicationException(messageData);
            }


            // 1. Aggregate shipping notice into work order line by attached document number, item, and lot

            ValueObjectList<WorkOrderLineVo> orderLinesGenerated = aggregateShippingNoticeToWorkOrderLineDao.Execute(trxContext, orderLineInVo) as ValueObjectList<WorkOrderLineVo>;

            List<WorkOrderLineVo> lines = orderLinesGenerated?.GetList();

            if (lines == null || lines.Count <= 0)
            {
                var messageData = new MessageData("zwce00016", Properties.Resources.zwce00016, nameof(aggregateShippingNoticeToWorkOrderLineDao));
                logger.Error(messageData);
                throw new Framework.ApplicationException(messageData);
            }


            // 2. Assign work order id, serial within work order, and page withing work order

            List<Tuple<string, string, string, string>> lineKeys = lines.Select(
                l => new Tuple<string, string, string, string>(l.PurchaseOrderNumber, l.CommercialInvoiceNumber, l.PackingMaterial1, l.StandardWorkInstruction)).Distinct().ToList();

            Dictionary<Tuple<string, string, string, string >, int> keyOrderIdPairs = headers.ToDictionary(
                    h => new Tuple<string, string, string, string>(h.PurchaseOrderNumber, h.CommercialInvoiceNumber, h.PackingMaterial1, h.StandardWorkInstruction), 
                    h => h.WorkOrderId);

            if (lineKeys.Count != keyOrderIdPairs.Count)
            {
                var messageData = new MessageData("zwce00017", Properties.Resources.zwce00017, nameof(aggregateShippingNoticeToWorkOrderLineDao));
                logger.Error(messageData);
                throw new Framework.ApplicationException(messageData);
            }

            int previousWorkOrderId = 0;
            int previousSerialWithinWorkOrder = 0;
            int previousPageWithinWorkOrderSubNumber = 0;

            foreach (WorkOrderLineVo line in lines)
            {
                // Assign work order id
                var key = new Tuple<string, string, string, string>(line.PurchaseOrderNumber, line.CommercialInvoiceNumber, line.PackingMaterial1, line.StandardWorkInstruction);
                line.WorkOrderId = keyOrderIdPairs[key];
 
                // Assign serial within work order
                bool isWorkOrderIdNew = line.WorkOrderId != previousWorkOrderId;
                line.SerialWithinWorkOrder = isWorkOrderIdNew ? 1 : previousSerialWithinWorkOrder + 1;

                // Assign work order sub number
                bool isReminderZero = line.SerialWithinWorkOrder % 12 == 0;
                line.WorkOrderSubNumber = isReminderZero ? (line.SerialWithinWorkOrder / 12) : (line.SerialWithinWorkOrder / 12) + 1;

                // Assign serial within work order sub number
                line.SerialWithinWorkOrderSubNumber = line.SerialWithinWorkOrder - (12 * (line.WorkOrderSubNumber - 1));

                // Assign page within work order sub number
                bool isSerialOne = line.SerialWithinWorkOrderSubNumber == 1;
                bool isSerialFirstInPage = (line.SerialWithinWorkOrderSubNumber % 3) == 1;
                line.PageWithinWorkOrderSubNumber = isSerialOne ? 1 : isSerialFirstInPage ? previousPageWithinWorkOrderSubNumber + 1 : previousPageWithinWorkOrderSubNumber;

                // Hold values into local variables for the next line's evaluation
                previousWorkOrderId = line.WorkOrderId;
                previousSerialWithinWorkOrder = line.SerialWithinWorkOrder;
                previousPageWithinWorkOrderSubNumber = line.PageWithinWorkOrderSubNumber;
            }


            // 3. Create work order lines in dateabase

            ResultVo creationResult = createWorkOrderLineDao.Execute(trxContext, orderLinesGenerated) as ResultVo;

            if (creationResult == null || creationResult.AffectedCount != lines.Count)
            {
                var messageData = new MessageData("zwce00018", Properties.Resources.zwce00018, nameof(createWorkOrderLineDao));
                logger.Error(messageData);
                throw new Framework.ApplicationException(messageData);
            }

            return orderLinesGenerated;

        }
    }
}
