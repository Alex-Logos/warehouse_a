using System;
using System.Data;
using System.Text;
using Com.ZimVie.Wcs.Framework;
using Com.ZimVie.Wcs.ZWCS.Vo;
using System.Collections.Generic;

namespace Com.ZimVie.Wcs.ZWCS.Dao
{
    class AggregateShippingNoticeToWorkOrderPurchaseOrderDao : AbstractDataAccessObject
    {
        /// <summary>
        /// Instantiate CommonLogger
        /// </summary>
        private static readonly CommonLogger logger = CommonLogger.GetInstance(typeof(AggregateShippingNoticeToWorkOrderPurchaseOrderDao));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="trxContext"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        public override ValueObject Execute(TransactionContext trxContext, ValueObject arg)
        {

            WorkOrderPurchaseOrderCreationVo inVo = arg as WorkOrderPurchaseOrderCreationVo;
     
            if (inVo == null || inVo.ShippingNoticeId ==0)
            {
                var messageData = new MessageData("zwce00008", Properties.Resources.zwce00008, nameof(AggregateShippingNoticeToWorkOrderPurchaseOrderDao));
                logger.Error(messageData);
                throw new Framework.ApplicationException(messageData);
            }

            //create SQL
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append("SELECT ");
            sqlQuery.Append(" i.attached_document_control_number, ");
            sqlQuery.Append(" sl.purchase_order_number ");
            sqlQuery.Append("FROM t_shipping_notice_line sl ");
            sqlQuery.Append(" LEFT JOIN m_item i USING(item_number)  ");
            sqlQuery.Append("WHERE sl.warehouse_cd = :warehouseCode ");
            sqlQuery.Append(" AND sl.shipping_notice_id = :shippingNoticeId ");
            sqlQuery.Append("GROUP BY ");
            sqlQuery.Append(" i.attached_document_control_number, ");
            sqlQuery.Append(" sl.purchase_order_number ");
            sqlQuery.Append("ORDER BY ");
            sqlQuery.Append(" i.attached_document_control_number, ");
            sqlQuery.Append(" sl.purchase_order_number ");

            //create command
            DbCommandAdaptor sqlCommandAdapter = base.GetDbCommandAdaptor(trxContext, sqlQuery.ToString());

            //create parameter
            DbParameterList sqlParameter = sqlCommandAdapter.CreateParameterList();
            sqlParameter.AddParameterString("warehouseCode", trxContext.UserData.FactoryCode);
            sqlParameter.AddParameterInteger("shippingNoticeId", inVo.ShippingNoticeId);

            //execute SQL
            IDataReader dataReader = sqlCommandAdapter.ExecuteReader(trxContext, sqlParameter);

            ValueObjectList<WorkOrderPurchaseOrderVo> outVo = new ValueObjectList<WorkOrderPurchaseOrderVo>();

            while (dataReader.Read())
            {
                WorkOrderPurchaseOrderVo vo = new WorkOrderPurchaseOrderVo();
                vo.AttachedDocumentControlNumber = ConvertDBNull<string>(dataReader, "attached_document_control_number");
                vo.PurchaseOrderNumber = ConvertDBNull<string>(dataReader, "purchase_order_number");
                outVo.add(vo);
            }
            dataReader.Close();

            return outVo;
        }
    }
}
