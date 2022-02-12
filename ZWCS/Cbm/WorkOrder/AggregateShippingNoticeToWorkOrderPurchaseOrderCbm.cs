using System;
using System.Linq;
using Com.ZimVie.Wcs.Framework;
using Com.ZimVie.Wcs.ZWCS.Vo;
using Com.ZimVie.Wcs.ZWCS.Dao;
using System.Collections.Generic;

namespace Com.ZimVie.Wcs.ZWCS.Cbm
{
    /// <summary>
    /// CBM to aggregate shipping notice into work order - purchase order relationship by attached document number
    /// </summary>
    public class AggregateShippingNoticeToWorkOrderPurchaseOrderCbm : CbmController
    {
        /// <summary>
        /// Instantiate CommonLogger
        /// </summary>
        private static readonly CommonLogger logger = CommonLogger.GetInstance(typeof(AggregateShippingNoticeToWorkOrderPurchaseOrderCbm));

        /// <summary>
        /// Instantiate DAO to aggregate shipping notice into work order header by attached document number
        /// </summary>
        private readonly DataAccessObject aggregateShippingNoticeToWorkOrderPurchaseOrderDao = new AggregateShippingNoticeToWorkOrderPurchaseOrderDao();

        /// <summary>
        /// Instantiate DAO to create work order - purchase order relationship
        /// </summary>
        private readonly DataAccessObject createWorkOrderPurchaseOrderDao = new CreateWorkOrderPurchaseOrderDao();

        /// <summary>
        /// 1. Aggregate shipping notice into work order - purchase order relationship by attached document number        
        /// 2. Create work order - purchase order relationship in dateabase
        /// </summary>
        /// <param name="trxContext"></param>
        /// <param name="vo"></param>
        /// <returns></returns>
        public ValueObject Execute(TransactionContext trxContext, ValueObject vo)
        {

            ValueObjectList<WorkOrderHeaderVo> inVo = vo as ValueObjectList<WorkOrderHeaderVo>;
            List<WorkOrderHeaderVo> workOrderHeaders = inVo?.GetList();

            if (workOrderHeaders == null || workOrderHeaders.Count <= 0)
            {
                var messageData = new MessageData("zwce00008", Properties.Resources.zwce00008, nameof(workOrderHeaders));
                logger.Error(messageData);
                throw new Framework.ApplicationException(messageData);
            }

            WorkOrderPurchaseOrderCreationVo poInVo = new WorkOrderPurchaseOrderCreationVo();
            poInVo.ShippingNoticeId = workOrderHeaders.First().ShippingNoticeId;

            if (poInVo.ShippingNoticeId == 0)
            {
                var messageData = new MessageData("zwce00008", Properties.Resources.zwce00008, nameof(poInVo.ShippingNoticeId));
                logger.Error(messageData);
                throw new Framework.ApplicationException(messageData);
            }


            // 1. Aggregate shipping notice into work order - purchase order relationship by attached document number  

            ValueObjectList<WorkOrderPurchaseOrderVo> workOrderPurachaseOrderVo = aggregateShippingNoticeToWorkOrderPurchaseOrderDao.Execute(trxContext, poInVo) as ValueObjectList<WorkOrderPurchaseOrderVo>;

            List<WorkOrderPurchaseOrderVo> relations = workOrderPurachaseOrderVo?.GetList();

            if (relations == null || relations.Count <= 0)
            {
                var messageData = new MessageData("zwce00021", Properties.Resources.zwce00021, nameof(aggregateShippingNoticeToWorkOrderPurchaseOrderDao));
                logger.Error(messageData);
                throw new Framework.ApplicationException(messageData);
            }



            // 2. Assign purchase order numbers to each work order header value object and assign work order id to each work order - purchase order value object

            foreach (WorkOrderHeaderVo header in workOrderHeaders)
            {
                if (string.IsNullOrWhiteSpace(header.AttachedDocumentControlNumber))
                {
                    continue;
                }

                List<WorkOrderPurchaseOrderVo> matchingRelations = relations
                    .Where(r => r.AttachedDocumentControlNumber.Equals(header.AttachedDocumentControlNumber))
                    .OrderBy(r => r.PurchaseOrderNumber).ToList();

                if (matchingRelations.Count <= 0)
                {
                    continue;
                }

                // Assign purchase order numbers to each work order header value object

                List<string> purchaseOrders = matchingRelations.Select(r => r.PurchaseOrderNumber).ToList();

                header.PurchaseOrderNumbers = purchaseOrders;


                // Assign work order id to each work order - purchase order value object

                foreach (WorkOrderPurchaseOrderVo relation in matchingRelations)
                {
                    relation.WorkOrderId = header.WorkOrderId;
                }
            }


            // 3. Create work order headers - purchase order relationship in dateabase

            ResultVo relationCreationResult = createWorkOrderPurchaseOrderDao.Execute(trxContext, workOrderPurachaseOrderVo) as ResultVo;

            if (relationCreationResult == null || relationCreationResult.AffectedCount <= 0)
            {
                var messageData = new MessageData("zwce00019", Properties.Resources.zwce00019, nameof(createWorkOrderPurchaseOrderDao));
                logger.Error(messageData);
                throw new Framework.ApplicationException(messageData);
            }


            return inVo;

        }
    }
}
