using System;
using System.Data;
using System.Text;
using Com.ZimVie.Wcs.Framework;
using Com.ZimVie.Wcs.ZWCS.Vo;
using System.Collections.Generic;

namespace Com.ZimVie.Wcs.ZWCS.Dao
{
    class AggregateShippingNoticeToWorkOrderHeaderDao : AbstractDataAccessObject
    {
        /// <summary>
        /// Instantiate CommonLogger
        /// </summary>
        private static readonly CommonLogger logger = CommonLogger.GetInstance(typeof(AggregateShippingNoticeToWorkOrderHeaderDao));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="trxContext"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        public override ValueObject Execute(TransactionContext trxContext, ValueObject arg)
        {

            WorkOrderHeaderCreationVo inVo = arg as WorkOrderHeaderCreationVo;

            if (inVo == null || inVo.ShippingNoticeId == 0)
            {
                var messageData = new MessageData("zwce00008", Properties.Resources.zwce00008, nameof(AggregateShippingNoticeToWorkOrderHeaderDao));
                logger.Error(messageData);
                throw new Framework.ApplicationException(messageData);
            }

            //create SQL
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append("SELECT ");
            sqlQuery.Append(" sl.purchase_order_number, ");
            sqlQuery.Append(" sl.commercial_invoice_number, ");
            sqlQuery.Append(" i.packing_material_1, ");
            sqlQuery.Append(" i.standard_work_instruction ");
            sqlQuery.Append("FROM t_shipping_notice_line sl ");
            sqlQuery.Append(" INNER JOIN m_item i USING(item_number) ");
            sqlQuery.Append("WHERE sl.warehouse_cd = :warehouseCode ");
            sqlQuery.Append(" AND sl.shipping_notice_id = :shippingNoticeId ");
            sqlQuery.Append("GROUP BY ");
            sqlQuery.Append(" sl.purchase_order_number, ");
            sqlQuery.Append(" sl.commercial_invoice_number, ");
            sqlQuery.Append(" i.packing_material_1, ");
            sqlQuery.Append(" i.standard_work_instruction ");
            sqlQuery.Append("ORDER BY ");
            sqlQuery.Append(" sl.purchase_order_number, ");
            sqlQuery.Append(" sl.commercial_invoice_number, ");
            sqlQuery.Append(" i.packing_material_1, ");
            sqlQuery.Append(" i.standard_work_instruction ");

            //create command
            DbCommandAdaptor sqlCommandAdapter = base.GetDbCommandAdaptor(trxContext, sqlQuery.ToString());

            //create parameter
            DbParameterList sqlParameter = sqlCommandAdapter.CreateParameterList();
            sqlParameter.AddParameterString("warehouseCode", trxContext.UserData.FactoryCode);
            sqlParameter.AddParameterInteger("shippingNoticeId", inVo.ShippingNoticeId);

            //execute SQL
            IDataReader dataReader = sqlCommandAdapter.ExecuteReader(trxContext, sqlParameter);

            ValueObjectList<WorkOrderHeaderVo> outVo = new ValueObjectList<WorkOrderHeaderVo>();

            while (dataReader.Read())
            {
                WorkOrderHeaderVo vo = new WorkOrderHeaderVo();
                vo.PurchaseOrderNumber = ConvertDBNull<string>(dataReader, "purchase_order_number");
                vo.CommercialInvoiceNumber = ConvertDBNull<string>(dataReader, "commercial_invoice_number");
                vo.PackingMaterial1 = ConvertDBNull<string>(dataReader, "packing_material_1");
                vo.StandardWorkInstruction = ConvertDBNull<string>(dataReader, "standard_work_instruction");
                outVo.add(vo);
            }
            dataReader.Close();

            return outVo;
        }
    }
}
