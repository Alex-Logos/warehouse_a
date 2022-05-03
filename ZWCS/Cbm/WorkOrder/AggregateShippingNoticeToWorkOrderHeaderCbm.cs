using System;
using System.Linq;
using Com.ZimVie.Wcs.Framework;
using Com.ZimVie.Wcs.ZWCS.Vo;
using Com.ZimVie.Wcs.ZWCS.Dao;
using System.Collections.Generic;

namespace Com.ZimVie.Wcs.ZWCS.Cbm
{
    /// <summary>
    /// CBM to aggregate shipping notice into work order header by attached document number, then create work order headers
    /// </summary>
    public class AggregateShippingNoticeToWorkOrderHeaderCbm : CbmController
    {
        /// <summary>
        /// Instantiate CommonLogger
        /// </summary>
        private static readonly CommonLogger logger = CommonLogger.GetInstance(typeof(AggregateShippingNoticeToWorkOrderHeaderCbm));

        /// <summary>
        /// Instantiate DAO to aggregate shipping notice into work order header by attached document number
        /// </summary>
        private readonly DataAccessObject aggregateShippingNoticeToWorkOrderHeaderDao = new AggregateShippingNoticeToWorkOrderHeaderDao();

        /// <summary>
        /// Instantiate DAO to read current max work order number
        /// </summary>
        private readonly DataAccessObject readMaxWorkOrderNumberDao = new ReadMaxWorkOrderNumberDao();

        /// <summary>
        /// Instantiate DAO to create work order headers
        /// </summary>
        private readonly DataAccessObject createWorkOrderHeaderDao = new CreateWorkOrderHeaderDao();

        /// <summary>
        /// 1. Aggregate shipping notice into work order header by attached document number
        /// 2. Read current max work order number in database then assign work order numbers to the work order header to be created
        /// 3. Create work order headers in dateabase then reflect order ids for returning work order headers
        /// </summary>
        /// <param name="trxContext"></param>
        /// <param name="vo"></param>
        /// <returns></returns>
        public ValueObject Execute(TransactionContext trxContext, ValueObject vo)
        {
            WorkOrderHeaderCreationVo orderHeaderInVo = vo as WorkOrderHeaderCreationVo;
            int shippingNoticeId = orderHeaderInVo?.ShippingNoticeId ?? 0;
            string shippingNoticeTrackingNumber = orderHeaderInVo?.ShippingNoticeTrackingNumberr;

            if (orderHeaderInVo == null || shippingNoticeId == 0)
            {
                var messageData = new MessageData("zwce00008", Properties.Resources.zwce00008, nameof(AggregateShippingNoticeToWorkOrderHeaderCbm));
                logger.Error(messageData);
                throw new Framework.ApplicationException(messageData);
            }


            // 1. Aggregate shipping notice into work order header by attached document number

            ValueObjectList<WorkOrderHeaderVo> orderHeadersGenerated = aggregateShippingNoticeToWorkOrderHeaderDao.Execute(trxContext, orderHeaderInVo) as ValueObjectList<WorkOrderHeaderVo>;

            List<WorkOrderHeaderVo> headers = orderHeadersGenerated?.GetList();

            if (headers == null || headers.Count <= 0)
            {
                var messageData = new MessageData("zwce00013", Properties.Resources.zwce00013, nameof(aggregateShippingNoticeToWorkOrderHeaderDao));
                logger.Error(messageData);
                throw new Framework.ApplicationException(messageData);
            }


            // 2. Read current max work order number in database then assign work order numbers to the work order header to be created

            MaxWorkOrderNumberVo maxOrderNumber = readMaxWorkOrderNumberDao.Execute(trxContext, null) as MaxWorkOrderNumberVo;

            string todayDataPart = DateTime.Today.ToString("yyMMdd");
            int serial = 0;

            if (maxOrderNumber != null && !string.IsNullOrWhiteSpace(maxOrderNumber.OrderNumber))
            {
                if (maxOrderNumber.OrderNumber.Length != 9)
                {
                    var messageData = new MessageData("zwce00022", Properties.Resources.zwce00022, nameof(maxOrderNumber.OrderNumber));
                    logger.Error(messageData);
                    throw new Framework.ApplicationException(messageData);
                }

                string maxDatePart = maxOrderNumber.OrderNumber.Substring(0, 6);
                string maxSerialPart = maxOrderNumber.OrderNumber.Substring(6, 3);
                
                bool workOrderNumberSerialFormatCorrect = int.TryParse(maxSerialPart, out int maxSerial);

                if (!workOrderNumberSerialFormatCorrect)
                {
                    var messageData = new MessageData("zwce00022", Properties.Resources.zwce00022, nameof(maxOrderNumber.OrderNumber));
                    logger.Error(messageData);
                    throw new Framework.ApplicationException(messageData);
                }

                serial = maxDatePart == todayDataPart ? maxSerial : 0;
            }

            foreach (WorkOrderHeaderVo header in headers)
            {
                serial++;
                header.WorkOrderNumber = todayDataPart + serial.ToString("000");
                header.ShippingNoticeId = shippingNoticeId;
                header.ShippingNoticeTrackingNumber = shippingNoticeTrackingNumber;
            }


            // 3. Create work order headers in dateabase then reflect order ids for returning work order headers

            WorkOrderNumerIdPairVo headerCreationResult = createWorkOrderHeaderDao.Execute(trxContext, orderHeadersGenerated) as WorkOrderNumerIdPairVo;

            Dictionary<string, int> orderNumberIdPairs = headerCreationResult?.WorkOrderNumerIdPairDictionary;

            if (orderNumberIdPairs == null || orderNumberIdPairs.Count <= 0 || orderNumberIdPairs.Count != headers.Count)
            {
                var messageData = new MessageData("zwce00015", Properties.Resources.zwce00015, nameof(createWorkOrderHeaderDao));
                logger.Error(messageData);
                throw new Framework.ApplicationException(messageData);
            }

            foreach (WorkOrderHeaderVo header in headers)
            {
                header.WorkOrderId = orderNumberIdPairs[header.WorkOrderNumber];
            }


            return orderHeadersGenerated;

        }
    }
}
